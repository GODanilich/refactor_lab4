namespace LifeSim;

public class Predator : Animal
{
    public Predator(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    protected override AnimalTraits Traits => SpeciesProfiles.Predator;

    protected override Organism? FindPrey() => World.FindNearest<Herbivore>(Pos, Traits.Vision);

    protected override Animal MakeChild(Point2 p) => new Predator(World, p);
}
