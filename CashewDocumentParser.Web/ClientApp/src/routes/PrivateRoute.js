
import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import { AppContext } from "../context/provider"

function PrivateRoute({ component: Component, ...rest }) {

  const context = React.useContext(AppContext)

  const Layout = rest.layout

  return (
    <Route
      {...rest}
      render={props =>
        context.isAuthenticated != null ? (
          <Layout>
            <Component {...props} />
          </Layout>
        ) : (
            <Redirect to="/account/signin" />
          )
      }
    />
  );
}

export default PrivateRoute;