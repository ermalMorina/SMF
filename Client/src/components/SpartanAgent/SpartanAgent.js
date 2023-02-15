import React, { useState, useEffect } from "react";
import SidebarAgent from "./SidebarAgent";

function SpartanAgent() {
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
    <SidebarAgent />
  );
}

export default SpartanAgent;
