import axios from "axios";
import React, { useEffect, useState } from "react";
import { Form, Button, Table, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";
import SidebarAgent from "../SidebarAgent";
import Zoom from "react-medium-image-zoom";
import "react-medium-image-zoom/dist/styles.css";
import { saveAs } from "file-saver";

export default function AgentVerifyTeams(props) {
  const { squadLogoUrl, id, stadium, name, city, photoUrl, isVerified } = props;
  const [APIData, setAPIData] = useState([]);
  const [token, setToken] = useState("");

  useEffect(() => {
    axios.get(`https://localhost:7122/api/Squad`).then((response) => {
      setAPIData(response.data);
    });
  }, []);

  const [deleteShow, setDeleteShow] = useState(false);
  const handleDeleteClose = () => setDeleteShow(false);
  const handleDeleteShow = () => setDeleteShow(true);

  const [selectedItem, setSelectedItem] = useState({});

  function downloadImage(photoUrl) {
    saveAs(`${photoUrl}`, "image.jpg");
  }

  async function verifyTeam(teamId) {
    const t = localStorage.getItem("token");
    setToken(t);

    axios({
      method: "put",
      url: `https://localhost:7122/api/Squad/Verify/${teamId}`,
      data: {
        isVerified: true,
      },
      headers: {
        Authorization: `bearer ${t}`,
        "Content-Type": "multipart/form-data",
      },
    }).then((res) => {
      window.location.reload();
    });
  }

  async function onDelete(teamId) {
    const t = localStorage.getItem("token");
    setToken(t);

    axios({
      method: "DELETE",
      url: `https://localhost:7122/api/Squad/${teamId}`,
      headers: {
        Authorization: `bearer ${t}`,
      },
    }).then(window.location.reload());
  }

  return (
    <div>
      <SidebarAgent />

      <Modal show={deleteShow} onHide={handleDeleteClose}>
        <Modal.Header closeButton>
          <Modal.Title>Delete</Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to delete</Modal.Body>
        <Modal.Footer>
          <Button
            variant="danger"
            onClick={() => onDelete(selectedItem.teamId)}
          >
            Delete
          </Button>
        </Modal.Footer>
      </Modal>

      <div
        style={{
          position: "absolute",
          top: "10%",
          left: "25%",
          width: "1100px",
          backgroundColor: "#fff",
          padding: "20px",
        }}
      >
        <h2>Verify squads</h2>
        <hr></hr>
        <Table
          bordered
          hover
          responsive
          style={{ alignItems: "center", textAlign: "center" }}
        >
          <thead>
            <tr>
              <th>Squad logo</th>
              <th>Squad Name</th>
              <th>Squad City</th>
              <th>Certification</th>
              <th>Download certificate</th>
              <th>Verify</th>
              <th>Delete</th>
            </tr>
          </thead>

          <tbody style={{ alignItems: "center", textAlign: "center" }}>
            {APIData.map((data) => {
              return (
                <tr>
                  <td>
                    <img src={data.squadLogoUrl} width="70px" />
                  </td>
                  <td>{data.name}</td>
                  <td>{data.city}</td>
                  <td>
                    <Zoom>
                      <picture>
                        <source media="(max-width: 1000px)" srcSet={photoUrl} />
                        <img src={data.photoUrl} width="120px" height="100px" />
                      </picture>
                    </Zoom>
                  </td>
                  <td>
                    <Button
                      variant="dark"
                      onClick={() => downloadImage(data.photoUrl)}
                    >
                      Download
                    </Button>
                  </td>
                  <td>
                    {data.isVerified ? (
                      <div
                        style={{
                          borderRadius: "5px",
                          marginTop: "25px",
                          color: "#fff",
                          backgroundColor: "#aaa",
                          padding: "5px 5px",
                          width: "120px",
                          margin: "0 auto",
                        }}
                      >
                        Verified
                      </div>
                    ) : (
                      <Button
                        variant="success"
                        onClick={() => verifyTeam(data.teamId)}
                      >
                        Verify
                      </Button>
                    )}
                  </td>

                  <td>
                    <Button
                      variant="danger"
                      onClick={() => {
                        handleDeleteShow(true);
                        setSelectedItem(data);
                      }}
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>
    </div>
  );
}
