import React from "react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";

export function AddDish(){

    const apiUrl = import.meta.env.VITE_API_URL;

    const navigate = useNavigate();
    const form = useForm();
    const { register, control, handleSubmit } = form;
    const mealId = useParams();


    const onSubmit = async(data) => {
        const json = JSON.stringify(data);
        const token = sessionStorage.getItem('token');
        try{
            const response = await axios.post(`${apiUrl}/api/Dish`, json, {
                headers:{
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                    }
            })
            let location = response.headers["location"]
            location = location.substring(10);
            navigate(`/CreateDish/${encodeURIComponent(location)}`)
        }
        catch(error){
            console.log(error)
        }

    }

    return(
        <div>
            <h1>Dodaj Posiłek:</h1>
                <form onSubmit={handleSubmit(onSubmit) }>
                    <label htmlFor="name">Nazwa posiłku</label>
                    <input type="text" id="name" {...register("name")} />

                    <label htmlFor="description">Opis</label>
                    <input type="text" id="description" placeholder="(opcjonalne)" {...register("description")} />

                    <button>Submit</button>
                </form>
      </div>
    )
}
export default AddDish;