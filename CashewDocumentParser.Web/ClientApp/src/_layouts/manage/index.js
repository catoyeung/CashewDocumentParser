import React from 'react'; 
import PropTypes from 'prop-types';

import { Snackbar } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';

import { AppContext } from "../../context/provider"

import { makeStyles } from '@material-ui/core/styles';

import Header from './header'
import Footer from './footer'

const useStyles = makeStyles((theme) => ({
  content: {
    display: "flex",
    flex: 1
  }
}));

const ManageLayout = ({ children }) => {

  const context = React.useContext(AppContext)

  const classes = useStyles();

  return (
    <React.Fragment>
      <Header />
        <div className={classes.content}>
          {children}
        </div>
        <Snackbar open={context.shouldSuccessMessageOpen} autoHideDuration={6000} onClose={context.successMessageCloseHandler}>
          <Alert onClose={context.successMessageCloseHandler} severity="success">
            {context.successMessage}
          </Alert>
        </Snackbar>
        <Snackbar open={context.shouldErrorMessageOpen} autoHideDuration={6000} onClose={context.errorMessageCloseHandler}>
          <Alert onClose={context.errorMessageCloseHandler} severity="error">
            {context.errorMessage}
          </Alert>
        </Snackbar>
      <Footer />
    </React.Fragment>
  )
}  

ManageLayout.propTypes = {   
  children: PropTypes.element.isRequired, 
};

export default ManageLayout