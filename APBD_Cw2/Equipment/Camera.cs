namespace APBD_Cw2;

public class Camera : Equipment
{
    public bool HasLensIncluded { get; set; }
    public int Megapixels { get; set; }

    public Camera(string name, bool hasLens, int megapixels) : base(name)
    {
        HasLensIncluded = hasLens;
        Megapixels = megapixels;
    }
}