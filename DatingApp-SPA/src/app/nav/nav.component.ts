import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  zdjecieUrl: string;

  constructor(public authService: AuthService, private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
    this.authService.dowolneZdjecieUrl.subscribe(zdjecieUrl => this.zdjecieUrl = zdjecieUrl);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
     this.alertify.success('Zalogowano pomyślnie');
    }, error => {
      this.alertify.error('Błędne dane logowania!');
    }, () => {
      this.router.navigate(['/uzytkownicy']);
    });
  }

  loggedIn() {
   return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Wylogowano');
    this.router.navigate(['']);
  }

}
