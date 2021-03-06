import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from 'src/app/_models/photo';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() idzZmienicZdjecieUzytkownika = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentGlowne: Photo;

  constructor(private authService: AuthService, private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const zdjecie = {
          id: res.id,
          url: res.url,
          dataDodania: res.dataDodania,
          opis: res.opis,
          toMenu: res.toMenu
        };
        this.photos.push(zdjecie);
        if (zdjecie.toMenu) {
          this.authService.zmienZdjecieUzytkownika(zdjecie.url);
          this.authService.currentUser.zdjecieUrl = zdjecie.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
      }
    };
  }

  ustawGlowneZdjecie(photo: Photo) {
    this.userService.ustawGlowneZdjecie(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      this.currentGlowne = this.photos.filter(p => p.toMenu === true)[0];
      this.currentGlowne.toMenu = false;
      photo.toMenu = true;
      this.authService.zmienZdjecieUzytkownika(photo.url);
      this.authService.currentUser.zdjecieUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
    }, error => {
      this.alertify.error(error);
    });
  }

  usunZdjecie(id: number) {
    this.alertify.confirm('Jeste?? pewien, ??e chcesz usun???? zdj??cie?', () => {
      this.userService.usunZdjecie(this.authService.decodedToken.nameid, id).subscribe(() => {
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alertify.success('Zdj??cie zosta??o usuni??te :)');
      }, error => {
        this.alertify.error('Nie uda??o si?? usun???? zdj??cia !!!');
      });
    });
  }
}
