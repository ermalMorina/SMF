import React, { useState, useEffect } from "react";
import SidebarAgent from "../SidebarAgent";

export default function Verify() {
  const [logged, setlogged] = useState(false);

  useEffect(() => {
    var items = null;
    items = localStorage.getItem("email");
    if (items != null) {
      setlogged(true);
    }
    console.log("LOGGED???", logged);
  });
  
  return (
    <div>
      {logged ? (
        <>
          <SidebarAgent />
          <div
            style={{
              position: "absolute",
              top: "10%",
              left: "25%",
              width: "1000px",
              backgroundColor: "red",
              border: "1px solid black",
            }}
          >
            <h1>Verify</h1>
          </div>
        </>
      ) : (
        <div>
          <h4>Agent Verify</h4>
          <h1>Sorry you are Unauthorized for this page </h1>
        </div>
      )}
    </div>
  );
} 
