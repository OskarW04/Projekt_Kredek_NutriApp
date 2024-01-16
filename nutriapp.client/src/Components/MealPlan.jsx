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

      const handleDeleteMeal = async() => {
        try{
          const del = await axios.delete(`https://localhost:7130/api/MealPlan/${mealId}`, {
            headers: {
              'Authorization': `Bearer ${token}`
            },
          })
        }catch(error){
          console.error(error)
        }
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
        <>
        <div>
        <h4>Wybierz datę: </h4>
        <DatePicker
            selected={dateSelect}
            onChange={handleDateChange}
            dateFormat="dd.MM.yyyy"
        />

        <h1>Dzisiejszy jadłospis:</h1>
        <div>
        <ul className="product-list">
          {(pickedMealDate.length !== 0) ? (
            pickedMealDate.map((meal,index) => (
              <>
              <div className="product">
                <li><strong>{meal.dish.name}</strong><button onClick={() => handleDeleteMeal(meal.id, meal.mealType)}>Usuń</button></li>
              </div>
              </>
            ))
          ) : (
            <p>Brak posiłków</p>
          )}
        </ul>
        </div>
        <button onClick={handleDeleteMeal}>Usuń</button>
        <button onClick={handleOpenPopup}>Dodaj posiłek</button>
        </div>
        
        <PopupModal
        isOpen={isPopupOpen}
        onClose={handleClosePopup}
        content={<div>
            <h1>Lista posiłków:</h1>
            <ul className="product-list">
            {Array.isArray(dish) ? (
                dish.map((product, index) => (
                    <>
                    <div className="product">
                    <li key={index + product.name}><strong>{product.name}</strong></li>
                    <ul>
                        
                    </ul>
                    <button onClick={() => handleDeleteDish(product.id)}>Usuń</button>
                    <button>Aktualizuj</button>



                    <form onSubmit={() => handleSubmit(onSave)()}>
                    <input type="hidden" {...register('Id')} value={product.id} />
                    <label htmlFor="grams">Ilość gram</label>
                    <input type="number" id="grams" {...register("grams")} />

                    <label htmlFor="MealType">Typ posiłku</label>
                    <Controller
                        name="MealType"
                        control={control}
                        defaultValue=""
                        rules={{
                            required: {
                                value:true,
                                message:"Pole jest wymagane"},
                        }}
                        render={({ field }) => (
                        <>
                            <select {...field}>
                            <option value="" disabled>
                                Wybierz opcję
                            </option>
                            <option value="1">Śniadanie</option>
                            <option value="2">Drugie śniadanie</option>
                            <option value="3">Lunch</option>
                            <option value="4">Obiad</option>
                            <option value="5">Przekąska</option>
                            <option value="6">Kolacja</option>
                            </select>
                            <p className="error">{errors.MealType?.message}</p>
                        </>
                        )}
                        />

                    <button>Dodaj</button>
                </form>
                    </div>
                    </>
                ))
                ) : (
                <>
                <li>Nie znaleziono posiłków</li>
                </>
                )}
                <button onClick={handleAddDish}>Stwórz posiłek</button>
            </ul>
            


        </div>}
      />
        </>
    )
}

export default MealPlan;