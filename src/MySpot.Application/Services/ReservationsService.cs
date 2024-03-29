using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public sealed class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpotRepository
            .GetAll()
            .SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            Date = x.Date.Value.Date
        });

    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public Guid? Create(CreateReservation command)
    {
        try
        {
            var (spotId, reservationId, employeeName, licencePlate, date) = command;
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(spotId); 

            if (weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(reservationId, employeeName, licencePlate, new Date(date));

            weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return reservation.Id;
        }
        catch (CustomException)
        {
            return default;
        }
    }

    public bool Update(ChangeReservationLicencePlate command)
    {
        try
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                .SingleOrDefault(x => x.Id == reservationId);

            if (reservation is null)
            {
                return false;
            }

            reservation.ChangeLicencePlate(command.LicencePlate);
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            return false;
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId id)
        => _weeklyParkingSpotRepository.GetAll()
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));

    private DateTime CurrentDate() => _clock.Current();
}