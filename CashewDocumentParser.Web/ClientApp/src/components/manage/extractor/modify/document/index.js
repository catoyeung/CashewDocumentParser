import React, { useState, useEffect } from "react";

import { withRouter, NavLink, useHistory } from "react-router-dom";

import { AppContext } from "../../../../../context/provider"

import { makeStyles, withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core'
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import DescriptionIcon from '@material-ui/icons/Description';
import ListItemText from '@material-ui/core/ListItemText';
import Button from '@material-ui/core/Button';
import PublishIcon from '@material-ui/icons/Publish';
import RefreshIcon from '@material-ui/icons/Refresh';
import Checkbox from '@material-ui/core/Checkbox';
import IconButton from '@material-ui/core/IconButton';

import Select from 'react-select'

import _ from 'lodash'

import getAPI from '../../../../../API'

import ExtractorSidebar from '../sidebar'

const useStyles = makeStyles((theme) => ({
  workBench: {
    display: "flex",
    flexGrow: 1,
    padding: 0,
  },
  extractorDocumentListDiv: {
    display: "flex",
    flexGrow: 1,
    fontSize: "21px",
    fontWeight: 400,
    background: "transparent",
    textTransform: "uppercase",
    width: "100%"
  },
  appBar: {
    backgroundColor: "#e31a2f",
    color: "#fff",
    flexDirection: "row"
  },
  tabs: {
    flexGrow: 1
  },
  uploadBtn: {
    color: "#fff",
    marginRight: "5px"
  },
  refreshBtn: {
    color: "#fff",
    marginRight: "35px"
  },
  actionListItem: {
    border: "1px solid #666",
    "&:not(:last-child)": {
      borderBottom: "none"
    }
  },
  actionOptionsSelect: {
    fontSize: "16px",
    width: "300px"
  },
  documentListItem: {
    border: "1px solid #666",
    "&:not(:last-child)": {
      borderBottom: "none"
    }
  }
}));

const ExtractorDocumentList = (props) => {

  const history = useHistory()

  const API = getAPI(history)

  const context = React.useContext(AppContext)

  const classes = useStyles()

  const [extractorName, setExtractorName] = useState("")

  const [requestSent, setRequestSent] = useState(false)

  const [tabIndex, setTabIndex] = useState(0)

  const [importQueue, setImportQueue] = useState([])

  const actionOptions = [
    { value: 'moveToPreprocessingQueue', label: 'Move to Preprocessing Queue' },
    { value: 'moveToExtractQueue', label: 'Move to Extract Queue' },
    { value: 'createNewLayoutModel', label: 'Create New Layout Model' },
    { value: 'deleteDocument', label: 'Delete Document' }
  ]

  useEffect(() => {
    API.get("templates/" + props.match.params.id + "/importqueue").then((res) => {
      setImportQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }, []);

  const handleTabChange = (event, newTabIndex) => {
    setTabIndex(newTabIndex);
  };

  const switchTab = (index) => {
    return {
      id: `queue-tab-${index}`,
      'aria-controls': `queue-tabpanel-${index}`,
    };
  }

  const TabPanel = (props) => {
    const { children, value, index, ...other } = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`queue-tabpanel-${index}`}
        aria-labelledby={`queue-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box p={3}>
            <div>{children}</div>
          </Box>
        )}
      </div>
    );
  }

  const allImportQueueChecked = () => {
    if (importQueue.length == 0) return false
    let isChecked = true
    for(const importQueueItem of importQueue) {
      if(importQueueItem.checked == undefined)
        isChecked = false;
      if(importQueueItem.checked == false)
        isChecked = false;
    }
    return isChecked
  }

  const importQueueAllCheckboxChangeHandler = () => {
    const newImportQueue = [...importQueue]
    if (allImportQueueChecked()) {
      for(const importQueueItem of importQueue) {
        importQueueItem.checked = false
      }
    } else {
      for(const importQueueItem of importQueue) {
        importQueueItem.checked = true
      }
    }
    setImportQueue(newImportQueue)
  }

  const importQueueCheckboxChangeHandler = (importQueueIndex) => {
    const newImportQueue = [...importQueue]
    if (newImportQueue[importQueueIndex].checked == undefined) {
      newImportQueue[importQueueIndex].checked = true
    } else {
      newImportQueue[importQueueIndex].checked = !newImportQueue[importQueueIndex].checked
    }
    setImportQueue(newImportQueue)
  }

  return (
    <div className={classes.workBench}>
      <div className={classes.extractorDocumentListDiv}>
        <Grid container style={{ display: "flex", flexGrow: 1}}>
          <Grid item style={{ display: "flex", flexGrow: 1}} lg={2} xs={12}>
            <ExtractorSidebar />
          </Grid>
          <Grid item lg={10} xs={12}>
            <AppBar position="static"
              className={classes.appBar}>
              <Tabs className={classes.tabs}
                value={tabIndex} 
                onChange={handleTabChange} 
                aria-label="simple tabs example">
                <Tab label="Processed Queue" {...switchTab(0)} />
                <Tab label="Import Queue" {...switchTab(1)} />
                <Tab label="Preprocessing Queue" {...switchTab(2)} />
                <Tab label="Extract Queue" {...switchTab(3)} />
                <Tab label="Integration Queue" {...switchTab(4)} />
              </Tabs>
              <Button className={classes.uploadBtn}>
                <PublishIcon />Upload Documents
              </Button>
              <Button className={classes.refreshBtn}>
                <RefreshIcon />Refresh
              </Button>
            </AppBar>
            <TabPanel value={tabIndex} index={0}>
              Processed
            </TabPanel>
            <TabPanel value={tabIndex} index={1}>
            <List dense={false}>
              <ListItem className={classes.actionListItem}>
                <Checkbox checked={allImportQueueChecked()} onChange={importQueueAllCheckboxChangeHandler}/>
                <Select className={classes.actionOptionsSelect} 
                  placeholder="Action Options..."
                  options={actionOptions} />
              </ListItem>
              {importQueue.map((queue, queueIndex) => (
                <ListItem key={queue.guid} className={classes.documentListItem}>
                  <Checkbox checked={queue.checked} onChange={() => importQueueCheckboxChangeHandler(queueIndex)} />
                  <DescriptionIcon />
                  <ListItemText
                    primary={queue.filenameWithoutExtension + "." + queue.extension}
                  />
                </ListItem>
              ))}
            </List>
              
            </TabPanel>
            <TabPanel value={tabIndex} index={2}>
              Preprocessing
            </TabPanel>
            <TabPanel value={tabIndex} index={3}>
              Extract
            </TabPanel>
            <TabPanel value={tabIndex} index={4}>
              Integration
            </TabPanel>
          </Grid>
        </Grid>
      </div>
    </div>
  )
}

export default withRouter(ExtractorDocumentList)