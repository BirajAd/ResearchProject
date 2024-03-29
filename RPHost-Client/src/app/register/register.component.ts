import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../_models/user';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;
  hide: true;
  constructor(private authService: AuthService, private router: Router,
    private alertify: AlertifyService, private fb: FormBuilder) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    },
    this.createRegisterForm();

  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender:['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64),
         Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { 'mismatch'  :  true};
  }

 

  register() {
    if (this.registerForm.valid){
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success("Registration successful");
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/members']);
        });
      });
    }

  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('It cancelled');
  }

}

//   constructor(private authService: AuthService, private alertify: AlertifyService) { }

//   ngOnInit() {
//   }

//   register() {
//       this.authService.register(this.model).subscribe(() => {
//         this.alertify.success('Registration successful');
//       }, error => {
//         this.alertify.error(error);
//       });
//   }

//   cancel() {
//     this.cancelRegister.emit(false);
//     console.log('It cancelled');
//   }

// }
