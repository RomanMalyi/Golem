import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  Validators,
  FormGroupDirective,
  NgForm,
  FormGroup,
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { SpinnerService } from 'src/app/services/spinner.service';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginUserModel } from 'src/app/models/loginUserModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  passwordFormControl = new FormControl('', [Validators.required]);

  matcher = new MyErrorStateMatcher();

  constructor(
    private router: Router,
    private authService: AuthService,
    private spinnerService: SpinnerService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  public Login() {
    if (this.emailFormControl.valid && this.passwordFormControl.valid) {
      this.spinnerService.showSpinner();
      const model: LoginUserModel = {
        email: this.emailFormControl.value,
        password: this.passwordFormControl.value,
      };
      this.authService.login(model).subscribe(
        (result) => {
          this.spinnerService.hideSpinner();
          this.authService.setToken(result);
          this.router.navigateByUrl('analytics');
        },
        (errorResult) => {
          this.spinnerService.hideSpinner();
          this.openSnackBar('Couldn\'t sign in. Please check your password.', 'Close');
        }
      );
    }
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000,
    });
  }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return !!(
      control &&
      control.invalid &&
      (control.dirty || control.touched || isSubmitted)
    );
  }
}
