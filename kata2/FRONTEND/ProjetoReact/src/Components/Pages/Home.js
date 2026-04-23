import { useEffect, useState } from 'react'
import style from './Home.module.css'
import { Link, useLocation } from 'react-router-dom'
import Message from '../layout/Message';
import axios from 'axios'

function Home (){
    
    const [ filterStatus, setFilterStatus ] = useState('');

    const [ message, setMessage ] = useState({
        text: "",
        type: ""
    })

    const [ currentPage, setCurrentPage ] = useState(1)

    const location = useLocation()

    useEffect(()=> {
        if(location.state){
            setMessage({text: location.state.message, type: "sucesso"})
        }
    }, [location])

    const [ tarefas, setTarefas ] = useState([])

    const CallBackTasks = () => {
        axios.get(`https://localhost:7142/api/Task?pageNumber=${currentPage}&pageSize=15&${filterStatus != '' ? `?status=${filterStatus}` : ''}`)
        .then((response) => setTarefas(response.data))
        .catch((error) => setMessage({text: error.message, type: "erro"}))
    }

    useEffect(()=>{
        CallBackTasks();
    }, [filterStatus, currentPage])
    
    const concluir = async (id) => {
        try {
            var response = await axios.put(`https://localhost:7142/api/Task/${id}`, { status: 1 })
            setTarefas([...tarefas.map(tarefa => tarefa.idTarefa == id ? {...tarefa, status: "concluida"} : tarefa)])
            setMessage({text: response.data, type: "sucesso"})
        } catch (error) {
            setMessage({text: error.message, type: "erro"})
        }
    }

    const deletar = async (id) => {
        try {
            var response = await axios.delete(`https://localhost:7142/api/Task/${id}`)
            setTarefas([...tarefas.filter(tarefa => tarefa.idTarefa !== id)])
            setMessage({text: response.data, type: "sucesso"})
        } catch (error) {
            setMessage({text: error.message, type: "erro"})
        }
    }

    const downPage = () => {
        if(currentPage > 1)
        setCurrentPage(prev => prev - 1);
    }

    const upPage = () => {
        setCurrentPage(prev => prev + 1);
    }
    
    return(
        <div className={style.corpoContainer}>
            <div>
                <h1>Lista de tarefas</h1>
            </div>
            {message.text != "" && (<Message mensagem={message.text} tipo={message.type} clearmessage={(e)=>{setMessage({...message, text: e})}}/>)}
            <div className={style.espacoTabela}>
                <div className={style.pesquisa}>
                    <p>Filtrar por </p>
                    <select className={style.filtroSelect} defaultValue="nome" onChange={(e) => {
                        const selectedatributte = e.target.value;
                        setFilterStatus(selectedatributte)
                    }}>
                        <option value={''}>Todos</option>
                        <option value={0}>Pendentes</option>
                        <option value={1}>Concluídos</option>
                    </select>
                </div>
                <table className={style.tabelaTarefas}>
                    <thead>
                        <tr className={style.tituloTabela}>
                            <td style={{borderRadius: '10px 0 0 0'}}>título</td>
                            <td>status</td>
                            <td>prioridade</td>
                            <td>Atualizar</td>
                            <td style={{borderRadius: '0 10px 0 0'}}>Remover</td>
                        </tr>
                    </thead>
                    <tbody>
                    {tarefas.length > 0 && tarefas.map((task) => (
                        <tr key={task.idTarefa}>
                            <td>{task.titulo}</td>
                            <td>{task.status == 0 ? 'pendente' : 'concluída'}</td>
                            <td>{task.prioridade == 2 ? 'ALTA' : task.prioridade == 1 ? 'MEDIA' : 'BAIXA'}</td>
                            <td>{task.status == 0 ? (<a className={style.concluir} onClick={()=>{concluir(task.idTarefa)}}>Concluir</a>) : (<span>Tarefa concluída</span>)}</td>
                            <td><a className={style.excluir} onClick={()=>{deletar(task.idTarefa)}}>Excluir</a></td>
                        </tr>))}
                    </tbody>
                </table>
                {tarefas.length == 0 && (<div className={style.defaultmessage}>Sem registros de tarefa</div>)}
                <div className={style.pageBtns}>
                    <button className={style.btnpage} disabled={currentPage <= 1} onClick={() => {downPage()}}>{"<< anterior"}</button> {currentPage} <button className={style.btnpage} onClick={()=>{upPage()}}>{"próxima >>"}</button>
                </div>
            </div>
        </div>
    )
}

export default Home;