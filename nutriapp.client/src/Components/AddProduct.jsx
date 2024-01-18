import React from "react";
import {useForm} from "react-hook-form"

export function AddProduct() {


    onSubmit


    return(
        <div>
        <h1>Stwórz własny produkt</h1>
        <form onSubmit={handleSubmit(onSubmit) }>

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
            </form>
        </div>
    )

}
export default AddProduct