
CREATE VIEW [dbo].[IND_AS_HC_PendientesAtenderGineco]
AS
SELECT        CASE a.codtippac WHEN '1' THEN 'Materna' END AS Tipo, A.IPFECLLEGA AS FechaHoraLlegada, A.IPCODPACI AS Identificacion, A.IPNOMCOMP AS Paciente, 
                         RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, E.NOMENTIDA AS Entidad, DATEDIFF(MINUTE,
                          A.IPFECLLEGA, GETDATE()) AS MinutosEspera
FROM            dbo.ADCONTURG AS A WITH (nolock) LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS t WITH (nolock) ON t.CODCONCEC = A.CODCONCEC INNER JOIN
                         dbo.INPACIENT AS P WITH (nolock) ON P.IPCODPACI = A.IPCODPACI INNER JOIN
                         dbo.INENTIDAD AS E WITH (nolock) ON E.CODENTIDA = A.CODENTIDA
WHERE        (A.CODCENATE = '001') AND (A.UFUCODIGO = 'N12') AND (A.IPFECLLEGA >= '19/09/2016 00:00:00') AND (A.CONESTADO = '1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de pacientes pendientes de atención en el servicio de Ginecología y Obstetricia (unidad funcional N12 del centro de atención 001) que se encuentran en sala de espera de urgencias y aún no han sido atendidos. Integra el control de convocatorias de urgencias (ADCONTURG), el triage (ADTRIAGEU), los datos maestros del paciente (INPACIENT) y la entidad aseguradora (INENTIDAD) para mostrar, por cada paciente en espera: el tipo de atención (Materna), la fecha y hora de llegada, la cédula o identificación, el nombre completo, la edad calculada, la entidad o EPS, y los minutos que lleva esperando desde su llegada hasta el momento de la consulta. Sirve como monitor en tiempo real para que el personal asistencial identifique cuánto tiempo llevan esperando las pacientes maternas y priorice su atención oportuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_PendientesAtenderGineco';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_PendientesAtenderGineco';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes pendientes por atender en la unidad de ginecología/maternidad de urgencias, mostrando su tiempo de espera, datos demográficos y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia del paciente en el maestro de pacientes; Existencia de la entidad asociada al contacto en el directorio de entidades; Los registros de contacto deben tener centro de atención, unidad funcional y estado poblados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan contactos del centro de atención ''001''; Solo se incluyen pacientes ubicados en la unidad funcional ''N12'' (ginecología/maternidad); Solo se consideran contactos en estado activo (''1''); Se excluyen llegadas anteriores al 19/09/2016; El tiempo de espera se calcula en minutos desde la llegada hasta el momento actual (GETDATE); La edad se calcula dinámicamente respecto a la fecha actual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; urgencias; triage; entidad/aseguradora; atención ginecológica; materna; tiempo de espera', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADCONTURG: Cuando CODCENATE=''001'' AND UFUCODIGO=''N12'' AND IPFECLLEGA>=''19/09/2016'' AND CONESTADO=''1'' → retorna fila con tipo, llegada, identificación, paciente, edad, entidad y minutos de espera', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si codtippac = ''1'' → Clasifica al paciente como tipo ''Materna'' else Devuelve NULL en el campo Tipo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_PendientesAtenderGineco';
GO
