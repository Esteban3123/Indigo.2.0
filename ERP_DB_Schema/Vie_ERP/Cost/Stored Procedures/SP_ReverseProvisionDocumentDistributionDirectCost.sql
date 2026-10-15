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
					lb.Code + ' - ' + lb.Name +  ': Se Generó el comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message, 
					jv.Consecutive AS Consecutive, 
					cddc.Code
				FROM Cost.CostDistributionDirectCost cddc WITH(NOLOCK)
				JOIN GeneralLedger.JournalVouchers jv WITH(NOLOCK) ON jv.EntityName = 'DistributionCostElements' AND jv.EntityId = cddc.Id AND jv.EntityCode = cddc.Code
				JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON jv.LegalBookId = lb.Id
				JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
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