-- =============================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Genera y guarda la depreciación de los activos
-- =============================================================
CREATE PROCEDURE [FixedAsset].[SP_SaveDepreciation_Previous] 
    @DepreciateMonth AS INT,
	@DepreciateYear AS INT,
	@CodeUser AS VARCHAR(20),
	@OperatingUnitId AS INT,
	@ModeConfirm AS BIT
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	--Id de la cabecera de la depreciación
	DECLARE @Id AS INT

	--Código de la cabecera
	DECLARE @Code AS VARCHAR(20)

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

	--Fecha Inicial de la Depreciacion
	DECLARE @DateDepreciateInitial date = CAST('01/' + CAST(@DepreciateMonth AS VARCHAR(10)) + '/' + CAST(@DepreciateYear AS VARCHAR(10)) AS date)

	--Fecha Final de la Depreciacion
	DECLARE @DateDepreciateEnd date = DATEADD(MONTH, 1, @DateDepreciateInitial)
	
	--Indica si se tiene activo el parameto Depreciacion a 30 dias
	DECLARE @Depreciation30Days BIT = (SELECT Depreciation30Days FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId)

	--Cantidad de dias que se va a depreciar en el mes
	DECLARE @DaysDepreciateMonth INT = IIF( @Depreciation30Days = 1, 30 ,datediff(DAY, @DateDepreciateInitial, @DateDepreciateEnd))

	-- Id del tercero de la entidad
	DECLARE @ThirdPartyId INT

	-- Variable para almacenar los errores encontrados
	DECLARE @errors VARCHAR(MAX)

	BEGIN TRY

		--se valida que todos los parámetros tengan el mismo periodo
		IF exists
		(
			SELECT 1
			FROM FixedAsset.SettingFixedAsset sfa
			JOIN FixedAsset.SettingFixedAsset sfad ON YEAR(sfa.ProcessDate) <> YEAR(sfad.ProcessDate) OR MONTH(sfa.ProcessDate) <> MONTH(sfad.ProcessDate)
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Existen unidades operativas con periodos de activos diferentes' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END
		
		--Se valida que existan parametros en contabilidad
		IF(SELECT count(*) FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId) = 0 BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de contabilidad' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END
		SET @ThirdPartyId = (SELECT IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId)
		--Se valida que exista parámetros de activos fijos en la BD
		IF (SELECT count(*) FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId) = 0
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de activos fijos para realizar la depreciacón' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se valida que el mes que se va a depreciar sea el mismo mes que el que tiene parámetros de activos fijos
		DECLARE @MonthSettings INT
		SELECT @MonthSettings = MONTH(ProcessDate) FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId
		IF @MonthSettings <> @DepreciateMonth --Si el mes que se va a depreciar es diferente al mes del proceso de parámetros
		BEGIN
			SELECT 999 AS CodeMessage, 'El mes que se va a depreciar no es igual al mes de proceso de parámetros de activos fijos' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se valida que todos los registros de parametros de activos tengan la misma fecha para poder depreciar, por peticion del bug 7489 att: Carlos Mario
		DECLARE @CountRepeat INT = 0 --Cantidad de registros repetidos con el mismo mes y el mismo año
		DECLARE @CountTable INT = 0 --Cantidad de registros que tiene la tabla de parametros

		--Se obtiene la cantidad de registros repetidos con el mismo mes y año
		SELECT @CountRepeat = Count(*)
		FROM FixedAsset.SettingFixedAsset
		GROUP BY MONTH(ProcessDate), YEAR(ProcessDate)
		Having Count(*) >= 1
		ORDER BY MONTH(ProcessDate), YEAR(ProcessDate)

		--Se obtiene la cantidad de registros que tiene la tabla de parametros para poder comparar que la cantidad de repetidos con mes y año sea igual a la cantidad de registros
		SELECT @CountTable = COUNT(*) FROM FixedAsset.SettingFixedAsset

		IF @CountRepeat <> @CountTable
		BEGIN
			SELECT 999 AS CodeMessage, 'No se puede depreciar porque la fecha de proceso de los registros de parámetros de activos no son iguales' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END
		
		--Se valida que al menos haya un registro en la tabla de physicalAsset para poder realizar las operaciones
		IF (SELECT COUNT(*)
		FROM FixedAsset.FixedAssetPhysicalAsset pa 
		INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb on padb.PhysicalAssetId = pa.Id
		INNER JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
		WHERE pa.HasOutput = 0) = 0
		BEGIN
			SELECT 999 AS CodeMessage, 'No hay registros de activos para poder depreciar' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se consulta si hay registros con el mes y el año enviados desde el form, si hay no se crea la cabecera
		--y se eliminan los detalles para volverlos a generar
		IF (SELECT COUNT(*) FROM FixedAsset.FixedAssetDepreciation WHERE ClosingMonth = @DepreciateMonth AND ClosingYear = @DepreciateYear) > 0
		BEGIN

			--Se obtiene el id y el código
			SELECT @Id = Id, @Code = Code FROM FixedAsset.FixedAssetDepreciation WHERE ClosingMonth = @DepreciateMonth AND ClosingYear = @DepreciateYear
			
			IF @ModeConfirm = 0 BEGIN
				--Se eliminan los detalles del costo
				DELETE FROM FixedAsset.FixedAssetDepreciationDetailCost 
				WHERE FixedAssetDepreciationDetailId in (SELECT Id FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id)
				--Se eliminan los detalles
				DELETE FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id
			END
		END
		ELSE
		BEGIN --Si no hay datos se crea la secuencia y la cabecera

			-- Consultamos la secuencia numerica del form de depreciación
			DECLARE @idSequenceDetail INT
			DECLARE @pattern VARCHAR(300)
			DECLARE @NextS INT
			SELECT @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  FROM FixedAsset.FixedAssetSequenceDetail bsd INNER JOIN FixedAsset.FixedAssetSequence bs on bs.Id = bsd.IdSequenseFixedAssetC INNER JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
			WHERE bs.IdForm = '1122'
			IF (@idSequenceDetail is NULL)
			BEGIN
				SELECT 999 AS CodeMessage, 'Secuencia numérica no encontrada para generar la depreciación' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
				RETURN
			END
			SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
			UPDATE FixedAsset.FixedAssetSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

			--Se inserta en la cabecera de la depreciación
			INSERT INTO [FixedAsset].[FixedAssetDepreciation]
			   ([Code], [ClosingMonth], [ClosingYear], [ClosingDate], [Observation], [OperatingUnitId], [Status])
			VALUES
			   (@Code, @DepreciateMonth, @DepreciateYear, [Common].[GETDATE](), '', @OperatingUnitId, 1)

			--Obtengo el id de la cabecera
			SET @Id = SCOPE_IDENTITY()

		END
		
		--Se recorren los libros que tenga cada activo y que no tengan la propiedad de salida en false
		DECLARE InfoItem CURSOR FOR 
		SELECT pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
		FROM FixedAsset.FixedAssetPhysicalAsset pa 
		INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb on padb.PhysicalAssetId = pa.Id
		INNER JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
		INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = pa.LocationId
		INNER JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
		WHERE pa.HasOutput = 0 AND pa.Depreciate = 1 AND pa.Status = 1
		GROUP BY pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
		HAVING SUM(padb.DaysPendingDepreciate) > 0 AND SUM(padb.ResidualValue) > 0

		OPEN InfoItem
		FETCH NEXT FROM InfoItem 
		INTO @PhysicalAssetId, @ResponsibleId, @LocationId, @LocationClass, @CostCenterId, @AdquisitionDate
		WHILE @@fetch_status = 0
		BEGIN
			SET @DaysDepreciateMonthPhysicalAsset = 0
			IF (SELECT count(*) FROM FixedAsset.FixedAssetKardexItem WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd)) = 0 BEGIN -- Si no tiene movimientos en el mes
				IF @LocationClass = 1 or @LocationClass = 2 BEGIN ---Si la clase de la unidad Actual es Admnistrativa o Productiva
					IF @DateDepreciateInitial > @AdquisitionDate BEGIN
						SET @DaysDepreciateMonthPhysicalAsset = @DaysDepreciateMonth
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
				--Si el Activo ingreso despues de la fecha inicial del mes
				IF @DateDepreciateInitial < @AdquisitionDate BEGIN
					SET @DateIndexCursor = @AdquisitionDate
				END
				
				SET @QuantityMovement = (SELECT count(*)
				FROM FixedAsset.FixedAssetKardexItem k
				INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = k.LocationId
				INNER JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
				LEFT JOIN FixedAsset.FixedAssetLocation lp on lp.Id = PreviousLocationId
				LEFT JOIN Payroll.FunctionalUnit fup on fup.Id = lp.FunctionalUnitId
				WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd))

				DECLARE movement_cursor CURSOR FOR 
				SELECT DocumentDate,ISNULL(PreviousResponsibleId,ResponsibleId),ResponsibleId,ISNULL(PreviousLocationId,LocationId),LocationId, ISNULL(fup.CostCenterId, fu.CostCenterId),fu.CostCenterId,ISNULL(lp.Class, l.Class), l.Class
				FROM FixedAsset.FixedAssetKardexItem k
				INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = k.LocationId
				INNER JOIN Payroll.FunctionalUnit fu on fu.Id = l.FunctionalUnitId
				LEFT JOIN FixedAsset.FixedAssetLocation lp on lp.Id = PreviousLocationId
				LEFT JOIN Payroll.FunctionalUnit fup on fup.Id = lp.FunctionalUnitId
				WHERE PhysicalAssetId = @PhysicalAssetId AND CAST(DocumentDate AS date) between @DateDepreciateInitial AND DATEADD(DAY,-1,@DateDepreciateEnd)
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
						SET @DaysDepreciateMovementPhysicalAsset = DATEDIFF(DAY, @DateIndexCursor, @DateDepreciateEnd)
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
		DepreciatedDays numeric(18,2), ValorizationValue numeric(18,2), DevaluationValue numeric(18,2), AdjustedValue numeric(18,2), 
		TransactionValue numeric(18,2), AccumulatedDepreciation numeric(18,2), ResidualValue numeric(18,2))
		
		DECLARE @FixedAssetDepreciationDetailCostTemp table(Id INT primary key identity(1,1), FixedAssetDepreciationDetailId INT, MainAccountId INT, ResponsibleId INT, LocationId INT, 
		CostCenterId INT, DepreciatedDays INT, DepreciationValue numeric(20,4))

		
		--Se inserta en las tablas temporales
		INSERT INTO @FixedAssetDepreciationDetailTemp
			([FixedAssetDepreciationId], [LegalBookId], [ActiveClass], [FixedAssetPhysicalAssetId], [FixedAssetPhysicalAssetDetailBookId],
			[FixedAssetPhysicalAssetPartsId], [FixedAssetPhysicalAssetPartsDetailBookId], [DepreciationValue], [LifeTime], [RemainingLifeTime], 
			[DepreciatedDays], [ValorizationValue], [DevaluationValue], [AdjustedValue], [TransactionValue], [AccumulatedDepreciation], [ResidualValue])
		SELECT 
			@Id, padb.LegalBookId, 1, pa.Id, padb.Id, NULL, NULL, 
			FixedAsset.fnCalculateDepreciateValue
			(
				padb.DepreciationType, 
				padb.DepreciatedValue,
				padb.ResidualValue,
				padb.PercentageRescue, 
				fai.DepreciateByTimeUse, 
				fal.UseTime, 
				padb.DepreciatedDays, 
				padb.DaysPendingDepreciate,
				tm.DepreciateDays
			), 
			padb.LifeTime, 0, IIF(tm.DepreciateDays > padb.DaysPendingDepreciate, padb.DaysPendingDepreciate, tm.DepreciateDays), padb.Valorization, padb.Devaluation, 
			padb.AdjustedValue, padb.TransactionValue, 0, padb.ResidualValue
		FROM 
		(
			SELECT PhysicalAssetId, SUM(DepreciatedDays) DepreciateDays
			FROM @TableMovement
			GROUP BY PhysicalAssetId
		) tm
		INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON tm.PhysicalAssetId = padb.PhysicalAssetId
		INNER JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
		INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
		INNER JOIN FixedAsset.FixedAssetItem fai on fai.Id = pa.ItemId
		INNER JOIN FixedAsset.FixedAssetLocation fal on fal.Id = pa.LocationId
		WHERE pa.Status = 1 
			AND pa.HasOutput = 0 
			AND pa.Depreciate = 1 
			AND padb.DaysPendingDepreciate > 0 
			AND padb.ResidualValue > 0 
			AND tm.DepreciateDays > 0 			
		
		INSERT INTO @FixedAssetDepreciationDetailCostTemp
			([FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], [DepreciatedDays], [DepreciationValue])
		SELECT dd.Id, pa.MainAccountId, tm.ResponsibleId, tm.LocationId, tm.CostCenterId, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays), 
		FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue, dd.DepreciatedDays, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays)) 
		FROM @FixedAssetDepreciationDetailTemp dd
		INNER JOIN @TableMovement tm on tm.PhysicalAssetId = dd.FixedAssetPhysicalAssetId
		INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
		WHERE dd.FixedAssetDepreciationId = @Id

		--Ajustamos diferencias por redondeos
		UPDATE faddc
			SET faddc.DepreciationValue = faddc.DepreciationValue + (fadd.DepreciationValue - faddcd.DepreciationValue)
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
			--SELECT SUM(DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id
			--SELECT SUM(DepreciationValue) FROM @FixedAssetDepreciationDetailTemp
			IF ISNULL((SELECT SUM(DepreciationValue) FROM @FixedAssetDepreciationDetailTemp), 0) <> ISNULL((SELECT SUM(DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id), 0) 
			BEGIN
				SELECT 999 AS CodeMessage, 'Los valores del detalle de la depreciación han cambiado y debe recalcular' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
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
				SET @activosmal = (SELECT STRING_AGG(CONCAT('\n',pas.Plate, ' Valores: ', CAST(ddct.DepreciationValue AS VARCHAR(30)), ' - ', CAST(ddc.DepreciationValue AS VARCHAR(30))), ',')
				FROM @FixedAssetDepreciationDetailCostTemp ddct
				INNER JOIN @FixedAssetDepreciationDetailTemp ddt on ddt.Id = ddct.FixedAssetDepreciationDetailId
				INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on dd.FixedAssetPhysicalAssetId = ddt.FixedAssetPhysicalAssetId AND dd.LegalBookId = ddt.LegalBookId AND dd.FixedAssetDepreciationId = @Id
				INNER JOIN FixedAsset.FixedAssetPhysicalAsset pas on pas.Id = ddt.FixedAssetPhysicalAssetId
				INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on ddc.FixedAssetDepreciationDetailId = dd.Id AND ddc.DepreciatedDays = ddt.DepreciatedDays
				WHERE ddct.DepreciationValue <> ddc.DepreciationValue)
				
				SELECT 999 AS CodeMessage, CONCAT('Los valores del costo de la depreciación han cambiado y debe recalcular', ' ', @activosmal) AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
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
					AND fadd.DepreciationValue <> faddc.DepreciationValue
			)
			BEGIN 
				SELECT 999 AS CodeMessage, 'El valor a depreciar del costo de la depreciación es diferente al valor detallado' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
				RETURN
			END
		END
		ELSE 
		BEGIN
			INSERT INTO [FixedAsset].[FixedAssetDepreciationDetail]
			   ([FixedAssetDepreciationId], [LegalBookId], [ActiveClass], [FixedAssetPhysicalAssetId], [FixedAssetPhysicalAssetDetailBookId],
			   [FixedAssetPhysicalAssetPartsId], [FixedAssetPhysicalAssetPartsDetailBookId], [DepreciationValue], [LifeTime], [RemainingLifeTime], 
			   [DepreciatedDays], [ValorizationValue], [DevaluationValue], [AdjustedValue], [TransactionValue], [AccumulatedDepreciation], [ResidualValue])
			SELECT 
				@Id, padb.LegalBookId, 1, pa.Id, padb.Id, NULL, NULL, 
				FixedAsset.fnCalculateDepreciateValue
				(
					padb.DepreciationType, 
					padb.DepreciatedValue,
					padb.ResidualValue,
					padb.PercentageRescue, 
					fai.DepreciateByTimeUse, 
					fal.UseTime, 
					padb.DepreciatedDays, 
					padb.DaysPendingDepreciate,
					tm.DepreciateDays
				), 
				padb.LifeTime, 0, IIF(tm.DepreciateDays > padb.DaysPendingDepreciate, padb.DaysPendingDepreciate, tm.DepreciateDays), padb.Valorization, padb.Devaluation, 
				padb.AdjustedValue, padb.TransactionValue, 0, padb.ResidualValue
			FROM 
			(
				SELECT PhysicalAssetId, SUM(DepreciatedDays) DepreciateDays
				FROM @TableMovement
				GROUP BY PhysicalAssetId
			) tm
			INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON tm.PhysicalAssetId = padb.PhysicalAssetId
			INNER JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = padb.PhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetItem fai on fai.Id = pa.ItemId
			INNER JOIN FixedAsset.FixedAssetLocation fal on fal.Id = pa.LocationId
			WHERE pa.Status = 1 
				AND pa.HasOutput = 0 
				AND pa.Depreciate = 1 
				AND padb.DaysPendingDepreciate > 0 
				AND padb.ResidualValue > 0 
				AND tm.DepreciateDays > 0
		
			--Se insertan los costos del detalle de la depreciación con los datos que se registraron anteriormente y la tabla temporal de movimientos
			INSERT INTO [FixedAsset].[FixedAssetDepreciationDetailCost]
			 ([FixedAssetDepreciationDetailId], [MainAccountId], [ResponsibleId], [LocationId], [CostCenterId], [DepreciatedDays], [DepreciationValue])
			SELECT dd.Id, pa.MainAccountId, tm.ResponsibleId, tm.LocationId, tm.CostCenterId, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays), 
			FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue, dd.DepreciatedDays, IIF(tm.DepreciatedDays > dd.DepreciatedDays, dd.DepreciatedDays, tm.DepreciatedDays)) 
			FROM FixedAsset.FixedAssetDepreciationDetail dd
			INNER JOIN @TableMovement tm on tm.PhysicalAssetId = dd.FixedAssetPhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
			WHERE dd.FixedAssetDepreciationId = @Id

			--Ajustamos diferencias por redondeos
			UPDATE faddc
				SET faddc.DepreciationValue = faddc.DepreciationValue + (fadd.DepreciationValue - faddcd.DepreciationValue)
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

		--Tipo de comprobante
		DECLARE @JournalVoucherType VARCHAR(MAX) 
		
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
				SELECT 999 AS CodeMessage, @errors AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
				RETURN
			END
			
			---- Validamos que todos los catalogos tengan parametrizada la estructura contable
			IF (SELECT 
			COUNT(*)
			FROM FixedAsset.FixedAssetDepreciation d
			INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
			INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
			INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
			INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
			INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
			INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
			INNER JOIN Payroll.AccountingStructure ast on ast.Id = f.AccountingStructureId
			LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = ast.Id
			WHERE d.Id = @Id AND cd.id is NULL) > 0
			BEGIN
				SELECT @errors = stuff((SELECT distinct N'; El catálogo ' + c.Code + ' no tiene parametrizada la estructura contable ' + ast.Code
				FROM FixedAsset.FixedAssetDepreciation d
				INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
				INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa on pa.Id = dd.FixedAssetPhysicalAssetId
				INNER JOIN FixedAsset.FixedAssetItem i on i.Id = pa.ItemId
				INNER JOIN FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
				INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc on dd.Id = ddc.FixedAssetDepreciationDetailId
				INNER JOIN FixedAsset.FixedAssetLocation l on l.Id = ddc.LocationId
				INNER JOIN Payroll.FunctionalUnit f on f.Id = l.FunctionalUnitId
				INNER JOIN Payroll.AccountingStructure ast on ast.Id = f.AccountingStructureId
				LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd on cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = ast.Id
				WHERE d.Id = @Id AND cd.id is NULL
				for xml path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')
				SELECT 999 AS CodeMessage, @errors AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
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
				VALUES
				(@IdJournalVoucher, @ProcessDate, 0, 2, 'Comprobante generado desde depreciación para cerrar el mes ' + DATENAME(MONTH, CAST('01/' + CAST(@DepreciateMonth AS VARCHAR(MAX)) + '/' + CAST(@DepreciateYear AS VARCHAR(MAX)) AS date))  + ' del año ' + CAST(@DepreciateYear AS VARCHAR(MAX)), 
				@Code, @Id, 'FixedAssetDepreciation', 0, @LegalBookId)

				--Se insertan los detalles del comprobante

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
				WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId
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
				WHERE d.Id = @Id AND dd.LegalBookId = @LegalBookId
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
			--DECLARE InfoItem CURSOR FOR SELECT LegalBookId FROM @TableJournalVoucher
			----Consecutivos generados
			DECLARE @ResultConsecutives VARCHAR(MAX) = ''
		
			--Se declara la variable para saber en que posición coloco la coma
			DECLARE @Cont INT = 0
			DECLARE InfoItemDepreciarion CURSOR FOR 
			SELECT distinct LegalBookId FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id

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
				
				INSERT @resultJournalVoucher exec GeneralLedger.SP_SaveJournalVoucher @JournalVoucherXML,@CodeUser 

				IF (SELECT code  FROM @resultJournalVoucher) = '999' 
				BEGIN		
					Close InfoItemDepreciarion
					Deallocate InfoItemDepreciarion
					DECLARE @errorJV VARCHAR(MAX)
					SELECT @errorJV = MessageResult  FROM @resultJournalVoucher 
					SELECT 999 AS CodeMessage, @errorJV AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
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

			--Se actualiza el estado de la depreciación
			UPDATE FixedAsset.FixedAssetDepreciation SET Status = 2 WHERE Id = @Id

			--Se actualizan los campos respectivos a la depreciación de la tabla FixedAssetPhysicalAssetDetailBook
			UPDATE padb 
				SET padb.DaysPendingDepreciate = IIF(padb.ResidualValue = dd.DepreciationValue, 0, padb.DaysPendingDepreciate - dd.DepreciatedDays),
					padb.DepreciatedDays = padb.DepreciatedDays + dd.DepreciatedDays,
					padb.DepreciatedValue = padb.DepreciatedValue + dd.DepreciationValue,
					padb.ResidualValue = padb.ResidualValue - dd.DepreciationValue
			FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb 
			join
			(
				SELECT dd.FixedAssetPhysicalAssetDetailBookId,
					SUM(dd.DepreciatedDays) DepreciatedDays,
					SUM(dd.DepreciationValue) DepreciationValue
				FROM FixedAsset.FixedAssetDepreciationDetail dd
				WHERE dd.FixedAssetDepreciationId = @Id
				GROUP BY dd.FixedAssetPhysicalAssetDetailBookId
			) dd on padb.Id = dd.FixedAssetPhysicalAssetDetailBookId

			--Se actualiza la fecha de proceso de todas la unidades operativas de parámetros de activos fijos agregandole un mes
			UPDATE FixedAsset.SettingFixedAsset SET ProcessDate = EOMONTH(DATEADD(MONTH,1,@ProcessDate)) 

			--Asi estaba antes y por peticion del bug 7489 se decidio cambiar para que actualice la fecha a todas las unidades operativas att: Carlos Mario
			--UPDATE FixedAsset.SettingFixedAsset SET ProcessDate = DATEADD(MONTH,1,ProcessDate) WHERE OperatingUnitId = @OperatingUnitId
		END

		SELECT 0 AS CodeMessage, 'Se generó y se guardó correctamente la depreciación con código ' + @Code AS Message, @Code AS Code, @Id AS Id, @JournalVoucherType AS JournalVoucherType, @ResultConsecutives AS Consecutives
		
	END TRY
	BEGIN catch
		DECLARE @error_message VARCHAR(MAX) = ERROR_MESSAGE()
		IF ERROR_NUMBER() = 51000
		BEGIN
			THROW 51000, @error_message, 1
		END
		ELSE
		BEGIN
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
		END
	END catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y registra la depreciación mensual de activos fijos para una unidad operativa, correspondiente a un mes y año indicados. Valida las configuraciones de activos fijos y contabilidad, verifica que todos los parámetros de las unidades operativas tengan el mismo período de proceso, y controla que no existan depreciaciones duplicadas para el período solicitado. Calcula los días a depreciar (30 días fijos o días reales del mes según parámetro), recorre los activos físicos activos con libro legal vigente y sin baja, y crea o actualiza el encabezado y detalle de la depreciación en la tabla FixedAssetDepreciation. También genera comprobantes contables (JournalVouchers) con su consecutivo, y puede operar en modo de confirmación o simulación según el parámetro ModeConfirm.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciation_Previous';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciation_Previous';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y persiste el cierre mensual de depreciación de activos fijos por libro contable, calcula sus costos por ubicación/responsable/centro de costo y, al confirmar, contabiliza el comprobante y actualiza saldos depreciados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation_Previous';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todas las unidades operativas en SettingFixedAsset deben tener la misma fecha de proceso (mismo mes y año).; Debe existir registro en GeneralLedger.GeneralLedgerSettings para la unidad operativa.; Debe existir registro en FixedAsset.SettingFixedAsset para la unidad operativa.; El mes a depreciar debe coincidir con el mes de ProcessDate de los parámetros de activos fijos.; Todos los registros de SettingFixedAsset deben tener exactamente la misma fecha de proceso.; Debe existir al menos un activo (HasOutput=0) con libro legal activo.; Debe existir secuencia numérica configurada para el formulario 1122.; Al confirmar, todas las unidades funcionales involucradas deben tener AccountingStructureId asignada.; Al confirmar, los catálogos de los activos deben tener parametrizada la estructura contable correspondiente.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation_Previous';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] FixedAsset.FixedAssetDepreciationDetailCost: Cuando ya existe cabecera para el mes/año y ModeConfirm=0, elimina los costos de los detalles de esa depreciación para regenerarlos.; [DELETE] FixedAsset.FixedAssetDepreciationDetail: Cuando ya existe cabecera para el mes/año y ModeConfirm=0, elimina los detalles previos de esa depreciación para regenerarlos.; [INSERT] FixedAsset.FixedAssetDepreciation: Cuando no hay cabecera para el ClosingMonth/ClosingYear solicitados, crea una nueva con Status=1 y código generado por la secuencia del formulario 1122.; [UPDATE] FixedAsset.FixedAssetSequenceDetail: Cuando se crea una nueva cabecera, incrementa en 1 el campo Next de la secuencia asociada al formulario 1122.; [INSERT] FixedAsset.FixedAssetDepreciationDetail: Cuando ModeConfirm=0, inserta un detalle por cada activo con Status=1, HasOutput=0, Depreciate=1, DaysPendingDepreciate>0, ResidualValue>0 y días depreciados>0, calculando el valor con FixedAsset.fnCalculateDepreciateValue y limitando los días depreciados al pendiente.; [INSERT] FixedAsset.FixedAssetDepreciationDetailCost: Cuando ModeConfirm=0, inserta un costo por cada movimiento (responsable/ubicación/centro de costo) usando FixedAsset.fnCalculateDepreciationValueCost para distribuir el valor por días.; [UPDATE] FixedAsset.FixedAssetDepreciationDetailCost: Cuando la suma de DepreciationValue de los costos no coincide con el DepreciationValue del detalle (diferencia por redondeo), ajusta esa diferencia al costo con mayor Id del detalle.; [DELETE] FixedAsset.FixedAssetDepreciationDetailCost: Tras insertar costos, elimina los registros con DepreciationValue<=0.; [UPDATE] FixedAsset.FixedAssetDepreciation: Cuando ModeConfirm=1 y se procesa correctamente el comprobante, actualiza Status=2 (confirmada) en la cabecera de depreciación.; [UPDATE] FixedAsset.FixedAssetPhysicalAssetDetailBook: Al confirmar, descuenta DaysPendingDepreciate (o lo pone en 0 si ResidualValue=DepreciationValue), suma DepreciatedDays y DepreciatedValue, y resta ResidualValue por cada detalle de depreciación.; [UPDATE] FixedAsset.SettingFixedAsset: Al confirmar, avanza ProcessDate al fin de mes siguiente (EOMONTH(DATEADD(MONTH,1,ProcessDate))) en TODAS las unidades operativas.; [INSERT] GeneralLedger.JournalVouchers: Al confirmar, por cada LegalBookId del detalle invoca GeneralLedger.SP_SaveJournalVoucher con un comprobante que debita la cuenta de gasto/costo y acredita la cuenta del activo según AdquisitionType (3=Loan, 7=Leasing, 9=FinancialRenting, otro=normal).; [RETURN_RESULT] FixedAssetDepreciation: Devuelve fila con CodeMessage=999 y mensaje de error para cada validación fallida; CodeMessage=0 al finalizar correctamente con Code, Id, JournalVoucherType y consecutivos generados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation_Previous';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation_Previous';
-- GO
