using HarmonyLib;

Harmony.DEBUG = true;
var harmony = new Harmony("io.github.danilwhale.minecraft_ikvm");
harmony.PatchAll();

Console.WriteLine("s");
com.mojang.rubydung.RubyDung.main(args);