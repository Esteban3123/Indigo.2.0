-- =============================================================
-- Author:		Andrea Pahola Coqueco
-- Create date: 26/03/2024
-- Description:	Genera y guarda la amortización de los activos
-- =============================================================
CREATE   PROCEDURE [FixedAsset].[SP_SaveAmortizationDetail]
    @Id INT,
	@ModeConfirm BIT,
	@OperatingUnitId INT,
	@DateDepreciateInitial DATE,
	@DateDepreciateEnd DATE,
	@DaysDepreciateMonth INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@JournalVoucherType VARCHAR(MAX) OUTPUT,
	@ResultConsecutives VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @PhysicalAssetId AS INT,
			@AdquisitionDate AS DATE,
			@ThirdPartyId AS INT,
			@LocationId AS INT,
			@CostCenterId AS INT,
			@DaysDepreciateMonthPhysicalAsset AS INT,
			-------------------------------------------
			@EntityThirdPartyId INT,
			@errors VARCHAR(MAX)

	--Tabla donde especifico la cantidad de dias que vamos a depreciar
	DECLARE @TableMovement TABLE (
		PhysicalAssetId INT, 
		ThirdPartyId INT, 
		LocationId INT, 
		CostCenterId INT, 
		AmortizedDays INT
	)

	--------------------------------------------------  ASIGNACIONES --------------------------------------------------

	SET @EntityThirdPartyId = (SELECT IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId)		

	IF @ModeConfirm = 0 BEGIN
		--Se eliminan los detalles del costo
		DELETE FROM FixedAsset.FixedAssetAmortizationDetailCost 
		WHERE FixedAssetAmortizationDetailId in (SELECT Id FROM FixedAsset.FixedAssetAmortizationDetail WHERE FixedAssetDepreciationId = @Id)
		--Se eliminan los detalles
		DELETE FROM FixedAsset.FixedAssetAmortizationDetail WHERE FixedAssetDepreciationId = @Id
	END

	----------------------------------------------------- PROCESO -----------------------------------------------------
		
	--Se recorren los libros que tenga cada activo y que no tengan la propiedad de salida en false
	DECLARE InfoItem CURSOR FOR 
		SELECT pa.Id, ISNULL(s.IdThirdParty, @EntityThirdPartyId), pa.LocationId, fu.CostCenterId, pa.AdquisitionDate
		FROM FixedAsset.FixedAssetItemCatalog faic
		JOIN FixedAsset.FixedAssetItem fai ON faic.Id = fai.ItemCatalogId
		JOIN FixedAsset.FixedAssetPhysicalAsset pa ON fai.Id = pa.ItemId
		JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb on padb.PhysicalAssetId = pa.Id
		JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
		JOIN FixedAsset.FixedAssetLocation l on l.Id = pa.LocationId
		JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
		LEFT JOIN Common.Supplier s ON pa.SupplierId = s.Id
		WHERE pa.HasOutput = 0 AND pa.Amortize = 1 AND pa.Status = 1 AND faic.Classification = 2
		GROUP BY pa.Id, ISNULL(s.IdThirdParty, @EntityThirdPartyId), pa.LocationId, fu.CostCenterId, pa.AdquisitionDate
		HAVING SUM(padb.DaysPendingDepreciate) > 0 AND SUM(padb.ResidualValue) > 0

	OPEN InfoItem
	FETCH NEXT FROM InfoItem 
	INTO @PhysicalAssetId, @ThirdPartyId, @LocationId, @CostCenterId, @AdquisitionDate
	WHILE @@fetch_status = 0
	BEGIN
		SET @DaysDepreciateMonthPhysicalAsset = 0
		IF NOT EXISTS (
			SELECT 1 
			FROM FixedAsset.FixedAssetKardexItem 
			WHERE PhysicalAssetId = @PhysicalAssetId 
				AND CAST(DocumentDate AS date) BETWEEN @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd)
		) BEGIN -- Si no tiene movimientos en el mes
			IF @DateDepreciateInitial > @AdquisitionDate BEGIN
				SET @DaysDepreciateMonthPhysicalAsset = @DaysDepreciateMonth
			END
			ELSE BEGIN
				SET @DaysDepreciateMonthPhysicalAsset = DATEDIFF(DAY, @AdquisitionDate, @DateDepreciateEnd)
			END

			INSERT INTO @TableMovement(PhysicalAssetId, ThirdPartyId, LocationId, CostCenterId, AmortizedDays)
			VALUES(@PhysicalAssetId, @ThirdPartyId, @LocationId, @CostCenterId, @DaysDepreciateMonthPhysicalAsset)
		END
		ELSE BEGIN --Si tiene movimientos en el mes
			-- Recorro los movimientos para determinar los dias que deprecian en cada ubicacion
			DECLARE @MovementDocumentDate date,
					@MovementPreviousCostCenterId INT,
					@MovementPreviousLocationId INT,
					@MovementLocationId INT,
					@MovementCostCenterId INT, 
					@IndexCursor INT,
					@DateIndexCursor date,
					-----------------------------------------------
					@DaysDepreciateMovementPhysicalAsset INT,
					@QuantityMovement INT

			SET @IndexCursor = 0
			SET @DateIndexCursor = @DateDepreciateInitial
			SET @DaysDepreciateMovementPhysicalAsset = 0

			--Si el Activo ingreso despues de la fecha inicial del mes
			IF @DateDepreciateInitial < @AdquisitionDate BEGIN
				SET @DateIndexCursor = @AdquisitionDate
			END
				
			SET @QuantityMovement = (SELECT count(*)
			FROM FixedAsset.FixedAssetKardexItem k
			WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd))

			DECLARE movement_cursor CURSOR FOR 
				SELECT DocumentDate, ISNULL(PreviousLocationId,LocationId), LocationId, ISNULL(fup.CostCenterId, fu.CostCenterId), fu.CostCenterId
				FROM FixedAsset.FixedAssetKardexItem k
				JOIN FixedAsset.FixedAssetLocation l on l.Id = k.LocationId
				JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
				LEFT JOIN FixedAsset.FixedAssetLocation lp on lp.Id = PreviousLocationId
				LEFT JOIN Payroll.FunctionalUnit fup on fup.Id = lp.FunctionalUnitId
				WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd)
				ORDER BY DocumentDate ASC

			OPEN movement_cursor

			FETCH NEXT FROM movement_cursor 
			INTO @MovementDocumentDate, @MovementPreviousLocationId,@MovementLocationId, @MovementPreviousCostCenterId, @MovementCostCenterId

			WHILE @@FETCH_STATUS = 0
			BEGIN
				SET @IndexCursor += 1
				SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @MovementDocumentDate)

				IF @DaysDepreciateMovementPhysicalAsset > 0 BEGIN -- Si estuvo mas de 0 dias
					SET @DaysDepreciateMonthPhysicalAsset += @DaysDepreciateMovementPhysicalAsset

					IF EXISTS (
						SELECT 1
						FROM @TableMovement tm
						WHERE tm.PhysicalAssetId = @PhysicalAssetId 
							AND tm.ThirdPartyId = @ThirdPartyId 
							AND tm.LocationId = @MovementPreviousLocationId
							AND tm.CostCenterId = @MovementPreviousCostCenterId
					) BEGIN
						UPDATE tm
							SET tm.AmortizedDays += @DaysDepreciateMovementPhysicalAsset
						FROM @TableMovement tm
						WHERE tm.PhysicalAssetId = @PhysicalAssetId 
							AND tm.ThirdPartyId = @ThirdPartyId 
							AND tm.LocationId = @MovementPreviousLocationId
							AND tm.CostCenterId = @MovementPreviousCostCenterId
					END
					ELSE
					BEGIN
						INSERT INTO @TableMovement(PhysicalAssetId, ThirdPartyId, LocationId, CostCenterId, AmortizedDays)
						VALUES(@PhysicalAssetId, @ThirdPartyId, @MovementPreviousLocationId,  @MovementPreviousCostCenterId, @DaysDepreciateMovementPhysicalAsset)
					END
				END

				SET @DateIndexCursor = @MovementDocumentDate

				---Si es el ultimo recorrido agrego entonces la unidad actual en la que se realizo el ultimo movimiento hasta fin de mes
				IF @IndexCursor = @QuantityMovement BEGIN
					SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd)
					SET @DaysDepreciateMonthPhysicalAsset += @DaysDepreciateMovementPhysicalAsset

					IF EXISTS (
						SELECT 1
						FROM @TableMovement tm
						WHERE tm.PhysicalAssetId = @PhysicalAssetId 
							AND tm.ThirdPartyId = @ThirdPartyId 
							AND tm.LocationId = @MovementLocationId
							AND tm.CostCenterId = @MovementCostCenterId
					) BEGIN
						UPDATE tm
							SET tm.AmortizedDays += @DaysDepreciateMovementPhysicalAsset
						FROM @TableMovement tm
						WHERE tm.PhysicalAssetId = @PhysicalAssetId 
							AND tm.ThirdPartyId = @ThirdPartyId 
							AND tm.LocationId = @MovementLocationId
							AND tm.CostCenterId = @MovementCostCenterId
					END
					ELSE
					BEGIN
						INSERT INTO @TableMovement(PhysicalAssetId, ThirdPartyId, LocationId, CostCenterId, AmortizedDays)
						VALUES(@PhysicalAssetId, @ThirdPartyId, @MovementLocationId, @MovementCostCenterId, @DaysDepreciateMovementPhysicalAsset)
					END					
				END

				FETCH NEXT FROM movement_cursor 
				INTO @MovementDocumentDate, @MovementPreviousLocationId,@MovementLocationId, @MovementPreviousCostCenterId, @MovementCostCenterId
			END

			Close movement_cursor
			Deallocate movement_cursor
		END

		--Se pasa a la siguiente posicion del cursor
		FETCH NEXT FROM InfoItem 
		INTO @PhysicalAssetId, @ThirdPartyId, @LocationId, @CostCenterId, @AdquisitionDate
	END

	Close InfoItem
	Deallocate InfoItem

	--------------------------------------- Se empieza a insertar las tablas de depreciación -----------------------------------------------
		
	--Se declaran las tablas temporales para poder comparar si hay que recalcular
	DECLARE @FixedAssetAmortizationDetailTemp table(
		Id INT primary key identity(1,1),
		FixedAssetDepreciationId INT, 
		FixedAssetPhysicalAssetDetailBookId INT,
		AccumulatedAmortization numeric(18,2), 
		ResidualValue numeric(18,2),
		AmortizedDays numeric(18,2), 
		AmortizedValue numeric(18,2)
	)
		
	DECLARE @FixedAssetAmortizationDetailCostTemp table(
		Id INT primary key identity(1,1), 
		FixedAssetAmortizationDetailId INT, 
		MainAccountId INT, 
		ThirdPartyId INT, 
		LocationId INT, 
		CostCenterId INT, 
		AmortizedDays INT, 
		AmortizedValue numeric(20,4)
	)

		
	--Se inserta en las tablas temporales
	INSERT INTO @FixedAssetAmortizationDetailTemp
	(
		[FixedAssetDepreciationId],
		[FixedAssetPhysicalAssetDetailBookId],
		[AccumulatedAmortization], 
		[ResidualValue],
		[AmortizedDays],
		[AmortizedValue]
	)
	SELECT 
		@Id,
		padb.Id,
		padb.DepreciatedValue, 
		padb.ResidualValue,
		IIF(tm.AmortizedDays > padb.DaysPendingDepreciate, padb.DaysPendingDepreciate, tm.AmortizedDays),
		FixedAsset.fnCalculateDepreciateValue
		(
			padb.DepreciationType, 
			padb.DepreciatedValue,
			padb.ResidualValue,
			padb.PercentageRescue, 
			0, --fai.DepreciateByTimeUse, 
			fal.UseTime, 
			padb.DepreciatedDays, 
			padb.DaysPendingDepreciate,
			tm.AmortizedDays
		) 
	FROM 
	(
		SELECT PhysicalAssetId, SUM(AmortizedDays) AmortizedDays
		FROM @TableMovement
		GROUP BY PhysicalAssetId
	) tm
	JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON tm.PhysicalAssetId = padb.PhysicalAssetId
	JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
	JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
	JOIN FixedAsset.FixedAssetItem fai on fai.Id = pa.ItemId
	JOIN FixedAsset.FixedAssetLocation fal on fal.Id = pa.LocationId
	WHERE padb.DaysPendingDepreciate > 0 
		AND padb.ResidualValue > 0 
		AND tm.AmortizedDays > 0
		
	INSERT INTO @FixedAssetAmortizationDetailCostTemp
	(
		[FixedAssetAmortizationDetailId], 
		[MainAccountId], 
		[ThirdPartyId],
		[LocationId],
		[CostCenterId], 
		[AmortizedDays], 
		[AmortizedValue]
	)
	SELECT	dd.Id, 
			pa.MainAccountId, 
			tm.ThirdPartyId, 
			tm.LocationId,
			tm.CostCenterId, 
			IIF(tm.AmortizedDays > dd.AmortizedDays, dd.AmortizedDays, tm.AmortizedDays), 
			FixedAsset.fnCalculateDepreciationValueCost(dd.AmortizedValue, dd.AmortizedDays, IIF(tm.AmortizedDays > dd.AmortizedDays, dd.AmortizedDays, tm.AmortizedDays)) 
	FROM @FixedAssetAmortizationDetailTemp dd
	JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset pa ON padb.PhysicalAssetId = pa.Id
	JOIN @TableMovement tm ON pa.Id = tm.PhysicalAssetId

	--Ajustamos diferencias por redondeos
	UPDATE faddc
		SET faddc.AmortizedValue = faddc.AmortizedValue + (fadd.AmortizedValue - faddcd.AmortizedValue)
	FROM @FixedAssetAmortizationDetailTemp fadd
	JOIN 
	(
		SELECT 
			faddc.FixedAssetAmortizationDetailId, 
			SUM(faddc.AmortizedValue) AmortizedValue,
			MAX(faddc.Id) FixedAssetAmortizationDetailCostId
		FROM @FixedAssetAmortizationDetailCostTemp faddc
		GROUP BY faddc.FixedAssetAmortizationDetailId
	) faddcd ON fadd.Id = faddcd.FixedAssetAmortizationDetailId
		AND fadd.AmortizedValue <> faddcd.AmortizedValue
	JOIN @FixedAssetAmortizationDetailCostTemp faddc ON faddcd.FixedAssetAmortizationDetailCostId = faddc.Id
	WHERE fadd.FixedAssetDepreciationId = @Id

	DELETE FROM @FixedAssetAmortizationDetailCostTemp WHERE AmortizedValue <= 0

	----------------------------------------------------  GUARDADO ----------------------------------------------------

	--Si no se esta confirmando
	IF NOT @ModeConfirm = 1
	BEGIN
		INSERT INTO [FixedAsset].[FixedAssetAmortizationDetail]
		(
			[FixedAssetDepreciationId],
			[FixedAssetPhysicalAssetDetailBookId],
			[AccumulatedAmortization], 
			[ResidualValue],
			[AmortizedDays],
			[AmortizedValue]
		)
		SELECT	[FixedAssetDepreciationId],
				[FixedAssetPhysicalAssetDetailBookId],
				[AccumulatedAmortization], 
				[ResidualValue],
				[AmortizedDays],
				[AmortizedValue]
		FROM @FixedAssetAmortizationDetailTemp
		
		--Se insertan los costos del detalle de la depreciación con los datos que se registraron anteriormente y la tabla temporal de movimientos
		INSERT INTO [FixedAsset].[FixedAssetAmortizationDetailCost]
		(
			[FixedAssetAmortizationDetailId], 
			[MainAccountId], 
			[ThirdPartyId], 
			[LocationId],
			[CostCenterId], 
			[AmortizedDays], 
			[AmortizedValue]
		)
		SELECT	dd.Id [FixedAssetAmortizationDetailId], 
				ddct.[MainAccountId], 
				ddct.[ThirdPartyId], 
				ddct.LocationId,
				ddct.[CostCenterId], 
				ddct.[AmortizedDays], 
				ddct.[AmortizedValue]
		FROM FixedAsset.FixedAssetAmortizationDetail dd
		JOIN @FixedAssetAmortizationDetailTemp ddt ON dd.FixedAssetPhysicalAssetDetailBookId = ddt.FixedAssetPhysicalAssetDetailBookId
		JOIN @FixedAssetAmortizationDetailCostTemp ddct ON ddt.Id = ddct.FixedAssetAmortizationDetailId
		WHERE dd.FixedAssetDepreciationId = @Id

		SELECT	@CodeResult = 0, 
				@MessageResult = 'Se guardó la Amortización de Activos'
		RETURN
	END

	---------------------------------------------------- VALIDACION ---------------------------------------------------

	--Se valida que los datos nuevos que se registraron en la tabla temporal sean los mismos que estan en la BD de lo contrario se debe recalcular
	IF EXISTS (
		SELECT 1
		FROM
		(
			SELECT	ddt.FixedAssetPhysicalAssetDetailBookId,
					ddt.AmortizedDays,
					ddt.AmortizedValue
			FROM @FixedAssetAmortizationDetailTemp ddt
		) ddt
		FULL JOIN
		(
			SELECT	dd.FixedAssetPhysicalAssetDetailBookId,
					dd.AmortizedDays,
					dd.AmortizedValue
			FROM FixedAsset.FixedAssetAmortizationDetail dd
			WHERE dd.FixedAssetDepreciationId = @Id
		) dd ON ddt.FixedAssetPhysicalAssetDetailBookId = dd.FixedAssetPhysicalAssetDetailBookId 
		WHERE ISNULL(ddt.AmortizedDays, 0) <> ISNULL(dd.AmortizedDays, 0) OR ISNULL(ddt.AmortizedValue, 0) <> ISNULL(dd.AmortizedValue, 0)
	) 
	BEGIN
		SET @errors = STUFF((
			SELECT DISTINCT CONCAT(N';', fapa.Plate, ' Valores: ', ISNULL(ddt.AmortizedValue, 0), ' - ', ISNULL(dd.AmortizedValue, 0), ' Días: ', ISNULL(ddt.AmortizedDays, 0), ' - ', ISNULL(dd.AmortizedDays, 0))
			FROM
			(
				SELECT	ddt.FixedAssetPhysicalAssetDetailBookId,
						ddt.AmortizedDays,
						ddt.AmortizedValue
				FROM @FixedAssetAmortizationDetailTemp ddt
			) ddt
			FULL JOIN
			(
				SELECT	dd.FixedAssetPhysicalAssetDetailBookId,
						dd.AmortizedDays,
						dd.AmortizedValue
				FROM FixedAsset.FixedAssetAmortizationDetail dd
				WHERE dd.FixedAssetDepreciationId = @Id
			) dd ON ddt.FixedAssetPhysicalAssetDetailBookId = dd.FixedAssetPhysicalAssetDetailBookId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON ISNULL(ddt.FixedAssetPhysicalAssetDetailBookId, dd.FixedAssetPhysicalAssetDetailBookId) = fapadb.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapadb.PhysicalAssetId = fapa.Id
			WHERE ISNULL(ddt.AmortizedDays, 0) <> ISNULL(dd.AmortizedDays, 0) OR ISNULL(ddt.AmortizedValue, 0) <> ISNULL(dd.AmortizedValue, 0)
			FOR XML path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N''
		)
				
		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('Los valores del detalle de la amortización han cambiado y se debe recalcular', ' ', @errors)
		RETURN
	END
	
	--Se valida que los datos nuevos de costos que se registraron en la tabla temporal sean los mismos que estan en la BD de lo contrario se debe recalcular
	IF EXISTS (
		SELECT 1
		FROM
		(
			SELECT	ddt.FixedAssetPhysicalAssetDetailBookId,
					ddct.ThirdPartyId,
					ddct.LocationId,
					ddct.CostCenterId,
					ddct.AmortizedDays,
					ddct.AmortizedValue
			FROM @FixedAssetAmortizationDetailTemp ddt
			JOIN @FixedAssetAmortizationDetailCostTemp ddct ON ddt.Id = ddct.FixedAssetAmortizationDetailId
		) ddt
		FULL JOIN
		(
			SELECT	dd.FixedAssetPhysicalAssetDetailBookId,
					ddc.ThirdPartyId,
					ddc.LocationId,
					ddc.CostCenterId,
					ddc.AmortizedDays,
					ddc.AmortizedValue
			FROM FixedAsset.FixedAssetAmortizationDetail dd
			JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on ddc.FixedAssetAmortizationDetailId = dd.Id
			WHERE dd.FixedAssetDepreciationId = @Id
		) dd ON ddt.FixedAssetPhysicalAssetDetailBookId = dd.FixedAssetPhysicalAssetDetailBookId
			AND ddt.ThirdPartyId = dd.ThirdPartyId AND ddt.LocationId = dd.LocationId AND ddt.CostCenterId = dd.CostCenterId		 
		WHERE ISNULL(ddt.AmortizedDays, 0) <> ISNULL(dd.AmortizedDays, 0) OR ISNULL(ddt.AmortizedValue, 0) <> ISNULL(dd.AmortizedValue, 0)
	) BEGIN
		SET @errors = STUFF((
			SELECT DISTINCT CONCAT(N';', fapa.Plate, ' Valores: ', ISNULL(ddt.AmortizedValue, 0), ' - ', ISNULL(dd.AmortizedValue, 0), ' Días: ', ISNULL(ddt.AmortizedDays, 0), ' - ', ISNULL(dd.AmortizedDays, 0))
			FROM
			(
				SELECT	ddt.FixedAssetPhysicalAssetDetailBookId,
						ddct.ThirdPartyId,
						ddct.LocationId,
						ddct.CostCenterId,
						ddct.AmortizedDays,
						ddct.AmortizedValue
				FROM @FixedAssetAmortizationDetailTemp ddt
				JOIN @FixedAssetAmortizationDetailCostTemp ddct ON ddt.Id = ddct.FixedAssetAmortizationDetailId
			) ddt
			FULL JOIN
			(
				SELECT	dd.FixedAssetPhysicalAssetDetailBookId,
						ddc.ThirdPartyId,
						ddc.LocationId,
						ddc.CostCenterId,
						ddc.AmortizedDays,
						ddc.AmortizedValue
				FROM FixedAsset.FixedAssetAmortizationDetail dd
				JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on ddc.FixedAssetAmortizationDetailId = dd.Id
				WHERE dd.FixedAssetDepreciationId = @Id
			) dd ON ddt.FixedAssetPhysicalAssetDetailBookId = dd.FixedAssetPhysicalAssetDetailBookId
				AND ddt.ThirdPartyId = dd.ThirdPartyId AND ddt.LocationId = dd.LocationId AND ddt.CostCenterId = dd.CostCenterId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON ISNULL(ddt.FixedAssetPhysicalAssetDetailBookId, dd.FixedAssetPhysicalAssetDetailBookId) = fapadb.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapadb.PhysicalAssetId = fapa.Id
			WHERE ISNULL(ddt.AmortizedDays, 0) <> ISNULL(dd.AmortizedDays, 0) OR ISNULL(ddt.AmortizedValue, 0) <> ISNULL(dd.AmortizedValue, 0)
			FOR XML path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N''
		)
				
		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('Los valores del costo de la amortización han cambiado y se debe recalcular ', @errors)
		RETURN
	END

	--Se valida que el detalle y el costo coincidan
	IF EXISTS
	(
		SELECT 1
		FROM FixedAsset.FixedAssetAmortizationDetail fadd
		LEFT JOIN
		(
			SELECT	FixedAssetAmortizationDetailId,
					SUM(AmortizedValue) AmortizedValue					
			FROM FixedAsset.FixedAssetAmortizationDetailCost
			GROUP BY FixedAssetAmortizationDetailId
		) faddc ON fadd.Id = faddc.FixedAssetAmortizationDetailId
		WHERE fadd.FixedAssetDepreciationId = @Id
			AND fadd.AmortizedValue <> ISNULL(faddc.AmortizedValue, 0)
	)
	BEGIN
		SET @errors = STUFF((
			SELECT DISTINCT CONCAT(N';',fapa.Plate, ' Valores: ', fadd.AmortizedValue, ' - ', ISNULL(faddc.AmortizedValue, 0))
			FROM FixedAsset.FixedAssetAmortizationDetail fadd
			LEFT JOIN
			(
				SELECT	FixedAssetAmortizationDetailId,
						SUM(AmortizedValue) AmortizedValue					
				FROM FixedAsset.FixedAssetAmortizationDetailCost
				GROUP BY FixedAssetAmortizationDetailId
			) faddc ON fadd.Id = faddc.FixedAssetAmortizationDetailId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fadd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapadb.PhysicalAssetId = fapa.Id
			WHERE fadd.FixedAssetDepreciationId = @Id
				AND fadd.AmortizedValue <> ISNULL(faddc.AmortizedValue, 0)
			FOR XML path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N''
		)

		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('El valor del costo de la amortización es diferente al valor detallado ', @errors)
		RETURN
	END
	
	--Valida que las unidades Funcionales tengan Estructura Contable
	IF EXISTS (
		SELECT 1
		FROM FixedAsset.FixedAssetAmortizationDetail dd
		JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc ON dd.Id = ddc.FixedAssetAmortizationDetailId
		JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
		JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
		WHERE dd.FixedAssetDepreciationId = @Id AND f.AccountingStructureId IS NULL
	) BEGIN
		SELECT @errors = STUFF((
			SELECT DISTINCT CONCAT(N';', ' La unidad funcional ', f.Code, ' no tiene estructura contable')
			FROM FixedAsset.FixedAssetDepreciation d
			JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
			JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
			JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
			JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
			WHERE d.Id = @Id AND f.AccountingStructureId is NULL
			FOR XML path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N''
		)
			
		SELECT	@CodeResult = 999, 
				@MessageResult = @errors
		RETURN
	END
			
	---- Validamos que todos los catalogos tengan parametrizada la estructura contable
	IF EXISTS (
			SELECT 1
				FROM FixedAsset.FixedAssetAmortizationDetail dd
				JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
				JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
				JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
				JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON dd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset pa ON fapadb.PhysicalAssetId = pa.Id
				JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = ast.Id
				WHERE dd.FixedAssetDepreciationId = @Id AND cd.id IS NULL AND c.HandlesDepreciationbyDistribution = 0
	) BEGIN
		SELECT @errors = STUFF((
			SELECT DISTINCT CONCAT(N';', ' El catálogo ', c.Code, ' no tiene parametrizada la estructura contable ', ast.Code)
			FROM FixedAsset.FixedAssetAmortizationDetail dd
			JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
			JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
			JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
			JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
			JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON dd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
			JOIN FixedAsset.FixedAssetPhysicalAsset pa ON fapadb.PhysicalAssetId = pa.Id
			JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
			JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = ast.Id
			WHERE dd.FixedAssetDepreciationId = @Id AND cd.id IS NULL
			FOR XML path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N''
		)
			
		SELECT	@CodeResult = 999, 
				@MessageResult = @errors
		RETURN
	END

	----------------------------------------------------  COMPROBANTE ----------------------------------------------------
			
	--Cabecera del comprobante contable
	DECLARE @TableJournalVoucher table(
		IdJournalVoucher INT NOT NULL, 
		VoucherDate datetime NOT NULL, 
		Imported BIT NOT NULL, 
		[Status] TINYINT NOT NULL, 
		Detail VARCHAR(500) NULL, 
		EntityCode VARCHAR(20) NULL, 
		EntityId INT NULL, 
		EntityName VARCHAR(250) NULL, 
		IsClosedYear BIT NOT NULL, 
		LegalBookId INT NOT NULL
	)

	--Detalles del comprobante contable
	DECLARE @TableJournalVoucherDetail table(
		IdMainAccount INT NOT NULL, 
		IdThirdParty INT NULL, 
		IdCostCenter INT NULL,
		DebitValue decimal(20, 4) NOT NULL,
		CreditValue decimal(20, 4) NOT NULL,
		Detail VARCHAR(MAX) NULL,
		IdRetention INT NULL,
		RetentionRate decimal(5, 2) NULL,
		BaseValue decimal(18, 0) NULL,
		BillingValue decimal(18, 0) NULL, 
		LegalBookId INT NOT NULL
	)

	--Id del tipo de comprobante contable
	DECLARE @IdJournalVoucher INT

	--Fecha de proceso de parámetros con el que se asigna la fecha al comprobante
	DECLARE @ProcessDate date

	--Se obtiene el tipo de comprobante contable y la fecha de proceso de parámetros
	SELECT @IdJournalVoucher = IdIntangibleAssetAmortizationVoucher, @ProcessDate = ProcessDate 
	FROM FixedAsset.SettingFixedAsset 
	WHERE OperatingUnitId = @OperatingUnitId

	--Se inicia con la creación del comprobante contable
	DECLARE @LegalBookId INT
	DECLARE InfoItem CURSOR FOR 
		SELECT DISTINCT fapadb.LegalBookId 
		FROM FixedAsset.FixedAssetAmortizationDetail dd
		JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON dd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
		WHERE dd.FixedAssetDepreciationId = @Id

	OPEN InfoItem
	FETCH NEXT FROM InfoItem INTO @LegalBookId
	WHILE @@fetch_status = 0
	BEGIN

		--Se inserta la cabecera del comprobante
		INSERT INTO @TableJournalVoucher
		(IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear, LegalBookId)
		SELECT	@IdJournalVoucher, @ProcessDate, 0, 2, 
				CONCAT('Comprobante generado desde depreciación para cerrar el mes ', DATENAME(MONTH, @DateDepreciateInitial), ' del año ', ClosingYear), 
				Code, @Id, 'FixedAssetDepreciation', 0, @LegalBookId
		FROM FixedAsset.FixedAssetDepreciation
		WHERE Id = @Id

		--Se insertan los detalles del comprobante

		IF EXISTS (
				SELECT 1 
				FROM FixedAsset.FixedAssetDepreciation d
				INNER JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
				INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON dd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
				INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = fapadb.PhysicalAssetId
				INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				INNER JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
				INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
				INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
				INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id 
				INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = cd.LoanSpendAccountId
				INNER JOIN GeneralLedger.MainAccounts maloan on maloan.Id = cd.ExpenseLoanAccountId
				INNER JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = cd.LoanLeasingSpendAccountId
				INNER JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
				WHERE d.Id = @Id AND fapadb.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
		) BEGIN
				--Maneja Depreciacion por Distribucion en SI

				--- Inserto la cuenta de Depreciacion que afecto al Credito (Cuenta del Activo)
				-- Se modifica la consulta, validando si la cuenta es de tipo leassing, me realice la siguiente validación con la cuenta leassing tanto del centro de costo, como del tercero.
				INSERT INTO @TableJournalVoucherDetail
					(IdMainAccount, IdThirdParty, IdCostCenter,	DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
				SELECT 
					CASE pa.AdquisitionType 
						WHEN 3 THEN c.LoanLeasingAccountId
						WHEN 7 THEN c.DepreciationLeasingAccountId 
						WHEN 9 THEN c.FinancialRentingAccountId
						ELSE c.DepreciationAccountId 
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
					END
					,0
					,SUM(ddc.AmortizedValue)
					,'Detalle generado desde amortización'
					,NULL
					,NULL
					,NULL
					,NULL
					,@LegalBookId
				FROM FixedAsset.FixedAssetDepreciation d
				JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
				JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
				JOIN GeneralLedger.MainAccounts ma on ma.Id = c.DepreciationAccountId
				JOIN GeneralLedger.MainAccounts maloan on maloan.Id = c.LoanLeasingAccountId
				JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = c.DepreciationLeasingAccountId				
				WHERE d.Id = @Id AND padb.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
				GROUP BY ddc.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
					c.DepreciationAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
					c.DepreciationLeasingAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 
					c.LoanLeasingAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty, c.FinancialRentingAccountId

				--- Ahora debitamos la cuenta de gastos de depreciacion (Costo o Gasto)
				-- Se modifica la consulta, validando si la cuenta es de tipo leassing, me realice la siguiente validación con la cuenta leassing tanto del centro de costo, como del tercero.
				INSERT INTO @TableJournalVoucherDetail
					(IdMainAccount, IdThirdParty, IdCostCenter,	DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
				SELECT 
					CASE pa.AdquisitionType 
						WHEN 3 THEN maloan.Id
						WHEN 7 THEN maleasing.Id 
						WHEN 9 THEN maFinancialRenting.Id 
						ELSE ma.Id 
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId
								ELSE NULL 
							END
					END
					,cd.CostCenterId
					,SUM(ddc.AmortizedValue * cd.DistributionPercentage / 100)
					,0
					,'Detalle generado desde amortización'
					,NULL
					,NULL
					,NULL
					,NULL
					,@LegalBookId
				FROM FixedAsset.FixedAssetDepreciation d
				JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
				JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
				INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id
				INNER JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc ON dd.Id = ddc.FixedAssetAmortizationDetailId
				INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId														 
				INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = cd.LoanSpendAccountId
				INNER JOIN GeneralLedger.MainAccounts maloan ON maloan.Id = cd.ExpenseLoanAccountId
				INNER JOIN GeneralLedger.MainAccounts maleasing ON maleasing.Id = cd.LoanLeasingSpendAccountId	
				INNER JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
				WHERE d.Id = @Id AND padb.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
				GROUP BY ddc.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
					ma.Id, ma.HandlesCostCenter, ma.HandlesThirdParty, 
					maleasing.Id, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 					
					maloan.Id, maloan.HandlesCostCenter, maloan.HandlesThirdParty, maFinancialRenting.Id,
					cd.CostCenterId

		END
		ELSE
		BEGIN
				--Maneja Depreciacion por Distribucion en NO

				--- Inserto la cuenta de Depreciacion que afecto al Credito (Cuenta del Activo)
				-- Se modifica la consulta, validando si la cuenta es de tipo leassing, me realice la siguiente validación con la cuenta leassing tanto del centro de costo, como del tercero.
				INSERT INTO @TableJournalVoucherDetail
					(IdMainAccount, IdThirdParty, IdCostCenter,	DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
				SELECT 
					CASE pa.AdquisitionType 
						WHEN 3 THEN c.LoanLeasingAccountId
						WHEN 7 THEN c.DepreciationLeasingAccountId 
						WHEN 9 THEN c.FinancialRentingAccountId
						ELSE c.DepreciationAccountId 
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
					END
					,0
					,SUM(ddc.AmortizedValue)
					,'Detalle generado desde amortización'
					,NULL
					,NULL
					,NULL
					,NULL
					,@LegalBookId
				FROM FixedAsset.FixedAssetDepreciation d
				JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
				JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
				JOIN GeneralLedger.MainAccounts ma on ma.Id = c.DepreciationAccountId
				JOIN GeneralLedger.MainAccounts maloan on maloan.Id = c.LoanLeasingAccountId
				JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = c.DepreciationLeasingAccountId				
				WHERE d.Id = @Id AND padb.LegalBookId = @LegalBookId
				GROUP BY ddc.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
					c.DepreciationAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
					c.DepreciationLeasingAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 
					c.LoanLeasingAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty, c.FinancialRentingAccountId

				--- Ahora debitamos la cuenta de gastos de depreciacion (Costo o Gasto)
				-- Se modifica la consulta, validando si la cuenta es de tipo leassing, me realice la siguiente validación con la cuenta leassing tanto del centro de costo, como del tercero.
				INSERT INTO @TableJournalVoucherDetail
					(IdMainAccount, IdThirdParty, IdCostCenter,	DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
				SELECT 
					CASE pa.AdquisitionType 
						WHEN 3 THEN maloan.Id
						WHEN 7 THEN maleasing.Id 
						WHEN 9 THEN maFinancialRenting.Id 
						ELSE ma.Id 
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesThirdParty 
								WHEN 1 THEN ddc.ThirdPartyId
								ELSE NULL 
							END
					END
					,CASE pa.AdquisitionType 
						WHEN 3 THEN 
							CASE maloan.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						WHEN 7 THEN 
							CASE maleasing.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
						ELSE 
							CASE ma.HandlesCostCenter 
								WHEN 1 THEN ddc.CostCenterId 
								ELSE NULL 
							END
					END
					,SUM(ddc.AmortizedValue)
					,0
					,'Detalle generado desde amortización'
					,NULL
					,NULL
					,NULL
					,NULL
					,@LegalBookId
				FROM FixedAsset.FixedAssetDepreciation d
				JOIN FixedAsset.FixedAssetAmortizationDetail dd on d.Id = dd.FixedAssetDepreciationId
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
				JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				JOIN FixedAsset.FixedAssetAmortizationDetailCost ddc on dd.Id = ddc.FixedAssetAmortizationDetailId
				JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
				JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
				JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = f.AccountingStructureId
				JOIN GeneralLedger.MainAccounts ma on ma.Id = cd.LoanSpendAccountId
				JOIN GeneralLedger.MainAccounts maloan on maloan.Id = cd.ExpenseLoanAccountId
				JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = cd.LoanLeasingSpendAccountId
				JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
				WHERE d.Id = @Id AND padb.LegalBookId = @LegalBookId
				GROUP BY ddc.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
					ma.Id, ma.HandlesCostCenter, ma.HandlesThirdParty, 
					maleasing.Id, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 					
					maloan.Id, maloan.HandlesCostCenter, maloan.HandlesThirdParty, maFinancialRenting.Id
		END

		--Se pasa a la siguiente posicion del cursor
		FETCH NEXT FROM InfoItem INTO @LegalBookId
		CONTINUE
	END

	Close InfoItem
	Deallocate InfoItem

	--Se guarda los comprobantes generados
	--DECLARE InfoItem CURSOR FOR SELECT LegalBookId FROM @TableJournalVoucher
		
	--Se declara la variable para saber en que posición coloco la coma
	DECLARE @Cont INT = 0
	DECLARE InfoItemDepreciarion CURSOR FOR 
		SELECT DISTINCT fapadb.LegalBookId 
		FROM FixedAsset.FixedAssetAmortizationDetail dd
		JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON dd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
		WHERE dd.FixedAssetDepreciationId = @Id

	OPEN InfoItemDepreciarion
	FETCH NEXT FROM InfoItemDepreciarion INTO @LegalBookId
	WHILE @@fetch_status = 0
	BEGIN			
		--genero el XML para guardar el comprobante 
		DECLARE @resultJournalVoucher table (code VARCHAR(20),MessageResult VARCHAR(MAX),IdJournalVoucher integer)
		DELETE FROM @resultJournalVoucher
		DECLARE @JournalVoucherXML AS XML
		SELECT @JournalVoucherXML =  convert(xml, (SELECT * 
		FROM @TableJournalVoucher JournalVoucher 
		INNER JOIN @TableJournalVoucherDetail JournalVoucherDetail on JournalVoucher.LegalBookId = JournalVoucherDetail.LegalBookId
		WHERE JournalVoucher.LegalBookId = @LegalBookId For xml AUTO,TYPE, ELEMENTS))
		--SELECT @JournalVoucherXML
				
		INSERT @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

		IF (SELECT code  FROM @resultJournalVoucher) = '999' 
		BEGIN		
			Close InfoItemDepreciarion
			Deallocate InfoItemDepreciarion
			DECLARE @errorJV VARCHAR(MAX)
			SELECT @errorJV = MessageResult  FROM @resultJournalVoucher
				
			SELECT	@CodeResult = 999, 
					@MessageResult = @errorJV
			RETURN
		END  
				
		DECLARE @JournalVoucherId AS INT
		SELECT @JournalVoucherId = IdJournalVoucher  FROM @resultJournalVoucher
		DECLARE @Consecutive AS VARCHAR(MAX) = ISNULL( (SELECT CAST( Consecutive AS VARCHAR(30))  
		FROM GeneralLedger.JournalVouchers WHERE id = @JournalVoucherId ),0)
				
		IF @Cont = 0 --Si es la primera pasada
		BEGIN
			SET @ResultConsecutives = @Consecutive
		END
		ELSE --Si es apartir de la segunda pasada
		BEGIN
			SET @ResultConsecutives = @ResultConsecutives + ', ' + @Consecutive
		END

		SET @Cont = @Cont + 1

		--Se pasa a la siguiente posicion del cursor
		FETCH NEXT FROM InfoItemDepreciarion INTO @LegalBookId
		continue
	END
	Close InfoItemDepreciarion
	Deallocate InfoItemDepreciarion
			
	--Se consulta el tipo de comprobante contable para enviarlo al form
	SELECT @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name) 
	FROM FixedAsset.SettingFixedAsset sfa
	INNER JOIN GeneralLedger.JournalVoucherTypes jvt on jvt.Id = sfa.IdDepreciationAccountingVoucher
	WHERE sfa.OperatingUnitId = @OperatingUnitId

	--Se actualizan los campos respectivos a la depreciación de la tabla FixedAssetPhysicalAssetDetailBook
	UPDATE padb 
		SET padb.DaysPendingDepreciate = IIF(padb.ResidualValue = dd.AmortizedValue, 0, padb.DaysPendingDepreciate - dd.AmortizedDays),
			padb.DepreciatedDays = padb.DepreciatedDays + dd.AmortizedDays,
			padb.DepreciatedValue = padb.DepreciatedValue + dd.AmortizedValue,
			padb.ResidualValue = padb.ResidualValue - dd.AmortizedValue
	FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb 
	join
	(
		SELECT dd.FixedAssetPhysicalAssetDetailBookId,
			SUM(dd.AmortizedDays) AmortizedDays,
			SUM(dd.AmortizedValue) AmortizedValue
		FROM FixedAsset.FixedAssetAmortizationDetail dd
		WHERE dd.FixedAssetDepreciationId = @Id
		GROUP BY dd.FixedAssetPhysicalAssetDetailBookId
	) dd on padb.Id = dd.FixedAssetPhysicalAssetDetailBookId

	SELECT	@CodeResult = 0, 
			@MessageResult = CONCAT('Se confirmó la Amortización de Activos, generando los comprobantes contables ', @ResultConsecutives, ' de tipo ', @JournalVoucherType)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y registra el detalle de amortización (depreciación) periódica de los activos fijos intangibles (clasificación 2) de la organización para un rango de fechas determinado. Calcula los días a depreciar por cada activo físico según sus movimientos de kardex y ubicación, distribuyendo el cargo a los centros de costo y unidades funcionales correspondientes; luego inserta o reemplaza los registros en la tabla de detalle de amortización y sus costos asociados. En modo confirmación (@ModeConfirm=1) genera el comprobante contable (Journal Voucher) en el libro legal activo de contabilidad general, registrando el asiento de depreciación por unidad operativa. Utiliza tablas como FixedAssetAmortizationDetail, FixedAssetAmortizationDetailCost, FixedAssetKardexItem, FixedAssetPhysicalAssetDetailBook y el configurador contable (GeneralLedgerSettings) para obtener el tercero DIAN y respetar los libros legales vigentes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAmortizationDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAmortizationDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y persiste el detalle mensual de amortización de activos fijos clasificados como intangibles, distribuyendo los días por ubicación según movimientos de kardex, valida consistencia y, al confirmar, genera el comprobante contable por libro legal y actualiza saldos depreciables.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAmortizationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en FixedAsset.SettingFixedAsset para la unidad operativa con IdIntangibleAssetAmortizationVoucher y ProcessDate.; Debe existir GeneralLedger.GeneralLedgerSettings con IdDian para la unidad operativa (tercero por defecto).; Los activos a amortizar deben tener HasOutput=0, Amortize=1, Status=1 y Classification=2 en su catálogo.; Los libros legales asociados deben tener Status=1 (vigentes).; La suma de DaysPendingDepreciate y ResidualValue por activo debe ser > 0 para ser considerado.; Las unidades funcionales asociadas deben tener AccountingStructureId definido.; Los catálogos deben tener parametrizada la estructura contable salvo cuando HandlesDepreciationbyDistribution=1.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAmortizationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] FixedAsset.FixedAssetAmortizationDetailCost: Cuando @ModeConfirm=0 se eliminan los costos de detalle previos cuyos detalles pertenecen a la depreciación @Id.; [DELETE] FixedAsset.FixedAssetAmortizationDetail: Cuando @ModeConfirm=0 se eliminan los detalles de amortización existentes para FixedAssetDepreciationId=@Id.; [INSERT] FixedAsset.FixedAssetAmortizationDetail: Cuando NOT @ModeConfirm=1 (modo cálculo), inserta un detalle por libro de cada activo con los días amortizados topados a DaysPendingDepreciate y el valor calculado por fnCalculateDepreciateValue.; [INSERT] FixedAsset.FixedAssetAmortizationDetailCost: Cuando NOT @ModeConfirm=1, inserta los costos por ubicación/centro de costo/tercero usando fnCalculateDepreciationValueCost; se omiten registros con AmortizedValue<=0.; [UPDATE] @FixedAssetAmortizationDetailCostTemp: Cuando la suma de costos no coincide con el valor del detalle, ajusta la diferencia por redondeo sumándola al registro de mayor Id de cada detalle.; [UPDATE] FixedAsset.FixedAssetPhysicalAssetDetailBook: Al confirmar, si ResidualValue=AmortizedValue entonces DaysPendingDepreciate pasa a 0; en caso contrario se resta AmortizedDays. Se acumulan DepreciatedDays y DepreciatedValue, y se descuenta ResidualValue por el valor amortizado.; [INSERT] GeneralLedger.JournalVouchers: Al confirmar, por cada LegalBookId distinto del detalle se invoca SP_CreateAndValidateJournalVoucherMovement para crear el comprobante contable de tipo IdIntangibleAssetAmortizationVoucher con fecha=ProcessDate.; [RETURN_RESULT] @out: Devuelve CodeResult=0 con mensaje de guardado/confirmación; CodeResult=999 con detalle de placas y diferencias cuando falla validación o creación del comprobante.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAmortizationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAmortizationDetail';
-- GO
