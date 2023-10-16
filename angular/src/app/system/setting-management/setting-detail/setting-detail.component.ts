import { Component, OnInit, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import {
  SettingClient,
  SettingDto,
  SettingType,
} from 'src/app/api/custom-api-generate';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-setting-detail',
  templateUrl: './setting-detail.component.html',
  styleUrls: ['./setting-detail.component.scss'],
})
export class SettingDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as SettingDto;

  // Validate
  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên nhóm' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    description: [{ type: 'required', message: 'Bạn phải mô tả' }],
  };

  settingTypes: any[]; // Mảng tùy chọn
  selectedSettingType: SettingType; // Trường được chọn

  

  /**
   *
   */
  constructor(
    private utilService: UtilityService,
    private settingService: SettingClient,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private fb: FormBuilder
  ) {
    this.settingTypes = Object.keys(SettingType)
      .filter((v) => isNaN(Number(v))).map((key) => ({
        label: key,
        value: SettingType[key],
      }));
  }

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      // this.loadDetail(this.config.data.id);
      this.saveBtnName = 'Update Setting';
      this.closeBtnName = 'Cancel';
    } else {
      this.saveBtnName = 'Add';
      this.closeBtnName = 'Close';
    }
  }

  saveChange() {
    this.toggleBlockUI(true);

    this.saveData();
  }

  private saveData() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.settingService
        .settingPOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      this.settingService
        .settingPUT(this.config.data.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        });
    }
  }

  buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(50),
          Validators.minLength(3),
        ])
      ),
      type: new FormControl(
        this.selectedEntity.type || null,
        Validators.compose([Validators.required])
      ),
      description: new FormControl(this.selectedEntity.description || null),
    });
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