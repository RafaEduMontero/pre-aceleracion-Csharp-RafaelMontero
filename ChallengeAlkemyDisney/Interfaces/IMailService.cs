using ChallengeAlkemyDisney.Models;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Interfaces
{
    public interface IMailService
    {
        Task SendMail(User user);
    }
}