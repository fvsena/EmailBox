using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailBox
{
    public class ServidorEmail
    {
        private string ServidorEmailPop3;
        private string Usuario;
        private string Senha;
        private int Porta;
        private bool UsarSSL;

        public ServidorEmail(string servidorEmailPop3, string usuario, string senha, int porta, bool usarSSL)
        {
            this.ServidorEmailPop3 = servidorEmailPop3;
            this.Usuario = usuario;
            this.Senha = senha;
            this.Porta = porta;
            this.UsarSSL = usarSSL;
        }

        public LerCaixaResult LerCaixaDeEntrada()
        {
            var result = new LerCaixaResult();
            try
            {
                using (Pop3Client client = new Pop3Client())
                {
                    client.Connect(ServidorEmailPop3, Porta, UsarSSL);
                    client.Authenticate(Usuario, Senha);
                    List<Message> mensagens = new List<Message>();
                    for (int i = client.GetMessageCount(); i > 0; i--)
                    {
                        mensagens.Add(client.GetMessage(i));
                    }

                    result.Emails = new List<Email>();
                    foreach (Message mensagem in mensagens)
                    {
                        var destinatarios = new List<string>();
                        var copia = new List<string>();

                        foreach (var dest in mensagem.Headers.To)
                        {
                            destinatarios.Add(dest.Address);
                        }

                        foreach (var cc in mensagem.Headers.Cc)
                        {
                            copia.Add(cc.Address);
                        }

                        string body = null;
                        MessagePart mpText = mensagem.FindFirstPlainTextVersion();
                        MessagePart mpHtml = mensagem.FindFirstHtmlVersion();
                        if (mpText != null)
                            body = mpText.GetBodyAsText();
                        else if (mpHtml != null)
                            body = mpHtml.GetBodyAsText();

                        result.Emails.Add(new Email()
                        {
                            Data = mensagem.Headers.Date,
                            Remetente = mensagem.Headers.From.Address,
                            Para = destinatarios,
                            CC = copia,
                            Assunto = mensagem.Headers.Subject,
                            Corpo = body
                        });
                    }
                }
                result.ProcessOk = true;
            }
            catch (Exception ex)
            {
                result.ProcessOk = false;
                result.MsgCatch = ex.Message;
            }
            return result;
        }
    }
}
