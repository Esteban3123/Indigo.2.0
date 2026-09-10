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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de retenciones tributarias (impuestos retenidos) para un período y libro contable determinados. Consolida comprobantes contables confirmados, sus cuentas principales de retención y los terceros (proveedores, contratistas u otras entidades) asociados a cada movimiento, calculando la base gravable, la tarifa de retención y el valor debitado/acreditado por concepto de retención. Permite filtrar por rango de fechas, rango de cuentas contables y rango de NIT del tercero, produciendo un informe detallado utilizado para declaraciones de retención en la fuente, IVA retenido y otros tributos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRetentions';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRetentions';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el informe de retenciones tributarias listando los movimientos contables confirmados de un libro legal cuyas cuentas tienen tipo de retención, calculando la base y tarifa aplicada por línea, filtrado por fechas, rango de cuentas y rango de NIT.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben contener los nodos esperados (DateStart, DateEnd, Book, AccountsStart/End, ThirdPartyStart/End); Debe existir al menos un libro legal válido referenciado por LegalBookId; Las cuentas principales deben tener configurada su naturaleza (Nature) y tipo de retención (RetencionType); El formato de fecha se fuerza a YMD para la sesión', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos de comprobantes en estado Confirmado (Status = 2); Solo se incluyen líneas asociadas a cuentas con configuración de retención (RetencionType <> 0); El reporte está acotado al libro legal indicado (LegalBookId); Si no se especifican rangos de cuenta o NIT, se aplica un rango abierto (''0'' a ''z'') vía ISNULL; La tarifa de retención reportada toma primero la del detalle (jvd.RetentionRate) y, si es nula, la del concepto de retención (rc.Rate); si ambas son nulas se asume 0; El signo de la base de retención respeta la naturaleza contable de la cuenta principal; Cuando la tarifa es 0 o 1 no se reconstruye la base por fórmula, se usa BaseValue tal cual; Ante cualquier excepción se devuelve un único registro con Code=''999'', mensaje y línea de error en lugar del resultado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención tributaria; Concepto de retención; Tarifa de retención; Base de retención; Comprobante contable; Tipo de comprobante; Cuenta contable (PUC); Naturaleza de la cuenta (débito/crédito); Tercero (NIT); Libro legal contable; Honorarios / Freelancer; Cuenta por pagar; Factura básica; Recibo de caja; Egreso; Cruce de anticipos (CxC/CxP); Reclasificación de cartera; Liquidación de nómina', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.JournalVoucherDetails: Cuando jv.Status=2, ma.RetencionType<>0 y la fecha/cuenta/NIT están dentro de los rangos, se retorna una fila por línea de detalle con datos del comprobante, cuenta, tercero, concepto y base de retención calculada; [RETURN_RESULT] (resultset de error): Si ocurre cualquier error en el TRY, se retorna un único resultset con Code=''999'', ERROR_MESSAGE() y ERROR_LINE() en lugar del informe', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.FreelancerCategory = 1 AND ISNULL(jvd.BillingValue,0) > 0 → Se usa jvd.BillingValue como base de retención (caso honorarios/freelancer con valor facturado) else Se calcula la base según la tarifa: si Rate ∈ {0,1} se toma jvd.BaseValue; en otro caso se reconstruye como ABS(Debito-Credito)*100/Rate; si ma.Nature = 1 (cuenta de naturaleza débito) → El signo de la base es +1 si DebitValue ≥ CreditValue, de lo contrario -1 else Para naturaleza crédito, el signo es +1 si CreditValue ≥ DebitValue, de lo contrario -1; si JV.EntityName coincide con valores conocidos (AccountPayable, BasicBilling, CashReceipts, PaymentNotes, PayrollLiquidation, PortfolioNote, TreasuryNote, VoucherTransaction, PortfolioTransfer, PaymentTransfer, PortfolioReclassification) → Se traduce a la descripción en español del tipo de documento origen else Se usa JVT.Name como descripción del documento origen; si jv.Status ∈ {1,2,3} → Se traduce a Registrado / Confirmado / Anulado respectivamente', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRetentions';
-- GO
