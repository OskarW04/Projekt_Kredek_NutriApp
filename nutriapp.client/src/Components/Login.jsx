import React from 'react';
import { useForm } from "react-hook-form";
import { DevTool } from '@hookform/devtools';
import { Link } from 'react-router-dom';
import "../App.css"


export function Login() {

    const form = useForm();
    const { register, control } = form;

    return (
        <div>
        <h1>Zaloguj sie:</h1>
            <form>
                <label htmlFor="email">E-mail</label>
                <input type="text" id="email" {...register("email")} />

                <label htmlFor="password">Haslo</label>
                <input type="password" id="password" {...register("password")} />

                <button>Submit</button>
                <p>Nie masz konta? <Link to ="/Register">Zarejestruj sie</Link> </p>
            </form>
            <DevTool control={control} />
        </div>
    )
}
export default Login;