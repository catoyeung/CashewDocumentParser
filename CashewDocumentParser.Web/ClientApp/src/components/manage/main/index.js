import React, { useState, useEffect } from "react";

import { withRouter, NavLink } from "react-router-dom";

import { AppContext } from "../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { Button } from '@material-ui/core'

import getAPI from '../../../API'

const useStyles = makeStyles((theme) => ({
  workBench: {
    width: "100%",
    padding: "15px",
    display: "flex",
    flexWrap: "wrap",
    justifyContent: "space-between",
    "&::after": {
      content: '""',
      flex: "auto"
    }
  },
  newExtractorDiv: {
    fontSize: "21px",
    textAlign: "center",
    fontWeight: 400,
    minHeight: "130px",
    height: "130px",
    background: "transparent",
    color: "#828282",
    textTransform: "uppercase",
    marginBottom: "15px",
    width: "20%",
    padding: "5px",
    "&:hover": {
      color: "#000"
    }
  },
  newInnerExtractorDiv: {
    border: "1px dashed #e31a2f",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  extractorDiv: {
    position: "relative",
    fontSize: "21px",
    textAlign: "center",
    fontWeight: 400,
    minHeight: "130px",
    height: "130px",
    background: "transparent",
    color: "#828282",
    textTransform: "uppercase",
    marginBottom: "15px",
    width: "20%",
    padding: "5px",
    "&:hover $actionDiv": {
      display: "block"
    }
  },
  innerExtractorDiv: {
    position: "relative",
    border: "1px solid #e31a2f",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  extractorLink: {
    padding: "10px",
    width: "100%",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  actionDiv: {
    position: "absolute",
    top: 0,
    right: 0,
    height: "100%",
    width: "auto",
    padding: "10px",
    display: "none"
  },
  actionBtn: {
    "&:hover": {
      cursor: "pointer"
    }
  }
}));

const ManageMain = (props) => {

  const API = getAPI(props.history)

  const context = React.useContext(AppContext)

  const [extractors, setExtractors] = useState([])

  const classes = useStyles()

  const getTemplates = () => {
    API.get("templates").then((res) => {
      setExtractors(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const copyBtnClickHandler = (id) => {
  }

  const trashBtnClickHandler = (id) => {
    API.delete("templates/" + id).then((res) => {
      getTemplates()
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  useEffect(() => {
    getTemplates()
  }, []);

  return (
    <div className={classes.workBench}>
      <NavLink className={classes.newExtractorDiv} to="/manage/extractor/create">
        <div className={classes.newInnerExtractorDiv}><i className="fa fa-plus-circle" aria-hidden="true"></i> New Extractor</div>
      </NavLink>
      {extractors.map(extractor => {
        return (
          <div className={classes.extractorDiv} key={extractor.id}>
            <div className={classes.innerExtractorDiv}>
              <NavLink className={classes.extractorLink} to={"/manage/extractor/modify/" + extractor.id}>
                <p>{extractor.name}</p>
              </NavLink>
            </div>
            <div className={classes.actionDiv}>
              <ul>
                <li><i className={clsx(" fa fa-copy", classes.actionBtn)} onClick={() => copyBtnClickHandler(extractor.id)}></i></li>
                <li><i className={clsx(" fa fa-trash", classes.actionBtn)} aria-hidden="true" onClick={() => trashBtnClickHandler(extractor.id)}></i></li>
              </ul>
            </div>
          </div>
        )
      })}
    </div>
  )
}

export default withRouter(ManageMain)