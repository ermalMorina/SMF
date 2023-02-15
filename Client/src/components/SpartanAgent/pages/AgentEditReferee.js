import React, { useState, useEffect } from "react";
import { Button, Form } from "react-bootstrap";
import SidebarAgent from "../SidebarAgent";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function AgentEditReferee() {
  const navigate = useNavigate();

  const [id, setId] = useState(null);
  const [name, setName] = useState("");
  const [lastName, setLastName] = useState("");
  const [experience, setExperience] = useState("");
  const [city, setCity] = useState("");
  const [position, setPosition] = useState("");

  const [token, setToken] = useState("");

  useEffect(() => {
    setId(localStorage.getItem("RId"));
    setName(localStorage.getItem("RName"));
    setLastName(localStorage.getItem("RLastName"));
    setExperience(localStorage.getItem("RExperience"));
    setCity(localStorage.getItem("RCity"));
    setPosition(localStorage.getItem("RPosition"));
  }, []);

  async function updateReferee() {
    const t = localStorage.getItem("token");
    setToken(t);

    await axios({
      method: "PUT",
      url: `https://localhost:7122/api/Referees/${id}`,
      data: {
        id: id,
        name: name,
        lastName: lastName,
        experience: experience,
        city: city,
        position: position,
      },
      headers: {
        Authorization: `bearer ${t}`,
      },
    }).then(navigate("/agent-referee", window.location.reload()));
  }

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
            <Form.Label>Referee Name</Form.Label>
            <Form.Control
              type='text'
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </Form.Group>

          <Form.Group className='mb-3'>
            <Form.Label>Referee Last Name</Form.Label>
            <Form.Control
              type='text'
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
          </Form.Group>

          <Form.Group className='mb-3'>
            <Form.Label>Referee Experience</Form.Label>
            <Form.Control
              type='text'
              value={experience}
              onChange={(e) => setExperience(e.target.value)}
            />
          </Form.Group>

          <Form.Group className='mb-3'>
            <Form.Label>Referee City</Form.Label>
            <Form.Control
              type='text'
              value={city}
              onChange={(e) => setCity(e.target.value)}
            />
          </Form.Group>

          <Form.Group className='mb-3'>
            <Form.Label>Referee Position</Form.Label>
            <Form.Control
              type='text'
              value={position}
              onChange={(e) => setPosition(e.target.value)}
            />
          </Form.Group>

          <Button
            variant='success'
            type='submit'
            style={{ width: "auto" }}
            onClick={updateReferee}
          >
            Update Referee
          </Button>
        </Form>
      </div>
    </div>
  );
}

export default AgentEditReferee;
