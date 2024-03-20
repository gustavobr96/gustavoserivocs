using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IPlanDapperRepository
    {
        Task DeletePlansVenciment();
    }
}
