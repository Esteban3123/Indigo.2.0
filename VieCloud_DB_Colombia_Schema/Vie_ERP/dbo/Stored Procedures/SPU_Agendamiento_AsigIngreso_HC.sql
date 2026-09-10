

CREATE PROCEDURE [dbo].[SPU_Agendamiento_AsigIngreso_HC] 
    @FechaInicial date,
    @FechaFinal date,
	@Especialidad char(3)
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
		LEFT OUTER JOIN HCINFPROM AS PM ON HC.NUMEFOLIO=PM.NUMEFOLIO AND PM.CODSERIPS=AM.CODSERIPS AND PM.IPCODPACI=CI.IPCODPACI
	WHERE CI.FECHORAIN >= @FechaInicial AND CI.FECHORAIN < @FechaFinal AND CI.CODESPECI=@Especialidad
	ORDER BY CI.FECHORAIN;
RETURN 0
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de agendamiento que consolida la información de citas médicas programadas con su historia clínica asociada, para un rango de fechas y una especialidad médica específica. Cruza las citas agendadas (AGASICITA) con la historia clínica del paciente (HCHISPACA), la conciliación o ingreso generado (ADCONCOED), la actividad médica programada (AGACTIMED), el catálogo de servicios CUPS/IPS (INCUPSIPS) y los hallazgos o procedimientos menores documentados (HCINFPROM). Retorna por cada cita: el folio de historia clínica, el tipo de HC, el estado del agendamiento (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada), si ya tiene ingreso asignado, el profesional que agendó y el que atendió, la especialidad, la actividad y el servicio IPS con su clasificación de dashboard (Laboratorio, Imágenes Diagnósticas, Consulta Externa, entre otros), y datos de cancelación u observaciones. Se usa principalmente para reportería y seguimiento de la relación entre citas agendadas y atenciones efectivamente registradas en la historia clínica, permitiendo identificar citas cumplidas sin historia clínica o sin ingreso generado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas agendadas en un rango de fechas y especialidad, indicando estado, si tienen ingreso asociado, datos de la atención de historia clínica, actividad agendada y clasificación de servicio para dashboard.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer rango de fechas (inicial y final) y un código de especialidad de 3 caracteres; Las tablas maestras INESPECIA y AGACTIMED deben tener registros consistentes con las citas (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas cuya fecha-hora de inicio esté en [FechaInicial, FechaFinal) y pertenezcan a la especialidad indicada; El cruce con historia clínica exige coincidencia de paciente, fecha (sin hora) y que la especialidad tratante sea igual a la especialidad de la cita; El procedimiento menor (HCINFPROM) se asocia solo si comparte folio, paciente y código de servicio IPS con la actividad agendada; Resultado ordenado cronológicamente por fecha-hora de la cita; Se eliminan duplicados con SELECT DISTINCT; Ingreso se considera ''Asignado'' únicamente cuando la cita tiene número de autorización vinculado a un consecutivo en ADCONCOED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Especialidad médica; Historia clínica; Paciente; Profesional de salud; Procedimiento menor; Servicio IPS (CUPS); Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Cancelación de cita; Actividad médica; Ingreso asignado vs no asignado; Dashboard de servicios (Laboratorio, Patología, Imágenes, Consulta Externa, Quimioterapia, Radioterapia, Diálisis, Procedimientos Qx/No Qx, Interconsultas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Cuando CI.FECHORAIN está en el rango y CI.CODESPECI coincide con la especialidad, retorna el conjunto de citas enriquecido con historia clínica, especialidad, actividad médica, servicio IPS y procedimiento menor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT (estado de la cita) en {0,1,2,3,4} → Se traduce a etiqueta de negocio: 0=Asignada, 1=Cumplida, 2=Incumplida, 3=PreAsignada, 4=Cancelada; si Existe correspondencia en ADCONCOED (C.CODCONCEC IS NOT NULL) → Se reporta Ingreso=''Asignado'' else Se reporta Ingreso=''NoAsignado''; si SERIPSDASH del servicio IPS en {1..11} → Se clasifica el servicio para dashboard: Laboratorio, Patología, Imágenes Diagnósticas, Consulta Externa, Quimioterapia, Radioterapia, Diálisis, Ninguno, Procedimiento No Qx, Procedimiento Qx, Interconsultas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCONCOED; dbo.INESPECIA; dbo.AGACTIMED; dbo.HCHISPACA; dbo.INCUPSIPS; dbo.HCINFPROM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_AsigIngreso_HC';
-- GO
