using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using Framework.DAL;

namespace Framework.BLL
{
    class ReferenceBL
    {
        ReferenceDL _ReferenceDL;

        public ReferenceBL()
        {
            _ReferenceDL = new ReferenceDL();
        }

        public List<objReference> GetReferenceList(ReferenceType type)
        {
            return _ReferenceDL.GetReferenceList(type);
        }

    }
}
