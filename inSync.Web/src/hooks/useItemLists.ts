import { QueryFunction, QueryFunctionContext, QueryKey, useQuery } from '@tanstack/react-query';
import { useStore } from '../store/store';
import { AdminApi, UserApi } from '../utils/Api';
import { getAdminLists, getCurrentUserLists, getListForAdmin, getUserList, queryBuilder } from '../utils/QueryFunctions';

export const queryKeys = {
    adminList: 'itemLists',
    userList: 'userLists',
    minecraftItems: 'minecraftItems',
};

export function useAdminLists() {
    const query = queryBuilder([queryKeys.adminList], getAdminLists);

    return {
        lists: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useUserLists(user: string) {
    const query = queryBuilder([queryKeys.userList, user], getCurrentUserLists, !!user);

    return {
        lists: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useUserList(id: string, password: string) {
    const { username } = useStore();
    const query = queryBuilder([queryKeys.userList, id, password], getUserList, !!username);
    return {
        list: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}

export function useAdminList(id: string) {
    const { adminKey } = useStore();
    const query = queryBuilder([queryKeys.adminList, id], getListForAdmin, !!adminKey);

    return {
        list: query.data,
        isLoading: query.isLoading,
        isError: query.isError,
        error: query.error,
    };
}
