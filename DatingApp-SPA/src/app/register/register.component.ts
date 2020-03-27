import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig, BsLocaleService} from 'ngx-bootstrap/datepicker';
import { User } from '../_models/user';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  user: User;
  formularzRejestracji: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router,
              private fb: FormBuilder, private localeService: BsLocaleService) {
                this.localeService.use('pl');
              }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red',
    },
    this.stworzFormularzRejestracji();
  }

  stworzFormularzRejestracji() {
    this.formularzRejestracji = this.fb.group({
      plec: ['kobieta'],
      username: ['', Validators.required],
      tytul: ['', Validators.required],
      Urodziny: [null, Validators.required],
      miasto: ['', Validators.required],
      kraj: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.sprawdzenieHaslaValidator});
  }

  sprawdzenieHaslaValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }

  rejestruj() {
    if (this.formularzRejestracji.valid) {
      this.user = Object.assign({}, this.formularzRejestracji.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('Rejestracja przebiegla pomyslnie');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/uzytkownicy']);
        });
      });
    }
  }

  anuluj() {
    this.cancelRegister.emit(false);
    console.log('Anulowano');
  }

}
