using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using CinemaApp.Resources;

namespace CinemaApp.Models
{
    public class IndexViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public List<ReservationViewModel> Reservations { get; set; }
    }

    public class ReservationViewModel
    {
        public int ID { get; set; }
        public Showing Showing { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<PlacePosition> Places { get; set; }
        public string CancelUrl { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i potwierdzenia hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePersonalDataViewModel
    {
        [Required]
        [Display(ResourceType =typeof(Strings), Name ="Name")]
        public string Name { get; set; }

        [Required]
        [Display(ResourceType =typeof(Strings), Name ="Surname")]
        public string Surname { get; set; }
    }
}