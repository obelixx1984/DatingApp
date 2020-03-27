import { Component, Input, OnInit } from '@angular/core';
import { Wiadomosci } from 'src/app/_models/wiadomosci';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input() odbiorcaId: number;
  wiadomosc: Wiadomosci[];
  nowaWiadomosc: any = {};

  constructor(private userService: UserService,
              private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadWiadomosci();
  }

  loadWiadomosci() {
    const domyslnyUserId = +this.authService.decodedToken.nameid;
    this.userService.getWiadomosciWatek(this.authService.decodedToken.nameid, this.odbiorcaId)
      .pipe(
        tap(wiadomosci => {
          // tslint:disable-next-line: prefer-for-of
          for (let i = 0; i < wiadomosci.length; i++) {
            if (wiadomosci[i].jestCzytana === false && wiadomosci[i].odbiorcaId === domyslnyUserId) {
              this.userService.wiadomoscPrzeczytana(domyslnyUserId, wiadomosci[i].id);
            }
          }
        })
      )
      .subscribe(wiadomosci => {
        this.wiadomosc = wiadomosci;
    }, error => {
      this.alertify.error(error);
    });
  }

  wyslijWiadomosc() {
    this.nowaWiadomosc.odbiorcaId = this.odbiorcaId;
    this.userService.wyslijWiadomosc(this.authService.decodedToken.nameid, this.nowaWiadomosc)
      .subscribe((wiadomosc: Wiadomosci) => {
        this.wiadomosc.unshift(wiadomosc);
        this.nowaWiadomosc.tresc = '';
    }, error => {
      this.alertify.error(error);
    });
  }

}
