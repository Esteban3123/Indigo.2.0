CREATE  FUNCTION [dbo].[ObtenerDigitoVerificacion]
(
	@Nit NVARCHAR(20)
 )
 RETURNS VARCHAR(1)
 
 AS
 BEGIN
 DECLARE @dv INT
 DECLARE @Lenght INT
 DECLARE @SEQ NVARCHAR(30)
 DECLARE @Contador INT
 DECLARE @Multiplo INT
 DECLARE @Acumulador INT
 --Variables
set @Lenght = Len(LTRIM (RTRIM ((@Nit))))
set @SEQ = substring('716759534743413729231917130703', (31 - (@Lenght * 2)),31)
set @Contador = 1
set @dv = 0
--Calculo sobre secuencia
while (@Contador <= @Lenght)
begin
    --Sumo y multiplico
    set @dv = @dv + (cast(substring(@SEQ,1,2) as INT)) * (cast(substring(@Nit, @Contador, 1) as INT))
    --Recorto secuencia y aumento indice
    set @SEQ = substring(@SEQ,3,31)
    set @Contador = @Contador + 1
end
--Obtengo digito de verificacion
set @dv = 11 - (@dv - (floor(@dv / 11) * 11))
 
RETURN @dv
 
END