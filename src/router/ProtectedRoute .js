import React ,{useEffect} from "react";
import { useNavigate } from "react-router-dom";


const ProtectedRoute = ({ children, roles }) => {
    // const token = localStorage.getItem("token");
    // const userRol = getRolesByToken(token);

    const navigate = useNavigate();
    
    useEffect(() => {
        if (!(roles.includes("Administrador"))) {
            return navigate('/Login');;
        }

    }, [])
    
    return children;
};

export default ProtectedRoute;
