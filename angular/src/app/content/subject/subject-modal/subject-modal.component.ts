import { Subject, takeUntil } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthClient, SubjectClient, SubjectDto } from './../../../api/api-generate';
import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-subject-modal',
  templateUrl: './subject-modal.component.html',
})
export class SubjectModalComponent implements OnInit,OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as SubjectDto;

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();


  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private subjectService: SubjectClient,
    public authService: AuthClient,
    private utilService: UtilityService,
    private fb: FormBuilder
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
    this.subjectService
      .subjectGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SubjectDto) => {
          this.selectedEntity = response;
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
    });
  }
  saveChange() {
    this.toggleBlockUI(true);

    this.saveData();
  }

  private saveData() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.subjectService
        .subjectPOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      this.subjectService
        .subjectPUT(this.config.data.id, this.form.value)
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
