
create FUNCTION [dbo].[EstadoCivilPaciente] (@EstadoCivil as int, @Sexopaciente as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @EstadoCivilPaciente nvarchar (60)

SELECT @EstadoCivilPaciente = CASE WHEN (@EstadoCivil = 1 AND @Sexopaciente = 1) THEN 'SOLTERO' WHEN (@EstadoCivil = 1 AND @Sexopaciente = 2) THEN 'SOLTERA' WHEN (@EstadoCivil = 2 AND @Sexopaciente = 1) THEN 'CASADO' WHEN (@EstadoCivil = 2 AND @Sexopaciente = 2) THEN 'CASADA' WHEN (@EstadoCivil = 3 AND @Sexopaciente = 1) THEN 'VIUDO' WHEN (@EstadoCivil = 3 AND @Sexopaciente = 2) THEN 'VIUDA' WHEN (@EstadoCivil = 4 AND @Sexopaciente = 1) THEN 'UNION LIBRE' WHEN (@EstadoCivil = 4 AND @Sexopaciente = 2) THEN 'UNION LIBRE' WHEN (@EstadoCivil = 5 AND @Sexopaciente = 1) THEN 'SEPARADO' WHEN (@EstadoCivil = 5 AND @Sexopaciente = 2) THEN 'SEPARADA' END  

RETURN @EstadoCivilPaciente

END