using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Robo
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new AutomacaoWeb();
            web.AbrirSite();
        }

    } 
}
