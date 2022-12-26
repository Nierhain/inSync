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
    username: string,
    password: string,
    items: Item[]
}