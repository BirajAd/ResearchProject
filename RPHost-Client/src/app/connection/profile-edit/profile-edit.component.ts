import { Component, OnInit, ViewChild, NgModule, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { NgForm } from '@angular/forms';
import { ModalModule, BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})

export class ProfileEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  canEdit = false;
  modalRef: BsModalRef;
  profilePhoto: File = null;
  baseUrl = environment.apiUrl;
  mainPhoto = "";

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private http: HttpClient,
              private userService: UserService, private authService: AuthService, private modalService: BsModalService) { }


  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template, { backdrop: 'static', keyboard: false });
  }

  onFileSelected(event) {
    this.profilePhoto = <File>event.target.files[0];
  }

  onUpload() {
    const fd = new FormData();
    fd.append('File', this.profilePhoto, this.profilePhoto.name);
    //this.http.post(url to upload to)
    this.http.post(this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos', fd)
        .subscribe(res => {
            this.authService.changeUserPhoto(res['path']); //changes profile as soon as the upload button is clicked
            this.modalRef.hide();
            window.location.reload();
        });
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      this.authService.changeUserPhoto(this.user.photoPath); //changes profile in edit page and others until (other) web pages are refreshed
      localStorage.setItem('profile', this.user.photoPath);
    });
  }

  makeEditable() {
    this.canEdit = !this.canEdit;
  }

  updateUser() {
      this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
        this.alertify.success('Profile updated Successfully');
        this.editForm.reset(this.user);
        //this.makeEditable();
      });
  }

}
