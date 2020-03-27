import { Component, OnInit } from '@angular/core';
import { Wiadomosci } from '../_models/wiadomosci';
import { Paginacja, PaginacjaResultat } from '../_models/paginacja';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { strings as polishStrings } from 'ngx-timeago/language-strings/pl';
import { TimeagoIntl } from 'ngx-timeago';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  wiadomosci: Wiadomosci[];
  paginacja: Paginacja;
  naglowekWiadomosci = 'Nieprzeczytane';

  constructor(private userService: UserService, private authService: AuthService,
              private route: ActivatedRoute, private alertify: AlertifyService, intl: TimeagoIntl) {
                intl.strings = polishStrings;
                intl.changes.next();
              }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.wiadomosci = data['wiadomosci'].resultat;
      this.paginacja = data['wiadomosci'].paginacja;
    });
  }

  loadWiadomosci() {
    this.userService.getWiadomosci(this.authService.decodedToken.nameid, this.paginacja.domyslnaStrona,
        this.paginacja.itemsNaStrone, this.naglowekWiadomosci)
        .subscribe((res: PaginacjaResultat<Wiadomosci[]>) => {
          this.wiadomosci = res.resultat;
          this.paginacja = res.paginacja;
        }, error => {
          this.alertify.error(error);
        });
  }

  usunWiadomosc(id: number) {
    this.alertify.confirm('Jesteś pewien, że chcesz usunąć wiadomość?', () => {
      this.userService.usunWiadomosc(id, this.authService.decodedToken.nameid).subscribe(() => {
        this.wiadomosci.splice(this.wiadomosci.findIndex(m => m.id === id), 1);
        this.alertify.success('Wiadomość zostałą usunięta');
      }, error => {
        this.alertify.error('Nie udało się usunąć wiadomości');
      });
    });
  }

  zmienStrone(event: any): void {
    this.paginacja.domyslnaStrona = event.strona;
    this.loadWiadomosci();
  }

}
