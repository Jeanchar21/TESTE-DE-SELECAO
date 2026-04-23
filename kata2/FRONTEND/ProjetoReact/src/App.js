import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css';
import NavBar from './Components/layout/NavBar';
import Container from './Components/layout/Container';
import Home from './Components/Pages/Home';
import RegistrarTask from './Components/Pages/RegistarTask';

function App() {
  return (
     <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Container customClass="min-height">
            <Home />
          </Container>} />
          <Route path="/registrar-tarefa" element={<Container customClass="min-height">
            <RegistrarTask />
          </Container>} />
        </Routes>
    </Router>
  );
}

export default App;
