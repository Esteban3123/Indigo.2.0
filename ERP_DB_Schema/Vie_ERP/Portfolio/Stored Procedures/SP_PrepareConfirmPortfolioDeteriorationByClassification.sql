-- =============================================
-- Author:      Andrés Steven Rojas
-- Create date: 2026-02-18
-- Description: Prepara y valida el proceso de confirmación de deterioro
--              por clasificación (Process=2, ApplyDeterioration=3).
--              Confirma la cabecera del PortfolioProvision en Status=2
--              y retorna los grupos de ThirdPartyId para procesamiento
--              paralelo desde la capa de aplicación.
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_PrepareConfirmPortfolioDeteriorationByClassification]
    @PortfolioProvisionId   INT,
    @CodeUser               VARCHAR(20),
    @OperativeUnitId        INT
AS
BEGIN
    SET NOCOUNT ON;

    /***************************************** VARIABLES *****************************************/
    DECLARE
        @Code                                       VARCHAR(20),
        @DocumentDate                               DATE,
        @CourtDate                                  DATE,
        @OperatingUnitId                            INT,
        @errors                                     VARCHAR(MAX),
        @BasicBillingDebitAccount                   INT,
        @BasicBillingCreditAccount                  INT,
        @BasicBillingReversalAccount                INT,
        @BasicBillingPreviousPeriodReversalAccount  INT;

    DECLARE @TableErrors TABLE ([Message] VARCHAR(500));

    BEGIN TRY

        /***************************************** ASIGNACIONES *****************************************/
        SELECT
            @Code               = pp.Code,
            @DocumentDate       = pp.DocumentDate,
            @CourtDate          = pp.CourtDate,
            @OperatingUnitId    = pp.OperatingUnitId
        FROM Portfolio.PortfolioProvision pp
        WHERE pp.Id = @PortfolioProvisionId;

        -- Cargar cuentas de facturación básica para DocumentType 6 y 7
        SELECT
            @BasicBillingDebitAccount                   = dbbp.DebitDeteriorationAccount,
            @BasicBillingCreditAccount                  = dbbp.CreditDeteriorationAccount,
            @BasicBillingReversalAccount                = dbbp.ReversalDeteriorationAccount,
            @BasicBillingPreviousPeriodReversalAccount  = dbbp.PreviousPeriodReversalAccount
        FROM Portfolio.DeteriorationBasicBillingPortfolio dbbp
        JOIN Portfolio.SettingPortfolio sp ON sp.Id = dbbp.SettingPortfolioId
        WHERE sp.OperatingUnitId = @OperativeUnitId;

        /***************************************** VALIDACIONES RÁPIDAS *****************************************/

        -- Validar estado de la cabecera
        IF EXISTS (
            SELECT 1 FROM Portfolio.PortfolioProvision pp
            WHERE pp.Id = @PortfolioProvisionId AND pp.[Status] <> 1
        )
        BEGIN
            SELECT
                999 AS CodeMessage,
                'La Modificación de Obligación se encuentra en estado: '
                    + IIF(pp.[Status] = 2, 'Confirmado', 'Anulado') AS Message,
                '' AS Consecutive
            FROM Portfolio.PortfolioProvision pp
            WHERE pp.Id = @PortfolioProvisionId;
            RETURN;
        END

        -- Validar parámetros de la unidad operativa
        IF NOT EXISTS (
            SELECT sp.Id FROM Portfolio.SettingPortfolio sp
            WHERE OperatingUnitId = @OperatingUnitId
        )
        BEGIN
            SELECT 999 AS CodeMessage,
                   'No existe parámetros de cartera para la unidad operativa escogida' AS Message,
                   '' AS Consecutive;
            RETURN;
        END

        -- Validar cuentas básicas si existen facturas tipo 6 o 7
        IF EXISTS (
            SELECT 1
            FROM Portfolio.PortfolioProvisionDetail ppd
            JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd.AccountReceivableId
            JOIN Billing.Invoice inv ON inv.Id = ar.InvoiceId
            WHERE ppd.PortfolioProvisionId = @PortfolioProvisionId
              AND inv.DocumentType IN (6, 7)
        )
        BEGIN
            IF @BasicBillingDebitAccount IS NULL
               OR @BasicBillingCreditAccount IS NULL
               OR @BasicBillingReversalAccount IS NULL
               OR @BasicBillingPreviousPeriodReversalAccount IS NULL
            BEGIN
                INSERT INTO @TableErrors
                SELECT 'No se tienen parametrizadas las cuentas para la contabilización de las facturas básicas en la Unidad Operativa '
                       + CONCAT(ou.UnitCode, ' - ', ou.UnitName)
                FROM Common.OperatingUnit ou
                WHERE ou.Id = @OperativeUnitId;
            END
        END

        /***************************************** VALIDACIONES EN BATCH *****************************************/
        -- Un único JOIN sobre la tabla real para todas las validaciones de cuentas y consistencia.
        -- Evita los 8 scans secuenciales del SP original.
        INSERT INTO @TableErrors ([Message])
        SELECT DISTINCT
            CASE
                WHEN ar.CreditAccountDeteriorationId IS NULL
                     AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
                     THEN 'La factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code
                          + ' no tiene diligenciado el campo CreditAccountDeteriorationId' + CHAR(13) + CHAR(10)
                WHEN ar.DebitAccountDeteriorationId IS NULL
                     AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
                     THEN 'La factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code
                          + ' no tiene diligenciado el campo DebitAccountDeteriorationId' + CHAR(13) + CHAR(10)
                WHEN ar.ReversalAccountDeteriorationId IS NULL
                     AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
                     THEN 'La factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code
                          + ' no tiene diligenciado el campo ReversalAccountDeteriorationId' + CHAR(13) + CHAR(10)
                WHEN ar.PreviousPeriodReversalAccountDeteriorationId IS NULL
                     AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
                     THEN 'La factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code
                          + ' no tiene diligenciado el campo PreviousPeriodReversalAccountDeteriorationId' + CHAR(13) + CHAR(10)
                WHEN ar.Balance <> ppd.BalanceAccountReceivable
                     THEN 'El saldo de la factura ' + ppd.InvoiceNumber + ' ha cambiado, por favor actualícela' + CHAR(13) + CHAR(10)
                WHEN ar.DeteriorationBalance <> ppd.AccumulatedDeterioration
                     THEN 'El deterioro acumulado de la factura ' + ppd.InvoiceNumber + ' ha cambiado, por favor actualícela' + CHAR(13) + CHAR(10)
                WHEN ar.DeteriorationBalanceCurrentYear <> ppd.DeteriorationBalanceCurrentYear
                     THEN 'El deterioro acumulado en el año de la factura ' + ppd.InvoiceNumber + ' ha cambiado, por favor actualícela' + CHAR(13) + CHAR(10)
                WHEN ar.DeteriorationBalancePreviousYear <> ppd.DeteriorationBalancePreviousYear
                     THEN 'El deterioro acumulado de años anteriores de la factura ' + ppd.InvoiceNumber + ' ha cambiado, por favor actualícela' + CHAR(13) + CHAR(10)
                WHEN ar.CurrentDeteriorationYear <> ppd.CurrentDeteriorationYear
                     THEN 'El año del último deterioro de la factura ' + ppd.InvoiceNumber + ' ha cambiado, por favor actualícela' + CHAR(13) + CHAR(10)
                WHEN ppd.[Value] > ar.Balance
                     THEN 'El saldo de la factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code
                          + ' es menor al valor a Deteriorar' + CHAR(13) + CHAR(10)
            END
        FROM Portfolio.PortfolioProvisionDetail ppd
        JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd.AccountReceivableId
        LEFT JOIN Billing.Invoice inv ON inv.Id = ar.InvoiceId
        WHERE ppd.PortfolioProvisionId = @PortfolioProvisionId
          AND (
                ar.CreditAccountDeteriorationId IS NULL AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
             OR ar.DebitAccountDeteriorationId IS NULL AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
             OR ar.ReversalAccountDeteriorationId IS NULL AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
             OR ar.PreviousPeriodReversalAccountDeteriorationId IS NULL AND ISNULL(inv.DocumentType, 0) NOT IN (6, 7)
             OR ar.Balance <> ppd.BalanceAccountReceivable
             OR ar.DeteriorationBalance <> ppd.AccumulatedDeterioration
             OR ar.DeteriorationBalanceCurrentYear <> ppd.DeteriorationBalanceCurrentYear
             OR ar.DeteriorationBalancePreviousYear <> ppd.DeteriorationBalancePreviousYear
             OR ar.CurrentDeteriorationYear <> ppd.CurrentDeteriorationYear
             OR ppd.[Value] > ar.Balance
          );

        -- Validar duplicados de deterioro en mismo mes o posterior
        INSERT INTO @TableErrors ([Message])
        SELECT
            'Ya se ha generado deterioro para la factura ' + ppd_src.InvoiceNumber
            + ' con código(CxC) ' + ar.Code
            + ' en el mes ' + CAST(MONTH(dup.CourtDate) AS VARCHAR)
            + ' - año ' + CAST(YEAR(dup.CourtDate) AS VARCHAR) + CHAR(13) + CHAR(10)
        FROM Portfolio.PortfolioProvisionDetail ppd_src
        JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd_src.AccountReceivableId
        CROSS APPLY (
            SELECT TOP 1 pp_conf.CourtDate
            FROM Portfolio.PortfolioProvisionDetail ppd_det
            JOIN Portfolio.PortfolioProvision pp_conf ON ppd_det.PortfolioProvisionId = pp_conf.Id
            WHERE ppd_det.AccountReceivableId = ppd_src.AccountReceivableId
              AND pp_conf.[Status] = 2
              AND pp_conf.DocumentType = 2
              AND pp_conf.Id <> @PortfolioProvisionId
              AND pp_conf.CourtDate >= DATEFROMPARTS(YEAR(@CourtDate), MONTH(@CourtDate), 1)
        ) dup
        WHERE ppd_src.PortfolioProvisionId = @PortfolioProvisionId;

        -- Retornar errores si los hay
        IF EXISTS (SELECT 1 FROM @TableErrors WHERE [Message] IS NOT NULL)
        BEGIN
            SELECT @errors = COALESCE(@errors + '', '') + [Message]
            FROM @TableErrors
            WHERE [Message] IS NOT NULL;

            SELECT 999 AS CodeMessage, @errors AS Message, '' AS Consecutive;
            RETURN;
        END

        /***************************************** CONFIRMAR CABECERA *****************************************/
        -- Se confirma ANTES de lanzar los lotes paralelos para evitar condición de carrera
        -- y para que la validación de Status <> 1 del inicio bloquee re-entradas.
        UPDATE Portfolio.PortfolioProvision
        SET
            [Status]           = 2,
            ConfirmationUser   = @CodeUser,
            ConfirmationDate   = [Common].[GETDATE]()
        WHERE Id = @PortfolioProvisionId;

        /***************************************** RETORNO DE GRUPOS *****************************************/
        -- Devuelve los ThirdPartyId distintos con su volumen para que la aplicación
        -- balancee los lotes y gestione el paralelismo.
        SELECT 0 AS CodeMessage, 'Preparación exitosa' AS Message, '' AS Consecutive;

        SELECT
            ar.ThirdPartyId,
            COUNT(ppd.Id) AS DetailCount
        FROM Portfolio.PortfolioProvisionDetail ppd
        JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd.AccountReceivableId
        WHERE ppd.PortfolioProvisionId = @PortfolioProvisionId
        GROUP BY ar.ThirdPartyId
        ORDER BY COUNT(ppd.Id) DESC; -- Descendente para balanceo de carga en la app

    END TRY
    BEGIN CATCH
        SELECT
            999 AS CodeMessage,
            ERROR_MESSAGE() + ' línea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message,
            '' AS Consecutive;
    END CATCH
END
GO
