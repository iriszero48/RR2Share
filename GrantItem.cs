[Server]
private void GrantItem(CharacterBody body, Inventory inventory)
{
	if (!NetworkServer.active)
	{
		Debug.LogWarning("[Server] function 'System.Void RoR2.GenericPickupController::GrantItem(RoR2.CharacterBody,RoR2.Inventory)' called on client");
		return;
	}
	PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
	foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
	{
		playerCharacterMasterController.master.inventory.GiveItem((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None, 1);
	}
	GenericPickupController.SendPickupMessage(inventory.GetComponent<CharacterMaster>(), this.pickupIndex);
	UnityEngine.Object.Destroy(base.gameObject);
}
