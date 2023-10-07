import { Component,EventEmitter, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UploadEvent } from 'primeng/fileupload';
import { forkJoin, takeUntil, Subject } from 'rxjs';
import { AuthClient, RoleClient, RoleDto, UserClient, UserDto } from 'src/app/api/api-generate';
import { MessageConstants } from 'src/app/shared/constants/message.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { RoleAssignComponent } from '../role-assign/role-assign.component';
import { FileService } from 'src/app/shared/services/file.service';
import { UserClientCustom } from 'src/app/api/custom-api-generate';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public items: UserDto[];
  public selectedItems: UserDto[] = [];
  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public roles: any[] = [];
  selectedEntity = {} as UserDto;
  public avatarImage : any;
  public keyword: string = '';
  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private roleService: RoleClient,
    private userService: UserClient,
    private userServiceCustom: UserClientCustom,
    public authService: AuthClient,
    private utilService: UtilityService,
    private notificationService : NotificationService,
    private cd: ChangeDetectorRef,
    public dialogService: DialogService,
    private fb: FormBuilder,
    private fileService: FileService,
    private utilityService: UtilityService,
  ) {}
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit() {
    //Init form
    this.buildForm();
    //Load data to form
    this.toggleBlockUI(true);
    this.roleService.all()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (repsonse: any) => {
          //Push categories to dropdown list
          var roles = repsonse.roles as RoleDto[];
          roles.forEach(element => {
            this.roles.push({
              value: element.id,
              label: element.name,
            });
          });

          if (this.utilService.isEmpty(this.config.data?.id) == false) {
            this.loadFormDetails(this.config.data?.id);
          } else {
            this.setMode('create');
            this.toggleBlockUI(false);
          }
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
    
  }

  public async GetFileFromFirebase(fileName: string) {
    if (fileName === null) {
      return 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuH4tyG12O2rHbqYAnze8XPhJhJzKmWibEqgC_wrPVfBJu8iBHMebhXA1afSZZ6mZMQmg&usqp=CAU';
    }

    let file = await this.fileService.GetFileFromFirebase(fileName);

    return file;
  }

  loadFormDetails(id: string) {
    this.userService
      .userGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: async (response: UserDto) => {
          this.selectedEntity = response;
          this.selectedEntity.avatar = await this.GetFileFromFirebase(response.avatar);
          this.buildForm();
          this.setMode('update');

          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  // Validate
  validationMessages = {
    fullName: [{ type: 'required', message: 'Bạn phải nhập tên' }],
    email: [{ type: 'required', message: 'Bạn phải nhập email' }],
    userName: [{ type: 'required', message: 'Bạn phải nhập tài khoản' }],
    password: [
      { type: 'required', message: 'Bạn phải nhập mật khẩu' },
      {
        type: 'pattern',
        message: 'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
      },
    ],
    phoneNumber: [{ type: 'required', message: 'Bạn phải nhập số điện thoại' }],
  };

  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    this.toggleBlockUI(true);
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.userService                 //Create
        .userPOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.ref.close(this.form.value);
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    } else {
      this.userServiceCustom
        .userPUT(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);

            this.ref.close(this.form.value);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }
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

  setMode(mode: string) {
    if (mode == 'update') {
      this.form.controls['userName'].clearValidators();
      this.form.controls['userName'].disable();
      this.form.controls['email'].clearValidators();
      this.form.controls['password'].clearValidators();
      this.form.controls['password'].disable();
    } else if (mode == 'create') {
      this.form.controls['userName'].addValidators(Validators.required);
      this.form.controls['userName'].enable();
      this.form.controls['email'].addValidators(Validators.required);
      this.form.controls['email'].enable();
      this.form.controls['password'].addValidators(Validators.required);
      this.form.controls['password'].enable();
    }
  }

  buildForm() {
    this.form = this.fb.group({
      avatar: new FormControl(this.selectedEntity.avatar || null),
      fileImage: new FormControl(null),
      userName: new FormControl(this.selectedEntity.userName || null,Validators.required),
      email: new FormControl(this.selectedEntity.email || null,Validators.required),
      phoneNumber: new FormControl(this.selectedEntity.phoneNumber || null),
      birthDay: new FormControl(this.selectedEntity.birthDay || null),
      fullName: new FormControl(this.selectedEntity.fullName || null,Validators.required),
      password: new FormControl(
        null,
        Validators.compose([
          Validators.required,
          Validators.pattern(
            '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
          ),
        ])
      ),
    });
    
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
        this.form.get('fileImage').setValue(file);
      // Đọc dữ liệu của file thành URL cho việc hiển thị ảnh trước
      const reader = new FileReader();
      reader.onload = (e: any) => {
        // Cập nhật giá trị của FormControl 'avatar' với URL hình ảnh đã đọc
        this.form.get('avatar').setValue(e.target.result);
      };
      reader.readAsDataURL(file);
    }

  }
  
}
