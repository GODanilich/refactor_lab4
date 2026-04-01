namespace LifeSim;

public class Herbivore : Animal
{
    public Herbivore(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    protected override AnimalTraits Traits => SpeciesProfiles.Herbivore;

    protected override Organism? FindPrey() => World.FindNearest<Plant>(Pos, Traits.Vision);

    protected override Animal MakeChild(Point2 p) => new Herbivore(World, p);
}
