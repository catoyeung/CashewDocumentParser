import React, { useState } from "react"
import { useHistory } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles'
import clsx from 'clsx'
import { TextField, Button } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert'

import API from '../../../API'

const useStyles = makeStyles((theme) => ({
  registerForm: {
    width: "320px",
    margin: "60px auto 60px auto"
  },
  validationMessageDiv: {
    marginBottom: "20px"
  },
  alreadyRegisteredText: {
    display: "inline-block"
  }
}))

const SignUp = () => {

  const history = useHistory()

  const classes = useStyles();

  const [firstName, setFirstName] = useState("")
  const [lastName, setLastName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [requestSent, setRequestSent] = useState(false)

  const [validationMesssage, setValidationMessage] = useState("")

  const txtFirstNameChangeHandler = (e) => {
    setFirstName(e.target.value)
  }

  const txtLastNameChangeHandler = (e) => {
    setLastName(e.target.value)
  }

  const txtEmailChangeHandler = (e) => {
    setEmail(e.target.value)
  }

  const txtPasswordChangeHandler = (e) => {
    setPassword(e.target.value)
  }

  const txtConfirmPasswordChangeHandler = (e) => {
    setConfirmPassword(e.target.value)
  }

  const validateForm = () => {
    if (firstName == "") {
      return "First Name is required."
    }
    if (lastName == "") {
      return "Last Name is required."
    }
    if (email == "") {
      return "Email is required."
    }
    const regexEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    if (!regexEmail.test(String(email).toLowerCase())) {
      return "Email address is not valid."
    }
    if (password == "") {
      return "Password is required."
    }
    if (confirmPassword == "") {
      return "Confirm Password is required."
    }
    if (password != confirmPassword) {
      return "Password and Confirm Password do not match."
    }
    return ""
  }

  const registerBtnClickHandler = async () => {
    let validationMessage = validateForm()
    if (validationMessage != "") {
      setValidationMessage(validationMessage)
    } else {
      try {
        let data = {
          FirstName: firstName,
          LastName: lastName,
          Email: email,
          Password: password,
          ConfirmPassword: confirmPassword,
          VerifyEmailLink: process.env.REACT_APP_BASE_URL + "account/verify-email"
        }

        setRequestSent(true)
        let res = await API.post("Account/SignUp", data)

        history.push({
          pathname: "/account/signup-complete",
          state: { user: res.data.user }
        })

      } catch (error) {
        if (error.response?.data) {
          setValidationMessage(error.response.data.errorMessage)
        } else {
          setValidationMessage("Sorry, something went wrong. Please contact system administrator")
        }
      }
    }
  }

  const loginBtnClickHandler = async () => {
    history.push({ pathname: "/account/signin" })
  }

  return (
    <div className={clsx("form", classes.registerForm)}>
      <div className="formHeader">Welcome to KYOCERA Form Xtractor</div>
      <div className="formDiv">
        <div>
          <TextField
            id="txtFirstName"
            className="txtHalfWidth"
            label="First Name"
            placeholder=""
            margin="normal"
            onChange={txtFirstNameChangeHandler}
            value={firstName}
            InputLabelProps={{
              shrink: true,
            }}
          />
          <TextField
            id="txtLastName"
            className="txtHalfWidth"
            label="Last Name"
            placeholder=""
            margin="normal"
            onChange={txtLastNameChangeHandler}
            value={lastName}
            InputLabelProps={{
              shrink: true,
            }}
          />
        </div>
        <TextField
          id="txtEmail"
          label="Email"
          placeholder=""
          fullWidth
          margin="normal"
          onChange={txtEmailChangeHandler}
          value={email}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextField
          id="txtPassword"
          label="Password"
          type="password"
          placeholder=""
          fullWidth
          margin="normal"
          onChange={txtPasswordChangeHandler}
          value={password}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextField
          id="txtConfirmPassword"
          label="Confirm Password"
          type="password"
          placeholder=""
          fullWidth
          margin="normal"
          onChange={txtConfirmPasswordChangeHandler}
          value={confirmPassword}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </div>
      {validationMesssage &&
        <div className={classes.validationMessageDiv}>
          <Alert severity="error">{validationMesssage}</Alert>
        </div>
      }
      <div className="formActionDiv">
        <Button className="btn"
                disabled={requestSent}
                onClick={registerBtnClickHandler}>REGISTER</Button>
        <Button className="btn"
          disabled={requestSent}
          onClick={loginBtnClickHandler}>Already Registered? LOGIN</Button>
      </div>
    </div>
  )
}

export default SignUp;