import React, { useState, useEffect, useCallback } from "react";

import { withRouter, NavLink, useHistory } from "react-router-dom";

import { useDropzone } from 'react-dropzone'

import { v4 as uuidv4 } from 'uuid';

import { AppContext } from "../../../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import { TextField, Button } from '@material-ui/core'

import getAPI from '../../../../../API'

const useStyles = makeStyles((theme) => ({
  workBench: {
    padding: "15px",
    width: "100%"
  },
  uploadSampleDocumentsFormDiv: {
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
  sampleDocumentsDropzone: {
    border: "1px dashed #e31a2f",
    marginBottom: "15px",
    fontSize: "21px",
    textAlign: "center",
    fontWeight: 400,
    background: "transparent",
    color: "#828282",
    textTransform: "uppercase",
    width: "100%",
    minHeight: "360px",
    height: "auto",
    padding: "15px",
    "&:hover": {
      border: "3px dashed #e31a2f",
    }
  },
  fileTypes: {
    fontSize: "14px"
  },
  sampleDocumentsDiv: {
    display: "block",
    height: "auto",
    display: "flex",
    flexWrap: "wrap",
    justifyContent: "space-between",
    "&::after": {
      content: '""',
      flex: "auto"
    }
  },
  sampleDocumentDiv: {
    width: "20%",
    height: "250px",
    padding: "0.5em"
  },
  sampleDocumentInnerDiv: {
    border: "3px solid #e31a2f",
    height: "100%",
    padding: "15px"
  },
  fileName: {
    fontSize: "14px",
    wordWrap: "break-word",
    marginTop: "20px"
  }
}));

const ManageUploadSampleDocument = (props) => {

  const history = useHistory()

  const API = getAPI(history)

  const context = React.useContext(AppContext)

  const classes = useStyles()

  const [uploadedFiles, setUploadedFiles] = useState([])

  const [requestSent, setRequestSent] = useState(false)

  const templateId = props.match.params.templateId

  useEffect(() => {
    
  }, []);

  const onDrop = useCallback(acceptedFiles => {
    console.log(acceptedFiles)
    for (let i=0; i<acceptedFiles.length; i++) {
      acceptedFiles[i].guid = uuidv4()
    }
    setUploadedFiles(uploadedFiles.concat(acceptedFiles))
  }, [])

  const { getRootProps, getInputProps } = useDropzone({ onDrop });

  const cancelBtnClickHandler = (e) => {
    history.push("/manage/main")
  }

  const continueBtnClickHandler = (e) => {
    try {
      var formData = new FormData();
      formData.append("TemplateId", templateId);
      for (let i = 0; i < uploadedFiles.length; i++) {
        formData.append("SampleDocuments", uploadedFiles[i]);
      }
      setRequestSent(true)
      API.post(`templates/${templateId}/sampledocuments`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }).then(() => {
        history.push(`/manage/extractor/modify/${templateId}/document-list`)
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
      <div className={classes.uploadSampleDocumentsFormDiv}>
        <Button className="btn"
          onClick={cancelBtnClickHandler}
          disabled={requestSent}
          variant="outlined" >Cancel</Button>
        <Button className="btn"
          onClick={continueBtnClickHandler}
          disabled={requestSent}>Continue</Button>
      </div>
      <div {...getRootProps({ className: classes.sampleDocumentsDropzone })}>
        <input {...getInputProps()} />
        <p>Drag & Drop or Click to Upload Your Sample Documents Here.</p>
        <p className={classes.fileTypes}>File Types: PDF, TIFF, JPEG, PNG</p>
        <div className={classes.sampleDocumentsDiv}>
            {uploadedFiles && uploadedFiles.map(file => {
              return (
                <div className={classes.sampleDocumentDiv}
                     key={file.guid}>
                  <div className={classes.sampleDocumentInnerDiv}>
                    <i className="fa fa-file-pdf-o" aria-hidden="true"></i>
                    <div className={classes.fileName}>
                    {file.name}
                    </div>
                  </div>
                </div>
              )
            })}
        </div>
      </div>
    </div>
  )
}

export default withRouter(ManageUploadSampleDocument)