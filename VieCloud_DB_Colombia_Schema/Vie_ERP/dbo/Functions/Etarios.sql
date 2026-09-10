CREATE FUNCTION [dbo].[Etarios] (@Fnacimiento as datetime, @Fatencion as datetime)
RETURNS varchar (18)
AS
BEGIN

declare @grupo varchar(18)
SET @grupo=
     CASE WHEN (datediff(YY,@Fnacimiento, @Fatencion))<=1 THEN 'Menores de 1 año' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>1  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 4)  THEN 'Entre 1 y 4 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 5 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 14) THEN 'Entre 5 y 14 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 15 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 44) THEN 'Entre 15 y 44 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 45 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 59) THEN 'Entre 45 y 59 años' 
          WHEN (datediff(YY, @Fnacimiento, @Fatencion))>= 60 THEN '60 y más años' 
     END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el grupo etario de un paciente según su edad al momento de la atención, comparando la fecha de nacimiento con la fecha de atención. Retorna una categoría de edad en texto (por ejemplo: ''Menores de 1 año'', ''Entre 15 y 44 años'', ''60 y más años'') siguiendo los rangos estándar utilizados en reportería epidemiológica y estadísticas de salud pública. Se usa para clasificar pacientes por franja de edad en informes, RIPS y análisis demográficos de atenciones médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Etarios';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Etarios';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica a una persona en un grupo etario estándar a partir de su fecha de nacimiento y la fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren fecha de nacimiento y fecha de atención válidas (no nulas) para calcular la diferencia en años.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La clasificación se realiza usando DATEDIFF en años (YY), por lo que considera el cambio de año calendario y no la edad cumplida exacta.; Los grupos etarios son mutuamente excluyentes y cubren desde menores de 1 año hasta 60 y más años.; Siempre devuelve una cadena de máximo 18 caracteres correspondiente a un único grupo etario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo etario; Fecha de nacimiento; Fecha de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una etiqueta de grupo etario según el rango de años calculado con DATEDIFF entre fecha de nacimiento y fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Diferencia en años entre fecha de nacimiento y fecha de atención <= 1 → Clasifica como ''Menores de 1 año''; si Diferencia en años > 1 y <= 4 → Clasifica como ''Entre 1 y 4 años''; si Diferencia en años entre 5 y 14 → Clasifica como ''Entre 5 y 14 años''; si Diferencia en años entre 15 y 44 → Clasifica como ''Entre 15 y 44 años''; si Diferencia en años entre 45 y 59 → Clasifica como ''Entre 45 y 59 años''; si Diferencia en años >= 60 → Clasifica como ''60 y más años''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Etarios';
GO
