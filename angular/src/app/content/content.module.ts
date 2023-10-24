import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { SystemRoutingModule } from '../system/system-routing.module';
import { PickListModule } from 'primeng/picklist';
import { KeyFilterModule } from 'primeng/keyfilter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { FileUploadModule } from 'primeng/fileupload';
import { NightMarketSharedModule } from '../shared/modules/nightmarket-shared.module';
import { SubjectComponent } from './subject/subject.component';
import { ContentRoutingModule } from './content-routing.module';
import { ClassComponent } from './class/class.component';
import { IssueComponent } from './issue/issue.component';
import { AccordionModule } from 'primeng/accordion';
import { SubjectDetailComponent } from './subject/subject-detail/subject-detail.component';
import { SubjectModalComponent } from './subject/subject-modal/subject-modal.component';
import { AssignmentDetailComponent } from './subject/subject-detail/assignment-detail/assignment-detail.component';

@NgModule({
  declarations: [
    SubjectComponent,
    ClassComponent,
    IssueComponent,
    SubjectDetailComponent,
    SubjectModalComponent,
    AssignmentDetailComponent,
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
    PickListModule,
    KeyFilterModule,
    FormsModule,
    ReactiveFormsModule,
    BadgeModule,
    TagModule,
    ConfirmDialogModule,
    ToastModule,
    ToolbarModule,
    DialogModule,
    FileUploadModule,
    CalendarModule,
    NightMarketSharedModule,
    ContentRoutingModule,
    AccordionModule
  ],
})
export class ContentModule { }
