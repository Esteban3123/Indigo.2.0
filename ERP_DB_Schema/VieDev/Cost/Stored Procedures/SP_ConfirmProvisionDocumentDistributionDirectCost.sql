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
							lb.Code + ' - ' + lb.Name +  ': Se generó el comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message, 
							jv.Consecutive AS Consecutive, 
							cddc.Code
						FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
						JOIN GeneralLedger.JournalVouchers jv WITH(NOLOCK) ON jv.EntityName = 'DistributionCostElements' AND jv.EntityId = cddc.Id AND jv.EntityCode = cddc.Code
						JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON jv.LegalBookId = lb.Id
						JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
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