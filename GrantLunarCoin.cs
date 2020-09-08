[Server]
private void GrantLunarCoin(CharacterBody body, uint count)
{
	if (!NetworkServer.active)
	{
		Debug.LogWarning("[Server] function 'System.Void RoR2.GenericPickupController::GrantLunarCoin(RoR2.CharacterBody,System.UInt32)' called on client");
		return;
	}
	foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
	{
		CharacterBody body2 = playerCharacterMasterController.master.GetBody();
		CharacterMaster master = body2.master;
		NetworkUser networkUser = Util.LookUpBodyNetworkUser(body2);
		if (networkUser)
		{
			if (master)
			{
				GenericPickupController.SendPickupMessage(master, this.pickupIndex);
			}
			networkUser.AwardLunarCoins(count);
		}
	}
	UnityEngine.Object.Destroy(base.gameObject);
}
