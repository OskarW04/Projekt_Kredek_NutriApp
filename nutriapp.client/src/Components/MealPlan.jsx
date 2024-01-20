import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from 'react-datepicker';
import { useForm, Controller } from "react-hook-form";
import 'react-datepicker/dist/react-datepicker.css'

export function MealPlan(){
    const navigate = useNavigate();
    const Todaydate = new Date();
    const [dateSelect, setDateSelect] = useState(Todaydate);
    const [pickedMealDate, setPickedMealDate] = useState([]);
    const [isPopupOpen, setPopupOpen] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [mealId, setMealId] = useState();
    const [dish, setDish] = useState([]);
    const token = sessionStorage.getItem('token');


    const form = useForm();
    const { register, control, handleSubmit, formState } = form;
    const {errors} = formState;

    const handleDateChange = date => {
        setDateSelect(date);
      };

      const getDish = async () => {
        try{
        const Dish = await axios.get("https://localhost:7130/api/Dish/userDishes", {
          headers:{
                      'Authorization': `Bearer ${token}`
                  },
          params: {
                  'pageNumber': pageNumber,
                  'pageSize': 5,
                  },                          
     })
        console.log(Dish.data.items)
        setDish(Dish.data.items)
        }
        catch(error){
          console.log(error);
        }
      }

      useEffect(() => { 
        const fetchData = async () => {
          const date = dateSelect.toLocaleDateString('en-US').replace(/\//g, '.');
          try {
            const response = await axios.get(`https://localhost:7130/api/MealPlan/${date}`, {
              headers: {
                'Authorization': `Bearer ${token}`
              },
            });
            setMealId(response.data.id)
            setPickedMealDate(response.data.meals);

          } catch (error) {
            console.error(error);
          }
        };
    
        fetchData(); 
      }, [dateSelect]);

      useEffect(() => {
      }, [pickedMealDate]);



      const handleOpenPopup = () => {
        getDish();
        console.log(sessionStorage.getItem('token'))
        setPopupOpen(true);
      };
    
    const handleClosePopup = () => {
        setPopupOpen(false);
      };

    const handleAddDish =() => {
        navigate(`/AddDish`)
    }
    
    
    const PopupModal = ({ isOpen, onClose, content }) => {
        return (
          <div className={`popup-modal ${isOpen ? 'open' : ''}`}>
            <div className="modal-content">
              <span className="close-btn" onClick={onClose}>×</span>
              {content}
            </div>
          </div>
        );
      };

      const handleDeleteDish = async(dishID) => {
        try{
          const del = await axios.delete(`https://localhost:7130/api/Dish/${dishID}`, {
            headers: {
              'Authorization': `Bearer ${token}`
            },
          })
          getDish();
        }catch(error){
          console.error(error)
        }
      }

      const handleDeleteMeal = async(mealType) => {
        try{
          const del = await axios.delete(`https://localhost:7130/api/MealPlan/${mealId}?mealType=${mealType}`, {
            headers: {
              'Authorization': `Bearer ${token}`
            },
          })
          window.alert("Usunięto");
          window.location.reload();

        }catch(error){
          console.error(error)
        }
      }

      const handleUpdateDish = (DishID) => {
        navigate(`/CreateDish/${encodeURIComponent(DishID)}`)
      }


      const onSave = async(data) => {
        try{
             const response = await axios.put(`https://localhost:7130/api/MealPlan/${mealId}?dishId=${data.Id}&gramsOfPortion=${data.grams}&mealType=${data.MealType}`,null,{
                headers:{
                  'Authorization': `Bearer ${token}`
                  }
              })
             
        }
        catch(error)
        {
            console.error(error)
        }
    }

    


      console.log(pickedMealDate)
    return(
        <div>
          <div>
          <h2>Wybierz datę:</h2>
          <DatePicker
              selected={dateSelect}
              onChange={handleDateChange}
              dateFormat="dd.MM.yyyy"
              customInput={<input style={{ width: '400px',textAlign: 'center' }} />}
              shouldCloseOnSelect={false}
              calendarStartDay={1}
          />

          <h1>Dzisiejszy jadłospis:</h1>
          <div>
          <ul className="product-list">
            {(pickedMealDate.length !== 0) ? (
              pickedMealDate.map((meal,index) => (
                <>
                <div className="product">
                  <li><strong>{meal.dish.name}</strong><button onClick={() => handleDeleteMeal(meal.mealType)}>Usuń</button></li>
                  <li>{meal.mealType}</li>
                  <li></li>
                </div>
                </>
              ))
            ) : (
              <p className="mealP">Brak posiłków</p>
            )}
          </ul>
          </div>
          <button onClick={handleOpenPopup}>Dodaj posiłek</button>
          </div>
          
          <PopupModal
          isOpen={isPopupOpen}
          onClose={handleClosePopup}
          content={
              <div>
              <h1 className="dishInput"  >Lista posiłków:</h1>
              <ul className="product-list">
              {Array.isArray(dish) ? (
                  dish.map((product) => (
                      <div className="product" key={product.id}>
                      <li className="dishName"><strong>{product.name}</strong></li>
                      <div className="mealButtons">
                        <button className="dishButton1" onClick={() => handleDeleteDish(product.id)}>Usuń</button>
                        <button className="dishButton2" onClick={() => handleUpdateDish(product.id)}>Aktualizuj</button>
                      </div>
                      <div className="dishDescription">
                        <li>Kalorie: {product.calories}</li>
                        <li><small>Proteins: {product.proteins}, Carbs: {product.carbohydrates}, Fats: {product.fats}</small></li>
                        <li>Opis: <i>{product.description}</i></li>
                      </div>
                      <form key={product.id} onSubmit={() => handleSubmit(onSave)()}>
                        <input type="hidden" {...register('Id')} value={product.id} />
                        <div className="dishInputForm">
                        <input placeholder="Ilość gram"className="dishInputGrams" type="number" id={`grams_${product.id}`} {...register(`grams_${product.id}`,  {
                          required:{
                            value: true,
                            message: "Pole jest wymagane"
                          }
                        })} />
                        <p className="errorDish">{errors[`grams_${product.id}`]?.message}</p>
                        <Controller
                            name={`MealType_${product.id}`}
                            control={control}
                            defaultValue=""
                            rules={{
                                required: {
                                    value:true,
                                    message:"Pole jest wymagane"},
                            }}
                            render={({ field }) => (
                            <>
                                <select className="dishInputType"  {...field}>
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
                                <p className="errorDish">{errors[`MealType_${product.id}`]?.message}</p>
                            </>
                            )}
                            />
                          <button className="dishInputAdd">Dodaj</button>
                        </div>
                      </form>
                    </div>
                  ))
                  ) : (
                  <>
                  <li>Nie znaleziono posiłków</li>
                  </>
                  )}
                  <button className="dishInputCreate" onClick={handleAddDish}>Stwórz posiłek</button>
              </ul>
            </div>}
        />
        </div>
    )
}

export default MealPlan;