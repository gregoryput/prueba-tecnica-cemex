import axiosClient from "../config/axios";

export const update = async (data) => {
    const { data: response } = await axiosClient.api().put('/Doctor/Update', data);
    return response;
};


export const Insert = async (data) => {
    const { data: response } = await axiosClient.api().post('/Doctor/Insert', data);
    return response;
};

 // Definición de la función para la asignación de pacientes
 export const AsignarPaciente = async ({ IDPaciente, IDDoctor }) => {
    const data = { IDPaciente, IDDoctor };
    const response = await axiosClient.api().post(`/Doctor/AsignarPaciente?IDPaciente=${data.IDPaciente}&IDDoctor=${data.IDDoctor}`);
    return response.data; // Suponiendo que la API devuelve algún dato útil
};

// export const Delete = async (id) => {
//     const { data: response } = await axiosClient.api().delete(`/Doctor/Delete?IDdoctor=${id}`);
//     return response;
// };


