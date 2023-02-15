import React, { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import ReactPaginate from "react-paginate";
import { useNavigate } from "react-router-dom";
import SidebarAgent from "../../SidebarAgent";
import "./app.css";
export default function PaginatedItems(props) {
  const navigate=useNavigate();
  const { data } = props;
  const [currentItems, setCurrentItems] = useState(null);
  const [pageCount, setPageCount] = useState(0);
  const [itemOffset, setItemOffset] = useState(0);
  const itemsPerPage = 10;
  useEffect(() => {
    console.log("data", data);
    const endOffset = itemOffset + itemsPerPage;
    setCurrentItems(data.slice(itemOffset, endOffset));
    setPageCount(Math.ceil(data.length / itemsPerPage));
  }, [itemOffset, itemsPerPage, data]);

  const handlePageClick = (event) => {
    const newOffset = (event.selected * itemsPerPage) % data.length;
    setItemOffset(newOffset);

    
  };
    function AddReferee(){
      navigate("/agent-referees")
    }
  return (
    <div>
      <SidebarAgent />
      <div
        class="table-responsive"
        style={{
          position: "absolute",
          top: "10%",
          left: "30%",
          width: "800px",
        }}
      >
        <div style={{
          display:"flex",
          flexDirection:"row",
          justifyContent:"space-between"
        }}>
          <h1>Referees</h1>
          <Button variant="dark" onClick={AddReferee}>Add referee</Button>
        </div> 
        <Table striped>
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Lastname</th>
              <th scope="col">Experience</th>
              <th scope="col">City</th>
              <th scope="col">Position</th>
            </tr>
          </thead>
          <tbody>
            {currentItems &&
              currentItems.map((item) => {
                return (
                  <tr>
                    <td> {item.name} </td>
                    <td> {item.lastName} </td>
                    <td> {item.experience} </td>
                    <td> {item.city} </td>
                    <td> {item.position}</td>
                  </tr>
                );
              })}
          </tbody>
        </Table>
        <ReactPaginate
          breakLabel="..."
          nextLabel="next >"
          onPageChange={handlePageClick}
          pageRangeDisplayed={5}
          pageCount={pageCount}
          previousLabel="< previous"
          renderOnZeroPageCount={null}
          containerClassName="pagination"
          pageLinkClassName="page-num"
          previousLinkClassName="page-num"
          nextLinkClassName="page-num"
          activeClassName="active"
        />
      </div>
    </div>
  );
}
