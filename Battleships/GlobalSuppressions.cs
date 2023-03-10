// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Correctness", "LRT001:There is only one restricted namespace", Justification = "NI namespace should not be forced", Scope = "namespaceanddescendants", Target = "~N:Battleships")]