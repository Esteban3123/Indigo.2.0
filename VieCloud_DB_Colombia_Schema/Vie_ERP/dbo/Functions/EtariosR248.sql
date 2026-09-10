
create FUNCTION [dbo].[EtariosR248] (@Fnacimiento as datetime, @Fatencion as datetime)
RETURNS varchar (13)
AS
BEGIN

declare @grupo varchar(13)
SET @grupo=
     CASE WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 0  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 4)  THEN '0 a 4 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 5  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 9)  THEN '05 a 9 años' 
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que clasifica a un paciente en un grupo etario quinquenal según su fecha de nacimiento y la fecha de atención. Calcula la edad del paciente al momento de la atención y retorna una etiqueta de rango de edad (por ejemplo ''0 a 4 años'', ''20 a 24 años'', ''80 y más años''). Se usa en reportes estadísticos y epidemiológicos — como el informe R248 — para agrupar atenciones o consultas por grupos de edad, siguiendo la clasificación estándar utilizada en salud pública y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EtariosR248';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EtariosR248';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica a una persona en un grupo etario quinquenal (de 0-4 hasta 80 y más) a partir de su fecha de nacimiento y una fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dos fechas deben ser válidas (datetime).; La fecha de atención debería ser posterior o igual a la de nacimiento; si la diferencia en años es negativa, el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula con DATEDIFF(YY, ...), que solo considera el cambio de año calendario, no la fecha exacta de cumpleaños.; Los grupos son quinquenales cerrados en ambos extremos hasta 79 años; el grupo abierto superior es ''80 y más años''.; El resultado siempre es una cadena varchar(13) o NULL.; La etiqueta ''05 a 9 años'' usa formato distinto (con cero a la izquierda) respecto al resto, pero es la salida garantizada para ese rango.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'grupo etario; fecha de nacimiento; fecha de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna una etiqueta de grupo etario quinquenal según DATEDIFF(YY, fecha_nac, fecha_atencion); si la edad es negativa o no cae en ningún rango, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si edad (DATEDIFF YY) entre 0 y 4 → ''0 a 4 años''; si edad entre 5 y 9 → ''05 a 9 años''; si edad entre 10 y 14 → ''10 a 14 años''; si edad entre 15 y 19 → ''15 a 19 años''; si edad entre 20 y 24 → ''20 a 24 años''; si edad entre 25 y 29 → ''25 a 29 años''; si edad entre 30 y 34 → ''30 a 34 años''; si edad entre 35 y 39 → ''35 a 39 años''; si edad entre 40 y 44 → ''40 a 44 años''; si edad entre 45 y 49 → ''45 a 49 años''; si edad entre 50 y 54 → ''50 a 54 años''; si edad entre 55 y 59 → ''55 a 59 años''; si edad entre 60 y 64 → ''60 a 64 años''; si edad entre 65 y 69 → ''65 a 69 años''; si edad entre 70 y 74 → ''70 a 74 años''; si edad entre 75 y 79 → ''75 a 79 años''; si edad >= 80 → ''80 y más años'' else NULL (cuando edad < 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EtariosR248';
GO
