-- =============================================
-- Author:		Giovanny plazas
-- Create date: 2021-02-08
-- Description:	Procedimiento para el reporte de alistamiento de conciliacion de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioReconciliation]

		@HisContainer AS VARCHAR(20),
		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			-- CRITERIOS --
			@IncludeAdvance INT,
			-- FILTROS --
			@ThirdParties VARCHAR(MAX),
			@OperatingUnits VARCHAR(MAX),
			@DocumentTypes VARCHAR(MAX),
			@Status VARCHAR(MAX),
			@InvoiceCategories VARCHAR(MAX),
			@CareGroup VARCHAR(MAX),
			@ClosingDate DATE,
			
			---------------------------------------------------------------------------------------
			@FilterByOperatingUnit BIT = 0,
			@FilterByInvoiceCategories BIT = 0,
			@FilterByCareGroup BIT = 0,
			@FilterByThirdParty BIT = 0,
			@FilterByDocumentType BIT = 0,
			@FilterByStatus BIT = 0

	DECLARE @Table_OperatingUnit AS TABLE(Id INT)
	DECLARE @Table_InvoiceCategories AS TABLE(Id INT)
	DECLARE @Table_CareGroup AS TABLE(Id INT)
	DECLARE @Table_ThirdParty AS TABLE(Id INT)
	DECLARE @Table_DocumentType AS TABLE(Id INT)
	DECLARE @Table_Status AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@IncludeAdvance = t.x.value('IncludeAdvance[1]','bit')
				FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@ClosingDate = t.x.value('ClosingDate[1]','date'),
				---------------------------------------------------------------------------------------
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)'),
				@OperatingUnits = t.x.value('OperatingUnits[1]','varchar(max)'),
				@DocumentTypes = t.x.value('DocumentTypes[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','varchar(max)'),
				@InvoiceCategories = t.x.value('InvoiceCategories[1]','varchar(max)'),
				@CareGroup = t.x.value('CareGroup[1]','varchar(max)')
				FROM @xmlFilters.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------
		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParty = 1

			INSERT INTO @Table_ThirdParty
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		IF ISNULL(@OperatingUnits, '') <> ''
		BEGIN
			SET @FilterByOperatingUnit = 1

			INSERT INTO @Table_OperatingUnit
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@OperatingUnits, ',')
		END

		IF ISNULL(@DocumentTypes, '') <> ''
		BEGIN
			SET @FilterByDocumentType = 1

			INSERT INTO @Table_DocumentType
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@DocumentTypes, ',')
		END

		IF ISNULL(@Status, '') <> ''
		BEGIN
			SET @FilterByStatus = 1

			INSERT INTO @Table_Status
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@Status, ',')
		END

		IF ISNULL(@InvoiceCategories, '') <> ''
		BEGIN
			SET @FilterByInvoiceCategories = 1

			INSERT INTO @Table_InvoiceCategories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@InvoiceCategories, ',')
		END

		IF ISNULL(@CareGroup, '') <> ''
		BEGIN
			SET @FilterByCareGroup = 1

			INSERT INTO @Table_CareGroup
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@CareGroup, ',')
		END
		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
			d.*
		FROM
		(
				SELECT	ar.Id,
						ar.InvoiceNumber AS DocumentCode, 
						ar.AccountReceivableDate, 
						ar.AccountReceivableType,
						ar.PortfolioStatus,
						ar.PortfolioStatusName,
						ar.NumberShares,
						ar.Term,
						ar.OpeningBalance,
						tp.Nit AS ThirdPartyNit, 
						tp.Name AS ThirdPartyName, 
						p.IdentificationType,
						ic.Code + ' - ' + ic.[Name] AS Category, 
						cg.EntityType AS Regimen, 
						cg.Code AS CareGroupCode,
						cg.Name AS CareGroupName, 
						ct.Code AS ContractCode,
						ct.ContractName AS ContractName,
						ar.AccountWithoutRadicateNumber, 
						ar.RadicatedConsecutive,
						ar.RadicatedUser,
						ar.RadicatedDate,
						ar.RadicatedState,
						ma.Number AS MainAccountNumber, 
						ma.Name AS MainAccountName,
						ar.DocumentValue, 
						ar.RetentionValue,
						ar.InitialValue,
						ar.DebitValue,
						ar.CreditValue,
						ar.TransferValue,
						ar.CashReceiptValue,
						ar.CrossingValue,
						ar.Balance,
						ar.CurrentBalance,
						c.NOMCENATE AS CenterAttention,
						ar.RegimenCalculated,
						gpg.PatientName As PatientName,
						gpg.Nit AS	PatientNit,
						ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
						ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
						ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
						gpg.State GlosaState,
						CASE gpg.state 
							WHEN 1 THEN 'Pendiente Confirmar Glosa'
							WHEN 2 THEN 'Pendiente Evaluacion Glosa'
							WHEN 3 THEN 'Pendiente envio de oficio'
							WHEN 4 THEN 'Pendiente confirmar reiteracion'
							WHEN 5 THEN 'Pendiente evaluacion reitreacion'
							WHEN 6 THEN 'Pendiente conciliacion'
							WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
							WHEN 8 THEN 'Conciliada'
							WHEN 9 THEN 'Conciliada Parcialmente'
							WHEN 11 THEN 'Glosa con Respuesta'
							WHEN 12 THEN 'Reiteracion con respuesta'
							WHEN 13 THEN 'Pendiente confirmar pago parcial'
							WHEN 14 THEN 'Confirmado pago parcial'
							WHEN 15 THEN 'Cobro juridico'
							ELSE 'No esta Glosada'
						END as GlosaStateName
				FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
				JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
				JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
				LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
				LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
				LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
				LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
				/********************************** ******* **********************************/
				LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
				LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
				LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
				/**************************************** FILTROS ****************************************/
				LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
				LEFT JOIN @Table_InvoiceCategories tpt ON ar.InvoiceCategoryId = tpt.Id
				LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
				LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
				LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
				LEFT JOIN @Table_CareGroup cp ON ar.CareGroupId = cp.Id
				WHERE (@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
					AND (@FilterByInvoiceCategories = 0 OR tpt.Id IS NOT NULL)
					AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
					AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
					AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
					AND (@FilterByCareGroup = 0 OR cp.Id IS NOT NULL)
					AND ar.Balance <> 0
			
			UNION ALL

				SELECT	pa.Id,
						pa.Code AS DocumentCode, 
						pa.DocumentDate AS AccountReceivableDate, 
						0 AS AccountReceivableType,
						0 AS PortfolioStatus,
						NULL PortfolioStatusName,
						0 AS NumberShares,
						0 AS Term,
						0 AS OpeningBalance,
						pa.ThirdPartyNit, 
						pa.ThirdPartyName, 
						pa.IdentificationType,
						NULL AS Category, 
						NULL AS Regimen, 
						NULL AS CareGroupCode,
						NULL AS CareGroupName, 
						NULL AS ContractCode,
						NULL AS ContractName,
						NULL AS AccountWithoutRadicateNumber, 
						NULL AS RadicatedConsecutive,
						NULL AS RadicatedUser,
						NULL AS RadicatedDate,
						NULL RadicatedState,
						ma.Number AS MainAccountNumber, 
						ma.Name AS MainAccountName,
						pa.DocumentValue, 
						0 RetentionValue,
						0 InitialValue,
						pa.DebitValue,
						pa.CreditValue,
						pa.TransferValue,
						0 AS CashReceiptValue,
						0 AS CrossingValue,
						pa.Balance,
						pa.CurrentBalance,
						NULL AS CenterAttention,
						'' AS RegimenCalculated,
						Null AS PatientName,
						NULL AS PatientNit,
						0 ValueGlosado,
						0 ValueAcceptedFirstInstance,
						0 ValueAcceptedSecondInstance,
						NULL GlosaState,
						NULL as GlosaStateName
				FROM [Portfolio].[GetPortfolioAdvanceByAge](@ClosingDate) pa
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id AND mac.Type = 1
				/**************************************** FILTROS ****************************************/
				LEFT JOIN @Table_ThirdParty tc ON pa.ThirdPartyId = tc.Id
				WHERE @IncludeAdvance = 1
					AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
					AND pa.Balance <> 0

		) AS d
		ORDER BY 1, 2
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de alistamiento para conciliación de cartera: consolida, por factura o documento de cartera, los saldos pendientes, valores glosados, montos aceptados en primera y segunda instancia, estado de glosa y datos de radicación, cruzando la cartera por edades (GetAccountReceivableByAge) con terceros (aseguradoras/EPS), personas, categorías de facturación, grupos de atención, contratos, cuentas contables principales, glosas (GlosaPortfolioGlosada) e ingresos de pacientes (ADINGRESO). Permite filtrar por tercero, unidad operativa, tipo de documento, estado, categoría de factura, grupo de atención y fecha de corte, recibiendo criterios y filtros en formato XML. Se utiliza en el módulo de cartera para preparar la conciliación financiera entre lo facturado, lo glosado y lo efectivamente pagado por las entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioReconciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioReconciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de alistamiento para la conciliación de cartera a una fecha de corte, combinando cuentas por cobrar (con datos de tercero, contrato, grupo de atención, glosas y centro de atención) y, opcionalmente, anticipos de cartera, aplicando filtros multi-valor.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data/IncludeAdvance interpretable como bit.; @xmlFilters debe contener nodo /Data con ClosingDate y los filtros opcionales (ThirdParties, OperatingUnits, DocumentTypes, Status, InvoiceCategories, CareGroup) como listas separadas por coma de Ids enteros.; Las funciones Portfolio.GetAccountReceivableByAge y Portfolio.GetPortfolioAdvanceByAge deben existir y aceptar la fecha de corte.; La función dbo.Split debe estar disponible para parsear los CSV de filtros.; Los valores dentro de los CSV deben ser convertibles a INT (CAST(Data AS INT)).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros cuyo Balance sea distinto de cero (tanto en cartera como en anticipos).; El reporte siempre se calcula a la fecha de corte @ClosingDate, tanto para edades de cartera como para validez de glosas y radicados.; Las glosas solo se asocian si su RadicatedDate (cast a DATE) es menor o igual a la fecha de corte.; Los valores aceptados de glosa (primera/segunda instancia) solo se computan si la fecha de evaluación correspondiente ya ocurrió antes o igual a la fecha de corte; de lo contrario se fuerzan a 0.; Los anticipos solo se reportan si pertenecen a una clase de cuenta contable de Type = 1 y si @IncludeAdvance = 1.; Cada filtro opcional (Tercero, Unidad Operativa, Tipo Documento, Estado, Categoría, Grupo de atención) se aplica sólo cuando se recibió valor; si viene vacío no restringe.; El filtro de Tercero también aplica al bloque de anticipos; los demás filtros (unidad, categoría, etc.) no se aplican a anticipos.; Los errores no propagan excepción: se devuelven como resultset con Code=''999''.; El resultado final se ordena por Id y DocumentCode.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Conciliación de cartera; Cuentas por cobrar; Anticipos de cartera; Glosas (primera y segunda instancia, reiteración, conciliación); Radicación de cuentas; Categorías de factura; Grupo de atención (CareGroup); Contrato; Centro de atención; Régimen; Plan de cuentas (Main Accounts); Tercero / NIT; Paciente', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un único resultset con la cartera por edades unida (UNION ALL) a los anticipos, ordenado por Id y DocumentCode, excluyendo registros con Balance = 0.; [RETURN_RESULT] (resultset de error): Cuando ocurre una excepción dentro del TRY, retorna un resultset alternativo con columnas Code=''999'', Message=ERROR_MESSAGE(), Line=ERROR_LINE().; [INSERT] @Table_* (variables de tabla): Cuando el filtro CSV correspondiente no es vacío, inserta los Ids parseados con dbo.Split en las tablas temporales @Table_ThirdParty/@Table_OperatingUnit/@Table_DocumentType/@Table_Status/@Table_InvoiceCategories/@Table_CareGroup.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' (y análogos para OperatingUnits, DocumentTypes, Status, InvoiceCategories, CareGroup) → Activa el flag de filtro correspondiente y carga la tabla temporal partiendo el string CSV con dbo.Split, restringiendo el resultado a los Ids incluidos. else El filtro queda inactivo y no restringe el resultado (LEFT JOIN con condición ''@FilterByX = 0 OR ... IS NOT NULL'').; si @IncludeAdvance = 1 → Se incluye en el reporte (UNION ALL) la cartera de anticipos obtenida de Portfolio.GetPortfolioAdvanceByAge, restringida a cuentas cuya clase contable tenga Type = 1. else Se omite el bloque de anticipos del resultado final.; si gpg.EvaluationDateGlosa <= @ClosingDate → Se reporta el ValueAcceptedFirstInstance de la glosa. else Se reporta 0 como valor aceptado en primera instancia.; si gpg.EvaluationDateReiteration <= @ClosingDate → Se reporta el ValueAcceptedSecondInstance de la glosa. else Se reporta 0 como valor aceptado en segunda instancia.; si CASE sobre gpg.State (1..15) → Traduce el código de estado de la glosa a su nombre legible (Pendiente Confirmar Glosa, Conciliada, Cobro juridico, etc.). else Si State no está en el rango mapeado se entrega ''No esta Glosada''.; si ERROR en el bloque TRY → Devuelve un resultset con Code=''999'', mensaje y línea del error en lugar de los datos del reporte.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.GetAccountReceivableByAge; Portfolio.GetPortfolioAdvanceByAge; Common.ThirdParty; Common.Person; Billing.InvoiceCategories; Contract.CareGroup; Contract.Contract; GeneralLedger.MainAccounts; Glosas.GlosaPortfolioGlosada; dbo.ADINGRESO; dbo.ADCENATEN; GeneralLedger.MainAccountClasses; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioReconciliation';
-- GO
