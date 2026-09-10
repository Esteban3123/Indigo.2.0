
CREATE FUNCTION [dbo].[EtariosR] (@Fnacimiento as datetime, @Fatencion as datetime)
RETURNS varchar (13)
AS
BEGIN

declare @grupo varchar(13)
SET @grupo=
     CASE WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 0  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 4)  THEN '0 a 4 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 5  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 9)  THEN '5 a 9 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 10 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 14) THEN '10 a 14 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 15 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 19) THEN '15 a 19 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 20 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 24) THEN '20 a 24 años'
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 25 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 29) THEN '25 a 29 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 30 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 34) THEN '30 a 34 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 35 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 39) THEN '35 a 39 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 40 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 44) THEN '40 a 44 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 45 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 49) THEN '45 a 49 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 50 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 54) THEN '50 a 54 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 55 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 59) THEN '55 a 59 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 60 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 64) THEN '60 a 64 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 65 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 69) THEN '65 a 69 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 70 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 74) THEN '70 a 74 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 75 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 79) THEN '75 a 79 años' 
          WHEN (datediff(YY, @Fnacimiento, @Fatencion))>= 80 THEN '80 y más años' 
     END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasifica a un paciente en su grupo etario (rango de edad quinquenal) a partir de su fecha de nacimiento y la fecha de atención. Calcula la edad en años cumplidos al momento de la atención y retorna una etiqueta como ''0 a 4 años'', ''5 a 9 años'', ..., ''80 y más años''. Se usa en reportes estadísticos, epidemiológicos y de RIPS para agrupar atenciones por edad del paciente, permitiendo análisis de población atendida por rangos etarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EtariosR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EtariosR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica a una persona en un grupo etario quinquenal (0-4, 5-9, …, 75-79, 80 y más) a partir de su fecha de nacimiento y una fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de atención debe ser igual o posterior a la fecha de nacimiento (DATEDIFF en años >= 0); de lo contrario el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula con DATEDIFF(YY, ...), que solo considera la diferencia de años calendario, sin ajustar por mes/día.; Los grupos etarios son quinquenales y mutuamente excluyentes desde 0 hasta 79 años; 80 o más se agrupa en una única categoría abierta.; Si la diferencia de años es negativa, la función retorna NULL al no satisfacer ningún WHEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo etario; Fecha de nacimiento; Fecha de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (valor escalar): Retorna varchar(13) con la etiqueta del grupo etario quinquenal según los años calculados con DATEDIFF(YY, nacimiento, atención).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Años entre 0 y 4 → Retorna ''0 a 4 años''; si Años entre 5 y 9 → Retorna ''5 a 9 años''; si Años entre 10 y 14 → Retorna ''10 a 14 años''; si Años entre 15 y 19 → Retorna ''15 a 19 años''; si Años entre 20 y 24 → Retorna ''20 a 24 años''; si Años entre 25 y 29 → Retorna ''25 a 29 años''; si Años entre 30 y 34 → Retorna ''30 a 34 años''; si Años entre 35 y 39 → Retorna ''35 a 39 años''; si Años entre 40 y 44 → Retorna ''40 a 44 años''; si Años entre 45 y 49 → Retorna ''45 a 49 años''; si Años entre 50 y 54 → Retorna ''50 a 54 años''; si Años entre 55 y 59 → Retorna ''55 a 59 años''; si Años entre 60 y 64 → Retorna ''60 a 64 años''; si Años entre 65 y 69 → Retorna ''65 a 69 años''; si Años entre 70 y 74 → Retorna ''70 a 74 años''; si Años entre 75 y 79 → Retorna ''75 a 79 años''; si Años >= 80 → Retorna ''80 y más años''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR';
GO
