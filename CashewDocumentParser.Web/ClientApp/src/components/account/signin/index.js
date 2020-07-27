import React, { useState } from "react";

import { AppContext } from "../../../context/provider"

import { useHistory } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { TextField, Button, FormControlLabel, Checkbox } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert'

import API from '../../../API'

const useStyles = makeStyles((theme) => ({
  loginForm: {
    width: "320px",
    margin: "60px auto 20px auto"
  },
  validationMessageDiv: {
    marginBottom: "20px"
  },
}))

const SignIn = () => {

  const context = React.useContext(AppContext)

  const history = useHistory()

  const classes = useStyles();

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [rememberMe, setRememberMe] = useState(false)
  const [requestSent, setRequestSent] = useState(false)

  const [validationMesssage, setValidationMessage] = useState("")

  const txtEmailChangeHandler = (e) => {
    setEmail(e.target.value)
  }

  const txtPasswordChangeHandler = (e) => {
    setPassword(e.target.value)
  }

  const chkRememberMeChangeHandler = (e) => {
    setRememberMe(!rememberMe)
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
    return ""
  }

  const loginBtnClickHandler = async () => {
    let validationMessage = validateForm()
    if (validationMessage != "") {
      setValidationMessage(validationMessage)
    } else {
      try {
        let data = {
          Email: email,
          Password: password,
          RememberMe: rememberMe
        }
        setRequestSent(true)
        await API.post("Account/SignIn", data, {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
        }).then((res) => {
          context.setIsAuthenticated(true)
          history.push("/")
        })
      } catch (error) {
        setRequestSent(false)
        if (error.response?.data) {
          setValidationMessage(error.response.data.errorMessage)
        } else {
          setValidationMessage("Sorry, something went wrong. Please contact system administrator")
        }
      }
    }
  }

  const registerBtnClickHandler = () => {
    history.push({ pathname: "/account/signup" })
  }

  const forgetPasswordBtnClickHandler = () => {
    history.push({ pathname: "/account/forget-password" })
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
        <FormControlLabel
          control={
            <Checkbox
              checked={rememberMe}
              onChange={chkRememberMeChangeHandler}
              name="RememberMe"
              color="primary"
            />
          }
          label="Remember Me"
        />
      </div>
      {validationMesssage &&
        <div className={classes.validationMessageDiv}>
          <Alert severity="error">{validationMesssage}</Alert>
        </div>
      }
      <div className="formActionDiv">
        <Button className="btn"
          onClick={loginBtnClickHandler}
          disabled={requestSent}>LOGIN</Button>
        <Button className="btn"
          onClick={registerBtnClickHandler}
          disabled={requestSent}>REGISTER</Button>
        <Button className="btn"
          onClick={forgetPasswordBtnClickHandler}
          disabled={requestSent}>FORGET PASSWORD</Button>
      </div>
    </div>
  )
}

export default SignIn;