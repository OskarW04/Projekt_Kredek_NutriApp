import './App.css';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import LoginPage from './Components/Login.jsx';
import RegisterPage from './Components/Register.jsx';

function App() {
    return (
        <div>
            <Router>
                <Route exact path="/" component={LoginPage} />
                <Route path="/register" component={RegisterPage} />
            </Router>
        </div>
    )
}

export default App;