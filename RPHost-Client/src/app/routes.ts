import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ResearchesComponent } from './researches/researches.component';
import { AuthService } from './_services/auth.service';
import { AuthGuard } from './_guards/auth.guard';
import { ConnectionComponent } from './connection/connection-list/connection.component';
import { ConnectionProfileComponent } from './connection/connection-profile/connection-profile.component';
import { ConnectionDetailResolver } from './_resolvers/connection-detail.resolver';
import { ConnectionListResolver } from './_resolvers/connection-list.resolver';
import { ProfileEditComponent } from './connection/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MessagesComponent } from './messages/messages.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path : 'researches', component: ResearchesComponent},
            { path : 'connections', component: ConnectionComponent,
                        resolve: {users: ConnectionListResolver}},
            { path : 'connections/:id', component: ConnectionProfileComponent,
                        resolve: {user: ConnectionDetailResolver}},
            { path: 'profile/edit', component: ProfileEditComponent,
                        resolve: {user: ProfileEditResolver}},
            { path: 'messages', component: MessagesComponent, 
                        resolve: {messages: MessagesResolver}}
        ]
    },
    { path : '**', redirectTo: 'home', pathMatch: 'full'},

    // { path : '', component: HomeComponent},
    // { path : 'researches', component: ResearchesComponent, canActivate: [AuthGuard] },
    // { path : 'connections', component: ConnectionComponent, canActivate: [AuthGuard] },
    // { path : 'connections/:id', component: ConnectionProfileComponent, canActivate: [AuthGuard] },
    // { path : '**', redirectTo: '', pathMatch: 'full'}
];
