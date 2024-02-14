import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from "react-datepicker";
import { useForm, Controller } from "react-hook-form";
import "react-datepicker/dist/react-datepicker.css";

const DishForm = ({ product, onDelete, onUpdate, onSave }) => {
  const form = useForm();
  const { register, control, handleSubmit, formState } = form;
  const { errors } = formState;
  return (
    <div className="product" key={product.id}>
      <li className="dishName">
        <strong>{product.name}</strong>
      </li>
      <div className="mealButtons">
        <button className="dishButton1" onClick={() => onDelete(product.id)}>
          Usuń
        </button>
        <button className="dishButton2" onClick={() => onUpdate(product.id)}>
          Aktualizuj
        </button>
      </div>
      <div className="dishDescription">
        <li>Kalorie: {product.calories}</li>
        <li>
          <small>
            Białka: {product.proteins} Węgle: {product.carbohydrates} Tłuszcze:{" "}
            {product.fats}
          </small>
        </li>
        <li>
          Opis: <i>{product.description}</i>
        </li>
      </div>
      <form key={product.id} onSubmit={handleSubmit(onSave)}>
        <input type="hidden" {...register("Id")} value={product.id} />
        <div className="dishInputForm">
          <input
            placeholder="Ilość gram"
            className="dishInputGrams"
            type="number"
            id={"grams"}
            {...register("grams", {
              required: {
                value: true,
                message: "Pole jest wymagane",
              },
            })}
          />
          <p className="errorDish">{errors["grams"]?.message}</p>
          <Controller
            name={"MealType"}
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
                <select className="dishInputType" {...field}>
                  <option value="" disabled>
                    Wybierz opcję
                  </option>
                  <option value="Breakfast">Śniadanie</option>
                  <option value="SecondBreakfast">Drugie śniadanie</option>
                  <option value="Lunch">Lunch</option>
                  <option value="Dinner">Obiad</option>
                  <option value="Snack">Przekąska</option>
                  <option value="Supper">Kolacja</option>
                </select>
                <p className="errorDish">{errors["MealType"]?.message}</p>
              </>
            )}
          />
          <button className="dishInputAdd">Dodaj</button>
        </div>
      </form>
    </div>
  );
};

