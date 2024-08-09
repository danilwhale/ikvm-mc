using System.Reflection;
using System.Reflection.Emit;
using com.mojang.rubydung;
using HarmonyLib;
using org.lwjgl.opengl;

namespace IkvmMc.Patches;

[HarmonyPatch(typeof(RubyDung), nameof(RubyDung.init))]
public sealed class RubyDungInitPatch
{
    private static MethodInfo _displayCreateMethod = AccessTools.Method(typeof(Display), "create");
    private static MethodInfo _displaySetTitleMethod = AccessTools.Method(typeof(Display), "setTitle");
    
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var found = false;
        
        foreach (var instruction in instructions)
        {
            if (instruction.Calls(_displayCreateMethod))
            {
                yield return instruction;
                yield return new CodeInstruction(OpCodes.Ldstr, "ikvm-mc");
                yield return new CodeInstruction(OpCodes.Call, _displaySetTitleMethod);
                found = true;
                continue;
            }

            yield return instruction;
        }

        if (!found)
        {
            Console.Error.WriteLine("failed to find call to Display.create");
        }
    }
}