using System.Numerics;
using WebSeeSharpers.Services.SeatService;
using Xunit;

namespace UnitTests.Services.SeatAllocateService;

public class SeatTests
{
    [Fact]
    public void Number_CanSetSeatNumber_NumberIsSet()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 5));

        // Act
        seat.Number = 14;

        // Assert
        Assert.Equal(14, seat.Number);
    }

    [Fact]
    public void Number_NumberIsDefined_ReturnsInt()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 5)) {Number = 12};

        // Act
        var result = seat.Number;

        // Assert
        Assert.Equal(12, result);
    }

    [Fact]
    public void RowNumber_RowNumberIsDefined_ReturnsInt()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 5));

        // Act
        var result = seat.RowNumber;

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Position_VectorOfTheSeat_ReturnsVector2()
    {
        // Arrange
        Vector2 vector2 = new(10, 12);
        Seat seat = new(vector2);

        // Act
        var result = seat.Position;

        // Assert
        Assert.Equal(vector2, result);
    }

    [Fact]
    public void Occupied_SeatIsOccupied_ReturnsTrue()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 12), true);

        // Act
        var result = seat.Occupied;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Occupied_SeatIsNotOccupied_ReturnsFalse()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 12));

        // Act
        var result = seat.Occupied;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_TwoSeatsAreEqual_ReturnsTrue()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 5));
        Seat seat2 = new(new Vector2(10, 5));

        // Act
        var result = seat.Equals(seat2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_TwoSeatsAreNotEqual_ReturnsFalse()
    {
        // Arrange
        Seat seat = new(new Vector2(10, 5));
        Seat seat2 = new(new Vector2(11, 6));

        // Act
        var result = seat.Equals(seat2);

        // Assert
        Assert.False(result);
    }
}