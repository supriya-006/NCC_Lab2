import { useState } from 'react'
import './App.css'

function App() {
  const [view, setView] = useState('home')
  const [firstNumber, setFirstNumber] = useState(0)
  const [secondNumber, setSecondNumber] = useState(0)
  const [operation, setOperation] = useState('add')
  const [resultText, setResultText] = useState('0')

  const computeResult = () => {
    const a = Number(firstNumber)
    const b = Number(secondNumber)

    if (operation === 'add') setResultText(String(a + b))
    if (operation === 'subtract') setResultText(String(a - b))
    if (operation === 'multiply') setResultText(String(a * b))
  }

  return (
    <div className="app-shell">
      <nav className="navbar">
        <div className="brand">ReactAppBySupriya</div>
        <div className="nav-links">
          <button type="button" className={view === 'home' ? 'active' : ''} onClick={() => setView('home')}>Home</button>
          <button type="button" className={view === 'calculator' ? 'active' : ''} onClick={() => setView('calculator')}>Calculator</button>
        </div>
      </nav>

      <main>
        {view === 'home' ? (
          <section className="home-view">
            <img src="https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=800&q=80" alt="Profile" />
            <h1>Supriya</h1>
            <p>Welcome to my React app.</p>
          </section>
        ) : (
          <section className="calculator-view">
            <div className="calculator-card">
              <h2>Calculator</h2>

              <div className="form-grid">
                <label>
                  First Number
                  <input type="number" value={firstNumber} onChange={(e) => setFirstNumber(e.target.value)} />
                </label>

                <label>
                  Second Number
                  <input type="number" value={secondNumber} onChange={(e) => setSecondNumber(e.target.value)} />
                </label>

                <label>
                  Operation
                  <select value={operation} onChange={(e) => setOperation(e.target.value)}>
                    <option value="add">Add</option>
                    <option value="subtract">Subtract</option>
                    <option value="multiply">Multiply</option>
                  </select>
                </label>

                <button type="button" onClick={computeResult}>Compute</button>
              </div>

              <div className="result-box">
                <strong>Result:</strong> {resultText}
              </div>
            </div>
          </section>
        )}
      </main>

      <footer>© Supriya, {new Date().getFullYear()}</footer>
    </div>
  )
}

export default App
