using System;

namespace EmailBox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Email Box | Criado por: Felipe Sena");

            string servidor = null;
            string usuario = null;
            string senha = null;
            string porta = null;
            string usarSSL = null;

            bool SSL;
            int portaPOP3;

            while (string.IsNullOrEmpty(servidor))
            {
                Console.Write("Digite o endereço do servidor POP3: ");
                servidor = Console.ReadLine();
            }


            while (string.IsNullOrEmpty(usuario))
            {
                Console.Write("Digite o endereço de email: ");
                usuario = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(senha))
            {
                Console.Write("Digite a senha: ");
                senha = Console.ReadLine();
            }

            Console.Write("Digite o número da porta: ");
            porta = Console.ReadLine();
            int.TryParse(porta, out portaPOP3);

            Console.Write("Usar SSL? (true/false): ");
            usarSSL = Console.ReadLine();
            bool.TryParse(usarSSL, out SSL);

            var emails = new ServidorEmail(servidor, usuario, senha, portaPOP3, SSL).LerCaixaDeEntrada();
            if (emails.ProcessOk)
            {
                foreach (Email email in emails.Emails)
                {
                    Console.WriteLine(email);
                }
            }
        }
    }
}
