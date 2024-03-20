using SistemaBico.Web.Models.Filters;
using SistemaBico.Web.Util;

namespace SistemaBico.Web.Models.Reponse
{
    public class ProfileWorkerProfessionalPaginationResponse : FilterPaginatedBaseModel
    {
        public List<WorkerDoneResponse> WorkerDone { get; set; }
        public ProfessionalProfileDto ProfessionalProfile { get; set; }
        public ThreeAvaliationResponse ThreeAvaliation { get; set; }
        public string TextReputationGeneral { get; set; }
        public decimal PercentReputationGeneral { get; set; }
        public string TextReputationDeadline { get; set; }
        public decimal PercentReputationDeadline { get; set; } = 0;
        public string TextReputationQuality { get; set; }
        public decimal PercentReputationQuality { get; set; } = 0;
        public string TextReputationCommunication { get; set; }
        public decimal PercentReputationCommunication { get; set; } = 0;
        public int CountRegister { get; set; }
        public int PagesSize { get; set; }


        public void SetReputation()
        {
            RegraReputationGeneral();
            RegraReputationDeadline();
            RegraReputationQuality();
            RegraReputationCommunication();
        }


        private void RegraReputationGeneral()
        {
            string reputacao = "";

            if (double.Parse(ProfessionalProfile.Avaliation) >= 4)
                reputacao = "Esse prestador possui ótimas recomendações.";
            else
            {
                if (double.Parse(ProfessionalProfile.Avaliation) > 2.5 && double.Parse(ProfessionalProfile.Avaliation) < 4)
                    reputacao = "Esse prestador tem uma reputação média sobre os seus serviços.";
                else
                {
                    if (WorkerDone.Count == 0)
                    reputacao = "Esse prestador não possui avaliações.";
                    else
                    reputacao = "Esse prestador está com reputação abaixo do esperado.";
                }
            }

            PercentReputationGeneral = ConvertGeneric.CalculaPercent(ProfessionalProfile.Avaliation, 5);
            TextReputationGeneral = reputacao;
        }

        private void RegraReputationDeadline()
        {
            string reputacao = "";

            if (ThreeAvaliation == null)
                reputacao = "Esse prestador não possui avaliações.";
            else
            {
                var mediaReputacao = Math.Round((ThreeAvaliation.Deadline / ThreeAvaliation.NumberAvaliation),2);

                if (mediaReputacao >= 4)
                    reputacao = "Esse prestador entrega no prazo estipulado.";
                else
                    if (mediaReputacao > 2 && mediaReputacao < 4)
                        reputacao = "Esse prestador pode atrasar em algumas entregas.";
                    else
                    {
                        reputacao = "Esse prestador atrasa com uma certa frequência as suas entregas.";
                    }

                PercentReputationDeadline = ConvertGeneric.CalculaPercent(mediaReputacao.ToString(), 5);
            }

            TextReputationDeadline = reputacao;
        }

        private void RegraReputationQuality()
        {
            string reputacao = "";

            if (ThreeAvaliation == null)
                reputacao = "Esse prestador não possui avaliações.";
            else
            {
                var mediaReputacao = Math.Round((ThreeAvaliation.Quality / ThreeAvaliation.NumberAvaliation),2);

                if (mediaReputacao >= 4)
                    reputacao = "Esse prestador tem uma ótima qualidade na sua prestação de serviço.";
                else
                    if (mediaReputacao > 2 && mediaReputacao < 4)
                        reputacao = "Esse prestador tem uma reputação mediana na sua qualidade.";
                    else
                    {
                        reputacao = "Esse prestador tem uma reputação abaixo do esperado na qualidade do seu serviço.";
                    }

                PercentReputationQuality = ConvertGeneric.CalculaPercent(mediaReputacao.ToString(), 5);
            }

            TextReputationQuality = reputacao;
        }

        private void RegraReputationCommunication()
        {
            string reputacao = "";

            if (ThreeAvaliation == null)
                reputacao = "Esse prestador não possui avaliações.";
            else
            {
                var mediaReputacao = Math.Round((ThreeAvaliation.Communication / ThreeAvaliation.NumberAvaliation),2);

                if (mediaReputacao >= 4)
                    reputacao = "Esse prestador tem uma ótima comunicação.";
                else
                    if (mediaReputacao > 2 && mediaReputacao < 4)
                        reputacao = "Esse prestador tem uma reputação mediana em relação a sua comunicação.";
                    else
                    {
                        reputacao = "Esse prestador tem uma reputação abaixo do esperado na sua comunicação";
                    }

                PercentReputationCommunication = ConvertGeneric.CalculaPercent(mediaReputacao.ToString(), 5);
            }

            TextReputationCommunication = reputacao;
        }
    }

}
