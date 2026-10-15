
/****** Object:  View [Billing].[ViewListDiagnosticImaging]   ******/
CREATE VIEW [Billing].[ViewListDiagnosticImaging]
as
(
	select distinct cup.Id as ID, cup.Code, cup.Description, hcor.AUTO, hcor.ESTSERIPS, hcor.MEDREALEC, sdet.Id as ServiceOrderDetailId
	from Billing.ServiceOrderDetail sdet
		inner join Contract.CUPSEntity cup on cup.Id = sdet.CUPSEntityId
		inner join HCORDIMAG hcor on hcor.CODSERIPS = cup.Code and hcor.GENSERVICEORDER = sdet.ServiceOrderId

)