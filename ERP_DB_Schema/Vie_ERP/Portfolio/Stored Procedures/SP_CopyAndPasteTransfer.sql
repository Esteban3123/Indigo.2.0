-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de Cruce Anticipo vs CxC
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_CopyAndPasteTransfer] 
	@XmlObject XML,
	@TransferType INT,
	@CompanyType INT
AS
BEGIN
	SET NOCOUNT ON
	
	/*************************************************** VARIABLES ***************************************************/

	--Datos para recorrer los registros
	DECLARE @Rows INT,
			@RowId INT, 
			----------------------------
			@Row_Details INT,
			@AccountPayableShareId INT,
			@ShareBalance NUMERIC(20,4),
			@CrossShareBalance NUMERIC(20,4),
			@CrossShareValue NUMERIC(20,4),
			@PortfolioAdvanceId INT,
			@CurrencyId INT,
			@OffcialCurrencyId INT

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject table
	(
		Id INT IDENTITY PRIMARY KEY,
		CountFields INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-----------------------------------------------------------------------
		OperatingUnitId INT,
		ThirdPartyId INT, 
		ThirdPartyNit VARCHAR(20), 
		InvoiceNumber VARCHAR(20), 
		--MainAccountNumber VARCHAR(50), 
		SpecificPortfolioStatus TINYINT,
		ValueString VARCHAR(20),
		-----------------------------------------------------------------------
		AccountReceivableId INT, 
		PortfolioStatus INT,
		MainAccountId INT,
		CostCenterId INT, 
		InvoiceValue DECIMAL(18,2), 
		Balance DECIMAL(18,2), 
		Value DECIMAL(18,2),
		CurrencyId INT,
		TRMValue NUMERIC(20,5)
	)

	BEGIN TRY
	/****************************** HEADER ******************************************************************************************************/
		SELECT
		@CurrencyId =t.x.value('CurrencyId[1]','INT'),
		@PortfolioAdvanceId= t.x.value('PortfolioAdvanceId[1]','INT')
		FROM @XmlObject.nodes('/Header') t(x)

		SELECT TOP 1 @OffcialCurrencyId= cs.OfficialCurrencyId
		FROM GeneralLedger.CompanySettings cs

	/**************************************************************************************************************************************/
		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, OperatingUnitId,ThirdPartyId, ThirdPartyNit, InvoiceNumber, SpecificPortfolioStatus, ValueString)
			SELECT	t.x.value('CountFields[1]','int') as CountFields,
					t.x.value('StatusField[1]','int') as StatusField,
					t.x.value('MessageField[1]','varchar(100)') as MessageField,
					-----------------------------------------------------------
					t.x.value('OperatingUnitId[1]','int') as OperatingUnitId,
					t.x.value('ThirdPartyId[1]','int') as ThirdPartyId,
					t.x.value('ThirdPartyNit[1]','varchar(20)') as ThirdPartyNit,
					t.x.value('BillNumber[1]','varchar(20)') as BillNumber,
					t.x.value('CodeAccount[1]','varchar(50)') as CodeAccount,
					t.x.value('Value[1]','varchar(20)') as Value
			FROM @XmlObject.nodes('/Data') t(x)

		/*******************************************  VALIDACIONES MASIVAS *******************************************/
		/****--Se valida que venga el anticipo para valida que el valor del cruce no sea mayor al valor del anticipo--*/
			IF @PortfolioAdvanceId IS NULL OR @PortfolioAdvanceId=0 BEGIN			
				DELETE @TableXmlObject
				INSERT INTO @TableXmlObject(StatusField, MessageField)
				VALUES (0, 'No hay un anticipo seleccionado')
				--RETURN SELECT * from @TableXmlObject
			END			
		/*------------------------------------------------------------------------------------------------------*/
		-- Validar el numero de columnas de acuerdo al tipo de traslado
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('El registro ', tx.Id, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1 AND
		(
			(@TransferType = 1 AND tx.CountFields < 3) -- Mismo Cliente
			OR
			(@TransferType = 2 AND tx.CountFields < 4) -- Diferente Cliente
		)

		-- Validar que el registro no se encuentre duplicado
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('Existe mas de un registro asociado a la Factura: ', tx.InvoiceNumber, ' - En el estado: ',	CASE tx.SpecificPortfolioStatus
																																		WHEN 1 THEN '1-Sin Radicar'
																																		WHEN 3 THEN	'3-Radicada Entidad'
																																		WHEN 4 THEN '4-Glosada sin Conciliar'
																																		WHEN 12 THEN '12-Glosada Conciliada'
																																		WHEN 15 THEN '15-Cuenta de Dificil Recaudo'
																																		WHEN 16 THEN '16-Cobro Jurídico'
																																		ELSE 'N/A'
																																		END)
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT InvoiceNumber, SpecificPortfolioStatus
			FROM @TableXmlObject
			WHERE StatusField = 1
			GROUP BY InvoiceNumber, SpecificPortfolioStatus
			HAVING COUNT(1) > 1
		) txd ON tx.InvoiceNumber = txd.InvoiceNumber AND tx.SpecificPortfolioStatus = txd.SpecificPortfolioStatus
		WHERE tx.StatusField = 1

		--Validamos que no se afecten saldo en glosa son conciliar si así esta el parametro
		UPDATE tx	
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('No se puede afectar el saldo de la factura ',tx.InvoiceNumber,' en estado de glosa sin conciliar ya que así esta definido en parámetros de cartera')
		FROM @TableXmlObject tx
		JOIN Portfolio.SettingPortfolio sp ON sp.OperatingUnitId = tx.OperatingUnitId
		WHERE sp.Transfers = 0
		AND EXISTS 
		(
			SELECT 1
			FROM @TableXmlObject
			WHERE SpecificPortfolioStatus = 4
		)

		-- Validar que el valor a cruzar se numerico
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('El valor a cruzar del registro ', tx.Id, ' es invalido')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1
			AND ISNUMERIC(tx.ValueString) <> 1

		-- Actualizamos el valor a cruzar en el respectivo campo numerico
		UPDATE tx
			SET tx.Value = CAST(tx.ValueString AS DECIMAL(18,2))
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1
		
		-- Validar que el valor a cruzar sea mayor a 0
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('El valor a cruzar del registro ', tx.Id, ' debe ser mayor a 0')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1
			AND NOT (tx.Value > 0)

		-- Asignar los valores de la factura
		UPDATE tx
			SET tx.AccountReceivableId = ar.AccountReceivableId,
				tx.PortfolioStatus = ar.PortfolioStatus,
				tx.MainAccountId = ar.MainAccountId,
				tx.ThirdPartyId = ar.ThirdPartyId,
				tx.CostCenterId = ar.CostCenterId,
				tx.InvoiceValue = Common.CurrencyConverter(ar.Value,ISNULL(ar.CurrencyId,@OffcialCurrencyId),ISNULL(@CurrencyId,@OffcialCurrencyId))  ,
				tx.Balance = Common.CurrencyConverter( ar.Balance,ISNULL(ar.CurrencyId,@OffcialCurrencyId),ISNULL(@CurrencyId,@OffcialCurrencyId)),
				tx.Value= Common.CurrencyConverter( tx.Value,ISNULL(ar.CurrencyId,@OffcialCurrencyId),ISNULL(@CurrencyId,@OffcialCurrencyId)),
				tx.SpecificPortfolioStatus = ar.SpecificPortfolioStatus,
				tx.CurrencyId= ISNULL(ar.CurrencyId,@OffcialCurrencyId),
				tx.TRMValue = Common.CurrencyConverter(1,ISNULL(@CurrencyId,@OffcialCurrencyId),ISNULL(ar.CurrencyId,@OffcialCurrencyId))
		FROM @TableXmlObject tx
		LEFT JOIN 
		(
			SELECT	ar.Id AccountReceivableId,
					ar.InvoiceNumber,
					ar.PortfolioStatus,
					ara.MainAccountId,
					ma.Number,
					ar.ThirdPartyId,
					tp.Nit ThirdPartyNit,
					ara.CostCenterId,
					ar.Value,
					ara.Balance,
					CASE ara.MainAccountId
						WHEN ar.AccountWithoutRadicateId THEN 1
						WHEN ar.AccountRadicateId THEN 3
						WHEN ar.AccountObjectionRemediedId THEN 4
						WHEN ar.AccountConciliationId THEN 12
						WHEN ar.AccountHardCollectionId THEN 15
						WHEN ar.AccountLegalCollectionId THEN 16
						ELSE 0
					END SpecificPortfolioStatus,
					ar.CurrencyId
			FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
			JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON ar.Id = ara.AccountReceivableId
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ara.MainAccountId = ma.Id
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		) ar ON tx.InvoiceNumber = ar.InvoiceNumber  AND tx.SpecificPortfolioStatus = ar.SpecificPortfolioStatus AND (tx.ThirdPartyId = ar.ThirdPartyId OR tx.ThirdPartyNit = ar.ThirdPartyNit) 
		WHERE tx.StatusField = 1

		/*-------------se valida que el total del cruce no supere el anticipo----------------------*/
			IF EXISTS(	SELECT 1
						FROM portfolio.PortfolioAdvance pa
						WHERE pa.id=@PortfolioAdvanceId AND (pa.Balance <(SELECT SUM(x.Value) FROM @TableXmlObject x WHERE x.StatusField = 1))) BEGIN

				DELETE @TableXmlObject
				INSERT INTO @TableXmlObject(StatusField, MessageField)
				VALUES (0, 'El saldo del anticipo es menor al total del valor de los cruces')
				--RETURN SELECT * from @TableXmlObject
			END
		/*-----------------------------------------------------------------------------------------*/


		 --Validar que la factura exista y este asociada a la cuenta contable y el cliente
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('La factura ', tx.InvoiceNumber, ' del registro ', tx.Id, ' no existe con el estado especificado o no se encuentra relacionada con el cliente')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1 AND tx.AccountReceivableId IS NULL

		 --Validar que el saldo sea suficiente para el valor a cruzar
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('La factura ', tx.InvoiceNumber, ' del registro ', tx.Id, ' no tiene saldo (', FORMAT(tx.Balance, 'C2', 'es-CO'), ') suficiente para realizar el cruce (', FORMAT(tx.Value, 'C2', 'es-CO'), ')')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 1 AND tx.Value > tx.Balance

		--Si la factura se encuentra glosada, se valida el estado de la glosa
		UPDATE tx
			SET tx.StatusField = 0,
				tx.MessageField = CONCAT('La factura ', tx.InvoiceNumber, ' del registro ', tx.Id, ' se encuentra ', CASE gpg.State 
										WHEN 1 THEN 'Pendiente confirmar recepcion'  
										WHEN 3 THEN 'Pendiente envio de oficio'  
										WHEN 4 THEN 'Pendiente confirmar reiteracion'  
										WHEN 5 THEN 'Pendiente evaluacion reiteracion'  
										WHEN 6 THEN 'Pendiente conciliacion'  
										WHEN 7 THEN 'Pendiente confirmar factura conciliacion' 
										WHEN 15 THEN 'Factura cobro juridico'
									END)
		FROM @TableXmlObject tx
		JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON tx.InvoiceNumber = gpg.InvoiceNumber 
		WHERE tx.StatusField = 1  AND gpg.State IN ('1','3','4','5','6','7','15') AND gpg.BalanceGlosa > 0 
	END TRY
	BEGIN CATCH
		DELETE @TableXmlObject

		INSERT INTO @TableXmlObject(StatusField, MessageField)
			VALUES (0, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	tx.Id,
			tx.AccountReceivableId,
			tx.InvoiceNumber,			
			tx.MainAccountId,
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			tx.ThirdPartyId,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			tx.CostCenterId,
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			tx.InvoiceValue,
			tx.Balance, 
			tx.Value,
			tx.SpecificPortfolioStatus,
			CASE tx.SpecificPortfolioStatus
				WHEN 1 THEN '1-Sin Radicar'
				WHEN 3 THEN	'3-Radicada Entidad'
				WHEN 4 THEN '4-Glosada sin Conciliar'
				WHEN 12 THEN '12-Glosada Conciliada'
				WHEN 15 THEN '15-Cuenta de Dificil Recaudo'
				WHEN 16 THEN '16-Cobro Jurídico'
				END PortfolioStatusName,
			tx.TRMValue,
			c.Abbreviation AS CurrencyAbbreviation,
			CONCAT(TRIM(i.PatientCode), ' - ', pa.IPNOMCOMP) AS Patient,
			----------------------------------------------------------------------------
			tx.StatusField, 
			tx.MessageField
	FROM @TableXmlObject tx
	LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tx.MainAccountId = ma.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON tx.ThirdPartyId = tp.Id
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON tx.CostCenterId = cc.Id
	LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON tx.AccountReceivableId = ar.Id
	LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ar.CurrencyId, (SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings))
	LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
	LEFT JOIN dbo.INPACIENT pa WITH (NOLOCK) ON i.PatientCode = pa.IPCODPACI
END