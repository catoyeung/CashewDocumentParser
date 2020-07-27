import React from "react";
import PropTypes from "prop-types";
import { Route, Redirect } from "react-router-dom";

import DefaultLayout from "../_layouts/default";
import ManageLayout from "../_layouts/manage";

export default function RouteWrapper({
  component: Component,
  isPrivate,
  ...rest
}) {

  const Layout = DefaultLayout;

  return (
    <Route
      {...rest}
      render={props => (
        <Layout>
          <Component {...props} />
        </Layout>
      )}
    />
  );
}

RouteWrapper.propTypes = {
  isPrivate: PropTypes.bool,
  component: PropTypes.oneOfType([PropTypes.element, PropTypes.func]).isRequired
};

RouteWrapper.defaultProps = {
  isPrivate: false
};
