

create FUNCTION [dbo].[Laboratorio] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Laboratorio varchar (max)
 SELECT @Laboratorio =  COALESCE(rtrim(@Laboratorio) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDLABO as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','6','4','7') 
RETURN @Laboratorio

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe un número de ingreso y devuelve la lista de exámenes de laboratorio clínico activos solicitados durante esa hospitalización o atención, concatenados en un solo texto separado por '';*''. Consulta las órdenes de laboratorio de la historia clínica (HCORDLABO) y obtiene el nombre descriptivo de cada examen cruzando con el catálogo de servicios CUPS/IPS (INCUPSIPS). Excluye órdenes en estados cancelados, anulados o cerrados (estados 3, 4, 6 y 7), devolviendo únicamente los exámenes vigentes o pendientes. Se usa típicamente para mostrar en pantalla o reportes el resumen de laboratorios ordenados en un ingreso específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Laboratorio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Laboratorio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una cadena concatenada con las descripciones de los exámenes de laboratorio ordenados en un ingreso, excluyendo los que están en estados finalizados o anulados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso en HCORDLABO con órdenes de laboratorio asociadas; Los códigos de servicio de las órdenes deben existir en el catálogo INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan exámenes cuyo estado no sea 3, 4, 6 ni 7; Las descripciones se separan con el delimitador '' ;*''; Si no hay órdenes válidas, retorna NULL (COALESCE inicial sobre variable no asignada); El resultado se trunca al tamaño del tipo de retorno nvarchar(1000)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio; Historia clínica; Catálogo CUPS/IPS; Ingreso del paciente; Estados de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna una cadena (nvarchar 1000) con las descripciones de servicios separadas por '' ;*'' para las órdenes del ingreso cuyo ESTSERIPS no esté en (''3'',''4'',''6'',''7'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS NOT IN (''3'',''6'',''4'',''7'') → Se incluye la orden de laboratorio en el resultado concatenado else Se excluye la orden del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDLABO; INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Laboratorio';
GO
