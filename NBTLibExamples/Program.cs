using Ionic.Zlib;
using NBTLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTLibExamples {
	class Program {
		private static void Main(string[] args) {

			using(var fileStream = File.OpenRead("Data/level.dat"))
			using(var nbtStream = new GZipStream(fileStream, CompressionMode.Decompress)) {
				var reader = new NBTReader(nbtStream);
				reader.MoveNext();

				Read(reader);
			}
		}

		private static void ReadDataCompound(NBTReader reader) {
			while(reader.MoveNext() && reader.Type != NBTType.End) {
				switch(reader.Name) {
					case "version": LogValue(reader); break;
					case "initialized": LogValue(reader); break;
					case "LevelName": LogValue(reader); break;
					case "generatorName": LogValue(reader); break;
					case "generatorVersion": LogValue(reader); break;
					case "generatorOptions": LogValue(reader); break;
					case "RandomSeed": LogValue(reader); break;
					case "MapFeatures": LogValue(reader); break;
					case "LastPlayed": LogValue(reader); break;
					case "SizeOnDisk": LogValue(reader); break;
					case "allowCommands": LogValue(reader); break;
					case "hardcore": LogValue(reader); break;
					case "GameType": LogValue(reader); break;
					case "Difficulty": LogValue(reader); break;
					case "DifficultyLocked": LogValue(reader); break;
					case "Time": LogValue(reader); break;
					case "DayTime": LogValue(reader); break;
					case "SpawnX": LogValue(reader); break;
					case "SpawnY": LogValue(reader); break;
					case "SpawnZ": LogValue(reader); break;
					case "BorderCenterX": LogValue(reader); break;
					case "BorderCenterZ": LogValue(reader); break;
					case "BorderSize": LogValue(reader); break;
					case "BorderSafeZone": LogValue(reader); break;
					case "BorderWarningBlocks": LogValue(reader); break;
					case "BorderWarningTime": LogValue(reader); break;
					case "BorderSizeLerpTarget": LogValue(reader); break;
					case "BorderSizeLerpTime": LogValue(reader); break;
					case "BorderDamagePerBlock": LogValue(reader); break;
					case "raining": LogValue(reader); break;
					case "rainTime": LogValue(reader); break;
					case "thundering": LogValue(reader); break;
					case "thunderTime": LogValue(reader); break;
					case "clearWeatherTime": LogValue(reader); break;
					case "Player": ReadPlayerCompound(reader); break;
					case "GameRules": ReadGameRulesCompound(reader); break;
					default: Read(reader); break;
				}
			}
		}
		private static void ReadGameRulesCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			while(reader.MoveNext() && reader.Type != NBTType.End) {
				switch(reader.Name) {
					case "commandBlockOutput": LogValue(reader); break;
					case "doDaylightCycle": LogValue(reader); break;
					case "doFireTick": LogValue(reader); break;
					case "doMobLoot": LogValue(reader); break;
					case "doMobSpawning": LogValue(reader); break;
					case "doTileDrops": LogValue(reader); break;
					case "keepInventory": LogValue(reader); break;
					case "logAdminCommands": LogValue(reader); break;
					case "mobGriefing": LogValue(reader); break;
					case "naturalRegeneration": LogValue(reader); break;
					case "randomTickSpeed": LogValue(reader); break;
					case "sendCommandFeedback": LogValue(reader); break;
					case "showDeathMessages": LogValue(reader); break;
					default: Read(reader); break;
				}
			}
			indention--;
		}
		private static void ReadPlayerCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			while(reader.MoveNext() && reader.Type != NBTType.End) {
				switch(reader.Name) {
					case "playerGameType": LogValue(reader); break;
					case "Score": LogValue(reader); break;
					case "SelectedItemSlot": LogValue(reader); break;
					case "SpawnX": LogValue(reader); break;
					case "SpawnY": LogValue(reader); break;
					case "SpawnZ": LogValue(reader); break;
					case "SpawnForced": LogValue(reader); break;
					case "Sleeping": LogValue(reader); break;
					case "SleepTimer": LogValue(reader); break;
					case "foodLevel": LogValue(reader); break;
					case "foodExhaustionLevel": LogValue(reader); break;
					case "foodSaturationLevel": LogValue(reader); break;
					case "foodTickTimer": LogValue(reader); break;
					case "XpLevel": LogValue(reader); break;
					case "XpP": LogValue(reader); break;
					case "XpTotal": LogValue(reader); break;
					case "XpSeed": LogValue(reader); break;
					case "Inventory": ReadItemListCompound(reader); break;
					case "EnderItems": ReadItemListCompound(reader); break;
					case "abilities": ReadAbilitiesCompound(reader); break;

					case "HealF": LogValue(reader); break;
					case "Health": LogValue(reader); break;
					case "AbsorptionAmount": LogValue(reader); break;
					case "AttackTime": LogValue(reader); break;
					case "HurtTime": LogValue(reader); break;
					case "HurtByTimestamp": LogValue(reader); break;
					case "DeathTime": LogValue(reader); break;
					case "Attributes": ReadAttributesCompound(reader); break;
					case "ActiveEffects": ReadActiveEffectsCompound(reader); break;

					case "Pos": LogValue(reader); break;
					case "Motion": LogValue(reader); break;
					case "Rotation": LogValue(reader); break;
					case "FallDistance": LogValue(reader); break;
					case "Fire": LogValue(reader); break;
					case "Air": LogValue(reader); break;
					case "OnGround": LogValue(reader); break;
					case "Dimension": LogValue(reader); break;
					case "Invulnerable": LogValue(reader); break;
					case "PortalCooldown": LogValue(reader); break;
					case "UUIDMost": LogValue(reader); break;
					case "UUIDLeast": LogValue(reader); break;
					case "UUID": LogValue(reader); break;
					case "Riding": LogValue(reader); break;
					case "CommandStats": ReadPlayerCommandStatsCompound(reader); break;
					default: Read(reader); break;
				}
			}
			indention--;
		}
		private static void ReadPlayerCommandStatsCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			while(reader.MoveNext() && reader.Type != NBTType.End) {
				switch(reader.Name) {
					case "SuccessCountName": LogValue(reader); break;
					case "SuccessCountObjective": LogValue(reader); break;
					case "AffectedBlocksName": LogValue(reader); break;
					case "AffectedBlocksObjective": LogValue(reader); break;
					case "AffectedEntitiesName": LogValue(reader); break;
					case "AffectedEntitiesObjective": LogValue(reader); break;
					case "AffectedItemsName": LogValue(reader); break;
					case "AffectedItemsObjective": LogValue(reader); break;
					default: Read(reader); break;
				}
			}
			indention--;
		}
		private static void ReadAbilitiesCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			while(reader.MoveNext() && reader.Type != NBTType.End) {
				switch(reader.Name) {
					case "walkSpeed": LogValue(reader); break;
					case "flySpeed": LogValue(reader); break;
					case "mayfly": LogValue(reader); break;
					case "flying": LogValue(reader); break;
					case "invulnerable": LogValue(reader); break;
					case "mayBuild": LogValue(reader); break;
					case "instabuild": LogValue(reader); break;
					default: Read(reader); break;
				}
			}
			indention--;
		}
		private static void ReadItemListCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			var length = (int)reader.Value;
			for(int i = 0; i < length; i++) {
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					switch(reader.Name) {
						case "Count": LogValue(reader); break;
						case "Slot": LogValue(reader); break;
						case "Damage": LogValue(reader); break;
						case "id": LogValue(reader); break;
						default: Read(reader); break;
					}
				}
			}
			indention--;
		}
		private static void ReadActiveEffectsCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			var length = (int)reader.Value;
			for(int i = 0; i < length; i++) {
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					switch(reader.Name) {
						case "Id": LogValue(reader); break;
						case "Amplifier": LogValue(reader); break;
						case "Duration": LogValue(reader); break;
						case "Ambient": LogValue(reader); break;
						case "ShowParticles": LogValue(reader); break;
						default: Read(reader); break;
					}
				}
			}
			indention--;
		}
		private static void ReadAttributesCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			var length = (int)reader.Value;
			for(int i = 0; i < length; i++) {
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					switch(reader.Name) {
						case "Name": LogValue(reader); break;
						case "Base": LogValue(reader); break;
						case "Modifiers": ReadModifiersCompound(reader); break;
						default: Read(reader); break;
					}
				}
			}
			indention--;
		}
		private static void ReadModifiersCompound(NBTReader reader) {
			LogValue(reader);
			indention++;

			var length = (int)reader.Value;
			for(int i = 0; i < length; i++) {
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					switch(reader.Name) {
						case "Name": LogValue(reader); break;
						case "Amount": LogValue(reader); break;
						case "Operation": LogValue(reader); break;
						case "Modifiers": LogValue(reader); break;
						case "UUIDMost": LogValue(reader); break;
						case "UUIDLeast": LogValue(reader); break;
						default: Read(reader); break;
					}
				}
			}
			indention--;
		}

		private static void Read(NBTReader reader) {
			LogValue(reader);

			indention++;
			if(reader.Type == NBTType.Compound) {
				while(reader.MoveNext() && reader.Type != NBTType.End) {
					Read(reader);
				}

			} else if(reader.Type == NBTType.CompoundList) {
				var length = (int)reader.Value;
				for(int i = 0; i < length; i++) {
					while(reader.MoveNext() && reader.Type != NBTType.End) {
						Read(reader);
					}
				}
			}
			indention--;
		}

		private static int indention;
		private static void LogValue(NBTReader reader) {
			Console.WriteLine("".PadLeft(indention) + "{0}: ({1}){2}", reader.Name, reader.Type, reader.Value);
		}
	}
}
