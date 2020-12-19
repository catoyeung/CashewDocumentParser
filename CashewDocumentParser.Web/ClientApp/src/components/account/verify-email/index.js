import React, { useState } from "react"
import { withRouter, useHistory } from "react-router-dom";

import {  } from 'react-router-dom';
import queryString from 'query-string';

import { makeStyles } from '@material-ui/core/styles'
import clsx from 'clsx'
import { TextField, Button } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert'

import getAPI from '../../../API'

const useStyles = makeStyles((theme) => ({
  verifyEmailForm: {
    width: "480px",
    margin: "60px auto 60px auto"
  },
  verifyMesssageDiv: {
    marginBottom: "20px"
  },
  alreadyRegisteredText: {
    display: "inline-block"
  }
}))

const VerifyEmail = (props) => {

  const history = useHistory()

  const API = getAPI(history)

  const classes = useStyles();

  const [verifyMesssage, setVerifyMessage] = useState("")

  try {
    let paramsInQueryString = queryString.parse(props.location.search)
    let data = {
      Token: paramsInQueryString.token,
      Email: paramsInQueryString.email
    }
    console.log(paramsInQueryString)
    API.post("Account/VerifyEmail", data).then((res) => {
      setVerifyMessage("Verification is successful. Please login.")
    })
  } catch (error) {
    console.log(error)
    if (error.response?.data) {
      setVerifyMessage(error.response.data.errorMessage)
    } else {
      setVerifyMessage("Sorry, something went wrong. Please contact system administrator")
    }
  }

  const loginBtnClickHandler = () => {
    history.push("/account/signin")
  }

  return (
    <div className={clsx("form", classes.verifyEmailForm)}>
      <div className="formHeader">Please wait for email verification here.</div>
      <div className="formDiv">
        {verifyMesssage &&
          <div className={classes.verifyMesssageDiv}>
          <Alert severity="success">{verifyMesssage}</Alert>
          </div>
        }
      </div>
      <div className="formActionDiv">
        <Button className="btn"
          onClick={loginBtnClickHandler}>GO TO LOGIN PAGE</Button>
      </div>
    </div>
  )
}

export default withRouter(VerifyEmail);