


-- =============================================
-- Author:		Juan Bermudez
-- Create date: 12/03/2016
-- Description:	Procedimiento para el reporte de Boletín Deudores Morosos del Estado
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportBulletinDefaultersState]
	@DateCourt as date,
	@ReportValue as bit,
	@value as decimal(18,0),
	@PeriodType as tinyint,
	@periodValue as integer,
	@thirdPartyStart as varchar(15),
	@thirdPartyEnd as varchar(15)

AS
BEGIN

	If @ReportValue = 0 And @value is null
	BEGIN
	set @Value = (Select Top 1 SMLV From GeneralLedger.CompanySettings)
	END

	SELECT 'DEUDOR PRINCIPAL' AS Concept
			,AR.InvoiceNumber as DocumentCode
			,TPAR.Nit as ThirdPartyNit
			,TPAR.Name as ThirdPartyName
			,CASE TPAR.PersonType when 1 then 'PERSONA NATURAL' when 2 then 'PERSONA JURIDICA' End as ThirdPartyType
			,TPAR.PersonType as ThirPartyTypeValue
			,CASE PAR.IdentificationType when 0 then 'CEDULA DE CIUDADANIA' when 1 then 'CEDULA DE EXTRANJERIA' when 2 then 'TARJETA DE IDENTIDAD' when 3 then 'REGISTRO CIVIL' when 4 then 'PASAPORTE' when 5 then 'ADULTO SIN IDENTIFICACION' when 6 then 'MENOR SIN IDENTIFICACION' when 7 then 'NIT' END as ThirdPartyTypeIdentification
			,PAR.IdentificationType as ThirdPartyTypeIdentificationValue
			,AR.Balance as Balance
			FROM  Portfolio.AccountReceivable AR with (nolock)
	  inner join Common.ThirdParty TPAR with (nolock) on TPAR.Id = AR.ThirdPartyId 
	  inner join Common.Person PAR with (nolock) on PAR.Id=TPAR.PersonId
	  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = AR.AccountWithoutRadicateId 
	  left outer join Portfolio.RadicateInvoiceD RID with (nolock) on RID.InvoiceNumber = AR.InvoiceNumber
	  left Outer Join Portfolio.RadicateInvoiceC RIC with (nolock) on RIC.Id = RID.RadicateInvoiceCId
	  where AR.Balance > @Value AND AR.Status = 2 AND ISNULL(RID.State,0) <> 4 
	  AND DATEADD(DAY,AR.Term,cast(IIF(AR.AccountReceivableType = 2,RIC.ConfirmDate, AR.AccountReceivableDate) as date)) <= @DateCourt
	  AND Case @PeriodType when 1 then DATEADD(DAY,@periodValue,DATEADD(DAY,AR.Term,cast(IIF(AR.AccountReceivableType = 2,RIC.ConfirmDate, AR.AccountReceivableDate) as date)))
						   when 2 then DATEADD(WEEK,@periodValue,DATEADD(DAY,AR.Term,cast(IIF(AR.AccountReceivableType = 2,RIC.ConfirmDate, AR.AccountReceivableDate) as date))) 
						   when 3 then DATEADD(MONTH,@periodValue,DATEADD(DAY,AR.Term,cast(IIF(AR.AccountReceivableType = 2,RIC.ConfirmDate, AR.AccountReceivableDate) as date)))
						   when 4 then DATEADD(YEAR,@periodValue,DATEADD(DAY,AR.Term,cast(IIF(AR.AccountReceivableType = 2,RIC.ConfirmDate, AR.AccountReceivableDate) as date))) End <= [Common].[GETDATE]()
	  AND TPAR.Nit >= ISNULL(@thirdPartyStart,'0') AND TPAR.Nit <= ISNULL(@thirdPartyEnd,'999999999999999')

END