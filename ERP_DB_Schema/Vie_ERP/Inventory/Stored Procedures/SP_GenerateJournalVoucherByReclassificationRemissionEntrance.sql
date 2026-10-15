
-- ===============================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/10/2017
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la reclasificacion remisión de entrada
-- ==============================================================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByReclassificationRemissionEntrance] 
	@Id As int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
/* -------------------- LIMPIEZA PREVENTIVA DE TABLAS TEMPORALES -------------------- */
		-- Por si quedaron de una ejecución anterior fallida
		IF OBJECT_ID('tempdb..#RemissionDetailsTmp') IS NOT NULL DROP TABLE #RemissionDetailsTmp
		IF OBJECT_ID('tempdb..#AccountingMovementTmp') IS NOT NULL DROP TABLE #AccountingMovementTmp

/* -------------------- DECLARACION DE VARIABLES -------------------- */

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		DECLARE @JournalVourcherTmp TABLE (
				Id INTEGER DEFAULT(0),
				Consecutive BIGINT DEFAULT(0),
				LegalBookId INTEGER,
				IdJournalVoucher INTEGER,
				VoucherDate VARCHAR(30),
				Imported VARCHAR(5),
				[Status] TINYINT,
				Detail VARCHAR(500),
				EntityCode VARCHAR(20),
				EntityId INTEGER,
				EntityName VARCHAR(250),
				IsClosedYear TINYINT DEFAULT(0),
				CurrencyId INT NOT NULL,
				DateTRM  DATE
			)

		--Se declara una tabla temporal para los detalles del comprobante, puede contener cuentas duplicadas
		DECLARE @JournalVourcherDetailTmp TABLE (
				Id INTEGER DEFAULT(0),
				IdAccounting INTEGER DEFAULT(0),
				IdMainAccount INTEGER,
				IdThirdParty INTEGER,
				IdCostCenter INTEGER,
				DebitValue DECIMAL(20,4) DEFAULT(0),
				CreditValue DECIMAL(20,4) DEFAULT(0),
				Detail VARCHAR(500),
				IdRetention INTEGER,
				RetentionRate DECIMAL(5,3) DEFAULT(0),
				BaseValue DECIMAL(18,2) DEFAULT(0),
				BillingValue DECIMAL(18,2) DEFAULT(0)
			)

		--Se declara una tabla temporal para los detalles del comprobante, sin cuentas duplicadas
		DECLARE @JournalVourcherDetailTmp2 TABLE (
				Id INTEGER DEFAULT(0),
				IdAccounting INTEGER DEFAULT(0),
				IdMainAccount INTEGER,
				IdThirdParty INTEGER,
				IdCostCenter INTEGER,
				DebitValue DECIMAL(20,4) DEFAULT(0),
				CreditValue DECIMAL(20,4) DEFAULT(0),
				Detail VARCHAR(500),
				IdRetention INTEGER,
				RetentionRate DECIMAL(5,3) DEFAULT(0),
				BaseValue DECIMAL(18,2) DEFAULT(0),
				BillingValue DECIMAL(18,2) DEFAULT(0)
			)

		--Tabla temporal para guardar el resultado del save del comprobante contable
		DECLARE @resultJournalVoucher TABLE (
				code VARCHAR(20),
				MessageResult VARCHAR(max),
				IdJournalVoucher INTEGER
			)

		--Variable para obtener el xml
		DECLARE @JournalVoucherXML XML

		--Datos del comprobante de entrada
		DECLARE 
			@Code VARCHAR(20),
			@OperatingUnitId INTEGER

		--Id del tipo de comprobante contable
		DECLARE 
			@LegalBookId INT,
			@JournalVoucherTypeId INT,
			@CurrencyId INT,
			@DateTRM DATE

		--Tabla temporal en donde se almacenan los errores  y poder validar
		DECLARE @TableErrors TABLE (Id INT IDENTITY(1,1) PRIMARY KEY, MessageError VARCHAR(MAX))

		DECLARE @JournalVoucherDetailOrigin Table (Id INTEGER DEFAULT(0),
													IdAccounting INTEGER DEFAULT(0),
													IdMainAccount INTEGER,
													IdThirdParty INTEGER,
													IdCostCenter INTEGER,
													DebitValue DECIMAL(20,4) DEFAULT(0),
													CreditValue DECIMAL(20,4) DEFAULT(0),
													Detail VARCHAR(500),
													IdRetention INTEGER,
													RetentionRate DECIMAL(5,3) DEFAULT(0),
													BaseValue DECIMAL(18,2) DEFAULT(0),
													BillingValue DECIMAL(18,2) DEFAULT(0))

