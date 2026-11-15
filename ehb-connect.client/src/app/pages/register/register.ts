
import { Component } from '@angular/core';
import {
  FormBuilder,
  Validators
} from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  submitted = false;
  message = '';

  form;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.nonNullable.group({
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(8)
      ]],
      confirmPassword: ['', [
        Validators.required
      ]]
    });
  }

  get passwordsDifferent(): boolean {
    return (
      this.form.controls.password.value !==
      this.form.controls.confirmPassword.value
    );
  }

  onSubmit(): void {
    this.submitted = true;
    this.message = '';

    if (this.form.invalid || this.passwordsDifferent) {
      return;
    }

    // TODO US-01:
    // Registreer gebruiker via ASP.NET Core API.
    // De backend bestaat hiervoor nog niet.

    this.message =
      'Het formulier is geldig. Registratie wordt ' +
      'beschikbaar zodra de backend gekoppeld is.';
  }
}

