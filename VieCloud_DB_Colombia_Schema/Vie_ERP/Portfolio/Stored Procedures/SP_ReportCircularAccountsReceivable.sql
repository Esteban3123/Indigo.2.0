-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-19
-- Description:	Procedimiento para el reporte de la circular 014
-- =============================================
CREATE  PROCEDURE [Portfolio].[SP_ReportCircularAccountsReceivable]
	@HisContainer VARCHAR(30),
	@xmlFilters AS XML
AS
BEGIN
	DECLARE @Year INT,
			@Month INT,
			@HealthAdministratorIds VARCHAR(MAX),
			@Status VARCHAR(MAX),
			@EntityNit AS VARCHAR(20), 
			@EntityDigitVerification AS VARCHAR(1)

	BEGIN TRY

		--Se obtienen los datos de los filtros
		SELECT 
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@HealthAdministratorIds = t.x.value('HealthAdministratorIds[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT 
			@EntityNit = tp.Nit, 
			@EntityDigitVerification = tp.DigitVerification
		FROM GeneralLedger.GeneralLedgerSettings gls
		JOIN Common.ThirdParty tp ON gls.IdDian = tp.Id

		DECLARE @query VARCHAR(MAX)

		SET @Query =	N'
SELECT ''' + @EntityNit + ''' EntityNit, 
	''' + @EntityDigitVerification + ''' EntityDigitVerification,
	YEAR(ri.RadicatedDate) RadicatedYear,
	MONTH(ri.RadicatedDate) RadicatedMonth,
	IIF(rid.InvoiceDocumentType = 4, 1, 2) InvoiceType,
	rid.InvoiceNumber,
	CAST(rid.InvoiceValueEntity + rid.InvoiceValuePacient AS DECIMAL) InvoiceValue,
	ha.HealthEntityCode,
	ISNULL(ca.DEPMUNCOD,'''') MunicipalityCode,
	0 PaymentAgreement
FROM Portfolio.RadicateInvoiceC ri
LEFT JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
LEFT JOIN Billing.Invoice i ON rid.InvoiceNumber = i.InvoiceNumber
LEFT JOIN Portfolio.AccountReceivable ar on rid.InvoiceNumber = ar.InvoiceNumber
LEFT JOIN Contract.HealthAdministrator ha ON ar.ThirdPartyId = ha.ThirdPartyId
LEFT JOIN ' + @HisContainer + '.dbo.ADINGRESO ing ON i.AdmissionNumber = ing.NUMINGRES
LEFT JOIN ' + @HisContainer + '.dbo.ADCENATEN ca ON ing.CODCENATE = ca.CODCENATE
WHERE rid.InvoiceDocumentType IN (1, 2, 4) 
	AND YEAR(ri.RadicatedDate) = ' + CAST(@Year AS VARCHAR(4)) + '
	AND MONTH(ri.RadicatedDate) = ' + CAST(@Month AS VARCHAR(2)) + '
	AND ha.Id IN (' + @HealthAdministratorIds + ')
	AND ri.State IN (' + @Status + ')'
	
		EXEC(@Query)	

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de cuentas por cobrar requerido por la Circular 014 de la Superintendencia Nacional de Salud. Para un año y mes determinados, consolida las facturas radicadas ante administradoras de salud (EPS/aseguradoras), incluyendo número de factura, tipo de documento, valor total (entidad + paciente), código del municipio de atención y código de la entidad administradora. Obtiene el NIT y dígito de verificación de la entidad prestadora desde la configuración contable (GeneralLedgerSettings y ThirdParty/DIAN), y cruza datos de facturación, cartera, contratos con administradoras de salud e información de admisión e ingreso del paciente desde el contenedor HIS indicado dinámicamente. Permite filtrar por administradoras de salud específicas, estado de radicación y período, para cumplir la obligación regulatoria de reporte de cartera ante la Superintendencia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCircularAccountsReceivable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de la Circular 014 de cartera, listando facturas radicadas por periodo y administradora de salud con sus valores, ubicación geográfica y datos de la entidad reportante.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir exactamente un registro en GeneralLedger.GeneralLedgerSettings cuyo IdDian referencie un Common.ThirdParty válido para obtener NIT y dígito de verificación de la entidad reportante; El parámetro @HisContainer debe corresponder a una base de datos existente con tablas dbo.ADINGRESO y dbo.ADCENATEN (esquema HIS legacy); El XML @xmlFilters debe contener nodos /Data con Year, Month, HealthAdministratorIds y Status; HealthAdministratorIds y Status deben venir como listas válidas de valores SQL (se concatenan directamente en IN(...))', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen radicados cuyo tipo de documento sea 1, 2 o 4 (rid.InvoiceDocumentType IN (1,2,4)); El periodo del reporte se delimita por YEAR/MONTH de RadicatedDate según filtros de entrada; El NIT y dígito de verificación de la entidad reportante provienen del tercero asociado a GeneralLedgerSettings.IdDian (configuración única de la organización); PaymentAgreement siempre se reporta como 0 (no se calcula acuerdo de pago); InvoiceValue se calcula como InvoiceValueEntity + InvoiceValuePacient convertido a DECIMAL; Si no hay centro de atención asociado (ADCENATEN), MunicipalityCode se reporta como cadena vacía', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Circular 014 (reporte regulatorio de cartera); Radicación de facturas; Cuentas por cobrar; Administradora de salud (EPS/ARS); NIT y dígito de verificación de la entidad; Centro de atención y municipio (DANE); Ingreso/admisión hospitalaria', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.RadicateInvoiceC: Devuelve el resultado del SELECT dinámico con las facturas radicadas filtradas por año, mes, administradoras y estados, uniendo cartera, facturación y datos del HIS legacy (ADINGRESO/ADCENATEN); [RETURN_RESULT] (error handler): Ante cualquier error en el TRY, retorna un resultset con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE() en lugar de propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rid.InvoiceDocumentType = 4 → InvoiceType = 1 (se marca como tipo factura especial, p.ej. nota) else InvoiceType = 2 (factura estándar)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerSettings; Common.ThirdParty; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.Invoice; Portfolio.AccountReceivable; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircularAccountsReceivable';
-- GO
