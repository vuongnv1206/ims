import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';
import { SettingManagementComponent } from './setting-management/setting-management.component';

const routes: Routes = [
  {
    path: 'role',
    component: RoleComponent,
  },
  {
    path: 'user',
    component: UserComponent,
  },
  {
    path: 'setting',
    component: SettingManagementComponent,
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class SystemRoutingModule { }
