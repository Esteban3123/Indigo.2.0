-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-05-25
-- Description:	Generación reporte informe de impuestos
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportRetentions]
	@Criterios xml,
	@filtros xml
AS
BEGIN
    SET NOCOUNT ON;
    SET DATEFORMAT YMD;

    DECLARE @DateStart DATE,
            @DateEnd DATE,
            @AccountStart VARCHAR(50),
            @AccountEnd VARCHAR(50),
            @NitStart VARCHAR(15),
            @NitEnd VARCHAR(15),
            @LegalBookId INT;

    BEGIN TRY

        SELECT  @DateStart = t.x.value('DateStart[1]', 'DATE'),
                @DateEnd = t.x.value('DateEnd[1]', 'DATE'),
                @LegalBookId = t.x.value('Book[1]', 'INT')
        FROM @Criterios.nodes('/Data') t(x);

        SELECT  @AccountStart = t.x.value('AccountsStart[1]', 'VARCHAR(50)'),
                @AccountEnd = t.x.value('AccountsEnd[1]', 'VARCHAR(50)'),
                @NitStart = t.x.value('ThirdPartyStart[1]', 'VARCHAR(15)'),
                @NitEnd = t.x.value('ThirdPartyEnd[1]', 'VARCHAR(15)')
        FROM @filtros.nodes('/Data') t(x);

        SELECT
            jv.Id,
            jv.VoucherDate,
            jv.Consecutive,
            jv.EntityName,
            jv.EntityCode,
            jv.EntityId,
            jv.IdJournalVoucher,
            jv.Status
        INTO #VouchersFiltered
        FROM GeneralLedger.JournalVouchers AS jv
        WHERE jv.VoucherDate >= @DateStart
          AND jv.VoucherDate < DATEADD(DAY, 1, @DateEnd)
          AND jv.LegalBookId = @LegalBookId
          AND jv.Status = 2;

        CREATE CLUSTERED INDEX IX_VouchersFiltered_Id
            ON #VouchersFiltered (Id);

        CREATE NONCLUSTERED INDEX IX_VouchersFiltered_JournalVoucher
            ON #VouchersFiltered (IdJournalVoucher)
            INCLUDE (VoucherDate, Consecutive, EntityName, EntityCode, EntityId, Status);

        SELECT
            jv.Id,
            CAST(jv.VoucherDate AS DATE) AS VoucherDate,
            JVT.Code + ' ' + JVT.Name AS CodeNameVoucherType,
            jv.Consecutive,
            CASE jv.EntityName
                WHEN 'AccountPayable' THEN 'Cuenta por Pagar'
                WHEN 'BasicBilling' THEN 'Factura básica'
                WHEN 'CashReceipts' THEN 'Recibo de caja'
                WHEN 'PaymentNotes' THEN 'Nota'
                WHEN 'PayrollLiquidation' THEN 'Liquidación de nomina'
                WHEN 'PortfolioNote' THEN 'Nota'
                WHEN 'TreasuryNote' THEN 'Nota'
                WHEN 'VoucherTransaction' THEN 'Egreso'
                WHEN 'PortfolioTransfer' THEN 'Cruce de anticipo vs CxC'
                WHEN 'PaymentTransfer' THEN 'Cruce de anticipo vs CxP'
                WHEN 'PortfolioReclassification' THEN 'Reclasificación documento de cartera'
                ELSE JVT.Name
            END + ' ' + ISNULL(jv.EntityCode, '') AS OriginDocument,
            ma.Number AS MainAccountNumber,
            ma.Name AS MainAccountName,
            tp.Nit AS ThirdPartyNit,
            tp.Name AS ThirdPartyName,
            jvd.DebitValue,
            jvd.CreditValue,
            jvd.Detail,
            ISNULL(rc.Name, '') AS RetentionConceptName,
            calc.RetentionRate,
            (
                CASE
                    WHEN ma.FreelancerCategory = 1
                         AND ISNULL(jvd.BillingValue, 0) > 0
                        THEN jvd.BillingValue
                    ELSE
                        ISNULL(
                            CASE calc.RetentionRate
                                WHEN 0 THEN CAST(jvd.BaseValue AS BIGINT)
                                WHEN 1 THEN CAST(jvd.BaseValue AS BIGINT)
                                ELSE CAST(ROUND((ABS(jvd.DebitValue - jvd.CreditValue) * 100.0 / calc.RetentionRate), 1) AS BIGINT)
                            END,
                            0
                        )
                END
            ) * calc.SignFactor AS BaseValue,
            CASE jv.Status
                WHEN 1 THEN 'Registrado'
                WHEN 2 THEN 'Confirmado'
                WHEN 3 THEN 'Anulado'
            END AS Estado,
            ISNULL(jvd.IdRetention, 0) AS IdRetention,
            jv.Status,
            tp.Id AS ThirdPartyId,
            ma.Id AS MainAccountId,
            jv.EntityId,
            jv.EntityName,
            ma.FreelancerCategory,
            calc.SignFactor
        INTO #RetentionReport
        FROM #VouchersFiltered AS jv
        INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT
            ON JVT.Id = jv.IdJournalVoucher
        INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd
            ON jv.Id = jvd.IdAccounting
        INNER JOIN GeneralLedger.MainAccounts AS ma
            ON ma.Id = jvd.IdMainAccount
        INNER JOIN Common.ThirdParty AS tp
            ON tp.Id = jvd.IdThirdParty
        LEFT JOIN GeneralLedger.RetentionConcepts AS rc
            ON rc.Id = jvd.IdRetention
        CROSS APPLY
        (
            SELECT
                ISNULL(jvd.RetentionRate, ISNULL(rc.Rate, 0)) AS RetentionRate,
                CASE
                    WHEN ma.Nature = 1
                        THEN CASE WHEN jvd.DebitValue >= jvd.CreditValue THEN 1 ELSE -1 END
                    ELSE CASE WHEN jvd.CreditValue >= jvd.DebitValue THEN 1 ELSE -1 END
                END AS SignFactor
        ) calc
        WHERE ma.RetencionType <> 0
          AND ma.Number >= ISNULL(@AccountStart, '0')
          AND ma.Number <= ISNULL(@AccountEnd, 'z')
          AND tp.Nit >= ISNULL(@NitStart, '0')
          AND tp.Nit <= ISNULL(@NitEnd, 'z');

        CREATE CLUSTERED INDEX IX_RetentionReport_MainAccountNumber_Id
            ON #RetentionReport (MainAccountNumber, Id);

        CREATE NONCLUSTERED INDEX IX_RetentionReport_Update
            ON #RetentionReport (FreelancerCategory, EntityName, EntityId)
            INCLUDE (BaseValue, SignFactor);

        UPDATE r
        SET r.BaseValue = r.BaseValue + ISNULL(liq.AccumulatedIncome, 0) * r.SignFactor
        FROM #RetentionReport r
        CROSS APPLY (
            SELECT TOP 1 apdcl.AccumulatedIncome
            FROM Payments.AccountPayableDetailConcept apdc
            JOIN Payments.AccountPayableDetailConceptLiquidation apdcl
                ON apdc.Id = apdcl.AccountPayableDetailConceptId
            WHERE apdc.IdAccountPayable = r.EntityId
        ) liq
        WHERE r.FreelancerCategory = 1
          AND r.EntityName = 'AccountPayable';

        SELECT
            Id,
            VoucherDate,
            CodeNameVoucherType,
            Consecutive,
            OriginDocument,
            MainAccountNumber,
            MainAccountName,
            ThirdPartyNit,
            ThirdPartyName,
            DebitValue,
            CreditValue,
            Detail,
            RetentionConceptName,
            RetentionRate,
            BaseValue,
            Estado,
            IdRetention,
            Status,
            ThirdPartyId,
            MainAccountId
        FROM #RetentionReport
        ORDER BY MainAccountNumber;

        DROP TABLE #RetentionReport;
        DROP TABLE #VouchersFiltered;

    END TRY
    BEGIN CATCH
        SELECT
            '999' AS Code,
            ERROR_MESSAGE() AS Message,
            ERROR_LINE() AS Line;
    END CATCH
END