import { QueryFunction, QueryFunctionContext, QueryKey, useQuery } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { ItemList, Response, ResponseType } from '../models';
import { useStore } from '../store/store';
import { AdminApi, UserApi } from './Api';

export const getUserLists = ({ queryKey }: QueryFunctionContext): Promise<Response<ItemList[]>> => {
    const [_type, user] = queryKey as string[];
    return UserApi.loadListsForUser(user);
};
export const getUserList = ({ queryKey }: QueryFunctionContext): Promise<Response<ItemList>> => {
    let [_type, id, password] = queryKey as string[];
    return UserApi.loadList(id, password ?? useStore.getState().password[id]);
};
export const getCurrentUserLists = () => UserApi.loadListsForUser(useStore.getState().username);

export const getAdminLists = () => AdminApi.loadLists(useStore.getState().adminKey);

export const getListForAdmin = ({ queryKey }: QueryFunctionContext) => {
    const [_type, id] = queryKey as string[];
    return AdminApi.loadList(id, useStore.getState().adminKey);
};
