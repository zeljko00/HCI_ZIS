namespace HCI_ZIS_Library.Model
{
    public class Refferal
    {
       public int Id { get; set; }
        public int Examination { get; set; }
        public HealthService HealthService { get; set; }

        public Refferal(int id, int examination, HealthService healthService)
        {
            Id = id;
            Examination = examination;
            HealthService = healthService;
        }
    }
}