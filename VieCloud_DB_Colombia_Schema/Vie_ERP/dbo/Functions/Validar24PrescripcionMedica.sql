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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si la duración de una prescripción médica es igual o mayor a 24 horas (1440 minutos). Recibe la cantidad de tiempo y su unidad (1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años), convierte todo a minutos y devuelve 1 si la prescripción cubre al menos un día completo, o 0 en caso contrario. Se usa en el módulo de prescripción o receta médica para asegurar que la duración del tratamiento cumpla con un mínimo de 24 horas antes de ser registrada o procesada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Validar24PrescripcionMedica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Validar24PrescripcionMedica';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si la duración de una prescripción médica, expresada en distintas unidades de tiempo, equivale o supera 24 horas (1440 minutos), devolviendo 1 si cumple y 0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de unidad debe estar en el rango 1-6 (1:Minutos, 2:Horas, 3:Días, 4:Semanas, 5:Meses, 6:Años); cualquier otro valor deja el resultado sin asignar (NULL).; El valor de unidad debe ser numérico y compatible con la multiplicación por factores de conversión a minutos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El umbral de validación equivale siempre a 24 horas (1440 minutos).; Un mes se considera fijo en 43800 minutos (~30.4 días) y un año en 525600 minutos (365 días).; Una semana se considera 10080 minutos (7 días exactos).; Si el tipo de unidad no coincide con ninguno de los valores 1-6, la función retorna NULL al no asignarse @Validador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Duración fija de tratamiento; Unidades de tiempo (minutos, horas, días, semanas, meses, años)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando la duración convertida a minutos es >= 1440 (24 horas) retorna 1; en caso contrario retorna 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @tipoUnidad = 1 (Minutos) → Compara @unidad directamente contra 1440 minutos.; si @tipoUnidad = 2 (Horas) → Convierte multiplicando @unidad * 60 y compara contra 1440.; si @tipoUnidad = 3 (Días) → Convierte multiplicando @unidad * 1440 y compara contra 1440.; si @tipoUnidad = 4 (Semanas) → Convierte multiplicando @unidad * 10080 y compara contra 1440.; si @tipoUnidad = 5 (Meses) → Convierte multiplicando @unidad * 43800 y compara contra 1440.; si @tipoUnidad = 6 (Años) → Convierte multiplicando @unidad * 525600 y compara contra 1440.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Validar24PrescripcionMedica';
GO
