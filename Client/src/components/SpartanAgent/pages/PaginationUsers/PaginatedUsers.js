import React, { useEffect, useState } from "react";
import axios from "axios";
import ReactPaginate from "react-paginate";
import SidebarAgent from "../../SidebarAgent";
import { Modal, Button, Table } from "react-bootstrap";
import UserModal from "../UserModal";

export default function PaginatedUsers(props) {
  const { data } = props;
  const [id, setID] = useState(null);

  const [currentItems, setCurrentItems] = useState(null);

  const [pageCount, setPageCount] = useState(0);
  const [itemOffset, setItemOffset] = useState(0);
  const itemsPerPage = 5;
  const apiEndPoint = "https://localhost:7122/api/User";

  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [selectedItem, setSelectedItem] = useState({});

  useEffect(() => {
    const endOffset = itemOffset + itemsPerPage;
    setCurrentItems(data.slice(itemOffset, endOffset));
    setPageCount(Math.ceil(data.length / itemsPerPage));
  }, [itemOffset, itemsPerPage, data]);

  const handlePageClick = (event) => {
    const newOffset = (event.selected * itemsPerPage) % data.length;
    setItemOffset(newOffset);
  };

  const onDelete = (userId) => {
    axios.delete(`https://localhost:7122/api/User/Delete/${userId}`).then(
      setInterval(function () {
        window.location.reload();
      }, 500)
    );
  };

  return (
    <div>
      <>
        <div>
          <SidebarAgent />

          <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
              <Modal.Title>Delete</Modal.Title>
            </Modal.Header>
            <Modal.Body>Do u really want to delete this User?</Modal.Body>
            <Modal.Footer>
              <Button
                variant='danger'
                onClick={() => onDelete(selectedItem.userId)}
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
              padding: "30px",
            }}
          >
            <Table striped bordered size='sm' style={{ textAlign: "center" }}>
              <thead>
                <tr>
                  <th width={"20%"}>Username</th>
                  <th width={"20%"}>Email</th>
                  <th width={"20%"}>Identity Number</th>
                  <th width={"20%"}>Role</th>
                  <th width={"10%"}>Update</th>
                  <th width={"10%"}>Delete</th>
                </tr>
              </thead>
              <tbody>
                {currentItems &&
                  currentItems.map((post) => (
                    <tr>
                      <td width={"20%"}> {post.username} </td>
                      <td width={"20%"}> {post.email} </td>
                      <td width={"20%"}> {post.identityNumber} </td>
                      <td width={"20%"}> {post.roleName} </td>
                      <td width={"10%"}>
                        <UserModal usernameModal={post.username} />
                      </td>
                      <td width={"10%"}>
                        <Button
                          variant='dark'
                          onClick={() => {
                            handleShow(true);
                            setSelectedItem(post);
                          }}
                        >
                          Delete
                        </Button>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </Table>
            <ReactPaginate
              breakLabel='...'
              nextLabel='next >'
              onPageChange={handlePageClick}
              pageRangeDisplayed={5}
              pageCount={pageCount}
              previousLabel='< previous'
              renderOnZeroPageCount={null}
              containerClassName='pagination'
              pageLinkClassName='page-num'
              previousLinkClassName='page-num'
              nextLinkClassName='page-num'
              activeClassName='active'
            />
          </div>
        </div>
      </>
    </div>
  );
}
