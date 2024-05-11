import React, { useState } from 'react'
import Navegacion from '../../components/Navegacion/Index';
import { useNavigate } from "react-router-dom";
import { Space, Table, notification, Button } from 'antd';
import { Insert, update } from '../../Apis/ApiDoctor';
import { useMutation, useQueryClient, useQuery } from '@tanstack/react-query';
import FormularioDoctor from '../../components/FormularioDoctor';
import axiosClient from "../../config/axios";
import './Doctor.css';
import { Perfil } from '../../components/Perfil';




export default function Doctor() {

  const navigate = useNavigate();

  const queryClient = useQueryClient();

  const [open, setOpen] = useState(false);

  const [edit, setEdit] = useState({});
  const [editBool, setEditBool] = useState(false);

  const [api, contextHolder] = notification.useNotification();

  const [openPerfil, setOpenPerfil] = useState(false);
  const [perfil, sertPerfil] = useState({});

  

  const { isLoading, data } = useQuery({
    queryKey: ['d'],
    queryFn: async () => {
      const response = await axiosClient.api().get('/Doctor/Get');
      const data = await response.data.Result;
      return data;
    },
  })

  const mutationInsert = useMutation({
    mutationFn: Insert,
    onSuccess: (data) => {

      api.success({
        message: "Agregado correctamente",
      })
      queryClient.invalidateQueries({ queryKey: ['d'], })


    },
    onError: (error) => {
      api.error({
        message: error.message,
      })
    }
  })

  const mutationUpdate = useMutation({
    mutationFn: update,
    onSuccess: (data) => {
      api.success({
        message: "Actualizado correctamente",
      })
      queryClient.invalidateQueries({ queryKey: ['d'] })
    },
  })





  const handelEdit = (items) => {
    setEdit(items);
    setEditBool(true);
    setOpen(true);


  }
  const showDrawer = () => {
    setOpen(true);
    setEditBool(false);
  };

  const onClose = () => {
    setOpen(false);
  };

  const showDrawerPerfil = (record) => {
    setOpenPerfil(true);
    sertPerfil(record)
};
const onClosePerfil = () => {
  setOpenPerfil(false);
};



  const columns = [
    {
      title: 'Nombre',
      dataIndex: 'Nombre',
      key: 'Nombre',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Especialidad',
      dataIndex: 'Especialidad',
      key: 'Especialidad',
    },
    {
      title: 'Hospital',
      dataIndex: 'Hospital',
      key: 'Hospital',
    },

    {
      title: '',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button type='text' onClick={() => { handelEdit(record) }}>Editar</Button>
          <Button type='text' onClick={()=> { showDrawerPerfil(record)}}>Ver </Button>
        </Space>
      ),
    },
  ];



  return (
    <>
      {contextHolder}
      <Navegacion />
      <div className='View_container2'>
        <div className='Contenedor_Hero'>
          <div className='Icon'>
            <ion-icon name="fitness-outline"></ion-icon>
          </div>
          <h1>Registro de doctores</h1>
          <p>Gestiona los medicos de tu preferencias, con nuestro servicio </p>
          <br />


        </div>
        <div className='container'>
          <Button type="text" onClick={() => { navigate("/") }} >
            <div className='Icon2'>
              <ion-icon name="arrow-back-outline"></ion-icon>
            </div>
          </Button>
          <Button onClick={showDrawer} >Nuevo doctor</Button>
        </div>
        <div className='Contendor_Body  animate__animated animate__slideInUp' >

          <Table columns={columns} loading={isLoading} dataSource={data} scroll={{
            y: 400
          }} pagination={false}

          />

        </div>
      </div>
    
      <FormularioDoctor onClose={onClose} open={open} mutationInsert={mutationInsert} mutationUpdate={mutationUpdate} edit={edit} editBool={editBool} />
    
      <Perfil onClosePerfil={onClosePerfil} openPerfil={openPerfil} perfil={perfil} />
    </>
  )
}
