﻿using System;
using System.Collections.Generic;
using LibraryData.Models;

namespace LibraryData
{
    public interface ICheckout
    {
        void Add ( Checkout newCheckout );

        IEnumerable<Checkout> GetAll ( );
        IEnumerable<CheckoutHistory> GetCheckoutHistory ( int id );
        IEnumerable<Hold> GetCurrentHolds ( int id );
        
        Checkout GetById ( int checkoutId );
        Checkout GetLatestCheckout ( int assetId );
        string GetCurrentCheckedoutPatron ( int assetId ); 
        string GetCurrentHoldPatronName ( int id );
        DateTime GetCurrentHoldPlaced ( int id );
        bool IsCheckedOut ( int id );

        void CheckOutItem ( int assetId, int libraryCardId );
        void CheckInItem ( int assetId );        
        void PlaceHold ( int assetId, int LibraryCardId );
        void MarkLost ( int assetId );
        void MarkFound ( int assetId );
    }
}
