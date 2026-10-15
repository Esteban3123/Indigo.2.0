


-- =============================================
-- Author:		Juan Bermudez
-- Create date: 12/04/2016
-- Description:	Procedimiento para el reporte auxiliar contable
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportAccountingAssistant]

	@dateStart date,
	@dateEnd date,
	@status int,
	@bookId as integer,
	@CuentaInicial varchar(50),
	@CuentaFinal varchar(50),
	@TerceroInicial varchar(50),
	@TerceroFinal varchar(50),
	@CentroInicial varchar(50),
	@CentroFinal varchar(50)

AS
BEGIN

	-- Creamos la tabla temporal que devolveremos con los datos
	declare @tableExecution table(id int identity(1,1) primary key,codeJournalVoucher bigint, voucherDate datetime, codeNameJournalVoucherType varchar(100),statusJournalVoucher varchar(12), entityName varchar(max),entityCode varchar(20),debitValue decimal(18,0), creditValue decimal(18,0), detailJournalVoucherDetail varchar(max),idMainAccount int,numberMainAccount varchar(20),nameMainAccount varchar(max),nature varchar(10), thirdPartyId int, thirdPartyNit varchar(20), thirdPartyName varchar(max),costCenterId int, costCenterCode varchar(20),costCenterName varchar(max), valueDebitInitial decimal(18,0), valueCreditInitial decimal(18,0),previousBalance decimal(18,0), UNIQUE NONCLUSTERED (numberMainAccount, id))

	INSERT INTO @tableExecution (codeJournalVoucher, voucherDate, codeNameJournalVoucherType, statusJournalVoucher, entityName, entityCode, debitValue, creditValue, detailJournalVoucherDetail, idMainAccount, numberMainAccount, nameMainAccount, nature, thirdPartyId, thirdPartyNit, thirdPartyName, costCenterId, costCenterCode, costCenterName)
	Select jv.Consecutive as codeJournalVoucher,jv.VoucherDate as voucherDate, jvt.code + ' - ' + jvt.Name as codeNameJournalVoucherType, 
	case jv.[status] when 1 then 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end as statusJournalVoucher,
	case jv.EntityName when 'JournalVouchers' then 'Contabilidad' when 'AccountPayable' then 'Cuenta Por Pagar' when 'AccountControl' then 'Control de Cuentas'
	when 'AccountReceivableDocument' then 'Documento Cuenta por Cobrar' when 'CashReceipts' then 'Recibo de Caja' when 'Consignment' then 'Consignaciones'
	when 'CrossingAccount' then 'Cruce de Cuentas' when 'DeferredCausation' then 'Causación Diferida' when 'DocumentInvoiceProductSales' then 'Venta De Productos' 
	when 'EntranceVoucher' then 'Comprobante de Entrada' when 'GlosaObjectionsReceptionD' then 'Recepción De Objeciones' when 'InitialBalance' then 'Saldo Inicial'
	when 'InventoryAdjustment' then 'Ajuste de Inventario' when 'Invoice' then 'Factura' when 'InvoiceEntityCapitated' then 'Factura a Entidad Capitada'
	when 'JournalVoucher' then 'Contabilidad' when 'LoanMerchandise' then 'Solicitud de Préstamo de Mercancía' when 'LoanMerchandiseDevolution' then 'Devolución de Préstamo de Mercancía'
	when 'MedicalFeesLiquidation' then 'Liquidación De Honorarios Medicos' when 'PaymentNotes' then 'Nota de Pago' when 'PaymentTransfer' then 'Cruce de Anticipos Vs CxP'
	when 'PharmaceuticalDispensing' then 'Dispensación Farmaceutica' when 'PharmaceuticalDispensingDevolution' then 'Devolución de Suministro' when 'PortfolioNote' then 'Nota de Cartera'
	when 'PortfolioReclassification' then 'Comprobante de Reclasificación de Cartera' when 'PortfolioTransfer' then 'Cruce de Anticipo Vs CxC' when 'RadicateInvoiceC' then 'Radicación de Cuentas'
	when 'StrTitleDocumentFolioLiq' then 'Liquidación' when 'TransferOrder' then 'Orden de Traslado' when 'TransferOrderDevolution' then 'Devolucion Orden de Traslado'
	when 'TreasuryNote' then 'Nota de Tesorería' when 'VoucherTransaction' then 'Comprobante de Egreso' End as entityName, jv.EntityCode, jvd.DebitValue, jvd.CreditValue, jvd.Detail,
	ma.Id as idMainAccount, ma.Number as numberMainAccount, ma.Name as nameMainAccount,case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza, tp.Id as idThirdParty, tp.Nit as nitThirdParty, tp.Name as nameThirdParty,
	cc.Id as idCostCenter, cc.Code as codeCostCenter, cc.Name as nameCostCenter
 	from GeneralLedger.JournalVoucherDetails jvd inner join GeneralLedger.JournalVouchers jv on jv.Id = jvd.IdAccounting 
	inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount left join Common.ThirdParty tp on tp.Id = jvd.Id
	left join Payroll.CostCenter cc on cc.Id = jvd.IdCostCenter inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
	Where cast(jv.VoucherDate as date) >= @dateStart And cast(jv.VoucherDate as date) <= @dateEnd And ma.Number >= ISNULL(@CuentaInicial, '0')
	And ma.Number <= ISNULL(@CuentaFinal, 'z') And ISNULL(tp.Nit,'0') >= ISNULL(@TerceroInicial,'0') And ISNULL(tp.Nit,'0') <= ISNULL(@TerceroFinal,'z')
	And ISNULL(cc.Code,'0') >= ISNULL(@CentroInicial,'0') And ISNULL(cc.Code,'0') <= ISNULL(@CentroFinal,'z') And ma.LegalBookId = @bookId  



	declare @ano as integer
	declare @monthPreviousBalance as integer
	set @ano = YEAR(@dateStart)
	set @monthPreviousBalance = MONTH(DATEADD(MONTH,-1,@dateStart))

	--INSERT INTO @tableExecution (mainAccountId,mainAccountCode,mainAccountName,nature, thirdPartyId, thirdPartyNit, thirdPartyName,costCenterId, costCenterCode,costCenterName, valueDebitInitial, valueCreditInitial,previousBalance)
	--SELECT Inicial.IdMainAccount as IdCuentaContable,Inicial.Number as CuentaContable,Inicial.Name as CuentaContableName, Inicial.Naturaleza as Naturaleza,
	--ISNULL (Inicial.IdThirdParty,0) as idTercero ,tp.Nit as Nit , tp.Name as NombreTercero ,ISNULL (Inicial.IdCostCenter , 0) as IdCC, cc.Code as CodigoCC , cc.Name as NombreCC,   
	--isnull(Inicial.Debit,0) as DebitoIncial, isnull(Inicial.Credit,0) as CreditoInicial, isnull(Inicial.Saldo,0) as SaldoInicial
	
	--FROM 
	--(select ma.Id as IdMainAccount, ma.Number,ma.Name, glb.IdThirdParty, glb.IdCostCenter, SUM(glb.DebitValue) AS Debit, SUM(glb.CreditValue) as Credit,  
	--case ma.nature when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) else    SUM(glb.CreditValue) -SUM(glb.DebitValue) end as Saldo, 
	--case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza
	--from 
	--(select IdMainAccount,  IdThirdParty, IdCostCenter, DebitValue, CreditValue from GeneralLedger.GeneralLedgerBalance where ([Year] = (@ano - 1) and [Month] = 14) or ([Year] = @ano and [Month] < @monthPreviousBalance)) as glb inner join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
	--inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel left join GeneralLedger.MainAccounts map on map.Id = ma.IdParent 
	--where ma.LegalBookId = @bookId
	--group by ma.Id, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter, ma.Nature) as Inicial
	--LEFT JOIN
	--Common.ThirdParty as tp on tp.Id = ISNULL(Inicial.IdThirdParty,0)
	--LEFT JOIN
	--Payroll.CostCenter as cc on cc.Id = ISNULL(Inicial.IdCostCenter,0)

	--WHERE Inicial.Number  >= ISNULL(@CuentaInicial,'0') and Inicial.Number <= ISNULL(@CuentaFinal, 'z') and isnull(tp.Nit,'0') >= ISNULL(@TerceroInicial, '0') and isnull(tp.Nit,'0') <= ISNULL(@TerceroFinal,'z') and isnull(cc.Code,'0') >= ISNULL(@CentroInicial,'0') and isnull(cc.Code,'0') <= ISNULL(@CentroFinal,'z')
	--order by CuentaContable
	
	Select * from @tableExecution order by numberMainAccount,thirdPartyNit,costCenterCode

END