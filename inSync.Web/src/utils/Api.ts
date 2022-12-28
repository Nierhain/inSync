import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
import { Response, ItemList, ResponseType, ItemListRequest, UpdateListRequest, LockRequest } from '../models';
import { isDev } from './EnvChecks';

const logRequest = (config: AxiosRequestConfig<Response<ResponseType>>) => {
    if (isDev) {
        console.log(`HTTP Request ${config.method} ${config.url}`);
    }
    return config;
};

const logError = (error: AxiosError<Response<ResponseType>>) => {
    if (!error.response && !error.request) {
        console.log("ERROR: axios couldn't handle the request");
        return;
    }
    if (isDev) {
        console.log(
            `ERROR: Request ${error.response?.config.url} failed with StatusCode ${error.response?.data.statusCode}
            \n Cause: ${error.response?.data.errorMessage}`
        );
    }
    return error;
};

const logResponse = (response: AxiosResponse<Response<ResponseType>>) => {
    if (isDev) {
        console.log(
            `HTTP Request ${response.config.url} completed with StatusCode ${response.data.statusCode} \n Response:`
        );
        console.log(response.data.data);
    }
    return response;
};

type HeaderType = 'admin' | 'user';

const getHeader = (headerString: string, type: HeaderType) => {
    const config: AxiosRequestConfig = {
        headers: {
            adminKey: type === 'admin' ? headerString : '',
            password: type === 'user' ? headerString : '',
        },
    };
    return config;
};

const instance = axios.create({
    baseURL: '/api/',
    headers: {
        adminKey: '',
        password: '',
    },
});

instance.interceptors.request.use(logRequest, logError);
instance.interceptors.response.use(logResponse, logError);

const requests = {
    get: <T extends ResponseType>(url: string, header: string = '', type: HeaderType = 'user') =>
        instance.get<Response<ResponseType>>(url, getHeader(header, type)).then((res) => res.data),
    post: <T extends ResponseType>(url: string, data: any, header: string = '', type: HeaderType = 'user') =>
        instance.post(url, data, getHeader(header, type)).then((res) => res.data),
    put: <T extends ResponseType>(url: string, data: any, header: string = '', type: HeaderType = 'user') =>
        instance.put<Response<T>>(url, data, getHeader(header, type)).then((res) => res.data),
};

export const UserApi = {
    loadListsForUser: (username: string) => requests.get<ItemList[]>(`lists/${username}`),
    loadList: (id: string, password: string) => requests.get<ItemList>(`lists/${id}`, password),
    createList: (list: ItemListRequest) => requests.post<string>(`lists`, list),
    updateList: (list: UpdateListRequest) => requests.put<boolean>(`lists`, list),
};

export const AdminApi = {
    verifyAdminKey: (adminKey: string) => requests.get<boolean>(`admin?adminKey=${adminKey}`),
    loadLists: (adminKey: string) => requests.get<ItemList[]>('admin/lists?adminKey=' + adminKey),
    loadList: (id: string, adminKey: string) => requests.get(`admin/lists/${id}?adminKey=${adminKey}`),
    loadListsForUser: (username: string, adminKey: string) =>
        requests.get<ItemList[]>(`admin/lists/user/${username}?adminKey=${adminKey}`),
    lockList: (lock: LockRequest) => requests.put<boolean>('admin/lists/lock', lock),
};
