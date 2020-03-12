using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tenaris_planta.Data.DAL;

namespace tenaris_planta.Services
{
    public class EmailService
    {
        public static object Get(string id = null)
        {
            return ((JObject)EmailModel.Get(id))["hits"];
        }

        public static JObject GetPriority()
        {
            JObject rtn = ((JObject)EmailModel.GetPriority()["hits"]);
            JArray rtnHitItems = new JArray();

            if (rtn != null)
            {
                foreach (JObject hitItem in rtn["hits"])
                {
                    if (hitItem != null)
                    {
                        List<string> tagStringList = hitItem["_source"]["tags"].ToObject<List<string>>();

                        if (tagStringList != null)
                        {
                            List<string> auxLowerTagStringList = tagStringList.ConvertAll(c => c.ToLower());
                            if(!auxLowerTagStringList.Any(a => a == "done"))
                            {
                                rtnHitItems.Add(hitItem);
                            }
                        }
                    }
                }
                rtn["hits"] = rtnHitItems;
            }

            return rtn;
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
