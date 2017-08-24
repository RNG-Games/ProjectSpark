using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSpark.actors;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using ProjectSpark.actors.enemies;
using ProjectSpark.actors.blocks;
using ProjectSpark.actors.lines;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text;
using ProjectSpark.glyphshaders;
using ProjectSpark.assets;
using ProjectSpark.loading;

namespace ProjectSpark.gamestates
{
    class MainState : GameState
    {
        private TextRenderer tr;
        private SpriteFont Trebuchet;
        private string collected, death;
        private Player player;

        private Vector2f CheckPlayerPos, CheckCameraPos;

        public MainState(string stage)
        {
            tr = new TextRenderer();
            tr.RegisterGlyphShader("shaky", new Shaky());
            tr.RegisterGlyphShader("rainbow", new Rainbow());
            tr.RegisterGlyphShader("wavy", new Wavy());
            Trebuchet = Resources.ContentManager.Load<SpriteFont>(GlobalFontID.TrebuchetMS16);

            //Add Stuff
            player = Player.getPlayer();
            Resources.actors = new List<IActable>();
            Resources.actorBuffer = new List<IActable>();

            Loader.Load(stage, Resources.actors);
            foreach (var checkp in Resources.actors.Where(a => a is Checkpoint))
            {
                ((Checkpoint) checkp).setCheckpoint = SetCheck;
            }
            CheckPlayerPos = Player.getPlayer().position;
            CheckCameraPos = Game.camera.Position;
        }

        public override void Update(UltravioletTime time)
        {
            Resources.actors = Resources.actors.Where(a => !a.IsExpired()).ToList();
            foreach (var actor in Resources.actors)
            {
                actor.Update(time);
            }
            if(Player.getPlayer().dead)
                Respawn();

            Resources.actors.AddRange(Resources.actorBuffer);
            Resources.actorBuffer.Clear();
            death = $"Deaths: {Resources.deaths}";
            collected = $"Collected: {Resources.collectables}";

            player.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var actor in Resources.actors)
            {
                actor.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, collected, new Vector2f(10,10), Color.Black, settings);
            tr.Draw(spriteBatch, death, new Vector2f(10, 30), Color.Black, settings);
        }

        private void SetCheck()
        {
            CheckPlayerPos = player.position;
            CheckCameraPos = Game.camera.Position;
        }

        private void Respawn()
        {
            Resources.actors.Clear();
            Loader.ApplyLoadedData(Resources.actors, Resources.StageData);

            player.position = CheckPlayerPos;
            Game.camera.Position = CheckCameraPos;

            player.unkill();
        }
    }
}
