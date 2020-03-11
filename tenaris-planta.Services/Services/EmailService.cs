using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenaris_planta.Data.DAL;

namespace tenaris_planta.Services
{
    public class EmailService
    {
        private EmailModel _emailModel;
        
        public List<object> Get(Nullable<int> id = null)
        
        
        
        {
            _emailModel = new EmailModel();
            return _emailModel.Get();
        }
    }
}
