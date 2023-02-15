import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Card, Container, Table } from "react-bootstrap";
import "./results.css";
import team1 from "../../images/team1.png";
import team2 from "../../images/team2.png";
import Navigation from "../Navigation/navigation";
import Footer from "../Footer/footer";
import axios from "axios";

function Results() {
  const [standings, setStandings] = useState([]);

  useEffect(() => {
    axios
      .get(`https://localhost:7122/api/Matches/GetStandings`)
      .then((response) => {
        setStandings(response.data);
      });
  }, []);

  return (
    <div style={{ backgroundColor: "#aaaa" }}>
      <Navigation />
      <div className='results__header'>
        <div className='results__header__content'>
          <h1>Standings</h1>
        </div>
      </div>

      <Table striped bordered size='sm' style={{ textAlign: "center" }}>
        <thead>
          <tr>
            <th>Squad Name</th>
            <th>Wins</th>
            <th>Losses</th>
            <th>Draws</th>
            <th>GS</th>
            <th>GC</th>
            <th>GD</th>
            <th>Points</th>
          </tr>
        </thead>

        <tbody>
          {standings.map((data) => {
            return (
              <>
                <tr>
                  <td>{data.squadTeamName}</td>
                  <td>{data.wins}</td>
                  <td>{data.losses}</td>
                  <td>{data.draws}</td>
                  <td>{data.goalsScored}</td>
                  <td>{data.goalsConceded}</td>
                  <td>{data.goalsDifference}</td>
                  <td>{data.points}</td>
                </tr>
              </>
            );
          })}
        </tbody>
      </Table>

      <Footer />
    </div>
  );
}

export default Results;
