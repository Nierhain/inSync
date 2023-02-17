import ItemListOverviewTable from '../components/ItemListOverviewTable';
import { useAdminLists } from '../hooks';

export default function () {
    const { data, isLoading } = useAdminLists();

    return (
        <>
            <ItemListOverviewTable
                data={data}
                type="admin"
            />
        </>
    );
}
