import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from "react-datepicker";
import DishForm from "../components/DishForm/DishForm";
import BottomBar from "../components/BottomBar/BottomBar";
import "react-datepicker/dist/react-datepicker.css";

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
    <div
      style={{
        position: "relative",
        minHeight: "100%",
      }}
    >
      <div className="mainPageButtons">
        <button onClick={() => handleLogOut()} className="logout">
          Wyloguj
        </button>
        <button
          onClick={() => navigate("/Details/Get")}
          className="goToDetails"
        >
          User Details
        </button>
      </div>
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
        <div
          style={{
            paddingBottom: "100px",
          }}
        >
          <div>
            <button
              style={{ margin: "0.2rem", width: "50%" }}
              onClick={handleOpenPopup}
            >
              Dodaj posiłek
            </button>

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
            </div>
          </div>
          <div className="showNutritions">
            <BottomBar
              calories={nutriTotal[0]}
              carbohydrates={nutriTotal[1]}
              fats={nutriTotal[2]}
              proteins={nutriTotal[3]}
            />
          </div>
        </div>
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
