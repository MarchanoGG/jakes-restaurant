using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controllers
{
    internal class ctlMain
    {
        public static ctlDiningTable diningtable { get; set; }
        public static ctlReservation reservation { get; set; }
        public static ctlUsers users { get; set; }
        public static TctlProducts products { get; set; }
        public static ctlOpeningTimes openingtimes { get; set; }
        public ctlMain()
        {
            diningtable = new ctlDiningTable();
            reservation = new ctlReservation();
            users = new ctlUsers();
            products = new TctlProducts();
            openingtimes = new ctlOpeningTimes();
        }
    }
}
