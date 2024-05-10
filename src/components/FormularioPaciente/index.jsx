import React, { useState, useEffect } from 'react'
import { Button, Drawer, Space, Form, Input, Select } from 'antd';

export default function FormularioPaciente({ open, onClose, mutationInsert, edit, editBool, mutationUpdate }) {
    const [placement, setPlacement] = useState('right');
    const [form] = Form.useForm();


    const onFinish = (values) => {

        let dataJson = {
            IDPaciente: edit.IDPaciente,
            Nombre: values.Nombre,
            Edad: values.Edad,
            Genero: values.Genero,
            Direccion: values.Direccion,
            Telefono: values.Telefono,
        }

        if (editBool) {
            mutationUpdate.mutate(dataJson);
        } else {
            mutationInsert.mutate(values);
        }

        handleReset();

    };
    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);

    };

    const handleSubmit = () => {
        form
            .validateFields()
            .then(onFinish)
            .catch((errorInfo) => {
                console.log('Falló la validación del formulario:', errorInfo);
            });


    };

    const handleReset = () => {
        form.resetFields();
        onClose();
    };

    useEffect(() => {
        if (editBool) {
            form.setFieldsValue({
                Nombre: edit.Nombre,
                Edad: edit.Edad,
                Genero: edit.Genero,
                Direccion: edit.Direccion,
                Telefono: edit.Telefono,
                

            })
        }
    }, [editBool, edit])


    useEffect(() => {
        if (!editBool) {
            form.resetFields();
        }
    }, [editBool])


    return (
        <>
            <div>
                <Drawer
                    title={editBool === false ? 'Nuevo Paciente' : 'Editar Paciente'}
                    placement={placement}
                    width={500}
                    onClose={onClose}
                    open={open}
                    extra={
                        <Space>
                            <Button onClick={handleReset}>Cancel</Button>
                            <Button type="primary" onClick={handleSubmit}>
                                Crear
                            </Button>
                        </Space>
                    }
                >
                    <Form
                        form={form}
                        name="basic"
                        labelCol={{
                            span: 8,
                        }}
                        layout='vertical'
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        initialValues={{
                            remember: true,
                        }}
                    >
                        <Form.Item
                            label="Nombre"
                            name="Nombre"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Nombre!',
                                },
                                {
                                    max: 30,
                                    message: 'maximo 30 caracteres!',
                                },

                            ]}
                        >
                            <Input />
                        </Form.Item>

                        <Form.Item
                            label="Edad"
                            name="Edad"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Edad!',
                                },
                               
                                {
                                    pattern: /^[0-9]*$/,
                                    message: 'Por favor, ingresa solo números!',
                                },

                            ]}
                        >
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Genero"
                            name="Genero"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Genero!',
                                },


                            ]}
                        >
                            <Select
                                
                               
                                options={[
                                    {
                                        value: "Masculino",
                                        label: "Masculino",
                                    },
                                    {
                                        value: "Femenino",
                                        label: "Femenino",
                                    },
                                    {
                                        value: "Otro",
                                        label: "Otro",
                                    },
                                   
                                ]}
                            />
                        </Form.Item>
                        <Form.Item
                            label="Direccion"
                            name="Direccion"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Direccion!',
                                },
                                {
                                    max: 80,
                                    message: 'maximo 80 caracteres!',
                                },

                            ]}
                        >
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Telefono"
                            name="Telefono"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Telefono!',
                                },
                                {
                                    max: 12,
                                    message: 'maximo 20 caracteres!',
                                },
                                {
                                    pattern: /^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/,
                                    message: 'Por favor, ingresa un número de teléfono válido!',
                                },

                            ]}
                        >
                            <Input />
                        </Form.Item>

                    </Form>

                </Drawer>
            </div>
        </>
    )
}
