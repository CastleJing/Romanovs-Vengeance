#region Copyright & License Information
/*
 * Copyright 2007-2020 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Linq;
using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	public class GrantConditionOnTileSetInfo : TraitInfo
	{
		[FieldLoader.Require]
		[GrantedConditionReference]
		[Desc("Condition to grant.")]
		public readonly string Condition = null;

		[FieldLoader.Require]
		[Desc("Tile set IDs to trigger the condition.")]
		public readonly string[] TileSets = { };

		public override object Create(ActorInitializer init) { return new GrantConditionOnTileSet(init, this); }
	}

	public class GrantConditionOnTileSet : INotifyCreated
	{
		readonly GrantConditionOnTileSetInfo info;

		public GrantConditionOnTileSet(ActorInitializer init, GrantConditionOnTileSetInfo info)
		{
			this.info = info;
		}

		void INotifyCreated.Created(Actor self)
		{
			if (info.TileSets.Contains(self.World.Map.Rules.TerrainInfo.Id))
				self.GrantCondition(info.Condition);
		}
	}
}
