-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2017-11-28
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la devolución de las remisiones
-- ==============================================================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByRemissionDevolution] 
	@Id AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
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
				CurrencyId INTEGER
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

		--Datos de la devolución
		DECLARE 
			@Code VARCHAR(20),
			@OperatingUnitId INTEGER,
			@DevolutionType TINYINT,
			@DevolutionTypeName VARCHAR(250),			
			@RemissionId INTEGER,
			@RemissionName VARCHAR(250),
			@Detail VARCHAR(MAX),
			@CurrencyId INTEGER,
			@OfficialCurrencyId INTEGER,
			@CurrencyConverterWithDate DECIMAL(21,5),
			@documentDate as DATE,
			@RemissionOfficialCurrencyValue  NUMERIC(20,2),
			@ValueKardex  NUMERIC(20,2)
			
		--Id del tipo de comprobante contable
		DECLARE 
			@LegalBookId INT,
			@JournalVoucherTypeId INT

		--Tabla temporal en donde se almacenan los errores  y poder validar
		DECLARE @TableErrors TABLE (Id INT IDENTITY(1,1) PRIMARY KEY, MessageError VARCHAR(MAX))

/**********************Tabla TEMP: --VALIDACION COMPROBANTE ORIGINAL DE LA REMISION-- *********************/

		DECLARE @JournalVoucherOriginRemision AS TABLE (JournalVoucherId INT,
														LegalBookId INT,
														CreditAccountId INT,
														DebitAccountId INT)

/* -------------------- DATOS NECESARIOS PARA LA CONTABILIZACION -------------------- */

		--Se obtiene el id del libro oficial
		SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook  WHERE OfficialBook = 1

		--Se obtiene el codigo y la unidad operativa de la devolución
		SELECT 
			@Code = Code, @OperatingUnitId = OperatingUnitId, 
			@DevolutionType = DevolutionType, 
			@RemissionId = IIF(DevolutionType = 1, RemissionEntranceId, IIF(DevolutionType = 2, RemissionOutputId, ConsignmentInventoryRemissionId)) 
		FROM Inventory.RemissionDevolution  WHERE Id = @Id

		--Se obtiene el tipo de comprobante contable de acuerdo al tipo de devolucion por unidad operativa
		SELECT 
			@JournalVoucherTypeId = IIF(@DevolutionType = 1, RemissionEntranceDevolutionJournalVoucherTypeId, IIF(@DevolutionType = 2, RemissionOutputDevolutionJournalVoucherTypeId, ConsignmentMerchandiseDevolutionJournalVoucherTypeId)) 
		FROM Inventory.SettingInventory 
		WHERE OperatingUnitId = @OperatingUnitId

		--se obtiene el id de la moneda de la remision de entrada
		SELECT @CurrencyId = COALESCE(re.CurrencyId,cir.CurrencyId,@OfficialCurrencyId) ,
			   @documentDate = COALESCE(re.RemissionDate,ro.RemissionDate,cir.RemissionDate) 
		FROM Inventory.RemissionDevolution rd 
		left join Inventory.RemissionEntrance re  on re.id = rd.RemissionEntranceId and @DevolutionType = 1
		left JOIN Inventory.RemissionOutput ro  on ro.Id = rd.RemissionOutputId and @DevolutionType = 2
		left join Inventory.ConsignmentInventoryRemission cir  on cir.Id = rd.ConsignmentInventoryRemissionId and @DevolutionType = 3
		where rd.id = @Id 

		-- se obtiene la moneda oficial
		set @OfficialCurrencyId = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings  )