/* -------------------- DATOS NECESARIOS PARA LA CONTABILIZACION -------------------- */

		--Se obtiene el id del libro oficial
		SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook WITH(NOLOCK) WHERE OfficialBook = 1

		--Se obtiene el codigo y la unidad operativa de la remision de entrada
		SELECT @Code = Code, @OperatingUnitId = OperatingUnitId FROM Inventory.EntranceVoucher WITH(NOLOCK) WHERE Id = @Id

		--Se obtiene el tipo de comprobante contable por unidad operativa
		SELECT @JournalVoucherTypeId = ReclassificationRemissionJournalVoucherTypeId FROM Inventory.SettingInventory WITH(NOLOCK) WHERE OperatingUnitId = @OperatingUnitId
	
/* -------------------- VERIFICACION PREVIA -------------------- */
		
		IF NOT EXISTS (
				SELECT 1
				FROM GeneralLedger.AccountingMovement jv WITH(NOLOCK)
				INNER JOIN Inventory.RemissionEntrance re WITH(NOLOCK) ON jv.EntityId = re.Id AND jv.EntityName = 'RemissionEntrance'
				INNER JOIN Inventory.RemissionEntranceDetail red WITH(NOLOCK) ON re.Id = red.RemissionEntranceId
				INNER JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH(NOLOCK) ON red.Id = redbs.RemissionEntranceDetailId
				INNER JOIN Inventory.EntranceVoucherDetail evd WITH(NOLOCK) ON redbs.Id = evd.RemissionEntranceDetailBatchSerialId AND evd.EntranceSource = 4
				WHERE evd.EntranceVoucherId = @Id) BEGIN

			--La reclasificación no contabiliza porque la remisión de entrada no contabilizó
			SELECT 0 as CodeMessage, '' as Message
			RETURN
		END
		
