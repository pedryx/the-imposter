using Microsoft.Xna.Framework;

using MonoGamePlus;

using TheImposter.GameStates;
using TheImposter.GameStates.Level;

var game = new MGPGame(new Vector2(1280, 720));
game.Run<LevelState>();