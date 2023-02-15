import React from "react";
import "./teamadminstyle.css";
export default function TeamAdminDetails(props) {
  const { city, name, photoUrl, squadLogoUrl, stadiumId } = props;
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        flexDirection: "column",
        border: "2px solid black",
        backgroundColor: "#eaeaea",
      }}
    >
      <h4>Team Details</h4>
      
      <section class="py-5">
        <div class="container px-4 px-lg-5 my-5">
          <div class="row gx-4 gx-lg-5 align-items-center">
            <div class="col-md-6">
              <img
                class="card-img-top mb-5 mb-md-0"
                src={squadLogoUrl}
                alt="..."
              />
            </div>
            <div class="col-md-6">
              <h1 class="small mb-1">City : {city}</h1>
              <h1 class="small mb-1">Name : {name}</h1>
              <h1 class="small mb-1">Stadium : {stadiumId}</h1> 
            </div>
          </div>
        </div>
        
      </section>
    </div>
  );
}
