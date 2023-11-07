import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ClassClient, ClassReponse, MilestoneClient, MilestoneDto, MilestoneResponse, ProjectClient, ProjectDto, ProjectReponse } from 'src/app/api/api-generate';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MilestoneDetailComponent } from './milestone-detail/milestone-detail.component';
import { MessageConstants } from 'src/app/shared/constants/message.const';

@Component({
  selector: 'app-milestone',
  templateUrl: './milestone.component.html',
})
export class MilestoneComponent implements OnInit, OnDestroy  {
//System variables
private ngUnsubscribe = new Subject<void>();
public blockedPanel: boolean = false;

public items: MilestoneDto[];

//Paging variables
public page: number = 1;
public itemsPerPage: number = 10;
public totalCount: number;
public keyWords: string;
public skip: number | null;
public take: number | null;
public sortField: string | null;

 //Filter variables
 projectId: number;
 classId:number ;
 classList: any[] = [];
 projectList: any[] = [];
 startDate: Date | null;
 dueDate: Date | null;
constructor(
  private milestoneService: MilestoneClient,
  private projectService: ProjectClient,
  private classService : ClassClient,
  public dialogService: DialogService,
  private notificationService: NotificationService,
  private confirmationService: ConfirmationService
) {

}
ngOnInit(): void {
  this.loadProjects();
  this.loadClasses();
  this.loadData();
}
ngOnDestroy(): void {
  this.ngUnsubscribe.next();
  this.ngUnsubscribe.complete();
}

loadData(selectionId = null) {
  this.toggleBlockUI(true);
  this.milestoneService
    .milestone(
      this.projectId,
      this.classId,
      this.startDate,
      this.dueDate,
      this.keyWords,
      this.page,
      this.itemsPerPage,
      this.skip,
      this.take,
      this.sortField
    )
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: MilestoneResponse) => {
        this.items = response.milestones;
        this.totalCount = response.page.toTalRecord;
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(true);
      },
    });
}

loadProjects() {
  this.projectService.projectGET().subscribe((response: ProjectReponse) => {
    response.projects.forEach(project => {
      this.projectList.push({
        label: project.name,
        value: project.id,
      });
    });
  });
}

loadClasses() {
  this.classService.classes().subscribe((response: ClassReponse) => {
    response.classes.forEach(c => {
      this.classList.push({
        label: c.name,
        value: c.id,
      });
    });
  });
}

showAddModal() {
  const ref = this.dialogService.open(MilestoneDetailComponent, {
    header: 'Add Milestone',
    width: '50%',
  });

  ref.onClose.subscribe((data: MilestoneDto) => {
    if (data) {
      this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
      this.loadData();
    }
  });
}

showEditModal(id: number) {
  const ref = this.dialogService.open(MilestoneDetailComponent, {
    data: {
      id: id,
    },
    header: 'Update Milestone',
    width: '50%',
  });

  ref.onClose.subscribe((data: MilestoneDto) => {
    if (data) {
      this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      this.loadData();
    }
  });
}

deleteItem(id: number) {
  this.confirmationService.confirm({
    message: MessageConstants.CONFIRM_DELETE_MSG,
    accept: () => {
      this.toggleBlockUI(true);
      this.milestoneService.deleteMilestone(id).subscribe({
        next: () => {
          this.notificationService.showSuccess(
            MessageConstants.DELETED_OK_MSG
          );
          this.loadData();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
    },
  });
}

pageChanged(event: any) {
  this.page = event.page + 1;
  this.itemsPerPage = event.rows;
  this.loadData({
    page: this.page,
    itemsPerPage: this.itemsPerPage,
  });
}

private toggleBlockUI(enabled: boolean) {
  if (enabled == true) {
    this.blockedPanel = true;
  } else {
    setTimeout(() => {
      this.blockedPanel = false;
    }, 1000);
  }
}
}
