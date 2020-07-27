import React from 'react';
import { Router } from "react-router-dom";

import AppProvider from "./context/provider"

import { makeStyles } from '@material-ui/core/styles';

import history from "./services/history";
import Routes from "./routes";

import './App.css';

const useStyles = makeStyles((theme) => ({
  app: {
    display: "flex",
    minHeight: "100vh",
    flexDirection: "column"
  }
}));

function App() {

  const classes = useStyles();

  return (
    <AppProvider>
      <div className={classes.app}>
        <Router history={history}>
          <Routes />
        </Router>
      </div>
    </AppProvider>
  );
}

export default App;
