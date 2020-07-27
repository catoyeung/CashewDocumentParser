import React, { useState, useEffect } from "react";

import { withRouter } from "react-router-dom";

import { makeStyles, withStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core'

const useStyles = makeStyles((theme) => ({
  banner: {
    position: "relative",
    zIndex: "1000",
    flexGrow: 1,
    height: "360px",
    padding: "30px 20px"
  },
  dynamicHeader: {
    fontSize: "30px",
    marginBottom: "30px"
  },
  dynamicDocument: {
    textDecoration: "underline"
  },
  description: {
    fontSize: "24px",
    width: "400px",
    marginBottom: "20px"
  },
  bannerBotton: {
    backgroundColor: "#e31a2f",
    color: "white",
    borderRadius: "3px",
    marginRight: "10px",
    transition: "box-shadow .3s",
    "&:hover": {
      backgroundColor: "#e31a2f",
      textShadow: "2px 2px 4px red",
      boxShadow: "2px 2px 4px rgba(33,33,33,.5)"
    }
  }
}));

const Home = (props) => {

  const classes = useStyles()

  const dynamicDocuments = ["Invoices", "Account Payables", "Contracts"]
  const [dynamicDocumentTextPosition, setDynamicDocumentTextPosition] = useState({
    dynamicDocumentIndex: 0,
    textPosition: 0,
    text: "",
    fullTextShownWaitCount: 0
  })

  const startToUseBtnClickHandler = () => {
    props.history.push("account/signup");
  }

  useEffect(() => {
    const dynamicDocumentTextInterval = setInterval(() => {
      let fullText = dynamicDocuments[dynamicDocumentTextPosition.dynamicDocumentIndex]
      if (dynamicDocumentTextPosition.textPosition < fullText.length) {
        let newTextPosition = dynamicDocumentTextPosition.textPosition + 1
        let newText = fullText.substring(0, newTextPosition)
        setDynamicDocumentTextPosition({
          dynamicDocumentIndex: dynamicDocumentTextPosition.dynamicDocumentIndex,
          textPosition: newTextPosition,
          text: newText,
          fullTextShownWaitCount: 0
        })
      } else if (dynamicDocumentTextPosition.textPosition >= fullText.length) {
        let newFullTextShownWaitCount = dynamicDocumentTextPosition.fullTextShownWaitCount + 1
        setDynamicDocumentTextPosition({
          ...dynamicDocumentTextPosition,
          fullTextShownWaitCount: newFullTextShownWaitCount
        })
        const waitCount = 10
        if (newFullTextShownWaitCount > waitCount) {
          newFullTextShownWaitCount = newFullTextShownWaitCount - waitCount
          let newDynamicDocumentIndex = dynamicDocumentTextPosition.dynamicDocumentIndex + 1
          if (newDynamicDocumentIndex >= dynamicDocuments.length) {
            newDynamicDocumentIndex = newDynamicDocumentIndex - dynamicDocuments.length
          }
          setDynamicDocumentTextPosition({
            dynamicDocumentIndex: newDynamicDocumentIndex,
            textPosition: 0,
            text: "",
            fullTextShownWaitCount: 0
          })
        }
      }
    }, 100);
    return () => clearInterval(dynamicDocumentTextInterval);
  }, [dynamicDocumentTextPosition]);

  return (
    <section className={classes.banner}>
      <h2 className={classes.dynamicHeader}>
        Extract Data From <span className={classes.dynamicDocument}>{dynamicDocumentTextPosition.text}</span><br/>
        And Automate Your Business
      </h2>
      <div className={classes.description}>
        Our ability to parse very specific data from documents is the #1 reason our customers say they love using <strong>KYOCERA Form Xtractor</strong>! Say good-bye to manual data entry and automate your business.
      </div>
      <div>
        <Button className="btn">WATCH 3-MINUTE VIDEO</Button>
        <Button className="btn"
                onClick={startToUseBtnClickHandler}>START TO USE</Button>
      </div>
    </section>
  )
}

export default withRouter(withStyles(useStyles)(Home))