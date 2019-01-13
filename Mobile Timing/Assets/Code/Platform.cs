using UnityEngine;

public class Platform : InteractableObstacle
{
    // Private:
    [SerializeField] private PlatformTypeTemp type;
    private PlatformColor color;

    public void Init()
    {
        if (type == PlatformTypeTemp.SPEEDING)
            RandomizeColor();
    }

    private void RandomizeColor()
    {
        color = (PlatformColor)Random.Range(0, System.Enum.GetValues(typeof(PlatformColor)).Length);
    }
}


public enum PlatformTypeTemp {DEFAULT, SPEEDING}
public enum PlatformColor {NONE, RED, BLUE, GREEN, YELLOW, PURPLE, BROWN, PINK, ORANGE, GREY, BLACK, WHITE}
