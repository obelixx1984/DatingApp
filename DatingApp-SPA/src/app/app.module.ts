import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, Injectable } from '@angular/core';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';

import { MessagesComponent } from './messages/messages.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { OchronaPrzedzapisemZmian } from './_guards/ochrona-przedzapisem-zmian.guard';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { plLocale } from 'ngx-bootstrap/locale';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { TimeagoModule, TimeagoIntl, TimeagoFormatter, TimeagoCustomFormatter } from 'ngx-timeago';

defineLocale('pl', plLocale);
registerLocaleData(localePl);

export function tokenGetter() {
   return localStorage.getItem('token');
 }

@Injectable()
export class MyIntl extends TimeagoIntl {
   // do extra stuff here...
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent
   ],
   imports: [
      BrowserModule,
      TimeagoModule.forRoot({
         intl: { provide: TimeagoIntl, useClass: MyIntl},
         formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter }}),
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      PaginationModule.forRoot(),
      ReactiveFormsModule,
      TabsModule.forRoot(),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      ButtonsModule.forRoot(),
      BsDatepickerModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
           tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      UserService,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditResolver,
      {provide: LOCALE_ID, useValue: 'pl'},
      OchronaPrzedzapisemZmian
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {}
