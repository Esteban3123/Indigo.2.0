-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/12/2016
-- Description:	Procedimiento que se encarga de guardar en las tablas de FixedAssetPhysicalAsset cuando se va a confirmar el saldo inicial
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ConfirmFixedAssetInitialBalance] 
	@FixedAssetInitialBalanceId as int
AS
BEGIN
	
	--begin transaction
	begin try

		if (select count(*) from FixedAsset.FixedAssetInitialBalanceItem where FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId group by Plate having count(*) > 1) > 1 begin
			declare @MessagePlateDuplicate varchar(max)
			select @MessagePlateDuplicate=stuff((select N'; la placa ' + Plate + ' se encuentra duplicada'
			from FixedAsset.FixedAssetInitialBalanceItem 
			where FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId 
			group by Plate having count(*) > 1
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			select 999 as CodeMessage, @MessagePlateDuplicate as Message
			return
		end

		--Parámetros definidos para determinar si maneja iva al costo en el libro contable oficial y el valor de menor cuantía establecido
		DECLARE @OfficialLegalBookId INT,
				@SettingFixedAssetId INT,
				@CurrentYear INT,
				@LowBidAmount NUMERIC(18,0) = 0,
				@TopMinorValue NUMERIC(18,0) = 0

		--Id del libro oficial, esta variable es utilizada para asignar la cuenta cuando es renting financiero
		select @OfficialLegalBookId = Id from GeneralLedger.LegalBook where OfficialBook = 1

		SELECT TOP 1 @SettingFixedAssetId = sfa.Id, @CurrentYear = YEAR(sfa.ProcessDate), @LowBidAmount = sfa.LowBidAmount, @TopMinorValue = sfa.TopMinorValue
		FROM FixedAsset.SettingFixedAsset sfa
		ORDER BY sfa.ProcessDate DESC

		--Se inserta en la tabla de FixedAssetPhysicalAsset
		INSERT INTO [FixedAsset].[FixedAssetPhysicalAsset]
			(
				[ItemId], [Serie], [Plate], [LocationId], [ResponsibleId], [SupplierId], [TrademarkId], [Model],
				[PolicyId], [HandlesWarranty], [WarrantyExpirationDate], [AdquisitionDate], [Depreciate], [Observation], [StatusAssetId], [Status], 
				[AdquisitionType], [AdquisitionTypeReal],
				[MainAccountId], 
				[HistoricalValue], [FairValue],
				[ApplyMinimunAmount],[Amortize]
			)
			SELECT 
				ItemId, bi.Serie, bi.Plate, bi.LocationId, bi.ResponsibleId, bi.SupplierId, bi.TrademarkId, bi.Model, 
				bi.PolicyId, bi.HandlesWarranty, bi.WarrantyExpirationDate, bi.AdquisitionDate, bi.Depreciate, '', bi.StatusAssetId, 1, 
				bi.AdquisitionType, bi.AdquisitionType, 
				CASE bi.AdquisitionType 
					WHEN 3 THEN c.DebitLoanAccountId 
					WHEN 7 THEN c.IncomeLeasingAccountId 
					WHEN 9 THEN ISNULL(faicat.MainAccountId, c.IncomeAccountId) 
					ELSE c.IncomeAccountId 
				END,
				bi.HistoricalValue, bi.FairValue,
				CASE 
				    WHEN bi.ValidMinorAmount IS NULL OR bi.ValidMinorAmount = 0 THEN 0
				    WHEN bi.ValidMinorAmount = 1 AND 
				         YEAR(bi.AdquisitionDate) = @CurrentYear AND 
				         bi.Depreciate = 1 AND 
				         (bi.HistoricalValue) <= ISNULL(@TopMinorValue, 0) THEN 1
				    ELSE 0
				END,
				bi.Amortize
			FROM FixedAsset.FixedAssetInitialBalanceItem bi
			JOIN FixedAsset.FixedAssetItem i on bi.ItemId = i.Id
			JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogAdquisitionType faicat ON faicat.ItemCatalogId = c.Id and faicat.LegalBookId = @OfficialLegalBookId
			WHERE bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId

		--Se inserta en la tabla FixedAssetPhysicalAssetDetailBook
		INSERT INTO [FixedAsset].[FixedAssetPhysicalAssetDetailBook]
			(
				[PhysicalAssetId],[LegalBookId],[LifeTime],[UnitLifeTime],[DepreciationType],[TotalProductionUnit],
				[PercentageRescue],[Valorization],[Devaluation],[AdjustedValue],[TransactionValue],[DepreciatedValue],[DepreciatedValuePart],
				[ResidualValue],[ResidualValuePart],[HistoricalValue],
				[DaysPendingDepreciate],[DepreciatedDays],[ApplyMinimunAmount]
			)
			SELECT 
				pa.Id, bidb.LegalBookId, bidb.[LifeTime], bidb.UnitLifeTime, bidb.DepreciationType, bidb.TotalProductionUnit, 
				bidb.PercentageRescue, 0, 0, 0, 0, bidb.DepreciatedValue, bidb.DepreciatedValuePart, 
				bidb.ResidualValue, bidb.ResidualValuePart, bidb.HistoricalValue, 
				bidb.DaysPendingDepreciate, bidb.DepreciatedDays,
				CASE
					WHEN bi.ValidMinorAmount IS NULL OR bi.ValidMinorAmount = 0 THEN 0
					WHEN bi.ValidMinorAmount = 1 AND
						bidb.HistoricalValue <= ISNULL(@TopMinorValue, 0) THEN 1
					ELSE 0
				END
			FROM FixedAsset.FixedAssetInitialBalanceItem bi
			JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
			JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
			LEFT JOIN FixedAsset.SettingFixedAssetByLegalBook sfalb ON sfalb.SettingFixedAssetId = @SettingFixedAssetId AND bidb.LegalBookId = sfalb.LegalBookId
			WHERE bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId 
				AND bi.Depreciate = 1
				AND pa.AdquisitionType <> 8 AND pa.AdquisitionType <> 10

		--Se inserta en la tabla FixedAssetPhysicalAssetParts
		insert into [FixedAsset].[FixedAssetPhysicalAssetParts]
		(PhysicalAssetId, PartAccesoriesConsumiblesId, DepreciatePart, HistoricalValue, HasOutput, OutputDate)
		select pa.Id, bidb.PartAccesoriesConsumablesId, bidb.DepreciatePart, bidb.HistoricalValue, 0, null
		from FixedAsset.FixedAssetInitialBalanceItem bi
		inner join FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
		inner join FixedAsset.FixedAssetInitialBalanceItemParts bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
		where bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId

		--Se inserta en la tabla FixedAssetPhysicalAssetPartsDetailBook
		insert into [FixedAsset].[FixedAssetPhysicalAssetPartsDetailBook]
		(PhysicalAssetPartsId, LegalBookId, LifeTime, UnitLifeTime, ValorizationDays, DaysPendingDepreciate, DepreciatedDays, DepreciationType, TotalProductionUnit, PercentageRescue, Valorization,
		Devaluation, AdjustedValue, TransactionValue, DepreciatedValue, InflationAdjustmentValue, ResidualValue)
		select par.Id, boo.LegalBookId, boo.LifeTime, boo.UnitLifeTime, 0, boo.DaysPendingDepreciate, boo.DepreciatedDays, boo.DepreciationType, boo.TotalProductionUnit, boo.PercentageRescue,
		0, 0, 0, 0, boo.DepreciatedValue, 0, boo.ResidualValue
		from FixedAsset.FixedAssetInitialBalanceItem bi
		inner join FixedAsset.FixedAssetPhysicalAsset pa on pa.Plate = bi.Plate
		inner join FixedAsset.FixedAssetPhysicalAssetParts par on par.PhysicalAssetId = pa.Id
		inner join FixedAsset.FixedAssetInitialBalanceItemParts bidb on bidb.FixedAssetInitialBalanceItemId = bi.Id
		inner join FixedAsset.FixedAssetInitialBalanceItemPartsDetailBook boo on boo.FixedAssetInitialBalanceItemPartsId = bidb.Id
		where bi.FixedAssetInitialBalanceId = @FixedAssetInitialBalanceId AND pa.AdquisitionType <> 8 AND pa.AdquisitionType <> 10

		--commit transaction
		select 0 as CodeMessage, 'Se confirmó correctamente' as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma el saldo inicial de activos fijos, materializando los ítems del balance inicial (registrados previamente como borradores) en las tablas definitivas de activos físicos. Antes de proceder, valida que no existan placas duplicadas dentro del mismo saldo inicial y lanza un mensaje de error si las hay. Toma la configuración vigente de activos fijos (valor de menor cuantía, fecha de proceso) y el libro contable oficial para determinar la cuenta contable principal según el tipo de adquisición (renting financiero, leasing, donación, etc.). Inserta los activos en FixedAssetPhysicalAsset con todos sus atributos (placa, serie, ubicación, responsable, proveedor, marca, modelo, póliza, garantía, fecha de adquisición, tipo de adquisición), y luego registra el detalle contable por libro legal (vida útil, método de depreciación, valores depreciados y residuales) en FixedAssetPhysicalAssetDetailBook, así como las partes, accesorios y consumibles del activo en FixedAssetPhysicalAssetParts y su detalle por libro en FixedAssetPhysicalAssetPartsDetailBook.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma un saldo inicial de activos fijos trasladando los ítems, sus libros contables y sus partes/accesorios desde las tablas de balance inicial hacia las tablas físicas operativas de activos fijos.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'No deben existir placas duplicadas dentro de los ítems del mismo saldo inicial a confirmar.; Debe existir al menos un libro contable marcado como oficial (OfficialBook = 1).; Debe existir al menos un registro de configuración de activos fijos (SettingFixedAsset) para obtener año de proceso, monto de menor cuantía y tope mínimo.; Cada ítem del balance inicial debe estar asociado a un FixedAssetItem y a su catálogo (FixedAssetItemCatalog).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la operación está envuelta en TRY/CATCH: si ocurre un error el flujo retorna CodeMessage=999 con el mensaje de error.; El emparejamiento entre ítems del balance y activos físicos creados se hace por Plate, por lo que la placa actúa como clave funcional única dentro del saldo inicial.; Los activos con tipo de adquisición 8 o 10 nunca obtienen detalle contable por libro ni detalle de partes por libro.; Los valores de ajuste (Valorization, Devaluation, AdjustedValue, TransactionValue, InflationAdjustmentValue) siempre se inicializan en 0 al confirmar el saldo inicial.; Las partes recién creadas siempre se inicializan sin salida (HasOutput=0, OutputDate=NULL).; La configuración tomada es siempre la más reciente por ProcessDate de SettingFixedAsset.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] FixedAsset.FixedAssetPhysicalAsset: Por cada ítem del saldo inicial se crea un activo físico con Status=1; la cuenta principal se asigna según AdquisitionType: 3→DebitLoanAccountId (renting financiero), 7→IncomeLeasingAccountId, 9→MainAccountId del catálogo por tipo de adquisición (o IncomeAccountId si es null), otros→IncomeAccountId.; [INSERT] FixedAsset.FixedAssetPhysicalAsset: ApplyMinimunAmount=1 sólo si ValidMinorAmount=1, el año de adquisición coincide con el año de proceso de la configuración vigente, Depreciate=1 y HistoricalValue ≤ TopMinorValue; en cualquier otro caso ApplyMinimunAmount=0.; [INSERT] FixedAsset.FixedAssetPhysicalAssetDetailBook: Sólo se inserta el detalle por libro legal cuando el ítem tiene Depreciate=1 y el activo físico no es de tipo de adquisición 8 ni 10; Valorization, Devaluation, AdjustedValue y TransactionValue se inicializan en 0.; [INSERT] FixedAsset.FixedAssetPhysicalAssetDetailBook: ApplyMinimunAmount=1 a nivel de libro sólo si ValidMinorAmount=1 y HistoricalValue del libro ≤ TopMinorValue; de lo contrario 0.; [INSERT] FixedAsset.FixedAssetPhysicalAssetParts: Por cada parte/accesorio/consumible del ítem del balance inicial se crea su contraparte en el activo físico con HasOutput=0 y OutputDate=NULL.; [INSERT] FixedAsset.FixedAssetPhysicalAssetPartsDetailBook: El detalle contable por libro de cada parte se inserta sólo si el activo físico no tiene AdquisitionType 8 ni 10; ValorizationDays, Valorization, Devaluation, AdjustedValue, TransactionValue e InflationAdjustmentValue se inicializan en 0.; [RETURN_RESULT] FixedAsset.FixedAssetPhysicalAsset: Si hay placas duplicadas devuelve CodeMessage=999 con el listado de placas duplicadas y aborta sin insertar.; [RETURN_RESULT] FixedAsset.FixedAssetPhysicalAsset: Al finalizar exitosamente devuelve CodeMessage=0 con mensaje ''Se confirmó correctamente''; ante error capturado devuelve CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen placas repetidas (count(*)>1) en los ítems del saldo inicial → Construye mensaje con las placas duplicadas, retorna CodeMessage=999 y termina sin insertar nada else Continúa con las inserciones en las tablas físicas; si AdquisitionType del ítem → Determina la cuenta principal del activo físico: 3=préstamo, 7=leasing, 9=cuenta del catálogo por tipo de adquisición, otros=cuenta de ingreso; si ValidMinorAmount, año de adquisición, Depreciate y HistoricalValue vs TopMinorValue → Marca ApplyMinimunAmount=1 indicando que el activo aplica como menor cuantía else ApplyMinimunAmount=0; si AdquisitionType del activo físico es 8 o 10 → No se insertan registros en FixedAssetPhysicalAssetDetailBook ni en FixedAssetPhysicalAssetPartsDetailBook para ese activo else Se insertan los detalles contables por libro; si Depreciate del ítem = 1 → Se generan los registros de detalle contable por libro legal else Se omite el detalle por libro para ese ítem', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetInitialBalance';
-- GO
