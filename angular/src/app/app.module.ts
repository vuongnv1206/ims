import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutModule } from './layout/app.layout.module';
import { API_BASE_URL, AuthClient, RoleClient, UserClient } from './api/api-generate';
import { environment } from 'src/environments/environment';
import { TokenService } from './shared/services/token.service';
import { HttpClientModule } from '@angular/common/http';
import { DialogService } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { NotificationService } from './shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilityService } from './shared/services/utility.service';
import { ImageModule } from 'primeng/image';
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppLayoutModule,
    HttpClientModule,
    ConfirmDialogModule,
    ToastModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    ImageModule
  ],
  providers:
    [{ provide: API_BASE_URL, useValue: environment.API_URL },
      TokenService,
      AuthClient,
      UserClient,
      RoleClient,
      DialogService,
      MessageService,
      NotificationService,
      ConfirmationService,
      UtilityService
    ],
  bootstrap: [AppComponent]
})

export class AppModule {}
