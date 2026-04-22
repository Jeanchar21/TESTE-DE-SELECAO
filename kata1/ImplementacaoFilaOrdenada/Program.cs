using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            void EnviarParaFila(Paciente paciente)
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
            }

             void MostrarLista ()
            {
                Console.Clear();
                Console.WriteLine("Fila dos pacientes: \n");
                foreach(Paciente paciente in Pcritica)
                {
                    Console.WriteLine("-----------------------------------\n");
                    Console.WriteLine($"Nome: {paciente.nome}\n");
                    Console.WriteLine($"Idade: {paciente.idade}\n");
                    Console.WriteLine($"Prioridade: {paciente.prioridade}\n");
                    Console.WriteLine("-----------------------------------\n");
                }
                foreach (Paciente paciente in Palta)
                {
                    Console.WriteLine("-----------------------------------\n");
                    Console.WriteLine($"Nome: {paciente.nome}\n");
                    Console.WriteLine($"Idade: {paciente.idade}\n");
                    Console.WriteLine($"Prioridade: {paciente.prioridade}\n");
                    Console.WriteLine("-----------------------------------\n");
                }
                foreach (Paciente paciente in Pmedia)
                {
                    Console.WriteLine("-----------------------------------\n");
                    Console.WriteLine($"Nome: {paciente.nome}\n");
                    Console.WriteLine($"Idade: {paciente.idade}\n");
                    Console.WriteLine($"Prioridade: {paciente.prioridade}\n");
                    Console.WriteLine("-----------------------------------\n");
                }
                foreach (Paciente paciente in Pbaixa)
                {
                    Console.WriteLine("-----------------------------------\n");
                    Console.WriteLine($"Nome: {paciente.nome}\n");
                    Console.WriteLine($"Idade: {paciente.idade}\n");
                    Console.WriteLine($"Prioridade: {paciente.prioridade}\n");
                    Console.WriteLine("-----------------------------------\n");
                }
                Console.ReadKey();
            }

            void registrarPaciente()
            {
                Console.Clear();
                Paciente paciente = new Paciente();
                int conversorPrioridade;
                Console.WriteLine("-----------------------------------\n");
                Console.WriteLine("Nome:");
                paciente.nome = Console.ReadLine();
                Console.WriteLine("idade:");
                paciente.idade = int.Parse(Console.ReadLine());
                Console.WriteLine("Prioridade\n 1-CRÍTICA \n 2-ALTA \n 3-MÉDIA \n 4-BAIXA");
                conversorPrioridade = int.Parse(Console.ReadLine());
                if ((conversorPrioridade - 1) >= 0 && (conversorPrioridade - 1) < 3)
                {
                    paciente.prioridade = (Prioridades)Enum.Parse(typeof(Prioridades), $"{conversorPrioridade - 1}", true);
                } 
                else
                {
                    paciente.prioridade = Prioridades.baixa;
                }
                        
                EnviarParaFila(paciente);
            }

            void menu()
            {
                Console.Clear();
                Console.WriteLine("-----------------MENU-----------------\n");
                Console.WriteLine("Bem-vindo!\n");
                Console.WriteLine("1- Registrar paciente\n");
                Console.WriteLine("2- Ver fila\n");
                Console.WriteLine("3- Sair\n");
                Console.WriteLine("--------------------------------------\n");
                respostaMenu = Int32.Parse(Console.ReadLine());
                switch (respostaMenu)
                {
                    case 1:
                        registrarPaciente();
                        menu();
                        break;
                    case 2:
                        MostrarLista();
                        menu();
                        break;
                    default:
                        Console.WriteLine("Saindo...");
                        Console.ReadKey();
                        break;
                }
            }

            do
            {
                menu();
            } while (respostaMenu > 2);

        }
    }
}
