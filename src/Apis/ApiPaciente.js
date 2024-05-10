import axiosClient from "../config/axios";

export const update = async (data) => {
    const { data: response } = await axiosClient.api().put('/Paciente/Update', data);
    return response;
};


export const Insert = async (data) => {
    const { data: response } = await axiosClient.api().post('/Paciente/Insert', data);
    return response;
};



// export const Delete = async (id) => {
//     const { data: response } = await axiosClient.api().delete(`/Doctor/Delete?IDdoctor=${id}`);
//     return response;
// };


