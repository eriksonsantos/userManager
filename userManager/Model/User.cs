using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userManager.Model
{
    public class Users
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string createdDate { get; set; }
        public int Telephone { get; set; }

        public Users(string name, string address,int telephone)
        {
            Name = name;
            Address = address;
            this.createdDate = DateTime.Now.ToString();
            Telephone = telephone;
        }
        public Users() { }
    }
}
