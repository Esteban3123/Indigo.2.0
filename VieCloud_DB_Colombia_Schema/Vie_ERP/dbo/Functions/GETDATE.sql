-- =============================================
-- Author:      Alvaro Heliud Herrera Novoa
-- Create Date: 2020-08-24
-- Description: Funcion Getdate() para PAAS
-- =============================================
CREATE FUNCTION [dbo].[GETDATE]()
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que devuelve la fecha y hora actual ajustada a la zona horaria de Colombia (SA Pacific Standard Time, UTC-5), reemplazando el uso directo de GETDATE() nativo de SQL Server. Fue creada para garantizar que todos los registros del sistema ERP/EHR Indigo Vie Cloud usen la hora local colombiana en entornos de nube (PaaS/Azure), donde el servidor puede operar en una zona horaria diferente. Se usa transversalmente en todas las entidades del sistema (pacientes, ingresos, órdenes médicas, facturación) cada vez que se necesita registrar la fecha y hora actual correcta para Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GETDATE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener la fecha y hora actual ajustada a la zona horaria del Pacífico Sudamericano, sustituyendo el GETDATE() nativo para entornos PaaS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servidor debe reconocer la zona horaria ''SA Pacific Standard Time'' (requiere SQL Server 2016+ y soporte de zonas horarias del SO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha/hora retornada siempre corresponde a la zona horaria ''SA Pacific Standard Time''.; El valor se entrega como DATETIME, descartando el offset de zona horaria tras la conversión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna SYSDATETIMEOFFSET() convertido a ''SA Pacific Standard Time'' y casteado a DATETIME.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GETDATE';
GO
