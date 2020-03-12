using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tenaris_planta.Core.DTO;
using tenaris_planta.Data.DAL;

namespace tenaris_planta.Services
{
    public class EmailService
    {
        public static object Get(string id = null)
        {
            return EmailModel.Get(id);
        }

        public static object Update(string id, JObject updateObject)
        {
            if (updateObject != null && id != null)
            {
                if (updateObject["doc"] != null)
                {
                    return EmailModel.Update(id, updateObject);
                }
            }
            return null;
        }
    }
}
