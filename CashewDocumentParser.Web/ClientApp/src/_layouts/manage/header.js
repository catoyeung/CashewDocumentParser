import React, { useState } from 'react';

import { AppContext } from "../../context/provider"

import { withRouter, NavLink } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { Container, Button } from '@material-ui/core';

import API from '../../API'

const useStyles = makeStyles((theme) => ({
  header: {
    background: "white",
    borderBottom: "5px solid #e31a2f",
    width: "100%",
    padding: "15px"
  },
  logo: {
    [theme.breakpoints.down('sm')]: {
      float: "none",
      display: "block",
      margin: "auto"
    },
    [theme.breakpoints.up('md')]: {
      float: "none",
      display: "block",
      margin: "auto"
    },
    [theme.breakpoints.up('lg')]: {
      float: "left",
    },
  },
  leftMenuList: {
    marginLeft: "30px",
    lineHeight: "50px",
    fontSize: "16px",
    float: "left",
    width: "auto"
  },
  rightMenuList: {
    lineHeight: "50px",
    fontSize: "16px",
    float: "right",
    width: "auto"
  },
  menuListItem: {
    position: "relative",
    marginRight: "20px",
    cursor: "pointer",
    "&.active $menuListSecondLevel": {
      display: "block"
    },
    [theme.breakpoints.down('sm')]: {
      display: "block",
      width: "100%",
      textAlign: "center",
      "&.active": {
        borderTop: "5px solid #e31a2f",
        borderRight: "5px solid #e31a2f",
        borderBottom: "5px solid #e31a2f",
        borderLeft: "5px solid #e31a2f"
      }
    },
    [theme.breakpoints.up('md')]: {
      display: "block",
      width: "100%",
      textAlign: "center",
      "&.active": {
        borderTop: "5px solid #e31a2f",
        borderRight: "5px solid #e31a2f",
        borderBottom: "5px solid #e31a2f",
        borderLeft: "5px solid #e31a2f"
      }
    },
    [theme.breakpoints.up('lg')]: {
      display: "inline-block",
      width: "auto",
      "&.active": {
        borderTop: "none",
        borderRight: "none",
        borderBottom: "none",
        borderLeft: "none"
      }
    }
  },
  menuListItemMaterialIcons: {
    fontSize: "16px"
  },
  menuListSecondLevel: {
    zIndex: "9999",
    display: "block",
    right: "0",
    width: "200px",
    maxWidth: "1280px",
    boxSizing: "border-box",
    background: "white",
    position: "absolute",
    top: "70px",
    borderLeft: "5px solid #e31a2f",
    borderRight: "5px solid #e31a2f",
    borderBottom: "5px solid #e31a2f",
    "&.hidden": {
      display: "none"
    }
  },
  menuListSecondLevelItem: {
    textAlign: "left",
    padding: "0 10px",
    '&:hover': {
      background: "#e31a2f",
      color: "white",
      cursor: "pointer"
    }
  },
  menumenuListButton: {
    marginRight: theme.spacing(2),
  },
  materialIcons: {
    verticalAlign: "middle",
    "&.hidden": {
      display: "none"
    }
  },
  active: {
    color: "#e31a2f"
  }
}));

const ManageHeader = (props) => {

  const context = React.useContext(AppContext)

  const [activeMenuListItem, setActiveMenuListItem] = useState("")

  const menuListItemClickHandler = (listItemId) => {
    if (activeMenuListItem === listItemId) {
      setActiveMenuListItem("")
    } else {
      setActiveMenuListItem(listItemId)
    }
  }

  const logoutBtnClickHandler = async () => {
    await API.post("Account/SignOut", {}, { withCredentials: true }).then(() => {
      try {
        localStorage.removeItem("isAuthenticated")
        context.setIsAuthenticated(false)
      } catch {

      }
      props.history.push("/");
    })
  }

  const classes = useStyles();

  return (
    <header className={classes.header}>
      <NavLink activeClassName={classes.active} to="/">
        <img className={classes.logo}
              src="/static/img/kyocera-logo-manage.png"
            alt="KYOCERA Document Solutions Asia" />
      </NavLink>
      <ul className={classes.leftMenuList}>
        <NavLink exact activeClassName={classes.active} to="/manage/main">
          <li className={classes.menuListItem}>
            <i className="fa fa-table" aria-hidden="true"></i> Workspace
          </li>
        </NavLink>
      </ul>
      <ul className={classes.rightMenuList}>
        <li id="MenuListItem_MyAccount"
          className={clsx(classes.menuListItem)}
          onClick={(e) => menuListItemClickHandler("MenuListItem_MyAccount")}>
          My Account
          <span className={clsx("material-icons", classes.materialIcons, activeMenuListItem === "MenuListItem_MyAccount" ? "" : "hidden")}>
            keyboard_arrow_up
          </span>
          <span className={clsx("material-icons", classes.materialIcons, activeMenuListItem === "MenuListItem_MyAccount" ? "hidden" : "")}>
            keyboard_arrow_down
          </span>
          <ul className={clsx(classes.menuListSecondLevel, classes.materialIcons, activeMenuListItem === "MenuListItem_MyAccount" ? "" : "hidden")}>
            <li className={classes.menuListSecondLevelItem}>
              <i className="fa fa-user" aria-hidden="true"></i> Account
            </li>
            <li className={classes.menuListSecondLevelItem}
              onClick={logoutBtnClickHandler}>
              <i className="fa fa-sign-out" aria-hidden="true"></i> Logout
            </li>
          </ul>
        </li>
      </ul>
    </header>
  )
}

export default withRouter(ManageHeader)