/* -------------------- VERIFICACION PREVIA -------------------- */

		--Solo contabilizo si la remisión contabilizó
		IF @DevolutionType = 1 BEGIN
			IF ISNULL((
					SELECT COUNT(jv.Id) 
					FROM GeneralLedger.AccountingMovement jv 
					INNER JOIN Inventory.RemissionEntrance re  ON jv.EntityId = re.Id AND jv.EntityName = 'RemissionEntrance' 
					INNER JOIN Inventory.RemissionDevolution rd  ON re.Id = rd.RemissionEntranceId
					WHERE rd.Id = @Id), 0) = 0 BEGIN

				--La devolución no contabiliza porque la remisión de entrada no contabilizó
				SELECT 0 as CodeMessage, '' as Message
				return
			END

			SET @Detail = 'Comprobante contable generado desde la Devolución de la Remisión de Entrada'
			SET @RemissionName = 'RemissionEntrance'
			SET @DevolutionTypeName = @RemissionName + 'Devolution'
		END
		ELSE IF @DevolutionType = 2 BEGIN
			IF ISNULL((
					SELECT COUNT(jv.Id) 
					FROM GeneralLedger.AccountingMovement jv 
					INNER JOIN Inventory.RemissionOutput ro  ON jv.EntityId = ro.Id AND jv.EntityName = 'RemissionOutput' 
					INNER JOIN Inventory.RemissionDevolution rd  ON ro.Id = rd.RemissionOutputId
					WHERE rd.Id = @Id), 0) = 0 BEGIN

				--La devolución no contabiliza porque la remisión de salida no contabilizó
				SELECT 0 as CodeMessage, '' as Message
				RETURN
			END

			SET @Detail = 'Comprobante contable generado desde la Devolución de la Remisión de Salida'
			SET @RemissionName = 'RemissionOutput'
			SET @DevolutionTypeName = @RemissionName + 'Devolution'
		END
		ELSE IF @DevolutionType = 3 BEGIN
			IF ISNULL((
					SELECT COUNT(jv.Id) 
					FROM GeneralLedger.AccountingMovement jv  
					INNER JOIN Inventory.ConsignmentInventoryRemission re  ON jv.EntityId = re.Id AND jv.EntityName = 'ConsignmentInventoryRemission' 
					INNER JOIN Inventory.RemissionDevolution rd  ON re.Id = rd.ConsignmentInventoryRemissionId
					WHERE rd.Id = @Id), 0) = 0 BEGIN

				--La devolución no contabiliza porque la remisión de entrada no contabilizó
				SELECT 0 as CodeMessage, '' as Message
				return
			END

			SET @Detail = 'Comprobante contable generado desde la Devolución de la Remisión de Inventario en Consignación'
			SET @RemissionName = 'ConsignmentInventoryRemission'
			SET @DevolutionTypeName = @RemissionName + 'Devolution'
		END
		ELSE BEGIN
			SELECT 999 as CodeMessage, 'Tipo de devolución no controlada' as Message
			RETURN
		END
		
		--Debe tener parametrizado el tipo de comprobante contable
		IF (ISNULL(@JournalVoucherTypeId, 0) = 0) BEGIN
			SELECT 999 AS CodeMessage, 'Debe parametrizar el Tipo de Comprobante para la devolución de la remisión de ' + IIF(@DevolutionType = 1, 'Entrada', IIF(@DevolutionType = 2, 'Salida', 'Inventario en Consignación')) AS Message
			RETURN
		END

/********************************************************************************************************/
	---SE INSERTA LOS REGISTROS DEL COMPROBANTE CONTABLE ORIGINAL DE LA REMISION DEL LIBRO OFICIAL

		INSERT INTO @JournalVoucherOriginRemision( JournalVoucherId,LegalBookId,CreditAccountId,DebitAccountId)
		SELECT	subjv.Id,subjv.LegalBookId,subjv.CreditAccountId,subjv.DebitAccountId
				FROM (	SELECT	jv.Id,
								jv.LegalBookId,
								IIF(jvd.CreditValue>0,jvd.IdMainAccount,NULL) CreditAccountId,
								IIF(jvd.DebitValue>0,jvd.IdMainAccount,NULL) DebitAccountId
						FROM GeneralLedger.JournalVouchers jv 
						INNER JOIN GeneralLedger.JournalVoucherDetails jvd  ON jv.Id = jvd.IdAccounting
						INNER JOIN GeneralLedger.LegalBook l  on jv.LegalBookId = l.Id and l.OfficialBook=1 --LIBRO OFICIAL
						WHERE jv.EntityId = @RemissionId AND jv.EntityName = @RemissionName AND jv.Status = 2) subjv
				GROUP by subjv.Id,subjv.LegalBookId,subjv.CreditAccountId,subjv.DebitAccountId

