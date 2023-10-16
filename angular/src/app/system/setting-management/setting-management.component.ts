import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  SettingClient,
  SettingDto,
  SettingResponse,
  SettingType,
} from 'src/app/api/custom-api-generate';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { ConfirmationService, SelectItem } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { SettingDetailComponent } from './setting-detail/setting-detail.component';
import { MessageConstants } from 'src/app/shared/constants/message.const';

@Component({
  selector: 'app-setting-management',
  templateUrl: './setting-management.component.html',
  styleUrls: ['./setting-management.component.scss'],
})
export class SettingManagementComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  public items: SettingDto[];
  public selectedItems: SettingDto[] = [];

  //Paging variables
  public page: number = 1;
  public itemsPerPage: number = 3;
  public totalCount: number;
  public keyWords: string;
  public skip: number | null;
  public take: number | null;
  public sortField: string | null;

  settingTypes: any[]; // Mảng tùy chọn
  selectedSettingType: SettingType; // Trường được chọn

  // filter
  settingTypeFilter: SettingType;
  /**
   *
   */
  constructor(
    private settingService: SettingClient,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {
    this.settingTypes = Object.keys(SettingType)
      .filter((v) => isNaN(Number(v)))
      .map((key) => ({
        label: key,
        value: SettingType[key],
      }));
  }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  loadData(selectionId = null) {
    this.toggleBlockUI(true);
    this.settingService
      .settingGET(
        this.selectedSettingType,
        this.keyWords,
        this.page,
        this.itemsPerPage,
        this.skip,
        this.take,
        this.sortField
      )
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SettingResponse) => {
          this.items = response.settings;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(true);
        },
      });
  }

  getSettingTypeName(value: number) {
    return SettingType[value];
  }

  onSettingTypeChange(event: any) {
    this.loadData();
  }

  showAddModal() {
    const ref = this.dialogService.open(SettingDetailComponent, {
      header: 'Add new setting',
      width: '50%',
    });

    ref.onClose.subscribe((data: SettingDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
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
