using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigmade.Domain.Models
{
    public class Main
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Child> Childs {  get; set; }
    }

    public class Child
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public int MainId { get; set; }
        public Main Main {  get; set; }

        public ICollection<SubChild> SubChilds { get; set; }
    }

    public class SubChild
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Child> Childs { get; set; }
    }
}
