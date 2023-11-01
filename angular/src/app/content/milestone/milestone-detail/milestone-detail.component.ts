import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { MilestoneClient, MilestoneDto, ProjectClient, ProjectDto, ProjectReponse } from 'src/app/api/api-generate';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-milestone-detail',
  templateUrl: './milestone-detail.component.html',
})
export class MilestoneDetailComponent implements OnInit,OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  categories: any[] = [
    { name: 'Class', key: 'C' },
    { name: 'Project', key: 'P' },
];

showProjectDropdown: boolean = false;
showClassDropdown: boolean = false;


isUpdating: boolean = false;


  // Default
  public blockedPanel: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as MilestoneDto;

//Filter variables
projectId: number ;
classId:number ;
classList: any[] = [];
projectList: any[] = [];


  constructor(
    private utilService: UtilityService,
    private milestoneService: MilestoneClient,
    private projectService: ProjectClient,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.buildForm();
    this.initFormData();
  }

  initFormData() {
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetails(this.config.data.id);
      this.saveBtnName = 'Cập nhật';
      this.closeBtnName = 'Hủy';
    } else {
      this.loadProjects();
      this.saveBtnName = 'Thêm';
      this.closeBtnName = 'Đóng';
    }

  }

  loadFormDetails(id: number) {
    this.isUpdating = true;
    this.toggleBlockUI(true);
    this.milestoneService
      .milestoneGET(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: MilestoneDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.loadProjects();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  buildForm() {
    this.form = this.fb.group({
      startDate: new FormControl(this.selectedEntity.startDate || null,Validators.required),
      dueDate: new FormControl(this.selectedEntity.dueDate || null,Validators.required),
      description: new FormControl(this.selectedEntity.description || null),
      classId: new FormControl(this.selectedEntity.classId || null),
      projectId: new FormControl(this.selectedEntity.projectId || null, Validators.required),
      selectedCategory: new FormControl()
    });
  }

  saveChange() {
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.milestoneService
        .milestonePOST(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(this.form.value);
          this.toggleBlockUI(false);
        });
    } else {
      this.milestoneService
        .milestonePUT(this.config.data.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        });
    }
  }

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }


  validationMessages = {
    name: [
      { type: 'required', message: 'Bạn phải nhập tên' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    slug: [{ type: 'required', message: 'Bạn phải URL duy nhất' }],
    sku: [{ type: 'required', message: 'Bạn phải mã SKU sản phẩm' }],
    projectId: [{ type: 'required', message: 'Bạn phải chọn project' }],
    classId: [{ type: 'required', message: 'Bạn phải chọn class' }],
  };

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanel = false;
      }, 1000);
    }
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

  onRadioButtonClick(categoryKey: string) {
    if (categoryKey === 'P') {
      this.showProjectDropdown = true;
      this.showClassDropdown = false;
    } else if (categoryKey === 'C') {
      this.showProjectDropdown = false;
      this.showClassDropdown = true;
    }
  }
}
