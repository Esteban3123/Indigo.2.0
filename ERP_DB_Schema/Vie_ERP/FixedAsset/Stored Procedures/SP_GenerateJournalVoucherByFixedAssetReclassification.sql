-- =================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-06-26
-- Description:	Procedimiento que se encarga de realizar la reclasificación de activos
-- =================================================================================
CREATE PROCEDURE [FixedAsset].[SP_GenerateJournalVoucherByFixedAssetReclassification] 
	@Id as INT,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
/* -------------------- DECLARACION DE VARIABLES -------------------- */

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		DECLARE @JournalVourcherTmp TABLE (
				Id INT, 
				Consecutive BIGINT,
				LegalBookId INT, 
				IdJournalVoucher INT, 
				VoucherDate VARCHAR(30),
				Imported VARCHAR(5),
				Status TINYINT,
				Detail VARCHAR(500),
				EntityCode VARCHAR(20),
				EntityId INT,
				EntityName VARCHAR(250),
				IsClosedYear VARCHAR(5)
			)

		--Se declara una tabla temporal para los detalles del comprobante
		DECLARE @JournalVourcherDetailTmp TABLE (
				Id INT,
				IdAccounting INT,
				IdMainAccount INT,
				IdThirdParty INT,
				IdCostCenter INT,
				DebitValue DECIMAL(20,4),
				CreditValue DECIMAL(20,4),
				Detail VARCHAR(500),
				IdRetention INT,
				RetentionRate decimal(5,3),
				BaseValue decimal(18,2),
				BillingValue decimal(18,2)
			)

		--Tabla temporal para guardar el resultado del save del comprobante contable
		DECLARE @resultJournalVoucher TABLE (
				code varchar(20),
				MessageResult varchar(max),
				IdJournalVoucher integer
			)

		--Datos de la reclasificación
		DECLARE 
			@Code VARCHAR(20),
			@OperatingUnitId INT,
			@Year INT,
			@Month TINYINT,
			@ReclassificationType TINYINT,
			@ItemIdPrevious INT,
			@ItemCatalogIdPrevious INT,			
			@ItemId INT,
			@ItemCatalogId INT

		--Datos necesarios para la contabilización
		DECLARE 
			@Form AS VARCHAR(200) = 'FixedAssetReclassification',			
			@ThirdPartyId AS INT,
			@ThirdPartyIdDIAN AS INT,
			@errors VARCHAR(MAX),			
			@IdJournalVoucher AS INT,
			@JournalVoucherXML AS XML
		
