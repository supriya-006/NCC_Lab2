import { useState } from 'react'
import './App.css'

function App() {
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    password: '',
    confirmPassword: '',
  })

  const [touched, setTouched] = useState({})
  const [submitted, setSubmitted] = useState(false)
  const [success, setSuccess] = useState(false)

  const handleChange = (event) => {
    const { name, value } = event.target
    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleBlur = (event) => {
    const { name } = event.target
    setTouched((prev) => ({ ...prev, [name]: true }))
  }

  const validate = () => {
    const errors = {}

    if (!formData.fullName.trim()) errors.fullName = 'Full name is required.'
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) errors.email = 'Enter a valid email address.'
    if (formData.password.length < 6) errors.password = 'Password must be at least 6 characters.'
    if (formData.confirmPassword !== formData.password || !formData.confirmPassword) {
      errors.confirmPassword = 'Passwords must match.'
    }

    return errors
  }

  const errors = validate()

  const submitForm = (event) => {
    event.preventDefault()
    setSubmitted(true)
    setTouched({
      fullName: true,
      email: true,
      password: true,
      confirmPassword: true,
    })

    if (Object.keys(errors).length === 0) {
      setSuccess(true)
      setFormData({ fullName: '', email: '', password: '', confirmPassword: '' })
      setTouched({})
      setSubmitted(false)
    } else {
      setSuccess(false)
    }
  }

  return (
    <div className="page-shell">
      <div className="card">
        <div className="card-header">
          <h2>Registration Form</h2>
          <p>Supriya | Roll No: 17</p>
        </div>

        <form onSubmit={submitForm} noValidate>
          <div className="field-row">
            <label htmlFor="fullName">Full Name</label>
            <input
              id="fullName"
              name="fullName"
              type="text"
              value={formData.fullName}
              onChange={handleChange}
              onBlur={handleBlur}
              className={(touched.fullName || submitted) && errors.fullName ? 'invalid' : ''}
            />
            {(touched.fullName || submitted) && errors.fullName && <small className="error">{errors.fullName}</small>}
          </div>

          <div className="field-row">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              name="email"
              type="email"
              value={formData.email}
              onChange={handleChange}
              onBlur={handleBlur}
              className={(touched.email || submitted) && errors.email ? 'invalid' : ''}
            />
            {(touched.email || submitted) && errors.email && <small className="error">{errors.email}</small>}
          </div>

          <div className="field-row">
            <label htmlFor="password">Password</label>
            <input
              id="password"
              name="password"
              type="password"
              value={formData.password}
              onChange={handleChange}
              onBlur={handleBlur}
              className={(touched.password || submitted) && errors.password ? 'invalid' : ''}
            />
            {(touched.password || submitted) && errors.password && <small className="error">{errors.password}</small>}
          </div>

          <div className="field-row">
            <label htmlFor="confirmPassword">Confirm Password</label>
            <input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              value={formData.confirmPassword}
              onChange={handleChange}
              onBlur={handleBlur}
              className={(touched.confirmPassword || submitted) && errors.confirmPassword ? 'invalid' : ''}
            />
            {(touched.confirmPassword || submitted) && errors.confirmPassword && <small className="error">{errors.confirmPassword}</small>}
          </div>

          <div className="button-row">
            <button type="reset" className="secondary" onClick={() => { setTouched({}); setSubmitted(false); setSuccess(false); }}>Reset</button>
            <button type="submit" className="primary">Register</button>
          </div>
        </form>

        {success && <div className="success">Registration successful! Your account has been created.</div>}
      </div>
    </div>
  )
}

export default App
