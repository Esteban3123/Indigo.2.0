-- =============================================
-- Author:		Andres Alarcon
-- Create date: 21-11-2023
-- Description:	Procedimiento que se encarga de confirmar las distribuciones de elementos del costo que son categorizadas como documento de provision
-- =============================================
CREATE PROCEDURE [Cost].[SP_ConfirmProvisionDocumentDistributionDirectCost] 
    @DistributionDirectCostId AS INT,
	@CodeUser AS VARCHAR(20)
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

		/************************************* VALIDACIONES ************************************/

		IF NOT EXISTS ( 
			SELECT 1 
			FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
			WHERE cddc.Id = @DistributionDirectCostId
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 
					'No existe la distribucion de elementos del costo ' AS Message, 
					'' AS Consecutive
				FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)

			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
			WHERE cddc.Status <> 2 AND cddc.Id = @DistributionDirectCostId
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La distribucion de elementos del costo ' + cddc.Code + ' se encuentra en estado ' + 
						CASE cddc.Status
							WHEN 3 THEN 'Anulado'
							WHEN 4 THEN 'Reversado'
							ELSE 'N/A'
						END
					AS Message, 
					'' AS Consecutive, 
					cddc.Code
				FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
				WHERE cddc.Id = @DistributionDirectCostId

			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		/***************************** CREACIÓN DOCUMENTO CONTABLE *****************************/

					/***************************** TODOS LOS DETALLES CONTABLES *****************************/
			INSERT INTO @JournalVourcherDetailTmp
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
				
				--Credito
				SELECT
					cddc.MainAccountId AS IdMainAccount,
					cddc.ThirdPartyId AS IdThirdParty,
					NULL AS IdCostCenter,
					'' AS Detail,
					0 AS DebitValue,
					cddc.Value AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					NULL AS BaseValue,
					NULL AS BillingValue
				FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
				where cddc.Id = @DistributionDirectCostId
					
				UNION ALL

				--Debito
				SELECT
					cddcd.MainAccountId AS IdMainAccount,
					IIF(ma.HandlesThirdParty = 1,cddc.ThirdPartyId,null ) IdThirdParty,
					cddcd.CostCenterId AS IdCostCenter,
					'' AS Detail,
					cddcd.Value AS DebitValue,
					0 AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					NULL AS BaseValue,
					NULL AS BillingValue
				FROM Cost.CostDistributionDirectCostDetail cddcd WITH(NOLOCK)
				JOIN Cost.CostDistributionDirectCost cddc WITH (NOLOCK) on cddc.Id = cddcd.DistributionDirectCostId
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on cddcd.MainAccountId = ma.Id
				where cddc.Id = @DistributionDirectCostId

			/************************************  CABECERA DEL COMPROBANTE ************************************/

				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
					SELECT TOP 1
						NULL LegalBookId, 
						(SELECT ProvisionJournalVoucherTypeId from Cost.CostSetting) IdJournalVoucher, 
						ISNULL(cddc.ServicePeriodDate, Common.GETDATE()) VoucherDate, 
						2 Status, 
						'Comprobante Generado desde la Distribución del Elemento del Costo para Provisión del Costo o Gasto' Detail,
						cddc.Code EntityCode,
						cddc.Id EntityId,
						'DistributionCostElements' EntityName,
						'' OriginEntityName,
						@OfficialCurrencyId
					FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
					WHERE cddc.Id = @DistributionDirectCostId

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
						SELECT 999 AS CodeMessage, 'No se confirmó la distribucion de elementos del costo' + cddc.Code + ' por ' + @Message AS Message, '' AS Consecutive, cddc.Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId

						UPDATE cddc 
							SET cddc.Status = 1
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId
				END
				ELSE
				BEGIN

					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 0 AS CodeMessage, 'Se confirmó correctamente el documento de provisión ' + cddc.Code AS Message, '' AS Consecutive, cddc.Code AS Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						WHERE cddc.Id = @DistributionDirectCostId

					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT DISTINCT
							0 AS CodeMessage,
							'Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
							'' AS Consecutive,
							cddc.Code
						FROM Cost.CostDistributionDirectCost cddc
						JOIN Cost.CostSetting cs ON 1 = 1
						JOIN GeneralLedger.JournalVoucherTypes jvt ON cs.ProvisionJournalVoucherTypeId = jvt.Id
						WHERE cddc.Id = @DistributionDirectCostId

						UPDATE cddc 
							SET cddc.Status = 5
						FROM Cost.CostDistributionDirectCost cddc
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma una distribución de costos directos categorizada como documento de provisión contable. Valida que el registro de distribución exista y se encuentre en estado borrador (estado 2); si no cumple, retorna un mensaje de error indicando si fue anulado o reversado. Al confirmar, genera el comprobante contable de provisión construyendo el detalle con un movimiento crédito a la cuenta principal de la distribución y los movimientos débito a cada cuenta de detalle con sus respectivos centros de costo, terceros y observaciones, incluyendo identificación del empleado cuando aplica. Utiliza la configuración contable de la compañía (moneda oficial, tipo de registro de IVA) y el tipo de comprobante de provisión definido en los parámetros de costos para registrar el asiento en el libro contable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una distribución de elementos del costo categorizada como documento de provisión, generando el comprobante contable asociado y actualizando el estado de la distribución.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La distribución de elementos del costo debe existir en Cost.CostDistributionDirectCost con el Id indicado.; La distribución debe encontrarse en estado 2 (no anulada ni reversada).; Debe existir configuración en GeneralLedger.CompanySettings (moneda oficial y registro de IVA).; Debe existir el tipo de comprobante de provisión configurado en Cost.CostSetting (ProvisionJournalVoucherTypeId).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante contable es siempre balanceado: un único crédito por el valor total de la distribución contra los débitos por cada detalle.; El crédito siempre se registra contra la cuenta principal y el tercero de la cabecera de la distribución, sin centro de costo.; Los débitos siempre conservan el centro de costo del detalle.; El tercero en débitos solo se asigna cuando la cuenta principal del detalle exige manejo de terceros.; El comprobante se registra con la moneda oficial de la compañía y el tipo de comprobante de provisión definido en CostSetting.; La fecha del comprobante usa ServicePeriodDate de la distribución, o la fecha actual (Common.GETDATE) si está nula.; EntityName del comprobante siempre se fija como ''DistributionCostElements''.; Solo distribuciones en estado 2 son confirmables; tras confirmación exitosa quedan en estado 5.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de elementos del costo; Documento de provisión de costo o gasto; Comprobante contable; Cuenta principal; Centro de costo; Tercero; Moneda oficial; Registro de IVA; Libro contable; Tipo de comprobante de provisión; Período de servicio', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Cost.CostDistributionDirectCost: Cuando SP_CreateAndValidateJournalVoucherMovement retorna code=999 (error en el comprobante), se revierte el estado de la distribución a 1 (borrador/pendiente).; [UPDATE] Cost.CostDistributionDirectCost: Cuando el comprobante contable se genera correctamente, se actualiza el estado de la distribución a 5 (confirmado).; [INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement: inserta un comprobante con un crédito a la cuenta principal de la distribución y débitos por cada detalle (CostDistributionDirectCostDetail), asignando tercero solo si la cuenta principal del detalle maneja terceros (ma.HandlesThirdParty=1).; [RETURN_RESULT] RESULT: Retorna un conjunto con CodeMessage/Message/Consecutive describiendo: inexistencia, estado inválido (Anulado/Reversado), error del comprobante (999) o confirmación exitosa con el consecutivo del comprobante generado.; [RAISERROR] RESULT: En CATCH, si ya existe un mensaje 999 en los resultados, se relanza THROW 51000 con dicho mensaje; si no, se inserta el ERROR_MESSAGE+línea como CodeMessage 999.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe la distribución con el Id recibido → Inserta mensaje 999 ''No existe la distribucion de elementos del costo'' y retorna sin generar comprobante.; si La distribución existe pero su Status <> 2 → Inserta mensaje 999 indicando estado ''Anulado'' (3), ''Reversado'' (4) o ''N/A'' y retorna sin generar comprobante.; si SP_CreateAndValidateJournalVoucherMovement devuelve CodeMessage = 999 → Registra mensaje de error de no confirmación y revierte el Status de la distribución a 1. else Registra mensajes de éxito con datos del comprobante generado y actualiza el Status de la distribución a 5.; si ma.HandlesThirdParty = 1 al construir el débito por detalle → Asigna cddc.ThirdPartyId como tercero en el movimiento débito. else Deja IdThirdParty en NULL en el movimiento débito.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionDirectCost; Cost.CostDistributionDirectCostDetail; GeneralLedger.MainAccounts; GeneralLedger.CompanySettings; Cost.CostSetting; GeneralLedger.JournalVouchers; GeneralLedger.LegalBook; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmProvisionDocumentDistributionDirectCost';
-- GO
