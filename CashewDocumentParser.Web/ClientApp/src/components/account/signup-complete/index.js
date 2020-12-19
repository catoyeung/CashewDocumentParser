import React from "react"
import { useHistory } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles'
import clsx from 'clsx'
import { Button } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert'
import CheckIcon from '@material-ui/icons/Check'

const useStyles = makeStyles((theme) => ({
  signUpCompleteForm: {
    width: "480px",
    margin: "60px auto 20px auto"
  }
}))

const SignUpComplete = (props) => {

  const history = useHistory()

  const classes = useStyles();

  const userFullName = props.location.state.user.firstName + " " + props.location.state.user.lastName

  const loginBtnClickHandler = (e) => {
    history.push({ pathname: "/account/signin" })
  }

  return (
    <div className={clsx("form", classes.signUpCompleteForm)}>
      <div className="formHeader">Signup successfully</div>
      <div className="formDiv">
        <Alert icon={<CheckIcon fontSize="inherit" />} severity="success">
          Welcome, {userFullName}. <br />
          Your account has been signed up succesfully. Please check your email to confirm.
        </Alert>
      </div>
      <div className="formActionDiv">
        <Button className="btn"
                onClick={loginBtnClickHandler}>LOGIN</Button>
      </div>
    </div>
  )
}

export default SignUpComplete;