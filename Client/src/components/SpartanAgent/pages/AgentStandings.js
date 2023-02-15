import React, { useState, useEffect } from "react";
import { Form, Button, Table } from "react-bootstrap";
import SidebarAgent from "../SidebarAgent";
import axios from "axios";

function AgentStandings() {
  const [logged, setlogged] = useState(false);

  const [standings, setStandings] = useState([]);

  useEffect(() => {
    axios
      .get(`https://localhost:7122/api/Matches/GetStandings`)
      .then((response) => {
        setStandings(response.data);
      });
  }, []);

  const [leagueId, setLeagueId] = useState(false);
  const [leagues, setLeagues] = useState([]);
  useEffect(() => {
    fetch("https://localhost:7122/api/League/getLeagues", {
      method: "get",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((resp) => resp.json())
      .then((resp) => setLeagues(resp));
  }, []);

  useEffect(() => {
    var items = null;
    items = localStorage.getItem("email");
    if (items != null) {
      setlogged(true);
    }
    console.log("LOGGED???", logged);
  });

  async function initializeStandings(e) {
    e.preventDefault();

    await axios({
      method: "POST",
      url: `https://localhost:7122/api/Matches/InitializeStandings?leagueid=${leagueId}`,
    }).then(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  return (
    <div>
      {logged ? (
        <>
          <SidebarAgent />
          <div
            style={{
              position: "absolute",
              top: "60%",
              left: "55%",
              transform: "translate(-50%, -50%)",
              width: "1100px",
            }}
          >
            <Form>
              <h2>Initilaize Standigs</h2>

              <Form.Group className='mb-3' controlId='formName'>
                <Form.Label>League</Form.Label>
                <Form.Control
                  onChange={(e) => setLeagueId(e.target.value)}
                  type='number'
                  placeholder='Enter League 
                  '
                />
              </Form.Group>

              <Button
                variant='primary'
                type='submit'
                onClick={initializeStandings}
                style={{ width: "auto", backgroundColor: "#35AD79" }}
              >
                Initialize
              </Button>
            </Form>

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
          </div>
        </>
      ) : (
        <div>
          <h4>Agent Matches</h4>
          <h1>Sorry you are Unauthorized for this page </h1>
        </div>
      )}
    </div>
  );
}

export default AgentStandings;
