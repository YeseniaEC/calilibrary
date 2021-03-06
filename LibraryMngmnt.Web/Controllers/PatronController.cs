﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.Models.Patron;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Web.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatron _patron;

        public PatronController ( IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index( )
        {
            var allPatrons = _patron.GetAll ( );
            var patronModels = allPatrons.Select ( p => new PatronDetailModel // essentially a mapping (think of .MAP )
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name

            } ).ToList();
            var model = new PatronIndexModel ( )
            {
                Patrons = patronModels
            };

            return View ( model );
        }

        public IActionResult Detail(int id)
        {
            var patron = _patron.Get ( id );

            var model = new PatronDetailModel
            {
                LastName=patron.LastName,
                FirstName=patron.FirstName,
                Address=patron.Address,
                HomeLibraryBranch=patron.HomeLibraryBranch.Name,
                MemberSince=patron.LibraryCard.Created,
                OverdueFees=patron.LibraryCard.Fees,
                LibraryCardId=patron.LibraryCard.Id,
                Telephone=patron.TelephoneNumber,
                AssetsCheckedOut=_patron.GetCheckouts ( id ).ToList ( )??new List<Checkout> ( ),
                CheckoutHistory=_patron.GetCheckoutHistory ( id ),
                Holds=_patron.GetHolds ( id )
            };

            return View ( model );
        }
    }
}
