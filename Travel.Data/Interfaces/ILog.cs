using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Data.Interfaces
{
    public interface ILog
    {
       bool  AddLog(string content, string type,string emailCreator, string classContent);
    }
}
