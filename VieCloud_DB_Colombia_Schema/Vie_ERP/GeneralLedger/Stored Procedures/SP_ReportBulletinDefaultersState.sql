

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de Boletín de Deudores Morosos del Estado, que identifica todos los terceros (empresas o personas naturales) con cuentas por cobrar vencidas cuyo saldo pendiente supera un umbral definido (ya sea un valor ingresado manualmente o el Salario Mínimo Legal Vigente tomado de la configuración de la empresa). Filtra las cuentas por cobrar en estado pendiente de cobro (estado 2) que hayan superado su plazo de vencimiento más un período de gracia adicional configurable (en días, semanas, meses o años), excluyendo documentos ya radicados y confirmados ante entidades pagadoras. Combina información de cuentas por cobrar, terceros, personas naturales y radicación de facturas para presentar por cada deudor su NIT, nombre, tipo de persona, tipo de identificación y saldo en mora, permitiendo filtrar por rango de NIT del tercero y fecha de corte.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBulletinDefaultersState';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBulletinDefaultersState';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el listado de deudores morosos del Estado a una fecha de corte, filtrando cuentas por cobrar vencidas con saldo superior a un umbral (valor explícito o SMLV) y dentro de un rango de terceros.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si @ReportValue = 0 y @value es NULL, debe existir al menos un registro en GeneralLedger.CompanySettings con SMLV configurado para usarlo como umbral.; Las cuentas por cobrar deben tener su tercero, persona y cuenta contable asociados (joins internos).; Para cuentas por cobrar tipo 2 debe existir radicación con ConfirmDate; para otros tipos se usa la fecha de la cuenta por cobrar.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cuentas por cobrar con Status = 2 (activas/vigentes para cobro).; Se excluyen cuentas por cobrar cuya radicación tiene State = 4.; Solo se incluyen documentos cuya fecha de vencimiento (Term + fecha base) sea menor o igual a la fecha de corte.; El umbral de saldo se aplica de forma estricta (Balance > umbral, no incluye iguales).; El rango de NIT acepta nulos sustituyendo por ''0'' y ''999999999999999'' como cotas por defecto.; La fecha base de vencimiento depende del tipo de cuenta por cobrar (radicada vs. directa).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Deudor moroso del Estado; Cuenta por cobrar; Cartera; Radicación de facturas; SMLV (Salario Mínimo Legal Vigente); Tercero (persona natural/jurídica); Tipo de identificación; Fecha de corte; Plazo de vencimiento (Term); NIT', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas con concepto ''DEUDOR PRINCIPAL'' por cada cuenta por cobrar cuyo Balance > umbral, Status = 2, estado de radicación (RID.State) distinto de 4, vencida al corte y dentro del rango de NIT.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReportValue = 0 AND @value IS NULL → Toma el umbral de saldo desde GeneralLedger.CompanySettings.SMLV (primer registro). else Usa el @value recibido como umbral de saldo.; si AR.AccountReceivableType = 2 → La fecha base de vencimiento se calcula sobre RIC.ConfirmDate (fecha de confirmación de la radicación). else La fecha base de vencimiento se calcula sobre AR.AccountReceivableDate.; si @PeriodType = 1/2/3/4 → Suma @periodValue en días/semanas/meses/años respectivamente sobre la fecha de vencimiento, y exige que esa fecha proyectada sea <= fecha actual (Common.GETDATE()).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.AccountReceivable; Common.ThirdParty; Common.Person; GeneralLedger.MainAccounts; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBulletinDefaultersState';
-- GO
