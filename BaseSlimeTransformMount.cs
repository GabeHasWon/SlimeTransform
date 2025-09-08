using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SlimeTransform;

public abstract class BaseSlimeTransformMount : ModMount
{
	protected abstract int BuffType { get; }
	protected virtual int DustType => DustID.SlimeBunny;

	public override void SetStaticDefaults()
	{
		MountData.buff = BuffType;
		MountData.spawnDust = DustType;
		MountData.heightBoost = -20;
		MountData.fallDamage = 0f;
		MountData.runSpeed = 5f;
		MountData.dashSpeed = 5f;
		MountData.flightTimeMax = 0;
		MountData.fatigueMax = 5;
		MountData.jumpHeight = 20;
		MountData.acceleration = 0.75f;
		MountData.jumpSpeed = 6f;
		MountData.blockExtraJumps = false;
		MountData.totalFrames = 1;
		MountData.constantJump = false;
		MountData.usesHover = false;
		MountData.blockExtraJumps = false;

		int[] array = new int[MountData.totalFrames];
		for (int i = 0; i < array.Length; i++)
			array[i] = 0;

		MountData.playerYOffsets = array;
		MountData.xOffset = 0;
		MountData.bodyFrame = 0;
		MountData.yOffset = 4;
		MountData.playerHeadOffset = -16;
		MountData.standingFrameCount = 1;
		MountData.standingFrameDelay = 1;
		MountData.standingFrameStart = 0;
		MountData.runningFrameCount = 0;
		MountData.runningFrameDelay = 0;
		MountData.runningFrameStart = 0;
		MountData.flyingFrameCount = 0;
		MountData.flyingFrameDelay = 0;
		MountData.flyingFrameStart = 0;
		MountData.inAirFrameCount = 0;
		MountData.inAirFrameDelay = 0;
		MountData.inAirFrameStart = 0;
		MountData.idleFrameCount = 0;
		MountData.idleFrameDelay = 0;
		MountData.idleFrameStart = 0;
		MountData.swimFrameCount = MountData.inAirFrameCount;
		MountData.swimFrameDelay = MountData.inAirFrameDelay;
		MountData.swimFrameStart = MountData.inAirFrameStart;

		if (Main.netMode != NetmodeID.Server)
		{
			MountData.textureWidth = MountData.backTexture.Width();
			MountData.textureHeight = MountData.backTexture.Height();
		}
	}

	public override void UpdateEffects(Player player)
	{
		var config = ModContent.GetInstance<SlimeTransformConfig>();

		if (player.wet && (config.SpecialSlimeMovement || config.FloatInWater))
			player.velocity.Y -= player.gravity * 2f;

		if (!config.SpecialSlimeMovement) //Normal movement, adjust for accessories
        {
			MountData.jumpSpeed = 10 * (1 + player.jumpSpeedBoost) * (player.jumpBoost ? 1.33f : 1);
			MountData.flightTimeMax = player.wingTimeMax;
			MountData.acceleration = player.moveSpeed;
			MountData.runSpeed = player.accRunSpeed;

			if (player.empressBrooch && player.wingTimeMax > 0) //Hardcoded infinite flight exception for Insignia
				MountData.flightTimeMax = int.MaxValue;
        }
		else //Slime movement
        {
			player.autoJump = true; //Allow spam jump
            MountData.flightTimeMax = 0;

            if (player.OnGround())
			{
				MountData.acceleration = 0;
				MountData.runSpeed = 0;
				player.velocity.X *= 0.75f;
			}
			else
			{
				MountData.acceleration = player.moveSpeed;
				MountData.runSpeed = player.accRunSpeed;
			}
			 
			MountData.jumpSpeed = 9 * (1 + player.jumpSpeedBoost) * (player.jumpBoost ? 1.25f : 1); //Slightly nerf jumps so autojump is easier
		}
	}
}