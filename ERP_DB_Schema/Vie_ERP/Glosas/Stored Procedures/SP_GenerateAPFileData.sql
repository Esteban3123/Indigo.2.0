-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AP de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAPFileData] 
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
		CUPSCode VARCHAR(50), 
		ManualRateCode VARCHAR(50), 
		ScopeRealization INT, 
		ProcedutePurpose INT, 
		PersonalService VARCHAR(50), 
		MainDiagnosticCode VARCHAR(50), 
		Complication VARCHAR(50), 
		FormSurgicalAct INT, 
		TotalSalesPrice DECIMAL(18,0),
		RIPSConcept Char(2)
	)

	DECLARE @DIAGNOSTICO_PRINCIPAL TABLE
	(
		InvoiceId INT,
		AdmissionNumber VARCHAR(10),
		DiagnosticCode VARCHAR(4)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i WITH (NOLOCK) ON t.x.value('InvoiceId[1]','int') = i.Id
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

		INSERT INTO @DIAGNOSTICO_PRINCIPAL
			SELECT	InvoiceId,
					AdmissionNumber,
					DiagnosticCode = CODDIAGNO
			FROM
			(
				SELECT xi.InvoiceId, I.AdmissionNumber, ROW_NUMBER() OVER (PARTITION BY xi.InvoiceId, I.AdmissionNumber order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END ASC, Diag.FECDIAGNO DESC) IdPartition, Diag.CODDIAGNO
				FROM Billing.Invoice AS I WITH (NOLOCK)
				JOIN @Invoices xi ON i.Id = xi.InvoiceId
				JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = I.AdmissionNumber 
				JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES
				WHERE I.[Status] = 1 AND DIAG.CODDIAPRI = 1
			) T
			WHERE IdPartition = 1

		INSERT INTO @DIAGNOSTICO_PRINCIPAL
			SELECT xi.InvoiceId, i.AdmissionNumber, i.OutputDiagnosis
			FROM @Invoices xi
			JOIN Billing.Invoice i WITH (NOLOCK) ON xi.InvoiceId = i.Id
			LEFT JOIN @DIAGNOSTICO_PRINCIPAL dp ON xi.InvoiceId = dp.InvoiceId
			WHERE dp.InvoiceId IS NULL AND i.OutputDiagnosis IS NOT NULL

		/*******************************************************************************************/

		INSERT INTO @TableResult 
			 SELECT SOD.Id, I.Id,	xi.InvoiceNumber AS InvoiceNumber, 
					LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
					TIP.SIGLA AS IdentificationTypeCode, 
					RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
					CONVERT(VARCHAR(10),SOD.ServiceDate,103) AS ServiceDate, 
					RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admision.IAUTORIZA,''))) AS AuthorizationNumber, 
					CUPS.RIPSCode AS RIPSCode, 
					CUPS.Code AS CUPSCode, 
					IPS.Code AS ManualRateCode,
					CASE admision.TIPOINGRE
						WHEN 2 THEN 2
						ELSE 
							CASE admision.IINGREPOR
								WHEN 1 THEN 3
								ELSE 1
							END
					END AS ScopeRealization, 
					ISNULL
					(
						CASE 
							WHEN rc.CONCEPTORIPSRIAS in ('01', '02', '03', '10') THEN 2 
							WHEN rc.CONCEPTORIPSRIAS in ('04', '05', '06', '07', '08', '09') THEN 1
							WHEN rc.CONCEPTORIPSRIAS = '11' THEN 3
							WHEN rc.CONCEPTORIPSRIAS = '12' THEN 4
							WHEN rc.CONCEPTORIPSRIAS = '13' THEN 5
						END, 
						CASE IPS.ServiceType 
							WHEN 3 THEN 2 
							WHEN 4 THEN 3 
							WHEN 5 THEN 4 
							WHEN 6 THEN 5 
							ELSE 1 
						END
					) AS ProcedutePurpose, 
					CASE ISNULL(parto.TIPPROFES, 0)
						WHEN 0 THEN ''
						WHEN 1 THEN '2'
						WHEN 2 THEN '1'
						WHEN 3 THEN '3'
						WHEN 4 THEN '4'
						ELSE '5'
					END AS PersonalService,
					COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,admision.CODDIAEGR,I.OutputDiagnosis,'Z000') AS MainDiagnosticCode, 
					'' AS Complication, 
					CASE SOD.SurgicalInterventionType 
						WHEN 1 THEN 1 
						WHEN 6 THEN 2 
						WHEN 4 THEN 3 
						WHEN 7 THEN 4 
						WHEN 5 THEN 5 
						ELSE 1 
					END AS FormSurgicalAct, 
					CAST(ROUND((ID.GrandTotalSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS TotalSalesPrice,
					CUPS.RIPSConcept
			 FROM Billing.Invoice I WITH (NOLOCK) 
			 JOIN @Invoices xi ON i.Id = xi.InvoiceId
			 JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			 JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId 
			 JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
			 JOIN [Contract].IPSService IPS WITH (NOLOCK) ON IPS.Id = SOD.IPSServiceId 
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON CA.CODCENATE = admision.CODCENATE 
			 JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			 JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			 LEFT JOIN dbo.RIASCUPS rc WITH(NOLOCK) ON rc.ID = SOD.RIASCupsId
			 LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			 LEFT JOIN @DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.AdmissionNumber = admision.NUMINGRES AND DiagPrin.InvoiceId = xi.InvoiceId
			 LEFT JOIN 
			 (
				 SELECT parto.NUMINGRES, MAX(profesional.TIPPROFES) TIPPROFES
				 FROM dbo.HCATINPAR parto
				 JOIN dbo.INPROFSAL profesional ON parto.CODPROSAL = profesional.CODPROSAL 
				 GROUP BY parto.NUMINGRES
			 ) parto ON admision.NUMINGRES = parto.NUMINGRES 
			 CROSS APPLY [Billing].[DuplicateRows](SOD.Id, SOD.InvoicedQuantity) 
			 WHERE I.Status = 1 
				AND SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)
				AND SOD.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14'))
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	If @PackageDetail = 1 Begin
		Insert Into @TableResult
		Select SOD.Id, tr.InvoiceId, tr.InvoiceNumber AS InvoiceNumber, 
			LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
			tr.IdentificationTypeCode, 
			tr.IdentificationNumber, 
			CONVERT(VARCHAR(10),SOD.ServiceDate,103) AS ServiceDate, 
			RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admision.IAUTORIZA,''))) AS AuthorizationNumber, 
			CUPS.RIPSCode AS RIPSCode, 
			CUPS.Code AS CUPSCode, 
			IPS.Code AS ManualRateCode,
			CASE admision.TIPOINGRE
				WHEN 2 THEN 2
				ELSE 
					CASE admision.IINGREPOR
						WHEN 1 THEN 3
						ELSE 1
					END
			END AS ScopeRealization, 
			ISNULL
			(
				CASE 
					WHEN rc.CONCEPTORIPSRIAS in ('01', '02', '03', '10') THEN 2 
					WHEN rc.CONCEPTORIPSRIAS in ('04', '05', '06', '07', '08', '09') THEN 1
					WHEN rc.CONCEPTORIPSRIAS = '11' THEN 3
					WHEN rc.CONCEPTORIPSRIAS = '12' THEN 4
					WHEN rc.CONCEPTORIPSRIAS = '13' THEN 5
				END, 
				CASE IPS.ServiceType 
					WHEN 3 THEN 2 
					WHEN 4 THEN 3 
					WHEN 5 THEN 4 
					WHEN 6 THEN 5 
					ELSE 1 
				END
			) AS ProcedutePurpose, 
			CASE ISNULL(parto.TIPPROFES, 0)
				WHEN 0 THEN ''
				WHEN 1 THEN '2'
				WHEN 2 THEN '1'
				WHEN 3 THEN '3'
				WHEN 4 THEN '4'
				ELSE '5'
			END AS PersonalService,
			COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,admision.CODDIAEGR,tr.MainDiagnosticCode,'Z000') AS MainDiagnosticCode, 
			'' AS Complication, 
			CASE SOD.SurgicalInterventionType 
				WHEN 1 THEN 1 
				WHEN 6 THEN 2 
				WHEN 4 THEN 3 
				WHEN 7 THEN 4 
				WHEN 5 THEN 5 
				ELSE 1 
			END AS FormSurgicalAct, 
			0 AS TotalSalesPrice,
			CUPS.RIPSConcept
		From @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
		JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
		JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON CA.CODCENATE = admision.CODCENATE 
		JOIN [Contract].IPSService IPS WITH (NOLOCK) ON IPS.Id = SOD.IPSServiceId 
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
		LEFT JOIN dbo.RIASCUPS rc WITH(NOLOCK) ON rc.ID = SOD.RIASCupsId
		LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
		LEFT JOIN @DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.AdmissionNumber = admision.NUMINGRES AND DiagPrin.InvoiceId = tr.InvoiceId
		LEFT JOIN 
		(
			SELECT parto.NUMINGRES, MAX(profesional.TIPPROFES) TIPPROFES
			FROM dbo.HCATINPAR parto
			JOIN dbo.INPROFSAL profesional ON parto.CODPROSAL = profesional.CODPROSAL 
			GROUP BY parto.NUMINGRES
		) parto ON admision.NUMINGRES = parto.NUMINGRES 
		WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)
				AND SOD.IsDelete = 0 
				AND cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14')
	End

	SELECT	*
	FROM @TableResult
	WHERE RIPSConcept IS NULL OR RIPSConcept not IN ('01','06','07','08','09','11', '12', '13','14')
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END