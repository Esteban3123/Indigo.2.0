-- =================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/03/2017
-- Description:	Procedimiento que se encarga de finalizar los contratos de leasing
-- =================================================================================
CREATE PROCEDURE [FixedAsset].[SP_ConfirmLeasingContractsFinalization]
	@Xml AS XML,
	@OperatingUnitId AS INT,
	@Year AS INT,
	@Month AS INT,
	@CompanyNit AS VARCHAR(50),
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
/* -------------------- DECLARACION DE VARIABLES -------------------- */

		--Tabla temporal de ids de la tabla PhysicalAsset que vienen desde el xml
		DECLARE @TableIds TABLE ( PhysicalAssetId INT )

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

		--Datos necesarios para la contabilización
		DECLARE 
			@Form AS VARCHAR(200) = 'LeasingContractsFinalization',			
			@ThirdPartyIdDIAN AS INT,
			@errors VARCHAR(MAX),
			@IdJournalVoucher AS INT,
			@JournalVoucherXML AS XML

		--Se obtienen los ids
		INSERT INTO @TableIds
			SELECT t.x.value('PhysicalAssetId[1]','int') AS PhysicalAssetId
			FROM @Xml.nodes('/TableIds') t(x)
		
/* ------------------------- VALIDACIONES --------------------------- */

		--Se valida que exista parámetros de activos fijos
		IF NOT EXISTS (SELECT Id FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId)
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de activo fijo para la unidad operativa seleccionada' AS Message, '' AS Consecutive
			RETURN
		END
		
		--Valido que el mes este abierto
		IF NOT EXISTS (SELECT Id FROM [GeneralLedger].[ClosedMonth] WHERE [Year] = @Year AND [Month] = @Month AND Status = 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'El mes ' + CAST(@Month AS VARCHAR(2)) + ' no se encuentra abierto' AS Message, '' AS Consecutive
			RETURN
		END

		--Se valida que el tercero de la dian exista
		IF NOT EXISTS (SELECT IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId)
		BEGIN
			SELECT 999 AS CodeMessage, 'No se ha parametrizado tercero de la DIAN para la unidad operativa' AS Message, '' AS Consecutive
			RETURN
		END

		--Se valida que se haya parametrizado al menos un libro contable en viebot
		IF NOT EXISTS (SELECT Id FROM GeneralLedger.VieBot WHERE Form = @Form AND Allow = 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'No se ha parametrizado un libro contable en VieBot' AS Message, '' AS Consecutive
			RETURN
		END

		--Los libros contables parametrizados deben ser no homologables
		IF EXISTS (SELECT Id FROM GeneralLedger.VieBot WHERE Form = @Form AND Allow = 1 AND HandlesHomologation = 1)
		BEGIN
			SELECT 999 AS CodeMessage, 'Los libros contables parametrizados en VieBot para Finalización de Contrato no deben ser homologables' AS Message, '' AS Consecutive
			RETURN
		END

		--Se valida que los activos que deprecian tengan asignado al menos un libro contable
		IF EXISTS (SELECT fapa.Id
			FROM @TableIds ti
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = ti.PhysicalAssetId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
			WHERE fapa.Depreciate = 1 AND fapadb.Id IS NULL
		)
		BEGIN
			SELECT @errors = stuff((SELECT DISTINCT N'; El activo con placa ' + fapa.Plate + ' no tiene asignado un libro contable'
				FROM @TableIds ti
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = ti.PhysicalAssetId
				LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
				WHERE fapa.Depreciate = 1 AND fapadb.Id IS NULL
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SELECT 999 AS CodeMessage, @errors AS Message, '' AS Consecutive
			RETURN
		END
		
		--Se valida si los activos ya salieron o fueron devueltos
		IF EXISTS (SELECT fapa.Id
			FROM @TableIds ti
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = ti.PhysicalAssetId
			WHERE fapa.HasOutput = 0 or fapa.OutputRefund = 0
		)
		BEGIN
			SELECT @errors = stuff((SELECT DISTINCT N'; El activo con placa ' + fapa.Plate + ' ya tiene salida o ya fue devuelto'
				FROM @TableIds ti
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = ti.PhysicalAssetId
				WHERE fapa.HasOutput = 0 or fapa.OutputRefund = 0
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SELECT 999 AS CodeMessage, @errors AS Message, '' AS Consecutive
			RETURN
		END

/* ------------------------- ASIGNACION VALORES PARA LA CONTABILIZACION --------------------------- */

		--Se obtiene el id del tercero de la DIAN
		SELECT @ThirdPartyIdDIAN = IdDian FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId

		--Se obtiene el tipo de comprobante contable
		SELECT @IdJournalVoucher = TransferJournalVoucherId FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId

		--Variables para recorrer los libros contables parametrizados en VieBot
		DECLARE @Rows INT = 1, 
				@RowId INT = 0,
				@LegalBookId INT

/* -------------------- RECORRIDO LIBROS VIEBOT -------------------- */
		WHILE @Rows > 0
		BEGIN
			--Obtengo el libro oficial
			SELECT TOP 1 @RowId = Id, @LegalBookId = LegalBookId
			FROM GeneralLedger.VieBot 
			WHERE Form = @Form AND Allow = 1 AND HandlesHomologation = 0 AND Id > @RowId
			ORDER BY Id
			
			--Obtenermos el numero de resultados, de ser 0 salimos del ciclo
			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
			BEGIN
				BREAK
			END

			/* -------------------- COMPROBANTE -------------------- */
			INSERT INTO FixedAsset.LeasingContractsEnd (OperatingUnitId, DocumentDate, CreationUser)
			VALUES (@OperatingUnitId, [Common].[GETDATE](), @CodeUser)

			DECLARE @Id int = SCOPE_IDENTITY()

			INSERT INTO FixedAsset.LeasingContractsEndDetail(LeasingContractsEndId, PhysicalAssetId, MainAccountId, Depreciate, DepreciatedValue, ResidualValue, HistoricalValue)
			SELECT 
			@Id
			,fapa.Id
			,fapa.MainAccountId
			,fapa.Depreciate
			,isnull(fapadb.DepreciatedValue, 0)
			,isnull(fapadb.ResidualValue, 0)
			,fapa.HistoricalValue
			FROM 
			@TableIds ti
			INNER JOIN 
			FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK)
			ON fapa.Id = ti.PhysicalAssetId
			INNER JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fapa.ItemId
			INNER JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
			INNER JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fal.Id = fapa.LocationId
			INNER JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fu.Id = fal.FunctionalUnitId
			left JOIN Common.Supplier s WITH (NOLOCK) ON fapa.SupplierId = s.Id
			LEFT OUTER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb WITH (NOLOCK) ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
			--Todos los activos del detalle, menos los que ya han sido devueltos o han salido
			WHERE --No se finaliza un contrato leasing de lo que ya salio o fue devuelto
			fapa.HasOutput = 0 AND fapa.OutputRefund = 0
			--Solo se finaliza el contrato a aquellos activos adquiridos por leasing
			AND fapa.AdquisitionType = 7
			
			/* -------------------- CABECERA DEL COMPROBANTE -------------------- */
			DELETE FROM @JournalVourcherTmp

			INSERT INTO @JournalVourcherTmp (Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
				VALUES (0, 0, @LegalBookId, @IdJournalVoucher, [Common].[GETDATE](), 'False', 2, 'Comprobante contable generado desde Finalización Contratos Leasing', '', @Id, @Form, 0)

			/* -------------------- DETALLE DEL COMPROBANTE -------------------- */			
			
			DELETE FROM @JournalVourcherDetailTmp

			INSERT INTO @JournalVourcherDetailTmp
				( Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue )
				/**** Reversion Contabilizacion Leasing ***/
				--Detalle Debito (Valor Depreciado) y credito (Valor Residual / Historico)
					SELECT 
						0 AS Id, 
						0 AS IdAccounting, 
						ma.Id AS IdMainAccount, 
						CASE ma.HandlesThirdParty WHEN 1 THEN IIF(fapa.IsMainAccount = 1, s.IdThirdParty, @ThirdPartyIdDIAN) ELSE NULL END AS IdThirdParty,
						CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END AS IdCostCenter,
						ISNULL(CASE WHEN fapa.Depreciate = 1 AND fapa.IsMainAccount = 0 THEN fapadb.DepreciatedValue ELSE 0 END, 0) AS DebitValue,
						ISNULL(CASE fapa.Depreciate WHEN 1 THEN IIF(fapa.IsMainAccount = 1, fapadb.ResidualValue, 0) ELSE fapa.HistoricalValue END, 0) AS CreditValue,
						'Detalle del Valor ' + IIF(fapa.Depreciate = 1, IIF(fapa.IsMainAccount = 1, 'Residual', 'Depreciado'), 'Histórico')  + ' de Finalización Contratos Leasing del Activo con placa ' + fapa.Plate AS Detail,
						NULL AS IdRetention, 
						0 AS RetentionRate, 
						0 AS BaseValue, 
						0 AS BillingValue
					FROM @TableIds ti
					JOIN
					(
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							fapa.MainAccountId, 1 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa
						UNION
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, fapa.AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							IIF(fapa.AdquisitionType = 7, faic.DepreciationLeasingAccountId, 0) MainAccountId, 0 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa 				
						JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fapa.ItemId
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
						WHERE fapa.Depreciate = 1
					) fapa ON fapa.Id = ti.PhysicalAssetId
					JOIN FixedAsset.FixedAssetLocation fal ON fal.Id = fapa.LocationId
					JOIN Payroll.FunctionalUnit fu ON fu.Id = fal.FunctionalUnitId
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = fapa.MainAccountId
					left join Common.Supplier s ON fapa.SupplierId = s.Id
					LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
					--Todos los activos del detalle, menos los que ya han sido devueltos o han salido
					WHERE --No se finaliza un contrato leasing de lo que ya salio o fue devuelto
						fapa.HasOutput = 0 AND fapa.OutputRefund = 0
						--Solo se finaliza el contrato a aquellos activos adquiridos por leasing
						AND fapa.AdquisitionType = 7
				UNION
				/**** Insersión valores activo propio ***/
				--Detalle Debito (Valor Residual / Historico) y credito (Valor Depreciado)
					SELECT 
						0 AS Id, 
						0 AS IdAccounting, 
						ma.Id AS IdMainAccount, 
						CASE ma.HandlesThirdParty WHEN 1 THEN IIF(fapa.IsMainAccount = 1, s.IdThirdParty, @ThirdPartyIdDIAN) ELSE NULL END AS IdThirdParty,
						CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END AS IdCostCenter,
						ISNULL(CASE fapa.Depreciate WHEN 1 THEN IIF(fapa.IsMainAccount = 1, fapadb.ResidualValue, 0) ELSE fapa.HistoricalValue END, 0) AS DebitValue,
						ISNULL(CASE WHEN fapa.Depreciate = 1 AND fapa.IsMainAccount = 0 THEN fapadb.DepreciatedValue ELSE 0 END, 0) AS CreditValue,						
						'Detalle del Valor ' + IIF(fapa.Depreciate = 1, IIF(fapa.IsMainAccount = 1, 'Residual', 'Depreciado'), 'Histórico')  + ' de Finalización Contratos Leasing del Activo con placa ' + fapa.Plate AS Detail,
						NULL AS IdRetention, 
						0 AS RetentionRate, 
						0 AS BaseValue, 
						0 AS BillingValue
					FROM @TableIds ti
					JOIN 
					(
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, IIF(fapa.AdquisitionType = 7, 6, 0) AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							IIF(fapa.AdquisitionType = 7, faic.IncomeAccountId, 0) MainAccountId, 1 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa
						JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fapa.ItemId
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
						UNION
						SELECT fapa.Id, fapa.Plate, fapa.LocationId, fapa.SupplierId, IIF(fapa.AdquisitionType = 7, 6, 0) AdquisitionType, fapa.Depreciate, fapa.HistoricalValue, 
							IIF(fapa.AdquisitionType = 7, faic.DepreciationAccountId, 0) AS MainAccountId, 0 IsMainAccount, fapa.HasOutput, fapa.OutputRefund
						FROM FixedAsset.FixedAssetPhysicalAsset fapa 				
						JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fapa.ItemId
						JOIN FixedAsset.FixedAssetItemCatalog faic ON faic.Id = fai.ItemCatalogId
						WHERE fapa.Depreciate = 1
					) fapa ON fapa.Id = ti.PhysicalAssetId
					JOIN FixedAsset.FixedAssetLocation fal ON fal.Id = fapa.LocationId
					JOIN Payroll.FunctionalUnit fu ON fu.Id = fal.FunctionalUnitId
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = fapa.MainAccountId
					left join Common.Supplier s ON fapa.SupplierId = s.Id
					LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
					WHERE --No se finaliza un contrato leasing de lo que ya salio o fue devuelto
						fapa.HasOutput = 0 AND fapa.OutputRefund = 0
						--Solo se finaliza el contrato a aquellos activos adquiridos por leasing
						AND fapa.AdquisitionType = 6

			--Eliminamos aquellos detalles donde no se ha depreciado o se ha depreciado todo el activo
			DELETE FROM @JournalVourcherDetailTmp WHERE DebitValue = 0 AND CreditValue = 0

			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			SELECT @JournalVoucherXML = convert(xml, (
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

			--Se consume el sp que guarda el comprobante contable
			INSERT @resultJournalVoucher EXEC GeneralLedger.SP_ProcessJournalVoucherMovement @JournalVoucherXML, @CodeUser

			--Se valida que no hayan errores en el guardado del comprobante contable
			IF EXISTS (SELECT * FROM @resultJournalVoucher WHERE code = '999')
			BEGIN							
				SELECT @errors = MessageResult FROM @resultJournalVoucher WHERE code = '999'
				SELECT 999 AS CodeMessage, @errors AS Message, '' AS Consecutive
				RETURN
			END
		END

		--Verificamos que se haya contabilizado
		IF NOT EXISTS (SELECT r.IdJournalVoucher FROM @resultJournalVoucher r)
		BEGIN
			SELECT 999 AS CodeMessage, 'No se genero ningun comprobante contable' AS Message
			RETURN
		END
		
		--Se actualiza el campo AdquisitionType de la tabla FixedAssetPhysicalAsset
		UPDATE pa 
			SET pa.AdquisitionType = 6, 
				pa.MainAccountId = ic.IncomeAccountId,
				pa.HasReclassified = 1,
				pa.EndDateLeasingExecuted = [Common].[GETDATE]()
		FROM @TableIds tXml
		JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = tXml.PhysicalAssetId
		JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
		JOIN FixedAsset.FixedAssetItemCatalog ic ON ic.Id = i.ItemCatalogId
		WHERE pa.AdquisitionType = 7
		
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

		SELECT 0 AS CodeMessage, @Message AS Message, @Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() AS Message, '' AS Consecutive
	END CATCH
END