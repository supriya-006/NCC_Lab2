import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="app-shell">
      <nav class="navbar">
        <div class="brand">AngularApp2BySupriya</div>
        <div class="nav-links">
          <button type="button" [class.active]="currentView === 'home'" (click)="showHome()">Home</button>
          <button type="button" [class.active]="currentView === 'calculator'" (click)="showCalculator()">Calculator</button>
        </div>
      </nav>

      <main>
        <section *ngIf="currentView === 'home'" class="home-view">
          <img src="https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=800&q=80" alt="Profile photo" />
          <h1>Supriya</h1>
          <p>Welcome to my Angular app.</p>
        </section>

        <section *ngIf="currentView === 'calculator'" class="calculator-view">
          <div class="calculator-card">
            <h2>Calculator</h2>
            <div class="form-grid">
              <label>
                First Number
                <input type="number" [(ngModel)]="firstNumber" name="firstNumber" />
              </label>

              <label>
                Second Number
                <input type="number" [(ngModel)]="secondNumber" name="secondNumber" />
              </label>

              <label>
                Operation
                <select [(ngModel)]="operation" name="operation">
                  <option value="add">Add</option>
                  <option value="subtract">Subtract</option>
                  <option value="multiply">Multiply</option>
                </select>
              </label>

              <button type="button" (click)="computeResult()">Compute</button>
            </div>

            <div class="result-box">
              <strong>Result:</strong> {{ resultText }}
            </div>
          </div>
        </section>
      </main>

      <footer>
        © Supriya, {{ currentYear }}
      </footer>
    </div>
  `,
  styles: [
    `
      :host {
        display: block;
        min-height: 100vh;
        font-family: Arial, sans-serif;
        background: #f8fafc;
      }

      .app-shell {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
      }

      .navbar {
        display: flex;
        align-items: center;
        justify-content: space-between;
        background: #0f172a;
        color: white;
        padding: 16px 28px;
      }

      .brand {
        font-size: 1.4rem;
        font-weight: 700;
      }

      .nav-links {
        display: flex;
        gap: 10px;
      }

      .nav-links button {
        background: transparent;
        border: 1px solid rgba(255,255,255,0.2);
        color: white;
        border-radius: 8px;
        padding: 8px 16px;
        cursor: pointer;
        font-weight: 600;
      }

      .nav-links button.active {
        background: #2563eb;
        border-color: #2563eb;
      }

      main {
        flex: 1;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 32px 16px;
      }

      .home-view {
        text-align: center;
        background: white;
        padding: 32px;
        border-radius: 18px;
        box-shadow: 0 12px 32px rgba(15, 23, 42, 0.08);
      }

      .home-view img {
        width: 220px;
        height: 220px;
        object-fit: cover;
        border-radius: 50%;
        border: 4px solid #dbeafe;
        margin-bottom: 18px;
      }

      .home-view h1 {
        margin: 0 0 8px;
        color: #0f172a;
      }

      .calculator-card {
        background: white;
        width: min(560px, 100%);
        padding: 28px;
        border-radius: 20px;
        box-shadow: 0 15px 35px rgba(15, 23, 42, 0.08);
      }

      .calculator-card h2 {
        margin-top: 0;
        margin-bottom: 20px;
      }

      .form-grid {
        display: grid;
        gap: 16px;
      }

      label {
        display: flex;
        flex-direction: column;
        gap: 8px;
        font-weight: 600;
      }

      input, select, button {
        border-radius: 10px;
        padding: 10px 12px;
        font-size: 1rem;
      }

      input, select {
        border: 1px solid #cbd5e1;
      }

      button {
        border: none;
        background: #2563eb;
        color: white;
        font-weight: 700;
        cursor: pointer;
      }

      .result-box {
        margin-top: 20px;
        padding: 12px 14px;
        background: #eff6ff;
        border: 1px solid #bfdbfe;
        border-radius: 10px;
        color: #1d4ed8;
        font-weight: 600;
      }

      footer {
        background: #e2e8f0;
        text-align: center;
        padding: 16px;
        color: #334155;
        font-weight: 600;
      }
    `
  ]
})
export class AppComponent {
  currentView = 'home';
  currentYear = new Date().getFullYear();
  firstNumber = 0;
  secondNumber = 0;
  operation = 'add';
  resultText = '0';

  showHome() {
    this.currentView = 'home';
  }

  showCalculator() {
    this.currentView = 'calculator';
  }

  computeResult() {
    const a = Number(this.firstNumber);
    const b = Number(this.secondNumber);

    switch (this.operation) {
      case 'add':
        this.resultText = String(a + b);
        break;
      case 'subtract':
        this.resultText = String(a - b);
        break;
      case 'multiply':
        this.resultText = String(a * b);
        break;
      default:
        this.resultText = '0';
    }
  }
}
