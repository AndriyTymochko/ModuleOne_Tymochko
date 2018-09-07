using System;

namespace Lab_1_3_Operators.OperationType.PuzzleFarmer
{
    internal enum FarmerPazle_Answers : byte
    {
        There_farmer_and_wolf = 1,
        There_farmer_and_cabbage,
        There_farmer_and_goat,
        There_farmer,
        Back_farmer_and_wolf,
        Back_farmer_and_cabbage,
        Back_farmer_and_goat,
        Back_farmer,
    }

    [Flags]
    internal enum FarmerPazle_ObjectToTransfer : byte
    {
        farmer = 1,
        wolf,
        cabbage,
        goat
    }

    [Flags]
    internal enum FarmerPazle_Direction : byte
    {
        there = 1,
        back
    }
}
