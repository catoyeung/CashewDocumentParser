import React from 'react'

import { useTranslation } from 'react-i18next'

import { makeStyles } from '@material-ui/core/styles'

import Container from '@material-ui/core/Container'

import LangChooser from '../../components/lang-chooser'

const useStyles = makeStyles((theme) => ({
  footer: {
    padding: "15px",
    display: "flex"
  },
  copyright: {
    flex: 1,
    lineHeight: 1.75
  }
}));

const DefaultFooter = () => {

  const [t, i18n] = useTranslation();

  const classes = useStyles();

  return (
    <div className={classes.footer}>
      <div className={classes.copyright}>
        {t('Copyright by Cato Yeung @ 2020')}
      </div>
      <LangChooser />
    </div>
  )
}

export default DefaultFooter