-- =============================================================
CREATE  PROCEDURE [FixedAsset].[SP_SaveDepreciationDetail]
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

	--Id del activo
	DECLARE @PhysicalAssetId AS INT

	--Cantidad de dias depreciados en el mes del Activo
	DECLARE @DaysDepreciateMonthPhysicalAsset AS INT

	--Responsable Actual del Activo
	DECLARE @ResponsibleId AS INT

	--Ubicacion Actual del Activo
	DECLARE @LocationId AS INT

	--Especifica la Clase de la Ubicacion Actual del Activo
	DECLARE @LocationClass AS TINYINT

	--Centro de Costo Actual del Activo
	DECLARE @CostCenterId AS INT

	--Fecha de Adquisicion del Activo
	DECLARE @AdquisitionDate AS date


	--Tabla donde especifico la cantidad de dias que vamos a depreciar
	DECLARE @TableMovement table(PhysicalAssetId INT, ResponsibleId INT, LocationId INT, CostCenterId INT, DepreciatedDays INT)

	-- Id del tercero de la entidad
	DECLARE @ThirdPartyId INT

	-- Variable para almacenar los errores encontrados
	DECLARE @errors VARCHAR(MAX)

	-- depresia a 30 dias
	Declare @Depreciation30Days BIT 


	--Tabla temporal almacenamiento ingreso activos
	CREATE TABLE #AssetEntrySchedulePayment (
		Id INT,
		LegalBookId INT,
		PhysicalAssetId INT,
		DepreciationType INT,
		DepreciatedValue DECIMAL(18,6),
		ResidualValue DECIMAL(18,6),
		PercentageRescue DECIMAL(18,6),
		DepreciatedDays INT,
		DaysPendingDepreciate INT,
		LifeTime INT,
		Valorization DECIMAL(18,6),
		Devaluation DECIMAL(18,6),
		AdjustedValue DECIMAL(18,6),
		TransactionValue DECIMAL(18,6),
		FinantialDiscountDepreciated DECIMAL(18,6),
		DepreciateByTimeUse BIT,
		UseTime INT,
		Status INT,
		HasOutput BIT,
		Depreciate BIT,
		FinancialDiscount DECIMAL(18,6)
);
	--------------------------------------------------  ASIGNACIONES --------------------------------------------------

	SET @ThirdPartyId = (SELECT IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId)

	SET @Depreciation30Days = (select Depreciation30Days FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId) 

	DECLARE @DateEndForMovements DATE =
    CASE WHEN @Depreciation30Days = 1
         THEN @DateDepreciateEnd  -- último día del mes (30 para cuando el parámetro deprecia a 30 días)
         ELSE DATEADD(DAY, -1, @DateDepreciateEnd) -- último día del mes cuando el parámetro no deprecia a 30 días
    END

	IF @ModeConfirm = 0 BEGIN
		--Se eliminan los detalles del costo
		DELETE FROM FixedAsset.FixedAssetDepreciationDetailCost 
		WHERE FixedAssetDepreciationDetailId in (SELECT Id FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id)
		--Se eliminan los detalles
		DELETE FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id
	END

	
	----------------------------------------------------- PROCESO -----------------------------------------------------
		
	--Se recorren los libros que tenga cada activo y que no tengan la propiedad de salida en false
	DECLARE InfoItem CURSOR FOR 
	SELECT pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
	FROM FixedAsset.FixedAssetItemCatalog faic
	JOIN FixedAsset.FixedAssetItem fai ON faic.Id = fai.ItemCatalogId
	JOIN FixedAsset.FixedAssetPhysicalAsset pa ON fai.Id = pa.ItemId
	JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb on padb.PhysicalAssetId = pa.Id
	JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
	JOIN FixedAsset.FixedAssetLocation l on l.Id = pa.LocationId
	JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
	WHERE pa.HasOutput = 0 AND pa.Depreciate = 1 AND pa.Status = 1 AND faic.Classification = 1
		AND pa.AdquisitionDate <= @DateDepreciateEnd
	GROUP BY pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
	HAVING SUM(padb.DaysPendingDepreciate) > 0 AND SUM(padb.ResidualValue) > 0



	OPEN InfoItem
	FETCH NEXT FROM InfoItem 
	INTO @PhysicalAssetId, @ResponsibleId, @LocationId, @LocationClass, @CostCenterId, @AdquisitionDate
	WHILE @@fetch_status = 0
	BEGIN

		SET @DaysDepreciateMonthPhysicalAsset = 0

		IF (SELECT count(*) 
			FROM FixedAsset.FixedAssetKardexItem 
			WHERE PhysicalAssetId = @PhysicalAssetId 
				AND CAST(DocumentDate AS date) between @DateDepreciateInitial 
				AND @DateEndForMovements) = 0 
		BEGIN -- Si no tiene movimientos en el mes
			IF @LocationClass = 1 or @LocationClass = 2 BEGIN ---Si la clase de la unidad Actual es Admnistrativa o Productiva
				IF @DateDepreciateInitial > @AdquisitionDate BEGIN
					SET @DaysDepreciateMonthPhysicalAsset =  @DaysDepreciateMonth
				END
				ELSE BEGIN
					SET @DaysDepreciateMonthPhysicalAsset = DATEDIFF(DAY, @AdquisitionDate, @DateDepreciateEnd)
				END
				INSERT INTO @TableMovement(PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
				VALUES(@PhysicalAssetId, @ResponsibleId, @LocationId, @CostCenterId, @DaysDepreciateMonthPhysicalAsset)
			END
		END
		ELSE BEGIN --Si tiene movimientos en el mes
			-- Recorro los movimientos para determinar los dias que deprecian en cada ubicacion
			DECLARE @MovementDocumentDate date,@MovementPreviousResponsibleId INT, @MovementResponsibleId INT,@MovementPreviousLocationId INT,@MovementLocationId INT,@MovementPreviousCostCenterId INT,@MovementCostCenterId INT, @MovementPreviousClass TINYINT, @MovementClass TINYINT, @IndexCursor INT = 0,@DateIndexCursor date = @DateDepreciateInitial
			DECLARE @DaysDepreciateMovementPhysicalAsset INT = 0
			DECLARE @QuantityMovement INT
		
			--Si el Activo ingresó despues de la fecha inicial del mes
			IF @DateDepreciateInitial < @AdquisitionDate BEGIN
				SET @DateIndexCursor = @AdquisitionDate
			END
				
			SET @QuantityMovement = (SELECT count(*)
			FROM FixedAsset.FixedAssetKardexItem k
			INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = k.LocationId
			INNER JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
			LEFT JOIN FixedAsset.FixedAssetLocation lp on lp.Id = PreviousLocationId
			LEFT JOIN Payroll.FunctionalUnit fup on fup.Id = lp.FunctionalUnitId
			WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND @DateEndForMovements)

			DECLARE movement_cursor CURSOR FOR 
			SELECT DocumentDate,ISNULL(PreviousResponsibleId,ResponsibleId),ResponsibleId,ISNULL(PreviousLocationId,LocationId),LocationId, ISNULL(fup.CostCenterId, fu.CostCenterId),fu.CostCenterId,ISNULL(lp.Class, l.Class), l.Class
			FROM FixedAsset.FixedAssetKardexItem k
			INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = k.LocationId
			INNER JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
			LEFT JOIN FixedAsset.FixedAssetLocation lp on lp.Id = PreviousLocationId
			LEFT JOIN Payroll.FunctionalUnit fup on fup.Id = lp.FunctionalUnitId
			WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND @DateEndForMovements
			ORDER BY DocumentDate ASC

			OPEN movement_cursor

			FETCH NEXT FROM movement_cursor 
			INTO @MovementDocumentDate,@MovementPreviousResponsibleId, @MovementResponsibleId,@MovementPreviousLocationId,@MovementLocationId, @MovementPreviousCostCenterId, @MovementCostCenterId, @MovementPreviousClass, @MovementClass

			WHILE @@FETCH_STATUS = 0
			BEGIN
				SET @IndexCursor += 1
				SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @MovementDocumentDate)
				IF (@MovementPreviousClass = 1 or @MovementPreviousClass = 2) AND @DaysDepreciateMovementPhysicalAsset > 0 BEGIN -- Si la clase de la unidad anterior es administrativa o productiva y si estuvo mas de 0 dias
					SET @DaysDepreciateMonthPhysicalAsset += @DaysDepreciateMovementPhysicalAsset
					INSERT INTO @TableMovement(PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
					VALUES(@PhysicalAssetId, @MovementPreviousResponsibleId, @MovementPreviousLocationId, @MovementPreviousCostCenterId, @DaysDepreciateMovementPhysicalAsset)
					
				END
				SET @DateIndexCursor = @MovementDocumentDate

				---Si es el ultimo recorrido agrego entonces la unidad actual en la que se realizo el ultimo movimiento hasta fin de mes
				IF @IndexCursor = @QuantityMovement AND (@MovementClass = 1 or @MovementClass = 2) BEGIN

				IF @Depreciation30Days = 1 BEGIN 
					IF DAY(@DateDepreciateEnd)= 28 BEGIN -- SI ES FEBRERO NORMAL
						 SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd) + 3
					

					END
					ELSE IF DAY(@DateDepreciateEnd)= 29 BEGIN --FEBRERO BISIESTO
					 SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd) + 2

					END 
					ELSE IF DAY(@DateDepreciateEnd)= 30 BEGIN --SI DEPRECIA A 30 DIAS
						SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd) + 1

					END 
					ELSE IF DAY(@AdquisitionDate)= 31 BEGIN --NO DEPRECIA EL DIA 31
						SET @DaysDepreciateMovementPhysicalAsset = 0

					END 
					ELSE BEGIN 
					--SI EL MES TIENE 31 DIAS
					 SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd) 

					END	
				END
				ELSE BEGIN-- SI EL PARAMETRO DE DEPRECIA A 30 DIAS ES  0
				 SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd)
				END 
					
					SET @DaysDepreciateMonthPhysicalAsset += @DaysDepreciateMovementPhysicalAsset
					INSERT INTO @TableMovement(PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
					VALUES(@PhysicalAssetId, @MovementResponsibleId, @MovementLocationId, @MovementCostCenterId, @DaysDepreciateMovementPhysicalAsset)
					
				END
				FETCH NEXT FROM movement_cursor 
				INTO @MovementDocumentDate,@MovementPreviousResponsibleId, @MovementResponsibleId,@MovementPreviousLocationId,@MovementLocationId, @MovementPreviousCostCenterId, @MovementCostCenterId, @MovementPreviousClass, @MovementClass
			END
			Close movement_cursor
			Deallocate movement_cursor
			
		END
		--Se pasa a la siguiente posicion del cursor
		FETCH NEXT FROM InfoItem 
		INTO @PhysicalAssetId, @ResponsibleId, @LocationId, @LocationClass, @CostCenterId, @AdquisitionDate
	END
	Close InfoItem
	Deallocate InfoItem
	
	--------------------------------------- Se empieza a insertar las tablas de depreciación -----------------------------------------------
		
	--Se declaran las tablas temporales para poder comparar si hay que recalcular
	DECLARE @FixedAssetDepreciationDetailTemp table(Id INT primary key identity(1,1),FixedAssetDepreciationId INT, LegalBookId INT, ActiveClass TINYINT, 
	FixedAssetPhysicalAssetId INT NULL, FixedAssetPhysicalAssetDetailBookId INT NULL, FixedAssetPhysicalAssetPartsId INT NULL, 
	FixedAssetPhysicalAssetPartsDetailBookId INT NULL, DepreciationValue numeric(18,2), LifeTime INT, RemainingLifeTime INT, 
	DepreciatedDays INT, ValorizationValue numeric(18,2), DevaluationValue numeric(18,2), AdjustedValue numeric(18,2), 
	TransactionValue numeric(18,2), AccumulatedDepreciation numeric(18,2), ResidualValue numeric(18,2), 
	FinantialDiscount NUMERIC(18,2), FinantialDiscountDepreciation NUMERIC(18,2), FinantialDiscountAdjusment NUMERIC(18,2))

	DECLARE @FixedAssetDepreciationDetailIds TABLE (Id INT)
		
	DECLARE @FixedAssetDepreciationDetailCostTemp table(Id INT primary key identity(1,1), FixedAssetDepreciationDetailId INT, MainAccountId INT, ResponsibleId INT, LocationId INT, 
	CostCenterId INT, DepreciatedDays INT, DepreciationValue numeric(20,4))

	---Insertar datos en la tabla temporal
	INSERT INTO #AssetEntrySchedulePayment
	SELECT DISTINCT
		padb.Id,
		padb.LegalBookId,
		pa.Id AS PhysicalAssetId,
		padb.DepreciationType,
		padb.DepreciatedValue,
		padb.ResidualValue,
		padb.PercentageRescue,
		padb.DepreciatedDays,
		padb.DaysPendingDepreciate,
		padb.LifeTime,
		padb.Valorization,
		padb.Devaluation,
		padb.AdjustedValue,
		padb.TransactionValue,
		padb.FinantialDiscountDepreciated,
		fai.DepreciateByTimeUse,
		fal.UseTime,
		pa.Status,
		pa.HasOutput,
		pa.Depreciate,
		CASE 
			WHEN MONTH(sp.ScheduledDate) = (SELECT ClosingMonth FROM FixedAsset.FixedAssetDepreciation WHERE Id = @Id)
			  AND YEAR(sp.ScheduledDate) = (SELECT ClosingYear FROM FixedAsset.FixedAssetDepreciation WHERE Id = @Id)
			THEN pa.FinancialDiscount
			ELSE 0
		END AS FinancialDiscount
	FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb
	INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = padb.PhysicalAssetId
	INNER JOIN FixedAsset.FixedAssetItem fai ON fai.Id = pa.ItemId
	INNER JOIN FixedAsset.FixedAssetLocation fal ON fal.Id = pa.LocationId
	LEFT JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON pa.Plate = faeid.Plate
	LEFT JOIN FixedAsset.FixedAssetEntryItem faei ON faei.Id = faeid.FixedAssetEntryItemId
	LEFT JOIN FixedAsset.FixedAssetEntry fae ON fae.Id = faei.FixedAssetEntryId
	LEFT JOIN Payments.AccountPayable ap ON ap.Id = fae.AccountPayableId
	LEFT JOIN Treasury.SchedulePaymentDetail spd ON spd.AccountPayableId = ap.Id
	LEFT JOIN Treasury.SchedulePayment sp ON sp.Id = spd.SchedulePaymentId
	WHERE (fae.Status IS NULL OR fae.Status = 2) --Trae los saldos iniciales y los que esten confirmados en ingreso


	--Se inserta en las tablas temporales
	INSERT INTO @FixedAssetDepreciationDetailTemp
		([FixedAssetDepreciationId], [LegalBookId], [ActiveClass], [FixedAssetPhysicalAssetId], [FixedAssetPhysicalAssetDetailBookId],
		[FixedAssetPhysicalAssetPartsId], [FixedAssetPhysicalAssetPartsDetailBookId], [DepreciationValue], [LifeTime], [RemainingLifeTime], 
		[DepreciatedDays], [ValorizationValue], [DevaluationValue], [AdjustedValue], [TransactionValue], [AccumulatedDepreciation], [ResidualValue],
		[FinantialDiscount], [FinantialDiscountDepreciation], [FinantialDiscountAdjusment])										
	SELECT 
		@Id,
		a.LegalBookId,
		1,
		a.PhysicalAssetId,
		a.Id,
		NULL, 
		NULL,
		FixedAsset.fnCalculateDepreciateValue(
			a.DepreciationType,
			a.DepreciatedValue,
			a.ResidualValue - IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
			a.PercentageRescue,
			a.DepreciateByTimeUse,
			a.UseTime,
			a.DepreciatedDays,
			a.DaysPendingDepreciate,
			tm.DepreciateDays
		),
		a.LifeTime,
		0,
		IIF(tm.DepreciateDays > a.DaysPendingDepreciate, a.DaysPendingDepreciate, tm.DepreciateDays),
		a.Valorization,
		a.Devaluation,
		a.AdjustedValue,
		a.TransactionValue,
		0,
		a.ResidualValue,
		a.FinancialDiscount,
		FixedAsset.fnCalculateDepreciateValue(
			a.DepreciationType,
			0,
			IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
			a.PercentageRescue,
			a.DepreciateByTimeUse,
			a.UseTime,
			a.DepreciatedDays,
			a.DaysPendingDepreciate,
			tm.DepreciateDays
		),
		0
	FROM #AssetEntrySchedulePayment a
	INNER JOIN (
		SELECT PhysicalAssetId, SUM(DepreciatedDays) AS DepreciateDays
		FROM @TableMovement
		GROUP BY PhysicalAssetId
	) tm ON tm.PhysicalAssetId = a.PhysicalAssetId
	INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = a.LegalBookId AND lb.Status = 1
	WHERE 
		a.Status = 1 AND 
		a.HasOutput = 0 AND 
		a.Depreciate = 1 AND 
		a.DaysPendingDepreciate > 0 AND 
		a.ResidualValue > 0 AND 
		tm.DepreciateDays > 0;
			   
		
	--------------------------- Se calcula el ajuste por descuento financiero ---------------------------

	UPDATE fadd
		SET fadd.FinantialDiscountAdjusment = [FixedAsset].[fnCalculateFinantialDiscountAdjusment](
				fapadb.FinantialDiscountDepreciated,
				fadd.FinantialDiscountDepreciation,
				fadd.FinantialDiscount,
				fapadb.DepreciatedDays,
				fapadb.DaysPendingDepreciate
			)		
	FROM @FixedAssetDepreciationDetailTemp fadd
	JOIN FixedAsset.FixedAssetDepreciation fad ON fadd.FixedAssetDepreciationId = fad.Id
	JOIN FixedAsset.FixedAssetDepreciation fadp ON fadp.Status = 2 
		AND (
			(fad.ClosingMonth = 1 AND fad.ClosingYear = fadp.ClosingYear + 1 AND fadp.ClosingMonth = 12)
			OR 
			(fad.ClosingYear = fadp.ClosingYear AND fad.ClosingMonth = fadp.ClosingMonth + 1)
		)
	JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fadd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
	LEFT JOIN FixedAsset.FixedAssetDepreciationDetail faddp ON fadp.Id = faddp.FixedAssetDepreciationId
		AND fadd.FixedAssetPhysicalAssetDetailBookId = faddp.FixedAssetPhysicalAssetDetailBookId
		AND fadd.FixedAssetPhysicalAssetId = faddp.FixedAssetPhysicalAssetId
		AND fadd.LegalBookId = faddp.LegalBookId
		AND faddp.FixedAssetPhysicalAssetPartsId IS NULL
	WHERE fad.Id = @Id
		AND fadd.FixedAssetPhysicalAssetPartsId IS NULL
		AND fadd.FinantialDiscount <> 0
		AND ISNULL(faddp.FinantialDiscount, 0) = 0
		AND fadd.FinantialDiscount > fapadb.FinantialDiscountDepreciated

	-----------------------------------------------------------------------------------------------------------

	---- tabla temporal para activos con menos dias por depreciar en el mes y con mas de 1 movimienro
		DECLARE @tableTempDetailCost AS TABLE(id int identity (1,1), FixedAssetDepreciationDetailId int, PhysicalAssetId int, 
							DaysPendingDepreciated int, DaysPending int, CurrentDays int,[ResponsibleId] int, [LocationId] int, [CostCenterId] int)


		INSERT INTO @tableTempDetailCost(FixedAssetDepreciationDetailId,PhysicalAssetId,DaysPendingDepreciated,DaysPending)
				SELECT fdd.id, fdd.FixedAssetPhysicalAssetId, padb.DaysPendingDepreciate,padb.DaysPendingDepreciate FROM (select count(*) as QuantityMovement,PhysicalAssetId, SUM(DepreciatedDays) DepreciateDays 
					FROM @TableMovement
					GROUP BY PhysicalAssetId
					) tm
			INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON tm.PhysicalAssetId = padb.PhysicalAssetId 
			inner join @FixedAssetDepreciationDetailTemp fdd on fdd.FixedAssetPhysicalAssetId = padb.PhysicalAssetId  and fdd.LegalBookId = padb.LegalBookId
			where tm.QuantityMovement > 1 and tm.DepreciateDays > padb.DaysPendingDepreciate and fdd.FixedAssetDepreciationId = @Id 

		IF EXISTS( SELECT TOP 1 * FROM @tableTempDetailCost) 

			BEGIN
				
				DECLARE @IdLegalBook int
				DECLARE MovementCursorBook CURSOR FOR
					SELECT ID  FROM GeneralLedger.LegalBook
					Open MovementCursorBook;
					FETCH NEXT FROM MovementCursorBook INTO @IdLegalBook;
					WHILE @@FETCH_STATUS = 0
					BEGIN

					DECLARE @PhysicalAssetsId INT, @DepreciatedDays INT, @DaysPending INT, @DaysPendingDepreciated INT, @FixedAssetDepreciationDetailId INT;
					DECLARE MovementCursor CURSOR FOR
					SELECT PhysicalAssetId, DepreciatedDays, ResponsibleId, LocationId, CostCenterId
					FROM @TableMovement order by DepreciatedDays;

					OPEN MovementCursor;

					FETCH NEXT FROM MovementCursor INTO @PhysicalAssetsId, @DepreciatedDays, @MovementResponsibleId, @MovementLocationId, @MovementCostCenterId;

					WHILE @@FETCH_STATUS = 0
					BEGIN
						
						DECLARE @IdTableTem int 

						SELECT TOP 1 @IdTableTem = FDC.id, 
									 @FixedAssetDepreciationDetailId = FDC.FixedAssetDepreciationDetailId,
									 @DaysPendingDepreciated = FDC.DaysPending
						FROM @tableTempDetailCost FDC
						INNER JOIN @FixedAssetDepreciationDetailTemp FDD ON FDC.FixedAssetDepreciationDetailId = FDD.Id AND FDD.LegalBookId  =@IdLegalBook
						WHERE PhysicalAssetId = @PhysicalAssetsId
						ORDER BY FDC.id DESC;

						IF @IdTableTem > 0
						BEGIN

							IF @DepreciatedDays < @DaysPendingDepreciated
							BEGIN
								UPDATE @tableTempDetailCost 
								SET CurrentDays = @DepreciatedDays, ResponsibleId = @MovementResponsibleId,LocationId = @MovementLocationId,CostCenterId = @MovementCostCenterId
								WHERE id = @IdTableTem;
							END
							ELSE
							BEGIN
								UPDATE @tableTempDetailCost 
								SET CurrentDays = @DaysPendingDepreciated,ResponsibleId = @MovementResponsibleId,LocationId = @MovementLocationId,CostCenterId = @MovementCostCenterId
								WHERE id = @IdTableTem;
							END

							SET @DaysPending = @DaysPendingDepreciated - @DepreciatedDays;

							IF @DaysPending > 0
							BEGIN
								INSERT INTO @tableTempDetailCost (FixedAssetDepreciationDetailId, PhysicalAssetId, DaysPending)
								VALUES (@FixedAssetDepreciationDetailId, @PhysicalAssetsId, @DaysPending);

							END
						END
						FETCH NEXT FROM MovementCursor INTO @PhysicalAssetsId, @DepreciatedDays, @MovementResponsibleId, @MovementLocationId, @MovementCostCenterId;
					END;

					CLOSE MovementCursor;
					DEALLOCATE MovementCursor;

					FETCH NEXT FROM MovementCursorBook INTO @IdLegalBook;
					END
					CLOSE MovementCursorBook;
					DEALLOCATE MovementCursorBook;
			END

		delete from @tableTempDetailCost  where CurrentDays is null and DaysPendingDepreciated is null

	
	INSERT INTO @FixedAssetDepreciationDetailCostTemp
		(
			[FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], 
			[DepreciatedDays], [DepreciationValue]
		)
	SELECT dd.Id, pa.MainAccountId, TTDC.ResponsibleId,
	TTDC.LocationId,
	TTDC.CostCenterId, 
	TTDC.CurrentDays, 
	FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue - dd.FinantialDiscountAdjusment, dd.DepreciatedDays, TTDC.CurrentDays)
	FROM @FixedAssetDepreciationDetailTemp dd
	INNER JOIN @tableTempDetailCost TTDC ON  TTDC.FixedAssetDepreciationDetailId = dd.Id 
	INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
	WHERE dd.FixedAssetDepreciationId = @Id

	INSERT INTO @FixedAssetDepreciationDetailCostTemp
		(
			[FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], 
			[DepreciatedDays], [DepreciationValue]
		)
		SELECT dd.Id, pa.MainAccountId, tm.ResponsibleId,
	tm.LocationId,
	tm.CostCenterId, 
	IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays), 
	FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue - dd.FinantialDiscountAdjusment, dd.DepreciatedDays, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays))
	FROM @FixedAssetDepreciationDetailTemp dd
	INNER JOIN @TableMovement tm on tm.PhysicalAssetId = dd.FixedAssetPhysicalAssetId
	INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
	WHERE dd.FixedAssetDepreciationId = @Id AND dd.ID NOT IN (SELECT T.FixedAssetDepreciationDetailId FROM @tableTempDetailCost T)
	
	--Ajustamos diferencias por redondeos
	UPDATE faddc
		SET faddc.DepreciationValue = faddc.DepreciationValue + (fadd.DepreciationValue - fadd.FinantialDiscountAdjusment - faddcd.DepreciationValue)
	FROM @FixedAssetDepreciationDetailTemp fadd
	JOIN 
	(
		SELECT 
			faddc.FixedAssetDepreciationDetailId, 
			SUM(faddc.DepreciationValue) DepreciationValue,
			MAX(faddc.Id) FixedAssetDepreciationDetailCostId
		FROM @FixedAssetDepreciationDetailCostTemp faddc
		GROUP BY faddc.FixedAssetDepreciationDetailId
	) faddcd ON fadd.Id = faddcd.FixedAssetDepreciationDetailId
		AND fadd.DepreciationValue <> faddcd.DepreciationValue
	JOIN @FixedAssetDepreciationDetailCostTemp faddc ON faddcd.FixedAssetDepreciationDetailCostId = faddc.Id
	WHERE fadd.FixedAssetDepreciationId = @Id

	DELETE FROM @FixedAssetDepreciationDetailCostTemp WHERE DepreciationValue <= 0

	--Si se esta confirmando
	IF @ModeConfirm = 1
	BEGIN
		--Se valida que los datos nuevos que se registraron en la tabla temporal sean los mismos que estan en la BD de lo contrario se debe recalcular

		IF ISNULL((SELECT SUM(DepreciationValue) FROM @FixedAssetDepreciationDetailTemp), 0) <> ISNULL((SELECT SUM(DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id), 0) 
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'Los valores del detalle de la depreciación han cambiado y debe recalcular'
			RETURN
		END
		ELSE IF (SELECT COUNT(*)
			FROM @FixedAssetDepreciationDetailCostTemp ddct
			INNER JOIN @FixedAssetDepreciationDetailTemp ddt on ddt.Id = ddct.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on dd.FixedAssetPhysicalAssetId = ddt.FixedAssetPhysicalAssetId AND dd.LegalBookId = ddt.LegalBookId AND dd.FixedAssetDepreciationId = @Id
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on ddc.FixedAssetDepreciationDetailId = dd.Id AND ddc.DepreciatedDays = ddt.DepreciatedDays
			WHERE ddct.DepreciationValue <> ddc.DepreciationValue) > 0 
		BEGIN
			DECLARE @activosmal VARCHAR(MAX)
			SET @activosmal = (SELECT STRING_AGG(CONCAT('Placa: ',pas.Plate, ' Valores: ', CAST(ddct.DepreciationValue AS VARCHAR(30)), ' - ', CAST(ddc.DepreciationValue AS VARCHAR(30))), ',')
			FROM @FixedAssetDepreciationDetailCostTemp ddct
			INNER JOIN @FixedAssetDepreciationDetailTemp ddt on ddt.Id = ddct.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on dd.FixedAssetPhysicalAssetId = ddt.FixedAssetPhysicalAssetId AND dd.LegalBookId = ddt.LegalBookId AND dd.FixedAssetDepreciationId = @Id
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pas on pas.Id = ddt.FixedAssetPhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on ddc.FixedAssetDepreciationDetailId = dd.Id AND ddc.DepreciatedDays = ddt.DepreciatedDays
			WHERE ddct.DepreciationValue <> ddc.DepreciationValue)
				
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('Los valores del costo de la depreciación han cambiado y debe recalcular', ' ', @activosmal)
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM FixedAsset.FixedAssetDepreciationDetail fadd WITH (NOLOCK)
			JOIN
			(
				SELECT 
					SUM(DepreciationValue) DepreciationValue,
					FixedAssetDepreciationDetailId
				FROM FixedAsset.FixedAssetDepreciationDetailCost WITH (NOLOCK) 
				GROUP BY FixedAssetDepreciationDetailId
			) faddc ON fadd.Id = faddc.FixedAssetDepreciationDetailId
			WHERE fadd.FixedAssetDepreciationId = @Id
				AND fadd.DepreciationValue - fadd.FinantialDiscountAdjusment <> faddc.DepreciationValue
		)
		BEGIN 
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El valor a depreciar del costo de la depreciación es diferente al valor detallado'
			RETURN
		END
	END
	ELSE 
	BEGIN

		INSERT INTO [FixedAsset].[FixedAssetDepreciationDetail]
			([FixedAssetDepreciationId], [LegalBookId], [ActiveClass], [FixedAssetPhysicalAssetId], [FixedAssetPhysicalAssetDetailBookId],
			[FixedAssetPhysicalAssetPartsId], [FixedAssetPhysicalAssetPartsDetailBookId], [DepreciationValue], [LifeTime], [RemainingLifeTime], 
			[DepreciatedDays], [ValorizationValue], [DevaluationValue], [AdjustedValue], [TransactionValue], [AccumulatedDepreciation], [ResidualValue],
			[FinantialDiscount], [FinantialDiscountDepreciation])									 
		SELECT 
				@Id,
				a.LegalBookId,
				1,
				a.PhysicalAssetId,
				a.Id,
				NULL, 
				NULL,
				FixedAsset.fnCalculateDepreciateValue(
					a.DepreciationType,
					a.DepreciatedValue,
					a.ResidualValue - IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
					a.PercentageRescue,
					a.DepreciateByTimeUse,
					a.UseTime,
					a.DepreciatedDays,
					a.DaysPendingDepreciate,
					tm.DepreciateDays
				),
				a.LifeTime,
				0,
				IIF(tm.DepreciateDays > a.DaysPendingDepreciate, a.DaysPendingDepreciate, tm.DepreciateDays),
				a.Valorization,
				a.Devaluation,
				a.AdjustedValue,
				a.TransactionValue,
				0,
				a.ResidualValue,
				a.FinancialDiscount,
				FixedAsset.fnCalculateDepreciateValue(
					a.DepreciationType,
					0,
					IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
					a.PercentageRescue,
					a.DepreciateByTimeUse,
					a.UseTime,
					a.DepreciatedDays,
					a.DaysPendingDepreciate,
					tm.DepreciateDays
				)
			FROM #AssetEntrySchedulePayment a
			INNER JOIN (
				SELECT PhysicalAssetId, SUM(DepreciatedDays) AS DepreciateDays
				FROM @TableMovement
				GROUP BY PhysicalAssetId
			) tm ON tm.PhysicalAssetId = a.PhysicalAssetId
			INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = a.LegalBookId AND lb.Status = 1
			WHERE 
				a.Status = 1 AND 
				a.HasOutput = 0 AND 
				a.Depreciate = 1 AND 
				a.DaysPendingDepreciate > 0 AND 
				a.ResidualValue > 0 AND 
				tm.DepreciateDays > 0;

		--------------------------- Se calcula el ajuste por descuento financiero ---------------------------

		DELETE FROM @FixedAssetDepreciationDetailIds

		INSERT INTO @FixedAssetDepreciationDetailIds
			SELECT fadd.Id			
			FROM FixedAsset.FixedAssetDepreciationDetail fadd
			JOIN FixedAsset.FixedAssetDepreciation fad ON fadd.FixedAssetDepreciationId = fad.Id
			JOIN FixedAsset.FixedAssetDepreciation fadp ON fadp.Status = 2 
				AND (
					(fad.ClosingMonth = 1 AND fad.ClosingYear = fadp.ClosingYear + 1 AND fadp.ClosingMonth = 12)
					OR 
					(fad.ClosingYear = fadp.ClosingYear AND fad.ClosingMonth = fadp.ClosingMonth + 1)
				)
			LEFT JOIN FixedAsset.FixedAssetDepreciationDetail faddp ON fadp.Id = faddp.FixedAssetDepreciationId
				AND fadd.FixedAssetPhysicalAssetDetailBookId = faddp.FixedAssetPhysicalAssetDetailBookId
				AND fadd.FixedAssetPhysicalAssetId = faddp.FixedAssetPhysicalAssetId
				AND fadd.LegalBookId = faddp.LegalBookId
				AND faddp.FixedAssetPhysicalAssetPartsId IS NULL
			WHERE fad.Id = @Id
				AND fadd.FixedAssetPhysicalAssetPartsId IS NULL
				AND fadd.FinantialDiscount <> 0
				AND ISNULL(faddp.FinantialDiscount, 0) = 0

		UPDATE fadd
			SET fadd.FinantialDiscountAdjusment = [FixedAsset].[fnCalculateFinantialDiscountAdjusment](
					fapadb.FinantialDiscountDepreciated,
					fadd.FinantialDiscountDepreciation,
					fadd.FinantialDiscount,
					fapadb.DepreciatedDays,
					fapadb.DaysPendingDepreciate
				)
		FROM @FixedAssetDepreciationDetailIds Ids
		JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON Ids.Id = fadd.Id
		JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fadd.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
		WHERE fadd.FinantialDiscount > fapadb.FinantialDiscountDepreciated

		-----------------------------------------------------------------------------------------------------------
		delete from @tableTempDetailCost

		INSERT INTO @tableTempDetailCost(fdd.FixedAssetDepreciationDetailId,PhysicalAssetId,DaysPendingDepreciated,DaysPending)
		SELECT fdd.id, fdd.FixedAssetPhysicalAssetId, padb.DaysPendingDepreciate,padb.DaysPendingDepreciate FROM (select count(*) as QuantityMovement,PhysicalAssetId, SUM(DepreciatedDays) DepreciateDays 
			FROM @TableMovement
			GROUP BY PhysicalAssetId
			) tm
		INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON tm.PhysicalAssetId = padb.PhysicalAssetId 
		inner join FixedAsset.FixedAssetDepreciationDetail fdd on fdd.FixedAssetPhysicalAssetId = padb.PhysicalAssetId and fdd.FixedAssetDepreciationId = @Id and padb.LegalBookId = fdd.LegalBookId
		where tm.QuantityMovement > 1 and (tm.DepreciateDays > padb.DaysPendingDepreciate)

		IF EXISTS( SELECT TOP 1 * FROM @tableTempDetailCost) 

			BEGIN
				
				DECLARE MovementCursorBook CURSOR FOR
					SELECT ID  FROM GeneralLedger.LegalBook
					Open MovementCursorBook;
					FETCH NEXT FROM MovementCursorBook INTO @IdLegalBook;
					WHILE @@FETCH_STATUS = 0
					BEGIN

					DECLARE MovementCursor CURSOR FOR
					SELECT PhysicalAssetId, DepreciatedDays, ResponsibleId, LocationId, CostCenterId
					FROM @TableMovement order by DepreciatedDays;

					OPEN MovementCursor;

					FETCH NEXT FROM MovementCursor INTO @PhysicalAssetsId, @DepreciatedDays, @MovementResponsibleId, @MovementLocationId, @MovementCostCenterId;

					WHILE @@FETCH_STATUS = 0
					BEGIN
						 

						SELECT TOP 1 @IdTableTem = FDC.id, 
									 @FixedAssetDepreciationDetailId = FDC.FixedAssetDepreciationDetailId,
									 @DaysPendingDepreciated = FDC.DaysPending
						FROM @tableTempDetailCost FDC
						INNER JOIN [FixedAsset].[FixedAssetDepreciationDetail] FDD ON FDC.FixedAssetDepreciationDetailId = FDD.Id AND FDD.LegalBookId  =@IdLegalBook and  fdd.FixedAssetDepreciationId = @Id
						WHERE PhysicalAssetId = @PhysicalAssetsId 
						ORDER BY FDC.id DESC;

						IF @IdTableTem > 0
						BEGIN

							IF @DepreciatedDays < @DaysPendingDepreciated
							BEGIN
								UPDATE @tableTempDetailCost 
								SET CurrentDays = @DepreciatedDays, ResponsibleId = @MovementResponsibleId,LocationId = @MovementLocationId,CostCenterId = @MovementCostCenterId
								WHERE id = @IdTableTem;
							END
							ELSE
							BEGIN
								UPDATE @tableTempDetailCost 
								SET CurrentDays = @DaysPendingDepreciated,ResponsibleId = @MovementResponsibleId,LocationId = @MovementLocationId,CostCenterId = @MovementCostCenterId
								WHERE id = @IdTableTem;
							END

							SET @DaysPending = @DaysPendingDepreciated - @DepreciatedDays;

							IF @DaysPending > 0
							BEGIN
								INSERT INTO @tableTempDetailCost (FixedAssetDepreciationDetailId, PhysicalAssetId, DaysPending)
								VALUES (@FixedAssetDepreciationDetailId, @PhysicalAssetsId, @DaysPending);

							END
						END
						FETCH NEXT FROM MovementCursor INTO @PhysicalAssetsId, @DepreciatedDays, @MovementResponsibleId, @MovementLocationId, @MovementCostCenterId;
					END;

					CLOSE MovementCursor;
					DEALLOCATE MovementCursor;

					FETCH NEXT FROM MovementCursorBook INTO @IdLegalBook;
					END
					CLOSE MovementCursorBook;
					DEALLOCATE MovementCursorBook;
			END

		delete from @tableTempDetailCost  where CurrentDays is null and DaysPendingDepreciated is null

		INSERT INTO [FixedAsset].[FixedAssetDepreciationDetailCost]
		(
			[FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], 
			[DepreciatedDays], [DepreciationValue]
		)
		SELECT dd.Id, pa.MainAccountId, TTDC.ResponsibleId,
			TTDC.LocationId,
			TTDC.CostCenterId, 
			TTDC.CurrentDays, 
			FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue - dd.FinantialDiscountAdjusment, dd.DepreciatedDays, TTDC.CurrentDays)
			FROM [FixedAsset].[FixedAssetDepreciationDetail] dd
			INNER JOIN @tableTempDetailCost TTDC ON  TTDC.FixedAssetDepreciationDetailId = dd.Id  and dd.FixedAssetPhysicalAssetId = TTDC.PhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
			WHERE dd.FixedAssetDepreciationId = @Id


		INSERT INTO [FixedAsset].[FixedAssetDepreciationDetailCost]
		(
			[FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], 
			[DepreciatedDays], [DepreciationValue]
		)
		SELECT dd.Id, pa.MainAccountId, tm.ResponsibleId,
		tm.LocationId,
		tm.CostCenterId, 
		IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays), 
		FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue - dd.FinantialDiscountAdjusment, dd.DepreciatedDays, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays))
		FROM [FixedAsset].[FixedAssetDepreciationDetail] dd
		INNER JOIN @TableMovement tm on tm.PhysicalAssetId = dd.FixedAssetPhysicalAssetId
		INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
		WHERE dd.FixedAssetDepreciationId = @Id AND dd.ID NOT IN (SELECT T.FixedAssetDepreciationDetailId FROM @tableTempDetailCost T)


		--Ajustamos diferencias por redondeos
		UPDATE faddc
			SET faddc.DepreciationValue = faddc.DepreciationValue + (fadd.DepreciationValue - fadd.FinantialDiscountAdjusment - faddcd.DepreciationValue)
		FROM FixedAsset.FixedAssetDepreciationDetail fadd
		JOIN 
		(
			SELECT 
				faddc.FixedAssetDepreciationDetailId, 
				SUM(faddc.DepreciationValue) DepreciationValue,
				MAX(faddc.Id) FixedAssetDepreciationDetailCostId
			FROM Fixedasset.FixedAssetDepreciationDetailCost faddc
			GROUP BY faddc.FixedAssetDepreciationDetailId
		) faddcd ON fadd.Id = faddcd.FixedAssetDepreciationDetailId
			AND fadd.DepreciationValue <> faddcd.DepreciationValue
		JOIN FixedAsset.FixedAssetDepreciationDetailCost faddc ON faddcd.FixedAssetDepreciationDetailCostId = faddc.Id
		WHERE fadd.FixedAssetDepreciationId = @Id
			
		--Se elimina si no hay movimientos
		DELETE faddc
		FROM FixedAsset.FixedAssetDepreciationDetail fadd
		JOIN FixedAsset.FixedAssetDepreciationDetailCost faddc ON fadd.Id = faddc.FixedAssetDepreciationDetailId
		WHERE fadd.FixedAssetDepreciationId = @Id
			AND faddc.DepreciationValue <= 0
	END

	--Se empieza la confirmación
	IF @ModeConfirm = 1 --Si se está confirmando
	BEGIN
		--Valida que las unidades Funcionales tengan Estructura Contable
		IF (SELECT 
		COUNT(*)
		FROM FixedAsset.FixedAssetDepreciation d
		INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
		INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
		INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
		INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
		WHERE d.Id = @Id AND f.AccountingStructureId is NULL) > 0 
		BEGIN
			SELECT @errors = stuff((SELECT distinct N'; La unidad funcional ' + f.Code	+ ' no tiene estructura contable'
			FROM FixedAsset.FixedAssetDepreciation d
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
			INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
			WHERE d.Id = @Id AND f.AccountingStructureId is NULL
			for xml path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')
			
			SELECT	@CodeResult = 999, 
					@MessageResult = @errors
			RETURN
		END
			
		---- Validamos que todos los catalogos tengan parametrizada la estructura contable
		IF EXISTS
			(SELECT 1
			FROM FixedAsset.FixedAssetDepreciation d
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
			INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
			INNER JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
			INNER JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id 
			WHERE d.Id = @Id AND cd.AccountingStructureId IS NULL AND cd.CostCenterId IS NULL) 
		BEGIN
			SELECT @errors = STUFF((SELECT N'; El catálogo ' + c.Code + ' no tiene parametrizada la estructura contable o el centro de costos'
                        FROM FixedAsset.FixedAssetDepreciation d
                        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId
                        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
                        INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
                        INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
                        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
                        INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
                        INNER JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
                        INNER JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
                        LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id 
                        WHERE d.Id = @Id AND cd.AccountingStructureId IS NULL AND cd.CostCenterId IS NULL
                        GROUP BY c.Code
                        FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

    
			SELECT  @CodeResult = 999, 
					@MessageResult = @errors
			RETURN
		END

		----- valido igualmente los catalogos que no manejan depreciación por distribución ya que tiene que tener relacion con la estructura contable de la unidad
		IF EXISTS
			(SELECT 1
			FROM FixedAsset.FixedAssetDepreciation d
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
			INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
			INNER JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
			INNER JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = f.AccountingStructureId
			WHERE d.Id = @Id AND cd.AccountingStructureId IS NULL and c.HandlesDepreciationbyDistribution = 0) 
		BEGIN
			SELECT @errors = STUFF((SELECT N'; El catálogo ' + c.Code + ' no tiene parametrizada la estructura contable'
                        FROM FixedAsset.FixedAssetDepreciation d
                        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId
                        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
                        INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
                        INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
                        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
                        INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId
                        INNER JOIN Payroll.FunctionalUnit f ON f.Id = l.FunctionalUnitId
                        INNER JOIN Payroll.AccountingStructure ast ON ast.Id = f.AccountingStructureId
                        LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = f.AccountingStructureId
                        WHERE d.Id = @Id AND cd.AccountingStructureId IS NULL AND cd.CostCenterId IS NULL and c.HandlesDepreciationbyDistribution = 0
                        GROUP BY c.Code
                        FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

    
			SELECT  @CodeResult = 999, 
					@MessageResult = @errors
			RETURN
		END

		IF EXISTS
			(SELECT 1 FROM FixedAsset.FixedAssetDepreciation d
						INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId 
						INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
						INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
						INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
						INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id
						where d.Id = @Id AND c.HandlesDepreciationbyDistribution = 1 AND cd.DistributionPercentage is NULL
						)
						BEGIN
			SELECT @errors = STUFF((SELECT N'; El catálogo ' + c.Code + ' tiene distribuciones con porcentajes vacíos'
                        FROM FixedAsset.FixedAssetDepreciation d
						INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId 
						INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
						INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
						INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
						INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id
						where d.Id = @Id AND c.HandlesDepreciationbyDistribution = 1 AND cd.DistributionPercentage is NULL
                        GROUP BY c.Code
                        FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')
			SELECT  @CodeResult = 999, 
					@MessageResult = @errors
			RETURN
		END
					
		--Cabecera del comprobante contable
		DECLARE @TableJournalVoucher table(IdJournalVoucher INT NOT NULL, VoucherDate datetime NOT NULL, Imported BIT NOT NULL, 
		[Status] TINYINT NOT NULL, Detail VARCHAR(500) NULL, EntityCode VARCHAR(20) NULL, EntityId INT NULL, EntityName VARCHAR(250) NULL, 
		IsClosedYear BIT NOT NULL, LegalBookId INT NOT NULL)

		--Detalles del comprobante contable
		DECLARE @TableJournalVoucherDetail table(IdMainAccount INT NOT NULL, IdThirdParty INT NULL, IdCostCenter INT NULL,
		DebitValue decimal(20, 4) NOT NULL,CreditValue decimal(20, 4) NOT NULL,Detail VARCHAR(MAX) NULL,IdRetention INT NULL,
		RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL, LegalBookId INT NOT NULL)

		--Id del tipo de comprobante contable
		DECLARE @IdJournalVoucher INT

		--Fecha de proceso de parámetros con el que se asigna la fecha al comprobante
		DECLARE @ProcessDate date

		--Se obtiene el tipo de comprobante contable y la fecha de proceso de parámetros
		SELECT @IdJournalVoucher = IdDepreciationAccountingVoucher, @ProcessDate = ProcessDate 
		FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId
		
		--Se inicia con la creación del comprobante contable
		DECLARE @LegalBookId INT
		DECLARE InfoItem CURSOR FOR 
		SELECT distinct LegalBookId FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id

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
				INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
				INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
				INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
				INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
				INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
				INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id 
				INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = cd.LoanSpendAccountId
				INNER JOIN GeneralLedger.MainAccounts maloan on maloan.Id = cd.ExpenseLoanAccountId
				INNER JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = cd.LoanLeasingSpendAccountId
				INNER JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
				INNER JOIN FixedAsset.FixedAssetResponsible r on r.Id = ddc.ResponsibleId
				WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
			)
			BEGIN
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
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							WHEN 7 THEN 
								CASE maleasing.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							ELSE 
								CASE ma.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
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
						,SUM(ddc.DepreciationValue)
						,'Detalle generado desde depreciación'
						,NULL
						,NULL
						,NULL
						,NULL
						,@LegalBookId
					FROM FixedAsset.FixedAssetDepreciation d
					INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
					INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
					INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
					INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
					INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
					INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
					INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = c.DepreciationAccountId
					INNER JOIN GeneralLedger.MainAccounts maloan on maloan.Id = c.LoanLeasingAccountId
					INNER JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = c.DepreciationLeasingAccountId				
					INNER JOIN FixedAsset.FixedAssetResponsible r on r.Id = ddc.ResponsibleId
					WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
					GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
						c.DepreciationAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
						c.DepreciationLeasingAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 
						c.LoanLeasingAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty, c.FinancialRentingAccountId
				
					-- Se insertan Débitos
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
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							WHEN 7 THEN 
								CASE maleasing.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							ELSE 
								CASE ma.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
						END
						,cd.CostCenterId
						,SUM(ddc.DepreciationValue * cd.DistributionPercentage / 100)
						,0
						,'Detalle generado desde depreciación'
						,NULL
						,NULL
						,NULL
						,NULL
						,@LegalBookId
					FROM 
						FixedAsset.FixedAssetDepreciation d
						INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd ON d.Id = dd.FixedAssetDepreciationId
						INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = dd.FixedAssetPhysicalAssetId
						INNER JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
						INNER JOIN FixedAsset.FixedAssetItemCatalog c ON c.Id = i.ItemCatalogId
						INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd ON cd.ItemCatalogId = c.Id
						INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
						INNER JOIN FixedAsset.FixedAssetLocation l ON l.Id = ddc.LocationId														 
						INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = cd.LoanSpendAccountId
						INNER JOIN GeneralLedger.MainAccounts maloan ON maloan.Id = cd.ExpenseLoanAccountId
						INNER JOIN GeneralLedger.MainAccounts maleasing ON maleasing.Id = cd.LoanLeasingSpendAccountId	
						INNER JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
						INNER JOIN FixedAsset.FixedAssetResponsible r ON r.Id = ddc.ResponsibleId
					WHERE 
						d.Id = @Id AND dd.LegalBookId = @LegalBookId AND c.HandlesDepreciationbyDistribution = 1
					GROUP BY 
						r.ThirdPartyId, 
						ddc.CostCenterId, 
						pa.AdquisitionType,
						cd.LoanSpendAccountId, 
						ma.HandlesCostCenter, 
						ma.HandlesThirdParty,
						cd.LoanLeasingSpendAccountId, 
						maleasing.HandlesCostCenter, 
						maleasing.HandlesThirdParty, 
						cd.ExpenseLoanAccountId, 
						maloan.HandlesCostCenter, 
						maloan.HandlesThirdParty, 
						cd.LoanFinancialRentingAccountId,
						cd.CostCenterId,
						maloan.Id,
						maleasing.Id,
						maFinancialRenting.Id,
						ma.Id

			END
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
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							WHEN 7 THEN 
								CASE maleasing.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							ELSE 
								CASE ma.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
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
						,SUM(ddc.DepreciationValue)
						,'Detalle generado desde depreciación'
						,NULL
						,NULL
						,NULL
						,NULL
						,@LegalBookId
					FROM FixedAsset.FixedAssetDepreciation d
					INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
					INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
					INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
					INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
					INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
					INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
					INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = c.DepreciationAccountId
					INNER JOIN GeneralLedger.MainAccounts maloan on maloan.Id = c.LoanLeasingAccountId
					INNER JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = c.DepreciationLeasingAccountId				
					INNER JOIN FixedAsset.FixedAssetResponsible r on r.Id = ddc.ResponsibleId
					WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId and c.HandlesDepreciationbyDistribution = 0
					GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
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
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							WHEN 7 THEN 
								CASE maleasing.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
									ELSE NULL 
								END
							ELSE 
								CASE ma.HandlesThirdParty 
									WHEN 1 THEN @ThirdPartyId 
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
						,SUM(ddc.DepreciationValue)
						,0
						,'Detalle generado desde depreciación'
						,NULL
						,NULL
						,NULL
						,NULL
						,@LegalBookId
					FROM FixedAsset.FixedAssetDepreciation d
					INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
					INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
					INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
					INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
					INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
					INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
					INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
					INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = f.AccountingStructureId
					INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = cd.LoanSpendAccountId
					INNER JOIN GeneralLedger.MainAccounts maloan on maloan.Id = cd.ExpenseLoanAccountId
					INNER JOIN GeneralLedger.MainAccounts maleasing on maleasing.Id = cd.LoanLeasingSpendAccountId
					INNER JOIN GeneralLedger.MainAccounts maFinancialRenting on maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
					INNER JOIN FixedAsset.FixedAssetResponsible r on r.Id = ddc.ResponsibleId
					WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId and c.HandlesDepreciationbyDistribution = 0
					GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
						ma.Id, ma.HandlesCostCenter, ma.HandlesThirdParty, 
						maleasing.Id, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty, 					
						maloan.Id, maloan.HandlesCostCenter, maloan.HandlesThirdParty, maFinancialRenting.Id

			
			--Se pasa a la siguiente posicion del cursor
			FETCH NEXT FROM InfoItem INTO @LegalBookId
			continue
		END
		Close InfoItem
		Deallocate InfoItem
		
		--Se guarda los comprobantes generados
		
		--Se declara la variable para saber en que posición coloco la coma
		DECLARE @Cont INT = 0
		DECLARE InfoItemDepreciarion CURSOR FOR 
		SELECT distinct LegalBookId FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id

		OPEN InfoItemDepreciarion
		FETCH NEXT FROM InfoItemDepreciarion INTO @LegalBookId
		WHILE @@fetch_status = 0
		BEGIN			
			--genero el XML para guardar el comprobante 
			DECLARE @resultJournalVoucher table (codeMessage VARCHAR(20), [Message] VARCHAR(MAX),IdJournalVoucher integer)
			DELETE FROM @resultJournalVoucher
			DECLARE @JournalVoucherXML AS XML
			SELECT @JournalVoucherXML =  convert(xml, (SELECT * 
			FROM @TableJournalVoucher JournalVoucher 
			INNER JOIN @TableJournalVoucherDetail JournalVoucherDetail on JournalVoucher.LegalBookId = JournalVoucherDetail.LegalBookId
			WHERE JournalVoucher.LegalBookId = @LegalBookId For xml AUTO,TYPE, ELEMENTS))
				
			INSERT @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
			
			IF (SELECT codeMessage  FROM @resultJournalVoucher) = '999' 
			BEGIN		
				Close InfoItemDepreciarion
				Deallocate InfoItemDepreciarion
				DECLARE @errorJV VARCHAR(MAX)
				SELECT @errorJV = [Message]  FROM @resultJournalVoucher
				
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
			SET padb.DaysPendingDepreciate = IIF(padb.ResidualValue = dd.DepreciationValue, 0, padb.DaysPendingDepreciate - dd.DepreciatedDays),
				padb.DepreciatedDays = padb.DepreciatedDays + dd.DepreciatedDays,
				padb.DepreciatedValue = padb.DepreciatedValue + dd.DepreciationValue,
				padb.ResidualValue = padb.ResidualValue - dd.DepreciationValue,
				padb.FinantialDiscountDepreciated = dd.FinantialDiscountDepreciation
		FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb 
		join
		(
			SELECT dd.FixedAssetPhysicalAssetDetailBookId,
				SUM(dd.DepreciatedDays) DepreciatedDays,
				SUM(dd.DepreciationValue - dd.FinantialDiscountAdjusment) DepreciationValue,
				SUM(dd.FinantialDiscountDepreciation + dd.FinantialDiscountAdjusment) FinantialDiscountDepreciation
			FROM FixedAsset.FixedAssetDepreciationDetail dd
			WHERE dd.FixedAssetDepreciationId = @Id
			GROUP BY dd.FixedAssetPhysicalAssetDetailBookId
		) dd on padb.Id = dd.FixedAssetPhysicalAssetDetailBookId

	END
	
	SELECT	@CodeResult = 0, 
			@MessageResult = IIF(@ModeConfirm = 0,
						'Se guardó la Depreciación de Activos',
						CONCAT('Se confirmó la Depreciación de Activos, generando los comprobantes contables ', @ResultConsecutives, ' de tipo ', @JournalVoucherType)
					)
END
