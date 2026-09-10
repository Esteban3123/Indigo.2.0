
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-27
-- Description:	Obtiene los anticipos para el informe de cartera por edades
-- =============================================
CREATE FUNCTION [Portfolio].[GetPortfolioAdvanceByAge]
(
	@ClosingDate DATE
)
RETURNS TABLE
AS
RETURN
SELECT 
    pa.Id,
    cr.OperatingUnitId,
    pa.Code,
    pa.DocumentDate,
    tp.Id AS ThirdPartyId,
    tp.Nit AS ThirdPartyNit,
    tp.Name AS ThirdPartyName,
    tp.PersonType,
    p.IdentificationType,
    pa.MainAccountId,
    IIF(pnd.Id IS NULL, pa.Value, 0) AS DocumentValue,
    ISNULL(pn.DebitValue, 0) + ISNULL(vt.DebitValue, 0) AS DebitValue,
    ISNULL(pn.CreditValue, 0) + ISNULL(vt.CreditValue, 0) AS CreditValue,
    ISNULL(pt.TransferValue, 0) AS TransferValue,
    IIF(pnd.Id IS NULL, pa.Value, 0)
    - ISNULL(pn.DebitValue, 0) + ISNULL(pn.CreditValue, 0) --Valor Notas
	- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
	- ISNULL(vt.DebitValue, 0) AS Balance,
    pa.Balance AS CurrentBalance,
    c.Id AS CurrencyId,
    c.Name AS CurrencyName
