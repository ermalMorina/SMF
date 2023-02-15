import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Card, Container, Table } from "react-bootstrap";
import team1 from "../../images/team1.png";
import team2 from "../../images/team2.png";
import "./match.css";
import Navigation from "../Navigation/navigation";
import Footer from "../Footer/footer";

function Match() {
  return (
    <div style={{ backgroundColor: "#aaaa" }}>
      <Navigation />
      <div className="match__header">
        <div className="match__header__content">
          <h1>Game Schedule</h1>
        </div>
      </div>

      <Container className="match__footer">
        <Card className="match__container">
          <Card.Title>
            <h3>Stadiumi Prishtina</h3>
            <h5>08:30 PM</h5>
          </Card.Title>
          <Card.Body className="d-flex justify-content-between">
            <div style={{ padding: "30px" }}>
              <img src={team1} />
              <h5>Sportland</h5>
              <h6>Prishtina</h6>
            </div>
            <div className="mach__info">
              <h2>FRI 19</h2>
              <h5>May 2022</h5>
              <h3>vs</h3>
            </div>
            <div style={{ padding: "30px" }}>
              <img src={team2} />
              <h5>Real Madrid</h5>
              <h6>Istogu</h6>
            </div>
          </Card.Body>
        </Card>

        <Card className="match__container">
          <Card.Title>
            <h3>Stadiumi Prishtina</h3>
            <h5>08:30 PM</h5>
          </Card.Title>
          <Card.Body className="d-flex justify-content-between">
            <div style={{ padding: "30px" }}>
              <img src={team1} />
              <h5>Sportland</h5>
              <h6>Prishtina</h6>
            </div>
            <div className="mach__info">
              <h2>FRI 19</h2>
              <h5>May 2022</h5>
              <h3>vs</h3>
            </div>
            <div style={{ padding: "30px" }}>
              <img src={team2} />
              <h5>Real Madrid</h5>
              <h6>Istogu</h6>
            </div>
          </Card.Body>
        </Card>

        <Card className="match__container">
          <Card.Title>
            <h3>Stadiumi Prishtina</h3>
            <h5>08:30 PM</h5>
          </Card.Title>
          <Card.Body className="d-flex justify-content-between">
            <div style={{ padding: "30px" }}>
              <img src={team1} />
              <h5>Sportland</h5>
              <h6>Prishtina</h6>
            </div>
            <div className="mach__info">
              <h2>FRI 19</h2>
              <h5>May 2022</h5>
              <h3>vs</h3>
            </div>
            <div style={{ padding: "30px" }}>
              <img src={team2} />
              <h5>Real Madrid</h5>
              <h6>Istogu</h6>
            </div>
          </Card.Body>
        </Card>
      </Container>
      <Footer />
    </div>
  );
}

export default Match;
