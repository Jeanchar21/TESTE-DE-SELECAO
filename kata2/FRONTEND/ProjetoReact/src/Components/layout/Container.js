import style from './Container.module.css'

function Container(propriedades) {
    return(
        <div className={`${style.container} ${style[propriedades.customClass]}`}>
            {propriedades.children}
        </div>
    )
}

export default Container;