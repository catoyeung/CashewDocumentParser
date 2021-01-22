import React, { useState } from 'react';

import { AppContext } from "../../context/provider"

import { withRouter, NavLink } from "react-router-dom";

import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import { Container, Button } from '@material-ui/core';

import getAPI from '../../API'

const useStyles = makeStyles((theme) => ({
  header: {
    background: "white",
    borderBottom: "5px solid #e31a2f",
    width: "100%"
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
  menuList: {
    lineHeight: "80px",
    fontSize: "28px",
    [theme.breakpoints.down('sm')]: {
      float: "none",
      display: "block",
      width: "100%",
      fontSize: "20px"
    },
    [theme.breakpoints.up('md')]: {
      float: "none",
      display: "block",
      width: "100%",
      fontSize: "20px"
    },
    [theme.breakpoints.up('lg')]: {
      float: "right",
      width: "auto"
    },
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
  menuListSecondLevel: {
    zIndex: "9999",
    display: "none",
    left: "50%",
    transform: "translateX(-50%)",
    width: "100%",
    maxWidth: "1280px",
    boxSizing: "border-box",
    background: "white",
    [theme.breakpoints.down('sm')]: {
      position: "relative",
      top: "0",
      borderLeft: "none",
      borderRight: "none",
      borderBottom: "none"
    },
    [theme.breakpoints.up('md')]: {
      position: "relative",
      top: "0",
      borderLeft: "none",
      borderRight: "none",
      borderBottom: "none"
    },
    [theme.breakpoints.up('lg')]: {
      position: "fixed",
      top: "85px",
      borderLeft: "5px solid #e31a2f",
      borderRight: "5px solid #e31a2f",
      borderBottom: "5px solid #e31a2f",
    },
  },
  menuListSecondLevelItem: {
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

const DefaultHeader = (props) => {

  const context = React.useContext(AppContext)

  const API = getAPI(props.history)

  const [activeMenuListItem, setActiveMenuListItem] = useState("")
  //const [isAuthenticated, setIsAuthenticated] = useState(localStorage.getItem("isAuthenticated"))

  const menuListItemClickHandler = (listItemId) => {
    if (activeMenuListItem === listItemId) {
      setActiveMenuListItem("")
    } else {
      setActiveMenuListItem(listItemId)
    }
  }

  const loginBtnClickHandler = () => {
    props.history.push("/account/signin");
  }

  const logoutBtnClickHandler = async () => {
    await API.post("account/signout", {}, { withCredentials: true }).then(() => {
      try {
        localStorage.removeItem("isAuthenticated")
        context.setIsAuthenticated(false)
        props.history.push("")
      } catch {

      }
      props.history.push("/");
    })
  }

  const consoleBtnClickHandler = () => {
    props.history.push("/manage/main");
  }

  const classes = useStyles();

  return (
    <header className={classes.header}>
      <Container>
        <NavLink activeClassName={classes.active} to="/">
          <img className={classes.logo}
            src="/static/img/kyocera-logo.png"
            alt="KYOCERA Document Solutions Asia" />
        </NavLink>
        <ul className={classes.menuList}>
          <NavLink exact activeClassName={classes.active} to="/">
            <li className={classes.menuListItem}>
              Home
          </li>
          </NavLink>
          <li id="MenuListItem_OurSolutions"
            className={clsx(classes.menuListItem, activeMenuListItem === "MenuListItem_OurSolutions" ? "active" : "")}
            onClick={(e) => menuListItemClickHandler("MenuListItem_OurSolutions")}>
            Our Solutions
          <span className={clsx("material-icons", classes.materialIcons, activeMenuListItem === "MenuListItem_OurSolutions" ? "" : "hidden")}>
              keyboard_arrow_up
          </span>
            <span className={clsx("material-icons", classes.materialIcons, activeMenuListItem === "MenuListItem_OurSolutions" ? "hidden" : "")}>
              keyboard_arrow_down
          </span>
            <ul className={classes.menuListSecondLevel}>
              <li className={classes.menuListSecondLevelItem}>
                <span className="material-icons">
                  insert_drive_file
              </span>
              Contract
            </li>
              <li className={classes.menuListSecondLevelItem}>
                <span className="material-icons">
                  attach_money
              </span>
              Invoice/Account Payable
            </li>
            </ul>
          </li>
          <li className={classes.menuListItem}>How it works</li>
          <li className={classes.menuListItem}>About Us</li>
          <li className={classes.menuListItem}>Contact Us</li>
          <li className={classes.menuListItem}>
            {!context.isAuthenticated &&
              <Button className="btn"
                onClick={loginBtnClickHandler}>LOGIN</Button>
            }
            {context.isAuthenticated &&
              <Button className="btn"
                onClick={logoutBtnClickHandler}>LOGOUT</Button>
            }
            {context.isAuthenticated &&
              <Button className="btn"
                onClick={consoleBtnClickHandler}>CONSOLE</Button>
            }
          </li>
        </ul>
      </Container>
    </header>
  )
}

export default withRouter(DefaultHeader)