

CREATE FUNCTION [dbo].[DiferenciaTXTDiasHoras] (@Fecha1 as datetime, @Fecha2 as datetime)
RETURNS varchar(max)
AS
BEGIN

declare @Tiempo varchar(max)
declare @r1 int
declare @r2 int
declare @r3 int

set @r1 = DateDiff(SECOND, @Fecha1, @Fecha2) / 86400
set @r2 = DateDiff(SECOND, @Fecha1, @Fecha2) / 3600
set @r3 = DateDiff(SECOND, @Fecha1, @Fecha2) / 60

set @Tiempo = Str(@r1 % 365) + ' días ' + Str( @r2 % 24) + ' horas ' + Str( @r3 % 60) + ' minutos '

RETURN @Tiempo

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la diferencia de tiempo entre dos fechas y la expresa en texto legible con días, horas y minutos. Recibe dos parámetros de tipo fecha (@Fecha1 como inicio y @Fecha2 como fin) y retorna una cadena con el formato ''X días Y horas Z minutos''. Se utiliza para mostrar duraciones en lenguaje humano, por ejemplo la estancia de un paciente, el tiempo entre una orden médica y su ejecución, o el lapso entre el ingreso y el egreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaTXTDiasHoras';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaTXTDiasHoras';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en formato de texto, la diferencia entre dos fechas expresada en días, horas y minutos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaTXTDiasHoras';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Ambas fechas deben ser valores datetime válidos.; Se asume que la segunda fecha es posterior o igual a la primera para obtener diferencias positivas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaTXTDiasHoras';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La diferencia se calcula siempre en base a segundos transcurridos entre las dos fechas, derivando luego días, horas y minutos.; Los días se expresan módulo 365 (no contempla años).; Las horas se expresan módulo 24 y los minutos módulo 60.; El resultado siempre se devuelve como cadena con el formato ''X días Y horas Z minutos''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaTXTDiasHoras';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna una cadena concatenando ''(días%365) días (horas%24) horas (minutos%60) minutos'' calculados a partir de DateDiff en segundos entre las dos fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaTXTDiasHoras';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaTXTDiasHoras';
GO
