
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AM de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAMFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices as XML,
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
		HealthEntityCode VARCHAR(50), 
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		Code VARCHAR(50), 
		CodeCUM VARCHAR(50), 
		CodeAlternative VARCHAR(50), 
		CodeAlternativeTwo VARCHAR(50), 
		ProductId INT, 
		MedicationType INT, 
		DrugName VARCHAR(300), 
		PharmaceuticalForm VARCHAR(300), 
		Concentration VARCHAR(50), 
		UnitMeasure VARCHAR(50), 
		InvoicedQuantity INT, 
		TotalSalesPrice DECIMAL(18,0), 
		GrandTotalSalesPrice DECIMAL(18,0),

		ProductTypeClass Tinyint NULL,
		RIPSConcept Char(2) NULL
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
			SELECT SOD.Id, I.Id,
					xi.InvoiceNumber AS InvoiceNumber, 
					ha.HealthEntityCode AS HealthEntityCode, 
					ca.CODIPSSEC AS IPSCode, 
					TIP.SIGLA AS IdentificationTypeCode, 
					RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
					RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber, 
					cups.RIPSCode as Code, 
					ISNULL(cups.Code, '') as CodeCUM, 
					ips.Code as CodeAlternative, 
					cups.RIPSCode as CodeAlternativeTwo, 
					cups.Id as ProductId, 
					CASE cups.RIPSConcept 
						WHEN '12' THEN 1 
						WHEN '13' THEN 1 
						ELSE 2 
					END as MedicationType, 
					cups.Description as DrugName, 
					'GAS MEDICINAL' as PharmaceuticalForm, 
					'35%' as Concentration, 
					'LT X MIN' as UnitMeasure, 
					ID.InvoicedQuantity, 
					CAST(ROUND((ID.GrandTotalSalesPrice) / sod.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
					(ID.GrandTotalSalesPrice) AS GrandTotalSalesPrice,
					NULL As ProductTypeClass,
					cups.RIPSConcept
			FROM Billing.Invoice i WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON ID.ServiceOrderDetailId = sod.Id 
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
			JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = sod.CUPSEntityId 
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON I.HealthAdministratorId = ha.Id 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
			WHERE sod.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND sod.SettlementType <> 3 
				AND sod.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('12','13'))
		UNION ALL
			SELECT SOD.Id, I.Id,
					xi.InvoiceNumber AS InvoiceNumber, 
					ha.HealthEntityCode AS HealthEntityCode, 
					ca.CODIPSSEC AS IPSCode, 
					TIP.SIGLA AS IdentificationTypeCode,--- ''''''''
					RTRIM(pacient.IPCODPACI) AS IdentificationNumber, 
					RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber,  
					IP.Code, 
					ISNULL(IP.CodeCUM, '') CodeCUM, 
					IP.CodeAlternative, 
					IP.CodeAlternativeTwo, 
					sod.ProductId, 
					CASE ISNULL(ATC.POSProduct, IP.POSProduct) WHEN 1 THEN 1 ELSE 2 END as MedicationType, 
					DCI.Name as DrugName, 
					ISNULL(PF.Name, '') as PharmaceuticalForm, 
					ISNULL(ATC.Concentration, '') as Concentration, 
					CASE ATC.FormulationType
						WHEN 1 THEN ISNULL(IMUW.Abbreviation, 'NA')     
						WHEN 2 THEN ISNULL(IMUV.Abbreviation, 'NA')     
						WHEN 3 THEN ISNULL(IMUW.Abbreviation, 'NA') + ' ' + ISNULL(IMUV.Abbreviation, 'NA')
						WHEN 4 THEN ISNULL(IMUA.Abbreviation,'NA')      
						ELSE 'NA'    
					END AS UnitMeasure, 
					sod.InvoicedQuantity, 
					CAST(ROUND((ID.GrandTotalSalesPrice) / sod.InvoicedQuantity, 2) AS NUMERIC(18,2)) as TotalSalesPrice, 
					(ID.GrandTotalSalesPrice) AS GrandTotalSalesPrice,
					PT.Class As ProductTypeClass,
					NULL AS RIPSConcept
			FROM Billing.Invoice i WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId 
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.ID 
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON ID.ServiceOrderDetailId = sod.Id 
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
			JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 			
			JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON I.HealthAdministratorId = ha.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.ADCENATEN AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
			-----------------------------------------------------------------------------------------------------------
			LEFT JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id 
			LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.Id 
			LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.Id 
			LEFT JOIN Inventory.InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.Id 
			WHERE sod.InvoicedQuantity > 0 
				AND	I.[Status] = 1 
				AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
				AND sod.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.Packaging = 1) OR PT.Class = 2)
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados

	if @PackageDetail = 1 Begin
		Insert Into @TableResult
		SELECT SOD.Id, tr.InvoiceId
			, tr.InvoiceNumber
			, tr.HealthEntityCode
			, ca.CODIPSSEC AS IPSCode
			, tr.IdentificationTypeCode
			, tr.IdentificationNumber
			, RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber
			, cups.RIPSCode as Code
			, ISNULL(cups.Code, '') as CodeCUM
			, ips.Code as CodeAlternative
			, cups.RIPSCode as CodeAlternativeTwo
			, cups.Id as ProductId
			, CASE cups.RIPSConcept 
				WHEN '12' THEN 1 
				WHEN '13' THEN 1 
				ELSE 2 
			END as MedicationType
			, cups.Description AS DrugName
			, tr.PharmaceuticalForm
			, tr.Concentration
			, tr.UnitMeasure
			, SOD.InvoicedQuantity
			, 0 AS TotalSalesPrice
			, 0 AS GrandTotalSalesPrice
			, NULL As ProductTypeClass
			, cups.RIPSConcept
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
		JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = sod.CUPSEntityId 
		JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
		WHERE SOD.CUPSEntityId IS NOT NULL
			AND sod.InvoicedQuantity > 0
			AND sod.SettlementType <> 3 
			AND sod.IsDelete = 0 
			AND cups.RIPSConcept in ('12','13')
		UNION ALL
		SELECT SOD.Id, tr.InvoiceId
			, tr.InvoiceNumber
			, tr.HealthEntityCode
			, ca.CODIPSSEC AS IPSCode
			, tr.IdentificationTypeCode
			, tr.IdentificationNumber
			, RTRIM(ISNULL(sod.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) AS AuthorizationNumber
			, IP.Code as Code
			, ISNULL(IP.CodeCUM, '') CodeCUM
			, IP.CodeAlternative
			, IP.CodeAlternativeTwo
			, sod.ProductId
			, CASE ISNULL(ATC.POSProduct, IP.POSProduct) WHEN 1 THEN 1 ELSE 2 END as MedicationType
			, DCI.Name as DrugName
			, ISNULL(PF.Name, '') AS PharmaceuticalForm
			, ISNULL(ATC.Concentration, '') as Concentration
			, CASE ATC.FormulationType
					WHEN 1 THEN ISNULL(IMUW.Abbreviation, 'NA')     
					WHEN 2 THEN ISNULL(IMUV.Abbreviation, 'NA')     
					WHEN 3 THEN ISNULL(IMUW.Abbreviation, 'NA') + ' ' + ISNULL(IMUV.Abbreviation, 'NA')
					WHEN 4 THEN ISNULL(IMUA.Abbreviation,'NA')      
					ELSE 'NA'    
				END AS UnitMeasure
			, SOD.InvoicedQuantity
			, 0 AS TotalSalesPrice
			, 0 AS GrandTotalSalesPrice
			, PT.Class AS ProductTypeClass
			, NULL AS RIPSConcept
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
		JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 	
		-----------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id 
		LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.Id 
		LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.Id 
		LEFT JOIN Inventory.InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.Id 
		WHERE SOD.ProductId IS NOT NULL
			AND sod.InvoicedQuantity > 0 
			AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
			AND sod.IsDelete = 0 
			AND PT.Class = 2
	End
	
	SELECT	InvoiceNumber, HealthEntityCode, IPSCode, IdentificationTypeCode, IdentificationNumber, AuthorizationNumber, 
			Code, CodeCUM, CodeAlternative, CodeAlternativeTwo, ProductId, MedicationType, DrugName, PharmaceuticalForm, 
			Concentration, UnitMeasure, SUM(InvoicedQuantity) InvoicedQuantity, TotalSalesPrice, SUM(GrandTotalSalesPrice) GrandTotalSalesPrice
	FROM @TableResult
	WHERE (RIPSConcept IS NULL OR RIPSConcept IN ('12','13')) AND (ProductTypeClass IS NULL OR ProductTypeClass = 2)
	GROUP BY InvoiceNumber, HealthEntityCode, IPSCode, IdentificationTypeCode, IdentificationNumber, AuthorizationNumber, 
			Code, CodeCUM, CodeAlternative, CodeAlternativeTwo, ProductId, MedicationType, DrugName, PharmaceuticalForm, 
			Concentration, UnitMeasure, TotalSalesPrice
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AM de RIPS (Registros Individuales de Prestación de Servicios) para medicamentos y gases medicinales, a partir de un radicado de cartera o de una lista de facturas enviadas como XML. Consolida información de facturas de cobro (encabezado y detalle), órdenes de servicio, medicamentos (con códigos CUPS, CUM y alternativos), datos del paciente (cédula, tipo de identificación) e ingresos hospitalarios, incluyendo número de autorización, IPS prestadora y entidad de salud. Cubre tanto facturas de servicios regulares como facturas de capitación asociadas, y soporta el detalle de paquetes cuando se indica. Su propósito es alimentar el proceso de generación de RIPS para el trámite de glosas y radicación de cuentas ante pagadores (EPS, aseguradoras).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAMFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateAMFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y devuelve el conjunto de datos del archivo AM (medicamentos) de RIPS para las facturas de un radicado o de un XML, incluyendo medicamentos CUPS (gases) y productos de inventario, con expansión opcional de paquetes.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el RadicateInvoiceC identificado por el parámetro de radicación, o el XML debe traer InvoiceId válidos; Las facturas referenciadas deben existir en Billing.Invoice con Status = 1; Cada factura debe tener detalle (InvoiceDetail), orden de servicio y admisión asociadas con centro de atención válido; El paciente de la factura debe existir en INPACIENT con tipo de identificación válido en ADTIPOIDENTIFICA; Para productos de inventario, el producto debe tener ProductType y opcionalmente ATC/DCI/forma farmacéutica/unidades de medida; Para capitación: la factura origen debe tener CapitationInitialDate y CapitationEndDate definidas y existir factura DocumentType=5 asociada por tercero, grupo de cuidado y categoría', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con Status = 1 (activas/vigentes); Solo se incluyen ServiceOrderDetail con InvoicedQuantity > 0 y IsDelete = 0; Se excluyen ítems con SettlementType = 3 (en modo no-paquete); Se excluyen detalles de radicación con State = 4; El resultado final solo contiene medicamentos: RIPSConcept en (''12'',''13'') o ProductTypeClass = 2; Para CUPS clasificados como gases medicinales se fija PharmaceuticalForm=''GAS MEDICINAL'', Concentration=''35%'', UnitMeasure=''LT X MIN''; Los ítems expandidos desde paquetes se reportan con TotalSalesPrice y GrandTotalSalesPrice = 0 (no se duplica el valor del paquete); TotalSalesPrice se calcula como GrandTotalSalesPrice / InvoicedQuantity redondeado; Si no hay AuthorizationNumber en el detalle, se toma el de la admisión (IAUTORIZA); si tampoco existe, queda cadena vacía; Las cantidades y totales se agregan (SUM) por la combinación de factura/paciente/producto/precio unitario; Los errores de ejecución se silencian imprimiendo el mensaje (no se relanzan)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AM; medicamentos; gases medicinales; facturación; radicación de facturas; autorización; paciente; EPS/Administradora de salud; CUPS; CUM; ATC/DCI; POS; capitación; paquetes de servicios; IPS', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Factura con DocumentType <> 4 referenciada en el XML o en el radicado → Se incluye la factura tal cual en el conjunto a procesar; si Factura del radicado con DocumentType = 4 (capitación) y existe factura relacionada con DocumentType = 5 dentro del rango CapitationInitialDate-CapitationEndDate y mismo tercero/grupo de cuidado/categoría → Se sustituye por la factura de capitación (cc) asociada; si RIPSConcept del CUPS es ''12'' o ''13'' → MedicationType = 1 (medicamento POS) else MedicationType = 2 (no POS); si ATC/IP.POSProduct = 1 → MedicationType = 1 else MedicationType = 2; si ATC.FormulationType IN (1,2,3,4) → Se arma UnitMeasure con abreviaturas de peso, volumen, peso+volumen o unidad de administración según el caso else UnitMeasure = ''NA''; si @PackageDetail = 1 y sod.IsPackage = 1 (o sod.Packaging = 1 para productos) → Se incluyen ítems de paquete; además se ejecuta un segundo bloque que expande los detalles hijos (PackageServiceOrderDetailId) con TotalSalesPrice y GrandTotalSalesPrice forzados a 0; si @PackageDetail = 0 → Para productos se exige sod.SettlementType <> 3 vía IIF; solo se incluyen ítems con RIPSConcept ''12''/''13'' o ProductType.Class = 2; si rid.State = 4 en RadicateInvoiceD → La factura radicada se excluye del procesamiento', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Contract.CUPSEntity; Contract.HealthAdministrator; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; dbo.ADCENATEN; Inventory.InventoryProduct; Inventory.ProductType; Billing.RevenueControlDetail; Inventory.ATC; Inventory.DCI; Inventory.PharmaceuticalForm; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateAMFileData';
-- GO
