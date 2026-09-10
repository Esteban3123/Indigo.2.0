
CREATE PROCEDURE [dbo].[SPU_Agendamiento_AsigIngreso_HC_2]
    @FechaInicial date,
    @FechaFinal date
AS
    SELECT DISTINCT HC.NUMEFOLIO AS Folio,HC.IDETIPHIS AS TipoHC, PM.CODSERIPS AS CodProcedimientoMenor,HC.CODPROSAL AS ProfesionalAtendio, HC.CODESPTRA AS EspecialidadTratante,
		HC.FECHISPAC AS FechaAtencion, 
		CASE CI.CODESTCIT
				when 0 then  'Asignada'  
				when 1 then  'Cumplida' 
				when 2 then  'Incumplida' 
				when 3 then  'PreAsignada'
				when 4 then  'Cancelada'
		END as EstadoAgendamiento,
		CASE  WHEN C.CODCONCEC IS NOT NULL THEN 'Asignado' WHEN C.CODCONCEC IS NULL THEN 'NoAsignado' END Ingreso, 
		ES.DESESPECI AS EspecialidadAgendada, CI.IPCODPACI AS IdPaciente, CI.CODPROSAL AS ProfesionalAgenda, 
		CI.FECHORAIN AS FechaHoraAgenda, AM.DESACTMED AS ActividadAgendada, I.CODSERIPS AS CodActividadIPS, I.DESSERIPS AS DescripcionActividadIPS,
		CASE I.SERIPSDASH WHEN 1 THEN 'Laboratorio' WHEN 2 THEN 'Patología' WHEN 3 THEN 'Imágenes Diagnósticas' 
		WHEN 4 THEN 'Consulta Externa' WHEN 5 THEN 'Quimioterapia' WHEN 6 THEN 'Radioterapia' 
		WHEN 7 THEN 'Diálisis' WHEN 8 THEN 'Ninguno' WHEN 9 THEN 'Procedimiento No Qx' WHEN 10 THEN 'Procedimiento Qx' 
		WHEN 11 THEN 'Interconsultas' WHEN 7 THEN 'Otros Procedimientos' END AS Dashboard,
		CI.OBSERVACI AS ObservacionAgenda,
		CI.FECREGSIS AS FechaRegistroAgenda, CI.CODCAUCAN AS CodigoCancelacion, CI.OBSCAUCAN AS ObservacionCancelacion
	FROM AGASICITA AS CI LEFT OUTER JOIN ADCONCOED AS C ON CI.CODAUTONU=C.NUMCONCIT
		INNER JOIN INESPECIA AS ES ON CI.CODESPECI=ES.CODESPECI
		INNER JOIN AGACTIMED AS AM ON CI.CODACTMED=AM.CODACTMED
		LEFT OUTER JOIN HCHISPACA AS HC ON CI.IPCODPACI=HC.IPCODPACI AND CAST(CI.FECHORAIN AS DATE)=CAST(HC.FECHISPAC AS date) AND CI.CODESPECI=HC.CODESPTRA
		LEFT OUTER JOIN INCUPSIPS AS I ON AM.CODSERIPS = I.CODSERIPS
		LEFT OUTER JOIN HCINFPROM AS PM ON HC.NUMEFOLIO=PM.NUMEFOLIO AND PM.CODSERIPS=AM.CODSERIPS
	WHERE CI.FECHORAIN >= @FechaInicial AND CI.FECHORAIN < @FechaFinal
	ORDER BY CI.FECHORAIN;
RETURN 0
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consulta citas agendadas en un rango de fechas, cruzando la tabla de citas (`AGASICITA`) con la historia clínica del paciente (`HCHISPACA`), la especialidad, la actividad médica y los servicios IPS. Permite identificar si cada cita tiene o no ingreso asociado (`ADCONCOED`), el estado de la cita (Asignada, Cumplida, Incumplida, etc.) y el tipo de servicio (Dashboard). Está orientada a reportes de seguimiento y control de agendamiento versus atención efectiva.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta las citas agendadas en un rango de fechas mostrando estado de agendamiento, si tienen ingreso/historia clínica asociada y detalles de la actividad/especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere rango de fechas (inicial y final) para filtrar las citas por FECHORAIN.; Las tablas maestras de especialidades, actividades médicas y CUPS-IPS deben estar pobladas para resolver descripciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas cuyo FECHORAIN esté en el rango [@FechaInicial, @FechaFinal).; El cruce con historia clínica exige coincidencia de paciente, fecha de atención (a nivel día) y especialidad tratante igual a la especialidad de la cita.; El resultado es DISTINCT y se ordena cronológicamente por la fecha/hora de la cita.; Una cita puede aparecer sin historia clínica ni ingreso asociado (LEFT JOIN), pero siempre debe tener especialidad y actividad médica válidas (INNER JOIN).; El valor 7 en SERIPSDASH siempre se traduce como ''Diálisis'' (la rama ''Otros Procedimientos'' definida también con 7 es inalcanzable).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Ingreso del paciente; Historia clínica; Especialidad tratante; Actividad médica; Procedimiento menor; Códigos CUPS/IPS; Causa de cancelación; Clasificación de servicios para dashboard (Laboratorio, Patología, Imágenes, Consulta Externa, Quimioterapia, Radioterapia, Diálisis, Procedimientos Qx/No Qx, Interconsultas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Cuando CI.FECHORAIN está entre @FechaInicial (inclusive) y @FechaFinal (exclusive), se devuelve la cita con sus datos asociados de historia clínica, especialidad, actividad e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT (estado de la cita) → Traduce el código a etiqueta: 0=Asignada, 1=Cumplida, 2=Incumplida, 3=PreAsignada, 4=Cancelada.; si C.CODCONCEC IS NOT NULL (existe concepto/ingreso enlazado a la cita) → Marca Ingreso=''Asignado'' else Marca Ingreso=''NoAsignado''; si I.SERIPSDASH (clasificación de servicio para dashboard) → Traduce el código a categoría: 1=Laboratorio, 2=Patología, 3=Imágenes Diagnósticas, 4=Consulta Externa, 5=Quimioterapia, 6=Radioterapia, 7=Diálisis, 8=Ninguno, 9=Procedimiento No Qx, 10=Procedimiento Qx, 11=Interconsultas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCONCOED; dbo.INESPECIA; dbo.AGACTIMED; dbo.HCHISPACA; dbo.INCUPSIPS; dbo.HCINFPROM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC_2';
-- GO
