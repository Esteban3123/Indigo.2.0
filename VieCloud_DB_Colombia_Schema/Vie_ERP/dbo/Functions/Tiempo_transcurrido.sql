-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Tiempo_transcurrido] (
@FECHA_INICIO as DATETIME,
@FECHA_FIN AS DATETIME

)
RETURNS varchar (100)
AS
BEGIN

declare @CODIGO varchar(100)

SET @CODIGO=(CAST(DATEPART(hh, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)) + ':' + CAST(DATEPART(mi, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)) + ':' + CAST(DATEPART(s, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)))

RETURN (@CODIGO)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el tiempo transcurrido entre dos fechas y horas, devolviendo el resultado en formato HH:MM:SS como texto. Recibe una fecha de inicio y una fecha de fin, y retorna la diferencia expresada en horas, minutos y segundos. Se utiliza para medir duraciones en procesos clínicos o administrativos, como tiempos de atención, espera o estancia del paciente. Es una función auxiliar de apoyo a reportes de tiempos de respuesta y gestión asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tiempo_transcurrido';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tiempo_transcurrido';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el tiempo transcurrido entre dos fechas y lo devuelve como cadena con formato ''HH:MM:SS''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Ambas fechas deben ser válidas (DATETIME no nulo) para evitar resultado nulo en la resta y CAST.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se devuelve como texto en formato ''H:M:S'' concatenado con '':''.; Usa la resta directa de DATETIME, por lo que el cálculo se basa en el desplazamiento desde la fecha base de SQL Server (1900-01-01).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tiempo transcurrido entre dos fechas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna varchar(100) con la diferencia de fechas formateada como ''horas:minutos:segundos'' obtenida vía DATEPART sobre (@FECHA_INICIO - @FECHA_FIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tiempo_transcurrido';
GO
