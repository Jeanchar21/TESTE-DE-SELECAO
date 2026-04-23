import { Link } from 'react-router-dom'
import Container from './Container'
import style from './NavBar.module.css'

function NavBar() {
    return (
        <nav className={style.navbar}>
            <Container>
                <ul className={style.list}>
                    <li className={style.item}>
                        <Link to="/">Home</Link>
                    </li>
                    <li className={style.item}>
                        <Link to="/registrar-tarefa">Registrar Tarefa</Link>
                    </li>
                </ul>
            </Container>
        </nav>
    )
}

export default NavBar;