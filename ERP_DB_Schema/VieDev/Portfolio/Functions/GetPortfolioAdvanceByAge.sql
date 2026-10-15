
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
    - ISNULL(pn.DebitValue, 0)
    - ISNULL(pn.CreditValue, 0)
    - ISNULL(pt.TransferValue, 0)
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
               SUM(IIF(ptoc.Nature = 0, ptoc.Value, 0)) AS CreditValue
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
               IIF(pn.Nature = 0, pnara.AdjusmentValue, 0) AS CreditValue
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
           SUM(IIF(vtd.Nature = 0, vta.Value, 0)) AS CreditValue
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