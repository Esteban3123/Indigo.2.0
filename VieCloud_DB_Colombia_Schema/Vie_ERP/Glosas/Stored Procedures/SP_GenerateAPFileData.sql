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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AP de RIPS (Registros Individuales de Prestación de Servicios) correspondientes a procedimientos y actividades de salud. A partir de un radicado de cartera (@RadicateInvoiceId) o una lista de facturas en formato XML (@XmlInvoices), consolida información de facturas (Billing.Invoice), radicados de cobro (Portfolio.RadicateInvoiceC/D), pacientes, ingresos, diagnósticos principales (CIE-10), códigos CUPS/RIPS, autorizaciones y valores cobrados para construir el reporte AP exigido por la normativa de RIPS. Este procedimiento es clave en el proceso de glosas y facturación a entidades pagadoras (EPS, aseguradoras), ya que estructura los datos de prestaciones de servicios de salud en el formato requerido para presentación y auditoría de cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAPFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAPFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los registros del archivo AP de RIPS (procedimientos) para facturas asociadas a una radicación o lista de facturas, calculando códigos, diagnóstico principal, propósito del procedimiento y desglosando paquetes cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Billing.Invoice y tener Status = 1 para ser incluidas en el resultado principal; DocumentType <> 4 para facturas tomadas del XML o de la radicación; las de DocumentType = 4 (capitación) se reemplazan por su factura DocumentType = 5 emparejada por tercero, grupo de atención, categoría y rango de capitación; El detalle de la radicación (RadicateInvoiceD) debe tener State <> 4 para considerarse; Las líneas SOD deben tener IsDelete = 0; SOD.SettlementType debe ser distinto de 3 cuando @PackageDetail=0 y distinto de 0 cuando @PackageDetail=1', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen facturas con DocumentType = 4 directamente; siempre se sustituyen por su factura capitada DocumentType = 5; Solo se procesan facturas con Status = 1 e ítems con IsDelete = 0; El resultado final excluye RIPSConcept en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14'') (conceptos no AP); Cada SOD se replica tantas veces como InvoicedQuantity mediante Billing.DuplicateRows; Si no hay diagnóstico identificado por ninguna fuente, se asigna ''Z000'' como diagnóstico principal por defecto; La fecha del servicio se formatea siempre como dd/MM/yyyy (formato 103); TotalSalesPrice se calcula como GrandTotalSalesPrice/InvoicedQuantity redondeado, salvo en filas hijas de paquetes donde es 0; El número de autorización proviene de SOD.AuthorizationNumber y, si es nulo, de admision.IAUTORIZA, o cadena vacía', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Inserta una fila por cada combinación factura/SOD que cumpla I.Status=1, IsDelete=0, filtro de SettlementType según @PackageDetail y (cuando @PackageDetail=0) cuyo CUPS.RIPSConcept no esté en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14''); se replican filas según InvoicedQuantity vía Billing.DuplicateRows; [INSERT] @TableResult: Cuando @PackageDetail = 1, además inserta los SOD hijos cuyo PackageServiceOrderDetailId apunta a un SOD ya cargado, con TotalSalesPrice=0 y excluyendo RIPSConcept en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14''); [RETURN_RESULT] RESULT_SET: Devuelve las filas de @TableResult cuyo RIPSConcept es NULL o no está en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14''), ordenadas por la parte numérica de InvoiceNumber; [INSERT] @DIAGNOSTICO_PRINCIPAL: Por cada factura/admisión activa, registra el diagnóstico principal (CODDIAPRI=1) priorizando DIAINGEGR=''E'' sobre ''A'' y luego cualquier otro, y por fecha FECDIAGNO descendente; [INSERT] @DIAGNOSTICO_PRINCIPAL: Si la factura no obtuvo diagnóstico principal y tiene OutputDiagnosis no nulo, se registra OutputDiagnosis como diagnóstico principal de respaldo; [RAISERROR] ERROR_OUTPUT: En caso de excepción durante la carga, se imprime ERROR_MESSAGE() y la línea con PRINT (no se relanza el error)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de las facturas: XML de entrada vs radicación (@RadicateInvoiceId) → Une facturas del XML (DocumentType<>4) con facturas radicadas activas (State<>4, DocumentType<>4) y, para radicadas con DocumentType=4, las sustituye por la factura capitada DocumentType=5 dentro del rango CapitationInitialDate–CapitationEndDate; si @PackageDetail = 1 → Procesa el detalle de los paquetes: incluye SOD con IsPackage=1 y agrega los SOD hijos vinculados por PackageServiceOrderDetailId con precio 0 else Solo incluye ítems no pertenecientes a paquete y excluye CUPS cuyo RIPSConcept esté en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14''); si admision.TIPOINGRE = 2 → ScopeRealization = 2 (hospitalario) else Si admision.IINGREPOR = 1 entonces ScopeRealization = 3, en otro caso 1; si rc.CONCEPTORIPSRIAS → Mapea ProcedutePurpose: (''01'',''02'',''03'',''10'')→2; (''04''..''09'')→1; ''11''→3; ''12''→4; ''13''→5 else Si no hay RIAS, usa IPS.ServiceType: 3→2, 4→3, 5→4, 6→5, otro→1; si parto.TIPPROFES (tipo profesional que atendió el parto) → Mapea PersonalService: 0→'''', 1→''2'', 2→''1'', 3→''3'', 4→''4'', otro→''5''; si SOD.SurgicalInterventionType → Mapea FormSurgicalAct: 1→1, 6→2, 4→3, 7→4, 5→5, otro→1; si Resolución de MainDiagnosticCode → Toma el primero no nulo entre Diag.CODDIAGNO (diagnóstico principal del ingreso), DiagPrin.DiagnosticCode (calculado), admision.CODDIAEGR, I.OutputDiagnosis y, por defecto, ''Z000''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.DuplicateRows', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAPFileData';
-- GO
