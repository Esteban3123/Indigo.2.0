-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AF de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAFFileData] 
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
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		InvoiceNumber VARCHAR(50), 
		AdmissionDate VARCHAR(50), 
		InvoiceDate VARCHAR(50), 
		OutputDate VARCHAR(50), 
		HealthEntityCode VARCHAR(50), 
		ThirdPartyName VARCHAR(300), 
		ContractNumber VARCHAR(100), 
		CareGroupName VARCHAR(100), 
		PolicyNumber VARCHAR(50), 
		TotalPatientSalesPrice DECIMAL(18,0),
		CommissionValue DECIMAL(18,0), 
		PatientDiscount DECIMAL(18,0), 
		ThirdPartySalesValue DECIMAL(18,0)
	)

	BEGIN TRY

		INSERT INTO @Invoices (InvoiceId, InvoiceNumber)
			SELECT DISTINCT
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i ON t.x.value('InvoiceId[1]','int') = i.Id
			--WHERE i.DocumentType <> 5
		UNION ALL
			SELECT DISTINCT 
				i.Id AS InvoiceId,
				i.InvoiceNumber AS InvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				--AND i.DocumentType <> 5

		/*******************************************************************************************/

		INSERT INTO @TableResult 
			SELECT DISTINCT 
				RTRIM(center.CODIPSSEC) AS IPSCode, 
				'NI' AS IdentificationType, 
				xi.InvoiceNumber AS InvoiceNumber, 
				CONVERT(VARCHAR(10),admission.IFECHAING,103) AS AdmissionDate, 
				CONVERT(VARCHAR(10),i.InvoiceDate,103) AS InvoiceDate, 
				CONVERT(VARCHAR(10),IIF(i.OutputDate > [Common].[GETDATE](), [Common].[GETDATE](), i.OutputDate),103) AS OutputDate, 
				ha.HealthEntityCode AS HealthEntityCode, 
				tp.Name AS ThirdPartyName, 
				ISNULL(c.ContractNumber,'SINCONTRATO') AS ContractNumber, 
				cg.Name AS CareGroupName, 
				''  AS PolicyNumber, 
				i.TotalPatientSalesPrice AS TotalPatientSalesPrice, 
				0 AS CommissionValue, 
				i.ThirdPartyDiscountValue AS PatientDiscount, 
				ROUND(i.ThirdPartySalesValue,0) AS ThirdPartySalesValue
			FROM Billing.RevenueControlDetail rcd WITH (NOLOCK) 
			JOIN Billing.Invoice i WITH (NOLOCK) ON i.RevenueControlDetailId = rcd.Id 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON i.HealthAdministratorId = ha.Id 
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.ID 
			JOIN [Contract].CareGroup cg WITH (NOLOCK) ON I.CareGroupId = cg.ID 
			JOIN .ADINGRESO admission WITH (NOLOCK) ON i.AdmissionNumber = admission.NUMINGRES 
			JOIN .ADCENATEN center WITH (NOLOCK) ON admission.CODCENATE = center.CODCENATE 
			LEFT JOIN [Contract].[Contract] c WITH (NOLOCK) ON cg.ContractId = c.ID 
			WHERE i.Status = 1
				AND i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT 
				RTRIM(ou.IPSCode) AS IPSCode, 
				'NI' AS IdentificationType, 
				i.InvoiceNumber AS InvoiceNumber, 
				CONVERT(VARCHAR(10),i.CapitationInitialDate,103) AS AdmissionDate, 
				CONVERT(VARCHAR(10),i.InvoiceDate,103) AS InvoiceDate, 
				CONVERT(VARCHAR(10),i.CapitationEndDate,103) AS OutputDate, 
				ha.HealthEntityCode AS HealthEntityCode, 
				tp.Name AS ThirdPartyName, 
				ISNULL(c.ContractNumber,'SINCONTRATO') AS ContractNumber, 
				cg.Name AS CareGroupName, 
				''  AS PolicyNumber, 
				0 AS TotalPatientSalesPrice, 
				0 AS CommissionValue, 
				0 AS PatientDiscount, 
				ROUND(i.ThirdPartySalesValue,0) as ThirdPartySalesValue
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Common.OperatingUnit ou WITH (NOLOCK) ON i.OperatingUnitId = ou.Id
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON i.HealthAdministratorId = ha.Id 
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.ID 
			JOIN [Contract].CareGroup cg WITH (NOLOCK) ON I.CareGroupId = cg.ID 
			LEFT JOIN [Contract].[Contract] c WITH (NOLOCK) ON cg.ContractId = c.ID 
			WHERE i.Status = 1
				AND i.DocumentType = 4
				
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera los datos del archivo AF de RIPS para un radicado de facturas de cartera. A partir de un ID de radicado (Portfolio.RadicateInvoiceC / RadicateInvoiceD) y/o una lista de facturas enviada en XML, consolida la información requerida por el archivo AF: código IPS, número de factura, fechas de admisión y egreso, código de la entidad pagadora (EPS/aseguradora), nombre del tercero, número de contrato, grupo de atención, valores cobrados al paciente y al tercero. Maneja dos tipos de factura: facturas de evento (asociadas a un ingreso hospitalario con datos de admisión y centro de atención) y facturas de capitación (con fechas de inicio y fin del período). El resultado se ordena numéricamente por número de factura y se usa en el proceso de generación de RIPS para radicación y gestión de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAFFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAFFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del archivo AF de RIPS consolidando facturas (por evento o capitación) provenientes de un radicado de cartera o de un listado XML.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data con nodo InvoiceId convertible a INT.; Las facturas deben existir en Billing.Invoice referenciadas por Id (XML) o por InvoiceNumber (radicado).; Para facturas por evento (DocumentType <> 4) debe existir admisión en ADINGRESO y centro en ADCENATEN asociados a la factura.; Para facturas de capitación (DocumentType = 4) debe existir la unidad operativa asociada a la factura.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con Status = 1.; Los detalles del radicado con State = 4 se excluyen.; OutputDate nunca puede ser superior a la fecha actual del sistema.; IdentificationTypeCode siempre se emite como ''NI''.; PolicyNumber siempre se devuelve vacío.; CommissionValue siempre se devuelve en 0.; Para facturas de capitación, los valores de venta al paciente, comisión y descuento se fuerzan a 0.; ThirdPartySalesValue se entrega redondeado a entero.; Los errores se capturan e imprimen sin propagarse (no se relanza la excepción).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AF; Factura de venta; Radicación de cartera; Admisión hospitalaria; Centro de atención / IPS; Administradora de salud (EPS); Tercero pagador; Contrato; Grupo de atención (CareGroup); Capitación; Descuento al paciente; Valor de venta a tercero', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve filas con datos AF para facturas con Status=1 y DocumentType<>4 (evento), tomando fechas de admisión/egreso desde ADINGRESO y truncando OutputDate a la fecha actual cuando es futura.; [RETURN_RESULT] @TableResult: Devuelve filas con datos AF para facturas con Status=1 y DocumentType=4 (capitación), usando CapitationInitialDate/CapitationEndDate como fechas de ingreso/egreso y poniendo en cero TotalPatientSalesPrice, CommissionValue y PatientDiscount.; [RETURN_RESULT] @TableResult: El resultado final se ordena por la parte numérica del InvoiceNumber extraída con PATINDEX.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de las facturas: nodos del XML vs. radicado de cartera (Portfolio.RadicateInvoiceC/D con ri.Id = parámetro y rid.State <> 4) → Se unen ambas fuentes en @Invoices para procesarse juntas; si i.DocumentType <> 4 (factura por evento) → Se obtienen IPSCode, AdmissionDate y OutputDate desde ADINGRESO/ADCENATEN y valores reales de venta/descuentos else Si DocumentType = 4 (capitación) se usan IPSCode de OperatingUnit y fechas de capitación, con valores de paciente/comisión/descuento en cero; si i.OutputDate > GETDATE() → Se reemplaza OutputDate por la fecha actual (Common.GETDATE()) else Se conserva OutputDate original; si c.ContractNumber IS NULL → Se reporta ''SINCONTRATO'' como número de contrato else Se usa el ContractNumber del contrato vinculado al CareGroup', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.RevenueControlDetail; Contract.HealthAdministrator; Common.ThirdParty; Contract.CareGroup; Contract.Contract; Common.OperatingUnit; ADINGRESO; ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAFFileData';
-- GO
