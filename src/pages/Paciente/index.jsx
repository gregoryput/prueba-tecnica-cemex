import React, { useState } from 'react'
import Navegacion from '../../components/Navegacion/Index';
import { useNavigate } from "react-router-dom";
import { Table, notification, Button,InputNumber, Space  } from 'antd';
import { Insert, update } from '../../Apis/ApiPaciente';
import { useMutation, useQueryClient, useQuery } from '@tanstack/react-query';
import axiosClient from "../../config/axios";
import './Paciente.css';
import FormularioPaciente from '../../components/FormularioPaciente';




export default function Paciente() {

  const navigate = useNavigate();

  const queryClient = useQueryClient();

  const [open, setOpen] = useState(false);

  const [edit, setEdit] = useState({});
  const [editBool, setEditBool] = useState(false);

  const [api, contextHolder] = notification.useNotification();





  const { isLoading, data } = useQuery({
    queryKey: ['pacien'],
    queryFn: async () => {
      const response = await axiosClient.api().get('/Paciente/Get');
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
      queryClient.invalidateQueries({ queryKey: ['pacien'], })
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
      queryClient.invalidateQueries({ queryKey: ['pacien'] })
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



  const columns = [
    {
      title: 'Nombre',
      dataIndex: 'Nombre',
      key: 'Nombre',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Edad',
      dataIndex: 'Edad',
      key: 'Edad',
    },
    {
      title: 'Genero',
      dataIndex: 'Genero',
      key: 'Genero',
    },
    {
      title: 'Direcciòn',
      dataIndex: 'Direccion',
      key: 'Direccion',
    },
    {
      title: 'Telefono',
      dataIndex: 'Telefono',
      key: 'Telefono',
    },
    {
      title: '',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button type='text' onClick={() => { handelEdit(record) }}>Editar</Button>
        </Space>
      ),
    },
  ];


  return (
    <>
      {contextHolder}
      <Navegacion />
      <div className='View_container2'>
        <div className='Contenedor_Hero2'>
          <div className='Icon'>
            <ion-icon name="person-outline" ></ion-icon>
          </div>
          <h1>Registro de pacientes</h1>
          <p>Gestiona los pacientes de tu preferencias, con nuestro servicio </p>
          <br />


        </div>
        <div className='container'>
          <Button type="text" onClick={() => { navigate("/") }} >
            <div className='Icon2'>
              <ion-icon name="arrow-back-outline"></ion-icon>
            </div>
          </Button>
          <Button onClick={showDrawer} >Nuevo paciente</Button>
        </div>
        <div className='Contendor_Body  animate__animated animate__slideInUp' >

          <Table columns={columns} loading={isLoading} dataSource={data} scroll={{
            y: 400
          }} pagination={false} />


        </div>


      </div>

      <FormularioPaciente onClose={onClose} open={open} mutationInsert={mutationInsert} mutationUpdate={mutationUpdate} edit={edit} editBool={editBool} />

    </>
  )
}
