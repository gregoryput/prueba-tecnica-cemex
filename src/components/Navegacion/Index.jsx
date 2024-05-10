import React from 'react'
import "./Navegacion.css"

export default function Navegacion() {
  return (
    <div className='Navegacion'>
      <div className='User'>
        <div className='Avatar'>
          <ion-icon name="person-circle-outline"></ion-icon>
        </div>
        <p>Gregoryput</p>
      </div>

      <p>Administrador</p>

      <span className='exit'>
        <ion-icon name="log-in-outline"></ion-icon>
      </span>
    </div>
  )
}
