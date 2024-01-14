import React from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export function AddDish(){
    const navigate = useNavigate();
    const form = useForm();
    const { register, control, handleSubmit } = form;

    const onSubmit = async(data) => {
        const json = JSON.stringify(data);
        const token = localStorage.getItem('token');
        try{
            const response = await axios.post("https://localhost:7130/api/Dish", json, {
                headers:{
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                    }
            })
            const location = response.headers["location"]
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
                    <input type="text" id="description" {...register("description")} />

                    <button>Submit</button>
                </form>
      </div>
    )
}
export default AddDish;