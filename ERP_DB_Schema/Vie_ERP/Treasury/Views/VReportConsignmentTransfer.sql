CREATE VIEW [Treasury].[VReportConsignmentTransfer]
as
select ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) as Row ,* from (
	SELECT 'Consignación' as Type
		,C.Code as Code
		,C.DocumentDate as DocumentDate
		,C.Description as Detail
		,C.Status as Status
		,C.Value as Value
		,EBA.Number + ' - ' + b.Name as GroupByBankCash
		,b.Code as CodeBank
		,b.Name as NameBank
		,'' as CodeCash
  FROM  Treasury.Consignment C with (nolock)
  inner join Treasury.EntityBankAccounts EBA with (nolock) on EBA.Id = C.EntityBankAccountId 
  inner join Payroll.Bank B with (nolock) on B.Id = EBA.IdBank 
  UNION ALL
  SELECT 'Traslado' as Type
		,VT.Code as Code
		,VT.DocumentDate as DocumentDate
		,VT.Detail as Detail
		,VT.Status as Status
		,VT.Value as Value
		,IIF(VT.IdCashRegister is null, EBA.Number + ' - ' + b.Name, CR.Code + ' - ' + CR.Name) as GroupByBankCash
		,IIF(VT.IdCashRegister is null, b.Code, '')  as CodeBank
		,IIF(VT.IdCashRegister is null, b.Name, '')  as NameBank
		,IIF(VT.IdCashRegister is null, '', CR.Code)  as CodeCash
		FROM Treasury.VoucherTransaction VT with (nolock)
		left join Treasury.CashRegisters CR with (nolock) on CR.Id = VT.IdCashRegister 
		left join Treasury.EntityBankAccounts EBA with (nolock) on EBA.Id = VT.IdEntityBankAccount 
		left join Payroll.Bank B with (nolock) on B.Id = EBA.IdBank where vT.VoucherClass = 3) as Datos