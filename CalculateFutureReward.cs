﻿using System;
using BehaviourTree;
using UnityEngine;

namespace HelloMod
{
	public class CalculateFutureReward :Node
	{
		private string _block;
		private string _reward;
		private Person _person;
		public CalculateFutureReward (string reward,string block)
		{
			_block = block;
			_reward = reward;
		}

		public override void initialize (BehaviourTree.DataContext dataContext)
		{
			this._person = dataContext.person;

			base.initialize (dataContext);
		}

		protected override Result run (DataContext dataContext)
		{
			var next = dataContext.get<Block> (_block);
			int currentX = Mathf.FloorToInt (_person.transform.position.x);
			int currentY = Mathf.RoundToInt (_person.transform.position.y);
			int currentZ = Mathf.FloorToInt (_person.transform.position.z);


			int futureX = Mathf.FloorToInt (next.transform.position.x);
			int futureY = Mathf.RoundToInt (next.transform.position.y);
			int futureZ = Mathf.FloorToInt (next.transform.position.z);

			var node = QLearningCache.Instance.GetNode (HelloBehaviour.GUEST_QLEARNING, currentX, currentY, currentZ);
			node.calculateNewState (futureX, futureY, futureZ, dataContext.get<float>(_reward));

			return Result.SUCCESS;
		}
	}
}

