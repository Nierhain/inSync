import { useQuery } from "@tanstack/react-query"

const queryKeys = {
    itemLists : "itemLists",
    userLists : "userLists",
    singleList : "singleList"
} 

export function useItemLists(){
    const {} = useQuery([queryKeys.itemLists], )
}

export function useUserLists(user: string){

}

