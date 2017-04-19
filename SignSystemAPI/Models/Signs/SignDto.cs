namespace SignSystem.API.Models.Signs
{
    public class SignDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public double AspectRatio => Width / (Height==0 ? 1.0:(double)Height) ;
    }
}
