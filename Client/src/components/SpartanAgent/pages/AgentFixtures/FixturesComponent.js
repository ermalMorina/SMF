import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table, Spinner } from "react-bootstrap";
export default function FixturesComponent() {
  const [calling, setcalling] = useState(false);
  const [datas, setdatas] = useState([]);
  useEffect(() => {
    setcalling(true);
    console.log("CALLING");
    axios.get("https://localhost:7122/api/Matches/GetMatches").then((resp) => {
      setdatas(resp.data);
      setcalling(false);
      console.log("resp", resp);
    });
  }, []);
  return (
    <div style={{
        width: "1100px",
        position: "absolute",
        top: "20%",
        left: "22.5%",
        backgrouColor: "white"
    }}>
      <Table striped bordered size="sm" style={{ textAlign: "center" }}>
        <thead>
          <tr>
            <th>Team Home</th>
            <th>Team Away</th>
            <th>Date</th>
            <th>Match Week</th>
          </tr>
        </thead>

        {datas.map((game) => {
          return (
            <tbody>
              <td>{game.nameOne}</td>
              <td>{game.nameTwo}</td>
              <td>{game.date}</td>
              <td>{game.matchWeek}</td>
            </tbody>
          );
        })}
      </Table>
      {calling ? (
        <div>
          <Spinner animation="grow" />
          <h3>Please wait...</h3>
        </div>
      ) : null}
    </div>
  );
}
