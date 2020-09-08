[Server]
private void GrantEquipment(CharacterBody body, Inventory inventory)
{
	if (!NetworkServer.active)
	{
		Debug.LogWarning("[Server] function 'System.Void RoR2.GenericPickupController::GrantEquipment(RoR2.CharacterBody,RoR2.Inventory)' called on client");
		return;
	}
	this.waitStartTime = Run.FixedTimeStamp.now;
	EquipmentIndex currentEquipmentIndex = inventory.currentEquipmentIndex;
	PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
	EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
	foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
	{
		playerCharacterMasterController.master.inventory.SetEquipmentIndex(equipmentIndex);
	}
	this.NetworkpickupIndex = PickupCatalog.FindPickupIndex(currentEquipmentIndex);
	this.consumed = false;
	GenericPickupController.SendPickupMessage(inventory.GetComponent<CharacterMaster>(), PickupCatalog.FindPickupIndex(equipmentIndex));
	if (this.pickupIndex == PickupIndex.none)
	{
		UnityEngine.Object.Destroy(base.gameObject);
		return;
	}
	if (this.selfDestructIfPickupIndexIsNotIdeal && this.pickupIndex != PickupCatalog.FindPickupIndex(this.idealPickupIndex.pickupName))
	{
		PickupDropletController.CreatePickupDroplet(this.pickupIndex, base.transform.position, new Vector3(UnityEngine.Random.Range(-4f, 4f), 20f, UnityEngine.Random.Range(-4f, 4f)));
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
