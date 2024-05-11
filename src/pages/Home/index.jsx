import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";

import "./Home.css"
import Navegacion from '../../components/Navegacion/Index';

export default function Home() {
  const [Hover, setHover] = useState(false);
  const [Hover1, setHover1] = useState(false);
  const navigate = useNavigate();

  /// estilo para crear el efecto de degradado 
  const divClassName = Hover ? 'fondo2' : '';
  const divClassName1 = Hover1 ? 'fondo' : '';

  return (
    <>
      <Navegacion />
      <div className={`View_container ${divClassName} ${divClassName1}`}>
        <div className='Contenedor '>
          <div style={{ padding: 20 }}>
            <h1>Bienvendios</h1>
            <p>Gestionar doctores y pacientes </p>
          </div>

          <div className=' animate__animated animate__fadeInUp'>

            <button className='Button2' onMouseLeave={() => { setHover1(false) }} onMouseEnter={() => { setHover1(true) }} onClick={() => { navigate("/Doctor") }}>
              <span className="icons">
                <ion-icon name="fitness-outline"></ion-icon>
              </span>
              <p>Doctores</p>
            </button>

            <button className='Button' onMouseLeave={() => { setHover(false) }} onMouseEnter={() => { setHover(true) }} onClick={() => { navigate("/Paciente") }}  >
              <span className="icons">
                <ion-icon name="person-outline" ></ion-icon>
              </span>
              <p>Pacientes</p>
            </button>
            
          </div>
        </div>
      </div>
    </>
  )
}


