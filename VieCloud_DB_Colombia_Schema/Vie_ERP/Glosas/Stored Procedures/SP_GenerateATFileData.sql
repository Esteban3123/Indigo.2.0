-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AT de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateATFileData] 
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
		IPSCode VARCHAR(50), 
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		AuthorizationNumber VARCHAR(50), 
		TypeService INT, 
		RIPSCode VARCHAR(50), 
		CUPSCode VARCHAR(50), 
		CUPSDescription VARCHAR(300), 
		ManualRateCode VARCHAR(50), 
		ManualRateName VARCHAR(300), 
		InvoicedQuantity INT, 
		TotalSalesPrice DECIMAL(18,0) ,
		GrandTotalSalesPrice DECIMAL(18,0),
		ContractDescriptionCode varchar(20),
		ContractDescriptionName varchar(250),
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
			SELECT 
				SOD.Id, I.Id, xi.InvoiceNumber as InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode, 
				LTRIM(RTRIM(pacient.IPCODPACI)) as IdentificationNumber, 
				RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
				CASE CUPS.RIPSConcept 
					WHEN '06' THEN 3 
					WHEN '07' THEN 4 
					WHEN '08' THEN 3 
					WHEN '09' THEN 1 
					WHEN '11' THEN 1 
					WHEN '14' THEN 2 
					ELSE 0 
				END AS TypeService, 
				CUPS.RIPSCode as RIPSCode, 
				RTRIM(CUPS.Code) as CUPSCode, 
				ISNULL(RTRIM(CUPS.RIPSDescription),RTRIM(CUPS.Description)) as CUPSDescription,
				RTRIM(IPS.Code) as ManualRateCode, 
				RTRIM(IPS.Name) as ManualRateName, 
				ID.InvoicedQuantity as InvoicedQuantity, 
				CAST(ROUND((ID.GrandTotalSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
				Id.GrandTotalSalesPrice AS GrandTotalSalesPrice,
				ISNULL(cd.Code, '') ContractDescriptionCode,
				ISNULL(cd.Name, '') ContractDescriptionName,
				NULL AS ProductRateClass,
				CUPS.RIPSConcept
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
			JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			JOIN [Contract].IPSService IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id 
			JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			left join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
			left join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
			WHERE SOD.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('06','07','08','09','11','14'))
				AND SOD.SettlementType <> 3 
				AND SOD.IsDelete = 0 
		UNION ALL 
			SELECT 
				SOD.Id, I.Id, xi.InvoiceNumber as InvoiceNumber, 
				CA.CODIPSSEC AS IPSCode, 
				TIP.SIGLA AS IdentificationTypeCode, 
				pacient.IPCODPACI as IdentificationNumber, 
				RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
				1 AS TypeService, 
				IP.Code as RIPSCode, 
				IP.CodeAlternative as CUPSCode, 
				IP.Name as CUPSDescription,
				IP.CodeAlternativeTwo as ManualRateCode, 
				IP.Name as ManualRateName, 
				SOD.InvoicedQuantity as InvoicedQuantity, 
				CAST(ROUND((ID.GrandTotalSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) as TotalSalesPrice, 
				Id.GrandTotalSalesPrice AS GrandTotalSalesPrice,
				'' ContractDescriptionCode,
				'' ContractDescriptionName,
				PT.Class AS ProductRateClass,
				NULL AS RIPSConcept
			FROM Billing.Invoice I WITH (NOLOCK) 
			JOIN @Invoices xi ON i.Id = xi.InvoiceId
			JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.ID 
			JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.Id 
			JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON SOD.ProductId = ip.id 
			JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.Id 
			JOIN [Contract].HealthAdministrator HA WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id 
			JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			WHERE SOD.InvoicedQuantity > 0 
				AND I.[Status] = 1 
				AND ((@PackageDetail = 1 AND sod.Packaging = 1) OR PT.Class = 3)
				AND SOD.SettlementType <> IIF(@PackageDetail = 0,3,0) 
				AND SOD.IsDelete = 0
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados

	If @PackageDetail = 1 Begin
		INSERT Into @TableResult
		select SOD.Id, tr.InvoiceId, tr.InvoiceNumber as InvoiceNumber, 
			LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
			tr.IdentificationTypeCode, 
			tr.IdentificationNumber, 
			RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
			CASE CUPS.RIPSConcept 
				WHEN '06' THEN 3 
				WHEN '07' THEN 4 
				WHEN '08' THEN 3 
				WHEN '09' THEN 1 
				WHEN '11' THEN 1 
				WHEN '14' THEN 2 
				ELSE 0 
			END AS TypeService, 
			CUPS.RIPSCode as RIPSCode, 
			RTRIM(CUPS.Code) as CUPSCode, 
			ISNULL(RTRIM(CUPS.RIPSDescription),RTRIM(CUPS.Description)) as CUPSDescription,
			RTRIM(IPS.Code) as ManualRateCode, 
			RTRIM(IPS.Name) as ManualRateName, 
			SOD.InvoicedQuantity as InvoicedQuantity, 
			0 as TotalSalesPrice, 
			0 AS GrandTotalSalesPrice,
			ISNULL(cd.Code, '') ContractDescriptionCode,
			ISNULL(cd.Name, '') ContractDescriptionName,
			NULL AS ProductRateClass,
			CUPS.RIPSConcept
		from @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
		JOIN [Contract].IPSService IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id 
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId 
		left join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
		left join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
		WHERE SOD.CUPSEntityId IS NOT NULL 
				AND SOD.InvoicedQuantity > 0 
				AND cups.RIPSConcept in ('06','07','08','09','11','14')
				AND SOD.SettlementType <> 3 
				AND SOD.IsDelete = 0 
		UNION ALL
		select SOD.Id, tr.InvoiceId, tr.InvoiceNumber as InvoiceNumber, 
			CA.CODIPSSEC AS IPSCode, 
			tr.IdentificationTypeCode, 
			tr.IdentificationNumber, 
			RTRIM(ISNULL(SOD.AuthorizationNumber, ISNULL(admission.IAUTORIZA,''))) as AuthorizationNumber, 
			1 AS TypeService, 
			IP.Code as RIPSCode, 
			IP.CodeAlternative as CUPSCode, 
			IP.Name as CUPSDescription,
			IP.CodeAlternativeTwo as ManualRateCode, 
			IP.Name as ManualRateName, 
			SOD.InvoicedQuantity as InvoicedQuantity, 
			0 as TotalSalesPrice, 
			0 AS GrandTotalSalesPrice,
			'' ContractDescriptionCode,
			'' ContractDescriptionName,
			PT.Class AS ProductRateClass,
			NULL AS RIPSConcept
		from @TableResult tr
		JOIN Billing.ServiceOrderDetail SOD WITH(NOLOCK) ON sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON SOD.ProductId = ip.id 
		JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.Id 
		JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON SO.AdmissionNumber = admission.NUMINGRES 
		JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
		WHERE SOD.ProductId IS NOT NULL
			AND sod.InvoicedQuantity > 0 
			AND sod.SettlementType <>  IIF(@PackageDetail = 0,3,0)   
			AND sod.IsDelete = 0 
			AND PT.Class = 3
	End

	SELECT * 
	FROM @TableResult
	WHERE (RIPSConcept IS NULL OR RIPSConcept IN ('06','07','08','09','11','14')) AND (ProductTypeClass IS NULL OR ProductTypeClass = 3)
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera los datos del archivo AT de RIPS para un radicado de cartera o un conjunto de facturas indicadas en XML. Recopila el detalle de servicios facturados (procedimientos, medicamentos, paquetes) cruzando facturas, órdenes de servicio, contratos, tarifas IPS y datos del paciente (cédula, tipo de identificación, número de ingreso, autorización) para construir la estructura requerida por el reporte AT de RIPS. Maneja tres escenarios de origen de facturas: selección manual por XML, facturas de un radicado de cartera activo, y facturas de capitación relacionadas a ese radicado. Es usado en el proceso de glosas y facturación para exportar información de prestación de servicios a las entidades pagadoras (EPS, aseguradoras).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateATFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateATFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los datos del archivo AT de RIPS (procedimientos y otros servicios) a partir de las facturas indicadas por XML o asociadas a un radicado de cartera, detallando ítems CUPS, productos clase 3 e ítems incluidos en paquetes.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas a procesar deben tener Status=1 (activas); Los detalles de orden de servicio deben tener InvoicedQuantity>0, IsDelete=0 y SettlementType distinto de 3 (excepto en modo paquete donde se admite SettlementType<>0); El paciente debe existir en INPACIENT y su tipo de documento en ADTIPOIDENTIFICA; La orden de servicio debe estar asociada a un ingreso (ADINGRESO) con centro de atención válido (ADCENATEN); Si se usa @XmlInvoices, debe contener nodos /Data con InvoiceId existentes en Billing.Invoice; Si se usa @RadicateInvoiceId, deben existir radicados con detalles cuyo State<>4', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ítems facturados (InvoicedQuantity>0) y no eliminados (IsDelete=0); Solo se consideran facturas activas (Status=1); Se excluyen detalles con SettlementType=3 (salvo modo paquete que invierte la regla en la rama de productos); Las facturas de capitación (DocumentType=4) son sustituidas por su correspondiente factura de cápita (DocumentType=5) dentro del periodo de capitación; Los ítems internos de un paquete se reportan con TotalSalesPrice y GrandTotalSalesPrice en 0 (no suman valor); El TotalSalesPrice se calcula como GrandTotalSalesPrice/InvoicedQuantity redondeado a entero; La salida final solo incluye conceptos RIPS válidos para el archivo AT (''06'',''07'',''08'',''09'',''11'',''14'') o productos de clase 3; El resultado se ordena por la parte numérica del número de factura; Los errores se capturan en TRY/CATCH y solo se imprimen, sin propagar excepción', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS; Archivo AT (procedimientos); CUPS; Factura de venta; Factura de capitación; Radicado de cartera; Autorización; Orden de servicio; Ingreso del paciente; IPS; Tipo de identificación del paciente; Paquete de servicios; Tarifario manual; Contrato', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Inserta un registro por cada ServiceOrderDetail cuando CUPSEntity.RIPSConcept ∈ (''06'',''07'',''08'',''09'',''11'',''14'') o (modo paquete y SOD.IsPackage=1), mapeando RIPSConcept a TypeService (06/08→3, 07→4, 09/11→1, 14→2, otros→0); [INSERT] @TableResult: Inserta un registro por cada ServiceOrderDetail asociado a un producto de inventario cuando ProductType.Class=3 (o modo paquete con sod.Packaging=1), asignando TypeService=1; [INSERT] @TableResult: Cuando @PackageDetail=1, agrega los ítems internos del paquete (SOD cuyo PackageServiceOrderDetailId apunta a un detalle ya cargado), tanto CUPS con RIPSConcept válido como productos clase 3, con TotalSalesPrice y GrandTotalSalesPrice forzados a 0; [RETURN_RESULT] RESULT: Retorna la tabla resultado filtrando solo filas con RIPSConcept NULL o en (''06'',''07'',''08'',''09'',''11'',''14'') y ProductTypeClass NULL o =3, ordenado por la parte numérica del InvoiceNumber', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType <> 4 (en facturas del XML o del radicado) → Se incluye la factura directamente en @Invoices; si i.DocumentType = 4 AND existe Invoice cc con DocumentType=5, mismo ThirdPartyId/CareGroupId/InvoiceCategoryId y cc.InvoiceDate dentro del rango de capitación de i → Se reemplaza la factura de capitación (DocType 4) por la factura de cápita asociada (DocType 5); si CUPS.RIPSConcept IN (''06'',''08'') → TypeService=3 else Se evalúan otros valores; si CUPS.RIPSConcept = ''07'' → TypeService=4; si CUPS.RIPSConcept IN (''09'',''11'') → TypeService=1; si CUPS.RIPSConcept = ''14'' → TypeService=2 else TypeService=0; si @PackageDetail=1 → Se incluyen ítems con IsPackage=1 / Packaging=1 y se procesan los detalles internos del paquete (PackageServiceOrderDetailId) con valores en cero else Solo se filtran ítems por RIPSConcept válido o ProductType.Class=3; si @PackageDetail=0 → SOD.SettlementType debe ser distinto de 3 en la rama de productos else En modo paquete se exige SettlementType<>0 en la rama de productos; si SOD.AuthorizationNumber IS NULL → Se usa admission.IAUTORIZA como número de autorización; si también es NULL se devuelve cadena vacía; si CUPS.RIPSDescription IS NULL → Se usa CUPS.Description como descripción del procedimiento', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.RevenueControlDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.HealthAdministrator; Inventory.InventoryProduct; Inventory.ProductType; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateATFileData';
-- GO
