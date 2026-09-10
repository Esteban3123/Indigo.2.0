
CREATE VIEW [Payments].[Onco_CuentasxPagar_Todas]
AS
     SELECT e.Code AS Identificación, 
            t.Nit, 
            t.DigitVerification AS [Digito Verificacion], 
            t.Name AS [Nombre Tercero], 
            tp.Code,
            CASE tp.Parentid
                WHEN '1'
                THEN 'GASTOS DE PERSONAL'
                WHEN '24'
                THEN 'GASTOS DE HONORARIOS'
                WHEN '32'
                THEN 'GASTOS DE IMPUESTOS'
                WHEN '37'
                THEN 'GASTOS DE ARRENDAMIENTOS'
                WHEN '43'
                THEN 'GASTOS SEGUROS'
                WHEN '48'
                THEN 'OTROS SERVICIOS'
                WHEN '65'
                THEN 'GASTOS LEGALES'
                WHEN '71'
                THEN 'MANTENIMIENTO Y REPARACIONES'
                WHEN '79'
                THEN 'GASTOS DE VIAJE'
                WHEN '90'
                THEN 'DIVERSOS'
                WHEN '121'
                THEN 'PERSONAL'
                WHEN '101'
                THEN 'COSTOS'
            END AS [Descripción Tipo Proveedor], 
            tp.Name AS [Tipo Proveedor], 
            p.BillNumber AS Factura, 
            p.BillDate AS [Fecha factura], 
            p.ExpirationDate AS [Fecha vencimiento], 
            p.InvoiceValue AS [Valor factura-De Radicación], 
            p.Balance AS Saldo,
            CASE p.EntityName
                WHEN 'AccountPayable'
                THEN 'Cuentas X Pagar'
                WHEN 'InitialBalance'
                THEN 'Saldo Inicial'
            END AS Módulo, 
            c.Number AS [Cuenta contable], 
            c.Name AS [Descripción Cuenta Contable], 
            p.Code AS [Consecutivo C x P],
            CASE p.IdOperatingUnit
                WHEN '1'
                THEN 'PEREIRA'
                WHEN '5'
                THEN 'ARMENIA'
                WHEN '6'
                THEN 'MANIZALES'
                WHEN '7'
                THEN 'CARTAGO'
            END AS Sede, 
            cb.Number AS [Nro Cuenta],
            CASE cb.Type
                WHEN '1'
                THEN 'Ahorro'
                WHEN '2'
                THEN 'Corriente'
            END AS [Tipo Cuenta], 
            b.Code AS [Código Banco], 
            b.Name AS Banco, 
            p.DocumentDate AS [Fecha documento], 
            p.Coments AS Observaciones,
            CASE p.STATUS
                WHEN '1'
                THEN 'Sin Confimar'
                WHEN '2'
                THEN 'Confirmado'
                WHEN '3'
                THEN 'Anulado'
            END AS Estado, 
            us.NOMUSUARI AS Usuario, 
            p.CreationDate AS [Fecha creación]   
     ---,e.id as Spid, t.id as tpid, p.IdSuppliersDistributionLines as LDID, p.id as paymid, ps.id as paysid --m.Email AS [E-mail]  
     FROM Payments.AccountPayable AS p
          LEFT OUTER JOIN Common.Supplier AS e ON e.Id = p.IdSupplier
          INNER JOIN Common.ThirdParty AS t ON t.Id = p.IdThirdParty
          INNER JOIN GeneralLedger.MainAccounts AS c ON c.Id = p.IdAccount
          LEFT OUTER JOIN Common.OperatingUnit AS uo ON uo.Id = p.IdOperatingUnit
          LEFT OUTER JOIN Common.SupplierBankAccount AS cb ON cb.SupplierId = e.Id
                                                              AND cb.PaymentDefault = '1'
          LEFT OUTER JOIN Payroll.Bank AS b ON b.Id = cb.BankId
          LEFT OUTER JOIN dbo.SEGusuaru AS us ON us.CODUSUARI = p.CreationUser
          LEFT OUTER JOIN Common.SupplierType AS tp ON tp.Id = p.SupplierTypeId
          LEFT OUTER JOIN Common.Person AS dp ON dp.Id = t.PersonId
          INNER JOIN Payments.AccountPayableShares AS ps ON ps.IdAccountPayable = p.Id  
     --LEFT OUTER JOIN  
     --Common.Email AS m ON m.IdPerson = t.PersonId  
     WHERE(p.STATUS = '2')
          AND p.Balance <> '0';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las cuentas por pagar confirmadas con saldo pendiente (saldo distinto de cero y estado confirmado), combinando información de proveedores, terceros, cuentas contables, sedes, cuentas bancarias y usuarios. Integra datos de facturación (número de factura, fecha, valor, vencimiento), clasificación del proveedor por tipo de gasto (personal, honorarios, arrendamientos, costos, entre otros), datos bancarios para pago (banco, tipo y número de cuenta), y la sede donde se originó la obligación (Pereira, Armenia, Manizales, Cartago). Está diseñada para reportería de tesorería y cuentas por pagar del módulo Onco, permitiendo conocer qué se debe, a quién se le debe, cuánto falta por pagar y cómo se puede realizar el pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'Onco_CuentasxPagar_Todas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'Onco_CuentasxPagar_Todas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que lista las cuentas por pagar confirmadas con saldo pendiente, enriquecidas con datos del proveedor, tercero, cuenta contable, sede, cuenta bancaria, banco, tipo de proveedor y usuario creador.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en cuentas por pagar con estado confirmado y saldo distinto de cero; Cada cuenta por pagar debe tener tercero y cuenta contable asociados (INNER JOIN); Cada cuenta por pagar debe tener al menos un registro de cuotas/distribución asociado (INNER JOIN con AccountPayableShares)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas por pagar en estado Confirmado (STATUS=2); Solo se exponen cuentas por pagar con saldo pendiente (Balance<>0); La cuenta bancaria del proveedor mostrada es siempre la marcada como predeterminada de pago (PaymentDefault=1); El tercero y la cuenta contable son obligatorios para que el registro aparezca en la vista; El registro debe tener cuotas asociadas en AccountPayableShares para ser visible', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Proveedor; Tercero; Factura; Saldo pendiente; Cuenta contable; Tipo de proveedor; Cuenta bancaria del proveedor; Banco; Sede / Unidad operativa; Estado de documento (Confirmado, Sin Confirmar, Anulado); Saldo inicial; Fecha de vencimiento; NIT y dígito de verificación', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.AccountPayable: Solo retorna filas cuando STATUS=2 (Confirmado) y Balance<>0 (saldo pendiente)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SupplierType.Parentid en {1,24,32,37,43,48,65,71,79,90,121,101} → Clasifica el tipo de proveedor en categorías de gasto (Personal, Honorarios, Impuestos, Arrendamientos, Seguros, Otros Servicios, Legales, Mantenimiento, Viaje, Diversos, Personal, Costos) else NULL; si AccountPayable.EntityName = ''AccountPayable'' o ''InitialBalance'' → Etiqueta el módulo como ''Cuentas X Pagar'' o ''Saldo Inicial'' respectivamente else NULL; si IdOperatingUnit en {1,5,6,7} → Mapea a sede física: PEREIRA, ARMENIA, MANIZALES o CARTAGO else NULL; si SupplierBankAccount.Type = 1 o 2 → Clasifica la cuenta bancaria como Ahorro o Corriente else NULL; si AccountPayable.STATUS en {1,2,3} → Traduce estado a ''Sin Confimar'', ''Confirmado'' o ''Anulado'' else NULL', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.ThirdParty; GeneralLedger.MainAccounts; Common.OperatingUnit; Common.SupplierBankAccount; Payroll.Bank; dbo.SEGusuaru; Common.SupplierType; Common.Person; Payments.AccountPayableShares', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'Onco_CuentasxPagar_Todas';
GO
