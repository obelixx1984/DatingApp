import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { strings as polishStrings } from 'ngx-timeago/language-strings/pl';
import { TimeagoIntl } from 'ngx-timeago';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  zdjecieUrl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private userService: UserService, private authService: AuthService, intl: TimeagoIntl) {
                intl.strings = polishStrings;
                intl.changes.next();
              }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.authService.dowolneZdjecieUrl.subscribe(zdjecieUrl => this.zdjecieUrl = zdjecieUrl);
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.success('Profil zapisany pomyślnie');
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.error(error);
    });

  }

  aktualizujGlowneZdjecie(zdjecieUrl) {
    this.user.zdjecieUrl = zdjecieUrl;
  }
}
