using RolePlayingGame.Core.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace RolePlayingGame.Core
{
	internal class Entity
	{
		#region Constants

		private const string _filePath = @"Content\Entities.csv";
		private const char _csvSeparator = ',';

		#endregion Constants

		#region Static

		private static Dictionary<string, EntityRawData> _TileDescriptions;

		public static Dictionary<string, EntityRawData> TileDescriptions
		{
			get
			{
				if (Entity._TileDescriptions == null)
				{
					Entity._TileDescriptions = new Dictionary<string, EntityRawData>();
					using (StreamReader stream = new StreamReader(_filePath))
					{
						string line;
						// Skip header
						stream.ReadLine();
						while ((line = stream.ReadLine()) != null)
						{
							string[] elements = line.Split(_csvSeparator);

							var entityRawData = new EntityRawData(elements);
							Entity._TileDescriptions.Add(entityRawData.Key, entityRawData);
						}
					}
				}
				return Entity._TileDescriptions;
			}
		}

		#endregion Static

		#region Fields

		public Tile Tile { get; set; }

		public string Name { get; set; }

		public EntityCategoryType Category { get; set; }

		public EntityType Type { get; set; }

		public string Key { get; set; }

		public bool IsPassable { get; set; }

		public bool IsTransparent { get; private set; }

		public string Special { get; private set; }

		public Color? ColorKey { get; private set; }

		public EntityRawData Raw { get; private set; }

		#endregion Fields

		public Entity(EntityType type)
			: this(type.ToString())
		{
		}

		public Entity(string key)
		{
			var entityRawData = Entity.TileDescriptions[key];
			try
			{
				this.Name = entityRawData.Name;
				this.Key = entityRawData.Key;
				this.Category = (EntityCategoryType)Enum.Parse(typeof(EntityCategoryType), entityRawData.Category);
				this.Type = (EntityType)Enum.Parse(typeof(EntityType), entityRawData.Type);
				this.IsPassable = bool.Parse(entityRawData.IsPassable);
				this.IsTransparent = bool.Parse(entityRawData.IsTransparent);
				this.Tile = new Tile(entityRawData);
				this.Special = entityRawData.Special;
				if (!string.IsNullOrWhiteSpace(entityRawData.ColorKey))
				{
					var colors = entityRawData.ColorKey.Split(';')
						.Select(item => Convert.ToInt32(item))
						.ToArray();
					int red = colors[0];
					int green = colors[1];
					int blue = colors[2];
					this.ColorKey = Color.FromArgb(red, green, blue);
				}

				this.Raw = entityRawData;
			}
			catch (Exception ex)
			{
				var exception = new FileParseException(_filePath, ex.Message, ex);
				throw exception;
			}
		}
	}
}