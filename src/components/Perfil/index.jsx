import React, { useEffect, useState } from 'react';
import { Button, Col, Divider, Drawer, Space, Row, Table, notification } from 'antd';
import './Perfil.css'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import axiosClient from "../../config/axios";
import { AsignarPaciente } from '../../Apis/ApiDoctor'


const DescriptionItem = ({ title, content }) => (
    <div className="site-description-item-profile-wrapper">
        <p className="site-description-item-profile-p-label">{title}:</p>
        {content}
    </div>
);


export const Perfil = ({ openPerfil, onClosePerfil, perfil }) => {
    const [api, contextHolder] = notification.useNotification();

    const [activo, setActivo] = useState(false);
    const queryClient = useQueryClient();

    const { isLoading, data, refetch } = useQuery({
        queryKey: ['registrado', { IDDoctor: perfil.IDDoctor }],
        queryFn: async ({ queryKey }) => {
            const [key, params] = queryKey;
            const response = await axiosClient.api().get(`/Doctor/GetPacientesByDoctor?IDDoctor=${params.IDDoctor}`);
            const data = await response.data.Result;
            return data;
        },
    });


    const { isLoading: isLoadingRegistre, data: dataRegistre, refetch: actualizarLista } = useQuery({
        queryKey: ['noRegistrado', { IDDoctor: perfil.IDDoctor }],
        queryFn: async ({ queryKey }) => {
            const [key, params] = queryKey;
            const response = await axiosClient.api().get(`/Doctor/GetPacienteNotRegistrado?IDDoctor=${params.IDDoctor}`);
            const data = await response.data.Result;
            return data;
        },
    });


    const mutationAsignarPaciente = useMutation({
        mutationFn: (data) => AsignarPaciente(data),
        onSuccess: (data) => {
            api.success({
                message: "Asignado correctamente",
            });
            queryClient.invalidateQueries({ queryKey: ['registrado'] });
        },

    });


    const AgregarPaciente = (IDPaciente, IDDoctor) => {
        let data = { IDPaciente, IDDoctor };
        mutationAsignarPaciente.mutate(data);
        onClosePerfil();

    };

    useEffect(() => {
        refetch();
        actualizarLista();
    }, [AgregarPaciente])



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
            title: 'Direccion',
            dataIndex: 'Direccion',
            key: 'Direccion',
        },
        {
            title: 'Telefono',
            dataIndex: 'Telefono',
            key: 'Telefono',
        },
    ];

    const columns2 = [
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
            title: 'Direccion',
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
                    <Button type='text' onClick={() => { AgregarPaciente(record.IDPaciente, perfil.IDDoctor) }}>Agregar</Button>
                </Space>
            ),
        },
    ];



    return (
        <>
            {contextHolder}

            <Drawer width={840} placement="right" closable={false} onClose={onClosePerfil} open={openPerfil} >
                <p
                    className="site-description-item-profile-p"
                    style={{
                        marginBottom: 24,
                    }}
                >
                    Perfil Informativo
                </p>
                <p className="site-description-item-profile-p">Doctor</p>
                <Row>
                    <Col span={12}>
                        <DescriptionItem title="Nombre" content={perfil.Nombre} />
                    </Col>
                    <Col span={12}>
                        <DescriptionItem title="Especialidad" content={perfil.Especialidad} />
                    </Col>
                </Row>
                <Row>
                    <Col span={12}>
                        <DescriptionItem title="Hospital" content={perfil.Hospital} />
                    </Col>

                </Row>

                <Divider />
                <p
                    className="site-description-item-profile-p"
                    style={{
                        marginBottom: 24,
                    }}
                >
                    Pacientes
                </p>
                <Table columns={columns} loading={isLoading} dataSource={data} scroll={{
                    y: 200
                }} pagination={false} />
                <Divider />
                <Button type="text" onClick={() => { setActivo(!activo) }}>
                    {
                        activo == false ? "Asignar pacientes" : "Cancelar"
                    }
                </Button>
                <br />
                <Divider />
                {
                    activo === true ? <>

                        <Table columns={columns2} loading={isLoadingRegistre} dataSource={dataRegistre} scroll={{
                            y: 200
                        }} pagination={false} />
                    </> : null
                }
            </Drawer>
        </>
    );
};