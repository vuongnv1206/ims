import { Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-setting-management',
  templateUrl: './setting-management.component.html',
  styleUrls: ['./setting-management.component.scss'],
})
export class SettingManagementComponent implements OnInit {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  public items: SettingDto[];
  public selectedItems: SettingDto[] = [];
  public keyword: string = '';

  //Paging variables
  public page: number = 1;
  public itemsPerPage: number = 3;
  public totalCount: number;
  public keyWords: string | null;
  public skip: number | null;
  public take: number | null;
  public sortField: string | null;

  selectedSettingType: SettingType;
  settingTypes: SelectItem[] = [
    { label: 'Semester', value: SettingType.Semester },
    { label: 'Domain', value: SettingType.Domain },
  ];

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
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(selectionId = null) {
    this.toggleBlockUI(true);
    this.settingService
      .settingGET(
        this.settingTypeFilter,
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
    this.settingTypeFilter = event.value;
    this.loadData();
    // Bây giờ bạn có thể thực hiện xử lý dựa trên giá trị đã chọn
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
