import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule, BsModalRef } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { RouterModule } from '@angular/router';
import { NgxGalleryModule } from 'ngx-gallery';
import { JwtModule } from '@auth0/angular-jwt';
import {FlexLayoutModule} from '@angular/flex-layout';
import { PaginationModule } from 'ngx-bootstrap/pagination';



import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ResearchesComponent } from './researches/researches.component';
import { appRoutes } from './routes';
import { ConnectionComponent } from './connection/connection-list/connection.component';
import { ConnectionCardComponent } from './connection/connection-card/connection-card.component';
import { ConnectionProfileComponent } from './connection/connection-profile/connection-profile.component';
import { ProfileEditComponent } from './connection/profile-edit/profile-edit.component';
import { SideNavComponent } from './sidenav/side-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { AvatarModule } from 'ngx-avatar';
import { AlertifyService } from './_services/alertify.service';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { ConnectionDetailResolver } from './_resolvers/connection-detail.resolver';
import { ConnectionListResolver } from './_resolvers/connection-list.resolver';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';



export function tokenGetter(){
   return localStorage.getItem('token');
}

export class CustomHammerConfig extends HammerGestureConfig  {
   overrides = {
       pinch: { enable: false },
       rotate: { enable: false }
   };
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ResearchesComponent,
      ConnectionComponent,
      ProfileEditComponent,
      ConnectionCardComponent,
      ConnectionProfileComponent,
      SideNavComponent,
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      AvatarModule,
      FlexLayoutModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      ModalModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
      LayoutModule,
      MatToolbarModule,
      MatButtonModule,
      MatSidenavModule,
      MatIconModule,
      MatListModule

   ],
   providers: [
      AuthService,
      BsModalRef,
      AlertifyService,
      AuthGuard,
      UserService,
      ConnectionDetailResolver,
      ConnectionListResolver,
      ProfileEditResolver,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }

   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
