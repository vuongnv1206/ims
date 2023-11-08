import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import {
  IssueClient,
  IssueDto,
  IssueResponse,
} from 'src/app/api/custom-api-generate';
import { MessageConstants } from 'src/app/shared/constants/message.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { IssueDetailComponent } from './issue-detail/issue-detail.component';

@Component({
  selector: 'app-issue',
  templateUrl: './issue.component.html',
})
export class IssueComponent implements OnInit {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  public items: IssueDto[];
  public assigneeId: any;
  public projectId: any;
  public issueSettingId: any;
  public milestoneId: any;
  public startDate: any;
  public dueDate: any;

  //Paging variables
  public page: number = 1;
  public itemsPerPage: number = 10;
  public totalCount: number;
  public keyWords: string;
  public skip: number | null;
  public take: number | null;
  public sortField: string | null;

  constructor(
    private issueClient: IssueClient,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData(request = null) {
    this.toggleBlockUI(true);
    this.issueClient
      .issue(
        this.assigneeId,
        this.projectId,
        this.issueSettingId,
        this.milestoneId,
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
        next: (response: IssueResponse) => {
          this.items = response.issues;
          this.totalCount = response.page.toTalRecord;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(true);
        },
      });
  }

  showEditModal(id: number) {
    const ref = this.dialogService.open(IssueDetailComponent, {
      data: {
        id: id,
      },
      header: 'Detail Issue',
      width: '50%',
    });

    ref.onClose.subscribe((data: IssueDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.loadData();
      }
    });
  }

  showAddModal() {
    const ref = this.dialogService.open(IssueDetailComponent, {
      header: 'Add new issue',
      width: '50%',
    });

    ref.onClose.subscribe((data: IssueDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.loadData();
      }
    });
  }

  deleteItem(id: number) {
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.toggleBlockUI(true);
        this.issueClient.deleteIssue(id).subscribe({
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

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }

  pageChanged(event: any) {
    this.page = event.page + 1;
    this.itemsPerPage = event.rows;
    this.loadData({
      page: this.page,
      itemsPerPage: this.itemsPerPage,
    });
  }
}
