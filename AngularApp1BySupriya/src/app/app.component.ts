import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="page-shell">
      <div class="card">
        <div class="card-header">
          <h2>Registration Form</h2>
          <p>Supriya | Roll No: 17</p>
        </div>

        <form #userForm="ngForm" (ngSubmit)="submitForm(userForm)" novalidate>
          <div class="field-row">
            <label for="fullName">Full Name</label>
            <input
              id="fullName"
              name="fullName"
              type="text"
              [(ngModel)]="model.fullName"
              required
              #fullName="ngModel"
              [class.invalid]="fullName.invalid && (fullName.dirty || fullName.touched || submitted)"
            />
            <small class="error" *ngIf="fullName.invalid && (fullName.dirty || fullName.touched || submitted)">
              Full name is required.
            </small>
          </div>

          <div class="field-row">
            <label for="email">Email</label>
            <input
              id="email"
              name="email"
              type="email"
              [(ngModel)]="model.email"
              required
              email
              #email="ngModel"
              [class.invalid]="email.invalid && (email.dirty || email.touched || submitted)"
            />
            <small class="error" *ngIf="email.invalid && (email.dirty || email.touched || submitted)">
              Enter a valid email address.
            </small>
          </div>

          <div class="field-row">
            <label for="password">Password</label>
            <input
              id="password"
              name="password"
              type="password"
              [(ngModel)]="model.password"
              required
              minlength="6"
              #password="ngModel"
              [class.invalid]="password.invalid && (password.dirty || password.touched || submitted)"
            />
            <small class="error" *ngIf="password.invalid && (password.dirty || password.touched || submitted)">
              Password must be at least 6 characters.
            </small>
          </div>

          <div class="field-row">
            <label for="confirmPassword">Confirm Password</label>
            <input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              [(ngModel)]="model.confirmPassword"
              required
              #confirmPassword="ngModel"
              [class.invalid]="(confirmPassword.invalid || model.password !== model.confirmPassword) && (confirmPassword.dirty || confirmPassword.touched || submitted)"
            />
            <small class="error" *ngIf="(confirmPassword.invalid || model.password !== model.confirmPassword) && (confirmPassword.dirty || confirmPassword.touched || submitted)">
              Passwords must match.
            </small>
          </div>

          <div class="button-row">
            <button type="reset" class="secondary">Reset</button>
            <button type="submit" class="primary">Register</button>
          </div>
        </form>

        <div class="success" *ngIf="successMessage">
          Registration successful! Your account has been created.
        </div>
      </div>
    </div>
  `,
  styles: [
    `
      :host {
        display: block;
        font-family: Arial, sans-serif;
        background: linear-gradient(135deg, #eef2ff, #f8fafc);
        min-height: 100vh;
        padding: 40px 20px;
      }

      .page-shell {
        max-width: 720px;
        margin: 0 auto;
      }

      .card {
        background: white;
        border-radius: 18px;
        box-shadow: 0 20px 45px rgba(15, 23, 42, 0.12);
        overflow: hidden;
      }

      .card-header {
        background: #0f172a;
        color: white;
        padding: 24px 28px;
      }

      .card-header h2 {
        margin: 0 0 6px;
        font-size: 2rem;
      }

      .card-header p {
        margin: 0;
        opacity: 0.8;
      }

      form {
        padding: 28px;
      }

      .field-row {
        display: flex;
        flex-direction: column;
        margin-bottom: 18px;
      }

      label {
        font-weight: 600;
        margin-bottom: 8px;
      }

      input {
        border: 1px solid #cbd5e1;
        border-radius: 10px;
        padding: 12px 14px;
        font-size: 1rem;
        transition: border-color 0.2s ease, box-shadow 0.2s ease;
      }

      input:focus {
        outline: none;
        border-color: #3b82f6;
        box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.15);
      }

      .invalid {
        border-color: #dc2626;
      }

      .error {
        margin-top: 6px;
        color: #dc2626;
        font-size: 0.82rem;
      }

      .button-row {
        display: flex;
        justify-content: flex-end;
        gap: 12px;
        margin-top: 12px;
      }

      button {
        border: none;
        border-radius: 10px;
        padding: 12px 18px;
        font-weight: 700;
        cursor: pointer;
      }

      .primary {
        background: #2563eb;
        color: white;
      }

      .secondary {
        background: #e2e8f0;
        color: #0f172a;
      }

      .success {
        margin: 0 28px 28px;
        background: #dcfce7;
        color: #166534;
        border: 1px solid #86efac;
        padding: 12px 16px;
        border-radius: 10px;
        font-weight: 600;
      }
    `
  ]
})
export class AppComponent {
  model = {
    fullName: '',
    email: '',
    password: '',
    confirmPassword: ''
  };

  submitted = false;
  successMessage = false;

  submitForm(form: any) {
    this.submitted = true;
    if (form.valid && this.model.password === this.model.confirmPassword) {
      this.successMessage = true;
      form.resetForm();
      this.model = {
        fullName: '',
        email: '',
        password: '',
        confirmPassword: ''
      };
    } else {
      this.successMessage = false;
    }
  }
}
