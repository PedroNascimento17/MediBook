using System.Net.Mail;
using System.Net;
using System.ServiceProcess;

public partial class ConsultaService : ServiceBase
{
    private Timer timer;

    public ConsultaService()
    {
        InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
        // Inicializa o timer para verificar a cada dia (86400000 ms = 1 dia)
        timer = new Timer(CheckConsultas, null, 0, 86400000);
    }

    protected override void OnStop()
    {
        timer.Dispose();
    }

    private void CheckConsultas(object state)
    {
        // Aqui você faria a lógica para verificar as consultas
        List<Consulta> consultas = GetConsultasPendentes();

        foreach (Consulta consulta in consultas)
        {
            // Verifica se a consulta é em 1 dia
            if (consulta.DataConsulta.Date == DateTime.Today.AddDays(1))
            {
                EnviarEmailConsulta(consulta);
            }
        }
    }

    private List<Consulta> GetConsultasPendentes()
    {
        // Exemplo de implementação: busca consultas agendadas no banco de dados
        // ou em qualquer outra fonte de dados
        // Aqui você precisará implementar a lógica de busca de consultas pendentes
        // Este método deve retornar uma lista de objetos Consulta
        throw new NotImplementedException();
    }

    private void EnviarEmailConsulta(Consulta consulta)
    {
        // Configurações do servidor SMTP e credenciais (exemplo para Gmail)
        string host = "smtp.gmail.com";
        int port = 587;
        string username = "seu_email@gmail.com";
        string password = "sua_senha";

        // Configuração do cliente SMTP
        SmtpClient client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };

        // Constrói a mensagem de email
        MailMessage message = new MailMessage();
        message.From = new MailAddress(username);
        message.To.Add(new MailAddress(consulta.PacienteEmail));
        message.Subject = "Lembrete de consulta";
        message.Body = $"Olá {consulta.PacienteNome}, lembrete de que sua consulta está marcada para amanhã.";

        try
        {
            // Envia o email
            client.Send(message);
            Console.WriteLine("Email enviado com sucesso para " + consulta.PacienteEmail);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao enviar email: " + ex.Message);
        }
        finally
        {
            // Limpa recursos
            message.Dispose();
            client.Dispose();
        }
    }
}

// Classe exemplo para representar uma consulta
public class Consulta
{
    public string PacienteNome { get; set; }
    public string PacienteEmail { get; set; }
    public DateTime DataConsulta { get; set; }
    // Outros campos relevantes para a consulta
}