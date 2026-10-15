

CREATE PROCEDURE [FixedAsset].[SP_ReportListMainAccountsFixedAssetByStatusAndBookId]
@Status bit,
@LegalBook int

AS
BEGIN 
	if (select OfficialBook from GeneralLedger.LegalBook where Id = @LegalBook) = 1 begin
		select distinct ma.id,ma.Number,ma.Name,ma.LegalBookId,case ma.[Status] when 1 then 'Activo' else 'Inactivo' end as [Status]
		,case ma.HandlesThirdParty when 1 then 'Si' else 'No' end as HandlesThirdParty   
		,case ma.HandlesCostCenter when 1 then 'Si' else 'No' end as HandlesCostCenter
		,case ma.RetencionType when 0 then 'Ninguna' when 1 then 'ReteFuente' when 2 then 'ReteIva' when 3 then 'ReteIca' when 4 then 'Otras' end as RetencionType
		,case ma.AllowsMovement when 1 then 'Si' else 'No' end as AllowsMovement 
		from GeneralLedger.MainAccounts as ma
		inner join FixedAsset.FixedAssetPhysicalAsset as fa on fa.MainAccountId = ma.Id
		where LegalBookId = @LegalBook and ma.[Status] = @Status
	end 
	else begin
		select distinct ma.id,ma.Number,ma.Name,ma.LegalBookId,case ma.[Status] when 1 then 'Activo' else 'Inactivo' end as [Status]
		,case ma.HandlesThirdParty when 1 then 'Si' else 'No' end as HandlesThirdParty   
		,case ma.HandlesCostCenter when 1 then 'Si' else 'No' end as HandlesCostCenter
		,case ma.RetencionType when 0 then 'Ninguna' when 1 then 'ReteFuente' when 2 then 'ReteIva' when 3 then 'ReteIca' when 4 then 'Otras' end as RetencionType
		,case ma.AllowsMovement when 1 then 'Si' else 'No' end as AllowsMovement 
		from GeneralLedger.MainAccounts as ma
		inner join GeneralLedger.HomologationAccount as ha on ha.MainAccountId = ma.Id
		inner join FixedAsset.FixedAssetPhysicalAsset as fa on fa.MainAccountId = ha.OfficialMainAccountId
		where ma.LegalBookId = @LegalBook and ma.[Status] = @Status
	end
END