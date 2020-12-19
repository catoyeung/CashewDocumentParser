import React, { useState, useEffect } from "react";

import { withRouter, NavLink, useHistory } from "react-router-dom";

import { AppContext } from "../../../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import Link from '@material-ui/core/Link';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faFileAlt } from '@fortawesome/free-solid-svg-icons'
import { faCodeBranch } from '@fortawesome/free-solid-svg-icons'
import { faDownload } from '@fortawesome/free-solid-svg-icons'
import { faChartBar } from '@fortawesome/free-solid-svg-icons'
import { faCogs } from '@fortawesome/free-solid-svg-icons'

import getAPI from '../../../../../API'

const useStyles = makeStyles((theme) => ({
  extracterConfigSidebar: {
    flexGrow: 1,
    padding: "15px",
    background: "#e31a2f",
    color: "#ffffff",
    "& ul": {
      textAlign: "left",
      "& li": {
        padding: "5px",
        marginBottom: "10px",
        display: "flex",
        alignItems: "center",
        cursor: "pointer"
      }
    }
  },
  link: {
    color: "#fff"
  }
}));

const ExtractorSidebar = (props) => {

  const history = useHistory()

  const API = getAPI(history)

  const context = React.useContext(AppContext)

  const classes = useStyles()

  useEffect(() => {
    
  }, []);

  return (
    <div className={classes.extracterConfigSidebar}>
      <ul>
        <Link href={`/manage/extractor/modify/${props.match.params.id}/document-list`}
          className={classes.link}>
          <li>
            <FontAwesomeIcon icon={faFileAlt} />
            <div className={classes.configTxt}>Documents</div>
          </li>
        </Link>
        <li>
          <FontAwesomeIcon icon={faCodeBranch} />
          <div className={classes.configTxt}>Rules</div>
        </li>
        <li>
          <FontAwesomeIcon icon={faDownload} />
          <div className={classes.configTxt}>Downloads</div>
        </li>
        <li>
          <FontAwesomeIcon icon={faChartBar} />
          <div className={classes.configTxt}>Activity</div>
        </li>
        <li>
          <FontAwesomeIcon icon={faCogs} />
          <div className={classes.configTxt}>Settings</div>
        </li>
      </ul>
    </div>
  )
}

export default withRouter(ExtractorSidebar)