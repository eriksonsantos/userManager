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

        public Users(int id, string name, string address, DateTime createdDate, int telephone)
        {
            Id = id;
            Name = name;
            Address = address;
            this.createdDate = createdDate.ToString();
            Telephone = telephone;
        }
        public Users()
        {

        }
    }
}
