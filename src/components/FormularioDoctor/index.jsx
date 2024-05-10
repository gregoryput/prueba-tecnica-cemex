import React, { useState, useEffect } from 'react'
import { Button, Drawer, Space, Form, Input, Spin } from 'antd';

export default function FormularioDoctor({ open, onClose, mutationInsert, edit, editBool, mutationUpdate }) {
    const [placement, setPlacement] = useState('right');
    const [form] = Form.useForm();


    const onFinish = (values) => {

        let dataJson = {
            IDdoctor: edit.IDDoctor,
            Nombre: values.Nombre,
            Especialidad: values.Especialidad,
            Hospital: values.Hospital,
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
                Especialidad: edit.Especialidad,
                Hospital: edit.Hospital,

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
                    title={editBool === false ? 'Nuevo Doctor' : 'Editar Doctor'}
                    placement={placement}
                    width={500}
                    loading={mutationInsert.loading}
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
                            label="Especialidad"
                            name="Especialidad"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Especialidad!',
                                },
                                {
                                    max: 50,
                                    message: 'maximo 50 caracteres!',
                                },

                            ]}
                        >
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Hospital"
                            name="Hospital"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor escribir Hospital!',
                                },
                                {
                                    max: 80,
                                    message: 'maximo 80 caracteres!',
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
