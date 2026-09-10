
CREATE VIEW [dbo].[IND_ESTANCIA]
AS
SELECT DISTINCT 
                      TOP (100) PERCENT E.IPCODPACI AS Identificacion, I.NUMINGRES AS Ingreso, E.REGDIAEST AS DiasEstancia, U.UFUDESCRI AS UnidadFuncional, 
                      E.FECINIEST AS FechaInicialEstancia, E.FECFINEST AS FechaFinalEstancia
FROM         dbo.CHREGESTA AS E INNER JOIN
                      dbo.CHCAMASHO AS C ON C.CODICAMAS = E.CODICAMAS INNER JOIN
                      dbo.INUNIFUNC AS U ON U.UFUCODIGO = C.UFUCODIGO INNER JOIN
                      dbo.ADINGRESO AS I ON I.IPCODPACI = E.IPCODPACI
WHERE     (C.CODCENATE = '002') AND (E.FECINIEST >= '01/01/2012 00:00:00')
ORDER BY FechaInicialEstancia
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida información de estancias hospitalarias de pacientes del centro de atención 002, mostrando por cada episodio la identificación del paciente (cédula), el número de ingreso, la unidad funcional o servicio donde estuvo, la fecha de inicio y fin de la estancia, y la cantidad de días de estancia registrados. Integra el historial de estados del paciente (CHREGESTA) con el maestro de camas (CHCAMASHO), las unidades funcionales (INUNIFUNC) y los ingresos o admisiones (ADINGRESO) para obtener el contexto completo de cada hospitalización. Existe para apoyar indicadores y reportes de estancia hospitalaria, giro cama, ocupación por servicio y análisis de días de hospitalización desde enero de 2012 en adelante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_ESTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_ESTANCIA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Indicador que lista las estancias hospitalarias de pacientes en un centro de atención específico desde 2012, mostrando días de estancia, unidad funcional y fechas asociadas a cada ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las camas deben tener asignado el centro de atención ''002''; Las estancias deben tener fecha de inicio igual o posterior al 01/01/2012; Debe existir relación entre paciente, cama, unidad funcional e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen estancias en camas del centro de atención ''002''; Solo se consideran estancias desde el 01/01/2012 en adelante; Se eliminan duplicados mediante DISTINCT; El resultado se ordena por fecha inicial de estancia ascendente; El cruce con ingresos se hace únicamente por identificación del paciente (no por número de ingreso), lo que puede asociar estancias con múltiples ingresos del mismo paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estancia hospitalaria; paciente; ingreso; cama hospitalaria; unidad funcional; centro de atención; días de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CHREGESTA: Cuando C.CODCENATE=''002'' y E.FECINIEST>=''01/01/2012'', se retorna una fila por estancia con identificación del paciente, ingreso, días, unidad funcional y fechas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_ESTANCIA';
GO
