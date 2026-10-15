
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

		SELECT @Cicrcular015XML = CONVERT(XML,
		(
			SELECT
				ISNULL(CASE RTRIM(fu.NUMSOA) 
							WHEN '' THEN '0'
							ELSE RTRIM(fu.NUMSOA) 
					  END,'0') as idPoliza, 
				ISNULL(CONVERT(VARCHAR(24),fu.FECFINPOL,112),'00000000') as fechaVencimiento,
				t.Nit as idVictima, 
				REPLACE(ou.IPSCode, '-', '') as codigoPrestador, 
				@idPrestador as idPrestador, -- Este se obtiene de la singleton

				CONVERT(VARCHAR(24),fu.FECOCUEVE,112) as fechaSiniestro, 

				te.Nit as idPagador,
				i.InvoiceNumber as idFactura,
				CONVERT(VARCHAR(24),i.InvoicedDate, 112) as fechaFactura,
				CONVERT(VARCHAR(24),radicate.RadicatedDate, 112) as fechaRadicacion, 

				ISNULL((
					SELECT SUM(id.ThirdPartySalesPrice)
					FROM Billing.InvoiceDetail id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
					JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
					WHERE ips.Presentation IN (2,3) 
					  AND id.InvoiceId = i.Id 
					  AND sod.IsDelete = 0
				),0) as valorProcedimientos, 

				ISNULL((
					SELECT SUM(id.ThirdPartySalesPrice)
					FROM Billing.InvoiceDetail id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = sod.ProductId
					JOIN Inventory.ProductType pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
					WHERE pt.Class = 3 
					  AND id.InvoiceId = i.Id 
					  AND sod.IsDelete = 0
				),0) as valorInsumos, 

				ISNULL((
					SELECT SUM(id.ThirdPartySalesPrice)
					FROM Billing.InvoiceDetail id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = sod.ProductId
					JOIN Inventory.ProductType pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
					WHERE pt.Class = 2 
					  AND id.InvoiceId = i.Id 
					  AND sod.IsDelete = 0
				),0) as valorMedicamentos,

				ISNULL((
					SELECT SUM(id.ThirdPartySalesPrice)
					FROM Billing.InvoiceDetail id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
					JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
					WHERE ips.Presentation NOT IN (2,3) 
					  AND id.InvoiceId = i.Id 
					  AND sod.IsDelete = 0
				),0) as valorOtros, 

				i.TotalInvoice as valorFactura
			FROM Portfolio.AccountReceivable ar
			JOIN Billing.Invoice i WITH(NOLOCK) ON i.InvoiceNumber = ar.InvoiceNumber
			JOIN Common.ThirdParty te WITH(NOLOCK) ON te.Id = i.ThirdPartyId
			JOIN Common.OperatingUnit ou WITH(NOLOCK) ON ou.Id = i.OperatingUnitId
			JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Nit = i.PatientCode
			JOIN (
				SELECT rd.InvoiceNumber, rc.RadicatedDate
				FROM Portfolio.RadicateInvoiceC rc
				JOIN Portfolio.RadicateInvoiceD rd WITH(NOLOCK) ON rc.Id = rd.RadicateInvoiceCId 
				WHERE rc.State = 2 
				  AND rd.State = 2
			) as radicate ON radicate.InvoiceNumber = ar.InvoiceNumber
			JOIN dbo.ADFURIPSU fu WITH(NOLOCK) ON fu.NUMINGRES = i.AdmissionNumber

			WHERE I.InvoiceDate BETWEEN CAST(@IntialDate as date) AND CAST(@EndDate as date)
			  AND fu.FECOCUEVE IS NOT NULL

			FOR XML PATH('RegistroST006'), ELEMENTS
		));

		SELECT @Cicrcular015XML
	END TRY
	BEGIN CATCH
		SELECT 'Error ! '+ ERROR_MESSAGE() + ' Line: ' + CAST(ERROR_LINE() as varchar(3)) as MessageResult
	END CATCH
END