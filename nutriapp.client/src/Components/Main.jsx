import React, {useState, useEffect} from "react"
import axios from "axios"

export function Main() {
    const [searchItem, setSearchItem] = useState('')
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(5);
    const [searchResults, setSearchResults] = useState([]);
    
    const fetchdata = async () => {
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
        } catch(error){
            console.error(error)
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
                    type="text"
                    placeholder="Wpisz szukane produkty..."
                    value={searchItem}
                    onChange={handleInputChange}
                />
                <button onClick={handlePrevPage} disabled={pageNumber === 1}>
                    Poprzednia strona
                </button>
                <button onClick={handleNextPage}>Następna strona</button>
                <ul>
                    {Array.isArray(searchResults) ? (
                    searchResults.map((product, index) => (
                        <>
                        <li key={index + product.name}>{product.name}</li>
                        <ul>
                            <li key={index + product.brand}>Brand: {product.brand}</li>
                            <li key={index + product.description}>{product.description}</li>
                        </ul>

                        </>
                    ))
                    ) : (
                    <li>Nie znaleziono produktów</li>
                    )}
                </ul>
                <p>{pageNumber}</p>
            </div>
    )
}

export default Main