FROM Portfolio.PortfolioAdvance pa WITH (NOLOCK)
JOIN Common.ThirdParty tp WITH (NOLOCK) ON pa.ThirdPartyId = tp.Id
JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
LEFT JOIN Treasury.CashReceipts cr WITH (NOLOCK) ON pa.CashReceiptId = cr.Id
LEFT JOIN Portfolio.PortfolioNoteDistribution pnd WITH (NOLOCK) ON pa.Id = pnd.PortfolioAdvanceId
LEFT JOIN (
    SELECT pt.PortfolioAdvanceId,
           SUM(ISNULL(ptd.Value, 0) + ISNULL(ptoc.CreditValue, 0) - ISNULL(ptoc.DebitValue, 0)) AS TransferValue
    FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
    LEFT JOIN (
        SELECT ptd.PortfolioTrasferId, SUM(ptd.Value) AS Value
        FROM Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK)
        GROUP BY ptd.PortfolioTrasferId
    ) ptd ON pt.Id = ptd.PortfolioTrasferId
    LEFT JOIN (
        SELECT ptoc.PortfolioTransferId,
               SUM(IIF(ptoc.Nature = 1, ptoc.Value, 0)) AS DebitValue,
               SUM(IIF(ptoc.Nature = 2, ptoc.Value, 0)) AS CreditValue
        FROM Portfolio.PortfolioTransferOtherConcept ptoc WITH (NOLOCK)
        GROUP BY ptoc.PortfolioTransferId
    ) ptoc ON pt.Id = ptoc.PortfolioTransferId
    WHERE pt.Status IN (2, 4)
      AND CAST(pt.DocumentDate AS DATE) <= CAST(@ClosingDate AS DATE)
      AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > CAST(@ClosingDate AS DATE)
    GROUP BY pt.PortfolioAdvanceId
) pt ON pa.Id = pt.PortfolioAdvanceId
LEFT JOIN (
    SELECT pn.PortfolioAdvanceId,
           SUM(pn.DebitValue) AS DebitValue,
           SUM(pn.CreditValue) AS CreditValue
    FROM (
        SELECT pnara.PortfolioAdvanceId,
               IIF(pn.Nature = 1, pnara.AdjusmentValue, 0) AS DebitValue,
               IIF(pn.Nature = 2, pnara.AdjusmentValue, 0) AS CreditValue
        FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
        JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
        WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= CAST(@ClosingDate AS DATE)
        UNION ALL
        SELECT pn.PortfolioAdvanceId,
               pnd.Value AS DebitValue,
               0 AS CreditValue
        FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
        JOIN Portfolio.PortfolioNoteDistribution pnd WITH (NOLOCK) ON pn.Id = pnd.PortfolioNoteId
        WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= CAST(@ClosingDate AS DATE)
        UNION ALL
        SELECT pnd.PortfolioAdvanceId,
               0 AS DebitValue,
               pnd.Value AS CreditValue
        FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
        JOIN Portfolio.PortfolioNoteDistribution pnd WITH (NOLOCK) ON pn.Id = pnd.PortfolioNoteId
        WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= CAST(@ClosingDate AS DATE)
    ) pn
    GROUP BY pn.PortfolioAdvanceId
) pn ON pa.Id = pn.PortfolioAdvanceId
LEFT JOIN (
    SELECT vta.PortfolioAdvanceId,
           SUM(IIF(vtd.Nature = 1, vta.Value, 0)) AS DebitValue,
           SUM(IIF(vtd.Nature = 2, vta.Value, 0)) AS CreditValue
    FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
    JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
    JOIN Treasury.VoucherTransactionAdvance vta WITH (NOLOCK) ON vtd.Id = vta.IdVoucherTransactionD
    WHERE vt.Status IN (2, 4)
      AND CAST(vt.DocumentDate AS DATE) <= CAST(@ClosingDate AS DATE)
      AND CAST(ISNULL(vt.ReversedDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > CAST(@ClosingDate AS DATE)
    GROUP BY vta.PortfolioAdvanceId
) vt ON pa.Id = vt.PortfolioAdvanceId
LEFT JOIN Common.Currency c ON c.Id = pa.CurrencyId
WHERE pa.Status = 2
  AND CAST(pa.DocumentDate AS DATE) <= CAST(@ClosingDate AS DATE)
  AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > CAST(@ClosingDate AS DATE)
  AND pa.Balance <> 0;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el estado de los anticipos de cartera a una fecha de corte determinada, para su uso en el informe de cartera por edades (aging de anticipos). Toma cada anticipo activo (no aplicado totalmente) registrado hasta la fecha de cierre y calcula su saldo real descontando notas débito/crédito, cruce de anticipos mediante traslados de cartera y comprobantes de tesorería vigentes a esa fecha. Compone información del tercero (empresa o persona: NIT, nombre, tipo de persona e identificación), la cuenta contable principal, la unidad operativa del recibo de caja, los movimientos de ajuste por notas de cartera, los traslados aprobados y los comprobantes de tesorería, entregando por cada anticipo su valor original, débitos, créditos, valor trasladado, saldo calculado y saldo actual en la moneda correspondiente. Se usa para reportes de envejecimiento de anticipos pendientes de aplicar, permitiendo identificar qué terceros tienen dinero adelantado sin cruzar contra facturas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetPortfolioAdvanceByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetPortfolioAdvanceByAge';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula, a una fecha de corte, el saldo vigente de cada anticipo de cartera (descontando notas, cruces/traslados y aplicaciones de tesorería) para alimentar el informe de cartera por edades.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar una fecha de corte (@ClosingDate) válida.; El anticipo debe estar activo (Status = 2) y con saldo distinto de cero.; La fecha del documento del anticipo debe ser anterior o igual a la fecha de corte.; El recibo de caja asociado no debe estar reversado a la fecha de corte (ReversedDate > @ClosingDate o NULL).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran anticipos con Status = 2 (activos) y saldo (Balance) distinto de cero.; Solo se incluyen movimientos cuya fecha de documento sea ≤ fecha de corte y que no estén reversados antes/en esa fecha (lógica de corte histórico).; Las notas de cartera y los traslados solo se consideran si están en estado válido (Status=2 para notas; Status IN (2,4) para traslados y comprobantes de tesorería).; El cálculo del Balance sigue la fórmula: DocumentValue - DebitValueNotas + CreditValueNotas - TransferValue - DebitValueTesorería.; Se usa NOLOCK en todas las lecturas, asumiendo tolerancia a lecturas sucias para el reporte.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipo de cartera; Cartera por edades; Recibo de caja; Nota de cartera (débito/crédito); Traslado/cruce de anticipos; Comprobante de tesorería; Tercero; Cuenta contable principal; Moneda; Reversión de documentos; Fecha de corte', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve una fila por anticipo vigente con: valor del documento (0 si tiene distribución de nota asociada vía PortfolioNoteDistribution), totales de débito/crédito por notas y por aplicaciones de tesorería, valor de cruces de anticipo y saldo calculado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pa.Status = 2 AND pa.DocumentDate <= @ClosingDate AND (cr.ReversedDate IS NULL OR cr.ReversedDate > @ClosingDate) AND pa.Balance <> 0 → Se incluye el anticipo en el resultado.; si Existe registro en Portfolio.PortfolioNoteDistribution para el anticipo (pnd.Id IS NOT NULL) → DocumentValue se reporta como 0 (el valor del anticipo se considera ya distribuido por la nota). else DocumentValue se reporta como pa.Value.; si PortfolioTransfer.Status IN (2,4) AND DocumentDate <= @ClosingDate AND (RecersalDate IS NULL OR RecersalDate > @ClosingDate) → El traslado de cartera se incluye sumando su valor (detalle + otros conceptos crédito - otros conceptos débito) como TransferValue, restando del saldo.; si PortfolioNote.Status = 2 AND NoteDate <= @ClosingDate → La nota de cartera afecta el cálculo: si Nature=1 suma a DebitValue (resta saldo), si Nature=2 suma a CreditValue (suma saldo); además las distribuciones de nota se acumulan tanto en débito como en crédito.; si VoucherTransaction.Status IN (2,4) AND DocumentDate <= @ClosingDate AND (ReversedDate IS NULL OR ReversedDate > @ClosingDate) → La transacción de tesorería se aplica al anticipo: Nature=1 suma a DebitValue, Nature=2 suma a CreditValue.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; Common.ThirdParty; Common.Person; GeneralLedger.MainAccounts; Treasury.CashReceipts; Portfolio.PortfolioNoteDistribution; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransferOtherConcept; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.VoucherTransactionAdvance; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetPortfolioAdvanceByAge';
GO
