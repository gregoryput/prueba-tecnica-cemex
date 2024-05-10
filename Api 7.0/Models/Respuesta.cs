namespace Api_7._0.Models
{
    public class Respuesta
    {

        public bool IsSuccess { get; set; } = true;
        public string DisplayMessage { get; set; }
        public object Result { get; set; }
        public List<string> ErrorMessages { get; set; }


    }
}