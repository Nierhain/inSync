import { QueryFunction, QueryFunctionContext, QueryKey, useQuery, UseQueryResult } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { ItemList, Response, ResponseType } from '../models';
import { useStore } from '../store/store';
import { AdminApi, UserApi } from '../utils/Api';
import { getAdminLists, getCurrentUserLists, getListForAdmin, getUserList } from '../utils/QueryFunctions';

export const queryKeys = {
    adminList: 'itemLists',
    userList: 'userLists',
    minecraftItems: 'minecraftItems',
};

export function useAdminLists() {
    const query = useQuery([queryKeys.adminList], getAdminLists);

    return returnBuilder(query);
}

export function useUserLists(user: string) {
    const query = useQuery([queryKeys.userList, user], getCurrentUserLists);

    return returnBuilder(query);
}

export function useUserList(id: string, password: string) {
    const query = useQuery([queryKeys.userList, id, password], getUserList);

    return returnBuilder(query);
}

export function useAdminList(id: string) {
    const query = useQuery([queryKeys.adminList, id], getListForAdmin);

    return returnBuilder(query);
}

function returnBuilder<T>(query: UseQueryResult<Response<T>>) {
    const data = query.data?.data;
    return {
        data: data!,
        isError: !data && !query.isLoading,
        isLoading: query.isLoading,
        error: query.data ? query.data.errorMessage : `Request failed`,
    };
}
