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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa el copiado y pegado masivo de registros en la pantalla de cruce de anticipos contra cuentas por cobrar (CxC) de cartera. Recibe un XML con los ítems a cruzar, verifica que cada registro tenga la estructura correcta según el tipo de traslado (mismo cliente o diferente cliente), detecta duplicados por número de factura y estado de cartera, y bloquea cruces sobre facturas glosadas sin conciliar cuando así lo indican los parámetros configurados en Portfolio.SettingPortfolio. También valida que el valor a cruzar sea numérico, que exista un anticipo seleccionado, y toma la moneda oficial desde GeneralLedger.CompanySettings para controlar las operaciones en moneda extranjera; al final retorna el resultado de cada fila con su estado de validación y mensaje de error o éxito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida masivamente las filas pegadas (copy/paste) en la rejilla de cruce de un anticipo contra cuentas por cobrar, verificando estructura, duplicados, parámetros de glosa, valores numéricos, existencia de la factura, suficiencia de saldo y estado de glosa, devolviendo cada fila con su estado y mensaje.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Header con CurrencyId y PortfolioAdvanceId; El XML debe contener uno o más nodos /Data con CountFields, OperatingUnitId, ThirdPartyId/ThirdPartyNit, BillNumber, CodeAccount y Value; Debe existir al menos un registro en GeneralLedger.CompanySettings que defina la moneda oficial; Debe existir el anticipo (PortfolioAdvance) referenciado para validar el saldo total del cruce; @TransferType debe ser 1 (mismo cliente) o 2 (diferente cliente) para que la validación de estructura aplique correctamente', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El SpecificPortfolioStatus se deriva comparando ara.MainAccountId con las cuentas configuradas en AccountReceivable: AccountWithoutRadicateId=1, AccountRadicateId=3, AccountObjectionRemediedId=4, AccountConciliationId=12, AccountHardCollectionId=15, AccountLegalCollectionId=16; Todos los valores monetarios (InvoiceValue, Balance, Value) se normalizan a la moneda del anticipo (@CurrencyId) usando Common.CurrencyConverter, defaulteando a la moneda oficial de la empresa cuando falta; La TRM se calcula como la conversión de 1 unidad desde @CurrencyId hacia la moneda original de la cuenta por cobrar; Las filas inválidas conservan StatusField = 0 y un mensaje descriptivo; sólo las filas con StatusField = 1 son evaluadas en validaciones posteriores; Ante cualquier excepción, se descarta el contenido y se retorna una sola fila con StatusField = 0 y el mensaje de error con su línea; El procedimiento solo valida; no realiza INSERT/UPDATE/DELETE sobre tablas físicas del modelo', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce anticipo vs cuentas por cobrar; Anticipo de cartera; Factura; Estado de cartera (sin radicar, radicada, glosada sin conciliar, glosada conciliada, cuenta de difícil recaudo, cobro jurídico); Glosa; Traslado mismo cliente / diferente cliente; Saldo de factura; Conversión de moneda / TRM; Centro de costo; Tercero / NIT; Cuenta contable principal', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PortfolioAdvanceId IS NULL OR @PortfolioAdvanceId = 0 → Limpia la tabla y devuelve un único registro con mensaje ''No hay un anticipo seleccionado''; si @TransferType = 1 (mismo cliente) y CountFields < 3, o @TransferType = 2 (diferente cliente) y CountFields < 4 → Marca la fila como inválida con mensaje ''no tiene la estructura válida''; si Existe SettingPortfolio.Transfers = 0 para la unidad operativa y alguna fila tiene SpecificPortfolioStatus = 4 (Glosada sin Conciliar) → Invalida la fila indicando que no se puede afectar el saldo de facturas en glosa sin conciliar según parámetros de cartera; si PortfolioAdvance.Balance < SUM(Value) de las filas válidas → Vacía resultados y devuelve mensaje ''El saldo del anticipo es menor al total del valor de los cruces''; si tx.Value > tx.Balance tras conversión de moneda → Invalida la fila indicando que la factura no tiene saldo suficiente para el cruce; si Factura tiene registro en GlosaPortfolioGlosada con State IN (1,3,4,5,6,7,15) y BalanceGlosa > 0 → Invalida la fila reportando el estado específico de la glosa (pendiente recepción, oficio, reiteración, conciliación, cobro jurídico, etc.); si tx.AccountReceivableId IS NULL tras el LEFT JOIN con AccountReceivable → Invalida indicando que la factura no existe con el estado especificado o no está relacionada con el cliente', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.SettingPortfolio; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; GeneralLedger.MainAccounts; Common.ThirdParty; Portfolio.PortfolioAdvance; Glosas.GlosaPortfolioGlosada; Payroll.CostCenter; Common.CurrencyConverter', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransfer';
-- GO
