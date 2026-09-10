

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

--DROP PROCEDURE dbo.SPU_Agendamiento

CREATE PROCEDURE [dbo].[SPU_Agendamiento]
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL AS DATETIME,
	@FECHA_FINAL AS DATETIME

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT        
	DATENAME(MONTH,AGA.FECHORAIN) AS Mes, dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumen, 
	INP.IPCODPACI AS Identificacion, INP.IPNOMCOMP AS  Nombre, AGA.FECHORAIN AS FechaConsulta, 
	case AGA.CODESTCIT when 0 then  'Asignada' when 1 then  'Cumplida' when 2 then  'Incumplida' 
	when 3 then  'PreAsignada' when 4 then  'Cita Cancelada' end as 'ESTADO-CITA',
	INE.NOMENTIDA AS Entidad,
	CASE AGA.CODTIPCIT WHEN 0 THEN 'Primera Vez' WHEN 1 THEN 'Control' WHEN 2 THEN 'PostOperatorio' 
	END AS Tipo,
	INPR.CODPROSAL AS CodMedico, INPR.NOMMEDICO AS NombreMedico, INES.DESESPECI AS Especialidad, 
	dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, 
	cast(INP.IPFECNACI as date)AS FNacimiento, dbo.Edad(CONVERT(varchar, INP.IPFECNACI, 105),
	CONVERT(varchar, AGA.FECHORAIN, 105))AS Edad, dbo.Etarios(INP.IPFECNACI,AGA.FECHORAIN)  AS GrupoEtario1,
	dbo.EtariosR248(INP.IPFECNACI,AGA.FECHORAIN)AS GrupoEtario2, dbo.TipoPaciente(IPTIPOPAC)  AS TipoRegimen,
	INP.IPDIRECCI AS Direccion, INMU.MUNNOMBRE AS Municipio, AGAC.DESACTMED AS Acitvidad
FROM dbo.INPACIENT AS INP INNER JOIN
	dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA INNER JOIN
    dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI INNER JOIN
    dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL INNER JOIN
    dbo.INESPECIA AS INES ON AGA.CODESPECI= INES.CODESPECI INNER JOIN
    dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI INNER JOIN
    dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD INNER JOIN
    dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
    dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI AND AGA.IPCODPACI = AGCI.IPCODPACI
WHERE (AGA.FECHORAIN BETWEEN @FECHA_INICIAL AND @FECHA_FINAL)
ORDER BY AGA.FECHORAIN ASC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de agendamiento de citas médicas para un rango de fechas dado. Consolida información del paciente (cédula, nombre, sexo, fecha de nacimiento, edad, grupo etario, dirección, municipio y régimen), la cita (fecha, estado, tipo de consulta y actividad médica), el profesional de salud asignado y la especialidad, junto con la entidad aseguradora o pagadora del paciente. Se usa para análisis de productividad, seguimiento de asistencia y auditoria de agendas médicas, combinando datos de pacientes, entidades, citas, profesionales, especialidades, actividades médicas y ubicaciones geográficas en un único resultado ordenado por fecha de consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de citas médicas agendadas dentro de un rango de fechas, con datos demográficos del paciente, profesional, especialidad, entidad pagadora y clasificación etaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido para filtrar las citas por FECHORAIN.; Las funciones escalares dbo.TipDocR256, dbo.SexoR256, dbo.Edad, dbo.Etarios, dbo.EtariosR248 y dbo.TipoPaciente deben existir y ser accesibles.; Cada cita debe tener paciente, profesional, especialidad, entidad, ubicación, municipio y actividad médica relacionados (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas cuya fecha/hora de inicio esté dentro del rango parametrizado.; Solo se reportan citas con paciente, entidad, profesional, especialidad, ubicación, municipio y actividad médica existentes (relación obligatoria).; La edad y el grupo etario se calculan en función de la fecha de la cita, no de la fecha actual.; Los códigos de estado de cita se restringen al dominio {0,1,2,3,4} y los de tipo de cita a {0,1,2}; valores fuera de rango se devuelven como NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Tipo de cita (Primera Vez/Control/PostOperatorio); Entidad pagadora; Profesional de salud; Especialidad médica; Actividad médica; Régimen del paciente; Grupo etario; Municipio de residencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AGA.FECHORAIN está entre el rango de fechas suministrado, se retorna una fila por cita con datos descriptivos decodificados y ordenados ascendentemente por fecha/hora de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT = 0 → Se etiqueta el estado de la cita como ''Asignada''.; si AGA.CODESTCIT = 1 → Se etiqueta el estado de la cita como ''Cumplida''.; si AGA.CODESTCIT = 2 → Se etiqueta el estado de la cita como ''Incumplida''.; si AGA.CODESTCIT = 3 → Se etiqueta el estado de la cita como ''PreAsignada''.; si AGA.CODESTCIT = 4 → Se etiqueta el estado de la cita como ''Cita Cancelada''.; si AGA.CODTIPCIT = 0 → Se clasifica el tipo de cita como ''Primera Vez''.; si AGA.CODTIPCIT = 1 → Se clasifica el tipo de cita como ''Control''.; si AGA.CODTIPCIT = 2 → Se clasifica el tipo de cita como ''PostOperatorio''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipDocR256; dbo.SexoR256; dbo.Edad; dbo.Etarios; dbo.EtariosR248; dbo.TipoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INENTIDAD; dbo.AGASICITA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.AGACTIMED; dbo.AGCITESPE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento';
-- GO
