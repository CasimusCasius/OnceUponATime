namespace Game.Control
{
    public interface IRaycastable
    {
        ECursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }

}