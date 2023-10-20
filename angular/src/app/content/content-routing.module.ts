import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { SubjectComponent } from "./subject/subject.component";
import { NgModule } from "@angular/core";



const routes: Routes = [
  {
    path: 'subject',
    component: SubjectComponent,
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
