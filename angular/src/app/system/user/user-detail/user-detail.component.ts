import { Component,EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UploadEvent } from 'primeng/fileupload';
import { forkJoin, takeUntil, Subject } from 'rxjs';
import { AuthClient, RoleClient, RoleDto, UserClient, UserDto } from 'src/app/api/api-generate';
import { MessageConstants } from 'src/app/shared/constants/message.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { RoleAssignComponent } from '../role-assign/role-assign.component';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit,OnDestroy {

  private ngUnsubscribe = new Subject<void>();
  public items: UserDto[]
  public selectedItems: UserDto[] = [];
  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public roles: any[] = [];
  selectedEntity = {} as UserDto;
  public avatarImage: string = ''; // Khởi tạo biến avatarImage
  public keyword: string = '';
  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private roleService: RoleClient,
    private userService: UserClient,
    public authService: AuthClient,
    private utilService: UtilityService,
    private notificationService : NotificationService,
    public dialogService: DialogService,
    private fb: FormBuilder
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
    var roles = this.roleService.all();
    this.toggleBlockUI(true);
    forkJoin({
      roles
    })
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
  loadFormDetails(id: string) {
    this.userService
      .userGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UserDto) => {
          this.selectedEntity = response;
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
      this.userService
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



  loadData(selectionId = null) {
    this.toggleBlockUI(true);
    this.userService
      .users(this.keyword)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UserDto[]) => {
          this.items = response;

          if (selectionId != null && this.items.length > 0) {
            this.selectedItems = this.items.filter(x => x.id == selectionId);
          }

          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  buildForm() {
    this.form = this.fb.group({
      avatar: new FormControl(this.selectedEntity.avatar || null),
      userName: new FormControl(this.selectedEntity.userName || null, Validators.required),
      email: new FormControl(this.selectedEntity.email || null, Validators.required),
      phoneNumber: new FormControl(this.selectedEntity.phoneNumber || null),
      birthDay: new FormControl(this.selectedEntity.birthDay || null),
      fullName: new FormControl(this.selectedEntity.fullName || null, Validators.required),
      password: new FormControl(
        null,
        Validators.compose([
          Validators.required,
          Validators.pattern(
            '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
          ),
        ])
      )
    });
    this.avatarImage = this.form.get('avatar').value;
  }

}
