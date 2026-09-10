CREATE VIEW [dbo].[ViewAuditoryDetail]
AS
SELECT A.ID as ID,A.CODUSUCONS AS CODUSUCONS,A.CODPACQCON AS CODPACQCON, A.FECHCONSU AS FECHCONSU,A.NOMMAQCONS AS NOMMAQCONS,
A.IPMAQCONS AS IPMAQCONS,A.INGRESO AS INGRESO,A.FOLIOIN AS FOLIOIN,A.ANONIMATO AS ANONIMATO,A.UFUCODIGO AS UFUCODIGO,B.IPNOMCOMP AS IPNOMCOMP,
C.NOMUSUARI AS NOMUSUARI, C.DESCARUSU AS DESCARUSU
FROM dbo.HCAUDITORIA A WITH(NOLOCK)
INNER JOIN dbo.INPACIENT B WITH(NOLOCK) ON A.CODPACQCON=B.IPCODPACI
INNER JOIN dbo.SEGusuaru C WITH(NOLOCK) ON A.CODUSUCONS=C.CODUSUARI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de auditoría de accesos a historias clínicas: combina los registros de auditoría (HCAUDITORIA) con los datos del paciente consultado (nombre completo, cédula) y los datos del usuario que realizó la consulta (nombre, cargo/rol), permitiendo trazabilidad completa de quién accedió a qué historia clínica, cuándo, desde qué equipo y dirección IP. Sirve para reportes de control de acceso, cumplimiento normativo y supervisión de privacidad de información clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAuditoryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAuditoryDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de auditoría de accesos a historias clínicas enriqueciendo cada evento con el nombre del paciente consultado y los datos del usuario que realizó la consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de auditoría debe referenciar un paciente existente en el maestro de pacientes; Cada registro de auditoría debe referenciar un usuario existente en el módulo de seguridad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen eventos de auditoría que tengan paciente y usuario válidos en sus respectivos maestros (INNER JOIN); Lecturas se hacen con NOLOCK, permitiendo lecturas sucias para evitar bloqueos sobre las tablas operativas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría de historia clínica; Paciente; Usuario del sistema; Anonimato de acceso; Ingreso/Folio clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCAUDITORIA: Devuelve los eventos de auditoría unidos con datos del paciente y del usuario, omitiendo registros cuyo paciente o usuario no exista (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCAUDITORIA; dbo.INPACIENT; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAuditoryDetail';
GO