/* -------------------- CABECERA DEL COMPROBANTE -------------------- */
			SELECT TOP 1
				@CurrencyId =ev.CurrencyId ,
				@DateTRM = re.RemissionDate
		FROM Inventory.EntranceVoucher ev WITH(NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH(NOLOCK) on ev.Id=evd.EntranceVoucherId
		JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH(NOLOCK) on evd.RemissionEntranceDetailBatchSerialId = redbs.Id
		join Inventory.RemissionEntranceDetail red WITH(NOLOCK) on redbs.RemissionEntranceDetailId= red.Id
		JOIN Inventory.RemissionEntrance re WITH(NOLOCK) ON re.Id = red.RemissionEntranceId
		WHERE ev.Id =@Id

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
			(LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName,CurrencyId,DateTRM)
		VALUES (@LegalBookId, @JournalVoucherTypeId, [Common].[GETDATE](), 'False', 2, 'Comprobante contable de reclasificación de remisión de entrada generado desde Comprobante de Entrada', @Code, @Id, 'RemissionReclassification',@CurrencyId,
				@DateTRM)

/* -------------------- DETALLE DEL COMPROBANTE -------------------- */
		DECLARE @PreviousRemissionEntranceId as INT =0
		
		DECLARE 
			@RemissionEntranceId INT,
			@RemissionEntranceDetailId INT,
			@RemissionEntranceDetailBatchSerialId INT,
			@ReferenceDebitAccountId INT,
			@ReferenceCreditAccountId INT,
			@ThirdPartyId INT,
			@Quantity INTEGER,
			@Value DECIMAL(20,4),
			@ValueWithDiscount DECIMAL(18,2)


		CREATE TABLE #RemissionDetailsTmp
		(
			RemissionEntranceId INT,
			RemissionEntranceDetailId INT,
			RemissionEntranceDetailBatchSerialId INT,
			ReferenceDebitAccountId INT,
			ReferenceCreditAccountId INT,
			ThirdPartyId INT,
			Quantity INTEGER,
			Value DECIMAL(20,4),
			ValueWithDiscount DECIMAL(18,2)
		)

		INSERT INTO #RemissionDetailsTmp
		SELECT DISTINCT		red.RemissionEntranceId, 
							red.RemissionEntranceDetailId,
							redbs.Id, 
							pg.ReferenceInputDebitAccountId,
							pg.ReferenceInputCreditAccountId,
							s.IdThirdParty,
							evd.Quantity,
							iif(ev.TaxRegistration=1,(red.TotalValue/red.Quantity),red.UnitValue ),
							iif(ev.TaxRegistration=1,(red.TotalValue/red.Quantity),(red.NetDiscount/red.Quantity ))*evd.Quantity
			FROM Inventory.EntranceVoucher ev WITH(NOLOCK)
			INNER JOIN Inventory.EntranceVoucherDetail evd WITH(NOLOCK) ON ev.Id = evd.EntranceVoucherId
			INNER JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH(NOLOCK) ON evd.RemissionEntranceDetailBatchSerialId = redbs.Id
			JOIN (	SELECT	re.Id RemissionEntranceId,
							red.Id RemissionEntranceDetailId,
							SUM(red.TotalValue) TotalValue,
							SUM(red.GrossUnitValue) UnitValue,
							sum(red.NetDiscount) NetDiscount,
							sum(red.Quantity) Quantity,
							red.ProductId,
							re.SupplierId
					FROM Inventory.RemissionEntrance re WITH(NOLOCK)
					JOIN Inventory.RemissionEntranceDetail red WITH(NOLOCK) ON re.Id= red.RemissionEntranceId
					GROUP by re.Id, red.Id,red.ProductId,re.SupplierId, red.Quantity) red on redbs.RemissionEntranceDetailId = red.RemissionEntranceDetailId
			INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON red.ProductId = ip.Id
			INNER JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
			INNER JOIN Common.Supplier s WITH(NOLOCK) ON red.SupplierId = s.Id
			WHERE ev.Id = @Id AND evd.EntranceSource = 4
			ORDER BY RED.RemissionEntranceId

			CREATE TABLE #AccountingMovementTmp
			(
				EntityId INT,
				Id INT,
				JournalVoucherXml XML,
				INDEX IX_AccountingMovementTmp_EntityId NONCLUSTERED (EntityId)
			)

			INSERT INTO #AccountingMovementTmp
			SELECT jv.EntityId, jv.Id, jv.JournalVoucherXml 
			FROM GeneralLedger.AccountingMovement jv WITH(NOLOCK)
			JOIN #RemissionDetailsTmp re WITH(NOLOCK) on re.RemissionEntranceId = jv.EntityId and jv.EntityName = 'RemissionEntrance'

		DECLARE detailJournalVoucher_cursor CURSOR FOR
		SELECT RemissionEntranceId, 
			   RemissionEntranceDetailId,
			   RemissionEntranceDetailBatchSerialId,
			   ReferenceDebitAccountId,
			   ReferenceCreditAccountId,
			   ThirdPartyId,
			   Quantity,
			   Value,
			   ValueWithDiscount
		FROM #RemissionDetailsTmp
	
		OPEN detailJournalVoucher_cursor  				
			FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionEntranceId, @RemissionEntranceDetailId, @RemissionEntranceDetailBatchSerialId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value, @ValueWithDiscount
  
			WHILE @@FETCH_STATUS = 0  
			BEGIN
				IF (SELECT COUNT(Id) FROM #AccountingMovementTmp WHERE EntityId = @RemissionEntranceId) > 0 BEGIN			
					
					DECLARE @JournalVoucherXmlOrigin as XML= NULL
					DECLARE @AccountingMovementId as INT
					if @RemissionEntranceId <> @PreviousRemissionEntranceId BEGIN

						SELECT TOP 1 @JournalVoucherXmlOrigin= JournalVoucherXml, @AccountingMovementId = Id
						FROM #AccountingMovementTmp
						WHERE EntityId = @RemissionEntranceId

						if (SELECT count(IdAccounting) from @JournalVoucherDetailOrigin where IdAccounting = @AccountingMovementId)=0
						BEGIN
							DELETE from @JournalVoucherDetailOrigin
					
							INSERT INTO @JournalVoucherDetailOrigin
							Select	t.x.value('IdAuto[1]', 'int'),
									(	select top 1 Id 
										FROM #AccountingMovementTmp
										WHERE EntityId = @RemissionEntranceId) IdAccounting,
									t.x.value('IdMainAccount[1]', 'int'),
									t.x.value('IdThirdParty[1]', 'Int'),
									t.x.value('IdCostCenter[1]', 'Int'),
									t.x.value('DebitValue[1]', ' Decimal(18, 2)'),
									t.x.value('CreditValue[1]', ' Decimal(18, 2)'),
									t.x.value('Detail[1]', 'varchar(500)'),
									t.x.value('IdRetention[1]', 'int'),
									t.x.value('RetentionRate[1]', 'Decimal(5, 3)'),
									t.x.value('BaseValue[1]', ' Decimal(18, 2)'),
									t.x.value('BillingValue[1]', ' Decimal(18, 2)')
							From @JournalVoucherXmlOrigin.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
						end
						set @PreviousRemissionEntranceId = @RemissionEntranceId
					end

					--Valido que las cuentas se encuentre en la contabilización de la remisión					
					DECLARE @NAccounts INTEGER = IIF(@ReferenceDebitAccountId = @ReferenceCreditAccountId, 1, 2)
					IF ISNULL((
						SELECT COUNT(jvd.IdMainAccount)
						FROM
						(
							SELECT ISNULL(ha.OfficialMainAccountId, jvd.IdMainAccount) IdMainAccount
							FROM #AccountingMovementTmp jv
							INNER JOIN @JournalVoucherDetailOrigin jvd  ON jv.Id = jvd.IdAccounting
							LEFT JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) on ha.MainAccountId = jvd.IdMainAccount
							WHERE jv.EntityId = @RemissionEntranceId
								AND 
								(
									jvd.IdMainAccount = @ReferenceDebitAccountId 
									OR 
									jvd.IdMainAccount = @ReferenceCreditAccountId
									OR 
									(jvd.IdMainAccount = ha.MainAccountId AND (ha.OfficialMainAccountId = @ReferenceDebitAccountId OR ha.OfficialMainAccountId = @ReferenceCreditAccountId))
								)
							GROUP BY ISNULL(ha.OfficialMainAccountId, jvd.IdMainAccount)
						) jvd
					), 0) <> @NAccounts BEGIN
						INSERT INTO @TableErrors (MessageError)
							SELECT DISTINCT 'Las cuentas contables del producto "' + ip.Code + ' - ' + ip.Name + '" no son las mismas que fueron registradas en la remision'
							FROM Inventory.RemissionEntranceDetail red WITH(NOLOCK)
							INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON red.ProductId = ip.Id
							WHERE red.Id = @RemissionEntranceDetailId;
					END
					ELSE BEGIN
						-- Detalle Debito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								--@Quantity * @Value,
								@ValueWithDiscount,
								'Detalle generado desde el Comprobante de Entrada ' + @Code
							FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
							WHERE ma.Id = @ReferenceCreditAccountId

						-- Detalle Credito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, CreditValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								--@Quantity * @Value,
								@ValueWithDiscount,
								'Detalle generado desde el Comprobante de Entrada ' + @Code
							FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
							WHERE ma.Id = @ReferenceDebitAccountId
					END					
				END

				FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionEntranceId, @RemissionEntranceDetailId, @RemissionEntranceDetailBatchSerialId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value, @ValueWithDiscount
			END

		CLOSE detailJournalVoucher_cursor 
		DEALLOCATE detailJournalVoucher_cursor

/* -------------------- VALIDACIONES -------------------- */
		
		/** --------- VALIDO SI HUBO ERRORES --------- **/
		IF (SELECT COUNT(*) FROM @TableErrors) > 0 BEGIN
			DECLARE @MessageError VARCHAR(MAX) = STUFF((SELECT N'' + MessageError + CHAR(13) + CHAR(10) FROM @TableErrors FOR xml path(N''), type).value(N'.[1]', N'NVARCHAR(MAX)'), 1,0, N'')			
			SELECT 999 AS CodeMessage, @MessageError AS Message
			RETURN
		END
		
		--SE ELIMINAN CUENTAS DUPLICADAS
		INSERT INTO @JournalVourcherDetailTmp2 (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
			--Unificamos el detalle Debito
			SELECT IdMainAccount, IdThirdParty, SUM(DebitValue), 0, Detail
			FROM @JournalVourcherDetailTmp
			WHERE ISNULL(DebitValue, 0) <> 0
			GROUP BY IdMainAccount, IdThirdParty, Detail
			UNION
			--Unificamos el detalle Credito
			SELECT IdMainAccount, IdThirdParty, 0, SUM(CreditValue), Detail
			FROM @JournalVourcherDetailTmp
			WHERE ISNULL(CreditValue, 0) <> 0
			GROUP BY IdMainAccount, IdThirdParty, Detail

/* -------------------- VIEBOT -------------------- */
		
		--Configuramos viebot para que contabilice en los libros en los cuales contabiliza la remisión
		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'RemissionReclassification' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'RemissionEntrance' AND vb2.Id IS NULL) > 0 BEGIN

			INSERT INTO [GeneralLedger].[VieBot] (Form, HandlesHomologation, LegalBookId, Allow, CreationUser, CreationDate, ModificationUser, ModificationDate)
				SELECT 'RemissionReclassification', vb.HandlesHomologation, vb.LegalBookId, vb.Allow, vb.CreationUser, vb.CreationDate, vb.ModificationUser, vb.ModificationDate
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'RemissionReclassification' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'RemissionEntrance' AND vb2.Id IS NULL
		END

		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'RemissionEntrance' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'RemissionReclassification' AND vb2.Id IS NULL) > 0 BEGIN

			DELETE vb 
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2
					ON vb2.Form = 'RemissionEntrance' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				where vb.Form = 'RemissionReclassification' AND vb2.Id IS NULL
		END		

