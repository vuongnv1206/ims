import { RoleClaimDto } from './../../../api/api-generate';
import { Component, OnDestroy,EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { PermissionDto, RoleClient } from 'src/app/api/api-generate';

@Component({
  selector: 'app-permission-grant',
  templateUrl: './permission-grant.component.html',
  styleUrls: ['./permission-grant.component.css']
})
export class PermissionGrantComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  public roleClaims: RoleClaimDto[] = [];
  public permission: PermissionDto;
  public selectedRoleClaims: string[] = [];
  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private roleService: RoleClient,
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
    this.buildForm();
    this.loadDetail(this.config.data.id);
    this.saveBtnName = 'Cập nhật';
    this.closeBtnName = 'Hủy';
  }

  loadDetail(roleId: string) {
    this.toggleBlockUI(true);
    this.roleService
      .permissionsGET(roleId)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PermissionDto) => {
          this.permission = response;
          this.roleClaims = response.roleClaims;

          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    var roleClaimsUpdate: RoleClaimDto[] = [];
    for (let index = 0; index < this.roleClaims.length; index++) {
      const selected =
        this.selectedRoleClaims.filter(x => x == this.roleClaims[index].displayName).length > 0;
      if (selected) {
        roleClaimsUpdate.push({
          displayName : this.roleClaims[index].displayName,
          selected : selected,
          type : this.roleClaims[index].type,
          value : this.roleClaims[index].value
        });
      }
    };
    var updateValues: PermissionDto = {
      roleId: this.permission.roleId,
      roleClaims : roleClaimsUpdate,
    };
    this.roleService
      .permissionsPUT(updateValues)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(() => {
        this.toggleBlockUI(false);
        this.ref.close(this.form.value);
      });
  }

  buildForm() {
    this.form = this.fb.group({});
    //Fill value
    this.roleClaims.forEach(element => {
      if (element.selected) {
        this.selectedRoleClaims.push(element.displayName);
      }
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
