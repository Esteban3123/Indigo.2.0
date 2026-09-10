CREATE VIEW [dbo].[HCVistaEspecialidadesInterconsultasOrdenes]
AS
SELECT DISTINCT 
                         B.ESTADO, D.CODCENATE, RTRIM(A.CODESPECI) AS Codigo, RTRIM(B.DESESPECI) AS Especialidad, RTRIM(A.CODSERINT) AS CodigoServicio, RTRIM(C.DESSERIPS) AS NombreServicio, RTRIM(A.CODESPECI) 
                         + ' - ' + RTRIM(B.DESESPECI) AS CodigoDescripcion, C.SERIPSDASH, C.TIPSERIPS, A.IDDESCRIPCIONRELACIONADA_INTER
FROM            dbo.HCESPSERU AS A INNER JOIN
                         dbo.INESPECIA AS B ON A.CODESPECI = B.CODESPECI INNER JOIN
                         dbo.INCUPSIPS AS C ON A.CODSERINT = C.CODSERIPS LEFT OUTER JOIN
                         dbo.HCTURNREC AS D ON D.CODESPECI = A.CODESPECI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las especialidades médicas disponibles para órdenes de interconsulta, cruzando el catálogo de especialidades (INESPECIA) con los servicios CUPS habilitados por especialidad (HCESPSERU) y su descripción oficial (INCUPSIPS). Para cada especialidad activa muestra el código y nombre de la especialidad, el código y nombre del servicio CUPS asociado a interconsulta, el centro de atención relacionado (obtenido de la tabla de turnos por especialidad HCTURNREC), e indicadores de clasificación del servicio (dashboard y tipo RIPS). Sirve como fuente de consulta para los módulos de historia clínica que necesitan presentar al médico qué especialidades y servicios están disponibles al generar una orden de interconsulta intrahospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de especialidades médicas con sus servicios CUPS asociados y el centro de atención donde se prestan, para soporte de interconsultas y órdenes en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada especialidad en HCESPSERU debe existir en el catálogo INESPECIA; Cada servicio en HCESPSERU debe existir en el catálogo INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen pares especialidad-servicio existentes en HCESPSERU (INNER JOIN con INESPECIA e INCUPSIPS); Se aplica RTRIM a códigos y descripciones para eliminar espacios en blanco a la derecha; Se concatena código y descripción de especialidad con separador '' - '' en el campo CodigoDescripcion; El centro de atención (CODCENATE) puede ser nulo cuando la especialidad no tiene turnos configurados (LEFT OUTER JOIN con HCTURNREC); Se eliminan duplicados con DISTINCT, lo que puede generar múltiples filas por especialidad si tiene turnos en varios centros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'especialidad médica; servicio CUPS/IPS; interconsulta; orden médica; centro de atención; turno por especialidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCVistaEspecialidadesInterconsultasOrdenes: Devuelve filas únicas (DISTINCT) combinando especialidad-servicio con su estado, descripción, tipo de servicio IPS y centro de atención asociado por turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESPSERU; dbo.INESPECIA; dbo.INCUPSIPS; dbo.HCTURNREC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaEspecialidadesInterconsultasOrdenes';
GO
