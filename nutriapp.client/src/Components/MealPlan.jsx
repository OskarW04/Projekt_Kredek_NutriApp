import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css'

export function MealPlan(){
    const navigate = useNavigate();
    const Todaydate = new Date();
    const [dateSelect, setDateSelect] = useState(Todaydate);
    const [pickedMealDate, setPickedMealDate] = useState();
    const [isPopupOpen, setPopupOpen] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [dish, setDish] = useState([]);

    const handleDateChange = date => {
        setDateSelect(date);
      };

      const getDish = async () => {
        const token = localStorage.getItem('token');
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
        setDish(Dish.items)
        console.log(Dish.items)
        }
        catch(error){
          console.log(error);
        }
      }

      useEffect(() => { // Pobieranie danych
        const fetchData = async () => {
          const date = dateSelect.toLocaleDateString('en-US').replace(/\//g, '.');
          const token = localStorage.getItem('token');
          try {
            const response = await axios.get(`https://localhost:7130/api/MealPlan/${date}`, {
              headers: {
                'Authorization': `Bearer ${token}`
              },
            });
            setPickedMealDate(response.data);
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
        console.log(localStorage.getItem('token'))
        setPopupOpen(true);
      };
    
    const handleClosePopup = () => {
        setPopupOpen(false);
      };

    const handleAddDish =() => {
        navigate("/AddDish")
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

    return(
        <>
        <div>
        <h4>Wybierz datę: </h4>
        <DatePicker
            selected={dateSelect}
            onChange={handleDateChange}
            minDate={new Date()}
            dateFormat="dd.MM.yyyy"
        />

        <h1>Dzisiejszy jadłospis:</h1>
        <p>{pickedMealDate && (pickedMealDate.meals === null ? (pickedMealDate.meals) : (<>Brak posiłkow danego dnia</>))}</p>
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
                    <button name="add" >Dodaj</button>
                    </div>
                    </>
                ))
                ) : (
                <>
                <li>Nie znaleziono posiłków</li>
                <button onClick={handleAddDish}>Stwórz posiłek</button>
                </>
                )}
            </ul>
            


        </div>}
      />
        </>
    )
}

export default MealPlan;