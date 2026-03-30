using System;

namespace LifeSim;

public static class SpeciesProfiles
{
    public static readonly AnimalTraits Herbivore = new(
        Vision: 8,
        MoveCost: 2,
        BiteGain: 18,
        ReproduceThreshold: 60,
        InitialEnergy: 30,
        MaxAge: 1000,
        Glyph: 'h',
        Color: ConsoleColor.Yellow);

    public static readonly AnimalTraits Predator = new(
        Vision: 12,
        MoveCost: 3,
        BiteGain: 28,
        ReproduceThreshold: 80,
        InitialEnergy: 40,
        MaxAge: 1000,
        Glyph: 'W',
        Color: ConsoleColor.Red);
}
