import { Component, OnInit, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import {
  MilestoneClient,
  MilestoneDto,
  MilestoneResponse,
} from 'src/app/api/api-generate';
import { IssueClient, IssueDto } from 'src/app/api/custom-api-generate';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { Milestone, Label } from '../../../api/custom-api-generate';

@Component({
  selector: 'app-issue-detail',
  templateUrl: './issue-detail.component.html',
})
export class IssueDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  public milestoneList: any[] = [];
  milestoneId: number;

  public statusList: any[] = [
    {
      label: 'true',
      value: true,
    },
    {
      label: 'false',
      value: false,
    },
  ];
  statusId: boolean;

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as IssueDto;

  //Filter variables
  projectId: number;
  classId: number;

  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên nhóm' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    description: [{ type: 'required', message: 'Bạn phải mô tả' }],
  };

  constructor(
    private utilService: UtilityService,
    private issueService: IssueClient,
    private milestoneService: MilestoneClient,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private fb: FormBuilder
  ) {}

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  ngOnInit(): void {
    this.loadMileStone();
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
      this.saveBtnName = 'Update Setting';
      this.closeBtnName = 'Cancel';
    } else {
      this.saveBtnName = 'Add';
      this.closeBtnName = 'Close';
    }
  }

  loadDetail(id: number) {
    this.toggleBlockUI(true);
    this.issueService
      .id(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: IssueDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.issueService
        .issuePOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      this.issueService
        .issuePUT(this.config.data.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        });
    }
  }

  buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(50),
          Validators.minLength(3),
        ])
      ),
      projectId: new FormControl(
        1
      ),
      assigneeId: new FormControl('9e224968-33e4-4652-b7b7-8574d048cdb9'),
      isOpen: new FormControl(this.selectedEntity.isOpen || null),
      milestoneId: new FormControl(this.selectedEntity.milestoneId || null),
      startDate: new FormControl(this.selectedEntity.startDate || null),
      dueDate: new FormControl(this.selectedEntity.dueDate || null),
      description: new FormControl(this.selectedEntity.description || null),
    });
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }

  loadMileStone() {
    this.milestoneService
      .milestone()
      .subscribe((response: MilestoneResponse) => {
        response.milestones.forEach((s) => {
          this.milestoneList.push({
            label: s.description,
            value: s.id,
          });
        });
      });
  }
}
