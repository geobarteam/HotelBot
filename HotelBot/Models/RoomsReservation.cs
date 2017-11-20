using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace HotelBot.Models
{
    public enum BedSizeOptions
    {
        King,
        Queen,
        Single,
        Double
    }

    public enum AmenitiesOptions
    {
        Kitchen,
        ExtraTowels,
        GymAccess,
        Wifi
    }

    [Serializable]
    public class RoomsReservation
    {
        public BedSizeOptions? BedSize;
        public int? NumberOfOccupants;
        public DateTime? CheckinDate;
        public int? NumberOfDaysToStay;
        public List<AmenitiesOptions> Amenities;

        public static IForm<RoomsReservation> BuildForm()
        {
            return new FormBuilder<RoomsReservation>()
                .Message("Welcome to the hotel reservation bot!")
                .Build();
        }
    }
}