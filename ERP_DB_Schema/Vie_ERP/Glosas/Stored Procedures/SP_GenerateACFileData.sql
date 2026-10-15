-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AC de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateACFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices AS XML,
	@PackageDetail BIT
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(50)
		UNIQUE NONCLUSTERED (InvoiceId),
		UNIQUE NONCLUSTERED (InvoiceNumber)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		ServiceOrderDetailId Int,
		InvoiceId Int,
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		ServiceDate VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		RIPSCode VARCHAR(50),
		ManualRateCode VARCHAR(50),
		ConsultationPurpose VARCHAR(2), 
		ExternalCause VARCHAR(2), 
		MainDiagnosticCode VARCHAR(50), 
		DiagnosticType INT, 
		TotalSalesPrice DECIMAL(18,0),
		ModeratorFee DECIMAL(18,0), 
		NetToPay DECIMAL(18,0),
		DiagnosticCodeRel1 VARCHAR(4),
		DiagnosticCodeRel2 VARCHAR(4),
		DiagnosticCodeRel3 VARCHAR(4),
		RIPSConcept Char(2)
	)

	DECLARE @DIAGNOSTICO TABLE
	(
		InvoiceId INT,
		AdmissionNumber VARCHAR(10),
		DiagnosticCodeRel1 VARCHAR(4),
		DiagnosticCodeRel2 VARCHAR(4),
		DiagnosticCodeRel3 VARCHAR(4),
		UNIQUE NONCLUSTERED (AdmissionNumber, InvoiceId) 
	)

	DECLARE @DIAGNOSTICO_PRINCIPAL TABLE
	(
		InvoiceId INT,
		AdmissionNumber VARCHAR(10),
		DiagnosticCode VARCHAR(4),
		UNIQUE NONCLUSTERED (AdmissionNumber, InvoiceId)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i with(nolock) ON t.x.value('InvoiceId[1]','int') = i.Id
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

		INSERT INTO @DIAGNOSTICO
			SELECT	InvoiceId,
					AdmissionNumber,
					DiagnosticCodeRel1 = MAX(CASE WHEN IdPartition = 1 THEN CODDIAGNO ELSE '' END),
					DiagnosticCodeRel2 = MAX(CASE WHEN IdPartition = 2 THEN CODDIAGNO ELSE '' END),
					DiagnosticCodeRel3 = MAX(CASE WHEN IdPartition = 3 THEN CODDIAGNO ELSE '' END)
			FROM
			(
				SELECT	xi.InvoiceId, 
					I.AdmissionNumber,
					ROW_NUMBER() OVER (PARTITION BY xi.InvoiceId, I.AdmissionNumber order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END) IdPartition,
					Diag.CODDIAGNO
				FROM Billing.Invoice AS I WITH (NOLOCK)
				INNER JOIN @Invoices xi ON i.Id = xi.InvoiceId
				INNER JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = I.AdmissionNumber 
				INNER JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES
				WHERE I.[Status] = 1 AND DIAG.CODDIAPRI = 0
			) T
			GROUP BY InvoiceId, AdmissionNumber

		/*******************************************************************************************/
		
			;with DIAGNOSTICO_PRINCIPAL as  (
				SELECT	Diag.NUMINGRES,
						ROW_NUMBER() OVER (PARTITION BY Diag.NUMINGRES order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END ASC, Diag.FECDIAGNO DESC) IdPartition,
						Diag.CODDIAGNO
				FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
				WHERE DIAG.CODDIAPRI = 1
			)
			INSERT INTO @TableResult 
			 SELECT 
				SOD.Id,
				i.Id,
				xi.InvoiceNumber AS InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				tip.SIGLA AS IdentificationTypeCode, 
				RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
				CONVERT(VARCHAR(10),SOD.ServiceDate,103) AS ServiceDate, 
				REPLACE(TRIM(COALESCE(SOD.AuthorizationNumber, admision.IAUTORIZA, '')), CHAR(9), '') AS AuthorizationNumber, 
				RTRIM(CUPS.RIPSCode) AS RIPSCode,
				i1.Code AS ManualRateCode,
				ISNULL(hp.Code, CASE rc.CONCEPTORIPSRIAS 
								WHEN '01' THEN '01' 
								WHEN '02' THEN '02'
								WHEN '03' THEN '03'
								WHEN '04' THEN '04'
								WHEN '05' THEN '05'
								WHEN '06' THEN '06'
								WHEN '07' THEN '07'
								WHEN '08' THEN '08'
								WHEN '12' THEN '09'
								ELSE '10' END) ConsultationPurpose, 
				CASE rc.CONCEPTORIPSRIAS 
					WHEN '01' THEN '15'
					WHEN '02' THEN '15'
					WHEN '03' THEN '15'
					WHEN '04' THEN '15'
					WHEN '05' THEN '15'
					WHEN '06' THEN '15'
					WHEN '07' THEN '15'
					WHEN '08' THEN '13'
					WHEN '09' THEN '13'
					ELSE 
						CASE admision.ITIPORIES 
							WHEN  2 THEN '02' 
							WHEN  3 THEN '06' 
							WHEN  5 THEN '01' 
							WHEN  6 THEN '14' 
							WHEN  8 THEN '05' 
							WHEN  9 THEN '07' 
							WHEN 10 THEN '08' 
							WHEN 11 THEN '09' 
							WHEN 13 THEN '15' 
							WHEN 14 THEN '03' 
							WHEN 15 THEN '04' 
							WHEN 16 THEN '10' 
							WHEN 17 THEN '11' 
							WHEN 18 THEN '12' 
							ELSE '13' 
						END
				END AS ExternalCause, 
				COALESCE(Diag.CODDIAGNO, DiagPrin.CODDIAGNO, admision.CODDIAEGR, I.OutputDiagnosis,'Z000') AS MainDiagnosticCode,
				CASE Diag.TIPDIAGNO 
					WHEN 'I' THEN 1 
					WHEN 'C' THEN 2 
					WHEN 'R' THEN 3 
					ELSE 1 
				END AS DiagnosticType, 
				CAST(ROUND((ID.ThirdPartySalesPrice + ID.SubTotalPatientSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS TotalSalesPrice, 
				CAST(ROUND(ID.SubTotalPatientSalesPrice / SOD.InvoicedQuantity,0) AS NUMERIC(18,2)) AS ModeratorFee, 
				CAST(ROUND((ID.ThirdPartySalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS NetToPay,
				DiagnosticCodeRel1 = ISNULL(DiagRel.DiagnosticCodeRel1, ''),
				DiagnosticCodeRel2 = ISNULL(DiagRel.DiagnosticCodeRel2, ''),
				DiagnosticCodeRel3 = ISNULL(DiagRel.DiagnosticCodeRel3, ''),
				CUPS.RIPSConcept
			 FROM Billing.Invoice AS I WITH (NOLOCK)
			 JOIN @Invoices xi ON i.Id = xi.InvoiceId
			 JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			 JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId 
			 JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
			 JOIN Contract.IPSService i1 WITH(NOLOCK) ON SOD.IPSServiceId = i1.Id
			 JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
			 JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			 LEFT JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON admision.IdHealthPurposes = hp.Id
			 LEFT JOIN dbo.RIASCUPS rc WITH(NOLOCK) on rc.ID = SOD.RIASCupsId 
			 LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND Diag.IPCODPACI = admision.IPCODPACI AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			 LEFT JOIN @DIAGNOSTICO DiagRel ON DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.InvoiceId = xi.InvoiceId
			 LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.IdPartition = 1 AND DiagPrin.NUMINGRES = admision.NUMINGRES 
			 CROSS APPLY [Billing].[DuplicateRows](SOD.Id, SOD.InvoicedQuantity) 
			 WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)  
				AND SOD.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept = '01')
				AND I.[Status] = 1

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	If @PackageDetail = 1 Begin

		;with DIAGNOSTICO_PRINCIPAL as  (
				SELECT	Diag.NUMINGRES,
						ROW_NUMBER() OVER (PARTITION BY Diag.NUMINGRES order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END ASC, Diag.FECDIAGNO DESC) IdPartition,
						Diag.CODDIAGNO
				FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
				WHERE DIAG.CODDIAPRI = 1
		)
		INSERT INTO @TableResult 
		SELECT SOD.Id, tr.InvoiceId, tr.InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode,
				tr.IdentificationTypeCode, 
				tr.IdentificationNumber, 
				CONVERT(VARCHAR(10),SOD.ServiceDate,103) AS ServiceDate, 
				REPLACE(TRIM(COALESCE(SOD.AuthorizationNumber, admision.IAUTORIZA, '')), CHAR(9), '') AS AuthorizationNumber,
				RTRIM(CUPS.RIPSCode) AS RIPSCode,
				i1.Code AS ManualRateCode,
				ISNULL(hp.Code, CASE rc.CONCEPTORIPSRIAS 
								WHEN '01' THEN '01' 
								WHEN '02' THEN '02'
								WHEN '03' THEN '03'
								WHEN '04' THEN '04'
								WHEN '05' THEN '05'
								WHEN '06' THEN '06'
								WHEN '07' THEN '07'
								WHEN '08' THEN '08'
								WHEN '12' THEN '09'
								ELSE '10' END) ConsultationPurpose, 
				CASE rc.CONCEPTORIPSRIAS 
					WHEN '01' THEN '15'
					WHEN '02' THEN '15'
					WHEN '03' THEN '15'
					WHEN '04' THEN '15'
					WHEN '05' THEN '15'
					WHEN '06' THEN '15'
					WHEN '07' THEN '15'
					WHEN '08' THEN '13'
					WHEN '09' THEN '13'
					ELSE 
						CASE admision.ITIPORIES 
							WHEN  2 THEN '02' 
							WHEN  3 THEN '06' 
							WHEN  5 THEN '01' 
							WHEN  6 THEN '14' 
							WHEN  8 THEN '05' 
							WHEN  9 THEN '07' 
							WHEN 10 THEN '08' 
							WHEN 11 THEN '09' 
							WHEN 13 THEN '15' 
							WHEN 14 THEN '03' 
							WHEN 15 THEN '04' 
							WHEN 16 THEN '10' 
							WHEN 17 THEN '11' 
							WHEN 18 THEN '12' 
							ELSE '13' 
						END
				END AS ExternalCause,
				COALESCE(Diag.CODDIAGNO, DiagPrin.CODDIAGNO, admision.CODDIAEGR, tr.MainDiagnosticCode,'Z000') AS MainDiagnosticCode,
				CASE Diag.TIPDIAGNO 
					WHEN 'I' THEN 1 
					WHEN 'C' THEN 2 
					WHEN 'R' THEN 3 
					ELSE 1 
				END AS DiagnosticType, 
				0, 
				0, 
				0,
				ISNULL(DiagRel.DiagnosticCodeRel1, '') as DiagnosticCodeRel1,
				ISNULL(DiagRel.DiagnosticCodeRel2, '') as DiagnosticCodeRel2,
				ISNULL(DiagRel.DiagnosticCodeRel3, '') as DiagnosticCodeRel3,
				CUPS.RIPSConcept
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail sod with(nolock) on sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
		JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
		JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
		JOIN Contract.IPSService i1 WITH(NOLOCK) ON SOD.IPSServiceId = i1.Id
		LEFT JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON hp.Id = admision.IdHealthPurposes
		LEFT JOIN dbo.RIASCUPS rc WITH(NOLOCK) on rc.ID = SOD.RIASCupsId
		LEFT JOIN @DIAGNOSTICO DiagRel ON DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.InvoiceId = tr.InvoiceId
		LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND Diag.IPCODPACI = admision.IPCODPACI AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
		LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.IdPartition = 1 AND DiagPrin.NUMINGRES = admision.NUMINGRES 
		WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)  
			AND SOD.IsDelete = 0 
			AND cups.RIPSConcept = '01'
	End

	SELECT * 
	FROM @TableResult
	WHERE RIPSConcept IS NULL OR RIPSConcept = '01'
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END