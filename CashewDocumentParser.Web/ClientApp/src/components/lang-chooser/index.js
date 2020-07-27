import React from "react";

import { useTranslation } from 'react-i18next'

import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
  langChooser: {
    lineHeight: 1.75
  },
  langChooserItem: {
    float: "left",
    display: "inline-block",
    marginRight: "10px",
    cursor: "pointer"
  }
}));

export default function LangChooser() {

  const [t, i18n] = useTranslation();

  const classes = useStyles();

  const changeLanguage = (lang) => {
    i18n.changeLanguage(lang);
  }

  return (
    <ul className={classes.langChooser}>
      <li className={classes.langChooserItem} onClick={() => changeLanguage('en')}>EN</li>
      <li className={classes.langChooserItem} onClick={() => changeLanguage('zh')}>繁</li>
      <li className={classes.langChooserItem} onClick={() => changeLanguage('cn')}>殘</li>
    </ul>
  )
}
