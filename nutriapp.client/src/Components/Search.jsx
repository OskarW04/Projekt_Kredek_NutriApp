import React, {useState, useEffect} from "react"
import axios from "axios"
import "../App.css"

export function Search() {
    const [searchItem, setSearchItem] = useState('')
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(5);
    const [allPages, setAllPages] = useState('');
    const [searchResults, setSearchResults] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [loadingError, setLoadingError] = useState(null);
    
    const fetchdata = async () => {
        setIsLoading(true)
        const token = localStorage.getItem('token')
        try {
            const response = await axios.get("https://localhost:7130/api/Product/apiProducts", {
                headers:{
                            'Authorization': `Bearer ${token}`
                        },
                params: {
                        'searchPhrase': searchItem.substring(0, searchItem.length-1),
                        'pageNumber': pageNumber,
                        'pageSize': pageSize,
                        },                          
           })
           setSearchResults(response.data.items.map((item) => ({ name: item.name, description: item.description, brand: item.brand })));
           setAllPages(response.data.totalPages)
        } catch(error){
            console.error(error)
            setLoadingError("Wystąpił błąd podczas ładowania danych")
        }
        finally{
            setIsLoading(false)
        }
    };
    useEffect(() => {
        
        if(searchItem.length >= 3)
        {
            fetchdata()
        }
        else{
            setSearchResults([]);
        }
    }, [searchItem, pageNumber, pageSize])

    const handleInputChange = (event) => {
        setSearchItem(event.target.value);
        setPageNumber(1);
      };
    
      const handleNextPage = () => {
        setPageNumber(pageNumber+1);
        fetchdata();
      };
    
      const handlePrevPage = () => {
        setPageNumber(pageNumber-1);
        fetchdata();
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
                {searchItem !== "" && (<p className="pages">{pageNumber}/{allPages}</p>)}
                <button className="prevPage" onClick={handlePrevPage} disabled={pageNumber === 1}>
                    Poprzednia strona
                </button>
                <button className="nextPage" onClick={handleNextPage}>Następna strona</button>
                </div>
                {loadingError && (<div>{loadingError}</div>)}

                {isLoading ? 
                (<div><img className="gif" src="/loading.gif" alt="Ikona ładowania" /></div>):
                <ul className="product-list">
                {Array.isArray(searchResults) ? (
                searchResults.map((product, index) => (
                    <>
                    <div className="product">
                    <li key={index + product.name}><strong>{product.name}</strong></li>
                    <ul>
                        {product.brand !== null && (<li key={index + product.brand}>Brand: {product.brand}</li>)}
                        <li key={index + product.description}>{product.description}</li>
                    </ul>
                    <button name="add" >Dodaj</button>
                    </div>
                    </>
                ))
                ) : (
                <li>Nie znaleziono produktów</li>
                )}
            </ul>
            }
                
                
            </div>
    )
}

export default Search