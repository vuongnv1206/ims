import { Subject, takeUntil } from 'rxjs';
import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AssignmentClient, AssignmentDTO, SubjectClient } from 'src/app/api/api-generate';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-assignment-detail',
  templateUrl: './assignment-detail.component.html',
})
export class AssignmentDetailComponent implements OnInit,OnDestroy {

  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as AssignmentDTO;
  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  currentSubject = {} as AssignmentDTO;

  constructor(
    private activatedRoute: ActivatedRoute,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private subjectService: SubjectClient,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private assignmentService : AssignmentClient
  ) { }

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
      this.saveBtnName = 'Cập nhật';
      this.closeBtnName = 'Hủy';
    } else {
      this.saveBtnName = 'Thêm';
      this.closeBtnName = 'Đóng';
    }
  }

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.assignmentService
      .assignmentGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: AssignmentDTO) => {
          this.selectedEntity = response;
          console.log(response);
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
          Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
      description: new FormControl(this.selectedEntity.description || null),
      subjectId : new FormControl(this.selectedEntity.subjectId || null),
    });
  }
  saveChange() {
    this.toggleBlockUI(true);

    this.saveData();
  }

  private saveData() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.form.get('subjectId').patchValue(this.config.data?.subjectId);
      this.assignmentService
        .assignmentPOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      console.log(this.form.value);
      this.assignmentService
        .assignmentPUT(this.config.data.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        });
    }
  }
   // Validate
   validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên subject' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    description: [{ type: 'required', message: 'Bạn phải nhập mô tả' }],
  };

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }

}
