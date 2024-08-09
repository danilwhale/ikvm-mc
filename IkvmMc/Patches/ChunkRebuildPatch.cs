using System.Reflection;
using System.Reflection.Emit;
using com.mojang.rubydung.level;
using HarmonyLib;

namespace IkvmMc.Patches;

[HarmonyPatch(typeof(Chunk), "rebuild")]
public sealed class ChunkRebuildPatch
{
    private static Tile _magentaTile = (Tile)AccessTools.Constructor(typeof(Tile), [typeof(int)]).Invoke([3]);
    private static FieldInfo _magentaTileField = AccessTools.Field(typeof(ChunkRebuildPatch), "_magentaTile");
    private static FieldInfo _rockTileField = AccessTools.Field(typeof(Tile), "rock");
    
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var found = false;

        foreach (var instruction in instructions)
        {
            if (instruction.opcode == OpCodes.Ldsfld && Equals(instruction.operand, _rockTileField))
            {
                yield return new CodeInstruction(OpCodes.Ldsfld, _magentaTileField);
                found = true;
                continue;
            }

            yield return instruction;
        }

        if (!found)
        {
            Console.Error.WriteLine("failed to find ldsfld with Tile::rock");
        }
    }
}