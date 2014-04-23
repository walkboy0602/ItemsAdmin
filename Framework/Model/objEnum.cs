using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    public enum ReferenceType
    {
        None = 0,
        MainCategory = 1,
        CategoryLv1 = 2,
        CategoryLv2 = 3,
        CategoryLv3 = 4
    }

    public enum ListingStatus
    {
        New = 'N',
        Draft = 'D',
        PendingReview = 'R',
        Published = 'P',
        Suspended = 'S'
    }
}
