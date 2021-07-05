using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Oligert Crroj
/// Creted on :7/1/2021 10:24 Am
/// Last changes made: 7/5/2021 12:04 am 
/// </summary>


namespace CupCakeShop.Models
{
    public class Cake : Entity
    {
        #region Datatypes
        //Validation / Changed the Display name 
        [Display(Name = "CupCake Name")]
        [Required(ErrorMessage = "You cannot leave the CupCake Name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string CupCakeName { get; set; }

        //Validation / Changed the Display name 
        //Change the datatype to Currency
        [Display(Name = "CupCake Price")]
        [Required(ErrorMessage = "You cannot leave the CupCake Price blank.")]
        [DataType(DataType.Currency)]
        public double CupCakePrice { get; set; }
        #endregion

        #region Random Picture selector
        //Created the function on this class 
        //Faster to get the function from class to ..cshtml page
        //Picking pictures is only for testing purpose


        public static string RandomPicture( )
        {
            string[] picLink = { "/lib/CupCakeImages/cupcakes1.jpg", "/lib/CupCakeImages/cupcakes2.jpg" , "/lib/CupCakeImages/cupcakes3.jpg" ,
                                "/lib/CupCakeImages/cupcakes4.jpg" ,"/lib/CupCakeImages/cupcakes5.jpg" ,"/lib/CupCakeImages/cupcakes6.jpg" };
         
            Random rndr = new Random();
            int numb = rndr.Next(6);
            return picLink[numb];

        }
        #endregion

    }
}
