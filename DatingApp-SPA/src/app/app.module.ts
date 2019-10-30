import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './Components/Register/routes';

import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interseptor';
import { AlertifyService } from './_services/Alertify.service';
import { BsDropdownModule } from 'ngx-bootstrap';
import { NavComponent } from './Components/nav/nav.component';
import { HomeComponent } from './Components/Home/Home.component';
import { RegisterComponent } from './Components/Register/Register.component';
import { RouterModule } from '@angular/router';
import { ListComponent } from './Components/list/list.component';
import { MemberComponent } from './Components/member/member.component';
import { MessagesComponent } from './Components/messages/messages.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListComponent,
      MemberComponent,
      MessagesComponent,

   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService
   ],
      
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
