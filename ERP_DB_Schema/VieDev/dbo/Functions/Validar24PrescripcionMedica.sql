-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Validar24PrescripcionMedica]
(
@tipoUnidad as int,
@unidad as int
)
RETURNS int
AS
BEGIN

declare @Validador as int

--@tipoUnidad = Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias 4:Semanas 5:meses 6:Años
 if @tipoUnidad = 1
	
	if @unidad >= 1440
		set @Validador = 1
	else
		set @Validador = 0

 else if @tipoUnidad = 2
	if @unidad * 60 >= 1440
		set @Validador = 1
	else
		set @Validador = 0

 else if @tipoUnidad = 3
		if @unidad * 1440 >= 1440
		set @Validador = 1
	else
		set @Validador = 0
	
else if @tipoUnidad = 4
	if @unidad * 10080 >= 1440
		set @Validador = 1
	else
		set @Validador = 0

else if @tipoUnidad = 5
	if @unidad * 43800 >= 1440
		set @Validador = 1
	else
		set @Validador = 0

else if @tipoUnidad = 6
	if @unidad * 525600 >= 1440
		set @Validador = 1
	else
		set @Validador = 0
		
return @Validador

END