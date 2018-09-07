using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Models
{
    public class MVCRegisterModel
    {
        [Required]
        [Display(Name = "User Name:")]
        [Remote("IsUserNameAvailable","Register",ErrorMessage="User Name  Already Exists. Username ")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email:")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        [StringLength(10, ErrorMessage = "Maximum length is {1}")]
       // [StringLength(10, ErrorMessage = "Maximum length is {2}")]
        //[RegularExpression(@"0-9", ErrorMessage = "Use numbers only please")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Sex Required")]
        [Display(Name = "Sex :")]
        public string Gender { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public string Address { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage="Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name="Password:")]
        public string Password { get; set; }

        [Required(ErrorMessage="Confirmation Password is required.")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage="Password and Confirm Password must match")]
        public string ConfirmPassword {get;set;}
    }
    public class Sex
    {
        public string ID { get; set; }
        public string Type { get; set; }
    }
    
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            this.States = new HashSet<State>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<State> States { get; set; }
    }
    public partial class State
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public State()
        {
            this.Cities = new HashSet<City>();
        }

        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> Cities { get; set; }
        public virtual Country Country { get; set; }
    }
    public partial class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }

        public virtual State State { get; set; }
    }
    public class MVCLoginModel
    {
        [Required(ErrorMessage = "UserName is required.")]
        [Display(Name = "User Name:")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }

    public class MVCClientModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name:")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Required(ErrorMessage="Country required")]
        public string Country { get; set; }
    }
}