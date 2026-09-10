-- =============================================
-- Author:		Andres Alarcon
-- Create date: 28-11-2023
-- Description:	Procedimiento que se encarga de reversar la contabilidad de lo documentos de prevision que van a ser legalizados
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReverseProvisionDocumentDistributionDirectCost] 
    @DistributionDirectCostId AS INT,
	@CodeUser AS VARCHAR(20),
	@Type INT
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	--Tabla con las variables de resultado
	DECLARE @TableResult TABLE
	(
		CodeMessage INT,
		Message VARCHAR(MAX),
		Consecutive VARCHAR(MAX),
		Code VARCHAR(20)
	)

	/******************************** VARIABLES CONTABLES *******************************/

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
	    Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
	    IdAuto INT identity(1,1),
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML,
			@OfficialCurrencyId as INT,
			@TaxRegistration tinyINT

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/*************************************** PROCESO ************************************/

	BEGIN TRY
	/*se consulta al moneda oficial y el tipo de registro de iva*/
		select @OfficialCurrencyId = cs.OfficialCurrencyId ,
				@TaxRegistration = cs.TaxRegistration
		from GeneralLedger.CompanySettings cs
		
		IF @Type = 1 BEGIN --Estado final: Confirmado Legalizado

		/***************************** CREACIÓN DOCUMENTO CONTABLE *****************************/

		DECLARE @IdProvision INT,
				@MainAccountId INT,
				@ThirdpartyId INT,
				@ValueProvision DECIMAL(18,2),
				@CodeProvision INT

		DECLARE DetailProvisionDocument_cursor CURSOR FOR

		SELECT DISTINCT ld.ProvisionDocumentId IdProvision,
						cd.Code CodeProvision,
						cd.MainAccountId,
						cd.ThirdPartyId,
						cd.Value ValueProvision
		FROM Cost.CostDistributionDirectCostLegalizedDocuments ld WITH(NOLOCK)
		JOIN Cost.CostDistributionDirectCost cd WITH(NOLOCK) ON cd.Id = ld.ProvisionDocumentId
		WHERE ld.DistributionDirectCostId = @DistributionDirectCostId

		OPEN DetailProvisionDocument_cursor
			FETCH NEXT FROM DetailProvisionDocument_cursor INTO @IdProvision, @CodeProvision, @MainAccountId, @ThirdpartyId, @ValueProvision
			
			WHILE @@fetch_status = 0
			BEGIN

					/***************************** TODOS LOS DETALLES CONTABLES *****************************/

				INSERT INTO @JournalVourcherDetailTmp
					( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue)

					--Debito
					SELECT
						ma.Id AS IdMainAccount,
						IIF(ma.HandlesThirdParty = 1, @ThirdpartyId , NULL ) AS IdThirdParty,
						NULL AS IdCostCenter,
						CONCAT('Documento Provisión Codigo:',' - ',@CodeProvision) AS Detail,
						@ValueProvision AS DebitValue,
						0 AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
					WHERE ma.Id = @MainAccountId
					
					UNION ALL

					--Credito
					SELECT
						cddc.MainAccountId AS IdMainAccount,
						IIF(ma.HandlesThirdParty = 1,cd.ThirdPartyId,null ) IdThirdParty,
						cddc.CostCenterId AS IdCostCenter,
						'' AS Detail,
						0 AS DebitValue,
						cddc.Value AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM Cost.CostDistributionDirectCostDetail cddc WITH(NOLOCK)
					JOIN Cost.CostDistributionDirectCost cd WITH(NOLOCK) on cd.Id = cddc.DistributionDirectCostId
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cddc.MainAccountId = ma.Id
					WHERE  cd.Id = @IdProvision
				
				FETCH NEXT FROM DetailProvisionDocument_cursor INTO @IdProvision, @CodeProvision, @MainAccountId, @ThirdpartyId, @ValueProvision
			END

		CLOSE DetailProvisionDocument_cursor
		DEALLOCATE DetailProvisionDocument_cursor

			/************************************  CABECERA DEL COMPROBANTE ************************************/

				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
					SELECT TOP 1
						NULL LegalBookId, 
						(SELECT ProvisionReversalJournalVoucherTypeId from Cost.CostSetting) IdJournalVoucher, 
						cddc.ServicePeriodDate VoucherDate,
						2 Status, 
						'Reversión de los documentos legalizados de Distribución del Elemento del Costo para Provisión del Costo o Gasto' Detail,
						cddc.Code EntityCode,
						cddc.Id EntityId,
						'DistributionCostElements' EntityName,
						'' OriginEntityName,
						@OfficialCurrencyId CurrencyId
					FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
					WHERE cddc.Id = @DistributionDirectCostId

		END
		ELSE IF @Type = 2 BEGIN --Estado final: Reversado

					/***************************** TODOS LOS DETALLES CONTABLES *****************************/
		INSERT INTO @JournalVourcherDetailTmp
					( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue)
					--Debito
					SELECT
						ma.Id AS IdMainAccount,
						IIF(ma.HandlesThirdParty = 1,cddc.ThirdPartyId, NULL ) AS IdThirdParty,
						NULL AS IdCostCenter,
						CONCAT('Documento Provisión Codigo:',' - ',cddc.Code) AS Detail,
						cddc.Value AS DebitValue,
						0 AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cddc.MainAccountId = ma.Id
					WHERE cddc.Id = @DistributionDirectCostId
					
					UNION ALL

					--Credito
					SELECT
						ma.Id AS IdMainAccount,
						IIF(ma.HandlesThirdParty = 1,cddc.ThirdPartyId, NULL ) IdThirdParty,
						cddcd.CostCenterId AS IdCostCenter,
						'' AS Detail,
						0 AS DebitValue,
						cddcd.Value AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM Cost.CostDistributionDirectCostDetail cddcd WITH(NOLOCK)
					JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) on cddc.Id = cddcd.DistributionDirectCostId
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cddcd.MainAccountId = ma.Id
					WHERE  cddc.Id = @DistributionDirectCostId

					/************************************  CABECERA DEL COMPROBANTE ************************************/

				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
					SELECT TOP 1
						NULL LegalBookId, 
						(SELECT ProvisionReversalJournalVoucherTypeId from Cost.CostSetting) IdJournalVoucher, 
						cddc.ServicePeriodDate VoucherDate, 
						2 Status, 
						'Reversión del documento No ' + cddc.Code Detail,
						cddc.Code EntityCode,
						cddc.Id EntityId,
						'DistributionCostElements' EntityName,
						'' OriginEntityName,
						@OfficialCurrencyId CurrencyId
					FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
					WHERE cddc.Id = @DistributionDirectCostId

		END
		ELSE IF @Type = 3 BEGIN --Estado final: Confirmado Sin Legalizar
                          		/***************************** CREACIÓN DEL COMPROBANTE CONTABLE DEL DOCUMENTO DE PROVISIÓN CUANDO SE REALIZA UNA REVERSIÓN DE CXP *****************************/

			DECLARE @IdProvisionR INT

			DECLARE DetailProvisionDocument_cursorR CURSOR FOR

			SELECT DISTINCT ld.ProvisionDocumentId IdProvision
			FROM Cost.CostDistributionDirectCostLegalizedDocuments ld WITH(NOLOCK)
			JOIN Cost.CostDistributionDirectCost cd WITH(NOLOCK) ON cd.Id = ld.ProvisionDocumentId
			WHERE ld.DistributionDirectCostId = @DistributionDirectCostId

			OPEN DetailProvisionDocument_cursorR
				FETCH NEXT FROM DetailProvisionDocument_cursorR INTO @IdProvisionR
			
				WHILE @@fetch_status = 0
				BEGIN

						/***************************** TODOS LOS DETALLES CONTABLES *****************************/

					INSERT INTO @JournalVourcherDetailTmp
						( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue)

						--Credito
						SELECT
							ma.Id AS IdMainAccount,
							IIF(ma.HandlesThirdParty = 1,cd.ThirdPartyId, NULL ) IdThirdParty,
							NULL AS IdCostCenter,
							CONCAT('Documento Provisión Codigo:',' - ',cd.Code) AS Detail,
							0 AS DebitValue,
							cd.Value AS CreditValue,
							NULL IdRetention,
							NULL RetentionRate,
							NULL BaseValue,
							NULL BillingValue
						FROM Cost.CostDistributionDirectCost cd WITH(NOLOCK)
						JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cd.MainAccountId = ma.Id
						WHERE  cd.Id = @IdProvisionR

						UNION ALL

						--Debito
						SELECT
							ma.Id AS IdMainAccount,
							IIF(ma.HandlesThirdParty = 1,cddc.ThirdPartyId, NULL ) IdThirdParty,
							cddcd.CostCenterId AS IdCostCenter,
							'' AS Detail,
							cddcd.Value AS DebitValue,
							0 AS CreditValue,
							NULL IdRetention,
							NULL RetentionRate,
							NULL BaseValue,
							NULL BillingValue
					FROM Cost.CostDistributionDirectCostDetail cddcd WITH(NOLOCK)
					JOIN Cost.CostDistributionDirectCost cddc WITH (NOLOCK) on cddc.Id = cddcd.DistributionDirectCostId
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cddcd.MainAccountId = ma.Id
					where cddc.Id = @IdProvisionR
				
					FETCH NEXT FROM DetailProvisionDocument_cursorR INTO @IdProvisionR
				END

			CLOSE DetailProvisionDocument_cursorR
			DEALLOCATE DetailProvisionDocument_cursorR

				/************************************  CABECERA DEL COMPROBANTE ************************************/

					INSERT INTO @JournalVourcherTmp
						( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
						SELECT TOP 1
							NULL LegalBookId, 
							(SELECT ProvisionJournalVoucherTypeId from Cost.CostSetting) IdJournalVoucher, 
							Common.GETDATE() VoucherDate, 
							2 Status, 
							'Comprobante generado desde la reversión de CXP proveniente de la distribucion de elementos del costo que llevaba consigo documentos de provisión' Detail,
							cddc.Code EntityCode,
							cddc.Id EntityId,
							'DistributionCostElements' EntityName,
							'' OriginEntityName,
							@OfficialCurrencyId CurrencyId
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId
            END

				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				Declare @CodeMessage INT,
						@Message Varchar(Max),
						@IdJournalVoucherResult INT

				--Se consume el sp que guarda el movimiento contable
				insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
				select 
					@CodeMessage = rjv.code, 
					@Message = rjv.MessageResult, 
					@IdJournalVoucherResult = rjv.IdJournalVoucher
				from @resultJournalVoucher rjv
				
				--Se valida que no hayan errores en el guardado del comprobante contable
				IF @CodeMessage = '999' 
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 999 AS CodeMessage, 'No se Legalizaron los documentos de provisión por ' + @Message AS Message, '' AS Consecutive, cddc.Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId

				END
				ELSE
				BEGIN

				IF @Type = 1 BEGIN

					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 0 AS CodeMessage, 'Se Legalizaron correctamente los documentos de provisión ' AS Message, '' AS Consecutive, cddc.Code AS Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId

						--Confirmado Legalizado
						UPDATE cddc 
							SET cddc.Status = 6
						FROM Cost.CostDistributionDirectCostLegalizedDocuments ld WITH(NOLOCK)
						JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON cddc.Id = ld.ProvisionDocumentId
						WHERE ld.DistributionDirectCostId = @DistributionDirectCostId

				END
				ELSE IF @Type = 2 BEGIN

						INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 0 AS CodeMessage, 'Se Reversó correctamente el Documento de provisión con codigo ' + cddc.Code AS Message, '' AS Consecutive, cddc.Code AS Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId

						--Reversado
						UPDATE cddc 
							SET cddc.Status = 4
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId
				END
				ELSE IF @Type = 3 BEGIN

						--Confirmado sin legalizar
						UPDATE cddc 
							SET cddc.Status = 5
						FROM Cost.CostDistributionDirectCostLegalizedDocuments cddcld WITH(NOLOCK)
						JOIN Cost.CostDistributionDirectCost cddc ON cddcld.ProvisionDocumentId = cddc.Id
						WHERE cddcld.DistributionDirectCostId = @DistributionDirectCostId
						RETURN

				END

				INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT
					0 AS CodeMessage,
					'Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
					'' AS Consecutive,
					cddc.Code
				FROM Cost.CostDistributionDirectCost cddc
				JOIN Cost.CostSetting cs ON 1 = 1
				JOIN GeneralLedger.JournalVoucherTypes jvt ON cs.ProvisionReversalJournalVoucherTypeId = jvt.Id
				WHERE cddc.Id = @DistributionDirectCostId
			END

	END TRY
	BEGIN CATCH
		DECLARE @error_message VARCHAR(MAX) = (SELECT Message FROM @TableResult WHERE CodeMessage = 999)
		IF NOT EXISTS (SELECT 1 FROM @TableResult WHERE CodeMessage = 999)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
		END
		ELSE
		BEGIN
			THROW 51000, @error_message, 1
		END
	END CATCH

	SELECT CodeMessage, Message, Consecutive 
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la reversión contable de los documentos de provisión asociados a una distribución de costo directo en el módulo de costeo. Según el tipo de operación solicitado (confirmado-legalizado o reversado), genera los asientos contables inversos —débitos y créditos— utilizando las cuentas principales, terceros y centros de costo registrados en los documentos de provisión legalizados (CostDistributionDirectCostLegalizedDocuments y CostDistributionDirectCost). Consulta la configuración contable de la compañía (moneda oficial y tipo de registro de IVA) desde GeneralLedger.CompanySettings, y construye el comprobante de diario (cabecera y detalles) que revierte el impacto contable previamente generado al legalizar la distribución de costos directos. Es utilizado en el proceso de cierre o corrección de períodos de costeo cuando se deben anular provisiones de costos o gastos ya contabilizadas.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversar/generar la contabilidad asociada a documentos de provisión vinculados a una distribución de costo directo, según el tipo de operación (legalización, reversión o reversión por CXP), y actualizar el estado del documento.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro en Cost.CostDistributionDirectCost con el Id recibido.; Para Type=1 y Type=3 deben existir documentos de provisión asociados en Cost.CostDistributionDirectCostLegalizedDocuments.; Cost.CostSetting debe tener configurado ProvisionReversalJournalVoucherTypeId (Type 1 y 2) y ProvisionJournalVoucherTypeId (Type 3).; GeneralLedger.CompanySettings debe tener OfficialCurrencyId y TaxRegistration definidos.; El usuario @CodeUser debe ser válido para SP_CreateAndValidateJournalVoucherMovement.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El asiento contable siempre queda balanceado: débito y crédito por el valor del documento de provisión / detalle de distribución.; La moneda del comprobante siempre es la moneda oficial definida en CompanySettings.OfficialCurrencyId.; EntityName del comprobante siempre es ''DistributionCostElements''.; El tercero solo se registra en el asiento cuando la cuenta lo maneja (HandlesThirdParty=1).; El tipo de comprobante depende del flujo: ProvisionReversalJournalVoucherTypeId para Type 1 y 2; ProvisionJournalVoucherTypeId para Type 3.; Si hay error en la creación del comprobante contable, no se cambia el estado del documento de distribución.; El flujo Type=3 no produce mensaje informativo del comprobante por uso de RETURN.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Para Type=1: por cada documento de provisión legalizado, se arma un comprobante con débito a la cuenta del documento de provisión y crédito a las cuentas del detalle de la distribución, usando el tipo ProvisionReversalJournalVoucherTypeId.; [INSERT] GeneralLedger.JournalVouchers: Para Type=2: se genera comprobante de reversión con débito a la cuenta principal del CostDistributionDirectCost y crédito a las cuentas del detalle, tipo ProvisionReversalJournalVoucherTypeId, fecha=ServicePeriodDate.; [INSERT] GeneralLedger.JournalVouchers: Para Type=3: por cada documento de provisión asociado, se genera comprobante con crédito a la cuenta del documento de provisión y débito al detalle de la distribución, tipo ProvisionJournalVoucherTypeId y fecha actual (Common.GETDATE()).; [UPDATE] Cost.CostDistributionDirectCost: Cuando Type=1 y el comprobante se generó sin error (CodeMessage<>999), se actualiza Status=6 (Confirmado Legalizado) a los documentos de provisión vinculados a la distribución.; [UPDATE] Cost.CostDistributionDirectCost: Cuando Type=2 y el comprobante se generó sin error, se actualiza Status=4 (Reversado) al documento de la distribución.; [UPDATE] Cost.CostDistributionDirectCost: Cuando Type=3 (reversión de CXP), se actualiza Status=5 (Confirmado sin Legalizar) a los documentos de provisión vinculados, sin pasar por el bloque de mensaje de comprobante (RETURN).; [RETURN_RESULT] @TableResult: Se devuelve CodeMessage=999 con el mensaje de error cuando SP_CreateAndValidateJournalVoucherMovement retorna code=999, o ERROR_MESSAGE() en CATCH; en caso exitoso se retorna CodeMessage=0 con mensaje según el tipo y el consecutivo del comprobante.; [RAISERROR] @TableResult: En CATCH, si ya existía un registro con CodeMessage=999, se relanza la excepción con THROW 51000.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type = 1 (Estado final: Confirmado Legalizado) → Recorre con cursor los documentos de provisión legalizados y arma asiento débito (cuenta del provisión) / crédito (detalle de distribución), usando ProvisionReversalJournalVoucherTypeId.; si @Type = 2 (Estado final: Reversado) → Genera asiento débito a la cuenta del documento de distribución y crédito al detalle, con ProvisionReversalJournalVoucherTypeId.; si @Type = 3 (Estado final: Confirmado Sin Legalizar, reversión de CXP) → Recorre con cursor los provisiones asociados y genera asiento crédito (cuenta del provisión) / débito (detalle), con ProvisionJournalVoucherTypeId y fecha actual.; si ma.HandlesThirdParty = 1 en la cuenta contable → Se asigna el ThirdPartyId al detalle del asiento; en caso contrario IdThirdParty queda NULL. else IdThirdParty = NULL; si @CodeMessage = 999 tras ejecutar SP_CreateAndValidateJournalVoucherMovement → Inserta mensaje de error 999 en @TableResult y omite la actualización de estado. else Continúa con la actualización de estado según @Type e inserta mensaje de éxito.; si @Type = 3 tras éxito → Actualiza Status=5 y RETURN inmediato (no inserta mensaje de comprobante generado).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Cost.CostDistributionDirectCostLegalizedDocuments; Cost.CostDistributionDirectCost; GeneralLedger.MainAccounts; Cost.CostDistributionDirectCostDetail; Cost.CostSetting; GeneralLedger.JournalVouchers; GeneralLedger.LegalBook; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseProvisionDocumentDistributionDirectCost';
-- GO
