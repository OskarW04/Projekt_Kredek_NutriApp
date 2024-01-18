import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import axios from "axios";

export function CreateDish() {
    const navigate = useNavigate();
    const {location} = useParams();
    let adress = decodeURIComponent(location);
    const Id = adress;
    const token = sessionStorage.getItem('token');
    const[products, setProducts] = useState([]);


    useEffect(() => {
        const getProducts = async() =>{
                try{
                const response = await axios.get(`https://localhost:7130/api/Dish/${adress}`, {
                    headers:{
                        'Authorization': `Bearer ${token}`
                    },
                    params:{
                        "id": Id
                    }
                })
                console.log(response)
                const allProducts = response.data.dishProducts.concat(response.data.dishApiProducts)
                console.log(allProducts)
                setProducts(allProducts.map((dish) => ({name: dish.name, id: dish.id || dish.apiId})))
                console.log(products)
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
        navigate(`/AddProduct/${encodeURIComponent(adress)}`)
    }



    return(
        <div>
            <h1>Produkty:</h1>
            <ul>
            {Array.isArray(products) ? (
                products.map((product, index) => (
                <div key={index}>
                    <li>{product.name}</li>
                </div>
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