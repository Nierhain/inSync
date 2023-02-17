import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
import {
    Response,
    ItemList,
    ResponseType,
    ItemListRequest,
    UpdateListRequest,
    LockRequest,
    MinecraftItem,
} from '../models';
import { useStore } from '../store/store';
import { isDev } from './EnvChecks';

const shouldLog = false;

const logRequest = (config: AxiosRequestConfig<Response<ResponseType>>) => {
    if (isDev) {
        console.log(`HTTP Request ${config.method} ${config.url} with data: `, config);
    }
    return config;
};

const logError = (error: AxiosError<Response<ResponseType>>) => {
    if (!error.response && !error.request) {
        console.log("ERROR: axios couldn't handle the request");
        return;
    }
    if (isDev && shouldLog) {
        console.log(
            `ERROR: Request ${error.response?.config.url} failed with StatusCode ${error.response?.data.statusCode}
            \n Cause: ${error.response?.data.errorMessage}`
        );
    }
    return error;
};

const logResponse = (response: AxiosResponse<Response<ResponseType>>) => {
    if (isDev && shouldLog) {
        console.log(
            `HTTP Request ${response.config.url} completed with StatusCode ${response.data.statusCode} \n Response:`
        );
        console.log(response.data.data);
    }
    return response;
};

interface Header {
    adminKey?: string;
    password?: string;
}

const getHeader = ({ adminKey, password }: Header): AxiosRequestConfig => {
    console.log({adminKey, password})
    return {
        headers: {
            adminKey: adminKey,
            password: password,
        },
    };
};

const instance = axios.create({
    baseURL: 'https://localhost:8000/api',
});

instance.interceptors.request.use(logRequest, logError);
instance.interceptors.response.use(logResponse, logError);

const requests = {
    get: <T extends ResponseType>(url: string, config?: AxiosRequestConfig) => {
        console.log(config);
        return instance.get<Response<T>>(url, config).then((res) => res.data);
    },
    post: <T extends ResponseType>(url: string, data: any, config?: AxiosRequestConfig) =>
        instance.post<Response<T>>(url, data, config).then((res) => res.data),
    put: <T extends ResponseType>(url: string, data: any, config?: AxiosRequestConfig) =>
        instance.put<Response<T>>(url, data, config).then((res) => res.data),
};

export const UserApi = {
    loadListsForUser: (username: string) => requests.get<ItemList[]>(`lists?username=${username}`),
    loadList: (id: string, password: string) =>
        requests.get<ItemList>(`lists/${id}`, getHeader({ password: password })),
    updateList: (list: UpdateListRequest, password: string) =>
        requests.put<boolean>(`lists`, list, getHeader({ password: password })),
};

export const AdminApi = {
    verifyAdminKey: (adminKey: string) => requests.get<boolean>(`admin`, getHeader({ adminKey: adminKey })),
    loadLists: (adminKey: string) => requests.get<ItemList[]>('admin/lists', getHeader({ adminKey: adminKey })),
    loadList: (id: string, adminKey: string) =>
        requests.get<ItemList>(`admin/lists/${id}`, getHeader({ adminKey: adminKey })),
    loadListsForUser: (username: string, adminKey: string) =>
        requests.get<ItemList[]>(`admin/lists/user/${username}`, getHeader({ adminKey: adminKey })),
    lockList: (lock: LockRequest, adminKey: string) =>
        requests.put<boolean>('admin/lists/lock', lock, getHeader({ adminKey: adminKey })),
};

export const ItemApi = {
    loadItems: () => requests.get<MinecraftItem[]>(`items`),
    uploadItems: (items: MinecraftItem[], adminKey: string) =>
        requests.post(`items`, items, getHeader({ adminKey: adminKey })),
};

export const MetaApi = {
    getVersion: (): Promise<string> => instance.get('/meta/version').then((res) => res.data),
    getEnvironment: (): Promise<string> => instance.get('/meta/environment').then((res) => res.data),
};
