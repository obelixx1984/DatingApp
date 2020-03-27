import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { OchronaPrzedzapisemZmian } from './_guards/ochrona-przedzapisem-zmian.guard';
import { ListsResolver } from './_resolvers/lists.resolver';
import { WiadomosciResolver } from './_resolvers/wiadomosci.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'uzytkownicy', component: MemberListComponent,
                resolve: {users: MemberListResolver}},
            { path: 'uzytkownicy/:id', component: MemberDetailComponent,
                resolve: {user: MemberDetailResolver}},
            { path: 'uzytkownik/edytuj', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [OchronaPrzedzapisemZmian]},
            { path: 'wiadomosci', component: MessagesComponent, resolve: {wiadomosci: WiadomosciResolver}},
            { path: 'listy', component: ListsComponent, resolve: {users: ListsResolver}},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
];
