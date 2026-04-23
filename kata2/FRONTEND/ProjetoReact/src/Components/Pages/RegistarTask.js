import { useState } from 'react';
import style from './RegistrarTask.module.css'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import Message from '../layout/Message';

function RegistrarTask () {

    const navigate = useNavigate()

    const [ message, setMessage ] = useState({
        text: "",
        type: ""
    })

    const [ tarefa, setTarefa ] = useState({
        titulo: "",
        situacao: "",
        prioridade: "",
        status: 0
    })

    const handleChange = (e) => {
        setTarefa({ ...tarefa, [e.target.id]: e.target.value })
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        if(tarefa.titulo == ""){
            setMessage({text: "O título da tarefa é necessário", type: "erro"})
            return
        } else if (tarefa.situacao == ""){
            setMessage({text: "A descrição da tarefa é necessário", type: "erro"})
            return
        } else if (tarefa.prioridade == ""){
            setMessage({text: "Informe uma prioridade", type: "erro"})
            return
        }

        try {
            var tarefaTratada = tarefa
            tarefaTratada.prioridade = parseInt(tarefaTratada.prioridade)
            var response = await axios.post('https://localhost:7142/api/Task', tarefa, {
                headers: {
                    "Content-Type": "application/json"
                }
            })
            console.log(response)
            if(response)
                navigate("/", { state: { message: "Tarefa Registrada com sucesso" }})
        } catch (error) {
            setMessage({text: error.message, type: "erro"})
            console.log(error)
        }
        console.log(tarefa)
    }

    return(
        <div className={style.containerRegister}>
            {message.text != "" && (<Message mensagem={message.text} tipo={message.type} clearmessage={(e)=>{setMessage({...message, text: e})}}/>)}
            <h1>Registrar atividade</h1>
            <form className={style.formulario} onSubmit={handleSubmit}>
                <div className={style.elemento}>
                    <label>Título</label>
                    <input id='titulo' value={tarefa.titulo ? tarefa.titulo : ''} onChange={handleChange}/>
                </div>
                <div className={style.elemento}>
                    <label>Situação</label>
                    <textarea id='situacao' value={tarefa.situacao ? tarefa.situacao : ''} onChange={handleChange}/>
                </div>
                <div className={style.elemento}>
                    <label>Prioridade</label>
                    <select id='prioridade' value={tarefa.prioridade ? tarefa.prioridade : ''} onChange={handleChange}>
                        <option value={undefined}></option>
                        <option value={0}>BAIXA</option>
                        <option value={1}>MEDIA</option>
                        <option value={2}>ALTA</option>
                    </select>
                </div>
                <button className={style.botaoEnvio} type='submit'>Criar Task</button>
            </form>
        </div>
    )
}

export default RegistrarTask;