/********************************************************************************************************/

/* -------------------- CABECERA DEL COMPROBANTE -------------------- */
		
		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
			(LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, CurrencyId)
		VALUES (@LegalBookId, @JournalVoucherTypeId, [Common].[GETDATE](), 'False', 2, @Detail, @Code, @Id, @DevolutionTypeName, isnull(@CurrencyId,@OfficialCurrencyId))

/* -------------------- DETALLE DEL COMPROBANTE -------------------- */
		
		DECLARE 
			@RemissionDevolutionDetailId INT,
			@ReferenceDebitAccountId INT,
			@ReferenceCreditAccountId INT,
			@ThirdPartyId INT,
			@Quantity INTEGER,
			@Value DECIMAL(20,4)

/* -------------------- variable cuando es tipo reposicion -------------------- */
		declare 
			@ConsignmentInventoryRemissionDetailId INT,
			@remisionType TINYINT,
            @Batchserial INT = NULL

	/* -------------------- SI ES UNA DEVOLUCIÓN DE UNA REMISION DE ENTRADA -------------------- */
		IF @DevolutionType = 1 BEGIN
			DECLARE detailJournalVoucher_cursor CURSOR LOCAL FOR
				SELECT DISTINCT rdd.Id, pg.ReferenceInputDebitAccountId, pg.ReferenceInputCreditAccountId, s.IdThirdParty, rdd.Quantity,IIF(re.CurrencyId <> @OfficialCurrencyId,red.UnitValue,k.Value) 				   
				FROM Inventory.RemissionDevolution rd 
				INNER JOIN Inventory.RemissionDevolutionDetail rdd  ON rd.Id = rdd.RemissionDevolutionId
				INNER JOIN Inventory.RemissionEntranceDetailBatchSerial redbs  ON rdd.RemissionEntranceDetailBatchSerialId = redbs.Id
				INNER JOIN Inventory.RemissionEntranceDetail red  ON redbs.RemissionEntranceDetailId = red.Id
				INNER JOIN Inventory.RemissionEntrance re  ON red.RemissionEntranceId = re.Id
				INNER JOIN
				(
					SELECT k.EntityId, k.EntityName, k.ProductId, ROUND(SUM(k.Quantity * k.Value) / SUM(k.Quantity), 2) AS Value
					FROM Inventory.Kardex k WITH (NOLOCK)
					GROUP BY k.EntityId, k.EntityName, k.ProductId
				) k ON re.Id = k.EntityId AND k.EntityName = @RemissionName AND red.ProductId = k.ProductId
				INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
				INNER JOIN Inventory.ProductGroup pg  ON ip.ProductGroupId = pg.Id
				INNER JOIN Common.Supplier s  ON re.SupplierId = s.Id
				WHERE rd.Id = @Id
	
			OPEN detailJournalVoucher_cursor  				
				FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
  
				WHILE @@FETCH_STATUS = 0  
				BEGIN					
					--Valido que las cuentas sean las que estan en la remisión
					DECLARE @NAccountsIn INTEGER = IIF(@ReferenceDebitAccountId = @ReferenceCreditAccountId, 1, 2)
					IF ISNULL((
						SELECT COUNT(jvd.IdMainAccount)
						FROM
						(
							SELECT ISNULL(ha.OfficialMainAccountId, jvd.IdMainAccount) IdMainAccount
							FROM GeneralLedger.JournalVouchers jv 
							INNER JOIN GeneralLedger.JournalVoucherDetails jvd  ON jv.Id = jvd.IdAccounting
							LEFT JOIN GeneralLedger.HomologationAccount ha  on ha.MainAccountId = jvd.IdMainAccount
							WHERE jv.EntityId = @RemissionId AND jv.EntityName = @RemissionName AND jv.Status = 2
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
					), 0) <> @NAccountsIn BEGIN
						INSERT INTO @TableErrors (MessageError)
							SELECT DISTINCT 'Las cuentas contables del producto "' + ip.Code + ' - ' + ip.Name + '" no son las mismas que fueron registradas en la remision'
							FROM Inventory.RemissionDevolutionDetail rdd 
							INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
							WHERE rdd.Id = @RemissionDevolutionDetailId;
					END
					ELSE BEGIN
						-- Detalle Debito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Entrada '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceCreditAccountId

						-- Detalle Credito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, CreditValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Entrada '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceDebitAccountId
					END

					FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
				END

			CLOSE detailJournalVoucher_cursor 
			DEALLOCATE detailJournalVoucher_cursor
		END
	/* -------------------- SI ES UNA DEVOLUCIÓN DE UNA REMISION DE SALIDA -------------------- */
		ELSE IF @DevolutionType = 2 BEGIN
			DECLARE detailJournalVoucher_cursor CURSOR LOCAL FOR
				SELECT DISTINCT rdd.Id, pg.ReferenceOutputDebitAccountId, pg.ReferenceOutputCreditAccountId, c.ThirdPartyId, rdd.Quantity, k.Value
				FROM Inventory.RemissionDevolution rd 
				INNER JOIN Inventory.RemissionDevolutionDetail rdd  ON rd.Id = rdd.RemissionDevolutionId
				INNER JOIN Inventory.RemissionOutput ro  ON rd.RemissionOutputId = ro.Id
				INNER JOIN Inventory.RemissionOutputDetail rod   ON ro.Id = rod.RemissionOutputId AND rdd.ProductId = rod.ProductId
				INNER JOIN Inventory.Kardex k  ON ro.Id = k.EntityId AND k.EntityName = @RemissionName AND rdd.ProductId = k.ProductId
				INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
				INNER JOIN Inventory.ProductGroup pg  ON ip.ProductGroupId = pg.Id
				INNER JOIN Common.Customer c  ON ro.CustomerId = c.Id
				WHERE rd.Id = @Id
	
			OPEN detailJournalVoucher_cursor  				
				FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
  
				WHILE @@FETCH_STATUS = 0  
				BEGIN
					--Valido que las cuentas sean las que estan en la remisión
					DECLARE @NAccountsOut INTEGER = IIF(@ReferenceDebitAccountId = @ReferenceCreditAccountId, 1, 2)
					IF ISNULL((
						SELECT COUNT(jvd.IdMainAccount)
						FROM
						(
							SELECT ISNULL(ha.OfficialMainAccountId, jvd.IdMainAccount) IdMainAccount
							FROM GeneralLedger.JournalVouchers jv 
							INNER JOIN GeneralLedger.JournalVoucherDetails jvd  ON jv.Id = jvd.IdAccounting
							LEFT JOIN GeneralLedger.HomologationAccount ha  on ha.MainAccountId = jvd.IdMainAccount
							WHERE jv.EntityId = @RemissionId AND jv.EntityName = @RemissionName AND jv.Status = 2
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
					), 0) <> @NAccountsOut BEGIN
						INSERT INTO @TableErrors (MessageError)
							SELECT DISTINCT 'Las cuentas contables del producto "' + ip.Code + ' - ' + ip.Name + '" no son las mismas que fueron registradas en la remision'
							FROM Inventory.RemissionDevolutionDetail rdd 
							INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
							WHERE rdd.Id = @RemissionDevolutionDetailId;
					END
					ELSE BEGIN
						-- Detalle Debito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Salida '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceCreditAccountId

						-- Detalle Credito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, CreditValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Salida '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceDebitAccountId
					END

					FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
				END

			CLOSE detailJournalVoucher_cursor 
			DEALLOCATE detailJournalVoucher_cursor
		END
	/* -------------------- SI ES UNA DEVOLUCIÓN DE UNA REMISION DE INVENTARIO EN CONSIGNACION -------------------- */
		ELSE IF @DevolutionType = 3 BEGIN
			DECLARE detailJournalVoucher_cursor CURSOR LOCAL FOR
				SELECT DISTINCT
					rdd.Id, 
					pg.ConsignmentMerchandiseDebitAccountId, 
					pg.ConsignmentMerchandiseCreditAccountId, 
					s.IdThirdParty, 
					rdd.Quantity,
					IIF(re.CurrencyId <> @OfficialCurrencyId,red.UnitValue,k.Value)
				FROM Inventory.RemissionDevolution rd 
					INNER JOIN Inventory.RemissionDevolutionDetail rdd  ON rd.Id = rdd.RemissionDevolutionId
					INNER JOIN Inventory.ConsignmentInventoryRemission re  ON rd.ConsignmentInventoryRemissionId = re.Id
					INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial redb   on redb.Id = rdd.ConsignmentInventoryRemissionDetailBatchSerialId
					INNER JOIN Inventory.ConsignmentInventoryRemissionDetail red   ON re.Id = red.ConsignmentInventoryRemissionId AND rdd.ProductId = red.ProductId and redb.ConsignmentInventoryRemissionDetailId = red.id
					INNER JOIN
					(
						SELECT 
							k.EntityId,
							k.EntityName, 
							k.ProductId,
							ROUND(SUM(k.Quantity * k.Value) / SUM(k.Quantity), 4) AS Value
						FROM Inventory.Kardex k WITH (NOLOCK)
						GROUP BY k.EntityId, k.EntityName, k.ProductId
					) k ON re.Id = k.EntityId AND re.Id = k.EntityId AND k.EntityName = @RemissionName AND rdd.ProductId = k.ProductId
					INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
					INNER JOIN Inventory.ProductGroup pg  ON ip.ProductGroupId = pg.Id
					INNER JOIN Common.Supplier s  ON re.SupplierId = s.Id
				WHERE rd.Id = @Id
	
			OPEN detailJournalVoucher_cursor  				
				FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
  
				WHILE @@FETCH_STATUS = 0  
				BEGIN					

					/*---------Valido que las cuentas sean las que estan en la remisión (Libro Oficial) -------*/
					/*Por Adriana Parra se valida si solo si existe un documento contable*/
					IF EXISTS(SELECT 1 FROM @JournalVoucherOriginRemision) AND	
						(	NOT EXISTS(SELECT 1 FROM @JournalVoucherOriginRemision WHERE CreditAccountId = @ReferenceCreditAccountId ) OR
							NOT EXISTS(SELECT 1 from @JournalVoucherOriginRemision where DebitAccountId =@ReferenceDebitAccountId)
						)
					BEGIN
							INSERT INTO @TableErrors (MessageError)
							SELECT DISTINCT 'Las cuentas contables del producto "' + ip.Code + ' - ' + ip.Name + '" no son las mismas que fueron registradas en la remision'
							FROM Inventory.RemissionDevolutionDetail rdd 
							INNER JOIN Inventory.InventoryProduct ip  ON rdd.ProductId = ip.Id
							WHERE rdd.Id = @RemissionDevolutionDetailId;
					END
					ELSE BEGIN
						-- Detalle Debito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Inventario en Consignación '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceCreditAccountId

						-- Detalle Credito
						INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, CreditValue, Detail)
							SELECT 
								ma.Id,
								CASE ma.HandlesThirdParty WHEN 1 THEN @thirdPartyId ELSE NULL END,
								@Quantity * @Value,
								'Detalle generado desde la devolución de la remisión de Inventario en Consignación '
							FROM GeneralLedger.MainAccounts ma 
							WHERE ma.Id = @ReferenceDebitAccountId

						--sacamos el id de los detalles de la remision que fueron usadasa en la remision que estamos devolviendo
						SELECT 
							@ConsignmentInventoryRemissionDetailId = cird.ConsignmentInventoryRemissionDetailId,
							@remisionType = cir.MovementType,
                            @Batchserial = cirdbs.BatchSerialId		
						FROM Inventory.RemissionDevolutionDetail rdd 
						JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs  on cirdbs.id = rdd.ConsignmentInventoryRemissionDetailBatchSerialId
						join Inventory.ConsignmentInventoryRemissionDetail cird  on cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
						JOIN Inventory.ConsignmentInventoryRemission cir  on cir.id = cird.ConsignmentInventoryRemissionId
						where rdd.id = @RemissionDevolutionDetailId

						-- si es de tipo reposicion actualizamos la columna ReplacementQuantity
						if @remisionType = 3
						BEGIN
							-- si las cantidades que se repusieron son menores a las devueltas
							if (SELECT cirdbs.ReplacementQuantity
								FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs 
								where cirdbs.ConsignmentInventoryRemissionDetailId = @ConsignmentInventoryRemissionDetailId AND (
                                                                                cirdbs.BatchSerialId = @BatchSerial
                                                                                OR (cirdbs.BatchSerialId IS NULL AND @BatchSerial IS NULL)
                                                                          )) < @Quantity 
							BEGIN
								SELECT 999 AS CodeMessage, 'La cantidad a devolver es mayor a la cantidad que ya se ha repuesto.' AS Message
								RETURN
							END
							ELSE BEGIN
								--actualizamos el campo para habilitar esas remisiones usadas en la remision que estamos devolviendo
								UPDATE Inventory.ConsignmentInventoryRemissionDetailBatchSerial  
								set ReplacementQuantity = ReplacementQuantity - @Quantity
								FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs
								where cirdbs.ConsignmentInventoryRemissionDetailId = @ConsignmentInventoryRemissionDetailId AND (
                                                                                cirdbs.BatchSerialId = @BatchSerial
                                                                                OR (cirdbs.BatchSerialId IS NULL AND @BatchSerial IS NULL)
                                                                          )
							END
						END
					END
					FETCH NEXT FROM detailJournalVoucher_cursor INTO @RemissionDevolutionDetailId, @ReferenceDebitAccountId, @ReferenceCreditAccountId, @ThirdPartyId, @Quantity, @Value
				END

			CLOSE detailJournalVoucher_cursor 
			DEALLOCATE detailJournalVoucher_cursor
		END

/* -------------------- VALIDACIONES -------------------- */
		
		/** --------- VALIDO SI HUBO ERRORES EN EL REGISTRO AJUSTES CIERRE INVENTARIO --------- **/
		IF (SELECT COUNT(*) FROM @TableErrors) > 0 BEGIN
			DECLARE @MessageError VARCHAR(MAX) = STUFF((SELECT N'' + MessageError + CHAR(13) + CHAR(10) FROM @TableErrors FOR xml path(N''), type).value(N'.[1]', N'NVARCHAR(MAX)'), 1,0, N'')			
			SELECT 999 AS CodeMessage, @MessageError AS Message
			RETURN
		END

		--SE ELIMINAN CUENTAS DUPLICADAS
		INSERT INTO @JournalVourcherDetailTmp2 (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
			--Unificamos el Detalle Debito
			SELECT IdMainAccount, IdThirdParty, SUM(DebitValue), 0, Detail
			FROM @JournalVourcherDetailTmp
			WHERE ISNULL(DebitValue, 0) <> 0
			GROUP BY IdMainAccount, IdThirdParty, Detail
			UNION
			--Unificamos el Detalle Credito
			SELECT IdMainAccount, IdThirdParty, 0, SUM(CreditValue), Detail
			FROM @JournalVourcherDetailTmp
			WHERE ISNULL(CreditValue, 0) <> 0
			GROUP BY IdMainAccount, IdThirdParty, Detail

			--se valida que los movimientos sean iguales en contabilidad y en la remision cuando es una moneda diferente a la oficial
			-- y contra el kardex si es  con la moneda official
			IF	ISNULL((SELECT ROUND(SUM(DebitValue),2) FROM @JournalVourcherDetailTmp2), 0) 
				<> 
				ISNULL( IIF(@CurrencyId <> @OfficialCurrencyId OR @DevolutionType = 3,
							CASE @DevolutionType
							WHEN 1 THEN	   (SELECT SUM( rdd.Quantity * red.UnitValue)				   
											FROM Inventory.RemissionDevolution rd 
											INNER JOIN Inventory.RemissionDevolutionDetail rdd  ON rd.Id = rdd.RemissionDevolutionId
											INNER JOIN Inventory.RemissionEntranceDetailBatchSerial redbs  ON rdd.RemissionEntranceDetailBatchSerialId = redbs.Id
											INNER JOIN Inventory.RemissionEntranceDetail red  ON redbs.RemissionEntranceDetailId = red.Id
											INNER JOIN Inventory.RemissionEntrance re  ON red.RemissionEntranceId = re.Id
											WHERE rd.Id = @Id)
							WHEN 3 THEN (SELECT SUM( rdd.Quantity * red.UnitValue)
											FROM Inventory.RemissionDevolution rd 
											INNER JOIN Inventory.RemissionDevolutionDetail rdd  ON rd.Id = rdd.RemissionDevolutionId
											JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial crdb  on rdd.ConsignmentInventoryRemissionDetailBatchSerialId = crdb.Id
											INNER JOIN Inventory.ConsignmentInventoryRemissionDetail red  ON crdb.ConsignmentInventoryRemissionDetailId = red.Id
											INNER JOIN Inventory.ConsignmentInventoryRemission re  ON red.ConsignmentInventoryRemissionId= re.Id and re.Id = rd.ConsignmentInventoryRemissionId
											WHERE rd.Id = @Id)
							ELSE (SELECT SUM (k.Quantity * k.Value) FROM Inventory.Kardex k  WHERE k.EntityId = @Id AND k.EntityName = 'RemissionDevolution')
							END,
							(SELECT SUM (k.Quantity * k.Value) FROM Inventory.Kardex k  WHERE k.EntityId = @Id AND k.EntityName = 'RemissionDevolution')), 0) 
			BEGIN
				SELECT 999 AS CodeMessage, 'La suma de los movimientos contables y del kardex no son iguales' AS Message
				RETURN
			END

/*** VIEBOT ***/
		--Configuramos viebot para que contabilice en los libros en los cuales contabilizó la remisión
		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2 
					ON vb2.Form = @DevolutionTypeName AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = @RemissionName AND vb2.Id IS NULL) > 0 BEGIN

			INSERT INTO [GeneralLedger].[VieBot] (Form, HandlesHomologation, LegalBookId, Allow, CreationUser, CreationDate, ModificationUser, ModificationDate)
				SELECT @DevolutionTypeName, vb.HandlesHomologation, vb.LegalBookId, vb.Allow, vb.CreationUser, vb.CreationDate, vb.ModificationUser, vb.ModificationDate
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2 
					ON vb2.Form = @DevolutionTypeName AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = @RemissionName AND vb2.Id IS NULL
		END

		IF (SELECT COUNT(*)
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2 
					ON vb2.Form = @RemissionName AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = @DevolutionTypeName AND vb2.Id IS NULL) > 0 BEGIN

			DELETE vb 
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2 
					ON vb2.Form = @RemissionName AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				where vb.Form = @DevolutionTypeName AND vb2.Id IS NULL
		END
/*** VIEBOT ***/

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
		DECLARE @Message VARCHAR(max) = 'Se confirmó correctamente la devolución con código ' + @Code
		SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó el comprobante contable de tipo ' + Code + ' - ' + Name
		FROM GeneralLedger.JournalVoucherTypes WHERE Id = @JournalVoucherTypeId

		SELECT 0 AS CodeMessage, @Message AS Message

	END TRY
	BEGIN CATCH
		
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' - ' + cast(ERROR_LINE() AS varchar(10)) AS Message

	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento contable) correspondiente a una devolución de remisión de inventario, distinguiendo tres tipos de devolución: devolución de remisión de entrada, devolución de remisión de salida, y devolución de mercancía en consignación. Consulta la configuración contable del módulo de inventario (SettingInventory) para determinar el tipo de comprobante a usar según la unidad operativa, y valida que la remisión original ya haya sido contabilizada antes de proceder. Integra información de las tablas RemissionDevolution, RemissionEntrance, RemissionOutput y ConsignmentInventoryRemission para construir la cabecera y el detalle del comprobante, y lo registra en el libro oficial de contabilidad (GeneralLedger.LegalBook y GeneralLedger.JournalVouchers). Es el mecanismo central de reversión contable de movimientos de inventario remisionado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de remisión de inventario; Remisión de entrada; Remisión de salida; Remisión de inventario en consignación; Reposición de mercancía en consignación; Comprobante contable (Journal Voucher); Libro oficial contable; Plan de cuentas (cuenta principal débito/crédito); Homologación de cuentas contables; Tercero (proveedor/cliente); Kardex (costo promedio); Moneda oficial vs moneda extranjera; VieBot (asistencia contable automática); Grupo de productos (parametrización contable)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DevolutionType = 1 (devolución de remisión de entrada) → Valida que la remisión de entrada haya generado movimiento contable; usa cuentas pg.ReferenceInputDebitAccountId/ReferenceInputCreditAccountId y tercero del proveedor (Supplier.IdThirdParty); el valor unitario es UnitValue si moneda <> oficial, si no toma el costo promedio del Kardex.; si @DevolutionType = 2 (devolución de remisión de salida) → Valida que la remisión de salida haya contabilizado; usa cuentas pg.ReferenceOutputDebitAccountId/ReferenceOutputCreditAccountId y tercero del cliente (Customer.ThirdPartyId); el valor unitario proviene del Kardex.; si @DevolutionType = 3 (devolución de remisión de consignación) → Valida que la remisión de consignación haya contabilizado; usa cuentas pg.ConsignmentMerchandiseDebitAccountId/CreditAccountId y tercero del proveedor; valida cuentas contra @JournalVoucherOriginRemision; si MovementType=3 (reposición), descuenta @Quantity de ReplacementQuantity en ConsignmentInventoryRemissionDetailBatchSerial.; si @DevolutionType no está en {1,2,3} → Devuelve CodeMessage=999 con ''Tipo de devolución no controlada'' y termina.; si ISNULL(@JournalVoucherTypeId,0) = 0 → Devuelve CodeMessage=999 indicando que se debe parametrizar el tipo de comprobante para la devolución del tipo de remisión correspondiente.; si Para tipo 3 con MovementType=3 y ReplacementQuantity < @Quantity (cantidad ya repuesta menor a la devuelta) → Devuelve CodeMessage=999 con ''La cantidad a devolver es mayor a la cantidad que ya se ha repuesto.'' y termina sin contabilizar. else Actualiza ReplacementQuantity = ReplacementQuantity - @Quantity en ConsignmentInventoryRemissionDetailBatchSerial.; si Las cuentas contables del producto no coinciden con las usadas en el comprobante original de la remisión (incluyendo homologación) → Inserta error en @TableErrors indicando que las cuentas no son las mismas que se registraron en la remisión.; si Existen errores en @TableErrors después del recorrido → Devuelve CodeMessage=999 con todos los errores concatenados y termina sin generar comprobante.; si SUM(DebitValue) del detalle <> SUM esperado (UnitValue de la remisión si moneda<>oficial; Kardex de la devolución si moneda oficial) → Devuelve CodeMessage=999 ''La suma de los movimientos contables y del kardex no son iguales'' y termina.; si Existe configuración de VieBot para el formulario de la remisión origen pero no para el formulario de la devolución → Inserta filas equivalentes en GeneralLedger.VieBot para el formulario de devolución (Form=@DevolutionTypeName) replicando HandlesHomologation, LegalBookId y Allow.; si Existe configuración de VieBot para el formulario de devolución que no tiene equivalente en la remisión origen → Elimina esas filas de GeneralLedger.VieBot para mantener sincronía con la configuración de la remisión.; si El SP GeneralLedger.SP_CreateAndValidateJournalVoucherMovement devuelve code=''999'' → Devuelve CodeMessage=999 con el MessageResult del error y termina. else Devuelve CodeMessage=0 con mensaje de confirmación incluyendo el consecutivo del comprobante generado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Inventory.RemissionDevolution; Inventory.SettingInventory; Inventory.RemissionEntrance; Inventory.RemissionOutput; Inventory.ConsignmentInventoryRemission; GeneralLedger.CompanySettings; GeneralLedger.AccountingMovement; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.HomologationAccount; GeneralLedger.MainAccounts; Inventory.RemissionDevolutionDetail; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.Kardex; Inventory.InventoryProduct; Inventory.ProductGroup; Common.Supplier; Common.Customer; Inventory.RemissionOutputDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetail; GeneralLedger.VieBot; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionDevolution';
-- GO
