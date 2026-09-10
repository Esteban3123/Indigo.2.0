-- =============================================
-- Author:      Alvaro Heliud Herrera Novoa
-- Create Date: 2020-08-24
-- Description: Funcion Common.GETDATE() para PAAS
-- =============================================
CREATE FUNCTION [dbo].[Common.GETDATE()]()
RETURNS DATETIME
AS
BEGIN
    -- Declare the return variable here
    DECLARE @ResultVar DATETIME

    -- Add the T-SQL statements to compute the return value here
    SELECT @ResultVar = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'SA Pacific Standard Time' AS datetime)

    -- Return the result of the function
    RETURN @ResultVar
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha y hora actual convertida a la zona horaria ''SA Pacific Standard Time'' (UTC-5, correspondiente a Colombia/Perú/Ecuador). Fue creada como alternativa a `GETDATE()` para entornos PaaS (Azure SQL), donde la hora del servidor puede estar en UTC, garantizando consistencia horaria en los registros clínicos o transaccionales del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha y hora actual ajustada a la zona horaria ''SA Pacific Standard Time'' para uso uniforme en entornos PaaS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La hora retornada siempre corresponde a la zona horaria ''SA Pacific Standard Time'' independientemente de la zona del servidor.; El valor retornado es de tipo DATETIME (sin offset).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna SYSDATETIMEOFFSET() convertido a la zona horaria ''SA Pacific Standard Time'' y casteado a DATETIME.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Common.GETDATE()';
GO
