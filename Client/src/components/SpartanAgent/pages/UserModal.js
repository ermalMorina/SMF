import React, { useState } from "react";
import { Modal, ModalHeader, ModalFooter, ModalBody } from "reactstrap";
import { Button } from "react-bootstrap";
import axios from "axios";
import { Form } from "react-bootstrap";
export default function UserModal(props) {
  const { usernameModal } = props;
  const [roles, setRoles] = useState("admin");
  const [modal, setModal] = useState(false);

  const toggle = () => setModal(!modal);
  function handleRoleChange(e) {
    e.preventDefault();
    setRoles(e.target.value);
  }

  //modal
  function sendDataToAPI(e) {
    axios
      .put("https://localhost:7122/api/User/EditUserRoles", {
        username: usernameModal,
        roles: [roles],
      })
      .then((response) => {
        window.location.reload();
      });
  }

  return (
    <div>
      <Button variant='success' onClick={toggle}>
        <i class='far fa-edit'></i>Edit
      </Button>
      <Modal isOpen={modal} toggle={toggle} style={{ width: "500px" }}>
        <ModalHeader toggle={toggle}>Change User Role</ModalHeader>
        <ModalBody>
          <Form>
            <Form.Group className='mb-3' controlId='exampleForm.ControlInput1'>
              <Form.Label>Username</Form.Label>
              <Form.Control
                type='text'
                placeholder='username'
                value={usernameModal}
                autoFocus
              />
            </Form.Group>
            <Form.Group
              className='mb-3'
              controlId='exampleForm.ControlTextarea1'
            >
              <Form.Label>Role</Form.Label>
              <form onSubmit={handleRoleChange}>
                <select name='roles' id='roles' onChange={handleRoleChange}>
                  <option value='admin'>Admin</option>
                  <option value='agent'>Agent</option>
                </select>
              </form>
            </Form.Group>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color='primary' onClick={sendDataToAPI}>
            Save
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}
