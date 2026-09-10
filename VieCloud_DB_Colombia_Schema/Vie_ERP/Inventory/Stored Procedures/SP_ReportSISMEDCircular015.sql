-- =============================================
-- Author:		Andres Alarcon
-- Create date: 21/09/2023
-- Description:	Procedimiento para el reporte de SISMED Circular 015
-- =============================================

CREATE PROCEDURE [Inventory].[SP_ReportSISMEDCircular015]
	@dateStart DATE,
	@dateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	-- Creamos la tabla temporal que devolveremos con los datos
	DECLARE @tableExecution TABLE
	(
		Habilitacion varchar(20),
		Mes VARCHAR(2), 
		TypeOperation VARCHAR(2), 
		TipoPersonaMin VARCHAR(2),
		NITMin varchar(20),
		TipoPersonaMax VARCHAR(2),
		NITMax varchar(20),
		IUM VARCHAR(20),
		ValorMinimo DECIMAL(18,2), 
		ValorMaximo DECIMAL(18,2), 
		TotalVentas DECIMAL(18,2), 
		Cantidad DECIMAL(18,2), 
		FacturaVentaMin VARCHAR(100), 
		FacturaVentaMax VARCHAR(100)
	)

		INSERT INTO @TableExecution 
			(Habilitacion, Mes, IUM, ValorMinimo, ValorMaximo, TotalVentas, Cantidad, TypeOperation, TipoPersonaMin, NITMin, FacturaVentaMin, TipoPersonaMax, NITMax, FacturaVentaMax)

			--Ventas'VN'
			SELECT	(SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ip.InvoiceDate) AS VARCHAR(2)),2),
					ISNULL(ip.IUM, '') IUM,
					MIN(ip.UnitValue) AS 'Valor Minimo', 
					MAX(ip.UnitValue) AS 'Valor Maximo',
					SUM(ip.Subtotalvalue) AS 'Total Ventas',
					SUM(ip.Quantity) AS 'Cantidad',
					'VN' TypeOperation,
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) TipoPersonaMin,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) NITMin,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) AS 'Factura de Venta Min',  
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) TipoPersonaMax,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id,MAX(ip.UnitValue)) NITMax,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) AS 'Factura de Venta Max'  
			FROM
			(
					SELECT	pt.Class, 
							ip.Id, 
							ip.IUM, 
							i.InvoiceDate,
							sod.SubTotalSalesPrice UnitValue,
							sod.InvoicedQuantity Quantity,
							sod.GrandTotalSalesPrice Subtotalvalue
					FROM Billing.Invoice i WITH (NOLOCK)    
					JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
					JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND ISNULL(atc.ProductNPT, 0) = 0

				UNION ALL

					SELECT	pt.Class, 
							ip.Id,
							ip.IUM, 
							i.InvoiceDate,
							dipsd.SalePrice UnitValue,
							dipsd.Quantity Quantity,
							dipsd.TotalValue Subtotalvalue
					FROM Billing.Invoice i WITH (NOLOCK)
					JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
					JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
					JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND ISNULL(atc.ProductNPT, 0) = 0

			) ip
			GROUP BY YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate),
				ip.Id, ip.Class, ip.IUM

		UNION ALL
			--Compras'CM'
			SELECT  (SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ev.DocumentDate) AS VARCHAR(2)),2),
					ISNULL(ip.IUM, '') IUM,
					MIN(evd.UnitValue) AS 'Valor Minimo',
					MAX(evd.UnitValue) AS 'Valor Maximo',
					SUM(evd.Subtotalvalue) AS 'Total Ventas',
					SUM(evd.Quantity) AS 'Cantidad',
					'CM' TypeOperation,
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) TipoPersonaMin,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) NITMin,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) AS 'Factura de Venta Min',  
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) TipoPersonaMax,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id,MAX(evd.UnitValue)) NITMax,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) AS 'Factura de Venta Max'  
			FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
			JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
			JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
			WHERE ev.Status = 2 
				AND ev.DocumentDate BETWEEN @dateStart and @dateEnd
				AND ISNULL(atc.ProductNPT, 0) = 0
			GROUP BY YEAR(ev.DocumentDate), MONTH(ev.DocumentDate),
			ip.id, pt.Class, ip.CodeCUM, ip.IUM
			

	SELECT	ROW_NUMBER() OVER(ORDER BY Mes) as Consecutivo, 
			*
	FROM @tableExecution
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte SISMED Circular 015 requerido por el INVIMA para la vigilancia de precios de medicamentos. Consolida en una tabla de resultados dos tipos de operaciones: ventas (VN) de medicamentos facturados a pacientes y terceros pagadores, y compras (CM) registradas en comprobantes de entrada al inventario, todo dentro de un rango de fechas indicado. Por cada medicamento (identificado por su código IUM y clasificado como tipo de producto clase 2 en el catálogo ATC, excluyendo productos NPT) calcula el valor mínimo, valor máximo, total de ventas y cantidad vendida o comprada por mes, junto con el NIT, tipo de persona y número de factura correspondientes a esos valores extremos. Utiliza funciones auxiliares del esquema Inventory (TypePersonThirdPartySISMED, NITThirdPartySISMED, InvoiceSISMED) para identificar el tercero asociado a cada precio mínimo y máximo, y retorna el resultado con un consecutivo para facilitar la presentación del informe regulatorio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDCircular015';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDCircular015';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte SISMED Circular 015 consolidando ventas y compras mensuales de medicamentos por producto (IUM), con valores mínimo/máximo, totales, cantidades y datos del tercero asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango @dateStart - @dateEnd debe estar definido para filtrar facturas y comprobantes.; Debe existir al menos un registro en ADCENATEN para obtener el código de habilitación (CODIPSSEC).; Las funciones Inventory.TypePersonThirdPartySISMED, Inventory.NITThirdPartySISMED e Inventory.InvoiceSISMED deben existir y ser invocables.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos cuyo ProductType.Class = 2 (clase de medicamentos/insumos aplicable a SISMED).; Solo se incluyen productos cuyo ATC.ProductNPT = 0 (excluye productos marcados como NPT).; Las ventas solo consideran facturas con Status = 1 (activas/emitidas).; En la fuente de ventas por orden de servicio, solo se incluyen líneas con GrandTotalSalesPrice > 0.; Las compras solo consideran EntranceVoucher con Status = 2 (recepción confirmada/aprobada).; El campo Habilitacion siempre proviene del primer CODIPSSEC encontrado en ADCENATEN (TOP 1 sin ORDER BY).; El mes se reporta siempre con dos dígitos (formato ''MM'').; La agregación se realiza por año, mes, producto, clase e IUM, calculando MIN/MAX de valor unitario y SUM de cantidades y valores.; El IUM nulo se reemplaza por cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SISMED Circular 015; Habilitación IPS (CODIPSSEC); Medicamentos (clasificación ATC); IUM (Identificador Único de Medicamento); Ventas de medicamentos (VN); Compras de medicamentos (CM); Tercero (NIT y tipo de persona); Factura de venta; Comprobante de entrada de inventario; Producto NPT (excluido del reporte)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado (result set): Devuelve un conjunto numerado (ROW_NUMBER por Mes) con el consolidado mensual de ventas (VN) y compras (CM) por producto/IUM dentro del rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del registro de ventas: factura con InvoiceDetail/ServiceOrderDetail vs. DocumentInvoiceProductSales (ventas directas de inventario) → Se unen ambas fuentes vía UNION ALL para conformar el universo de ventas ''VN''. else Las compras ''CM'' se obtienen exclusivamente de EntranceVoucher/EntranceVoucherDetail.; si Tipo de operación reportada → Para ventas se etiqueta ''VN'' y se invoca InvoiceSISMED con tipo=1; para compras ''CM'' con tipo=2.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.TypePersonThirdPartySISMED; Inventory.NITThirdPartySISMED; Inventory.InvoiceSISMED', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ProductType; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCircular015';
-- GO
