import { Routes } from '@angular/router';
import { HomeComponent } from '../Home/Home.component';
import { MessagesComponent } from '../messages/messages.component';
import { ListComponent } from '../list/list.component';
import { AuthGuard } from 'src/app/_guards/auth.guard';
import { MemberListComponent } from '../member/member-list/member-list.component';
import { MemberDetailsComponent } from '../member/member-details/member-details.component';
import { MemberDetailResolver } from 'src/app/_resolvers/member-detail.resolver';
import { MemberListResolver } from 'src/app/_resolvers/member-list.resolver';
import { MemberEditComponent } from '../member/member-edit/member-edit.component';
import { MemberEditResolver } from 'src/app/_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from 'src/app/_guards/prevent-unsaved-changes.guards';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent, resolve: { users: MemberListResolver } },
            {
                path: 'members/:id', component: MemberDetailsComponent,
                resolve: { user: MemberDetailResolver }
            },
            { path: 'message', component: MessagesComponent },
            {
                path:'member/edit', component:MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate:[PreventUnsavedChanges]},
            { path: 'lists', component: ListComponent },
        ]
    },

    { path: '**', redirectTo: '', pathMatch: 'full' },
];
