import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryOptions, NgxGalleryImage } from '@kolkov/ngx-gallery';
import { strings as polishStrings } from 'ngx-timeago/language-strings/pl';
import { TimeagoIntl } from 'ngx-timeago';
import { TabsetComponent } from 'ngx-bootstrap/tabs/ngx-bootstrap-tabs';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, private alertify: AlertifyService,
              private route: ActivatedRoute, intl: TimeagoIntl) {
                intl.strings = polishStrings;
                intl.changes.next();
              }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });

    this.route.queryParams.subscribe(params => {
      const zaznaczonaTab = params['tab'];
      this.memberTabs.tabs[zaznaczonaTab > 0 ? zaznaczonaTab : 0].active = true;
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }

  getImages() {
    const zdjeciaUrls = [];
    for (const zdjecie of this.user.zdjecia) {
      zdjeciaUrls.push({
        small: zdjecie.url,
        medium: zdjecie.url,
        big: zdjecie.url,
        description: zdjecie.opis
      });
    }
    return zdjeciaUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  // loadUser() {
  //  this.userService.getUser(+this.route.snapshot.params['id']).subscribe((user: User) => {
  //    this.user = user;
  //  }, error => {
  //    this.alertify.error(error);
  //  });
}