/* -------------------- GENERACIÓN DEL COMPROBANTE CONTABLE -------------------- */

		SELECT @JournalVoucherXML =  convert(xml, (select * from @JournalVourcherTmp JournalVoucher 
		inner join @JournalVourcherDetailTmp2 JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

		--Se consume el sp que guarda el comprobante contable
		INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

		--Se valida que no hayan errores en el guardado del comprobante contable
		IF (SELECT code FROM @resultJournalVoucher) = '999' BEGIN
			DECLARE @errorJV VARCHAR(max)
			SELECT @errorJV = MessageResult FROM @resultJournalVoucher 
			SELECT 999 AS CodeMessage, @errorJV AS Message
			RETURN
		END

		--Se obtiene el id que genero el comprobante contable
		DECLARE @JournalVoucherId AS INT = (SELECT IdJournalVoucher FROM @resultJournalVoucher)

		--Se obtiene el consecutivo que generó el comprobante contable
		DECLARE @Consecutive AS VARCHAR(MAX) = ISNULL((SELECT CAST(Consecutive AS VARCHAR(MAX)) FROM GeneralLedger.JournalVouchers WITH(NOLOCK) WHERE id = @JournalVoucherId), '0')

		--Se genera el mensaje a devolver
		DECLARE @Message varchar(max) = 'Se confirmó correctamente el comprobante de entrada con código ' + @Code
		SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable tipo ' + Code + ' - ' + Name + ' con consecutivo ' + @Consecutive 
		FROM GeneralLedger.JournalVoucherTypes WITH(NOLOCK) WHERE Id = @JournalVoucherTypeId

		SELECT 0 AS CodeMessage, @Message AS Message

	END TRY
	BEGIN CATCH
		IF CURSOR_STATUS('global','detailJournalVoucher_cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','detailJournalVoucher_cursor') > -1 BEGIN
			CLOSE detailJournalVoucher_cursor
		  END
		 DEALLOCATE detailJournalVoucher_cursor
		END
		SELECT 999 as CodeMessage, ERROR_MESSAGE() + ' - ' + cast(ERROR_LINE() AS VARCHAR(10)) AS Message

	END CATCH
	-- NOTA: La limpieza de tablas temporales se hace al inicio del procedimiento (limpieza preventiva)
	-- y después del cursor (liberación de memoria). No es necesario limpiar aquí.
END