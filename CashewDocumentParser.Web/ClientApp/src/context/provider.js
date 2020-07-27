import React, { Component } from 'react'

export const AppContext = React.createContext();

class AppProvider extends Component {
  state = {
    isAuthenticated: localStorage.getItem("isAuthenticated") == "true" ? true : false,
    shouldErrorMessageOpen: false,
    errorMessage: "",
    shouldSuccessMessageOpen: false,
    successMessage: ""
  }

  render() {
    return (
      <AppContext.Provider
        value={{
          isAuthenticated: this.state.isAuthenticated,
          setIsAuthenticated: isAuthenticated => {
            this.setState({
              isAuthenticated
            });
            if (isAuthenticated) {
              localStorage.setItem("isAuthenticated", "true")
            } else {
              localStorage.removeItem("isAuthenticated")
            }
          },
          shouldErrorMessageOpen: this.state.shouldErrorMessageOpen,
          shouldSuccessMessageOpen: this.state.shouldSuccessMessageOpen,
          errorMessageCloseHandler: () => {
            this.setState({
              shouldErrorMessageOpen: false
            })
          },
          successMessageCloseHandler: () => {
            this.setState({
              shouldSuccessMessageOpen: false
            })
          },
          errorMessage: this.state.errorMessage,
          setErrorMessage: (errorMessage) => {
            this.setState({
              errorMessage,
              shouldErrorMessageOpen: true
            })
          },
          successMessage: this.state.successMessage,
          setSuccessMessage: (successMessage) => {
            this.setState({
              successMessage,
              shouldSuccessMessageOpen: true
            })
          }
        }}
      >
        {this.props.children}
      </AppContext.Provider>
    );
  }
}

export default AppProvider