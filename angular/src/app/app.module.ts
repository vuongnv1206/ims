import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutModule } from './layout/app.layout.module';
import { API_BASE_URL, AuthClient } from './api/api-generate';
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
  ],
  providers:
    [{ provide: API_BASE_URL, useValue: environment.API_URL },
      TokenService,
      AuthClient,
      DialogService,
      MessageService,
      NotificationService,
      ConfirmationService
    ],
  bootstrap: [AppComponent]
})
  
export class AppModule {}
