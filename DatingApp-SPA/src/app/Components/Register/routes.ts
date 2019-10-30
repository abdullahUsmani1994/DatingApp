import { Routes } from '@angular/router';
import { HomeComponent } from '../Home/Home.component';
import { MemberComponent } from '../member/member.component';
import { MessagesComponent } from '../messages/messages.component';
import { ListComponent } from '../list/list.component';
import { AuthGuard } from 'src/app/_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'member', component: MemberComponent },
            { path: 'message', component: MessagesComponent },
            { path: 'lists', component: ListComponent },
        ]
    },

    { path: '**', redirectTo: '', pathMatch: 'full' },
];
