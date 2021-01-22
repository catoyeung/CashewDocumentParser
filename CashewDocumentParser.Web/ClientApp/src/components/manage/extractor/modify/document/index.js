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

  const [tabIndex, setTabIndex] = useState(0)

  const [processedQueue, setProcessedQueue] = useState([])
  const [importQueue, setImportQueue] = useState([])
  const [preprocessingQueue, setPreprocessingQueue] = useState([])
  const [extractQueue, setExtractQueue] = useState([])
  const [integrationQueue, setIntegrationQueue] = useState([])

  const actionOptions = [
    { value: 'moveToPreprocessingQueue', label: 'Move to Preprocessing Queue' },
    { value: 'moveToExtractQueue', label: 'Move to Extract Queue' },
    { value: 'createNewLayoutModel', label: 'Create New Layout Model' },
    { value: 'deleteDocument', label: 'Delete Document' }
  ]

  useEffect(() => {
    loadProcessedQueue()
  }, []);

  const loadProcessedQueue = () => {
    API.get("templates/" + props.match.params.id + "/processedqueue").then((res) => {
      setProcessedQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const loadImportQueue = () => {
    API.get("templates/" + props.match.params.id + "/importqueue").then((res) => {
      setImportQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const loadPreprocessingQueue = () => {
    API.get("templates/" + props.match.params.id + "/preprocessingqueue").then((res) => {
      setPreprocessingQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const loadExtractQueue = () => {
    API.get("templates/" + props.match.params.id + "/extractqueue").then((res) => {
      setExtractQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const loadIntegrationQueue = () => {
    API.get("templates/" + props.match.params.id + "/integrationqueue").then((res) => {
      setIntegrationQueue(res.data)
    }).catch(error => {
      if (error.response?.data) {
        context.setErrorMessage(error.response.data.errorMessage)
      } else {
        context.setErrorMessage("Sorry, something went wrong. Please contact system administrator")
      }
    })
  }

  const handleTabChange = (event, newTabIndex) => {
    if (newTabIndex == 0) loadProcessedQueue()
    else if (newTabIndex == 1) loadImportQueue()
    else if (newTabIndex == 2) loadPreprocessingQueue()
    else if (newTabIndex == 3) loadExtractQueue()
    else if (newTabIndex == 4) loadIntegrationQueue()
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

  const allProcessedQueueChecked = () => {
    if (processedQueue.length == 0) return false
    let isChecked = true
    for(const processedQueueItem of processedQueue) {
      if(processedQueueItem.checked == undefined)
        isChecked = false;
      if(processedQueueItem.checked == false)
        isChecked = false;
    }
    return isChecked
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

  const allPreprocessingQueueChecked = () => {
    if (preprocessingQueue.length == 0) return false
    let isChecked = true
    for(const preprocessingQueueItem of preprocessingQueue) {
      if(preprocessingQueueItem.checked == undefined)
        isChecked = false;
      if(preprocessingQueueItem.checked == false)
        isChecked = false;
    }
    return isChecked
  }

  const allExtractQueueChecked = () => {
    if (extractQueue.length == 0) return false
    let isChecked = true
    for(const extractQueueItem of extractQueue) {
      if(extractQueueItem.checked == undefined)
        isChecked = false;
      if(extractQueueItem.checked == false)
        isChecked = false;
    }
    return isChecked
  }

  const allIntegrationQueueChecked = () => {
    if (integrationQueue.length == 0) return false
    let isChecked = true
    for(const integrationQueueItem of integrationQueue) {
      if(integrationQueueItem.checked == undefined)
        isChecked = false;
      if(integrationQueueItem.checked == false)
        isChecked = false;
    }
    return isChecked
  }

  const processedQueueAllCheckboxChangeHandler = () => {
    const newProcessedQueue = [...processedQueue]
    if (allProcessedQueueChecked()) {
      for(const processedQueueItem of processedQueue) {
        processedQueueItem.checked = false
      }
    } else {
      for(const processedQueueItem of processedQueue) {
        processedQueueItem.checked = true
      }
    }
    setProcessedQueue(newProcessedQueue)
  }

  const importQueueAllCheckboxChangeHandler = () => {
    const newImportQueue = [...importQueue]
    if (allImportQueueChecked()) {
      for(const importQueueItem of newImportQueue) {
        importQueueItem.checked = false
      }
    } else {
      for(const importQueueItem of newImportQueue) {
        importQueueItem.checked = true
      }
    }
    setImportQueue(newImportQueue)
  }

  const preprocessingQueueAllCheckboxChangeHandler = () => {
    const newPreprocessingQueue = [...preprocessingQueue]
    if (allPreprocessingQueueChecked()) {
      for(const preprocessingQueueItem of newPreprocessingQueue) {
        preprocessingQueueItem.checked = false
      }
    } else {
      for(const preprocessingQueueItem of newPreprocessingQueue) {
        preprocessingQueueItem.checked = true
      }
    }
    setPreprocessingQueue(newPreprocessingQueue)
  }

  const extractQueueAllCheckboxChangeHandler = () => {
    const newExtractQueue = [...extractQueue]
    if (allExtractQueueChecked()) {
      for(const newExtractQueueItem of newExtractQueue) {
        newExtractQueueItem.checked = false
      }
    } else {
      for(const newExtractQueueItem of newExtractQueue) {
        newExtractQueueItem.checked = true
      }
    }
    setExtractQueue(newExtractQueue)
  }

  const integrationQueueAllCheckboxChangeHandler = () => {
    const newIntegrationQueue = [...integrationQueue]
    if (allIntegrationQueueChecked()) {
      for(const integrationQueueItem of newIntegrationQueue) {
        integrationQueueItem.checked = false
      }
    } else {
      for(const integrationQueueItem of newIntegrationQueue) {
        integrationQueueItem.checked = true
      }
    }
    setIntegrationQueue(newIntegrationQueue)
  }

  const processedQueueCheckboxChangeHandler = (processedQueueIndex) => {
    const newProcessedQueue = [...processedQueue]
    if (newProcessedQueue[processedQueueIndex].checked == undefined) {
      newProcessedQueue[processedQueueIndex].checked = true
    } else {
      newProcessedQueue[processedQueueIndex].checked = !newProcessedQueue[processedQueueIndex].checked
    }
    setProcessedQueue(newProcessedQueue)
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

  const preprocessingQueueCheckboxChangeHandler = (preprocessingQueueIndex) => {
    const newPreprocessingQueue = [...preprocessingQueue]
    if (newPreprocessingQueue[preprocessingQueueIndex].checked == undefined) {
      newPreprocessingQueue[preprocessingQueueIndex].checked = true
    } else {
      newPreprocessingQueue[preprocessingQueueIndex].checked = !newPreprocessingQueue[preprocessingQueueIndex].checked
    }
    setPreprocessingQueue(newPreprocessingQueue)
  }

  const extractQueueCheckboxChangeHandler = (extractQueueIndex) => {
    const newExtractQueue = [...extractQueue]
    if (newExtractQueue[extractQueueIndex].checked == undefined) {
      newExtractQueue[extractQueueIndex].checked = true
    } else {
      newExtractQueue[extractQueueIndex].checked = !newExtractQueue[extractQueueIndex].checked
    }
    setExtractQueue(newExtractQueue)
  }

  const integrationQueueCheckboxChangeHandler = (integrationQueueIndex) => {
    const newIntegrationQueue = [...integrationQueue]
    if (newIntegrationQueue[integrationQueueIndex].checked == undefined) {
      newIntegrationQueue[integrationQueueIndex].checked = true
    } else {
      newIntegrationQueue[integrationQueueIndex].checked = !newIntegrationQueue[integrationQueueIndex].checked
    }
    setIntegrationQueue(newIntegrationQueue)
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
                <Tab label="Processed" {...switchTab(0)} />
                <Tab label="Import" {...switchTab(1)} />
                <Tab label="OCR" {...switchTab(1)} />
                <Tab label="Classification" {...switchTab(1)} />
                <Tab label="Preprocessing" {...switchTab(2)} />
                <Tab label="Extract" {...switchTab(3)} />
                <Tab label="Integration" {...switchTab(4)} />
              </Tabs>
              <Button className={classes.uploadBtn}>
                <PublishIcon />Upload Documents
              </Button>
              <Button className={classes.refreshBtn}>
                <RefreshIcon />Refresh
              </Button>
            </AppBar>
            <TabPanel value={tabIndex} index={0}>
              <List dense={false}>
                <ListItem className={classes.actionListItem}>
                  <Checkbox checked={allProcessedQueueChecked()} onChange={processedQueueAllCheckboxChangeHandler}/>
                  <Select className={classes.actionOptionsSelect} 
                    placeholder="Action Options..."
                    options={actionOptions} />
                </ListItem>
                {processedQueue.map((queue, queueIndex) => (
                  <ListItem key={queue.guid} className={classes.documentListItem}>
                    <Checkbox checked={queue.checked} onChange={() => processedQueueCheckboxChangeHandler(queueIndex)} />
                    <DescriptionIcon />
                    <ListItemText
                      primary={queue.filenameWithoutExtension + "." + queue.extension}
                    />
                  </ListItem>
                ))}
              </List>
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
              <List dense={false}>
                <ListItem className={classes.actionListItem}>
                  <Checkbox checked={allPreprocessingQueueChecked()} onChange={preprocessingQueueAllCheckboxChangeHandler}/>
                  <Select className={classes.actionOptionsSelect} 
                    placeholder="Action Options..."
                    options={actionOptions} />
                </ListItem>
                {preprocessingQueue.map((queue, queueIndex) => (
                  <ListItem key={queue.guid} className={classes.documentListItem}>
                    <Checkbox checked={queue.checked} onChange={() => preprocessingQueueCheckboxChangeHandler(queueIndex)} />
                    <DescriptionIcon />
                    <ListItemText
                      primary={queue.filenameWithoutExtension + "." + queue.extension}
                    />
                  </ListItem>
                ))}
              </List>
            </TabPanel>
            <TabPanel value={tabIndex} index={3}>
              <List dense={false}>
                <ListItem className={classes.actionListItem}>
                  <Checkbox checked={allExtractQueueChecked()} onChange={extractQueueAllCheckboxChangeHandler}/>
                  <Select className={classes.actionOptionsSelect} 
                    placeholder="Action Options..."
                    options={actionOptions} />
                </ListItem>
                {extractQueue.map((queue, queueIndex) => (
                  <ListItem key={queue.guid} className={classes.documentListItem}>
                    <Checkbox checked={queue.checked} onChange={() => extractQueueCheckboxChangeHandler(queueIndex)} />
                    <DescriptionIcon />
                    <ListItemText
                      primary={queue.filenameWithoutExtension + "." + queue.extension}
                    />
                  </ListItem>
                ))}
              </List>
            </TabPanel>
            <TabPanel value={tabIndex} index={4}>
              <List dense={false}>
                <ListItem className={classes.actionListItem}>
                  <Checkbox checked={allIntegrationQueueChecked()} onChange={integrationQueueAllCheckboxChangeHandler}/>
                  <Select className={classes.actionOptionsSelect} 
                    placeholder="Action Options..."
                    options={actionOptions} />
                </ListItem>
                {integrationQueue.map((queue, queueIndex) => (
                  <ListItem key={queue.guid} className={classes.documentListItem}>
                    <Checkbox checked={queue.checked} onChange={() => integrationQueueCheckboxChangeHandler(queueIndex)} />
                    <DescriptionIcon />
                    <ListItemText
                      primary={queue.filenameWithoutExtension + "." + queue.extension}
                    />
                  </ListItem>
                ))}
              </List>
            </TabPanel>
          </Grid>
        </Grid>
      </div>
    </div>
  )
}

export default withRouter(ExtractorDocumentList)