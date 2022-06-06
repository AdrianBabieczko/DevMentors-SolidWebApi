namespace MySpot.Api.Exceptions;

public class InvalidEntityIdException: CustomException
{
    public object Id { get;}

    public InvalidEntityIdException(object id) : base($"Cannot set: {id} as entity identifier.")
        => Id = id;
}