
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