
create FUNCTION [dbo].[Terapias] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Terapias varchar (max)
 SELECT @Terapias =  COALESCE(rtrim(@Terapias) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDPRON as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('4') 
RETURN @Terapias

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un número de ingreso hospitalario, devuelve una lista concatenada (separada por '';*'') de todas las terapias y procedimientos ordenados para ese paciente durante su estancia. Consulta las órdenes médicas de procedimientos (HCORDPRON) y las cruza con el catálogo de servicios CUPS/IPS (INCUPSIPS) para obtener el nombre descriptivo de cada servicio. Excluye los servicios en estado ''4'' (generalmente cancelados o anulados), mostrando solo las terapias vigentes o ejecutadas. Se usa para presentar en resumen el conjunto de terapias activas de un ingreso, útil en visualización de historia clínica, reportes asistenciales y auditorías de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Terapias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Terapias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una cadena concatenada con las descripciones de las terapias/procedimientos ordenados a un paciente durante su ingreso, excluyendo los anulados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso indicado en HCORDPRON; Los códigos de servicio en HCORDPRON deben existir en el catálogo INCUPSIPS para obtener descripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye servicios con ESTSERIPS=''4''; Solo retorna descripciones de servicios cuyo NUMINGRES coincide con el ingreso de entrada; El primer elemento de la cadena no lleva separador inicial; los siguientes se separan con '' ;*''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapias; Procedimientos ordenados; Órdenes médicas; Servicios CUPS/IPS; Ingreso del paciente; Estado del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna nvarchar(1000) con las descripciones de servicio (DESSERIPS) concatenadas y separadas por '' ;*'' para todas las órdenes del ingreso cuyo ESTSERIPS no sea ''4''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = ''4'' → Se excluye el servicio del resultado (se considera cancelado/anulado) else Se incluye la descripción del servicio en la cadena concatenada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDPRON; INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Terapias';
GO
