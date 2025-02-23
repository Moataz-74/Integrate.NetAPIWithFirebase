namespace APIWithFireBase.Models
{
    public class BabyTemperature 
    {
        public int Id { get; set; } 
        public double Temperature { get; set; }  
        public const float MinimumRange = 35.5f;

        public const float MiximumRange = 38.5f;
        public bool IsNormal { get; set; }  
    }
}

