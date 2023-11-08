import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '@angular/fire/auth';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject } from 'rxjs';
import { UserClient, UserDto } from 'src/app/api/api-generate';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-class-detail',
  templateUrl: './class-detail.component.html',
})
export class ClassDetailComponent implements OnInit,OnDestroy {
//System variables
private ngUnsubscribe = new Subject<void>();
public blockedPanel: boolean = false;

classId;

//Paging variables
public page: number = 1;
public itemsPerPage: number = 5;
public totalCount: number;
public keyWords: string |  null;
public skip: number | null;
public take: number | null;
public sortField: string | null;

//Api variables
public items: UserDto[];
public selectedItems: UserDto[] = [];


  constructor(
    private activatedRoute: ActivatedRoute,
    public dialogService: DialogService,
    private userService : UserClient,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private utilService : UtilityService,

  ) { }

  ngOnInit() {
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
