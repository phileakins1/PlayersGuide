// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification = "<Pending>", Scope = "member", Target = "~M:CreatePotion.GetIngredientChoice~Ingredient")]
[assembly: SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "<Pending>", Scope = "member", Target = "~M:CreatePotion.BrewPotion")]
[assembly: SuppressMessage("Minor Code Smell", "S2325:Methods and properties that don't access instance data should be static", Justification = "<Pending>", Scope = "member", Target = "~M:CreatePotion.BrewPotion")]
[assembly: SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "<Pending>", Scope = "type", Target = "~T:CreatePotion")]
[assembly: SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "<Pending>", Scope = "type", Target = "~T:Potion")]
[assembly: SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "<Pending>", Scope = "type", Target = "~T:Ingredient")]
