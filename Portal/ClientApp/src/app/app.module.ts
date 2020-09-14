//Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabMenuModule } from 'primeng/tabmenu';
import { TableModule } from 'primeng/table';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TooltipModule } from 'primeng/tooltip';

//Components
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';

//Services
import { getBaseUrl } from '../main';
import { MessageService } from 'primeng/api';
import { LoginService } from './services/login/login.service';
import { ClientService } from './services/client/client.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule.withServerTransition(
      {
        appId: 'ng-cli-universal'
      }
    ),
    HttpClientModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TabMenuModule,
    TableModule,
    ScrollPanelModule,
    MessagesModule,
    MessageModule,
    BlockUIModule,
    ProgressSpinnerModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    InputTextareaModule,
    TooltipModule,
  ],
  providers: [
    MessageService,
    LoginService,
    ClientService,
    {
      provide: 'BASE_URL',
      useFactory: getBaseUrl
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
