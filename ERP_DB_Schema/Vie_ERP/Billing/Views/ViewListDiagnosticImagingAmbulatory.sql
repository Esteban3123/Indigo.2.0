
/****** Object:  View [Billing].[ViewListDiagnosticImagingAmbulatory]   ******/
CREATE VIEW [Billing].[ViewListDiagnosticImagingAmbulatory]
as
(
	select distinct cup.Id as ID, cup.Code, cup.Description, ambor.AUTO, ambor.ESTSERIPS, ambor.MEDREALEC, sdet.Id as ServiceOrderDetailId
	from Billing.ServiceOrderDetail sdet
		inner join Contract.CUPSEntity cup on cup.Id = sdet.CUPSEntityId
		inner join AMBORDIMA ambor on ambor.CODSERIPS = cup.Code and ambor.GENSERVICEORDER = sdet.ServiceOrderId

)