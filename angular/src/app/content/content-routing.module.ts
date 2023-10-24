import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { SubjectComponent } from "./subject/subject.component";
import { NgModule } from "@angular/core";
import { ClassComponent } from "./class/class.component";
import { IssueComponent } from "./issue/issue.component";
import { SubjectDetailComponent } from "./subject/subject-detail/subject-detail.component";



const routes: Routes = [
  {
    path: 'subject',
    component: SubjectComponent,
  },
  {
    path: 'class',
    component: ClassComponent,
  },
  {
    path: 'issue',
    component: IssueComponent,
  },
  {
    path: 'subject/detail/:id',
    component: SubjectDetailComponent,
  },

];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class ContentRoutingModule { }
