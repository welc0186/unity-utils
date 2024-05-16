using Alf.Utils;
using UnityEngine;

namespace Alf.GameGridSystem
{
public static class GameGridEvents
{
    public static readonly CustomEvent<GameGridCellEvent> onGameGridCellEvent = new();
    public static readonly CustomEvent<GameObject> onGameGridHighlight = new();

}
}