import axios, { AxiosResponse } from "axios";

axios.defaults.baseURL = "https://localhost:5001/";

const responseBody = <T>(respose: AxiosResponse<T>) => respose.data;

export const axiosService = {
  get: async <T>(url: string, params?: URLSearchParams) => {
    const respose = await axios.get<T>(url, { params });
    return responseBody(respose);
  },
  post: async <T>(url: string, body: object) => {
    const respose = await axios.post<T>(url, body);
    return responseBody(respose);
  },
  put: async <T>(url: string, body: object) => {
    const respose = await axios.put<T>(url, body);
    return responseBody(respose);
  },
  delete: async <T>(url: string) => {
    const respose = await axios.delete<T>(url);
    return responseBody(respose);
  },
};