import { LoadingOutlined } from '@ant-design/icons';
import { useMutation, useQuery } from '@tanstack/react-query';
import { notification } from 'antd';
import { MinecraftItem } from '../models';
import { useStore } from '../store/store';
import { ItemApi } from '../utils/Api';

export function useMinecraftItems() {
    const query = useQuery(['minecraftItems'], ItemApi.loadItems);
    return {
        data: query.data?.data ?? [],
    };
}

export function uploadMinecraftItems() {
    const mutation = useMutation({
        mutationFn: (minecraftItems: MinecraftItem[]) => {
            return ItemApi.uploadItems(minecraftItems, useStore.getState().adminKey);
        },
        onMutate: () => {
            notification.info({
                message: 'Uploading Items...',
                description: 'Your list of minecraft items gets uploaded to the server. It may take a moment',
                icon: <LoadingOutlined />,
                key: 'upload_items',
            });
        },
        onSuccess: () => {
            notification.destroy('upload_items');
            notification.success({
                message: 'Upload success',
                description: 'All items were successfully uploaded to the server',
            });
        },
    });

    return {
        upload: mutation.mutate,
    };
}
