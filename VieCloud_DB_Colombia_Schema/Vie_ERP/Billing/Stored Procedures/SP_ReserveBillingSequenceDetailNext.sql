-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 2026-05-14
-- Description:	Procedimiento que se encarga de manejar la secuencia de BillingNote para evitar desincronización
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReserveBillingSequenceDetailNext]
    @IdForm VARCHAR(5)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @IdForm IS NULL OR LTRIM(RTRIM(@IdForm)) = N''
    BEGIN
        SELECT
            0                        AS CodeResult,
            N'IdForm inválido.'      AS MessageResult,
            CAST(NULL AS INT)        AS DetailId,
            CAST(NULL AS BIGINT)     AS ReservedNext,
            CAST(NULL AS VARCHAR(20)) AS Pattern;
        RETURN;
    END;

    -- La secuencia de Notas Crédito de Facturación Electrónica debe tener ámbito de Organización (Scope = 'O')
    IF EXISTS (
        SELECT 1
        FROM Billing.BillingSequence AS bs
        WHERE RTRIM(bs.IdForm) = RTRIM(@IdForm)
          AND bs.Id > 0
          AND RTRIM(ISNULL(bs.Scope, N'')) = N'OU'
    )
    BEGIN
        SELECT
            0                                                                                                                      AS CodeResult,
            N'El ámbito para la secuencia de Notas Crédito de Facturacion Electronica no debe ser por unidad operativa.'          AS MessageResult,
            CAST(NULL AS INT)                                                                                                      AS DetailId,
            CAST(NULL AS BIGINT)                                                                                                   AS ReservedNext,
            CAST(NULL AS VARCHAR(20))                                                                                              AS Pattern;
        RETURN;
    END;

    BEGIN TRANSACTION;

    DECLARE @Id INT;

    -- UPDLOCK + ROWLOCK garantizan que ninguna otra sesión lea el mismo [Next] antes del commit
    SELECT TOP (1)
        @Id = bd.Id
    FROM Billing.BillingSequenceDetail AS bd WITH (UPDLOCK, ROWLOCK)
    INNER JOIN Billing.BillingSequence  AS bs ON bs.Id = bd.IdSequenseBillingC
    WHERE RTRIM(bs.IdForm)               = RTRIM(@IdForm)
      AND RTRIM(ISNULL(bs.Scope, N'')) = N'O'
      AND bs.Sequential                 = 1
      AND bs.Id                         > 0
    ORDER BY bd.Id;

    IF @Id IS NULL
    BEGIN
        ROLLBACK TRANSACTION;
        SELECT
            0                                                                                                                                 AS CodeResult,
            N'La secuencia para las Notas Crédito de Facturacion Electronica no esta parametrizada o no es secuencial.'                      AS MessageResult,
            CAST(NULL AS INT)                                                                                                                 AS DetailId,
            CAST(NULL AS BIGINT)                                                                                                              AS ReservedNext,
            CAST(NULL AS VARCHAR(20))                                                                                                         AS Pattern;
        RETURN;
    END;

    DECLARE @ReservedNext BIGINT;
    DECLARE @IdSequense   INT;

    -- Lee el valor actual y lo incrementa atómicamente en una sola sentencia
    UPDATE Billing.BillingSequenceDetail
    SET
        @ReservedNext = [Next],
        @IdSequense   = IdSequense,
        [Next]        = [Next] + 1
    WHERE Id = @Id;

    DECLARE @Pattern VARCHAR(20);

    SELECT @Pattern = s.Pattern
    FROM Common.Sequense AS s
    WHERE s.Id = @IdSequense;

    COMMIT TRANSACTION;

    SELECT
        1                        AS CodeResult,
        CAST(N'' AS NVARCHAR(500)) AS MessageResult,
        @Id                      AS DetailId,
        @ReservedNext            AS ReservedNext,
        @Pattern                 AS Pattern;
END;
