import * as RiIcons from "react-icons/ri";
import * as AiIcons from "react-icons/ai";
import * as BiIcons from "react-icons/bi";
import * as BsIcons from "react-icons/bs";
import * as FiIcons from "react-icons/fi";
import * as MdIcon from "react-icons/md";
import * as CgIcons from "react-icons/cg";
import { IoIosPeople } from "react-icons/io";
export const AgentPages = [
  {
    title: "Home",
    path: "/agent",
    icon: <AiIcons.AiFillHome />,
    cName: "nav-text",
  },
  {
    title: "Stadium",
    path: "/agent-stadiums",
    icon: <AiIcons.AiFillFileAdd />,
    cName: "nav-text",
  },
  {
    title: "League",
    path: "/agent-leagues",
    icon: <BiIcons.BiFootball />,
    cName: "nav-text",
  },
  {
    title: "Add squads in league",
    path: "/agent-league-squads",
    icon: <BiIcons.BiFootball />,
    cName: "nav-text",
  },
  {
    title: "Verify Squads",
    path: "/agent-squads-verify",
    icon: <MdIcon.MdVerified />,
    cName: "nav-text",
  },
  {
    title: "Matches",
    path: "/agent-matches",
    icon: <BsIcons.BsFillCalendarDateFill />,
    cName: "nav-text",
  },
  {
    title: "Standings",
    path: "/agent-standings",
    icon: <BsIcons.BsFillCalendarDateFill />,
    cName: "nav-text",
  },
  {
    title: "Referees",
    path: "/agent-referee",
    icon: <FiIcons.FiCreditCard />,
    cName: "nav-text",
  },
  {
    title: "Users",
    path: "/agent-edit-users",
    icon: <IoIosPeople />,
    cName: "nav-text",
  },
  {
    title: "Games",
    path: "/agentgames",
    icon: <IoIosPeople />,
    cName: "nav-text",
  },
];
