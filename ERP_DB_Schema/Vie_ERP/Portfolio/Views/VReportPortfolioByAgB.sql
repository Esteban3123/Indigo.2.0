
CREATE VIEW [Portfolio].[VReportPortfolioByAgB]
as

SELECT ROW_NUMBER() OVER (ORDER BY TPAR.Nit) AS Id
	   ,AR.InvoiceNumber as DocumentCode
	   ,TPAR.Nit as ThirdPartyNit
	   ,TPAR.Name as ThirdPartyName
	   ,PAR.IdentificationType as IdentificationType
	   ,TPAR.Nit as CustomerNit
	   ,TPS.Nit as SellerNit
	   ,RADICATE.ConfirmDate as DocumentDate
	   ,RADICATE.ConfirmDate as expiredDate
	   ,ISNULL(RADICATE.ConfirmDate,convert(datetime, '1990-01-01 00:00:00', 120)) as DocumentDateCalculated
	   ,ISNULL(RADICATE.ConfirmDate, getdate()) as expiredDateCalculated
	   ,DATEDIFF(DAY,ISNULL(RADICATE.ConfirmDate, getdate()),getdate()) as Diferencia
	   ,AR.Balance as Balance
	   ,AR.AccountReceivableType as DocumentType
	   ,AR.PortfolioStatus as PortfolioStatus
	   ,AR.NumberShares as NumberShares
	   ,AR.Term as Term
	   ,AR.OpeningBalance as OpeningBalance
	   ,1 as AdvanceOrAccountReceivable
	   ,cg.EntityType as Regimen
	   ,cg.Code as CodeCareGroup
	   ,cg.Name as NameCareGroup
	   ,MA.Number as NumberAccount
	   ,RADICATE.RadicatedConsecutive as RadicatedConsecutive
	   ,RADICATE.RadicatedDate as RadicatedDate
	   ,cast(RADICATE.CreationUser as varchar(20)) RadicatedUser
	   ,case MA.Number when '14090103' then 'Contributivo' when '14090304' then 'Subsidiado' when '14090401' then 'Servicio IPS Privada' when '14090501' then 'Medicina Prepagada' when '14090601' then 'Compañias Aseguradoras' when '14090701' then 'Particulares' when '14090901' then 'Servicio IPS Publicas' when '14091004' then 'Regimen Especial' when '14091102' then 'Vinculados - Departamentos' when '14091103' then 'Vinculados Municipios' when '14091201' then 'Arl Riesgos Profesionales' when '14091403' then 'Accidentes de Transito' when '14090201' then 'Otras Cuentas X Cobrar' end as RegimenCalculated
  FROM  Portfolio.AccountReceivable AR with (nolock)
  inner join Common.ThirdParty TPAR with (nolock) on TPAR.Id = AR.ThirdPartyId 
  inner join Common.Person PAR with (nolock) on PAR.Id=TPAR.PersonId
  left outer join Common.Seller S with (nolock) on S.Id = AR.SellerId 
  left outer join Common.ThirdParty TPS with (nolock) on TPS.Id = S.ThirdPartyId 
  left outer join Billing.Invoice i with (nolock) on i.Id = AR.InvoiceId 
  left outer join [Contract].[CareGroup] cg with (nolock) on cg.Id = i.CareGroupId
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = AR.AccountWithoutRadicateId 
  left outer join 
  (select RIC.ConfirmDate, RIC.RadicatedConsecutive, RIC.RadicatedDate, RID.InvoiceNumber, RIC.CreationUser from Portfolio.RadicateInvoiceD RID with (nolock)
  inner Join Portfolio.RadicateInvoiceC RIC with (nolock) on RIC.Id = RID.RadicateInvoiceCId and ISNULL(RID.State,0) = 2 and RIC.State = 2) as RADICATE on RADICATE.InvoiceNumber = ar.InvoiceNumber
  where AR.Balance <> 0 AND AR.Status = 2  and ar.AccountReceivableType in (1,2)