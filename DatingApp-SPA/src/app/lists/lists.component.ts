import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { Paginacja, PaginacjaResultat } from '../_models/paginacja';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  users: User[];
  paginacja: Paginacja;
  lubieParametry: string;

  constructor(private authService: AuthService, private userService: UserService,
              private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].resultat;
      this.paginacja = data['users'].paginacja;
    });
    this.lubieParametry = 'Lubisz';
  }

  loadUsers() {
    this.userService.getUsers(this.paginacja.domyslnaStrona, this.paginacja.itemsNaStrone, null, this.lubieParametry)
     .subscribe((res: PaginacjaResultat<User[]>) => {
      this.users = res.resultat;
      this.paginacja = res.paginacja;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.paginacja.domyslnaStrona = event.page;
    this.loadUsers();
  }

}
