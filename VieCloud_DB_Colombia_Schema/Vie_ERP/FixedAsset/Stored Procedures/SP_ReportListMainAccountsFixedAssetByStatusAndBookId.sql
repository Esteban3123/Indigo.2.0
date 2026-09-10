

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las cuentas contables principales asociadas a activos fijos físicos, filtradas por estado (activo o inactivo) y por libro contable legal. Si el libro indicado es un libro oficial, obtiene las cuentas directamente vinculadas a los activos fijos; si no es oficial, utiliza la tabla de homologación para encontrar las cuentas equivalentes del libro oficial y luego relacionarlas con los activos. Se usa en reportería contable de activos fijos para auditar o revisar qué cuentas del plan de cuentas están siendo utilizadas en el módulo de activos, incluyendo información sobre manejo de terceros, centros de costo, tipo de retención (ReteFuente, ReteIva, ReteIca) y si la cuenta permite movimientos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cuentas contables principales utilizadas por activos fijos físicos para un libro contable y un estado dados, resolviendo la equivalencia vía homologación cuando el libro no es el oficial.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro contable indicado debe existir en el catálogo de libros legales para determinar si es oficial; Debe existir relación entre cuentas principales y activos fijos físicos (directa o vía homologación) para obtener resultados', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven cuentas pertenecientes al libro contable solicitado; Solo se devuelven cuentas cuyo estado coincide con el estado solicitado; Solo se devuelven cuentas que estén efectivamente vinculadas a algún activo fijo físico; Si el libro no es oficial, la vinculación a activos fijos se establece a través de la cuenta oficial homologada; Los valores de Status, HandlesThirdParty, HandlesCostCenter, AllowsMovement y RetencionType se traducen a etiquetas legibles (Activo/Inactivo, Si/No, Ninguna/ReteFuente/ReteIva/ReteIca/Otras); Las cuentas se devuelven sin duplicados (DISTINCT)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de cuentas (PUC); Libro contable oficial; Homologación de cuentas; Activos fijos físicos; Manejo de terceros; Centros de costo; Retenciones (ReteFuente, ReteIva, ReteIca); Movimientos contables permitidos', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.MainAccounts: Cuando OfficialBook = 1 del libro indicado, devuelve cuentas de MainAccounts unidas directamente a FixedAssetPhysicalAsset por MainAccountId, filtradas por LegalBookId y Status; [RETURN_RESULT] GeneralLedger.MainAccounts: Cuando OfficialBook ≠ 1, devuelve cuentas de MainAccounts unidas a HomologationAccount y luego a FixedAssetPhysicalAsset usando OfficialMainAccountId, filtradas por LegalBookId y Status', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El libro contable indicado tiene OfficialBook = 1 (libro oficial) → Lista las cuentas principales asociadas directamente a activos fijos físicos mediante MainAccountId else Lista las cuentas principales del libro no oficial homologadas a cuentas oficiales que estén asociadas a activos fijos físicos vía HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.MainAccounts; FixedAsset.FixedAssetPhysicalAsset; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListMainAccountsFixedAssetByStatusAndBookId';
-- GO
