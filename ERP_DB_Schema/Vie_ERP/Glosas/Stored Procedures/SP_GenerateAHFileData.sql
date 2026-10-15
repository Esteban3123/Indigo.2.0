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