namespace MySpot.Api.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public DateTime Date { get; set; }

    public InvalidReservationDateException(DateTime date)
        : base($"Reservation date {date} is invalid.")
    {
        Date = date;
    }
}