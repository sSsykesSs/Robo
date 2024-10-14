using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Robo
{
    class AutomacaoWeb
    {
        public IWebDriver driver;

        public AutomacaoWeb()
        {
            driver = new ChromeDriver();
        }

        public void AbrirSite()
        {
            driver.Navigate().GoToUrl("https://www.4devs.com.br/gerador_de_pessoas");
            Thread.Sleep(2000);
            FecharCookie();

            driver.FindElement(By.Id("txt_qtde")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("txt_qtde")).SendKeys("10");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("bt_gerar_pessoa")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='area_resposta_json']/div/button[1]")).Click();
            Thread.Sleep(2000);

            SalvarDadosEmCSV();
        }

        public void FecharCookie()
        {
            driver.FindElement(By.Id("cookiescript_close")).Click();
        }

        public void SalvarDadosEmCSV()
        {
            string downloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var latestJsonFile = Directory.GetFiles(downloadFolderPath, "*.json")
                                          .Select(f => new FileInfo(f))
                                          .OrderByDescending(f => f.LastWriteTime)
                                          .FirstOrDefault();

            if (latestJsonFile == null)
            {
                Console.WriteLine("Nenhum arquivo JSON encontrado na pasta Downloads.");
                return;
            }

            string csvFilePath = Path.Combine(Environment.CurrentDirectory, "PessoasGeradas.csv");

            try
            {
                string jsonData = File.ReadAllText(latestJsonFile.FullName);
                JArray pessoas = JArray.Parse(jsonData);

                using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("Nome;Idade;CPF;RG;Data de Nascimento;Sexo;Signo;Mãe;Pai;Email;Senha;CEP;Endereço;Número;Bairro;Cidade;Estado;Telefone Fixo;Celular;Altura;Peso;Tipo Sanguíneo");

                    foreach (var pessoa in pessoas)
                    {
                        string nome = (string)pessoa["nome"];
                        int idade = (int)pessoa["idade"];
                        string cpf = (string)pessoa["cpf"];
                        string rg = (string)pessoa["rg"];
                        string dataNascimento = (string)pessoa["data_nasc"];
                        string sexo = (string)pessoa["sexo"];
                        string signo = (string)pessoa["signo"];
                        string mae = (string)pessoa["mae"];
                        string pai = (string)pessoa["pai"];
                        string email = (string)pessoa["email"];
                        string senha = (string)pessoa["senha"];
                        string cep = (string)pessoa["cep"];
                        string endereco = (string)pessoa["endereco"];
                        string numero = (string)pessoa["numero"];
                        string bairro = (string)pessoa["bairro"];
                        string cidade = (string)pessoa["cidade"];
                        string estado = (string)pessoa["estado"];
                        string telefoneFixo = (string)pessoa["telefone_fixo"];
                        string celular = (string)pessoa["celular"];
                        string altura = (string)pessoa["altura"];
                        float peso = (float)pessoa["peso"];
                        string tipoSanguineo = (string)pessoa["tipo_sanguineo"];

                        writer.WriteLine($"{nome};{idade};{cpf};{rg};{dataNascimento};{sexo};{signo};{mae};{pai};{email};{senha};{cep};{endereco};{numero};{bairro};{cidade};{estado};{telefoneFixo};{celular};{altura};{peso};{tipoSanguineo}");
                    }
                }

                Console.WriteLine($"Dados salvos em: {csvFilePath}");
                AbrirArquivo(csvFilePath); // Chama o método para abrir o arquivo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o arquivo JSON: {ex.Message}");
            }
        }

        private void AbrirArquivo(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir o arquivo: {ex.Message}");
            }
        }
    }
}






