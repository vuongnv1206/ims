import { Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthClient, ClassClient, ClassDto, SettingClient, SettingResponse, SubjectClient, SubjectReponse, UserClient, UserResponse } from 'src/app/api/api-generate';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-class-modal',
  templateUrl: './class-modal.component.html',
})
export class ClassModalComponent implements OnInit ,OnDestroy{
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as ClassDto;


  //Filter variables
subjectId: number ;
settingId:number ;
assigneeId:number ;
subjectList: any[] = [];
settingList: any[] = [];
assigneeList:any[] = [];


  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private classService: ClassClient,
    public authService: AuthClient,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private subjectService: SubjectClient,
    private settingService:SettingClient,
    private userService:UserClient,
  ) { }

  ngOnInit() {
    this.loadAssignees();
    this.loadSettings();
    this.loadSubjects();
    this.buildForm();
    this.initFormData();
  }

  initFormData() {
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
    this.classService
      .classGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ClassDto) => {
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
      assigneeId: new FormControl(this.selectedEntity.assigneeId || null, Validators.required),
      subjectId: new FormControl(this.selectedEntity.subjectId || null, Validators.required),
      settingId: new FormControl(this.selectedEntity.settingId || null, Validators.required),
    });
  }

  saveChange() {
    this.toggleBlockUI(true);

    this.saveData();
  }

  private saveData() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.classService
        .classPOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      this.classService
        .classPUT(this.config.data.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        });
    }
  }

  loadSubjects() {
    this.subjectService.subjectGET2().subscribe((response: SubjectReponse) => {
      response.subjects.forEach(s => {
        this.subjectList.push({
          label: s.name,
          value: s.id,
        });
      });
    });
  }
  loadSettings() {
    this.settingService.settingGET().subscribe((response: SettingResponse) => {
      response.settings.forEach(s => {
        this.settingList.push({
          label: s.name,
          value: s.id,
        });
      });
    });
  }
  loadAssignees() {
    this.userService.users().subscribe((response: UserResponse) => {
      response.users.forEach(s => {
        this.assigneeList.push({
          label: s.fullName,
          value: s.id,
        });
      });
    });
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
