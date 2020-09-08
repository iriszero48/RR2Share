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
		NetworkUser networkUser = Util.LookUpBodyNetworkUser(playerCharacterMasterController.master.GetBody());
		if (networkUser)
		{
			networkUser.AwardLunarCoins(count);
		}
	}
	if (Util.LookUpBodyNetworkUser(body) && body.master)
	{
		GenericPickupController.SendPickupMessage(body.master, this.pickupIndex);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
