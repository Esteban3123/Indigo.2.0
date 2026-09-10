
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 15-03-2017
-- Description:	Procedimiento para creación XML Circular015
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReportCircular015] 
	-- Add the parameters for the stored procedure here
	@IntialDate date,
	@EndDate date,
	@idPrestador as varchar(20)
AS
BEGIN	
	BEGIN TRY
		DECLARE @Cicrcular015XML as XML
		SELECT @Cicrcular015XML =  CONVERT(XML,
				(
					SELECT ISNULL(CASE RTRIM(fu.NUMSOA) 
										WHEN '' THEN '0'
										ELSE RTRIM(fu.NUMSOA) 
										END,'0') as idPoliza, 
						   ISNULL(CONVERT(VARCHAR(24),fu.FECFINPOL,112),'00000000') as fechaVencimiento,
						   t.Nit as idVictima, 
						   replace(ou.IPSCode, '-', '') as codigoPrestador, 
						   @idPrestador as idPrestador, -- Este se obtiene de la singleton
						   CONVERT(VARCHAR(24),fu.FECOCUEVE,112) as fechaSiniestro, 
						   te.Nit as idPagador,
						   i.InvoiceNumber as idFactura,
						   CONVERT(VARCHAR(24),i.InvoicedDate, 112) as fechaFactura,
						   CONVERT(VARCHAR(24),radicate.RadicatedDate, 112) as fechaRadicacion, 
						   ISNULL(
					(
						   SELECT SUM(id.ThirdPartySalesPrice)
						   FROM Billing.InvoiceDetail id
								 JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
								 JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
							WHERE ips.Presentation IN (2,3) 
								  AND id.InvoiceId = i.Id 
								  AND sod.IsDelete = 0),0) as valorProcedimientos, 
									ISNULL(
					(
							SELECT SUM(id.ThirdPartySalesPrice)
							FROM Billing.InvoiceDetail id
								 JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
							     JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = sod.ProductId
								 JOIN Inventory.ProductType pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
							WHERE pt.Class = 3 
								 AND id.InvoiceId = i.Id 
								 AND sod.IsDelete = 0),0) as valorInsumos, 
										ISNULL(
					(
							SELECT SUM(id.ThirdPartySalesPrice)
							FROM Billing.InvoiceDetail id
								 JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
								 JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = sod.ProductId
								 JOIN Inventory.ProductType pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
							WHERE pt.Class = 2 
								AND id.InvoiceId = i.Id 
								AND sod.IsDelete = 0),0) as valorMedicamentos,
								ISNULL(
					(
							SELECT SUM(id.ThirdPartySalesPrice)
							FROM Billing.InvoiceDetail id
									JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
									JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
							WHERE ips.Presentation NOT IN (2,3) 
								  AND id.InvoiceId = i.Id 
								  AND sod.IsDelete = 0),0) as valorOtros, 
									i.TotalInvoice as valorFactura
							FROM Portfolio.AccountReceivable ar
							 JOIN Billing.Invoice i WITH(NOLOCK) ON i.InvoiceNumber = ar.InvoiceNumber
							 JOIN Common.ThirdParty te WITH(NOLOCK) ON te.Id = i.ThirdPartyId
							 JOIN Common.OperatingUnit ou WITH(NOLOCK) ON ou.Id = i.OperatingUnitId
							 JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Nit = i.PatientCode
							 JOIN 
						(
							 SELECT rd.InvoiceNumber, 
									rc.RadicatedDate 
							 FROM Portfolio.RadicateInvoiceC rc
							 JOIN Portfolio.RadicateInvoiceD rd WITH(NOLOCK) ON rc.Id = rd.RadicateInvoiceCId 
								WHERE rc.State = 2 
									  AND rd.State = 2) as radicate  ON radicate.InvoiceNumber = ar.InvoiceNumber
							 JOIN dbo.ADFURIPSU fu WITH(NOLOCK) ON fu.NUMINGRES = i.AdmissionNumber
							WHERE I.InvoiceDate BETWEEN  cast(@IntialDate as date) AND cast(@EndDate as date) 

			For XML path('RegistroST006'), ELEMENTS));

		SELECT @Cicrcular015XML
	END TRY
	BEGIN CATCH
		-- Insert statements for procedure here
		SELECT 'Error ! '+ ERROR_MESSAGE() + ' Line: ' + cast(ERROR_LINE() as varchar(3)) as MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo XML requerido por la Circular 015 (reporte regulatorio de atenciones a víctimas con póliza SOAT u otros seguros), consultando facturas radicadas y aprobadas en cartera para un rango de fechas y un prestador específico. Integra datos de la factura (número, fecha, valor total), del radicado de cobro (fecha de radicación), del paciente víctima y del pagador (NIT), de la unidad operativa (código IPS), y del ingreso clínico (número de póliza SOAT, fecha de vencimiento de póliza, fecha del siniestro). Desglosa el valor facturado en cuatro categorías: procedimientos, insumos, medicamentos y otros servicios, tomando los precios del detalle de factura según el tipo de producto o presentación del servicio contratado. Solo incluye facturas cuyo radicado está en estado activo/aprobado (State = 2) tanto en el encabezado como en el detalle del radicado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCircular015';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCircular015';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un XML con el reporte regulatorio Circular 015 (RegistroST006) consolidando facturas radicadas de pacientes con FURIPS por accidente de tránsito en un rango de fechas, desglosando valores por procedimientos, insumos, medicamentos y otros.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben estar radicadas en Portfolio.RadicateInvoiceC y Portfolio.RadicateInvoiceD ambas con State = 2 (radicación confirmada); Cada factura debe tener un registro asociado en dbo.ADFURIPSU vinculado por NUMINGRES = AdmissionNumber (atención de urgencia por accidente de tránsito); El paciente (PatientCode) debe existir como tercero en Common.ThirdParty (matching por Nit); La factura debe tener cuenta por cobrar en Portfolio.AccountReceivable; InvoiceDate debe estar dentro del rango [@IntialDate, @EndDate]', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con radicación efectiva (State=2 en cabecera y detalle de radicación); Los detalles eliminados (sod.IsDelete=1) nunca se totalizan en ningún rubro de valor; El código de prestador (codigoPrestador) se reporta sin guiones (replace de ''-''); Las fechas se serializan en formato AAAAMMDD (estilo 112); El idPrestador del XML proviene del parámetro de entrada (no de la base de datos); La clasificación de valores es excluyente entre Procedimientos/Otros (vía IPSService.Presentation) e Insumos/Medicamentos (vía ProductType.Class 2 y 3); Cuando una sumatoria no encuentra registros, se reporta 0 en lugar de NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Circular 015 (reporte regulatorio); FURIPS - Accidente de tránsito; Póliza SOAT; Fecha de siniestro; Radicación de factura; Pagador / Víctima / Prestador; Procedimientos; Insumos; Medicamentos; Cuentas por cobrar; Unidad operativa / IPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] XML RegistroST006: Devuelve un único XML con elementos RegistroST006 por cada factura que cumple los filtros (radicación State=2 y rango de InvoiceDate); [RETURN_RESULT] MessageResult: En caso de excepción, retorna un mensaje ''Error ! '' + ERROR_MESSAGE() + '' Line: '' + ERROR_LINE() en lugar del XML', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RTRIM(fu.NUMSOA) = '''' o NULL → idPoliza se reporta como ''0'' else idPoliza toma el valor de fu.NUMSOA; si fu.FECFINPOL es NULL → fechaVencimiento se reporta como ''00000000'' else fechaVencimiento toma FECFINPOL en formato 112 (yyyymmdd); si IPSService.Presentation IN (2,3) y sod.IsDelete=0 → El detalle suma a valorProcedimientos else Si Presentation NOT IN (2,3), suma a valorOtros; si ProductType.Class = 3 y sod.IsDelete=0 → El detalle suma a valorInsumos; si ProductType.Class = 2 y sod.IsDelete=0 → El detalle suma a valorMedicamentos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.IPSService; Inventory.InventoryProduct; Inventory.ProductType; Portfolio.AccountReceivable; Billing.Invoice; Common.ThirdParty; Common.OperatingUnit; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCircular015';
-- GO
