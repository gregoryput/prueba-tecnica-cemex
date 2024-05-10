


import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import ProtectedRoute from "./router/ProtectedRoute ";
import Login from './pages/Login';
import 'animate.css';
import './App.css'
import Home from "./pages/Home";
import Paciente from "./pages/Paciente";
import Doctor from "./pages/Doctor";


const router = createBrowserRouter([
  {
    path: "/",
    element: <ProtectedRoute roles={["Administrador"]}>
      <Home />
    </ProtectedRoute>,
  },
  {
    path: "/Paciente",
    element: <ProtectedRoute roles={["Administrador"]} >
      <Paciente />
    </ProtectedRoute>,
  },
  {
    path: "/Doctor",
    element: <ProtectedRoute roles={["Administrador"]} >
      <Doctor />
    </ProtectedRoute>, 
  },
  {
    path: "/Login",
    element: <Login />,
    
  },
]);


function App() {
  return <RouterProvider router={router} />
}

export default App
