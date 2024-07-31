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
        //Image2 : Update

           //ImagesSlides
        //Image3 : SignIn Img
        //Image4 : Forgot Password
        //Image5 : Slide3
        //Image6 : Slide4
        //Image7 : Slide5
        //Image8 : Slide7
        //Image9 : Slide8

        //More...
    }
}
