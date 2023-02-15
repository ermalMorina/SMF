import React, { useState, useEffect } from 'react'
import { Card } from "react-bootstrap";
import TeamDetails from './TeamDetails';
import axios from 'axios';
export default function TeamComponent(props) {
  const { image, teamName, id } = props
  const [players, setPlayers] = useState([])
  useEffect(() => {
    axios.get(`https://localhost:7122/PlayerOperations/GetPlayersOfSquad/${id}`).then((res) => {
      console.log("1111111a", res.data);
      setPlayers(res.data)
      console.log("players", players)
    });
  }, []);
  return (
    <div>
      <Card

        className="team__card"
        style={{ width: "300px", alignItems: "center", backgroundColor: "transparent", border: "none" }}
      >
        <img style={{ width: "100%" }} src={image} width="200px" />
        <Card.Body>
          <TeamDetails squadName={teamName} teamPlayers={players} />
        </Card.Body>
      </Card>
    </div>
  )
}
