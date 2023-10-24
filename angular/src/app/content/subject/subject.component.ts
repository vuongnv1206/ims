import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { SubjectClient, SubjectDto, SubjectReponse } from 'src/app/api/api-generate';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { SubjectDetailComponent } from './subject-detail/subject-detail.component';
import { MessageConstants } from 'src/app/shared/constants/message.const';
import { Router } from '@angular/router';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { SubjectModalComponent } from './subject-modal/subject-modal.component';
import * as FileSaver from 'file-saver';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';

interface ExportColumn {
  title: string;
  dataKey: string;
}
@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
})
export class SubjectComponent implements OnInit,OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;
  exportColumns!: ExportColumn[];
   //Paging variables
   public page: number = 1;
   public itemsPerPage: number = 5;
   public totalCount: number;
   public keyWords: string |  null;
   public skip: number | null;
   public take: number | null;
   public sortField: string | null;

  //Api variables
  public items: SubjectDto[];
  public selectedItems: SubjectDto[] = [];


  constructor(
    private subjectService: SubjectClient,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private utilService : UtilityService,
  ) { }


  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit() {
    this.loadData();
  }

  loadData(selectionId = null) {
    this.toggleBlockUI(true);

    this.subjectService
    .subjectGET2(this.keyWords, this.page, this.itemsPerPage, this.skip, this.take, this.sortField)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SubjectReponse) => {
          this.items = response.subjects;
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

  showAddModal() {
    const ref = this.dialogService.open(SubjectModalComponent, {
      header: 'Add Subject',
      width: '70%',
    });

    ref.onClose.subscribe((data: SubjectDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    var id = this.selectedItems[0].id;
    const ref = this.dialogService.open(SubjectModalComponent, {
      data: {
        id: id,
      },
      header: 'Update subject',
      width: '70%',
    });

    ref.onClose.subscribe((data: SubjectDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData(data.id);
      }
    });
  }

  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteItemsConfirm(ids);
      },
    });
  }

  deleteItemsConfirm(id: any) {
    this.toggleBlockUI(true);

    this.subjectService.subjectDELETE(id).subscribe({
      next: () => {
        this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
        this.loadData();
        this.selectedItems = [];
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

  activeIndex: number = -1; // Khởi tạo activeIndex với giá trị mặc định

  onTabClick(index: number) {
    if (this.activeIndex === index) {
      // Nếu tab đã được chọn rồi, bỏ chọn nó
      this.activeIndex = null;
      this.selectedItems = [];
    } else {
      // Nếu tab chưa được chọn, chọn nó và cập nhật selectedItems
      this.activeIndex = index;
      this.selectedItems = [this.items[index]];
    }
  }

  showDetail(subjectDto: SubjectDto) {
    let url: string = "detail/" + subjectDto.id;
    this.utilService.navigate(url);
  }
  exportPdf() {
    import('jspdf').then((jsPDF) => {
        import('jspdf-autotable').then((x) => {
            const doc = new jsPDF.default('p', 'px', 'a4');
            (doc as any).autoTable(this.exportColumns, this.items);
            doc.save('products.pdf');
        });
    });
}

  exportExcel() {
    import('xlsx').then((xlsx) => {
        const worksheet = xlsx.utils.json_to_sheet(this.items);
        const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, 'products');
    });
}

saveAsExcelFile(buffer: any, fileName: string): void {
  let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  let EXCEL_EXTENSION = '.xlsx';
  const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE
  });
  FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
}

}
