using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSim;

public abstract class Animal : Organism
{
    protected Animal(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    protected abstract AnimalTraits Traits { get; }

    public override char Glyph => Traits.Glyph;

    public override ConsoleColor? Color => Traits.Color;

    public int Energy { get; set; }

    public override void Tick()
    {
        base.Tick();

        if (Age == 1 && Energy == 0)
        {
            Energy = Traits.InitialEnergy;
        }

        var prey = FindPrey();
        if (prey != null)
        {
            StepToward(prey.Pos);
            if (AreNeighborsOrSame(Pos, prey.Pos) && prey.IsAlive)
            {
                World.Remove(prey);
                Energy += Traits.BiteGain;
            }
        }
        else
        {
            Wander();
        }

        Energy -= Traits.MoveCost;

        if (Energy >= Traits.ReproduceThreshold)
        {
            var empty = World.EmptyNeighbors8(Pos).ToList();
            if (empty.Count > 0)
            {
                var child = MakeChild(empty.Pick()!);
                Energy /= 2;
                World.Add(child);
            }
        }

        if (Energy <= 0 || (Age > Traits.MaxAge && Rand.Chance(MortalityRates.AnimalOldAgeDeathChance)))
        {
            World.Remove(this);
        }
    }

    protected abstract Organism? FindPrey();

    protected abstract Animal MakeChild(Point2 p);

    protected static bool AreNeighborsOrSame(Point2 a, Point2 b) => a.IsNeighborOrSame(b);

    protected void StepToward(Point2 target)
    {
        var dx = BestToroidalStep(Pos.X, target.X, World.Width);
        var dy = BestToroidalStep(Pos.Y, target.Y, World.Height);

        var candidates = new List<Point2>();
        if (dx != 0)
        {
            candidates.Add(World.Wrap(Pos.Offset(dx, 0)));
        }

        if (dy != 0)
        {
            candidates.Add(World.Wrap(Pos.Offset(0, dy)));
        }

        if (dx != 0 && dy != 0)
        {
            candidates.Add(World.Wrap(Pos.Offset(dx, dy)));
        }

        var free = candidates.Where(World.IsEmpty).ToList();
        if (free.Count == 0)
        {
            Wander();
            return;
        }

        World.MoveTo(this, free.Pick()!);
    }

    protected void Wander()
    {
        var options = World.EmptyNeighbors8(Pos).ToList();
        if (options.Count > 0)
        {
            World.MoveTo(this, options.Pick()!);
        }
    }

    private static int BestToroidalStep(int from, int to, int size)
    {
        var direct = to - from;
        var wrapA = (to + size) - from;
        var wrapB = to - (from + size);

        var best =
            Math.Abs(direct) <= Math.Abs(wrapA) && Math.Abs(direct) <= Math.Abs(wrapB)
                ? direct
                : Math.Abs(wrapA) < Math.Abs(wrapB)
                    ? wrapA
                    : wrapB;

        return Math.Sign(best);
    }
}
