using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.Logs
{
    public static class WriteLog
    {
        public static void SimpleWrite(string path, object res)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(res);

            using (StreamWriter w = System.IO.File.AppendText(path))
            {
                w.WriteLine(json);
                w.Close();
            }

        }
    }
}
