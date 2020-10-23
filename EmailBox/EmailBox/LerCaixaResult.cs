using System.Collections.Generic;

namespace EmailBox
{
    public class LerCaixaResult
    {
        public bool ProcessOk { get; set; }
        public string Msg { get; set; }
        public string MsgCatch { get; set; }
        public List<Email> Emails { get; set; }
    }
}
