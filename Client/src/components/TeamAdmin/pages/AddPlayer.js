import React, { useState } from 'react'
import SidebarAdmin from '../SidebarAdmin'
import axios from 'axios';
import { Form, Button } from 'react-bootstrap';

export default function AddPlayer() {
  const [name, setName] = useState("")
  const [lastname, setLastName] = useState("")
  const [age, setAge] = useState(0)
  const [number, setNumber] = useState(0)
  const [position, setPosition] = useState("")
  const teamId = localStorage.getItem('teamId')

  async function addPlayerToTeam(e){
    const t = localStorage.getItem('token')
    console.log("tttttttttt", t)
    e.preventDefault()
    await axios({
      method: "POST",
      url: "https://localhost:7122/api/PlayerOperations/addPlayer",
      data: { 
          name: name,
          lastName: lastname,
          age: age,
          number: number,
          position: position,
          suqadTeamId: teamId 
      },
      headers: {
        Authorization: `bearer ${t}`, 
      }
    }).then(
      (response) => {
        console.log("response Add Player", response); 
      },
      (error) => {
        console.log("error add player ", error); 
      }
    );
    window.location.reload()
  }

  return (
    <div>
      <SidebarAdmin />
      <div style={{
        position: "absolute",
        top: '50%',
        left: '55%',
        width: '700px',
        backgroundColor: "#fff",
        padding: "20px",
        transform: "translate(-50%, -50%)"
      }}>
{/* {
  "name": "ermal",
  "lastName": "morinaa",
  "age": 21,
  "number": 24,
  "position": "Goalkeeper",
  "suqadTeamId": 110666
} */}
        <Form>
          <h2>Register Squad</h2>
          <hr></hr>
          <Form.Group className="mb-3" controlId="formName">
            <Form.Label>Name</Form.Label>
            <Form.Control onChange={(e) => setName(e.target.value)} type="text" placeholder="Enter name of the player" /> 
          </Form.Group>
          <Form.Group className="mb-3" controlId="formName">
            <Form.Label>Lastname</Form.Label>
            <Form.Control onChange={(e) => setLastName(e.target.value)} type="text" placeholder="Enter lastname of the player" /> 
          </Form.Group>
          <Form.Group className="mb-3" controlId="formName">
            <Form.Label>Age</Form.Label>
            <Form.Control onChange={(e) => setAge(e.target.value)} type="number" placeholder="Enter age of the player" /> 
          </Form.Group>
          <Form.Group className="mb-3" controlId="formName">
            <Form.Label>Player number</Form.Label>
            <Form.Control onChange={(e) => setNumber(e.target.value)} type="number" placeholder="Enter number of the player" /> 
          </Form.Group>
          <Form.Group className="mb-3" controlId="formName">
            <Form.Label>Position</Form.Label>
            <Form.Control onChange={(e) => setPosition(e.target.value)} type="text" placeholder="Enter position of the player" /> 
          </Form.Group>

          
          <Button variant="primary" type="submit" onClick={addPlayerToTeam} style={{ width: "auto", backgroundColor: "#35AD79" }}>
            Register Squad
          </Button>
        </Form>
      </div>
    </div>
  )
}
