

CREATE VIEW [dbo].[AuditoriaCierreEnfermeria]
AS
SELECT     TOP (100) PERCENT A.IPCODPACI, A.NUMINGRES, C.UFUDESCRI, A.CODPROSAL, A.FECREGIST, A.NOTENFSUB, A.NOTENFOBJ, A.NOTENFANA, A.FECREGSIS, DATEDIFF(hour, A.FECREGIST, 
                      A.FECREGSIS) AS DiferenciaHoras, B.NOMMEDICO
FROM         dbo.HCCTRNOTE AS A INNER JOIN
                      dbo.INPROFSAL AS B ON A.CODPROSAL = B.CODPROSAL INNER JOIN
                      dbo.INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO
WHERE     (A.FECREGIST < DATEADD(hour, - 1, A.FECREGSIS)) AND (A.TITNOTENF = 'HISTORIA FINAL DE ENFERMERIA            ') AND (A.FECREGIST > '01/05/2012') AND (A.CODCENATE = '001')
ORDER BY DiferenciaHoras DESC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Auditoría de notas de cierre de enfermería que presentan demoras en su registro. Identifica los casos donde la Historia Final de Enfermería fue documentada con más de una hora de diferencia entre la fecha del evento clínico y la fecha de registro en el sistema, calculando esa brecha en horas. Combina las notas de enfermería (HCCTRNOTE) con el catálogo de profesionales de la salud (INPROFSAL) para obtener el nombre del enfermero o profesional responsable, y con las unidades funcionales (INUNIFUNC) para mostrar el servicio o área donde ocurrió. Sirve para control de calidad y auditoría clínica, permitiendo detectar registros tardíos de cierres de enfermería en hospitalización y verificar el cumplimiento en la oportunidad del diligenciamiento de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'AuditoriaCierreEnfermeria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'AuditoriaCierreEnfermeria';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica notas de cierre final de enfermería registradas con más de una hora de retraso respecto al momento real del sistema, para auditoría de oportunidad del registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las notas deben existir en la tabla de notas de enfermería con su profesional y unidad funcional asociados; El profesional debe estar registrado en el maestro de profesionales de salud; La unidad funcional debe existir en el catálogo de unidades funcionales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se auditan notas posteriores al 01/05/2012; Solo se consideran notas con título ''HISTORIA FINAL DE ENFERMERIA''; Solo se incluyen registros del centro de atención ''001''; La diferencia entre registro manual y registro del sistema debe superar una hora para ser reportada; Los resultados se ordenan por mayor diferencia de horas primero', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de enfermería; Historia final de enfermería; Profesional de la salud; Unidad funcional; Centro de atención; Ingreso hospitalario; Auditoría de oportunidad de registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCTRNOTE: Devuelve solo notas cuyo título sea ''HISTORIA FINAL DE ENFERMERIA'', con fecha de registro posterior al 01/05/2012, en centro de atención ''001'' y donde la fecha de registro manual sea anterior en más de una hora a la fecha de registro del sistema', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.FECREGIST < DATEADD(hour, -1, A.FECREGSIS) → Se incluye la nota en la auditoría por considerarse registrada con retraso mayor a una hora else Se excluye la nota del resultado; si A.TITNOTENF = ''HISTORIA FINAL DE ENFERMERIA'' → Se considera la nota como cierre final de enfermería auditable else Se descarta; si A.CODCENATE = ''001'' → Solo se auditan notas del centro de atención 001 else Se descartan', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRNOTE; dbo.INPROFSAL; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'AuditoriaCierreEnfermeria';
GO
