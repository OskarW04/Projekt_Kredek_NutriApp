import React from 'react';
import { Link } from 'react-router-dom';

function Home() {
    return (
        <div style={styles.container}>
            <h1>NutriApp</h1>
            <div style={styles.buttonsContainer}>
                <Link to="/Login" style={styles.button}>Zaloguj sie</Link>
                <Link to="/Register" style={styles.button}>Zarejestruj sie</Link>
            </div>
        </div>
    );
}

const styles = {
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        height: screen.availHeight / 2,
        
    },
    buttonsContainer: {
        marginTop: 20,
    },
    button: {
        margin: '0 10px',
        padding: '10px 20px',
        fontSize: 16,
        textDecoration: 'none',
        color: '#fff',
        backgroundColor: '#007BFF',
        border: 'none',
        borderRadius: 5,
        cursor: 'pointer',
    },
};

export default Home;