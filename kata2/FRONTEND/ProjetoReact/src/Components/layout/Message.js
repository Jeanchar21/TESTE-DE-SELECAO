import { useEffect, useState } from "react"
import style from './Message.module.css'

function Message ({ tipo, mensagem, clearmessage }) {

    const [ visivel, setVisivel ] = useState(true)

    const [ UpDown, setUpDown ] = useState(style.mensagemShow)

    const [ tamanhoTimer, setTamanhoTimer ] = useState(300)

    useEffect(() => {
        if (!mensagem){
            setVisivel(false)
            setTamanhoTimer(300)
            return
        }

        setVisivel(true)
        setUpDown(style.mensagemShow)

        const interval = setInterval(() => {
            setTamanhoTimer(prev => Math.max(0, prev - (300 / 30)));
        }, 100);

        const timerUpDown = setTimeout(() => {
            setUpDown()
        }, 3000)

        const timer = setTimeout(() => {
            setUpDown()
            setVisivel(false)
            clearmessage ? clearmessage("") : setVisivel(false)
        }, 4000)

        return () => {
            clearTimeout(timer);
            clearInterval(interval);
        };
    }, [mensagem])

    return (<>
        {visivel && (
            <div className={`${style.quadro} ${UpDown} ${style[tipo]}`}>
                <p className={style.mensagem}>{mensagem}</p>
                <div className={style.linhatimer} style={{'width':`${tamanhoTimer}px`}}></div>
            </div>
        )}
    </>)
}

export default Message;