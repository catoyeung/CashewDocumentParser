import React from 'react'; 
import PropTypes from 'prop-types';

import { makeStyles } from '@material-ui/core/styles';

import Container from '@material-ui/core/Container';

import Header from './header'
import Footer from './footer'

const useStyles = makeStyles((theme) => ({
  content: {
    flex: 1
  }
}));

const DefaultLayout = ({ children }) => {

  const classes = useStyles();

  return (
    <React.Fragment>
      <Header />
        <Container className={classes.content}>
          {children}
        </Container>
      <Footer />
    </React.Fragment>
  )
}  

DefaultLayout.propTypes = {   
  children: PropTypes.element.isRequired, 
};

export default DefaultLayout