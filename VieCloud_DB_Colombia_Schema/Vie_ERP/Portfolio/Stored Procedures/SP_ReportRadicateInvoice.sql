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
			@DateStart = 
			TRY_PARSE(
    REPLACE(
        REPLACE(
            REPLACE(
                t.x.value('DateStart[1]','varchar(max)'),
                CHAR(160),
                ' '
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
AS DATETIME USING 'es-co'),
		@DateEnd = TRY_PARSE(
    REPLACE(
        REPLACE(
            REPLACE(
                t.x.value('DateEnd[1]','varchar(max)'),
                CHAR(160),
                ' '
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
AS DATETIME USING 'es-co'),
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de facturas radicadas ante pagadores (aseguradoras o clientes), permitiendo consultar el listado de radicaciones con sus estados, devoluciones y saldos. Recibe criterios en XML para definir el tipo de reporte (resumen o detalle por factura) y el campo de fecha por el cual filtrar (fecha de radicación, fecha del documento o fecha de factura), además de filtros opcionales por rango de fechas, clientes/pagadores, radicados específicos, estado del radicado y si tiene devolución. Consulta la vista Portfolio.VReportListRadicatedInvoice para obtener los datos de cada radicación agrupados por consecutivo de radicado, pagador, grupo de atención y régimen, calculando totales de notas crédito y saldo de facturas. Cuando el tipo de reporte es detallado (TypeReport = 1), incluye además el número y fecha de factura, código y nombre del paciente, y la información de devolución o glosa asociada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRadicateInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRadicateInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte agregado de radicados de facturas, permitiendo elegir tipo de reporte (resumen o con detalle factura/paciente), criterio de fecha y múltiples filtros (clientes, radicados, estado, devolución).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con TypeReport y FilterDateBy; @xmlFilters debe contener nodo /Data con DateStart, DateEnd, Customers, RadicateInvoices, Status y Devolution; Las fechas deben ser parseables en cultura ''es-co''; Las listas Customers y RadicateInvoices deben ser cadenas separadas por coma con enteros válidos cuando no estén vacías; Status debe contener valores CHAR(1) y Devolution valores convertibles a BIT, separados por coma', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros cuyo Status esté dentro de la lista @Table_Status y cuyo flag Devolution esté en @Table_Devolution (INNER JOIN obligatorio); Cuando @TypeReport <> 1, las columnas de factura y paciente quedan en NULL y la agregación colapsa múltiples facturas por radicado en una sola fila; El filtro de fechas siempre aplica una y solo una de las tres fechas según @FilterDateBy (1=Radicación, 2=Documento, 3=Factura); Los filtros de clientes y radicados son opcionales: si la lista viene vacía, no restringen el resultado; Ante cualquier error, devuelve un único registro con Code=''999'', mensaje y línea, en lugar de propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'radicado de factura; cliente/pagador (NitCustomer); grupo de atención (CareGroup); régimen; factura; devolución; concepto de devolución; paciente; nota crédito; saldo de factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportListRadicatedInvoice: Retorna el listado de radicados agregando CreditNoteValue, BalanceInvoice y conteo de facturas por radicado, con detalle de factura/paciente solo si @TypeReport=1; [RETURN_RESULT] (error): Cuando ocurre una excepción en TRY, retorna un result-set con Code=''999'', ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Customers distinto de cadena vacía → Activa filtro por clientes (@FilterByCustomers=1) y carga la lista de IDs de clientes en tabla temporal usando dbo.Split; si @RadicateInvoices distinto de cadena vacía → Activa filtro por radicados (@FilterByRadicateInvoices=1) y carga la lista de IDs en tabla temporal; si @FilterDateBy = 1 → Filtra por v.RadicatedDate entre @DateStart y @DateEnd (fecha de radicación); si @FilterDateBy = 2 → Filtra por v.DocumentDate entre @DateStart y @DateEnd (fecha de documento); si @FilterDateBy = 3 → Filtra por v.InvoiceDate entre @DateStart y @DateEnd (fecha de factura); si @TypeReport = 1 → Incluye en el resultado columnas detalladas de factura/paciente (InvoiceNumber, InvoiceDate, Devolution, ConceptDevolution, PatientCode, PatientName) else Esas columnas se devuelven como NULL, produciendo agrupación a nivel resumen por radicado; si Status = ''1'' / ''2'' / ''4'' / otro → Mapea a ''Sin Confirmar'' / ''Confirmado'' / ''Anulado'' / ''N/A'' respectivamente como nombre legible del estado', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.VReportListRadicatedInvoice', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRadicateInvoice';
-- GO
