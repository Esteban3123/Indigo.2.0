
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

		-- OPTION (RECOMPILE): fuerza recompilación en tiempo de ejecución para que SQL Server
		INSERT INTO #AccountingMovementTmp
		SELECT jv.EntityId, jv.Id, jv.JournalVoucherXml
		FROM GeneralLedger.AccountingMovement jv WITH(NOLOCK)
		JOIN #RemissionDetailsTmp re WITH(NOLOCK) on re.RemissionEntranceId = jv.EntityId and jv.EntityName = 'RemissionEntrance'
		OPTION (RECOMPILE)

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

					DECLARE @AccountingMovementId as INT
					if @RemissionEntranceId <> @PreviousRemissionEntranceId BEGIN

						SELECT TOP 1 @AccountingMovementId = Id
						FROM #AccountingMovementTmp
						WHERE EntityId = @RemissionEntranceId

						if (SELECT count(IdAccounting) from @JournalVoucherDetailOrigin where IdAccounting = @AccountingMovementId)=0
						BEGIN
							DELETE from @JournalVoucherDetailOrigin

							SELECT @JournalVoucherXml = JournalVoucherXml
							FROM #AccountingMovementTmp
							WHERE EntityId = @RemissionEntranceId

							INSERT INTO @JournalVoucherDetailOrigin
								(Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter,
								 DebitValue, CreditValue, Detail, IdRetention, RetentionRate,
								 BaseValue, BillingValue)
							SELECT
								t.x.value('(Id/text())[1]', 'INT'),
								@AccountingMovementId,
								t.x.value('(IdMainAccount/text())[1]', 'INT'),
								t.x.value('(IdThirdParty/text())[1]', 'INT'),
								t.x.value('(IdCostCenter/text())[1]', 'INT'),
								t.x.value('(DebitValue/text())[1]', 'DECIMAL(20,4)'),
								t.x.value('(CreditValue/text())[1]', 'DECIMAL(20,4)'),
								t.x.value('(Detail/text())[1]', 'VARCHAR(500)'),
								t.x.value('(IdRetention/text())[1]', 'INT'),
								t.x.value('(RetentionRate/text())[1]', 'DECIMAL(5,3)'),
								t.x.value('(BaseValue/text())[1]', 'DECIMAL(18,2)'),
								t.x.value('(BillingValue/text())[1]', 'DECIMAL(18,2)')
							FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
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

		--Se genera el mensaje a devolver
		DECLARE @Message varchar(max) = 'Se confirmó correctamente el comprobante de entrada con código ' + @Code
		SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
		FROM GeneralLedger.JournalVoucherTypes WHERE Id = @JournalVoucherTypeId

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
	IF OBJECT_ID('tempdb..#RemissionDetailsTmp') IS NOT NULL DROP TABLE #RemissionDetailsTmp
	IF OBJECT_ID('tempdb..#AccountingMovementTmp') IS NOT NULL DROP TABLE #AccountingMovementTmp
END

GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable de reclasificación para una remisión de entrada de inventario. A partir del identificador de un comprobante de entrada (EntranceVoucher), consulta los parámetros contables configurados por unidad operativa (SettingInventory) para obtener el tipo de comprobante de reclasificación de remisión, y construye la cabecera y el detalle del asiento contable recorriendo el detalle de la remisión de entrada (RemissionEntrance, RemissionEntranceDetail, RemissionEntranceDetailBatchSerial) junto con el detalle del comprobante de entrada (EntranceVoucherDetail) donde la fuente de entrada es reclasificación (EntranceSource = 4). Valida previamente que la remisión de entrada haya contabilizado (verificando movimientos en AccountingMovement); si no existe contabilización previa, retorna sin generar el comprobante. Finalmente registra el comprobante en el libro oficial (LegalBook) dentro de GeneralLedger.JournalVouchers, consolidando débitos y créditos por cuenta contable para dejar trazabilidad del movimiento de reclasificación en la contabilidad general del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable de reclasificación de una remisión de entrada a partir de un EntranceVoucher, validando consistencia con el asiento original y sincronizando la configuración VieBot.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un EntranceVoucher con el Id recibido y su OperatingUnitId asociado; Debe existir SettingInventory para la unidad operativa con ReclassificationRemissionJournalVoucherTypeId configurado; Debe existir un libro oficial (LegalBook.OfficialBook = 1); La remisión de entrada origen debe tener un AccountingMovement previo (EntityName=''RemissionEntrance'') para que la reclasificación pueda contabilizarse; Los productos involucrados deben tener configurado ProductGroup con ReferenceInputDebitAccountId y ReferenceInputCreditAccountId; El proveedor (Supplier) debe tener IdThirdParty para alimentar el tercero en cuentas que manejan tercero', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante se genera siempre contra el libro oficial (LegalBook.OfficialBook = 1); Solo procesa detalles de EntranceVoucher con EntranceSource = 4 (origen reclasificación de remisión); El tipo de comprobante contable se toma de SettingInventory.ReclassificationRemissionJournalVoucherTypeId según OperatingUnitId del EntranceVoucher; La cabecera del comprobante se registra con EntityName=''RemissionReclassification'' y EntityId = Id del EntranceVoucher; Las cuentas débito y crédito provienen de ProductGroup (ReferenceInputDebitAccountId / ReferenceInputCreditAccountId), invertidas respecto al asiento original (la cuenta de crédito de referencia se debita y la de débito de referencia se acredita); Solo se asigna IdThirdParty al detalle cuando MainAccounts.HandlesThirdParty = 1; en caso contrario queda NULL; El detalle final se consolida agrupando por IdMainAccount, IdThirdParty y Detail, separando montos débito y crédito en filas distintas (sin cuentas duplicadas); La configuración VieBot para ''RemissionReclassification'' siempre se mantiene espejo de la de ''RemissionEntrance'' (mismo HandlesHomologation, LegalBookId y Allow); Si las cuentas del ProductGroup no coinciden con las del asiento original (ni directa ni homologadamente), no se genera comprobante alguno; El monto contabilizado por línea es @ValueWithDiscount, no Quantity * UnitValue', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reclasificación de remisión de entrada; Comprobante contable (Journal Voucher); Libro oficial contable; Comprobante de entrada de inventario; Remisión de entrada de inventario; Plan de cuentas (cuentas débito/crédito de referencia); Homologación de cuentas oficiales; Tercero/Proveedor; Grupo de productos (ProductGroup); Configuración VieBot por formulario y libro legal; Registro tributario (TaxRegistration)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe AccountingMovement previo de la remisión de entrada vinculada al EntranceVoucher con EntranceSource=4 → Retorna CodeMessage=0 con Message vacío y NO genera comprobante contable de reclasificación else Continúa con la construcción de cabecera y detalle del comprobante; si Para cada línea, las cuentas débito/crédito de referencia del ProductGroup no coinciden (directamente o vía HomologationAccount) con las cuentas registradas en el comprobante contable original de la remisión → Inserta error en @TableErrors indicando que las cuentas del producto no coinciden con las registradas en la remisión else Inserta líneas de débito y crédito en el detalle temporal usando @ValueWithDiscount; si @ReferenceDebitAccountId = @ReferenceCreditAccountId → Se exige solo 1 cuenta coincidente (NAccounts=1) else Se exigen 2 cuentas coincidentes (NAccounts=2); si ev.TaxRegistration = 1 → El valor unitario y el valor con descuento se calculan como red.TotalValue/red.Quantity else Se usa red.UnitValue para valor y (red.NetDiscount/red.Quantity) para valor con descuento; si Existen errores acumulados en @TableErrors tras el cursor → Retorna CodeMessage=999 con todos los mensajes concatenados y NO graba el comprobante else Procede a consolidar duplicados y a invocar SP_CreateAndValidateJournalVoucherMovement; si Existe configuración VieBot para Form=''RemissionEntrance'' que no tiene equivalente para Form=''RemissionReclassification'' → Inserta filas en GeneralLedger.VieBot replicando la configuración con Form=''RemissionReclassification''; si Existe configuración VieBot para Form=''RemissionReclassification'' que ya no tiene equivalente en Form=''RemissionEntrance'' → Elimina dichas filas de GeneralLedger.VieBot para mantener sincronizadas ambas formas; si El SP GeneralLedger.SP_CreateAndValidateJournalVoucherMovement devuelve code=''999'' → Retorna CodeMessage=999 con el mensaje de error proveniente del SP', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Inventory.EntranceVoucher; Inventory.SettingInventory; GeneralLedger.AccountingMovement; Inventory.RemissionEntrance; Inventory.RemissionEntranceDetail; Inventory.RemissionEntranceDetailBatchSerial; Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Inventory.ProductGroup; Common.Supplier; GeneralLedger.HomologationAccount; GeneralLedger.MainAccounts; GeneralLedger.VieBot; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByReclassificationRemissionEntrance';
-- GO
