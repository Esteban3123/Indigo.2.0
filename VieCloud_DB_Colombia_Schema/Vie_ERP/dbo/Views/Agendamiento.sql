
CREATE VIEW [dbo].[Agendamiento]
AS
SELECT     A.CODAUTONU
FROM         dbo.AGASICITA AS A INNER JOIN
                      dbo.HCHISPACA AS B ON A.CODPROSAL = B.CODPROSAL AND A.IPCODPACI = B.IPCODPACI AND MONTH(A.FECHORAIN) = MONTH(B.FECHISPAC) AND 
                      DAY(A.FECHORAIN) = DAY(B.FECHISPAC) AND YEAR(A.FECHORAIN) = YEAR(B.FECHISPAC) AND A.CODESTCIT = '0'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que identifica las citas médicas agendadas que tienen una historia clínica registrada el mismo día, para el mismo paciente y profesional de salud. Cruza la tabla de citas (AGASICITA) con las historias clínicas (HCHISPACA) comparando paciente, profesional y fecha exacta de la cita. Solo considera citas en estado ''0'' (pendientes o activas sin atención confirmada). Sirve para detectar citas que ya cuentan con nota clínica generada, útil en auditoría de asistencia, control de agendamiento y consistencia entre agenda y atención real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Agendamiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Agendamiento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica las citas agendadas activas que ya cuentan con un registro de historia clínica del mismo paciente, profesional y fecha, devolviendo su número de autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una cita en AGASICITA con estado ''0''; Debe existir un registro en HCHISPACA cuyo paciente, profesional y fecha coincidan con los de la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas en estado ''0'' (pendiente/activa, no cancelada/atendida según codificación); Se requiere correspondencia entre la cita y un folio de historia clínica del mismo paciente, mismo profesional y misma fecha (día, mes y año); La coincidencia de fechas se hace por componentes (día, mes, año) ignorando la hora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cita médica; agendamiento; historia clínica; paciente; profesional de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Cuando la cita tiene CODESTCIT=''0'' y existe historia clínica con mismo profesional, paciente y fecha (día/mes/año), entonces se retorna su CODAUTONU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Agendamiento';
GO
