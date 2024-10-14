using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo
{
    class AutomacaoWeb
    {
        public IWebDriver driver;

        public AutomacaoWeb()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public void AbrirSite()
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.4devs.com.br/gerador_de_pessoas");

                driver.FindElement(By.Id("txt_qtde")).Clear();

                driver.FindElement(By.Id("txt_qtde")).SendKeys("10");

                driver.FindElement(By.Id("cookiescript_aceppt")).Click();

                driver.FindElement(By.Id("bt_gerar_pessoa")).Click();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Elemento não encontrado: " + ex.Message);
            }
            catch (WebDriverException ex)
            {
                Console.WriteLine("Erro relacionado ao WebDriver: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                
                if (driver != null)
                {
                    driver.FindElement(By.Id("cookiescript_aceppt")).Click();

                    driver.FindElement(By.Id("bt_gerar_pessoa")).Click();
                }
            }
        }
    }
}

