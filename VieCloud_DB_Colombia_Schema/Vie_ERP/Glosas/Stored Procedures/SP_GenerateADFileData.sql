
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-01-31
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo AD de RIPS
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateADFileData] 
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
		InvoiceNumber VARCHAR(50), 
		IPSCode VARCHAR(50), 
		RIPSConcept VARCHAR(50),
		Quantity Int,
		TotalSalesPrice DECIMAL(18,0),
		NetToPay DECIMAL(18,0)
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
		
			INSERT INTO @TableResult 
			 SELECT 
				SOD.Id,
				xi.InvoiceNumber AS InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				RTRIM(CUPS.RIPSConcept) AS RIPSConcept,
				IIF(SOD.InvoicedQuantity > 1, 1,SOD.InvoicedQuantity)as Quantity,--
				CAST(ROUND((ID.ThirdPartySalesPrice + ID.SubTotalPatientSalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS TotalSalesPrice, 
				CAST(ROUND((ID.ThirdPartySalesPrice) / SOD.InvoicedQuantity, 0) AS NUMERIC(18,2)) AS NetToPay--
			 FROM Billing.Invoice AS I WITH (NOLOCK)
			 JOIN @Invoices xi ON i.Id = xi.InvoiceId
			 JOIN Billing.InvoiceDetail ID WITH (NOLOCK) ON ID.InvoiceId = I.Id 
			 JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId 
			 JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
			 CROSS APPLY [Billing].[DuplicateRows](SOD.Id, SOD.InvoicedQuantity) 
			 WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0) 
				and sod.InvoicedQuantity > 0 
				AND SOD.IsDelete = 0 
				AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('01','02','03','04','05','06','07','08','09','11', '12', '13','14'))
				AND I.[Status] = 1
				AND SOD.SettlementType <> 3 

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	If @PackageDetail = 1 Begin

		INSERT INTO @TableResult 
		SELECT 
				SOD.Id,
				tr.InvoiceNumber AS InvoiceNumber, 
				LTRIM(RTRIM(CA.CODIPSSEC)) AS IPSCode, 
				RTRIM(CUPS.RIPSConcept) AS RIPSConcept,
				SOD.InvoicedQuantity as Quantity,
				tr.TotalSalesPrice AS TotalSalesPrice, 
				tr.NetToPay AS NetToPay
		FROM @TableResult tr
		JOIN Billing.ServiceOrderDetail sod with(nolock) on sod.PackageServiceOrderDetailId = tr.ServiceOrderDetailId
		JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
		JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = SO.AdmissionNumber 
		JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
		JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
		JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
		JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 	
		WHERE SOD.SettlementType <> IIF(@PackageDetail = 0,3,0)  
			and sod.InvoicedQuantity > 0 
			AND SOD.IsDelete = 0 
			AND ((@PackageDetail = 1 AND sod.IsPackage = 1) OR cups.RIPSConcept in ('01','02','03','04','05','06','07','08','09','11', '12', '13','14'))
			AND SOD.SettlementType <> 3 
	End

	SELECT 
	InvoiceNumber,
	IPSCode,
	RIPSConcept,
	Quantity,
	TotalSalesPrice,
	NetToPay 
	FROM @TableResult
	WHERE RIPSConcept IS NULL OR RIPSConcept in  ('01','02','03','04','05','06','07','08','09','11', '12', '13','14')
	ORDER BY CAST(SUBSTRING(InvoiceNumber + '0', PATINDEX('%[0-9]%', InvoiceNumber + '0'), LEN(InvoiceNumber + '0')) AS DECIMAL)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo AD de RIPS para un radicado de cartera, consolidando los servicios facturados (procedimientos, medicamentos, insumos) con su concepto RIPS, cantidades, precio de venta y valor neto a pagar. Toma como entrada un identificador de radicado de facturas ante una entidad pagadora (EPS o aseguradora) y opcionalmente un listado adicional de facturas en formato XML, incluyendo el manejo especial de contratos de capitación. Compone la información cruzando las facturas radicadas (RadicateInvoiceC y RadicateInvoiceD), el detalle de facturación (Invoice e InvoiceDetail), las órdenes de servicio (ServiceOrderDetail) con su código CUPS/RIPS, y los datos del centro de atención (ADCENATEN y ADINGRESO), devolviendo el resultado ordenado por número de factura para su uso en la generación de reportes RIPS y el trámite de glosas con entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateADFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateADFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los datos del archivo AD de RIPS (consultas/procedimientos) a partir de facturas asociadas a un radicado o recibidas por XML, expandiendo cantidades y devolviendo conceptos RIPS válidos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Billing.Invoice y estar en Status = 1 para ser consideradas en el primer bloque de inserción.; Los detalles de orden de servicio deben tener IsDelete = 0 e InvoicedQuantity > 0.; Si se procesa por radicado, RadicateInvoiceD.State debe ser distinto de 4 (no anulado).; Las admisiones referenciadas en ServiceOrder deben existir en dbo.ADINGRESO y su centro de atención en dbo.ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan conceptos RIPS de tipo consulta/procedimiento (''01''-''09'',''11''-''14'') o nulos.; Las facturas de capitación (DocumentType=4) nunca aparecen directamente; siempre se mapean a su factura de evento (DocumentType=5) dentro del periodo de capitación.; Nunca se procesan detalles eliminados (IsDelete=1), sin cantidad facturada o con SettlementType=3.; El precio unitario reportado se calcula prorrateando el valor total entre la cantidad facturada y redondeando a entero.; Los errores no abortan la ejecución: se capturan e imprimen, devolviendo lo acumulado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS archivo AD; Factura de venta; Radicación de cuentas; Capitación (DocumentType 4) y factura de evento (DocumentType 5); CUPS (procedimientos en salud); Admisión / centro de atención (IPS); Orden de servicio y paquetes de servicios; Glosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Por cada detalle de factura cuyo SOD.SettlementType ≠ 3, IsDelete = 0, InvoicedQuantity > 0, factura con Status=1 y (IsPackage=1 cuando @PackageDetail=1, o RIPSConcept en 01–09,11–14), se inserta una fila por unidad facturada vía Billing.DuplicateRows, fijando Quantity en 1 cuando InvoicedQuantity>1.; [INSERT] @TableResult: Cuando @PackageDetail = 1, además se insertan los SOD hijos cuyo PackageServiceOrderDetailId apunta a filas ya cargadas, conservando IPS, RIPSConcept y valores del paquete padre.; [INSERT] @Invoices: Se cargan facturas: (a) las del XML cuyo DocumentType ≠ 4; (b) las del radicado @RadicateInvoiceId con RadicateInvoiceD.State ≠ 4 y DocumentType ≠ 4; (c) para facturas DocumentType = 4 (capitación), se sustituyen por la factura cc con DocumentType = 5 del mismo tercero/grupo/categoría cuya InvoiceDate cae entre CapitationInitialDate y CapitationEndDate.; [RETURN_RESULT] RESULT: Se retornan únicamente las filas con RIPSConcept NULL o en (''01''..''09'',''11''..''14''), ordenadas por la parte numérica del InvoiceNumber.; [RAISERROR] CONSOLE: En caso de excepción dentro del TRY, se imprime ERROR_MESSAGE() y la línea, sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType = 4 (factura de capitación) en el flujo del radicado → Se busca la factura asociada (DocumentType=5) del mismo tercero, grupo de atención y categoría cuya fecha caiga en el rango de capitación, y se usa su Id como InvoiceId else Se usa directamente la factura con DocumentType ≠ 4; si @PackageDetail = 1 → Adicionalmente se cargan los SOD hijos del paquete (PackageServiceOrderDetailId) y el filtro permite SOD.IsPackage = 1 else Solo se procesan ítems con RIPSConcept en la lista permitida y se excluye SettlementType = 3; si SOD.InvoicedQuantity > 1 → Se fuerza Quantity = 1 (una fila por unidad gracias a DuplicateRows) else Se usa la InvoicedQuantity original; si SOD.SettlementType <> IIF(@PackageDetail = 0, 3, 0) → Cuando @PackageDetail=0 excluye SettlementType=3; cuando @PackageDetail=1 excluye SettlementType=0', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.DuplicateRows', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.CUPSEntity; dbo.ADINGRESO; dbo.ADCENATEN; Inventory.InventoryProduct; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateADFileData';
-- GO
