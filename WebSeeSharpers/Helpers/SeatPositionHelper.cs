using System.Numerics;
using WebSeeSharpers.Services.SeatService;

namespace WebSeeSharpers.Helpers;

public class SeatPositionHelper
{
    public static string SerializeSeatToString(List<Seat> seats)
    {
        var serializedPosition = string.Empty;
        seats.ForEach(s => { serializedPosition += $"{(int) s.Position.X}_{(int) s.Position.Y};"; });

        return serializedPosition;
    }

    public static string SerializePositionToString(List<Vector2> position)
    {
        var serializedPosition = string.Empty;
        position.ForEach(p => { serializedPosition += $"{(int) p.X}_{(int) p.Y};"; });

        return serializedPosition;
    }

    public static string SerializePositionToString(Vector2 position)
    {
        return SerializePositionToString(new List<Vector2>() {position});
    }

    public static string SerializeSeatToString(Seat seat)
    {
        return SerializeSeatToString(new List<Seat>() {seat});
    }

    public static List<Vector2> DeserializePositionToVector2List(string serializedPosition)
    {
        List<Vector2> vectorList = new();
        var seatPositionsSplit = serializedPosition.Split(';');

        foreach (var position in seatPositionsSplit)
        {
            var positionXAndY = position.Split('_');
            if (positionXAndY.Length < 2)
                continue;

            vectorList.Add(new(Convert.ToSingle(positionXAndY[0]), Convert.ToSingle(positionXAndY[1])));
        }

        return vectorList;
    }
}