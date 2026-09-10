CREATE PROCEDURE [dbo].[SPU_RecibosCajaPagares2] @fechaInicial AS DATETIME, 
                                                @fechaFinal AS   DATETIME
AS
     WITH ING
          AS (SELECT dbo.ADINGRESO.NUMINGRES
              FROM dbo.ADINGRESO
              WHERE(ADINGRESO.IFECHAING BETWEEN CAST(@fechaInicial AS DATETIME) AND CAST(@fechaFinal AS DATETIME))
                   AND (ADINGRESO.IESTADOIN = ' '
                        OR ADINGRESO.IESTADOIN = 'P'))
          SELECT pa.Code AS CodigoAnticipo, 
                 pa.DocumentDate AS FechaAnticipo, 
                 pa.CreationUser AS CodFacturador, 
                 Pe.Fullname AS NombreFacturador, 
                 pa.Value AS ValorAnticipo, 
                 pa.Balance AS SaldoAnticipo, 
                 cr.Code AS CodigoReciboCaja, 
                 pa.AdmissionNumber AS Ingreso, 
                 cr.CreationUser AS UsuarioReciboCaja, 
                 pa.Observations, 
                 Treasury.CashReceiptConcepts.Code AS CodConcepto, 
                 Treasury.CashReceiptConcepts.Name AS Concepto, 
                 GeneralLedger.MainAccounts.Number AS CuentaContable, 
                 Common.ThirdParty.Nit, 
                 Common.ThirdParty.Name AS Tercero, 
                 cr.Detail AS DetalleRecibo
          FROM Portfolio.PortfolioAdvance AS pa
               INNER JOIN Security.Person AS Pe
               INNER JOIN Security.[User] AS Us ON Pe.Id = Us.IdPerson ON pa.CreationUser = Us.UserCode
               INNER JOIN Treasury.CashReceiptDetails ON pa.CashReceiptDetailId = Treasury.CashReceiptDetails.Id
                                                         AND pa.CashReceiptDetailId = Treasury.CashReceiptDetails.Id
               INNER JOIN GeneralLedger.MainAccounts ON Treasury.CashReceiptDetails.IdMainAccount = GeneralLedger.MainAccounts.Id
                                                        AND Treasury.CashReceiptDetails.IdMainAccount = GeneralLedger.MainAccounts.Id
               INNER JOIN Treasury.CashReceiptConcepts ON Treasury.CashReceiptDetails.IdCashReceiptConcept = Treasury.CashReceiptConcepts.Id
                                                          AND Treasury.CashReceiptDetails.IdCashReceiptConcept = Treasury.CashReceiptConcepts.Id
               INNER JOIN Common.ThirdParty ON pa.ThirdPartyId = Common.ThirdParty.Id
                                               AND pa.ThirdPartyId = Common.ThirdParty.Id
                                               AND pa.ThirdPartyId = Common.ThirdParty.Id
               LEFT OUTER JOIN Treasury.CashReceipts AS cr ON cr.Id = pa.CashReceiptId
          WHERE(Treasury.CashReceiptConcepts.Code = '45'
                OR Treasury.CashReceiptConcepts.Code = '06')
               AND pa.AdmissionNumber IN
          (
              SELECT *
              FROM ING
          );
     RETURN;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera anticipos de cartera (pagos adelantados) asociados a recibos de caja con conceptos de recaudo ''45'' o ''06'', filtrando únicamente los ingresos/admisiones de pacientes activos o en proceso (''P'' o '' '') cuya fecha de ingreso se encuentre dentro del rango indicado. Consolida información del anticipo, el recibo de caja, la cuenta contable del plan PUC, el tercero pagador y el facturador responsable, orientado a reportes de caja y conciliación de pagares en un período determinado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los anticipos de cartera y sus recibos de caja asociados a ingresos de pacientes ocurridos en un rango de fechas, restringidos a conceptos de recaudo específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido para filtrar los ingresos.; Deben existir ingresos en ADINGRESO con estado vacío ('' '') o ''P'' dentro del rango.; Los anticipos deben estar enlazados a un detalle de recibo de caja, cuenta contable, concepto y tercero válidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan anticipos asociados a ingresos vigentes o pendientes (estado '' '' o ''P'').; Solo se reportan anticipos cuyo concepto de recibo de caja sea ''45'' o ''06''.; Cada anticipo retornado debe tener tercero, cuenta contable y detalle de recibo de caja asociados (INNER JOIN); el recibo de caja en sí puede no existir (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión de paciente; Anticipo de cartera; Recibo de caja; Concepto de recaudo; Cuenta contable; Tercero; Facturador; Saldo de anticipo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioAdvance: Devuelve anticipos cuyo AdmissionNumber corresponde a ingresos en el rango con IESTADOIN='' '' o ''P'', y cuyo concepto de recibo de caja sea código ''45'' o ''06''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ADINGRESO.IESTADOIN = '' '' OR IESTADOIN = ''P'' → Se incluye el ingreso como elegible para listar sus anticipos else Se excluye el ingreso del resultado; si CashReceiptConcepts.Code = ''45'' OR Code = ''06'' → Se incluye el anticipo en la salida else Se excluye el anticipo del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Portfolio.PortfolioAdvance; Security.Person; Security.User; Treasury.CashReceiptDetails; GeneralLedger.MainAccounts; Treasury.CashReceiptConcepts; Common.ThirdParty; Treasury.CashReceipts', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares2';
-- GO
