-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AU de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAUFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices AS XML
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

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Histories TABLE
	(
		AdmissionNumber CHAR(10),
		Indication CHAR(2)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		AdmissionDate VARCHAR(50), 
		AdmissionTime VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		ExternalCause VARCHAR(2), 
		Diagnostic VARCHAR(50), 
		UserDestination VARCHAR(50), 
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

		INSERT INTO @Histories
			SELECT FOLIOMAX.NUMINGRES, HIS.INDICAPAC
			FROM
			(
				SELECT HP.NUMINGRES, MIN(CAST(HP.NUMEFOLIO AS INT)) AS NUMEFOLIO 
				FROM @Invoices xi
				JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON xi.InvoiceId = ID.InvoiceId
				JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
				JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
				JOIN dbo.HCHISPACA HP WITH (NOLOCK) ON SO.AdmissionNumber = HP.NUMINGRES
				WHERE INDICAPAC IN ('3','4','5','6','10','12')
				GROUP BY HP.NUMINGRES
			) FOLIOMAX
			JOIN dbo.HCHISPACA HIS WITH (NOLOCK)ON HIS.NUMINGRES = FOLIOMAX.NUMINGRES AND FOLIOMAX.NUMEFOLIO = HIS.NUMEFOLIO

		/*******************************************************************************************/

		INSERT INTO @TableResult 
			SELECT 
				xi.InvoiceNumber AS InvoiceNumber, 
				CA.CODIPSSEC AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode,
				RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
				CONVERT(VARCHAR(10),admission.IFECHAING,103) AS AdmissionDate, 
				CONVERT(VARCHAR(5),admission.IFECHAING,114) AS AdmissionTime, 
				ISNULL(RTRIM(admission.IAUTORIZA), '') AS AuthorizationNumber, 
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
				END AS ExternalCause, 
				ISNULL(admission.CODDIAEGR, '') AS Diagnostic, 
				CASE Egreso.ESTPACEGR 
					WHEN 3 THEN '1' 
					ELSE 
						CASE HIS.Indication 
							WHEN 12 THEN '1' 
							WHEN 10 THEN '2' 
							WHEN 3 THEN '3' 
							WHEN 4 THEN '3' 
							WHEN 5 THEN '3' 
							WHEN 6 THEN '3' 
							ELSE '1' 
					END 
				END AS UserDestination, 
				CASE Egreso.ESTPACEGR WHEN 3 THEN 2 ELSE 1 END AS OutputState, 
				CASE Egreso.ESTPACEGR WHEN 3 THEN admission.CODDIAEGR ELSE '' END AS CauseDeath, 
				CONVERT(VARCHAR(10),Alta.FECALTPAC,103) AS OutDate, 
				CONVERT(VARCHAR(5),Alta.FECALTPAC,114) AS OutTime
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN
			(
				SELECT ID.InvoiceId, SO.AdmissionNumber
				FROM @Invoices xi
				JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON xi.InvoiceId = ID.InvoiceId
				JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
				JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
				GROUP BY ID.InvoiceId, SO.AdmissionNumber
			) SO ON I.Id = SO.InvoiceId
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE 
			JOIN 
			(
				SELECT NUMINGRES, MAX(FECFINEST) AS FECALTPAC 
				FROM dbo.CHREGESTA AS CHR WITH (NOLOCK) 
				JOIN dbo.CHCAMASHO AS CHC WITH (NOLOCK) ON CHR.CODICAMAS = CHC.CODICAMAS 
				WHERE CHC.CODCLACAM  = 1 
				GROUP BY NUMINGRES
			) AS Alta ON admission.NUMINGRES = ALTA.NUMINGRES 
			JOIN dbo.CHREGEGRE AS Egreso WITH (NOLOCK) ON admission.NUMINGRES = EGRESO.NUMINGRES 
			JOIN @Histories HIS ON admission.NUMINGRES = HIS.AdmissionNumber
			WHERE I.[Status] = 1
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END