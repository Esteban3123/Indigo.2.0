-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-24
-- Description:	Procedimiento para el reporte de listado de radicados
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportRadicateInvoice]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --			
			@TypeReport INT,
			@FilterDateBy INT,
			-- FILTROS --
			@DateStart DATETIME,
			@DateEnd DATETIME,
			@Customers VARCHAR(MAX),
			@RadicateInvoices VARCHAR(MAX),
			@Status VARCHAR(MAX),
			@Devolution VARCHAR(MAX),
			-------------
			@FilterByCustomers BIT = 0,
			@FilterByRadicateInvoices BIT = 0

	DECLARE @Table_Customers AS TABLE(Id INT)
	DECLARE @Table_RadicateInvoices AS TABLE(Id INT)
	DECLARE @Table_Status AS TABLE(Status CHAR(1))
	DECLARE @Table_Devolution AS TABLE(Devolution BIT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 			
			@TypeReport = t.x.value('TypeReport[1]','int'),
			@FilterDateBy = t.x.value('FilterDateBy[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@DateStart = TRY_PARSE(t.x.value('DateStart[1]','varchar(max)') AS DATETIME USING 'es-co'),
			@DateEnd = TRY_PARSE(t.x.value('DateEnd[1]','varchar(max)') AS DATETIME USING 'es-co')	,
			@Customers = t.x.value('Customers[1]','varchar(max)'),
			@RadicateInvoices = t.x.value('RadicateInvoices[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','varchar(max)'),
			@Devolution = t.x.value('Devolution[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		--Se Cargan las listas enviadas

		IF @Customers <> ''
		BEGIN
			SET @FilterByCustomers = 1

			INSERT INTO @Table_Customers
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Customers, ',')
		END

		IF @RadicateInvoices <> ''
		BEGIN
			SET @FilterByRadicateInvoices = 1

			INSERT INTO @Table_RadicateInvoices
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@RadicateInvoices, ',')
		END

		INSERT INTO @Table_Status
			SELECT CAST(Data AS CHAR(1)) Data 
			FROM dbo.Split(@Status, ',')

		INSERT INTO @Table_Devolution
			SELECT CAST(Data AS BIT) Data 
			FROM dbo.Split(@Devolution, ',')

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
			v.RadicatedConsecutive,
			v.NitCustomer,
			v.NameCustomer,
			v.RadicatedDate,
			v.CodeCareGroup,
			v.NameCareGroup,
			CASE v.Status
				WHEN '1' THEN 'Sin Confirmar'
				WHEN '2' THEN 'Confirmado'
				WHEN '4' THEN 'Anulado'
				ELSE 'N/A'
			END StatusName,
			v.RegimenName,
			IIF(@TypeReport = 1, v.InvoiceNumber, NULL) InvoiceNumber,
			IIF(@TypeReport = 1, v.InvoiceDate, NULL) InvoiceDate,
			IIF(@TypeReport = 1, v.Devolution, NULL) Devolution, 
			IIF(@TypeReport = 1, v.ConceptDevolution, NULL) ConceptDevolution,
			IIF(@TypeReport = 1, v.PatientCode, NULL) PatientCode,
			IIF(@TypeReport = 1, v.PatientName, NULL) PatientName,
			SUM(v.CreditNoteValue) CreditNoteValue,
			SUM(v.BalanceInvoice) BalanceInvoice,
			COUNT(v.RadicatedConsecutive) Invoices
		FROM Portfolio.VReportListRadicatedInvoice v
		JOIN @Table_Status s ON v.Status = s.Status
		JOIN @Table_Devolution d ON v.Devolution = d.Devolution
		LEFT JOIN @Table_Customers c ON v.IdCustomer = c.Id
		LEFT JOIN @Table_RadicateInvoices r ON v.RadicatedId = r.Id
		WHERE (
				(@FilterDateBy = 1 AND v.RadicatedDate BETWEEN @DateStart AND @DateEnd)
				OR
				(@FilterDateBy = 2 AND v.DocumentDate BETWEEN @DateStart AND @DateEnd)
				OR
				(@FilterDateBy = 3 AND v.InvoiceDate BETWEEN @DateStart AND @DateEnd)
			)
			AND (@FilterByCustomers = 0 OR c.Id IS NOT NULL)
			AND (@FilterByRadicateInvoices = 0 OR r.Id IS NOT NULL)
		GROUP BY v.RadicatedConsecutive, v.NitCustomer, v.NameCustomer, v.RadicatedDate, v.CodeCareGroup, v.NameCareGroup, v.Status, v.RegimenName,
			IIF(@TypeReport = 1, v.InvoiceNumber, NULL), IIF(@TypeReport = 1, v.InvoiceDate, NULL),
			IIF(@TypeReport = 1, v.Devolution, NULL), IIF(@TypeReport = 1, v.ConceptDevolution, NULL),
			IIF(@TypeReport = 1, v.PatientCode, NULL), IIF(@TypeReport = 1, v.PatientName, NULL)
		OPTION (RECOMPILE)

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END