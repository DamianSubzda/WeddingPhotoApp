import React from "react";
import "./Button.css";

function Button({ content, onClick }) {
  return <div className="button" onClick={onClick}>{content}</div>;
}

export default Button;
