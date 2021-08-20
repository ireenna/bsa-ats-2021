import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { LoginRegistCommonComponent } from '../login-regist-common/login-regist-common.component';
import { ResetPasswordDto } from '../../models/reset-password-dto';
import { finalize, mergeMap } from 'rxjs/operators';
import { AuthenticationService } from '../../services/auth.service';

@Component({
  selector: 'app-reset-password-box',
  templateUrl: './reset-password-box.component.html',
  styleUrls: ['./reset-password-box.component.scss',
    '../login-regist-common/login-regist-common.component.scss'],
})
export class ResetPasswordBoxComponent {

  public constructor(public loginRegistCommonComponent: LoginRegistCommonComponent,
    private notificationService: NotificationService,
    private authService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router) { }

  public isPasswordHide = true;
  public isPasswordConfirmHide = true;
  public isRequestFinished = true;

  public resetPasswordForm: FormGroup = new FormGroup({
    'userPassword': new FormControl('', [
      Validators.required,
      this.loginRegistCommonComponent.minPasswordLenghtValidation,
      this.loginRegistCommonComponent.noWhitespaceValidation,
    ]),
    'userPasswordConfirmation': new FormControl('', [
      Validators.required,
    ]),
  }, { validators: this.loginRegistCommonComponent.passwordsMatch });

  public resetPassword() {
    if (this.resetPasswordForm.valid) {
      this.isRequestFinished = false;
      this.route.queryParams
        .pipe(
          mergeMap(params => {
            const resetPasswordDto: ResetPasswordDto = {
              password: this.resetPasswordForm.get('userPassword')?.value,
              email: params.email,
              token: params.token,
            };
            return this.authService.resetPassword(resetPasswordDto);
          }),
          finalize(() => this.isRequestFinished = true),
        )
        .subscribe(() => {
          this.notificationService.showSuccessMessage('Your password has been changed');
          this.router.navigate(['/login']);
        },
        (error) => {
          if (error.description != null) {
            this.notificationService.showErrorMessage(error.description, 'Something went wrong');
          }
          else {
            this.notificationService.showErrorMessage(error.message, 'Something went wrong');
          }
        });
    }
  }
}
