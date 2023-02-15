import * as RiIcons from "react-icons/ri"
import * as AiIcons from "react-icons/ai"
import * as BiIcons from "react-icons/bi"
import * as BsIcons from "react-icons/bs"
import * as FiIcons from "react-icons/fi"
import * as CgIcons from "react-icons/cg"
import * as MdIcon from "react-icons/md"
import { IoIosPeople } from "react-icons/io";



export const AdminPages = [
    {
        title: 'Home',
        path: '/admin',
        icon: <AiIcons.AiFillHome />,
        cName: 'nav-text'
    },
    {
        title: 'Add Squad',
        path: '/admin-squad',
        icon: <AiIcons.AiFillFileAdd />,
        cName: 'nav-text'
    },
    {
        title: 'Add Players',
        path: '/admin-addPlayer',
        icon: <AiIcons.AiFillEdit />,
        cName: 'nav-text'
    }
    
]