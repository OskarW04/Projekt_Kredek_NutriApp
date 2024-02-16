import PropTypes from "prop-types";
import { useForm, Controller } from "react-hook-form";
import "../../App.css";

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

DishForm.propTypes = {
  product: PropTypes.object.isRequired,
  onDelete: PropTypes.func.isRequired,
  onUpdate: PropTypes.func.isRequired,
  onSave: PropTypes.func.isRequired,
};

export default DishForm;
