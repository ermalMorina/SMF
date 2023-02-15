import React, {useState, useEffect} from 'react'
import SidebarAdmin from '../SidebarAdmin'; 
import axios from 'axios';
import TeamAdminDetails from './TeamAdminDetails';
function AdminHome() {
    const [squadDetails, setSquadDetails] = useState([])
    useEffect(() => {
        var userid = localStorage.getItem('userId')
      axios.get(`https://localhost:7122/Squad/AdminTeamDetails/${userid}`)
      .then((response)=>{
        setSquadDetails(response.data)
        localStorage.setItem('teamId', response.data.teamId)
        console.log("reponse ec1cc",response)
      })
    }, [])
    
    return (
        <div>
            <SidebarAdmin />
            <div style={{
                position: 'absolute',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)'
            }}>
                <h3>Admin Home</h3> 
                <TeamAdminDetails city={squadDetails.city} 
                                  name={squadDetails.name} 
                                  photoUrl={squadDetails.photoUrl} 
                                  squadLogoUrl={squadDetails.squadLogoUrl}
                                  stadiumId={squadDetails.stadiumId} />
            </div>
        </div>
    )
}

export default AdminHome