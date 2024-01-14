import React from "react";
import { useParams } from 'react-router-dom';

export function CreateDish() {

    const {location} = useParams();
    const adress = decodeURIComponent(location);

    return(
        <div>
            <h1>Edycja posiłku cdn</h1>
        </div>

    )
}

export default CreateDish;