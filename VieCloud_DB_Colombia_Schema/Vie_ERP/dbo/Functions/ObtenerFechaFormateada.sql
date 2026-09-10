
create function [dbo].[ObtenerFechaFormateada]
(
	@Fecha as DateTime
)
returns nvarchar(25)
as
BEGIN
	declare @dd nvarchar(2)
	declare @MM nvarchar(2)
	declare @yyyy nvarchar(4)
	declare @hh nvarchar(2)
	declare @mms nvarchar(2)

	set @yyyy = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 0 ,5)
	set @MM = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 6 ,8)
	set @dd = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 9 ,11)

	set @hh = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 12 ,14)
	set @mms = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 15 ,17)

	return  @dd + '/' + @MM + '/' + @yyyy + ' ' + @hh + ':' + @mms
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función auxiliar que convierte una fecha y hora (DateTime) al formato legible DD/MM/YYYY HH:MM, utilizado en la presentación de fechas en documentos clínicos, reportes y pantallas del sistema. Recibe una fecha con hora y devuelve una cadena de texto formateada según el estándar de visualización local (día/mes/año hora:minuto). Se usa típicamente para mostrar fechas de ingreso, atención, órdenes médicas, citas o cualquier evento clínico-administrativo de forma amigable para el usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ObtenerFechaFormateada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ObtenerFechaFormateada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Formatea un valor DateTime a una cadena con el patrón ''dd/MM/yyyy hh:mm'' para presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaFormateada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un valor DateTime válido como entrada para que la conversión al estilo 120 produzca el formato esperado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaFormateada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha se devuelve siempre en formato ''dd/MM/yyyy hh:mm'' a partir del estilo ODBC canónico 120 (yyyy-MM-dd hh:mm:ss).; Se descartan los segundos: solo se conservan horas y minutos en la cadena resultante.; El resultado es de tipo nvarchar(25).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaFormateada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaFormateada';
GO
