import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-connection-profile',
  templateUrl: './connection-profile.component.html',
  styleUrls: ['./connection-profile.component.css']
})
export class ConnectionProfileComponent implements OnInit {
  user: User;
  canEdit = false;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];


  constructor(private userService: UserService, private alertify: AlertifyService,
              private route: ActivatedRoute, public authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });

    //test

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
    const imagePaths = [];
    for (const photo of this.user.photos){
      imagePaths.push({
        small: photo.path,
        medium: photo.path,
        big: photo.path,
        description: photo.description
      });
    }
    return imagePaths;
  }

  makeEditable() {
    this.canEdit = !this.canEdit;
  }

  // loadUser() {
  //   this.userService.getUser(this.route.snapshot.params['id']).subscribe((user: User) => {
  //       this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }

}