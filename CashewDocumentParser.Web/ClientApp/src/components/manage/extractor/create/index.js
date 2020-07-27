import React, { useState, useEffect } from "react";

import { withRouter, NavLink, useHistory } from "react-router-dom";

import { AppContext } from "../../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import { TextField, Button, Snackbar } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert';

import API from '../../../../API'

const useStyles = makeStyles((theme) => ({
  workBench: {
    padding: "15px",
  },
  newExtractorFormDiv: {
    border: "1px dashed #e31a2f",
    marginBottom: "15px",
    fontSize: "21px",
    textAlign: "center",
    fontWeight: 400,
    background: "transparent",
    color: "#828282",
    textTransform: "uppercase",
    width: "100%"
  },
  txtExtractorName: {
    marginTop: 0,
    marginBottom: 0,
    marginRight: "15px",
    height: "37px",
    verticalAlign: "baseline"
  }
}));

const ManageCreateExtractor = (props) => {

  const history = useHistory()

  const context = React.useContext(AppContext)

  const classes = useStyles()

  const [extractorName, setExtractorName] = useState("")

  const [requestSent, setRequestSent] = useState(false)

  useEffect(() => {
    
  }, []);

  const txtExtractorNameChangeHandler = (e) => {
    setExtractorName(e.target.value)
  }

  const createBtnClickHandler = async () => {
    try {
      let data = {
        Name: extractorName
      }
      setRequestSent(true)
      await API.post("Template/Create", data, {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      }).then((res) => {
        context.setSuccessMessage("Template is created.")
        history.push({
          pathname: "/manage/extractor/" + res.data.id + "/sample-document/upload",
          state: {
            templateId: res.data.id
          }
        })
      })
    } catch (error) {
      setRequestSent(false)
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    }
  }

  return (
    <div className={classes.workBench}>
      <div className={classes.newExtractorFormDiv}>
        <Button className="btn"
          onClick={createBtnClickHandler}
          disabled={requestSent}
          variant="outlined" >Cancel</Button>
        <TextField
          className={classes.txtExtractorName}
          placeholder="Extractor Name"
          margin="normal"
          value={extractorName}
          onChange={txtExtractorNameChangeHandler}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <Button className="btn"
          onClick={createBtnClickHandler}
          disabled={requestSent}>CREATE</Button>
      </div>
    </div>
  )
}

export default withRouter(withStyles(useStyles)(ManageCreateExtractor))