import React, { useEffect, useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "./teams.css";
import { Container } from "react-bootstrap";
import Navigation from "../Navigation/navigation";
import Footer from "../Footer/footer";
import axios from "axios";
import TeamComponent from "./TeamComponent";
import TeamDetails from "./TeamDetails";
function Teams() {
  const [squads, setSquads] = useState([]);
  const [gotSquad, setGotSquad] = useState(false);
  //get squads
  useEffect(() => {
    axios.get(`https://localhost:7122/api/Squad`).then((res) => {
      setSquads(res.data);
      setGotSquad(true);
      console.log("setsquads", squads);
      console.log("RES0", res);
    });
  }, []);

  function testing() {
    console.log("res", squads);
  }

  return (
    <div style={{ backgroundColor: "#aaaa" }}>
      <Navigation />
      <div className="teams__header">
        <div className="teams__header__container">
          <h1>Teams</h1>
        </div>
      </div>
      <Container className="team__footer">
        {gotSquad ? (
          squads.map(({ name, squadLogoUrl, teamId }) => {
            return (
              <div>
                <TeamComponent teamName={name} image={squadLogoUrl} id={teamId} />
              </div>
            );
          })
        ) : (
          null
        )}
      </Container>
      <Footer />
    </div>
  );
}

export default Teams;