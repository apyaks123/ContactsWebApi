using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MVC.Models
{
    public class mvcContactModel
    {

        public int Id { get; set; }


        [Required]
        [Display(Name = "First Name")]

        public string firstName { get; set; }

        [Display(Name = "Last Name")]

        public string lastName { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
/*        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
*/       
        
        
        [RegularExpression(@"^\(?([0-9]{3})\)??([0-9]{3})?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string Phone { get; set; }

        [Display(Name = "Office Email")]

        public string Email { get; set; }




        
       
    }
}