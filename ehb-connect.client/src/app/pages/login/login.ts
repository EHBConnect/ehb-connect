
import { Component } from '@angular/core';
import {
  FormBuilder,
  Validators
} from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  submitted = false;
  message = '';

  form;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.nonNullable.group({
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.submitted = true;
    this.message = '';

    if (this.form.invalid) {
      return;
    }

    // TODO US-02:
    // Verstuur credentials naar de login API.
    // Handel ongeldige credentials en sessies af.

    this.message =
      'Het formulier is geldig. Echt inloggen ' +
      'wordt beschikbaar na de backendkoppeling.';
  }
}

