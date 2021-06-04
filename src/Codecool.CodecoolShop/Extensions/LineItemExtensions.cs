using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;

namespace Codecool.CodecoolShop.Extensions
{
    public static class LineItemExtensions
    {
        public static bool IsNull(this LineItem item)
        {
            return item == null;
        }
    }
}
