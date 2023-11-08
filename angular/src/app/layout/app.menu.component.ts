import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { TokenService } from '../shared/services/token.service';
import { LOGIN_URL } from '../shared/constants/url.const';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
  styles: [' .hidden { display: none; } ']
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService,
    private tokenService: TokenService,
    public router: Router,
    ) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-home',
            routerLink: ['/'],
          },
        ],
      },
      {
        label: 'System',
        items: [
          {
            label: 'Role Manager',
            icon: 'pi pi-fw pi-eye',
            routerLink: ['/system/role'],
            attributes: {
              "policyName": "Permissions.Roles.View"
            }
          },
          {
            label: 'User Manager',
            icon: 'pi pi-fw pi-user',
            routerLink: ['/system/user'],
            attributes: {
              "policyName": "Permissions.Users.View"
            }
          },
          {
            label: 'Setting Manager',
            icon: 'pi pi-spin pi-cog',
            routerLink: ['/system/setting'],
          },
        ],
      },
      {
        label: 'Content ',
        items: [
          {
            label: 'Subject Manager',
            icon: 'pi pi-fw pi-id-card',
            routerLink: ['/content/subject'],
          },
          {
            label: 'Class Manager',
            icon: 'pi pi-fw pi-check-square',
            routerLink: ['/content/class'],
          },
          {
            label: 'Issue Manager',
            icon: 'pi pi-fw pi-bookmark',
            routerLink: ['/content/issue'],
          },
          {
            label: 'Milestone Manager',
            icon: 'pi pi-fw pi-exclamation-circle',
            routerLink: ['/content/milestone'],
          },
          {
            label: 'Button',
            icon: 'pi pi-fw pi-box',
            routerLink: ['/uikit/button'],
          },
          {
            label: 'Table',
            icon: 'pi pi-fw pi-table',
            routerLink: ['/uikit/table'],
          },
          {
            label: 'List',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/uikit/list'],
          },
          {
            label: 'Tree',
            icon: 'pi pi-fw pi-share-alt',
            routerLink: ['/uikit/tree'],
          },
          {
            label: 'Panel',
            icon: 'pi pi-fw pi-tablet',
            routerLink: ['/uikit/panel'],
          },
          {
            label: 'Overlay',
            icon: 'pi pi-fw pi-clone',
            routerLink: ['/uikit/overlay'],
          },
          {
            label: 'Media',
            icon: 'pi pi-fw pi-image',
            routerLink: ['/uikit/media'],
          },
          {
            label: 'Menu',
            icon: 'pi pi-fw pi-bars',
            routerLink: ['/uikit/menu'],
            routerLinkActiveOptions: {
              paths: 'subset',
              queryParams: 'ignored',
              matrixParams: 'ignored',
              fragment: 'ignored',
            },
          },
          {
            label: 'Message',
            icon: 'pi pi-fw pi-comment',
            routerLink: ['/uikit/message'],
          },
          {
            label: 'File',
            icon: 'pi pi-fw pi-file',
            routerLink: ['/uikit/file'],
          },
          {
            label: 'Chart',
            icon: 'pi pi-fw pi-chart-bar',
            routerLink: ['/uikit/charts'],
          },
          {
            label: 'Misc',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/uikit/misc'],
          },
        ],
      },
      {
        label: 'Prime Blocks',
        items: [
          {
            label: 'Free Blocks',
            icon: 'pi pi-fw pi-eye',
            routerLink: ['/blocks'],
            badge: 'NEW',
          },
          {
            label: 'All Blocks',
            icon: 'pi pi-fw pi-globe',
            url: ['https://www.primefaces.org/primeblocks-ng'],
            target: '_blank',
          },
        ],
      },
      {
        label: 'Utilities',
        items: [
          {
            label: 'PrimeIcons',
            icon: 'pi pi-fw pi-prime',
            routerLink: ['/utilities/icons'],
          },
          {
            label: 'PrimeFlex',
            icon: 'pi pi-fw pi-desktop',
            url: ['https://www.primefaces.org/primeflex/'],
            target: '_blank',
          },
        ],
      },
      {
        label: 'Pages',
        icon: 'pi pi-fw pi-briefcase',
        items: [
          {
            label: 'Landing',
            icon: 'pi pi-fw pi-globe',
            routerLink: ['/landing'],
          },
          {
            label: 'Auth',
            icon: 'pi pi-fw pi-user',
            items: [
              {
                label: 'Login',
                icon: 'pi pi-fw pi-sign-in',
                routerLink: ['/auth/login'],
              },
              {
                label: 'Error',
                icon: 'pi pi-fw pi-times-circle',
                routerLink: ['/auth/error'],
              },
              {
                label: 'Access Denied',
                icon: 'pi pi-fw pi-lock',
                routerLink: ['/auth/access'],
              },
            ],
          },
          {
            label: 'Crud',
            icon: 'pi pi-fw pi-pencil',
            routerLink: ['/pages/crud'],
          },
          {
            label: 'Timeline',
            icon: 'pi pi-fw pi-calendar',
            routerLink: ['/pages/timeline'],
          },
          {
            label: 'Not Found',
            icon: 'pi pi-fw pi-exclamation-circle',
            routerLink: ['/notfound'],
          },
          {
            label: 'Empty',
            icon: 'pi pi-fw pi-circle-off',
            routerLink: ['/pages/empty'],
          },
        ],
      },
      {
        label: 'Hierarchy',
        items: [
          {
            label: 'Submenu 1',
            icon: 'pi pi-fw pi-bookmark',
            items: [
              {
                label: 'Submenu 1.1',
                icon: 'pi pi-fw pi-bookmark',
                items: [
                  { label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-bookmark' },
                  { label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-bookmark' },
                  { label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-bookmark' },
                ],
              },
              {
                label: 'Submenu 1.2',
                icon: 'pi pi-fw pi-bookmark',
                items: [
                  { label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-bookmark' },
                ],
              },
            ],
          },
          {
            label: 'Submenu 2',
            icon: 'pi pi-fw pi-bookmark',
            items: [
              {
                label: 'Submenu 2.1',
                icon: 'pi pi-fw pi-bookmark',
                items: [
                  { label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-bookmark' },
                  { label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-bookmark' },
                ],
              },
              {
                label: 'Submenu 2.2',
                icon: 'pi pi-fw pi-bookmark',
                items: [
                  { label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-bookmark' },
                ],
              },
            ],
          },
        ],
      },
      {
        label: 'Get Started',
        items: [
          {
            label: 'Documentation',
            icon: 'pi pi-fw pi-question',
            routerLink: ['/documentation'],
          },
          {
            label: 'View Source',
            icon: 'pi pi-fw pi-search',
            url: ['https://github.com/primefaces/sakai-ng'],
            target: '_blank',
          },
        ],
      },
    ];


    var user = this.tokenService.getUser();
    if (user == null) {
      this.router.navigate([LOGIN_URL]);
    }

    const permissions = user.permissions;

    // this.model.forEach(menu => {
    //   menu.items = menu.items.filter(item => this.hasPermission(permissions, item.attributes?.policyName));
    // });

    // Duyệt qua các mục menu cha
    for (let index = 0; index < this.model.length; index++) {
      // Duyệt qua các mục con (nếu có)
      for (let childIndex = 0; childIndex < this.model[index].items?.length; childIndex++) {
        const menuItem = this.model[index].items[childIndex];

        if (menuItem.attributes && menuItem.attributes.policyName) {
          // Kiểm tra xem quyền của menu này có trong danh sách quyền của người dùng không
          if (!permissions.includes(menuItem.attributes.policyName)) {
            // Nếu quyền không có trong danh sách, ẩn menu bằng cách thiết lập class
            menuItem.class = 'hidden';
          }
        }
      }
    }

  }

  private hasPermission(permissions: string[], policyName: string): boolean {
    return permissions.includes(policyName);
  }
}
