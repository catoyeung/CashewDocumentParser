import React, { useState, useEffect } from "react";

import { withRouter, NavLink } from "react-router-dom";

import { AppContext } from "../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core'

import API from '../../../API'

const useStyles = makeStyles((theme) => ({
  workBench: {
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
    background: "transparent",
    color: "#828282",
    textTransform: "uppercase",
    marginBottom: "15px",
    width: "20%",
    padding: "5px"
  },
  newInnerExtractorDiv: {
    border: "1px dashed #e31a2f",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  extractorDiv: {
    fontSize: "21px",
    textAlign: "center",
    fontWeight: 400,
    minHeight: "130px",
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
  actionDiv: {
    position: "absolute",
    right: 0,
    height: "100%",
    width: "auto",
    padding: "10px",
    display: "none"
  }
}));

const ManageMain = (props) => {

  const context = React.useContext(AppContext)

  const [extractors, setExtractors] = useState([])

  const classes = useStyles()

  useEffect(() => {
    try {
      const data = {}
      API.get("Template/GetAll", data).then((res) => {
        setExtractors(res.data)
      })
    } catch (error) {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    }
  }, []);

  return (
    <div className={classes.workBench}>
      <NavLink className={classes.newExtractorDiv} to="/manage/extractor/create">
        <div className={classes.newInnerExtractorDiv}><i className="fa fa-plus-circle" aria-hidden="true"></i> New Extractor</div>
      </NavLink>
      {extractors.map(extractor => {
        return (
          <NavLink className={classes.extractorDiv} to={"/manage/extractor/"+extractor.id}>
            <div className={classes.innerExtractorDiv}>
              {extractor.name}
              <div className={classes.actionDiv}>
                <ul>
                  <li><i className="fa fa-copy" onClick={() => copyBtnClickHandler(extractor.id)}></i></li>
                  <li><i className="fa fa-trash" aria-hidden="true" onClick={() => trashBtnClickHandler(extractor.id)}></i></li>
                </ul>
              </div>
            </div>
          </NavLink>
        )
      })}
    </div>
  )
}

export default withRouter(withStyles(useStyles)(ManageMain))