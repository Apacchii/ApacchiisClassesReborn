using IL.Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2
{
    //note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
    public class ACM2Commands : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "acr";

        public override string Usage
            => "/acr resetHUD";

        public override string Description
            => "Reset's the HUD from 'Apacchii's Clases: Reborn' if it is stuck";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int player;
            for (player = 0; player < 255; player++)
            {
                if (Main.player[player].active && /*Main.player[player].name == args[0]*/ Main.myPlayer == player)
                {
                    break;
                }
            }

            if (player == 255)
            {
                throw new UsageException("Could not find player: " + args[0]);
            }

            var modPlayer = Main.player[player].GetModPlayer<ACMPlayer>();

            if(args[0] == "devCC")
            {
                modPlayer.ability1Cooldown = 0;
                modPlayer.ability2Cooldown = 0;
                modPlayer.ultCharge = modPlayer.ultChargeMax;
                modPlayer.InBattle();

                caller.Reply("All cooldowns removed from player: '" + Main.player[player].name + "'");
            }

            if (args[0] == "resetHUD")
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    ModContent.GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                    ModContent.GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
                }
                caller.Reply("HUD has been reset");
                return;
            }
        }
    }
}