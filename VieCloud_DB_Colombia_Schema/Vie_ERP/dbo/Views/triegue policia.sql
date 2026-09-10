
CREATE VIEW [dbo].[triegue policia]
AS
SELECT        TOP (100) PERCENT TRIAFECHA, CODENTIDA, IPCODPACI, NUMINGRES, TRIANUMER, TRIAGECLA
FROM            dbo.ADTRIAGEU
WHERE        (TRIAFECHA >= CONVERT(DATETIME, '2015-05-01 00:00:00', 102)) AND (CODENTIDA = '00057')
ORDER BY TRIAFECHA
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Filtra registros de triage de urgencias de la entidad con código `00057` a partir del 1 de mayo de 2015, retornando fecha, paciente, número de ingreso, número de triage y clasificación. Sirve como vista de consulta para reportes o auditorías relacionados con atenciones de urgencias vinculadas a una entidad específica, posiblemente una aseguradora o entidad de policía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los triages de urgencias realizados desde el 1 de mayo de 2015 para los pacientes vinculados a la entidad ''00057'' (Policía), ordenados cronológicamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en ADTRIAGEU con valoraciones de triage de urgencias; La entidad ''00057'' está definida como la entidad correspondiente a Policía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen triages cuya fecha sea igual o posterior al 1 de mayo de 2015; Solo se exponen triages cuya entidad responsable sea ''00057'' (Policía); Nunca devuelve triages de otras entidades aseguradoras', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Paciente; Ingreso; Entidad responsable (Policía); Clasificación de triage', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADTRIAGEU: Devuelve registros donde TRIAFECHA >= ''2015-05-01'' y CODENTIDA = ''00057'', ordenados por TRIAFECHA ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTRIAGEU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'triegue policia';
GO
