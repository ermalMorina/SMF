import React, { useState, useEffect } from "react";
import { Button, Form } from "react-bootstrap";
import SidebarAgent from "../SidebarAgent";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function AgentEditLeague() {
  const navigate = useNavigate();

  const [leagueId, setLeagueId] = useState(null);
  const [leagueName, setLeagueName] = useState("");

  useEffect(() => {
    setLeagueId(localStorage.getItem("League Id"));
    setLeagueName(localStorage.getItem("League Name"));
  }, []);

  const updateLeague = (e) => {
    e.preventDefault();
    axios
      .put(`https://localhost:7122/api/League/${leagueId}`, {
        leagueId,
        leagueName,
      })
      .then(navigate("/agent-leagues"), window.location.reload());
  };

  return (
    <div>
      <SidebarAgent />
      <div
        style={{
          position: "absolute",
          top: "50%",
          left: "55%",
          transform: "translate(-50%, -50%)",
          backgroundColor: "#fff",
          padding: "20px",
          width: "800px",
        }}
      >
        <Form>
          <Form.Group className='mb-3'>
            <Form.Label>League Name</Form.Label>
            <Form.Control
              type='text'
              value={leagueName}
              onChange={(e) => setLeagueName(e.target.value)}
            />
          </Form.Group>

          <Button
            variant='success'
            type='submit'
            style={{ width: "auto" }}
            onClick={updateLeague}
          >
            Update League
          </Button>
        </Form>
      </div>
    </div>
  );
}

export default AgentEditLeague;
