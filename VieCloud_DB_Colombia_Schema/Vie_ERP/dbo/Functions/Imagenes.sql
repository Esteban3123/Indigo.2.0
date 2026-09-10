

create FUNCTION [dbo].[Imagenes] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Imagenes varchar (max)
 SELECT @Imagenes =  COALESCE(rtrim(@Imagenes) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDIMAG as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','4','6','7') 
RETURN @Imagenes

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un número de ingreso hospitalario, devuelve la lista de estudios de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, entre otros) solicitados para ese ingreso, concatenados en una sola cadena de texto separada por punto y coma. Consulta las órdenes de imágenes de la historia clínica (HCORDIMAG) y cruza con el catálogo de servicios CUPS/IPS (INCUPSIPS) para obtener el nombre descriptivo de cada estudio, excluyendo aquellos en estados cancelados, anulados o no vigentes. Se utiliza para resumir rápidamente los exámenes de imagen activos de un paciente durante su estancia, siendo útil en reportes clínicos, resúmenes de atención y consultas del expediente del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Imagenes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Imagenes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una cadena concatenada con las descripciones de los servicios de imágenes diagnósticas solicitados a un ingreso, excluyendo aquellos en estados no vigentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir en la tabla de órdenes de imágenes (HCORDIMAG); Los códigos de servicio deben estar parametrizados en INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de imágenes en estados considerados vigentes (distintos de 3, 4, 6, 7); Las descripciones se concatenan usando '' ;*'' como separador; El primer elemento no lleva separador inicial gracias al COALESCE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso del paciente; Órdenes de imágenes diagnósticas; Servicios CUPS; Estado del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve nvarchar(1000) con descripciones de servicios separadas por '' ;*'' para órdenes cuyo ESTSERIPS no esté en (''3'',''4'',''6'',''7'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS IN (''3'',''4'',''6'',''7'') → Excluye la orden del resultado else Incluye la descripción del servicio en la cadena concatenada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDIMAG; INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Imagenes';
GO
