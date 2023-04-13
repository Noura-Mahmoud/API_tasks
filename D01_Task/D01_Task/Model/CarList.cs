using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_D02_Task2.Models
{
    public class CarList
    {
        public static List<Car> cars = new List<Car>()
        {
            new Car(){ id=1,Color="Red",Model="Cerato",Type="Gas"},
            new Car(){ id=2,Color="Silver",Model="Ciaz",Type="Gas"},
            new Car(){ id=3,Color="Black",Model="Solaris",Type="Gas"},
        };
    }
}