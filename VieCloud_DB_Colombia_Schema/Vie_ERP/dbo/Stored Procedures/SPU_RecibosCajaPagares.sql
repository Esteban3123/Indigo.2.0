CREATE PROCEDURE [dbo].[SPU_RecibosCajaPagares]
AS
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
     WHERE Treasury.CashReceiptConcepts.Code = '045'
           OR Treasury.CashReceiptConcepts.Code = '06';
     SELECT *
     FROM Treasury.CashReceiptConcepts;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los anticipos y pagarés registrados en cartera, filtrando únicamente los recibos de caja asociados a los conceptos de cobro ''045'' y ''06'' (que corresponden a pagarés o compromisos de pago). Para cada anticipo muestra su código, fecha, valor, saldo disponible, número de ingreso del paciente, el recibo de caja vinculado, el tercero pagador (NIT y nombre), la cuenta contable afectada y el nombre del facturador que lo registró. Compone información de anticipos de cartera, recibos de caja, detalle contable, terceros y usuarios de seguridad para generar un reporte de pagarés y anticipos recibidos en tesorería. Adicionalmente retorna el catálogo completo de conceptos de recibo de caja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_RecibosCajaPagares';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_RecibosCajaPagares';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los anticipos de cartera asociados a recibos de caja cuyos conceptos correspondan a anticipos/pagarés (códigos ''045'' y ''06''), junto con el catálogo completo de conceptos de recibo de caja.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen anticipos en Portfolio.PortfolioAdvance vinculados a un detalle de recibo de caja (CashReceiptDetailId no nulo).; El usuario creador del anticipo existe en Security.User y tiene persona asociada en Security.Person.; El detalle del recibo de caja referencia una cuenta contable y un concepto de recibo válidos.; El anticipo tiene un tercero válido en Common.ThirdParty.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan anticipos asociados a conceptos de recibo de caja con código ''045'' o ''06''.; Cada anticipo reportado tiene obligatoriamente detalle de recibo de caja, cuenta contable, concepto y tercero (INNER JOIN).; El recibo de caja (CashReceipts) puede ser nulo (LEFT JOIN); el anticipo aparece aunque no tenga recibo materializado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipo de cartera; Recibo de caja; Concepto de recibo de caja; Pagaré; Cuenta contable (PUC); Tercero; Facturador; Ingreso (admisión)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve un primer resultset con anticipos cuyo concepto de recibo de caja sea ''045'' u ''06'', incluyendo datos del facturador, tercero, cuenta contable y recibo asociado.; [RETURN_RESULT] ResultSet: Devuelve un segundo resultset con el catálogo completo de Treasury.CashReceiptConcepts.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Treasury.CashReceiptConcepts.Code = ''045'' OR Code = ''06'' → Incluye el anticipo en el resultado (filtra solo conceptos de anticipo/pagaré). else Excluye el registro del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; Security.Person; Security.User; Treasury.CashReceiptDetails; GeneralLedger.MainAccounts; Treasury.CashReceiptConcepts; Common.ThirdParty; Treasury.CashReceipts', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_RecibosCajaPagares';
-- GO
