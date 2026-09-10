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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AU de RIPS (Registros Individuales de Prestación de Servicios) para un radicado de cartera específico o un conjunto de facturas enviadas en formato XML. Consolida información de admisiones, egresos, diagnósticos, causas externas, números de autorización e identificación del paciente (cédula, documento) cruzando facturas de cobro (Billing.Invoice), detalles de radicación de cartera (RadicateInvoiceC / RadicateInvoiceD) y tablas maestras de historia clínica, ingresos y pacientes. Incluye lógica especial para facturas de capitación (tipo 4) buscando las facturas de evento (tipo 5) asociadas al mismo tercero pagador y grupo de atención dentro del período de capitación. El resultado es un conjunto de registros estructurados con los campos exigidos por el estándar RIPS AU: IPS, tipo y número de identificación del paciente, fecha y hora de ingreso, número de autorización, causa externa, diagnóstico de egreso, destino del usuario, estado de salida, causa de muerte y fecha y hora de alta, listos para la generación del archivo plano de reporte a la entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAUFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAUFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el dataset del archivo AU de RIPS (datos de usuarios atendidos por urgencias/hospitalización) consolidando facturas radicadas e información de admisión, egreso, alta y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Billing.Invoice y estar en Status = 1 para incluirse en el resultado final.; Las facturas radicadas (Portfolio.RadicateInvoiceD) deben tener State distinto de 4 (no anuladas/excluidas) para ser consideradas.; Para facturas de DocumentType = 4 (capitación), debe existir una factura asociada con DocumentType = 5 mismo tercero, grupo de atención y categoría, con InvoiceDate dentro del rango CapitationInitialDate–CapitationEndDate.; Debe existir al menos un folio de historia clínica (HCHISPACA) con INDICAPAC en (''3'',''4'',''5'',''6'',''10'',''12'') para la admisión asociada.; Debe existir registro de egreso (CHREGEGRE) y de cama hospitalaria (CHCAMASHO con CODCLACAM = 1) para obtener fecha de alta.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con Status = 1.; Se excluyen radicaciones con State = 4.; Para cada admisión se selecciona el folio mínimo de historia clínica (MIN NUMEFOLIO) entre los indicadores clínicos válidos.; La fecha de alta corresponde al MAX(FECFINEST) sobre camas con CODCLACAM = 1 (camas de hospitalización).; Causa externa nunca queda nula: si no mapea, se asigna ''13''.; CauseDeath solo se diligencia cuando el estado de egreso indica fallecimiento (ESTPACEGR=3).; AuthorizationNumber y Diagnostic nunca son NULL (se reemplazan por cadena vacía).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AU (usuarios de urgencias/hospitalización); Factura; Radicación de factura; Capitación; Admisión/Ingreso; Historia clínica; Egreso del paciente; Alta hospitalaria; Causa externa; Diagnóstico de egreso; Autorización; Tipo de identificación; IPS; Estado del paciente al egreso (vivo/fallecido)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve los datos AU de RIPS solo para facturas con Status=1, ordenados por la parte numérica del InvoiceNumber.; [RAISERROR] N/A: En caso de error, se imprime el mensaje y la línea mediante PRINT (no se relanza la excepción).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Invoice.DocumentType <> 4 (no capitación) → Se incluye la factura directamente desde Billing.Invoice o desde la radicación. else Si DocumentType = 4 (capitación), se busca la factura asociada con DocumentType = 5 que coincida en tercero, grupo de atención, categoría y cuya InvoiceDate caiga dentro del periodo de capitación.; si ITIPORIES (tipo de causa externa) según valor → Se mapea a códigos RIPS de causa externa (''01''..''15''); cualquier valor no listado se asigna a ''13'' (otra).; si Egreso.ESTPACEGR = 3 (paciente fallecido) → UserDestination=''1'', OutputState=2 (muerto) y CauseDeath=CODDIAEGR (diagnóstico de egreso). else OutputState=1 (vivo), CauseDeath vacío y UserDestination según HIS.Indication: 12→''1'', 10→''2'', 3/4/5/6→''3'', otros→''1''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; dbo.ADCENATEN; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHREGEGRE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAUFileData';
-- GO
