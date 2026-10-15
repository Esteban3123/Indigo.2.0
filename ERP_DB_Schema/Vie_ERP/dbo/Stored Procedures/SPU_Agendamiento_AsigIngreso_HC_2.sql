
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