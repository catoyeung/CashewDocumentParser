import React, { useState, useEffect } from "react";

import { withRouter, NavLink, useHistory } from "react-router-dom";

import { AppContext } from "../../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import getAPI from '../../../../API'

import ExtractorSidebar from './sidebar'

const useStyles = makeStyles((theme) => ({
  workBench: {
    display: "flex",
    flexGrow: 1,
    padding: 0,
  },
  modifyExtractorDiv: {
    display: "flex",
    flexGrow: 1,
    fontSize: "21px",
    fontWeight: 400,
    background: "transparent",
    textTransform: "uppercase",
    width: "100%"
  },
  configTxt: {
    verticalAlign: "middle"
  }
}));

const ManageModifyExtractor = (props) => {

  const history = useHistory()

  const API = getAPI(history)

  const context = React.useContext(AppContext)

  const classes = useStyles()

  const [extractorName, setExtractorName] = useState("")

  const [requestSent, setRequestSent] = useState(false)

  useEffect(() => {
    
  }, []);

  return (
    <div className={classes.workBench}>
      <div className={classes.modifyExtractorDiv}>
        <Grid container style={{ display: "flex", flexGrow: 1}}>
          <Grid item style={{ display: "flex", flexGrow: 1}} lg={2} xs={12}>
            <ExtractorSidebar />
          </Grid>
          <Grid item lg={10} xs={12}>
            lg=9
          </Grid>
        </Grid>
      </div>
    </div>
  )
}

export default withRouter(ManageModifyExtractor)