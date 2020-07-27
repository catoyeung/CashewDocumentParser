import React, { useState } from "react";

import { useHistory } from "react-router-dom";

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

const ForgetPassword = () => {

  const history = useHistory()

  const classes = useStyles();

  const [email, setEmail] = useState("")
  const [requestSent, setRequestSent] = useState(false)

  const [successMesssage, setSuccessMessage] = useState("")
  const [validationMesssage, setValidationMessage] = useState("")

  const txtEmailChangeHandler = (e) => {
    setEmail(e.target.value)
  }

  const validateForm = () => {
    if (email == "") {
      return "Email is required."
    }
    const regexEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    if (!regexEmail.test(String(email).toLowerCase())) {
      return "Email address is not valid."
    }
    return ""
  }

  const forgetPasswordBtnClickHandler = async () => {
    let validationMessage = validateForm()
    if (validationMessage != "") {
      setValidationMessage(validationMessage)
    } else {
      try {
        let data = {
          Email: email,
          ResetPasswordLink: process.env.REACT_APP_BASE_URL + "account/reset-password"
        }
        setRequestSent(true)
        await API.post("Account/ForgetPassword", data, {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
        }).then((res) => {
          setValidationMessage("")
          setSuccessMessage("Reset password email has been sent. Please click the reset password link in your email.")
          setRequestSent(false)
        })
      } catch (error) {
        setSuccessMessage("")
        setRequestSent(false)
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
          onClick={forgetPasswordBtnClickHandler}
          disabled={requestSent}>FORGET PASSWORD</Button>
      </div>
    </div>
  )
}

export default ForgetPassword