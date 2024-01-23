import React, {useEffect, useState} from "react";
import {useForm} from "react-hook-form"
import { useNavigate, useParams } from 'react-router-dom';
import axios from "axios";


export function AddProduct() {

    const apiUrl = import.meta.env.VITE_API_URL;

    const form = useForm();
    const { register, handleSubmit, formState } = form;
    const {errors} = formState;
    const token = sessionStorage.getItem('token');
    const [pageNumber, setPageNumber] = useState(1);
    const [userProducts, setUserProducts] = useState([]);
    const [pages, setPages] = useState();
    const {adress} = useParams();
    const dishId = decodeURIComponent(adress);
    const navigate = useNavigate();
    const onSubmit = async(data) => {

        const json = JSON.stringify(data, (key, value) => {
            if (!isNaN(value) && value !== '') {
                return parseInt(value, 10); 
              }
              return value;
        }) 

        try{
        const saveProduct = await axios.post(`${apiUrl}/api/Product`, json, {
            headers:{
                'Authorization': `Bearer ${token}`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        window.location.reload();
        }
        catch(error)
        {
            console.error(error)
        }
        
    }
    
    useEffect(() => {
        const getProducts = async() =>{
            try{
            const response = await axios.get(`${apiUrl}/api/Product/userProducts?PageNumber=${pageNumber}&PageSize=3`, {
                headers:{
                    'Authorization': `Bearer ${token}`
                }
            })
            setPages(response.data.totalPages)
            setUserProducts(response.data.items.map((item) => ({id: item.id, name: item.name, brand: item.brand, calories: item.calories, carbohydrates: item.carbohydrates,
                                                     fats: item.fats, gramsInPortion: item.gramsInPortion, ignredients: item.ingredients, proteins: item.proteins})))
            }catch(error)
            {
                console.error(error);
            }
        }
        getProducts();
    }, [pageNumber, token])

    const handleNextPage = () => {
        setPageNumber(pageNumber+1);
    }
    

    const handlePrevPage = () => {
        setPageNumber(pageNumber-1);
    }

    const handleDeleteProduct = async(productId) => {
        try{
            const response = axios.delete(`${apiUrl}/api/Product/${productId}`, {
                headers:{
                    'Authorization': `Bearer ${token}`
                }
            })
            window.location.reload();
        }
        catch(error)
        {
            console.error(error);
        }
    }

    const handleAddProduct = async(productId, grams) => {
        try{
            const response = await axios.put(`${apiUrl}/api/Dish/${dishId}/addProduct?productId=${productId}&grams=${grams}`,null,{
                headers:{
                    'Authorization': `Bearer ${token}`,
                }
            })
            navigate(`/CreateDish/${encodeURIComponent(adress)}`)
        }
        catch(error)
        {
            console.error(error);
        }
    }

    return(
        <div className="wrapper">
        <div className="productCreate">
        <h1>Stwórz własny produkt</h1>
        <form className="addProductForm" onSubmit={handleSubmit(onSubmit) }>

                <div className="form-control">
                    <label htmlFor="name">Nazwa</label>
                    <input type="text" id="name" {...register("name", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                    })} />
                    <p className="error">{errors.name?.message}</p>
                </div>
                
                <div className="form-control">
                    <label htmlFor="brand">Marka</label>
                    <input type="text" id="brand" {...register("brand", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                        
                    } )} />
                    <p className="error">{errors.brand?.message}</p>
                </div>

                <div className="form-control">
                    <label htmlFor="calories">Kalorie</label>
                    <input type="number" id="calories" {...register("calories", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                       
                        min: {
                            value: 0,
                            message: "Niepoprawny format"
                        },
                        
                        
                    })} />
                    <p className="error">{errors.calories?.message}</p>
                </div>

                <div className="form-control">
                <label htmlFor="proteins">Białka</label>
                <input type="number" id="proteins" {...register("proteins",{
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                        min: {
                            value: 0,
                            message: "Niepoprawny format"
                        },
                    })} />
                    <p className="error">{errors.proteins?.message}</p>
                </div>

                <div className="form-control">
                <label htmlFor="carbohydrates">Węglowodany</label>
                <input type="number" id="carbohydrates" {...register("carbohydrates", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                        min: {
                            value: 1,
                            message: "Niepoprawny format"
                        },
                    })} />
                    <p className="error">{errors.carbohydrates?.message}</p>
                </div>
                <div className="form-control">
                <label htmlFor="fats">Tłuszcze</label>
                <input type="number" id="fats" {...register("fats", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                        min: {
                            value: 1,
                            message: "Niepoprawny format"
                        },
                    })} />
                    <p className="error">{errors.fats?.message}</p>
                </div>
                <div className="form-control">
                <label htmlFor="ingredients">Składniki</label>
                <input type="text" id="ingredients" placeholder="(Opcjonalne)" {...register("ingredients")} />
                    <p className="error">{errors.ingredients?.message}</p>
                </div>
                <div className="form-control">
                <label htmlFor="gramsInPortion">Gramy na porcję</label>
                <input type="number" id="gramsInPortion" {...register("gramsInPortion", {
                        required:{
                            value: true,
                            message: "Pole jest wymagane",
                        },
                        min: {
                            value: 1,
                            message: "Niepoprawny format"
                        },
                    })} />
                    <p className="error">{errors.gramsInPortion?.message}</p>
                </div>
                <button className="addProductButton">Submit</button>
            </form>
        </div>
        <div className="productCreate2">
            <h1>Lista twoich produktów:</h1>
            <ul>
            {(userProducts.length !== 0) ? (
                userProducts.map((item) => (
                    <div className="userProduct" key = {item.id}>
                        <li><strong>{item.name}</strong></li>
                        <li><i>Marka: {item.brand}</i></li>
                        <li>Kalorie: {item.calories}</li>
                        <li><small>Białka: {item.proteins}, Węglowodany: {item.carbohydrates}, Tłuszcze: {item.fats}</small></li>
                        <li>{item.gramsInPortion} grams in portion</li>
                        <div className="productButtons">
                        <button onClick={() => handleAddProduct(item.id, item.gramsInPortion)}>Dodaj</button>
                        <button onClick={() => handleDeleteProduct(item.id)}>Usuń</button>
                        </div>
                    </div>
                ))
            ): (
                <h3>Brak dodanych produktów</h3>
            )}
            </ul>
            <button onClick={handlePrevPage} disabled={pageNumber === 1}>Poprzednia strona</button>
            <button onClick={handleNextPage} disabled={pageNumber === pages}>Następna strona</button>
            <p>{pageNumber}/{pages}</p>
        </div>
        </div>
    )

}
export default AddProduct