import React from 'react';
import { useForm, useController } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import "../App.css"
import Select from 'react-select';

const Goals = [
    { value: 1, label: 'Redukcja' },
    { value: 2, label: 'Trzymanie wagi' },
    { value: 3, label: 'Masa' }
];

export function UserDetails() {

    const form = useForm();
    const { register, control } = form;
    const { field: { value: goalValue, onChange: goalOnChange, ...restgoalField } } = useController({ name: 'goal', control });
    return (
        <div>
            <h1>Powiedz nam cos o sobie:</h1>
            <form>
                <label htmlFor="name">Imie</label>
                <input type="text" id="name" {...register("name")} />

                <label htmlFor="surname">Nazwisko</label>
                <input type="text" id="surname" {...register("surname")} />

                <label htmlFor="age">Wiek</label>
                <input type="number" id="age" {...register("age")} />

                <label htmlFor="height">Wzrost</label>
                <input type="number" id="height" {...register("height")} />

                <label htmlFor="weight">Waga</label>
                <input type="number" id="weight" {...register("weight")} />

                <label htmlFor="email">Cel</label>
                <Select
                    className='select-input'
                    placeholder="Wybierz cel"
                    isClearable
                    options={Goals}
                    value={goalValue ? Goals.find(x => x.value === goalValue) : goalValue}
                    onChange={option => goalOnChange(option ? option.value : option)}
                    {...restgoalField}
                />

           
                <button>Submit</button>
                
            </form>
            <DevTool control={control} />
        </div>
    )
}
export default UserDetails;