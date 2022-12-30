import { Dayjs } from "dayjs";

export interface ItemList {
    id: string;
    items: Item[];
    createdAt: Dayjs;
    username: string;
    isActive: boolean;
    title: string,
    description: string,
    isLockedByAdmin: boolean,
    lockReason: string
}

export interface Item {
    resourceKey: string;
    amount: number;
}

export interface ItemListRequest {
    username: string;
    password: string;
    items: Item[];
}

export interface UpdateListRequest {
    id: string
    itemList: ItemList;
}

export interface LockRequest {
    id: string;
    adminKey: string;
    reason: string;
    isLocked: boolean;
}

export interface Response<T> {
    data: T;
    statusCode: number;
    errorMessage: string;
}

export interface MinecraftItem {
    resourceKey: string;
    displayName: string;
}

export type ResponseType =
    | Item
    | Item[]
    | ItemList
    | ItemList[]
    | ItemListRequest
    | string
    | boolean
    | MinecraftItem
    | MinecraftItem[];
