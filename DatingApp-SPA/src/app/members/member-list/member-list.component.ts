import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Paginacja, PaginacjaResultat } from 'src/app/_models/paginacja';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  listaPlec = [{value: 'mezczyzna', display: 'Mężczyzn'}, {value: 'kobieta', display: 'Kobiety'}];
  userParametry: any = {};
  paginacja: Paginacja;

  constructor(private userService: UserService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].resultat;
      this.paginacja = data['users'].paginacja;
    });

    this.userParametry.plec = this.user.plec === 'mezczyzna' ? 'kobieta' : 'mezczyzna';
    this.userParametry.minWiek = 18;
    this.userParametry.maxWiek = 99;
    this.userParametry.ostatnioByl = 'ostatnioAktywny';
  }

  pageChanged(event: any): void {
    this.paginacja.domyslnaStrona = event.page;
    this.loadUsers();
  }

  resetFiltrow() {
    this.userParametry.plec = this.user.plec === 'mezczyzna' ? 'kobieta' : 'mezczyzna';
    this.userParametry.minWiek = 18;
    this.userParametry.maxWiek = 99;
    this.loadUsers();
  }

 loadUsers() {
   this.userService.getUsers(this.paginacja.domyslnaStrona, this.paginacja.itemsNaStrone, this.userParametry)
    .subscribe((res: PaginacjaResultat<User[]>) => {
     this.users = res.resultat;
     this.paginacja = res.paginacja;
   }, error => {
     this.alertify.error(error);
   });
 }

}