/* ------------------------- VALIDACIONES --------------------------- */

		-- Se consultan los datos de la reclasificación para realizar las validaciones
		SELECT 
			@Code = far.Code,
			@OperatingUnitId = far.OperatingUnitId,
			@Year = YEAR(far.DocumentDate),
			@Month = MONTH(far.DocumentDate),
			@ReclassificationType = far.ReclassificationType,
			@ItemIdPrevious = far.ItemIdPrevious,
			@ItemCatalogIdPrevious = far.ItemCatalogIdPrevious,			
			@ItemId = far.ItemId,
			@ItemCatalogId = far.ItemCatalogId
		FROM FixedAsset.FixedAssetReclassification far			
		WHERE far.Id = @Id

		--Se valida que exista parámetros de activos fijos
		IF NOT EXISTS ( SELECT sfa.Id FROM FixedAsset.SettingFixedAsset sfa WHERE sfa.OperatingUnitId = @OperatingUnitId )
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de activo fijo para la unidad operativa seleccionada' AS Message
			RETURN
		END
		
		--Valido que el mes sea el mismo de parámetros de activos fijos
		IF NOT EXISTS ( SELECT sfa.Id FROM FixedAsset.SettingFixedAsset sfa WHERE sfa.OperatingUnitId = @OperatingUnitId AND YEAR(sfa.ProcessDate) = @Year AND MONTH(sfa.ProcessDate) = @Month )
		BEGIN
			SELECT 999 AS CodeMessage, 'La fecha del documento no corresponde con el periodo del parámetro de Activos Fijos' AS Message
			RETURN
		END

		--Valido que el mes este abierto
		IF NOT EXISTS ( SELECT cm.Id FROM [GeneralLedger].[ClosedMonth] cm WHERE [Year] = @Year AND [Month] = @Month AND cm.Status = 1 )
		BEGIN
			SELECT 999 AS CodeMessage, 'La fecha del documento no corresponde con un periodo abierto en contabilidad' AS Message
			RETURN
		END

		--Se valida que el tercero de la dian exista
		IF NOT EXISTS ( SELECT gls.IdDian FROM GeneralLedger.GeneralLedgerSettings gls WHERE gls.IdOperatingUnit = @OperatingUnitId )
		BEGIN
			SELECT 999 AS CodeMessage, 'No se ha parametrizado tercero de la DIAN para la unidad operativa' AS Message
			RETURN
		END

		--Se valida que se haya parametrizado al menos un libro contable en viebot
		IF NOT EXISTS (SELECT Id FROM GeneralLedger.VieBot WHERE Form = @Form AND Allow = 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'No se ha parametrizado un libro contable en VieBot' AS Message
			RETURN
		END

		--Los libros contables parametrizados deben ser no homologables
		IF EXISTS (SELECT Id FROM GeneralLedger.VieBot WHERE Form = @Form AND Allow = 1 AND HandlesHomologation = 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'Los libros contables parametrizados en VieBot para Finalización de Contrato no deben ser homologables' AS Message
			RETURN
		END

		--Los catalogos seleccionados no pueden ser el mismo
		IF @ItemCatalogIdPrevious = @ItemCatalogId
		BEGIN
			SELECT 999 AS CodeMessage, 'Los catalogos seleccionados son los mismos' AS Message
			RETURN
		END

		--Se valida que todos los activos en el detalle pertenezcan al articulo ya seleccionado, con el catalogo registrado
		IF EXISTS (SELECT *
			FROM 
			(
				SELECT fard.PhysicalAssetId
				FROM FixedAsset.FixedAssetReclassification far
				JOIN FixedAsset.FixedAssetReclassificationDetail fard ON far.Id = fard.FixedAssetReclassificationId
				WHERE far.Id = @Id
			) far
			FULL JOIN 
			(
				SELECT fapa.Id
				FROM FixedAsset.FixedAssetPhysicalAsset fapa
				JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				WHERE fai.Id = @ItemIdPrevious AND fai.ItemCatalogId = @ItemCatalogIdPrevious
			) fapa ON far.PhysicalAssetId = fapa.Id
			WHERE far.PhysicalAssetId IS NULL OR fapa.Id IS NULL
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Los activos registrados en reclasificación no pertenecen al artículo o catalogo seleccionado' AS Message
			RETURN
		END

		--Si es reclasificación de artículo
		IF @ReclassificationType = 2
		BEGIN			
			--Valido que el catalogo corresponda con el nuevo artículo
			IF NOT EXISTS (SELECT Id FROM FixedAsset.FixedAssetItem WHERE Id = @ItemId AND ItemCatalogId = @ItemCatalogId)
			BEGIN
				SELECT 999 AS CodeMessage, 'El catalogo no corresponde al artículo seleccionado' AS Message
				RETURN
			END
		END		
		
--/* ------------------------- ASIGNACION VALORES PARA LA CONTABILIZACION --------------------------- */

		--Se obtiene el id del tercero de la DIAN
		SELECT @ThirdPartyIdDIAN = IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId

		--Se obtiene el tipo de comprobante contable
		SELECT @IdJournalVoucher = ReclassificationJournalVoucherId FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId

		--Variables para recorrer los libros contables parametrizados en VieBot
		DECLARE @Rows INT = 1, 
				@RowId INT = 1,
				@LegalBookId INT

/* -------------------- RECORRIDO LIBROS VIEBOT -------------------- */
		WHILE @Rows > 0
		BEGIN
			--Obtengo el libro oficial
			SELECT TOP 1 @RowId = Id, @LegalBookId = LegalBookId
			FROM GeneralLedger.VieBot 
			WHERE Form = @Form AND Allow = 1 AND HandlesHomologation = 0 AND Id >= @RowId
			ORDER BY Id
			
			--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
			BEGIN
				BREAK
			END

			/* -------------------- CABECERA DEL COMPROBANTE -------------------- */
			
			DELETE FROM @JournalVourcherTmp

			INSERT INTO @JournalVourcherTmp (Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
				VALUES (0, 0, @LegalBookId, @IdJournalVoucher, [Common].[GETDATE](), 'False', 2, 'Comprobante contable generado desde Reclasificación de Activos', @Code, @Id, @Form, 0)

			/* -------------------- DETALLE DEL COMPROBANTE -------------------- */			
			
			DELETE FROM @JournalVourcherDetailTmp

			INSERT INTO @JournalVourcherDetailTmp
				( Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue )
				/**** Reversion Anterior Catalogo ***/
				--Detalle Debito (Valor Depreciado) y credito (Valor Residual / Historico)
					SELECT 
						0 AS Id, 
						0 AS IdAccounting, 
						ma.Id AS IdMainAccount, 
						CASE ma.HandlesThirdParty WHEN 1 THEN IIF(fapa.IsMainAccount = 1, s.IdThirdParty, @ThirdPartyIdDIAN) ELSE NULL END AS IdThirdParty,
						CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END AS IdCostCenter,
						ISNULL(CASE WHEN fapa.Depreciate = 1 AND fapa.IsMainAccount = 0 THEN fapadb.DepreciatedValue ELSE 0 END, 0) AS DebitValue,
						ISNULL(CASE fapa.Depreciate WHEN 1 THEN IIF(fapa.IsMainAccount = 1, fapadb.ResidualValue, 0) ELSE fapa.HistoricalValue END, 0) AS CreditValue,
						'Reclasificación del Valor ' + IIF(fapa.Depreciate = 1, IIF(fapa.IsMainAccount = 1, 'Residual', 'Depreciado'), 'Histórico')  + ' del anterior Catalogo del Activo con placa ' + fapa.Plate AS Detail,
						NULL AS IdRetention, 
						0 AS RetentionRate, 
						0 AS BaseValue, 
						0 AS BillingValue
					FROM FixedAsset.FixedAssetReclassification far
					JOIN FixedAsset.FixedAssetReclassificationDetail fard ON far.Id = fard.FixedAssetReclassificationId
					JOIN 
					(
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, fapa.MainAccountId, 1 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa
						UNION
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							CASE fapa.AdquisitionType 
								WHEN 3 THEN faic.LoanLeasingAccountId
								WHEN 7 THEN faic.DepreciationLeasingAccountId 
								ELSE faic.DepreciationAccountId
							END AS MainAccountId, 0 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa 				
						JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fapa.ItemId
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
						WHERE fapa.Depreciate = 1
					) fapa ON fapa.Id = fard.PhysicalAssetId
					JOIN FixedAsset.FixedAssetLocation fal ON fal.Id = fapa.LocationId
					JOIN Payroll.FunctionalUnit fu ON fu.Id = fal.FunctionalUnitId
					JOIN Common.Supplier s ON fapa.SupplierId = s.Id
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = fapa.MainAccountId
					LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
					--Todos los activos del detalle, menos los que ya han sido devueltos o han salido
					WHERE far.Id = @Id 
						--No se reclasifica lo que ya salio o fue devuelto
						AND fapa.HasOutput = 0 AND fapa.OutputRefund = 0
						--Tampoco se reclasifica el renting operativo ni el comodato tercerizado puesto que estos no contabilizan
						AND fapa.AdquisitionType NOT IN (8, 10)
				UNION
				/**** Insersión valores en el Nuevo Catalogo ***/
				--Detalle Debito (Valor Residual / Historico) y credito (Valor Depreciado)
					SELECT 
						0 AS Id, 
						0 AS IdAccounting, 
						ma.Id AS IdMainAccount, 
						CASE ma.HandlesThirdParty WHEN 1 THEN IIF(fapa.IsMainAccount = 1, s.IdThirdParty, @ThirdPartyIdDIAN) ELSE NULL END AS IdThirdParty,
						CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END AS IdCostCenter,
						 ISNULL(CASE fapa.Depreciate WHEN 1 THEN IIF(fapa.IsMainAccount = 1, fapadb.ResidualValue, 0) ELSE fapa.HistoricalValue END, 0) AS DebitValue,
						ISNULL(CASE WHEN fapa.Depreciate = 1 AND fapa.IsMainAccount = 0 THEN fapadb.DepreciatedValue ELSE 0 END, 0) AS CreditValue,
						'Reclasificación del Valor ' + IIF(fapa.Depreciate = 1, IIF(fapa.IsMainAccount = 1, 'Residual', 'Depreciado'), 'Histórico')  + ' del nuevo Catalogo del Activo con placa ' + fapa.Plate AS Detail,
						NULL AS IdRetention, 
						0 AS RetentionRate, 
						0 AS BaseValue, 
						0 AS BillingValue
					FROM FixedAsset.FixedAssetReclassification far
					JOIN FixedAsset.FixedAssetReclassificationDetail fard ON far.Id = fard.FixedAssetReclassificationId
					JOIN 
					(
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							CASE fapa.AdquisitionType 
								WHEN 3 THEN faic.DebitLoanAccountId
								WHEN 7 THEN faic.IncomeLeasingAccountId
								ELSE faic.IncomeAccountId
							END AS MainAccountId, 1 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = @ItemCatalogId
						UNION
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							CASE fapa.AdquisitionType 
								WHEN 3 THEN faic.LoanLeasingAccountId
								WHEN 7 THEN faic.DepreciationLeasingAccountId 
								ELSE faic.DepreciationAccountId
							END AS MainAccountId, 0 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa 				
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = @ItemCatalogId						
						WHERE fapa.Depreciate = 1
					) fapa ON fapa.Id = fard.PhysicalAssetId
					JOIN FixedAsset.FixedAssetLocation fal ON fal.Id = fapa.LocationId
					JOIN Payroll.FunctionalUnit fu ON fu.Id = fal.FunctionalUnitId
					JOIN Common.Supplier s ON fapa.SupplierId = s.Id
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = fapa.MainAccountId
					LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
					WHERE far.Id = @Id 
						--No se reclasifica lo que ya salio o fue devuelto
						AND fapa.HasOutput = 0 AND fapa.OutputRefund = 0
						--Tampoco se reclasifica el renting operativo ni el comodato tercerizado puesto que estos no contabilizan
						AND fapa.AdquisitionType NOT IN (8, 10)

			--Eliminamos aquellos detalles donde no se ha depreciado o se ha depreciado todo el activo (Valores en 0)
			DELETE FROM @JournalVourcherDetailTmp WHERE DebitValue = 0 AND CreditValue = 0

			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			SELECT @JournalVoucherXML = convert(xml, (
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

			--Se consume el sp que guarda el comprobante contable
			INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

			--Se valida que no hayan errores en el guardado del comprobante contable
			IF EXISTS (SELECT * FROM @resultJournalVoucher WHERE code = '999')
			BEGIN							
				SELECT @errors = MessageResult FROM @resultJournalVoucher WHERE code = '999'
				SELECT 999 AS CodeMessage, @errors AS Message
				RETURN
			END

			SET @RowId += 1
		END

		IF NOT EXISTS (SELECT r.IdJournalVoucher FROM @resultJournalVoucher r)
		BEGIN
			SELECT 999 AS CodeMessage, 'No se genero ningun comprobante contable' AS Message
			RETURN
		END
				
		IF @ReclassificationType = 1
		BEGIN
			--Si el tipo de reclasificación es de catálogo, se actualiza el catálogo al artículo
			UPDATE i 
				SET i.ItemCatalogId = @ItemCatalogId
			FROM FixedAsset.FixedAssetItem i 
			WHERE i.Id = @ItemIdPrevious
		END
		ELSE
		BEGIN
			--Si el tipo de reclasificación es de artículo, se actualiza el artículo a los activos fijos
			UPDATE fapa 
				SET fapa.ItemId = @ItemId
			FROM FixedAsset.FixedAssetReclassificationDetail fard
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id
			WHERE fard.FixedAssetReclassificationId = @Id
		END

		UPDATE fard 
				SET fard.MainAccountId = fapa.MainAccountId
		FROM FixedAsset.FixedAssetReclassificationDetail fard
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id
		WHERE fard.FixedAssetReclassificationId = @Id

		--Se actualiza la cuenta del activo
		UPDATE fapa 
			SET fapa.MainAccountId = CASE fapa.AdquisitionType 
								WHEN 3 THEN faic.DebitLoanAccountId
								WHEN 7 THEN faic.IncomeLeasingAccountId
								ELSE faic.IncomeAccountId
							END
		FROM FixedAsset.FixedAssetReclassificationDetail fard
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = @ItemCatalogId
		WHERE fard.FixedAssetReclassificationId = @Id

		--Actualizo el estado de los activos a la fecha de la reclasificación
		UPDATE fard
			SET fard.HasOutput = fapa.HasOutput,
				fard.Status = fapa.Status,
				fard.OutputRefund = fapa.OutputRefund,
				fard.AdquisitionType = fapa.AdquisitionType
		FROM FixedAsset.FixedAssetReclassificationDetail fard
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id
		WHERE fard.FixedAssetReclassificationId = @Id

		--Actualizo todo el detalle relacionado con los libros del activo a la fecha de la reclasificación
		UPDATE fardb
			SET fardb.DaysPendingDepreciate = ISNULL(fapadb.DaysPendingDepreciate, 0),
				fardb.DepreciatedDays = ISNULL(fapadb.DepreciatedDays, 0),
				fardb.Valorization = ISNULL(fapadb.Valorization, 0),
				fardb.Devaluation = ISNULL(fapadb.Devaluation, 0),
				fardb.AdjustedValue = ISNULL(fapadb.AdjustedValue, 0),
				fardb.TransactionValue = ISNULL(fapadb.TransactionValue, 0),
				fardb.DepreciatedValue = ISNULL(fapadb.DepreciatedValue, 0),
				fardb.InflationAdjustmentValue = ISNULL(fapadb.InflationAdjustmentValue, 0),
				fardb.ResidualValue = ISNULL(fapadb.ResidualValue, 0),
				fardb.HistoricalValue = ISNULL(fapadb.HistoricalValue, fapa.HistoricalValue)
		FROM FixedAsset.FixedAssetReclassificationDetail fard
		JOIN FixedAsset.FixedAssetReclassificationDetailBook fardb ON fard.Id = fardb.FixedAssetReclassificationDetailId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
		WHERE fard.FixedAssetReclassificationId = @Id
		
		--Se obtiene los consecutivos generados
		DECLARE @Consecutive AS VARCHAR(MAX)
		SELECT @Consecutive = stuff((SELECT DISTINCT N'; ' + CAST(jv.Consecutive AS VARCHAR(30))
				FROM @resultJournalVoucher r
				JOIN GeneralLedger.JournalVouchers jv ON r.IdJournalVoucher = jv.Id
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')					

		--Se genera el mensaje a devolver
		DECLARE @Message varchar(max) = 'Se confirmó correctamente'
		SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generaron los comprobantes contables tipo ' + Code + ' - ' + Name + ' con consecutivos ' + @Consecutive 
		FROM GeneralLedger.JournalVoucherTypes 
		WHERE Id = @IdJournalVoucher

		SELECT 0 AS CodeMessage, @Message AS Message
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() AS Message
	END CATCH
END