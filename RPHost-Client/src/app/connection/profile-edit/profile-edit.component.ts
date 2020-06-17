import { Component, OnInit, ViewChild, NgModule, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { NgForm } from '@angular/forms';
import { ModalModule, BsModalRef, BsModalService } from 'ngx-bootstrap';


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

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private userService: UserService, private authService: AuthService, private modalService: BsModalService) { }


  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template,{ backdrop: 'static', keyboard: false });
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  makeEditable() {
    this.canEdit = !this.canEdit;
  }

  updateUser() {
      this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
        this.alertify.success('Profile updated Successfully');
        this.editForm.reset(this.user);
        this.makeEditable();
      });
  }

}
