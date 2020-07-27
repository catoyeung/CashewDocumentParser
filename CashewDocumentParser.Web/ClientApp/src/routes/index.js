import React from "react"
import { Switch } from "react-router-dom"
import Route from "./Route"
import PrivateRoute from "./PrivateRoute"

import MainLayout from "../_layouts/default"
import ManageLayout from "../_layouts/manage"

import SignIn from "../components/account/signin"
import SignUp from "../components/account/signup"
import SignUpComplete from "../components/account/signup-complete"
import VerifyEmail from "../components/account/verify-email"
import ForgetPassword from "../components/account/forget-password"
import ResetPassword from "../components/account/reset-password"
import SessionTimeout from "../components/account/session-timeout"

import Home from "../components/pages/Home"

import ManageMain from "../components/manage/main"
import ManageCreateExtractor from "../components/manage/extractor/create"
import ManageUploadSampleDocument from "../components/manage/extractor/sample-document/upload"

export default function Routes() {
  return (
    <Switch>
      <Route path="/" exact component={Home} layout={MainLayout} />

      {/* redirect user to SignIn page if route does not exist and user is not authenticated */}
      <Route path="/account/signin" exact component={SignIn} layout={MainLayout} />
      <Route path="/account/signup" exact component={SignUp} layout={MainLayout} />
      <Route path="/account/signup-complete" exact component={SignUpComplete} layout={MainLayout} />
      <Route path="/account/verify-email" exact component={VerifyEmail} layout={MainLayout} />
      <Route path="/account/forget-password" exact component={ForgetPassword} layout={MainLayout} />
      <Route path="/account/reset-password" exact component={ResetPassword} layout={MainLayout} />
      <Route path="/account/session-timeout" exact component={SessionTimeout} layout={MainLayout} />

      <PrivateRoute path="/manage/main" exact component={ManageMain} layout={ManageLayout} />
      <PrivateRoute path="/manage/extractor/create" exact component={ManageCreateExtractor} layout={ManageLayout} />
      <PrivateRoute path="/manage/extractor/:templateId/sample-document/upload" exact component={ManageUploadSampleDocument} layout={ManageLayout} />
    </Switch>
  );
}
