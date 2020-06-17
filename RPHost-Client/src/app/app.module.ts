import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule, TabsModule, ModalModule, BsModalRef } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { NgxGalleryModule } from 'ngx-gallery';
import { JwtModule } from '@auth0/angular-jwt';

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
import { MatSidenavModule } from '@angular/material';
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
      HttpClientModule,
      FormsModule,
      AvatarModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
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
