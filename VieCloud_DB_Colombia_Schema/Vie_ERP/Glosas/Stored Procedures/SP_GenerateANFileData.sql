

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AN de RIPS (Recién Nacidos) para un radicado de cartera específico o a partir de un listado de facturas enviado como XML. Consolida información clínica de neonatos —fecha y hora de nacimiento, sexo, peso, edad gestacional, diagnóstico y causa/fecha de muerte si aplica— junto con datos del paciente, tipo de identificación y código de la IPS. Cruza facturas activas (Billing.Invoice) con su radicado en cartera (RadicateInvoiceC / RadicateInvoiceD), el ingreso hospitalario (ADINGRESO), la historia clínica de recién nacido (HCRECINAC, HCRECNADI) y datos gineco-obstétricos (HCANTGINE), excluyendo documentos de tipo nota crédito (DocumentType = 4). El resultado, ordenado por número de factura, es el insumo para construir el archivo AN que se reporta a las entidades pagadoras en el proceso de glosas y facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateANFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateANFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del archivo AN de RIPS (recién nacidos) consolidando información de facturas radicadas, ingresos, datos del paciente y registro clínico del recién nacido.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un radicado de facturas válido (Portfolio.RadicateInvoiceC) o una lista de facturas en el XML de entrada.; Las facturas deben tener Status=1 (activas) para ser incluidas.; Deben existir registros clínicos de recién nacido (HCRECINAC) y su diagnóstico asociado (HCRECNADI) ligados al ingreso de la factura.; El paciente debe estar en INPACIENT con tipo de identificación válido en ADTIPOIDENTIFICA.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen facturas con rid.State=4 (estado anulado/excluido en radicación).; Solo se procesan facturas activas (Status=1).; AntenatalControl siempre se reporta con valor fijo 1.; Si no hay edad gestacional registrada (NOMSEMGES) se asume 0.; Para nacidos vivos no se reporta fecha/hora ni causa de muerte.; Las fechas de nacimiento se formatean como dd/mm/yyyy (estilo 103) y la hora como hh:mi (estilo 114).; Los errores en el TRY se imprimen pero no detienen la ejecución; se devuelve el resultado parcial.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AN (recién nacidos); Radicación de facturas; Capitación (DocumentType 4 y 5); Ingreso/admisión hospitalaria; Centro de atención (IPS); Paciente y tipo de identificación; Edad gestacional; Control prenatal; Diagnóstico de recién nacido; Mortalidad neonatal (VITANACIM); Administradora de salud (EPS)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve los datos del archivo AN ordenados numéricamente por InvoiceNumber, solo para facturas con Status=1 que tengan registro de recién nacido (HCRECINAC) e ingreso asociado.; [INSERT] @Invoices: Incluye facturas del XML cuyo DocumentType<>4; facturas radicadas (rid.State<>4) con DocumentType<>4; y para facturas radicadas con DocumentType=4, agrega facturas relacionadas con DocumentType=5 que coincidan en ThirdPartyId, CareGroupId, InvoiceCategoryId y cuya InvoiceDate esté entre CapitationInitialDate y CapitationEndDate.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Factura proviene del XML y DocumentType<>4 → Se incluye directamente la factura del XML.; si Factura radicada con rid.State<>4 y DocumentType<>4 → Se incluye la factura del radicado.; si Factura radicada con DocumentType=4 (capitación) → Se buscan facturas relacionadas con DocumentType=5 dentro del rango de capitación (CapitationInitialDate a CapitationEndDate) y mismo tercero/grupo de atención/categoría.; si RECINAC.SEXRECNAC = 1 / = 2 → Gender se mapea a ''M'' / ''F'' respectivamente; otro valor queda NULL.; si VITANACIM = ''MUERTO'' → CauseDeath toma el código diagnóstico y DeathDate/DeathTime se llenan con FECHANACIM. else CauseDeath, DeathDate y DeathTime quedan vacíos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; dbo.ADINGRESO; dbo.ADCENATEN; Contract.HealthAdministrator; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.HCANTGINE; dbo.HCRECINAC; dbo.HCRECNADI', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateANFileData';
-- GO
