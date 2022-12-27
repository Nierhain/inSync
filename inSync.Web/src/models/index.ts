export interface ItemList {
    id: string;
    items: Item[];
    username: string;
    isActive: boolean;
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
    username: string;
    password: string;
    itemList: ItemList;
}

export interface LockRequest {
    id: string;
    adminKey: string;
    reason: string;
    isLocked: boolean;
}

export interface Response<T extends ResponseType> {
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
