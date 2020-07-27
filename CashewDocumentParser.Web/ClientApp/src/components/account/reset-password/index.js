import React, { useState } from "react";

import { useHistory, withRouter } from "react-router-dom";
import queryString from 'query-string';

import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { TextField, Button, FormControlLabel, Checkbox } from '@material-ui/core';
import { CheckBoxIcon } from '@material-ui/icons';
import Alert from '@material-ui/lab/Alert'

import API from '../../../API'

const useStyles = makeStyles((theme) => ({
  loginForm: {
    width: "320px",
    margin: "60px auto 20px auto"
  },
  successMessageDiv: {
    marginBottom: "20px"
  },
  validationMessageDiv: {
    marginBottom: "20px"
  },
}))

const ResetPassword = (props) => {

  const history = useHistory()

  const classes = useStyles();

  const urlParams = queryString.parse(props.location.search)

  const [email, setEmail] = useState(urlParams.email)
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [requestSent, setRequestSent] = useState(false)

  const [successMesssage, setSuccessMessage] = useState("")
  const [validationMesssage, setValidationMessage] = useState("")

  const txtEmailChangeHandler = (e) => {
    setEmail(e.target.value)
  }

  const txtPasswordChangeHandler = (e) => {
    setPassword(e.target.value)
  }

  const txtConfirmPasswordChangeHandler = (e) => {
    setConfirmPassword(e.target.value)
  }

  const loginBtnClickHandler = (e) => {
    history.push({ pathname: "/account/signin" })
  }

  const validateForm = () => {
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

  const resetPasswordBtnClickHandler = async () => {
    let validationMessage = validateForm()
    if (validationMessage != "") {
      setValidationMessage(validationMessage)
    } else {
      try {
        let data = {
          Email: email,
          Password: password,
          ConfirmPassword: confirmPassword,
          Token: urlParams.token
        }
        setRequestSent(true)
        await API.post("Account/ResetPassword", data).then((res) => {
          setValidationMessage("")
          setSuccessMessage("Password has been reset. Please login with the new password.")
          setRequestSent(false)
        })
      } catch (error) {
        setSuccessMessage("")
        setRequestSent(false)
        console.error(error)
        if (error.response?.data) {
          setValidationMessage(error.response.data.errorMessage)
        } else {
          setValidationMessage("Sorry, something went wrong. Please contact system administrator")
        }
      }
    }
  }

  return (
    <div className={clsx("form", classes.loginForm)}>
      <div className="formHeader">Welcome to KYOCERA Form Xtractor</div>
      <div className="formDiv">
        <TextField
          id="standard-full-width"
          label="Email"
          placeholder=""
          fullWidth
          margin="normal"
          value={email}
          onChange={txtEmailChangeHandler}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextField
          id="standard-full-width"
          label="Password"
          type="password"
          placeholder=""
          fullWidth
          margin="normal"
          value={password}
          onChange={txtPasswordChangeHandler}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextField
          id="standard-full-width"
          label="Confirm Password"
          type="password"
          placeholder=""
          fullWidth
          margin="normal"
          value={confirmPassword}
          onChange={txtConfirmPasswordChangeHandler}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </div>
      {successMesssage &&
        <div className={classes.successMessageDiv}>
          <Alert severity="success">{successMesssage}</Alert>
        </div>
      }
      {validationMesssage &&
        <div className={classes.validationMessageDiv}>
          <Alert severity="error">{validationMesssage}</Alert>
        </div>
      }
      <div className="formActionDiv">
        <Button className="btn"
          onClick={resetPasswordBtnClickHandler}
          disabled={requestSent}>RESET PASSWORD</Button>
        <Button className="btn"
          onClick={loginBtnClickHandler}
          disabled={requestSent}>LOGIN</Button>
      </div>
    </div>
  )
}

export default withRouter(ResetPassword)