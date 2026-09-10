CREATE VIEW [Payments].[ViewAccountPayableFactoring] 
as  
	SELECT 
	ap.Id,
	ap.Code,
	ap.BillNumber,
	ap.BillDate,
	ap.ExpirationDate,
	ap.Balance,
	ap.Term,
	Concat(s.Code,' - ',s.Name) as SupplierCodeName,
	m.Number as NumberAccount,
	Concat(dl.Code,' - ',dl.Name) as DistributionLineName,
	ISNULL(ap.Coments,' ') Coments
	FROM Payments.AccountPayable ap
	JOIN Common.Supplier s WITH (NOLOCK) ON s.Id = ap.IdSupplier
	JOIN GeneralLedger.MainAccounts m  WITH (NOLOCK) ON M.Id = AP.IdAccount
	JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON sdl.Id = ap.IdSuppliersDistributionLines
	JOIN Common.DistributionLines dl WITH (NOLOCK) ON dl.Id = sdl.IdDistributionLine	
	WHERE ap.Value = ap.Balance AND ap.Term > 0 and ap.Balance > 0 

	--ap.Id NOT IN (
	--SELECT fdd.IdAccountPayable 
	--	FROM Payments.FactoringDocument  FD
	--JOIN Payments.FactoringDocumentDetail fdd on FDD.IdFactoringDocument = FD.Id
	--WHERE fd.Status = 2 )  AND ap.Term > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas por pagar disponibles para operaciones de factoring: muestra las facturas de proveedores que aún no han tenido ningún pago parcial (saldo igual al valor original), con plazo de pago vigente y saldo pendiente mayor a cero. Integra información del proveedor (código y nombre), la cuenta contable mayor asociada y la línea de distribución presupuestal, filtrando únicamente las obligaciones elegibles para ser incluidas en un proceso de factoring o cesión de cartera. Es utilizada para identificar qué documentos de cuentas por pagar pueden ser negociados o cedidos a través de un esquema de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountPayableFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountPayableFactoring';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las cuentas por pagar elegibles para factoring: aquellas sin pagos parciales, con plazo definido y saldo pendiente, mostrando datos del proveedor, cuenta contable y línea de distribución.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por pagar deben estar relacionadas con un proveedor activo en Common.Supplier.; Las cuentas por pagar deben tener una cuenta contable principal asociada en GeneralLedger.MainAccounts.; Debe existir una línea de distribución del proveedor (SuppliersDistributionLines) y su línea de distribución base (DistributionLines).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone cuentas por pagar cuyo valor original es igual al saldo actual (no han tenido abonos/pagos parciales).; Solo expone cuentas por pagar con plazo (Term) mayor a cero.; Solo expone cuentas por pagar con saldo positivo (Balance > 0).; Si el campo de comentarios es nulo, se devuelve un espacio en blanco en su lugar.; El nombre del proveedor se presenta concatenado como ''Código - Nombre''.; El nombre de la línea de distribución se presenta concatenado como ''Código - Nombre''.; Cada cuenta por pagar debe estar vinculada a un proveedor, una cuenta contable principal y una línea de distribución del proveedor existentes (JOIN interno).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Proveedor; Factoring; Línea de distribución; Cuenta contable; Plazo (Term); Saldo; Factura', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.AccountPayable: Cuando ap.Value = ap.Balance AND ap.Term > 0 AND ap.Balance > 0, se retorna la cuenta por pagar como candidata a factoring.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; GeneralLedger.MainAccounts; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableFactoring';
GO
