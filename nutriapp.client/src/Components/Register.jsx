import React from 'react';
import { useForm } from "react-hook-form";
import { DevTool } from '@hookform/devtools';

function Register() {

    const form = useForm();
    const { register, control } = form;

    return (
        <div>
            <h1>Zarejestruj sie:</h1>
            <form>
                <label htmlFor="name">Imie</label>
                <input type="text" id="name" {...register("name")} />

                <label htmlFor="surname">Nazwisko</label>
                <input type="text" id="surname" {...register("surname")} />

                <label htmlFor="email">E-mail</label>
                <input type="text" id="email" {...register("email")} />

                <label htmlFor="password">Haslo</label>
                <input type="password" id="password" {...register("password")} />

                <button>Submit</button>
                <p>Masz juz konto? Zaloguj sie</p>
            </form>
            <DevTool control={control} />
        </div>
    )
}
export default Register;