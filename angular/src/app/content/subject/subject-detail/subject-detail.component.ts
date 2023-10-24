import { Subject, takeUntil } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssignmentDTO, SubjectClient, SubjectReponse } from 'src/app/api/api-generate';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-subject-detail',
  templateUrl: './subject-detail.component.html',
})
export class SubjectDetailComponent implements OnInit ,OnDestroy {
   //System variables
   private ngUnsubscribe = new Subject<void>();
   public blockedPanel: boolean = false;

  subjectId;

   //Paging variables
   public page: number = 1;
   public itemsPerPage: number = 5;
   public totalCount: number;
   public keyWords: string |  null;
   public skip: number | null;
   public take: number | null;
   public sortField: string | null;

  //Api variables
  public items: AssignmentDTO[];
  public selectedItems: AssignmentDTO[] = [];

  constructor(private activatedRoute: ActivatedRoute,
    private subjectService: SubjectClient,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private utilService : UtilityService,
    ) {

  }
  ngOnInit() {

    this.subjectId = this.activatedRoute.snapshot.paramMap.get('id');

  }

  loadData(selectionId = null) {
    this.toggleBlockUI(true);

    this.subjectService
    .subjectGET2(this.keyWords, this.page, this.itemsPerPage, this.skip, this.take, this.sortField)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SubjectReponse) => {
          this.items = response.subjects;
          response.subjects.forEach(element => {
              
          });
          this.totalCount = response.page.toTalRecord;
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
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
