-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-16
-- Description:	Procedimiento que se encarga de confirmar la distribucion Factura Monto Fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_ConfirmInvoiceEntityCapitatedDistribution]
    @InvoiceEntityCapitatedDistributionId AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @errors VARCHAR(MAX),
			@InvoiceEntityCapitatedId INT

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
		IsClosedYear TINYINT DEFAULT(0)
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variable para obtener el xml
	DECLARE @JournalVoucherTypeId AS INT,
			@CapitationRevenueMainAccountId AS INT,
			@CapitationProfitMainAccountId INT,
			@CapitationLossMainAccountId INT,
			@AccountingForSurgical TINYINT,			
			@DistributeCapitationControls TINYINT,
			--------------------------------------
			@JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		SELECT 
			@InvoiceEntityCapitatedId = iecd.InvoiceEntityCapitatedId,
			@JournalVoucherTypeId = sb.InvoiceEntityCapitatedDistributionJournalVoucherTypeId,
			@CapitationRevenueMainAccountId = sb.CapitationRevenueMainAccountId,
			@CapitationProfitMainAccountId = sb.CapitationProfitMainAccountId,
			@CapitationLossMainAccountId = sb.CapitationLossMainAccountId,
			@AccountingForSurgical = sb.AccountingForSurgical,
			@DistributeCapitationControls = sb.DistributeCapitationControls
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		LEFT JOIN Billing.SettingsBilling sb ON iecd.OperatingUnitId = sb.IdOperatingUnit
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		/*************************************VALIDACIONES************************************/

		-- Valido que el registro se encuentre en estado registrado
		IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitatedDistribution iec WHERE iec.Id = @InvoiceEntityCapitatedDistributionId AND iec.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(iec.Status = 2, 'Confirmado', 'Anulado') as Message, '' AS Consecutive
			FROM Billing.InvoiceEntityCapitatedDistribution iec
			WHERE iec.Id = @InvoiceEntityCapitatedDistributionId
			RETURN
		END

		--- Valido que la factura monto fijo no haya sido distribuida
		IF EXISTS 
		( 
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
			WHERE iecd.Id <> @InvoiceEntityCapitatedDistributionId 
				AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
				AND iecd.Status NOT IN (3, 4)
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + iecd.Code
					FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
					WHERE iecd.Id <> @InvoiceEntityCapitatedDistributionId 
						AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
						AND iecd.Status NOT IN (3, 4)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'La factura Monto Fijo ya se encuentra en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		--- Valido que los controles de servicios no se encuentren ya distribuidas
		IF EXISTS 
		(
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
			JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd2 ON iecdd.InvoiceId = iecdd2.InvoiceId
				AND iecdd.InvoiceEntityCapitatedDistributionId <> iecdd2.InvoiceEntityCapitatedDistributionId
			JOIN Billing.InvoiceEntityCapitatedDistribution iecd2 ON iecdd2.InvoiceEntityCapitatedDistributionId = iecd2.Id AND iecd2.Status = 2
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Distribución: ' + iecd2.Code + ')'
					FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
					JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd2 ON iecdd.InvoiceId = iecdd2.InvoiceId
						AND iecdd.InvoiceEntityCapitatedDistributionId <> iecdd2.InvoiceEntityCapitatedDistributionId
					JOIN Billing.InvoiceEntityCapitatedDistribution iecd2 ON iecdd2.InvoiceEntityCapitatedDistributionId = iecd2.Id AND iecd2.Status = 2
					JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
					WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios ya se encuentran en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		--- Valido que los controles de servicios no se encuentren anulados
		IF EXISTS 
		(
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
			JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId AND 
				i.Status <> 1
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Estado: ' + IIF(i.Status = 2, 'ANULADO', 'N/A') + ')'
					FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
					JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
					WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId AND 
						i.Status <> 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios no se encuentran en estado facturado ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.VieBot vb WHERE vb.Form = 'InvoiceEntityCapitatedDistribution' AND vb.Allow = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar al menos un libro contable' as Message, '' AS Consecutive
			RETURN
		END

		IF @JournalVoucherTypeId IS NULL
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar el Tipo de Comprobante de Distribución de Ingresos de Monto Fijo para la unidad Operativa' as Message, '' AS Consecutive
			RETURN
		END

		/*************************************** PROCESO ************************************/

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher, 
			VoucherDate, 
			Status, 
			Detail, 
			EntityCode, 
			EntityId, 
			EntityName
		)
		SELECT
			@JournalVoucherTypeId, 
			iecd.DocumentDate, 
			2, 
			'Distribución de Ingresos de la Factura No. ' +  i.InvoiceNumber + ' - Código ' + iec.Code + ' - Grupo de Atención: ' + CONCAT(cg.Code, ' - ', cg.Name), 
			iecd.Code, 
			iecd.Id, 
			'InvoiceEntityCapitatedDistribution'
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		LEFT JOIN Billing.Invoice i ON iec.InvoiceId = i.Id
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		-- Reversamos el ingreso de la factura monto fijo
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, ha.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) AS IdCostCenter,
			iecd.InvoiceValue,
			0,
			'Reversión del ingreso de la Factura Monto Fijo'
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		JOIN Contract.Contract c ON cg.ContractId = c.Id
		JOIN Contract.HealthAdministrator ha ON c.HealthAdministratorId = ha.Id
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = @CapitationRevenueMainAccountId		
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		DECLARE @rows INT = 1,
				@InvoiceEntityCapitatedDistributionDetailId INT = 0,
				@RevenueControlDetailId AS INT

		WHILE @rows > 0
		BEGIN
			SELECT TOP 1
				@InvoiceEntityCapitatedDistributionDetailId = iecdd.Id,
				@RevenueControlDetailId = i.RevenueControlDetailId
			FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
			JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
				AND iecdd.Id > @InvoiceEntityCapitatedDistributionDetailId
				AND i.RevenueControlDetailId IS NOT NULL
			ORDER BY iecdd.Id

			-- Verifico que haya encontrado un resultado
			SET @rows = @@RowCount
			IF @rows = 0
			BEGIN
				BREAK
			END

			-- Distribuimos los ingresos de cada una de las facturas asociadas
			INSERT INTO @JournalVourcherDetailTmp
			(
				IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
			)
			SELECT 
				IIF(@DistributeCapitationControls = 1, @CapitationRevenueMainAccountId, IdMainAccount), 
				IdThirdParty, 
				IdCostCenter, 
				SUM(IIF(DebitValue > 0, DebitValue, IIF(CreditValue < 0, ABS(CreditValue), 0))) DebitValue, 
				SUM(IIF(CreditValue > 0, CreditValue, IIF(DebitValue < 0, ABS(DebitValue), 0))) CreditValue,
				'Distribución del control de Monto Fijo'
			FROM Billing.fnJournalVoucherIncomeInvoiceDetail(@RevenueControlDetailId, @AccountingForSurgical, 2, NULL, NULL)
			GROUP BY IIF(@DistributeCapitationControls = 1, @CapitationRevenueMainAccountId, IdMainAccount), 
				IdThirdParty, 
				IdCostCenter
		END

		-- Insertamos el valor de la utilidad o perdida, de acuerdo a lo que corresponda
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) AS IdCostCenter,
			IIF(iecd.TotalControlValue > iecd.InvoiceValue, iecd.TotalControlValue - iecd.InvoiceValue, 0),
			IIF(iecd.InvoiceValue > iecd.TotalControlValue, iecd.InvoiceValue - iecd.TotalControlValue, 0),
			IIF(iecd.InvoiceValue > iecd.TotalControlValue, 'Utilidad por distribucion Factura Monto Fijo', 'Perdida por distribucion Factura Monto Fijo')
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		JOIN Contract.Contract c ON cg.ContractId = c.Id
		JOIN Contract.HealthAdministrator ha ON c.HealthAdministratorId = ha.Id
		JOIN GeneralLedger.MainAccounts ma 
			ON (iecd.InvoiceValue > iecd.TotalControlValue AND ma.Id = @CapitationProfitMainAccountId)
				OR
				(iecd.InvoiceValue < iecd.TotalControlValue AND ma.Id = @CapitationLossMainAccountId)
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId
			AND iecd.InvoiceValue <> iecd.TotalControlValue

		--Se eliminan los registros que traen valores en cero
		delete from @JournalVourcherDetailTmp where CreditValue = 0 and DebitValue = 0

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		/************************************************************************************************************************************/

		--Se consume el sp que guarda el mvimiento contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
		select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

		--Se valida que no hayan errores en el guardado del comprobante contable
		IF @CodeMessage = '999' 
		BEGIN
			SELECT 999 as CodeMessage, @Message as Message, '' AS Consecutive
			RETURN
		END

		--Se obtiene el consecutivo que generó el comprobante contable
		DECLARE @Consecutive as VARCHAR(max) = isnull( (select cast( Consecutive as varchar(30))  from GeneralLedger.JournalVouchers where id = @IdJournalVoucherResult),0)

		/************************************************************************************************************************************/

		--Actualizo el estado de la factura
		UPDATE iecd
			SET iecd.Status = 2,
				iecd.ConfirmationUser = @CodeUser,
				iecd.ConfirmationDate = [Common].[GETDATE]()
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		SELECT 0 AS CodeMessage, 'Se confirmó correctamente' AS Message, @Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
	END CATCH
END