import React, { useState, useEffect } from "react";
import axios from "axios";
import "../App.css";
import { useNavigate, useParams } from "react-router-dom";

export function Search() {
  const apiUrl = import.meta.env.VITE_API_URL;

  const [searchItem, setSearchItem] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(5);
  const [allPages, setAllPages] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [loadingError, setLoadingError] = useState(null);
  const navigate = useNavigate();
  const { adress } = useParams();
  const AddUrl = decodeURIComponent(adress);

  const fetchdata = async () => {
    setIsLoading(true);
    const token = sessionStorage.getItem("token");
    try {
      const response = await axios.get(`${apiUrl}/api/Product/apiProducts`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        params: {
          searchPhrase: searchItem.substring(0, searchItem.length - 1),
          pageNumber: pageNumber,
          pageSize: pageSize,
        },
      });
      setSearchResults(
        response.data.items.map((item) => ({
          apiId: item.apiId,
          gramsInPortion: item.gramsInPortion,
          name: item.name,
          description: item.description,
          brand: item.brand,
        }))
      );
      setAllPages(response.data.totalPages);
      setLoadingError(null);
    } catch (error) {
      console.error(error);
      setLoadingError("Wystąpił błąd podczas ładowania danych");
    } finally {
      setIsLoading(false);
    }
  };
  useEffect(() => {
    if (searchItem.length >= 3) {
      fetchdata();
    } else {
      setSearchResults([]);
    }
  }, [searchItem, pageNumber, pageSize]);

  const handleInputChange = (event) => {
    setSearchItem(event.target.value);
    setPageNumber(1);
  };

  const handleNextPage = () => {
    setPageNumber(pageNumber + 1);
    fetchdata();
  };

  const handlePrevPage = () => {
    setPageNumber(pageNumber - 1);
    fetchdata();
  };

  const handleAdd = async (product) => {
    try {
      const token = sessionStorage.getItem("token");
      const response = await axios.put(
        `${apiUrl}/api/Dish/${AddUrl}/addProduct?productId=${product.apiId}&grams=${product.gramsInPortion}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      navigate(`/CreateDish/${encodeURIComponent(adress)}`);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <input
        className="searchInput"
        type="text"
        placeholder="Wpisz szukane produkty..."
        value={searchItem}
        onChange={handleInputChange}
      />
      <div>
        {searchItem !== "" && (
          <p className="pages">
            {pageNumber}/{allPages}
          </p>
        )}
        <button
          className="prevPage"
          onClick={handlePrevPage}
          disabled={pageNumber === 1}
        >
          Poprzednia strona
        </button>
        <button
          className="nextPage"
          onClick={() => handleNextPage()}
          disabled={pageNumber === allPages}
        >
          Następna strona
        </button>
      </div>
      {loadingError && <div>{loadingError}</div>}

      {isLoading ? (
        <div>
          <img className="gif" src="/loading.gif" alt="Ikona ładowania" />
        </div>
      ) : (
        <ul className="product-list">
          {searchResults.length !== 0 ? (
            searchResults.map((product) => (
              <>
                <div key={product.id} className="product">
                  <li>
                    <strong>{product.name}</strong>
                  </li>
                  <ul>
                    {product.brand !== null && <li>Marka: {product.brand}</li>}
                    <li>{product.description}</li>
                  </ul>
                  <button name="add" onClick={() => handleAdd(product)}>
                    Dodaj
                  </button>
                </div>
              </>
            ))
          ) : (
            <li>Nie znaleziono produktów</li>
          )}
        </ul>
      )}
    </div>
  );
}

export default Search;
