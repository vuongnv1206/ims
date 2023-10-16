import { RoleComponent } from './role/role.component';
import { KeyFilterModule } from 'primeng/keyfilter';
import { PickListModule } from 'primeng/picklist';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { SystemRoutingModule } from './system-routing.module';
import { UserComponent } from './user/user.component';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { ToolbarModule } from 'primeng/toolbar';
import { TagModule } from 'primeng/tag';
import { FileUploadModule } from 'primeng/fileupload';
import { NightMarketSharedModule } from '../shared/modules/nightmarket-shared.module';
import { RoleAssignComponent } from './user/role-assign/role-assign.component';
import { RoleDetailComponent } from './role/role-detail/role-detail.component';
import { PermissionGrantComponent } from './role/permission-grant/permission-grant.component';
import { SettingManagementComponent } from './setting-management/setting-management.component';
import { SettingDetailComponent } from './setting-management/setting-detail/setting-detail.component';

@NgModule({
  declarations: [
    RoleComponent,
    UserComponent,
    UserDetailComponent,
    RoleAssignComponent,
    RoleDetailComponent,
    PermissionGrantComponent,
    SettingManagementComponent,
    SettingDetailComponent,
  ],
  imports: [
    CommonModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
    SystemRoutingModule,
    PickListModule,
    KeyFilterModule,
    FormsModule,
    ReactiveFormsModule,
    EditorModule,
    BadgeModule,
    TagModule,
    ConfirmDialogModule,
    ToastModule,
    ToolbarModule,
    DialogModule,
    FileUploadModule,
    CalendarModule,
    NightMarketSharedModule,
  ],
})
export class SystemModule {}
