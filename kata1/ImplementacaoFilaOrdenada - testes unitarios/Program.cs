using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImplementacaoFilaOrdenada
{
    class Program
    {
        enum Prioridades
        {
            critica,
            alta,
            media,
            baixa
        };

        struct Paciente
        {
            public string nome;
            public int idade;
            public Prioridades prioridade;

            public Paciente(string nome, int idade, Prioridades prioridade)
            {
                this.nome = nome;
                this.idade = idade;
                this.prioridade = prioridade;

            }
        }

        static void Main(string[] args)
        {
            ArrayList Pcritica = new ArrayList();
            ArrayList Palta = new ArrayList();
            ArrayList Pmedia = new ArrayList();
            ArrayList Pbaixa = new ArrayList();
            int respostaMenu = 0;

            Paciente ValidacaoDasRegras (Paciente paciente)
            {
                if (paciente.idade >= 60 && paciente.prioridade == Prioridades.media){   paciente.prioridade = Prioridades.alta;    } //regra 1
                else if (paciente.idade < 18){    paciente.prioridade--;    } //regra 2

                return paciente;
            }

            ArrayList EnviarParaFila(Paciente paciente)
            {
                //Tratamentos das regras de negócio
                Paciente pacienteValidado = ValidacaoDasRegras(paciente);

                //alocação nas filas
                switch (pacienteValidado.prioridade)
                {
                    case Prioridades.critica:
                        Pcritica.Add(pacienteValidado);
                        break;
                    case Prioridades.alta:
                        Palta.Add(pacienteValidado);
                        break;
                    case Prioridades.media:
                        Pmedia.Add(pacienteValidado);
                        break;
                    default:
                        Pbaixa.Add(pacienteValidado);
                        break;
                }

                ArrayList finalList = new ArrayList();

                finalList.AddRange(Pcritica);
                finalList.AddRange(Palta);
                finalList.AddRange(Pmedia);
                finalList.AddRange(Pbaixa);

                return finalList;
            }

            void testarValidacao()
            {
                Paciente pacienteTeste = new Paciente("Hugo", 17, Prioridades.media);

                var resultado = ValidacaoDasRegras(pacienteTeste);

                Paciente pacienteEsperado = new Paciente("Hugo", 17, Prioridades.alta);

                Assert.Equal(pacienteEsperado, resultado);
            }

            void testarEnvio()
            {
                ArrayList listaPacientesDesordenada = new ArrayList();

                listaPacientesDesordenada.Add(new Paciente("Hugo", 17, Prioridades.media));
                listaPacientesDesordenada.Add(new Paciente("Daniel", 30, Prioridades.media));
                listaPacientesDesordenada.Add(new Paciente("Jean", 23, Prioridades.baixa));
                listaPacientesDesordenada.Add(new Paciente("Thiago", 64, Prioridades.media));

                var resultado = new ArrayList();

                foreach (Paciente paciente in listaPacientesDesordenada)
                {
                    resultado = EnviarParaFila(paciente);
                }

                ArrayList listaPacientesCorreta = new ArrayList();

                listaPacientesCorreta.Add(new Paciente("Hugo", 17, Prioridades.alta));
                listaPacientesCorreta.Add(new Paciente("Thiago", 64, Prioridades.alta));
                listaPacientesCorreta.Add(new Paciente("Daniel", 30, Prioridades.media));
                listaPacientesCorreta.Add(new Paciente("Jean", 23, Prioridades.baixa));

                Assert.Equal(listaPacientesCorreta, resultado);
            }

            testarValidacao();
            testarEnvio();

        }
    }
}
