import React, { useEffect, useState } from "react";
import { Button, Modal, Table } from "react-bootstrap";
import './teams.css'
export default function TeamDetails(props) {
  const { squadName, teamPlayers } = props;
  const [lgShow, setLgShow] = useState(false);
  const [goalKeepers, setGoalKeepers] = useState([])
  const [defenders, setDefenders] = useState([])
  const [middfielders, setMiddfielders] = useState([])
  const [attackers, setAttackers] = useState([])
  const [sortedPlayers, setSortedPlayers] = useState([])

  function sortByPosition() {
    console.log("testinggggg")
    setGoalKeepers([])
    setDefenders([])
    setMiddfielders([])
    setAttackers([])
    teamPlayers.forEach(element => {
      if (element.position == "goalkeeper") {
        setGoalKeepers(goalKeepers => [...goalKeepers, element])
      }
      else if (element.position == "defender") {
        setDefenders(defenders => [...defenders, element])
      } else if (element.position == "middfielder") {
        setMiddfielders(middfielders => [...middfielders, element])
      } else (
        setAttackers(attackers => [...attackers, element])
      )
    });
    setSortedPlayers(goalKeepers.concat(defenders, middfielders, attackers))
  }
  function openModal() {
    setLgShow(true)
    sortByPosition()
  }
  return (
    <div>
      <div>
        <Button variant="success" color="link" onClick={openModal}>
          {squadName}
        </Button>
        <Modal
          size="lg"
          show={lgShow}
          onHide={() => setLgShow(false)}
          aria-labelledby="example-modal-sizes-title-lg"
        >
          <Modal.Header closeButton>
            <Modal.Title id="example-modal-sizes-title-lg">
              {squadName}: Players
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>First Name</th>
                  <th>Last Name</th>
                  <th>Age </th>
                  <th>Number</th>
                  <th>Position</th>
                </tr>
              </thead>
              {goalKeepers.map((playerMap) => {
                return (
                  <tbody>
                    <tr>
                      <td> {playerMap.name} </td>
                      <td> {playerMap.lastName} </td>
                      <td> {playerMap.age} </td>
                      <td> {playerMap.number} </td>
                      <td style={{ backgroundColor: "#2e2e6b", color: "white" }}> {playerMap.position} </td>
                    </tr>
                  </tbody>
                );
              })}
              {defenders.map((playerMap) => {
                return (
                  <tbody>
                    <tr>
                      <td> {playerMap.name} </td>
                      <td> {playerMap.lastName} </td>
                      <td> {playerMap.age} </td>
                      <td> {playerMap.number} </td>
                      <td style={{ backgroundColor: "red", color: "white" }}> {playerMap.position} </td>
                    </tr>
                  </tbody>
                );
              })}
              {middfielders.map((playerMap) => {
                return (
                  <tbody>
                    <tr>
                      <td> {playerMap.name} </td>
                      <td> {playerMap.lastName} </td>
                      <td> {playerMap.age} </td>
                      <td> {playerMap.number} </td>
                      <td style={{ backgroundColor: "blue", color: "white" }}> {playerMap.position} </td>
                    </tr>
                  </tbody>
                );
              })}
              {attackers.map((playerMap) => {
                return (
                  <tbody>
                    <tr>
                      <td> {playerMap.name} </td>
                      <td> {playerMap.lastName} </td>
                      <td> {playerMap.age} </td>
                      <td> {playerMap.number} </td>
                      <td style={{ backgroundColor: "green", color: "white" }}> {playerMap.position} </td>
                    </tr>
                  </tbody>
                );
              })}
            </Table>
          </Modal.Body>
        </Modal>
      </div>
    </div>
  );
}
