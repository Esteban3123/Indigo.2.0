

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AN de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateANFileData] 
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

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		BirthDate VARCHAR(50), 
		BirthTime VARCHAR(50), 
		GestationalAge INT, 
		AntenatalControl INT, 
		Gender VARCHAR(50), 
		[Weight] DECIMAL(18,3), 
		DiagnosticCode VARCHAR(50), 
		CauseDeath VARCHAR(50), 
		DeathDate VARCHAR(50), 
		DeathTime VARCHAR(50)
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
				xi.InvoiceNumber AS InvoiceNumber, 
				ca.CODIPSSEC AS IPSCode,
				TIP.SIGLA AS IdentificationTypeCode, 
				LTRIM(RTRIM(pacient.IPCODPACI)) AS IdentificationNumber, 
				CONVERT(VARCHAR(10),RECINAC.FECHANACIM,103) AS BirthDate, 
				CONVERT(VARCHAR(5),RECINAC.FECHANACIM,114) AS BirthTime, 
				CAST(ISNULL(GINE.NOMSEMGES, 0) AS INT) AS GestationalAge, 
				1 AS AntenatalControl, 
				CASE RECINAC.SEXRECNAC WHEN 1 THEN 'M' WHEN 2 THEN 'F' END AS Gender, 
				RECINAC.PESORECNA AS [Weight], 
				RECNADI.CODDIAGNO AS DiagnosticCode, 
				CASE VITANACIM WHEN 'MUERTO' THEN RECNADI.CODDIAGNO ELSE '' END AS CauseDeath, 
				CASE VITANACIM WHEN 'MUERTO' THEN CONVERT(VARCHAR(10), RECINAC.FECHANACIM,103) ELSE '' END AS DeathDate, 
				CASE VITANACIM WHEN 'MUERTO' THEN CONVERT(VARCHAR(5),RECINAC.FECHANACIM,114) ELSE '' END AS DeathTime
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId
			JOIN
			(
				SELECT ID.InvoiceId, SO.AdmissionNumber
				FROM Billing.InvoiceDetail ID WITH (NOLOCK)
				JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
				JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
				GROUP BY ID.InvoiceId, SO.AdmissionNumber
			) SO ON I.Id = SO.InvoiceId
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
			JOIN [Contract].HealthAdministrator HA WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN 
			(
				SELECT NOMSEMGES, NUMINGRES, MAX(NUMEFOLIO) AS NUMEFOLIO 
				FROM dbo.HCANTGINE WITH (NOLOCK) 
				GROUP BY NOMSEMGES,NUMINGRES 
			) AS GINE ON GINE.NUMINGRES = I.AdmissionNumber 
			JOIN dbo.HCRECINAC RECINAC WITH (NOLOCK) ON RECINAC.NUMINGRES = I.AdmissionNumber 
			JOIN 
			(
				SELECT CODDIAGNO, CONSECREC 
				FROM dbo.HCRECNADI WITH (NOLOCK) 
			) RECNADI ON RECINAC.NUMCONSEC = RECNADI.CONSECREC 
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