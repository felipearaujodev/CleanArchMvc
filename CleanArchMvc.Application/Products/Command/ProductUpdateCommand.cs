using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Command
{
    public class ProductUpdateCommand : ProductCommand
    {
        public int Id { get; set; }
    }
}
