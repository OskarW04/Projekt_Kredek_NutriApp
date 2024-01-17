import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import axios from "axios";
import { useForm, Controller } from "react-hook-form";

export function CreateDish() {
    const form = useForm();
    const { register, control, handleSubmit, formState } = form;
    const {errors} = formState;
    const navigate = useNavigate();
    const {location} = useParams();
    const adress = decodeURIComponent(location);
    const Id = adress.substring(10);
    const token = sessionStorage.getItem('token');
    const[products, setProducts] = useState([]);


    useEffect(() => {
        const getProducts = async() =>{
                try{
                const response = await axios.get(`https://localhost:7130${adress}`, {
                    headers:{
                        'Authorization': `Bearer ${token}`
                    },
                    params:{
                        "id": Id
                    }
                })
                console.log(response)
                setProducts(response.data.dishApiProducts.map((dish) => ({name: dish.name})))
            }catch(error)
            {
                console.error(error);
            }
        }
        getProducts();
    },[])
    
    const handleAddFromDB = () => {
        navigate(`/Search/${encodeURIComponent(adress)}`)
    }

    const handleAddNew = () => {
        navigate('/AddProduct')
    }



    return(
        <div>
            <h1>Produkty:</h1>
            <ul>
            {Array.isArray(products) ? (
                products.map((product, index) => (
                <>
                    <li>{product.name}</li>
                </>
                ))
            ) : (
                <li>Brak produktów</li>
            )}
            </ul>
            <h3>Dodaj produkty:</h3>
            <button onClick={handleAddFromDB}>Dodaj z bazy danych</button>
            <button onClick={handleAddNew}>Dodaj własny</button>
            <button onClick={() => navigate("/MealPlan")}>Wróć</button>
        </div>

    )
}

export default CreateDish;