import { Link } from "@tanstack/react-router";
import { Button } from "antd";

export default function () {
    return (
        <>
            <p>Lists</p>
            <Link to="/lists/$id" params={{id: "123asd"}}>To Single List</Link>
        </>
    );
}