export function MealPlan() {
  const apiUrl = import.meta.env.VITE_API_URL;

  const navigate = useNavigate();
  const Todaydate = new Date();
  const [dateSelect, setDateSelect] = useState(Todaydate);
  const [pickedMealDate, setPickedMealDate] = useState([]);
  const [isPopupOpen, setPopupOpen] = useState(false);
  const [dishPageNumber, setDishPageNumber] = useState(1);
  const [mealId, setMealId] = useState();
  const [dish, setDish] = useState([]);
  const [isDeleted, setDelete] = useState(false);
  const [dishAllPages, setDishAllPages] = useState();
  const [nutriTotal, setNutriTotal] = useState([0, 0, 0, 0]);
  const token = sessionStorage.getItem("token");

  const handleDateChange = (date) => {
    setDateSelect(date);
  };
  useEffect(() => {
    const getDish = async () => {
      try {
        const Dish = await axios.get(`${apiUrl}/api/Dish/userDishes`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
          params: {
            pageNumber: dishPageNumber,
            pageSize: 2,
          },
        });
        setDishAllPages(Dish.data.totalPages);
        setDish(Dish.data.items);
      } catch (error) {
        console.error(error);
      }
    };
    getDish();
    setDelete(false);
  }, [isPopupOpen, dishPageNumber, isDeleted]);

  useEffect(() => {
    const fetchData = async () => {
      const date = dateSelect.toLocaleDateString("en-US").replace(/\//g, ".");
      try {
        const response = await axios.get(`${apiUrl}/api/MealPlan/${date}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setMealId(response.data.id);
        setPickedMealDate(response.data.meals);
        setNutriTotal([
          response.data.caloriesTotal,
          response.data.carbohydratesTotal,
          response.data.fatsTotal,
          response.data.proteinsTotal,
        ]);
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, [dateSelect]);

  const handleOpenPopup = () => {
    setPopupOpen(true);
  };

  const handleClosePopup = () => {
    setPopupOpen(false);
  };

  const handleAddDish = () => {
    navigate(`/AddDish`);
  };

  const handleDishNextPage = () => {
    if (dishPageNumber < dishAllPages) {
      setDishPageNumber(dishPageNumber + 1);
    }
  };

  const handleDishPrevPage = () => {
    if (dishPageNumber !== 1) {
      setDishPageNumber(dishPageNumber - 1);
    }
  };

  const PopupModal = ({ isOpen, onClose, content }) => {
    return (
      <div className={`popup-modal ${isOpen ? "open" : ""}`}>
        <div className="modal-content">
          <span className="close-btn" onClick={onClose}>
            ×
          </span>
          {content}
          <span className="nextPage" onClick={() => handleDishNextPage()}>
            &#8594;
          </span>
          <span
            className="prevPage"
            onClick={() => handleDishPrevPage()}
            disabled={dishPageNumber === 1}
          >
            &#8592;
          </span>
        </div>
      </div>
    );
  };

  const handleDeleteDish = async (dishID) => {
    try {
      const del = await axios.delete(`${apiUrl}/api/Dish/${dishID}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setDelete(true);
    } catch (error) {
      console.error(error);
    }
  };

  const handleDeleteMeal = async (mealType) => {
    try {
      const del = await axios.delete(
        `${apiUrl}/api/MealPlan/${mealId}?mealType=${mealType}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      window.alert("Usunięto");
      window.location.reload();
    } catch (error) {
      console.error(error);
    }
  };

  const handleUpdateDish = (DishID) => {
    navigate(`/CreateDish/${encodeURIComponent(DishID)}`);
  };

  const onSave = async (data) => {
    try {
      const response = await axios.put(
        `${apiUrl}/api/MealPlan/${mealId}?dishId=${data.Id}&gramsOfPortion=${data.grams}&mealType=${data.MealType}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      window.location.reload();
    } catch (error) {
      console.error(error);
    }
  };

  const handleLogOut = () => {
    sessionStorage.clear();
    window.location.reload();
  };
  return (
    <div>
      <button onClick={() => navigate("/Details/Get")} className="goToDetails">
        User Details
      </button>
      <button onClick={() => handleLogOut()} className="logout">
        Wyloguj
      </button>
      <div>
        <h2>Wybierz datę:</h2>
        <DatePicker
          selected={dateSelect}
          onChange={handleDateChange}
          dateFormat="dd.MM.yyyy"
          customInput={
            <input style={{ width: "400px", textAlign: "center" }} />
          }
          shouldCloseOnSelect={false}
          calendarStartDay={1}
        />

        <h1>Dzisiejszy jadłospis:</h1>
        <div>
          <ul className="product-list">
            {pickedMealDate.length !== 0 ? (
              pickedMealDate.map((meal) => (
                <div
                  key={mealId + meal.mealType}
                  style={{ position: "relative" }}
                >
                  <h2 className="mealType">
                    <strong>{meal.mealType}</strong>
                  </h2>
                  <button
                    className="mealButtonadd"
                    onClick={() => handleDeleteMeal(meal.mealType)}
                  >
                    Usuń
                  </button>
                  <div key={meal.id} className="productMeal">
                    <li className="mealName">
                      <strong>{meal.dish.name}</strong>
                    </li>
                    <div className="nutritionMeal">
                      <li>
                        <span>Kalorie: {meal.dish.calories}</span>
                      </li>
                      <li>
                        <span>
                          Węglowodany: {meal.dish.carbohydrates} Tłuszcze:{" "}
                          {meal.dish.fats} Białko: {meal.dish.proteins}
                        </span>
                      </li>
                    </div>
                    <li>Porcja: {`${meal.gramsOfPortion} g`}</li>
                    <li>
                      Opis:{" "}
                      {meal.dish.description.trim() === null ||
                      meal.dish.description.trim() === ""
                        ? "Brak opisu"
                        : meal.dish.description}
                    </li>
                  </div>
                </div>
              ))
            ) : (
              <p className="mealP">Brak posiłków</p>
            )}
          </ul>
          <div className="showNutritions">
            <h3>Łączna kaloryka:</h3>
            <span>Kalorie: {nutriTotal[0]}</span> <br />
            <span>
              Węglowodany: {nutriTotal[1]} Tłuszcze: {nutriTotal[2]} Białko:{" "}
              {nutriTotal[3]}
            </span>
          </div>
        </div>
        <button onClick={handleOpenPopup}>Dodaj posiłek</button>
      </div>

      <PopupModal
        isOpen={isPopupOpen}
        onClose={handleClosePopup}
        content={
          <div>
            <h1 className="dishInput">Lista posiłków:</h1>
            <ul className="product-list">
              {dish.length !== 0 ? (
                dish.map((product) => (
                  <DishForm
                    key={product.id}
                    product={product}
                    onDelete={handleDeleteDish}
                    onUpdate={handleUpdateDish}
                    onSave={onSave}
                  />
                ))
              ) : (
                <>
                  <li className="product">Nie znaleziono posiłków</li>
                </>
              )}
              <p style={{ marginRight: "30px" }}>
                {dishPageNumber}/{dishAllPages}
              </p>
              <button className="dishInputCreate" onClick={handleAddDish}>
                Stwórz posiłek
              </button>
            </ul>
          </div>
        }
      />
    </div>
  );
}

export default MealPlan;
