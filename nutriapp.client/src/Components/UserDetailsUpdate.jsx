import React from "react";
import { useForm, useController, Controller } from "react-hook-form";
import { DevTool } from "@hookform/devtools";
import { Link, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import "../App.css";

import axios from "axios";

const apiUrl = import.meta.env.VITE_API_URL;

export const UserDetailsUpdate = () => {
  const location = useLocation();
  const details = location.state;

  const form = useForm({
    defaultValues: {
      name: details.name,
      lastName: details.lastName,
      age: details.age,
      height: details.height,
      weight: details.weight,
    },
  });
  const { register, control, handleSubmit, formState } = form;
  const { errors } = formState;
  const navigate = useNavigate();

  const onSubmit = async (data) => {
    const json = JSON.stringify(data, (key, value) => {
      if (!isNaN(value) && value !== "") {
        return parseInt(value, 10);
      }
      return value;
    });

    const token = sessionStorage.getItem("token");
    sendRequest(json, token);
    navigate("/Details/Get");
  };

  return (
    <div>
      <h1>Aktualizuj dane:</h1>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="form-control">
          <label htmlFor="name">Imie</label>
          <input
            type="text"
            id="name"
            {...register(
              "name",

              {
                required: {
                  value: true,
                  message: "Pole jest wymagane",
                },
                pattern: {
                  value: /[a-zA-Z]/,
                  message: "Niepoprawny format imienia",
                },
              }
            )}
          />
          <p className="error">{errors.name?.message}</p>
        </div>

        <div className="form-control">
          <label htmlFor="lastName">Nazwisko</label>
          <input
            type="text"
            id="lastName"
            {...register(
              "lastName",

              {
                required: {
                  value: true,
                  message: "Pole jest wymagane",
                },
                pattern: {
                  value: /[a-zA-Z]/,
                  message: "Niepoprawny format imienia",
                },
              }
            )}
          />
          <p className="error">{errors.lastName?.message}</p>
        </div>

        <div className="form-control">
          <label htmlFor="age">Wiek</label>
          <input
            type="number"
            id="age"
            {...register(
              "age",

              {
                required: {
                  value: true,
                  message: "Pole jest wymagane",
                },

                min: {
                  value: 1,
                  message: "Niepoprawny format wieku",
                },
              }
            )}
          />
          <p className="error">{errors.age?.message}</p>
        </div>

        <div className="form-control">
          <label htmlFor="height">Wzrost</label>
          <input
            type="number"
            id="height"
            {...register(
              "height",

              {
                required: {
                  value: true,
                  message: "Pole jest wymagane",
                },
                min: {
                  value: 1,
                  message: "Niepoprawny format wzrostu",
                },
              }
            )}
          />
          <p className="error">{errors.height?.message}</p>
        </div>

        <div className="form-control">
          <label htmlFor="weight">Waga</label>
          <input
            type="number"
            id="weight"
            {...register(
              "weight",

              {
                required: {
                  value: true,
                  message: "Pole jest wymagane",
                },
                min: {
                  value: 1,
                  message: "Niepoprawny format wagi",
                },
              }
            )}
          />
          <p className="error">{errors.weight?.message}</p>
        </div>

        <div className="form-control">
          <label htmlFor="nutritionGoal">Cel</label>
          <Controller
            name="nutritionGoal"
            control={control}
            defaultValue=""
            rules={{
              required: {
                value: true,
                message: "Pole jest wymagane",
              },
            }}
            render={({ field }) => (
              <>
                <select {...field}>
                  <option value="" disabled>
                    Wybierz opcję
                  </option>
                  <option value="1">Redukcja</option>
                  <option value="2">Trzymanie Wagi</option>
                  <option value="3">Masa</option>
                </select>
                <p className="error">{errors.nutritionGoal?.message}</p>
              </>
            )}
          />
        </div>
        <button>Submit</button>
      </form>
      <DevTool control={control} />
    </div>
  );
};

async function sendRequest(json, token) {
  try {
    const updateDetails = await axios.put(`${apiUrl}/api/UserDetails`, json, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      params: { email: sessionStorage.getItem("email") },
    });
    return updateDetails;
  } catch (error) {
    window.alert(error);
    return [];
  }
}
