using Models.Enums;

namespace Models
{
    public class UniFile
    {
        public int Id { get; set; }
        public string File { get; set; } = null!;
        public Filetype ContentType  { get; set; }

        //Image0 : Title Logo(png)
        //Image1 : Internal Map

        //ImagesSlides
        //Image2 : SignIn Img
        //Image3 : Forgot Password
        //Image4 : Slide3
        //Image5 : Slide4
        //Image6 : Slide5
        //Image7 : Slide7
        //Image8 : Slide8

        //More...
    }
}
