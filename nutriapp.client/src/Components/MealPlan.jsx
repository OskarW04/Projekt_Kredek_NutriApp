import React, { useState, useEffect } from "react";
import axios from "axios";
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css'

export function MealPlan(){
    const Todaydate = new Date();
    const [dateSelect, setDateSelect] = useState(Todaydate)
    const [pickedMealDate, setPickedMealDate] = useState()
    const [isPopupOpen, setPopupOpen] = useState(false);

    const handleDateChange = date => {
        setDateSelect(date);
      };

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
        console.log(pickedMealDate);
      }, [pickedMealDate]);



      const handleOpenPopup = () => {
        setPopupOpen(true);
      };
    
    const handleClosePopup = () => {
        setPopupOpen(false);
      };
    
    
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
        </div>}
      />
        </>
    )
}

export default MealPlan;