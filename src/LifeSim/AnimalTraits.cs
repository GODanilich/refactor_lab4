using System;

namespace LifeSim;

public sealed record AnimalTraits(
    int Vision,
    int MoveCost,
    int BiteGain,
    int ReproduceThreshold,
    int InitialEnergy,
    int MaxAge,
    char Glyph,
    ConsoleColor Color);
