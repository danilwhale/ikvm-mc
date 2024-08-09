using HarmonyLib;

Harmony.DEBUG = true;
var harmony = new Harmony("io.github.danilwhale.ikvm-mc");
harmony.PatchAll();

Console.WriteLine("s");
com.mojang.rubydung.RubyDung.main(args);