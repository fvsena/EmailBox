using System.Collections.Generic;

namespace EmailBox
{
    public class Email
    {
        public string Data { get; set; }
        public string Assunto { get; set; }
        public string Remetente { get; set; }
        public List<string> CC { get; set; }
        public List<string> Para { get; set; }
        public string Corpo { get; set; }

        public override string ToString()
        {
            return $"Data: {Data} | De: {Remetente} | Assunto: {Assunto}";
        }
    }
}
