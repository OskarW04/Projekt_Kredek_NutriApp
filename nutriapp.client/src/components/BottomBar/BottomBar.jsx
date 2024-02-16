import PropTypes from "prop-types";
import "./BottomBar.css";

const BottomBar = ({ calories, carbohydrates, fats, proteins }) => {
  return (
    <div className="bottom-bar">
      <div className="grouped-items">
        <h3>Podsumowanie dnia:</h3>
        <span>Kalorie: {calories}</span>
        <span>
          Węglowodany: {carbohydrates} Tłuszcze: {fats} Białko: {proteins}
        </span>
      </div>
    </div>
  );
};

BottomBar.propTypes = {
  calories: PropTypes.number.isRequired,
  carbohydrates: PropTypes.number.isRequired,
  fats: PropTypes.number.isRequired,
  proteins: PropTypes.number.isRequired,
};

export default BottomBar;
