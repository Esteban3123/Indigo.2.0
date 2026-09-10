-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-09
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad]
	@PortfolioTransferId INT,
	@CodeUser VARCHAR(20),
	@XmlParameters XML,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @Status TINYINT,
			------------------------------
			@JournalVoucherTypeId INT,
			@VoucherDate DATETIME,
			@Detail VARCHAR(MAX),
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			@OriginEntityName VARCHAR(250),
			@CurrencyId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	--Tabla de resultados para la generacion de detalles de comprobante contable para provision y deterioro
	DECLARE @ResultProvisionAndDeterioration TABLE
	(
		Id INT, 
		[Status] INT, 
		[Message] VARCHAR(MAX),
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue DECIMAL(21, 5), 
		CreditValue DECIMAL(21, 5)
	)

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
		Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate DATETIME,
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
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		
		SELECT	
				@EntityName =  t.x.value('EntityName[1]','VARCHAR(100)')
		FROM @XmlParameters.nodes('/Parameters') t(x)
		
		SELECT	@Status = pt.Status
		FROM Portfolio.PortfolioTransfer pt
		WHERE pt.Id = @PortfolioTransferId

		--si viene desde fact basica tiene que tomar el TRM Oficial en contabilidad, solo se usa EL TRM custom cuando se genera desde Fact salud (Invoice)
		IF @EntityName is not NULL AND @EntityName <> 'Invoice' BEGIN
			SET @EntityName=''
		END

		IF @Status = 2
		BEGIN
			SELECT @Detail = ' Facturas: ' + STUFF((
				SELECT DISTINCT ', ' + ar.InvoiceNumber
				FROM Portfolio.PortfolioTransferDetail ptd
				JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
				WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@JournalVoucherTypeId = sp.JournalVoucherTypeTranslationId,
					@VoucherDate = pt.DocumentDate,
					@Detail = 'Generado desde Cruce Anticipo vs CxC ' + pt.Code + ' - Anticipo: ' + pa.Code + ISNULL(@Detail, ''),
					@EntityId = pt.Id,
					@EntityCode = pt.Code,
					@EntityName = IIF(@EntityName is NULL OR @EntityName  ='','PortfolioTransfer','AutomaticPortfolioTransfer') ,
					@CurrencyId = pa.CurrencyId
			FROM Portfolio.PortfolioTransfer pt
			JOIN Portfolio.PortfolioAdvance pa ON pt.PortfolioAdvanceId = pa.Id
			JOIN Portfolio.SettingPortfolio sp ON pt.OperatingUnitId = sp.OperatingUnitId		
			WHERE pt.Id = @PortfolioTransferId
			
			/******************************** PROVISION Y DETERIORO ******************************/
			if @CurrencyId is null or @CurrencyId =0 BEGIN
				set @CurrencyId = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings)
			END

			SELECT @SubXml = CONVERT
			(
				XML,
				(
					SELECT Data.*
					FROM 
					(
					SELECT 
						ptd.AccountReceivableId, 
						@VoucherDate DocumentDate,
						ar.InvoiceNumber, 
						ptd.Value,
						@CurrencyId as CurrencyId,
						@PortfolioTransferId as PortfolioTransferId
					FROM Portfolio.PortfolioTransferDetail ptd
					JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
					WHERE PortfolioTrasferId = @PortfolioTransferId
					) AS Data
					For XML AUTO,TYPE, ELEMENTS
				)
			)

			INSERT @ResultProvisionAndDeterioration
				EXEC [Portfolio].[SP_CreateDetailsJournalVoucher] @SubXml

			IF EXISTS (SELECT 1 FROM @ResultProvisionAndDeterioration WHERE [Status] = 0 OR [Status] = 2)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Message
						FROM @ResultProvisionAndDeterioration 
						WHERE [Status] = 0 OR [Status] = 2
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, '')
				RETURN 
			END
		END
		ELSE IF @Status = 4
		BEGIN
			SELECT	@JournalVoucherTypeId = sb.ReverseTransferJournalVoucherTypeId,
					@VoucherDate = pn.NoteDate,
					@Detail = 'Generado desde la Nota de Cartera ' + pn.Code + ' reversando el Cruce Anticipo vs CxC ' + pt.Code,
					@EntityId = pn.Id,
					@EntityCode = pn.Code,
					@EntityName = IIF(@EntityName is NULL OR @EntityName  ='','PortfolioNote','AutomaticPortfolioNote') ,
					@CurrencyId= pn.CurrencyId,
					@OriginEntityName = 'PortfolioTransfer'
			FROM Portfolio.PortfolioNote pn
			JOIN Portfolio.PortfolioTransfer pt ON pn.PortfolioTransferId = pt.Id
			JOIN Billing.SettingsBilling sb ON pt.OperatingUnitId = sb.IdOperatingUnit
			WHERE pn.Status = 2 AND pt.Id = @PortfolioTransferId
		END

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher,VoucherDate,Status,Detail,EntityId,EntityCode,EntityName,OriginEntityName,CurrencyId
		)
		VALUES
		(
			@JournalVoucherTypeId,@VoucherDate,2,@Detail,@EntityId,@EntityCode,@EntityName,@OriginEntityName,@CurrencyId
		)

			;WITH  CTE_PortfolioTransferId AS( 	
			SELECT pt.PortfolioAdvanceId, SUM(d.DebitValue - d.CreditValue) Value
			FROM Portfolio.PortfolioTransfer pt
			JOIN
			(
				SELECT d.PortfolioTrasferId PortfolioTransferId, 0 DebitValue, d.Value CreditValue
				FROM Portfolio.PortfolioTransferDetail d
				WHERE d.PortfolioTrasferId = @PortfolioTransferId
				UNION ALL
				SELECT d.PortfolioTransferId PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0), IIF(d.Nature = 1, 0, d.Value) CreditValue
				FROM Portfolio.PortfolioTransferOtherConcept d
				WHERE d.PortfolioTransferId = @PortfolioTransferId
			) d ON pt.Id = d.PortfolioTransferId
			WHERE pt.Id = @PortfolioTransferId
			GROUP BY pt.PortfolioAdvanceId
			HAVING SUM(d.DebitValue - d.CreditValue) <> 0)

		--Se insertan los detalles del comprobante contable crédito
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, 
			IdThirdParty, 
			IdCostCenter, 
			Detail, 
			DebitValue, 
			CreditValue
		)
		--Anticipo
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN pa.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN pa.CostCenterId ELSE NULL END as CostCenterId,
				'',
				CAST(ABS(IIF(@Status = 2, IIF(pt.Value > 0, 0, pt.Value), IIF(pt.Value > 0, pt.Value, 0))) AS DECIMAL(21, 5)) DebitValue, 
				CAST(ABS(IIF(@Status = 2, IIF(pt.Value > 0, pt.Value, 0), IIF(pt.Value > 0, 0, pt.Value))) AS DECIMAL(21, 5)) CreditValue
			FROM Portfolio.PortfolioAdvance pa
			JOIN GeneralLedger.MainAccounts ma ON pa.MainAccountId = ma.Id
			JOIN CTE_PortfolioTransferId pt ON pa.Id = pt.PortfolioAdvanceId
		UNION ALL
			--Facturas
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN ptd.CostCenterId ELSE NULL END as CostCenterId,
				'',
				IIF(@Status = 2, 0, ptd.Value) DebitValue, 
				IIF(@Status = 2, ptd.Value, 0) CreditValue
			FROM Portfolio.PortfolioTransferDetail ptd			
			JOIN GeneralLedger.MainAccounts ma ON ptd.MainAccountId = ma.Id
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
		UNION ALL
			--Otros Conceptos
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN ptoc.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN ptoc.CostCenterId ELSE NULL END as CostCenterId,
				'',
				IIF(@Status = 2, IIF(ptoc.Nature = 1, ptoc.Value, 0), IIF(ptoc.Nature = 1, 0, ptoc.Value)) DebitValue, 
				IIF(@Status = 2, IIF(ptoc.Nature = 1, 0, ptoc.Value), IIF(ptoc.Nature = 1, ptoc.Value, 0)) CreditValue
			FROM Portfolio.PortfolioTransferOtherConcept ptoc
			JOIN GeneralLedger.MainAccounts ma ON ptoc.MainAccountId = ma.Id
			WHERE ptoc.PortfolioTransferId = @PortfolioTransferId
			--Provision / Deterioro
		UNION ALL
			SELECT 
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN jvd.IdThirdParty ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN jvd.IdCostCenter ELSE NULL END as CostCenterId,
				'Detalle generado con Provision/Deterioro',
				IIF(@Status = 2, jvd.DebitValue, jvd.CreditValue), 
				IIF(@Status = 2, jvd.CreditValue, jvd.DebitValue)
			FROM @ResultProvisionAndDeterioration jvd
			JOIN GeneralLedger.MainAccounts ma ON jvd.IdMainAccount = ma.Id
			WHERE jvd.Status = 1
		
		/******************************************** GENERAR COMPROBANTE ********************************************/

		SELECT @SubXml = CONVERT
		(
			XML,
			(
				SELECT *
				FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.id = JournalVoucherDetail.IdAccounting
				For XML AUTO,TYPE, ELEMENTS
			)
		)

		--Se consume el sp que guarda el movimiento contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodeUser 
			select 
				@Code_Output = rjv.code, 
				@Message_Output = rjv.MessageResult, 
				@Id_Output = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, '')
			RETURN
		END  

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name
		FROM GeneralLedger.JournalVoucherTypes jvt
		WHERE jvt.Id = @JournalVoucherTypeId
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento de diario) asociado a un traslado o cruce de cartera, a partir del identificador de una transferencia de cartera (PortfolioTransferId). Según el estado de la transferencia, produce el comprobante de cruce entre anticipos y cuentas por cobrar (facturas) o, en caso de reversión, genera el comprobante de nota de cartera correspondiente. Orquesta la consulta de traslados, anticipos, detalles de cuentas por cobrar y la configuración contable de cartera para construir la cabecera y el detalle del comprobante, aplicando la moneda oficial (TRM) cuando no se dispone de moneda personalizada. Retorna códigos de resultado y mensajes de error o éxito para el proceso de reconocimiento contable de movimientos de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable (journal voucher) asociado a un cruce de anticipo contra cuentas por cobrar o a su reverso por nota de cartera, ensamblando cabecera y detalles (anticipo, facturas, otros conceptos y provisión/deterioro).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El PortfolioTransfer debe existir y estar en estado 2 (cruce) o 4 (reverso); Para estado 4 debe existir una PortfolioNote vinculada al PortfolioTransfer con Status=2; Para estado 2 debe existir un PortfolioAdvance asociado y configuración SettingPortfolio para la unidad operativa; Para estado 4 debe existir configuración SettingsBilling para la unidad operativa con ReverseTransferJournalVoucherTypeId; Las cuentas contables (MainAccounts) referenciadas por anticipo, detalle, otros conceptos y provisión/deterioro deben existir; Si CurrencyId es nulo o cero debe existir OfficialCurrencyId en GeneralLedger.CompanySettings', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa PortfolioTransfer en estado 2 (cruce) o 4 (reverso); otros estados no generan comprobante; En estado 4 únicamente considera PortfolioNote con Status=2; La asignación débito/crédito del anticipo, facturas, otros conceptos y provisión se invierte entre el cruce (Status=2) y su reverso (Status=4); El campo ThirdPartyId/CostCenterId del detalle solo se llena cuando la cuenta principal lo permite (HandlesThirdParty/HandlesCostCenter=1); Solo se incluyen en el comprobante los registros de provisión/deterioro con Status=1; El detalle textual del comprobante incluye siempre el código del PortfolioTransfer y, en cruce, el del PortfolioAdvance y la lista de facturas; Los errores capturados se retornan con CodeResult=999 y nunca con éxito', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Cruce de anticipo vs cuenta por cobrar; Reverso de traslado de cartera; Nota de cartera; Anticipo de cartera; Cuenta por cobrar / Factura; Provisión y deterioro de cartera; Otros conceptos de traslado; Plan de cuentas (cuenta principal, tercero, centro de costo); TRM oficial vs TRM custom de facturación salud; Tipo de comprobante (JournalVoucherType); Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ResultProvisionAndDeterioration: Cuando @Status=2, ejecuta Portfolio.SP_CreateDetailsJournalVoucher con XML de detalles del traslado para obtener movimientos de provisión/deterioro; [INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement crea el comprobante contable con cabecera y detalles ensamblados (anticipo + facturas + otros conceptos + provisión/deterioro); [RETURN_RESULT] OUTPUT: Devuelve @CodeResult=0 y mensaje con consecutivo y tipo del comprobante generado cuando todo es exitoso; [RETURN_RESULT] OUTPUT: Devuelve @CodeResult=999 si hay filas con Status 0 o 2 en provisión/deterioro, si SP_CreateAndValidateJournalVoucherMovement retorna code distinto de 0, o si ocurre excepción (ERROR_MESSAGE + línea)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName del XML no es nulo y distinto de ''Invoice'' → Resetea @EntityName a '''' para usar TRM oficial de contabilidad (no la TRM custom de Facturación Salud) else Mantiene el EntityName recibido (caso Invoice usa TRM custom); si @Status = 2 (PortfolioTransfer activo / cruce anticipo vs CxC) → Arma detalle ''Generado desde Cruce Anticipo vs CxC...'', toma JournalVoucherTypeTranslationId de SettingPortfolio, calcula provisión/deterioro y signos débito/crédito según el cruce else Si @Status = 4: arma detalle de reverso desde PortfolioNote, toma ReverseTransferJournalVoucherTypeId de SettingsBilling e invierte los signos débito/crédito; si EXISTS filas en @ResultProvisionAndDeterioration con Status 0 o 2 → Retorna inmediatamente con CodeResult=999 concatenando los mensajes de error de provisión/deterioro; si @CurrencyId es null o 0 → Asigna OfficialCurrencyId desde GeneralLedger.CompanySettings; si @EntityName es nulo o vacío al asignar entidad → Usa ''PortfolioTransfer'' o ''PortfolioNote''; en caso contrario usa ''AutomaticPortfolioTransfer'' o ''AutomaticPortfolioNote''; si ma.HandlesThirdParty = 1 / ma.HandlesCostCenter = 1 en MainAccounts → Asigna ThirdPartyId / CostCenterId al detalle; en caso contrario los deja en NULL; si @Code_Output <> 0 tras SP_CreateAndValidateJournalVoucherMovement → Retorna CodeResult=999 con el mensaje devuelto por el SP de contabilidad', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_CreateDetailsJournalVoucher; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivable; Portfolio.PortfolioAdvance; Portfolio.SettingPortfolio; Portfolio.PortfolioNote; Portfolio.PortfolioTransferOtherConcept; Billing.SettingsBilling; GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad';
-- GO
