-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AH de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAHFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices as XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(50)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		ViaIncome INT, 
		AdmissionDate VARCHAR(50), 
		AdmissionTime VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		ExternalCause VARCHAR(2), 
		DiagnosticAdmission VARCHAR(50), 
		DiagnosticEgress VARCHAR(50), 
		DiagnosticComplication VARCHAR(50), 
		OutputState INT, 
		CauseDeath VARCHAR(50), 
		OutDate VARCHAR(50), 
		OutTime VARCHAR(50)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i ON t.x.value('InvoiceId[1]','int') = i.Id
			WHERE i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				cc.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			JOIN Billing.Invoice cc WITH (NOLOCK) ON i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType = 4
				AND cc.DocumentType = 5

		/*******************************************************************************************/

		INSERT INTO @TableResult 
			SELECT DISTINCT 
				xi.InvoiceNumber as InvoiceNumber, 
				CA.CODIPSSEC AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode, 
				RTRIM(pacient.IPCODPACI) as IdentificationNumber, 
				CASE admission.IINGREPOR 
					WHEN 1 THEN 1 
					WHEN 2 THEN 2 
					WHEN 3 THEN 4 
					WHEN 4 THEN 3 
					WHEN 5 THEN 2 
					ELSE 0 
				END as ViaIncome, 
				CONVERT(VARCHAR(10),admission.IFECHAING,103) as AdmissionDate, 
				CONVERT(VARCHAR(5),admission.IFECHAING,114) as AdmissionTime, 
				ISNULL(RTRIM(admission.IAUTORIZA), '') as AuthorizationNumber, 
				CASE ITIPORIES 
					WHEN 2 THEN '02' 
					WHEN 3 THEN '06' 
					WHEN 5 THEN '01' 
					WHEN 6 THEN '14' 
					WHEN 8 THEN '05' 
					WHEN 9 THEN '07' 
					WHEN 10 THEN '08' 
					WHEN 11 THEN '09' 
					WHEN 13 THEN '15' 
					WHEN 14 THEN '03' 
					WHEN 15 THEN '04' 
					WHEN 16 THEN '10' 
					WHEN 17 THEN '11' 
					WHEN 18 THEN '12' 
					ELSE '13' 
				END as ExternalCause, 
				ISNULL(admission.CODDIAING,'Z000') as DiagnosticAdmission, 
				ISNULL(I.OutputDiagnosis,'Z000') as DiagnosticEgress, 
				'' as DiagnosticComplication, 
				CASE egress.ESTPACEGR WHEN 3 THEN 2 ELSE 1 END as OutputState, 
				CASE egress.ESTPACEGR WHEN 3 THEN admission.CODDIAEGR ELSE '' END as CauseDeath, 
				CONVERT(VARCHAR(10),egress.FECALTPAC,103) as OutDate, 
				CONVERT(VARCHAR(5),egress.FECALTPAC,114) as OutTime 
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN dbo.INPACIENT AS pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN
			(
				SELECT ID.InvoiceId, SO.AdmissionNumber
				FROM Billing.InvoiceDetail ID WITH (NOLOCK)
				JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
				JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
				GROUP BY ID.InvoiceId, SO.AdmissionNumber
			) SO ON I.Id = SO.InvoiceId
			JOIN dbo.ADINGRESO AS admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.ADCENATEN AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE 
			JOIN dbo.HCREGEGRE AS egress WITH (NOLOCK) ON admission.NUMINGRES = egress.NUMINGRES 
			JOIN dbo.HCHISPACA AS HOSP WITH (NOLOCK) ON HOSP.NUMINGRES = admission.NUMINGRES 
			WHERE admission.TIPOINGRE = 2 
				AND I.[Status] = 1
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AH de RIPS correspondiente a hospitalizaciones (ingresos de tipo 2). Recibe un ID de radicado de cartera y/o un XML con facturas, consolida las facturas activas (no anuladas, no notas crédito) cruzando radicados de cobro, facturas de capitalización y sus asociadas, y retorna por cada factura los datos reglamentarios de hospitalización: código IPS, tipo y número de identificación del paciente (cédula), vía de ingreso, fechas y horas de admisión y egreso, número de autorización, causa externa, diagnósticos de ingreso y egreso codificados en CIE-10, estado de salida y causa de muerte si aplica. Se usa para la generación del archivo plano AH del reporte RIPS exigido por la normativa colombiana de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAHFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAHFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del archivo AH de RIPS (hospitalización) consolidando ingresos, egresos y diagnósticos asociados a las facturas de un radicado o lista XML.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de facturas debe respetar el nodo /Data con InvoiceId entero; Las facturas deben existir en Billing.Invoice; Para incluirse, el ingreso debe ser de tipo hospitalario (TIPOINGRE = 2); Las facturas deben tener Status = 1 (activas); Las facturas deben tener tipo de documento distinto de 4 (no nota crédito), salvo el caso de capitación donde se cruzan documentos tipo 4 con tipo 5; Los detalles de radicación deben tener State distinto de 4 (no anulados)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan ingresos hospitalarios (TIPOINGRE=2); Solo se procesan facturas activas (Status=1); Las facturas anuladas en el detalle de radicación (State=4) nunca se incluyen; DiagnosticComplication siempre se devuelve vacío; Los diagnósticos de ingreso y egreso nunca quedan nulos: por defecto ''Z000''; La causa externa siempre tiene un valor de 2 caracteres (''13'' como default); Las fechas se entregan en formato dd/mm/yyyy (estilo 103) y horas hh:mi (estilo 114); Para documentos tipo 4 (capitación) se sustituye por la factura tipo 5 vinculada por tercero, grupo de cuidado y categoría dentro del periodo de capitación; Los errores no abortan la ejecución: se imprimen y el procedimiento devuelve el resultado parcial', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AH (hospitalización); Radicación de facturas; Ingreso hospitalario; Egreso del paciente; Causa externa de atención; Diagnóstico de ingreso y egreso; Causa de muerte; Número de autorización; Vía de ingreso; Capitación; Centro de atención (IPS); Tipo de identificación del paciente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve los datos AH de RIPS solo para facturas con TIPOINGRE=2 y Status=1, ordenados por la parte numérica del InvoiceNumber; [RAISERROR] STDOUT: Ante cualquier error captura y hace PRINT del mensaje y línea, sin relanzar la excepción (el error se silencia)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentType <> 4 (factura no es nota crédito/capitación) → Se incluye directamente la factura (vía XML o vía radicado); si DocumentType = 4 en factura del radicado y existe factura cc con DocumentType = 5 del mismo tercero, grupo y categoría dentro del rango Capitation → Se incluye la factura cc (capitación) como sustituto en lugar de la nota; si admission.IINGREPOR IN (1..5) → Mapea vía de ingreso: 1→1, 2→2, 3→4, 4→3, 5→2; cualquier otro valor → 0; si ITIPORIES con valores específicos (2,3,5,6,8..18) → Mapea a códigos de causa externa RIPS (''01''..''15''); en cualquier otro caso → ''13''; si egress.ESTPACEGR = 3 → OutputState=2 (muerte) y CauseDeath=admission.CODDIAEGR else OutputState=1 y CauseDeath=''''; si admission.CODDIAING es NULL → DiagnosticAdmission se fija en ''Z000''; si I.OutputDiagnosis es NULL → DiagnosticEgress se fija en ''Z000''; si admission.IAUTORIZA es NULL → AuthorizationNumber se devuelve como cadena vacía', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; dbo.ADCENATEN; dbo.HCREGEGRE; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAHFileData';
-- GO
