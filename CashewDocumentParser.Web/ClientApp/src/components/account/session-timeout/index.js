import React, { useState } from "react";

import { useHistory } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { TextField, Button, FormControlLabel, Checkbox } from '@material-ui/core';
import { CheckBoxIcon } from '@material-ui/icons';
import Alert from '@material-ui/lab/Alert'

import API from '../../../API'

const useStyles = makeStyles((theme) => ({
  sessionTimeoutForm: {
    width: "320px",
    margin: "60px auto 20px auto"
  },
  errorMesssageDiv: {
    marginBottom: "20px"
  },
}))

const SessionTimeout = () => {

  const history = useHistory()

  const classes = useStyles();
  const [requestSent, setRequestSent] = useState(false)

  const [errorMesssage, setErrorMessage] = useState("Session time out. Maybe it is too long for your previous login. Please login again.")

  const loginBtnClickHandler = async () => {
    history.push({ pathname: "/account/signin" })
  }

  return (
    <div className={clsx("form", classes.sessionTimeoutForm)}>
      <div className="formHeader">Welcome to KYOCERA Form Xtractor</div>
      <div className="formDiv">

      </div>
      {errorMesssage &&
        <div className={classes.errorMesssageDiv}>
          <Alert severity="error">{errorMesssage}</Alert>
        </div>
      }
      <div className="formActionDiv">
        <Button className="btn"
          onClick={loginBtnClickHandler}
          disabled={requestSent}>LOGIN</Button>
      </div>
    </div>
  )
}

export default SessionTimeout;