import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ResearchesComponent } from './researches/researches.component';
import { AuthService } from './_services/auth.service';
import { AuthGuard } from './_guards/auth.guard';
import { ConnectionComponent } from './connection/connection-list/connection.component';
import { ConnectionProfileComponent } from './connection/connection-profile/connection-profile.component';

export const appRoutes: Routes = [
    { path : '', component: HomeComponent},
    { path : 'researches', component: ResearchesComponent, canActivate: [AuthGuard] },
    { path : 'connections', component: ConnectionComponent, canActivate: [AuthGuard] },
    { path : 'connections/:id', component: ConnectionProfileComponent, canActivate: [AuthGuard] },
    { path : '**', redirectTo: '', pathMatch: 'full'}
];
