namespace Teatastic.Models
{
    public class Tea_Function
    {
        public int TeaId { get; set; }
        public Tea Tea { get; set; }

        public int FunctionId { get; set; }
        public Function Function { get; set; }

    }
}
