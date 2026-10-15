

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