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
                const allProducts = response.data.dishProducts.concat(response.data.dishApiProducts)
                setProducts(allProducts.map((dish) => ({name: dish.name, id: dish.id || dish.apiId, brand: dish.brand, calories: dish.calories, carbohydrates: dish.carbohydrates,
                    fats: dish.fats, proteins: dish.proteins, gramsInPortion: dish.gramsInPortion})))
            }catch(error)
            {
                console.error(error);
            }
        }
        getProducts();
    },[])
    
    const handleDeleteProduct = async(productId) => {
        try{
            await axios.delete(`https://localhost:7130/api/Dish/${Id}/removeProduct?productId=${productId}`, {
                headers:{
                    'Authorization': `Bearer ${token}`
                }
            })
            window.location.reload();
        }catch(error){
            console.error(error)
        }
    }

    const handleAddFromDB = () => {
        navigate(`/Search/${encodeURIComponent(adress)}`)
    }

    const handleAddNew = () => {
        navigate(`/AddProduct/${encodeURIComponent(adress)}`)
    }


    console.log(products)
    return(
        <div>
            <h1>Produkty:</h1>
            <ul>
            {Array.isArray(products) ? (
                products.map((product, index) => (
                <div className="dishProducts" key={index}>
                    <li><strong>{product.name}</strong></li>
                    <li><i>Marka: {product.brand !== null ? (product.brand) : ("Brak")}</i></li>
                    <li>Calories: {product.calories}</li>
                    <li><small>Proteins: {product.proteins}, Carbs: {product.carbohydrates}, Fats: {product.fats}</small></li>
                    <li>Portion: {product.gramsInPortion} g</li>
                    <button className="dishButton" onClick={() => handleDeleteProduct(product.id)}>Usuń</button>
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