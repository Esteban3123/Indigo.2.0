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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada para el reporte de movimientos de tesorería que combina dos tipos de operaciones financieras: consignaciones bancarias y traslados de fondos (comprobantes de egreso con clase 3). Integra información de cuentas bancarias de la entidad, el catálogo de bancos y las cajas registradoras, permitiendo identificar si un traslado proviene de una cuenta bancaria o de una caja. Cada fila incluye el tipo de operación, código del documento, fecha, descripción, estado, valor y el banco o caja agrupador, facilitando el seguimiento y conciliación de entradas y movimientos de dinero en el módulo de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportConsignmentTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportConsignmentTransfer';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un único listado reportable las consignaciones bancarias y los traslados de tesorería, con su agrupador por banco o caja, para alimentar reportes operativos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las consignaciones deben tener cuenta bancaria de entidad (EntityBankAccountId) y banco asociados para resolver el agrupador.; Los traslados se identifican por VoucherTransaction.VoucherClass = 3.; Un traslado debe tener IdCashRegister o IdEntityBankAccount para resolver el agrupador (banco o caja).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila del reporte tiene Type ∈ {''Consignación'',''Traslado''}.; Las consignaciones siempre se reportan con datos de banco (CodeBank/NameBank) y CodeCash vacío.; Solo los traslados (VoucherClass=3) pueden estar asociados a una caja registradora; las consignaciones nunca lo están.; El campo Row es una numeración secuencial por fecha de documento ascendente sobre el conjunto unificado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consignación bancaria; Traslado de tesorería; Cuenta bancaria de entidad; Banco; Caja registradora; Comprobante (Voucher); Clase de comprobante', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportConsignmentTransfer: Devuelve filas etiquetadas como ''Consignación'' desde Treasury.Consignment y filas etiquetadas como ''Traslado'' desde Treasury.VoucherTransaction filtradas por VoucherClass=3, numeradas con ROW_NUMBER() ordenado por DocumentDate ASC.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VoucherTransaction.VoucherClass = 3 → La transacción se incluye en el reporte como tipo ''Traslado''. else Se excluye del reporte.; si En un traslado, VT.IdCashRegister IS NULL → El agrupador se construye con número de cuenta bancaria + nombre del banco (EBA.Number + '' - '' + B.Name) y se reportan CodeBank/NameBank, dejando CodeCash vacío. else El agrupador se construye con código y nombre de la caja (CR.Code + '' - '' + CR.Name) y se reporta CodeCash, dejando CodeBank/NameBank vacíos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.Consignment; Treasury.EntityBankAccounts; Payroll.Bank; Treasury.VoucherTransaction; Treasury.CashRegisters', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportConsignmentTransfer';
GO
