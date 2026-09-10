
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Control_Inacistencia]
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
dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumen, 
INP.IPCODPACI AS Identificacion, 
INP.IPNOMCOMP AS  Nombre,
AGA.FECHORAIN AS FechaConsulta, 
case AGA.CODESTCIT
when 0 then  'Asignada'  
when 1 then  'Cumplida' 
when 2 then  'Incumplida' 
when 3 then  'PreAsignada'
when 4 then  'Cita Cancelada'
end as 'ESTADO-CITA',
INE.NOMENTIDA AS Entidad,
CASE AGA.CODTIPCIT
WHEN 0 THEN 'Primera Vez' 
WHEN 1 THEN 'Control' 
WHEN 2 THEN 'PostOperatorio' 
END AS Tipo,
INPR.CODPROSAL AS CodMedico, 
INPR.NOMMEDICO AS NombreMedico,
INES.DESESPECI AS Especialidad, 
dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, 
cast(INP.IPFECNACI as date)AS FNacimiento,
 dbo.Edad(CONVERT(varchar, INP.IPFECNACI, 105),CONVERT(varchar, AGA.FECHORAIN, 105))AS Edad,
dbo.Etarios(INP.IPFECNACI,AGA.FECHORAIN)  AS GrupoEtario1,
dbo.EtariosR248(INP.IPFECNACI,AGA.FECHORAIN)AS GrupoEtario2,
dbo.TipoPaciente(IPTIPOPAC)  AS TipoRegimen,
--INP.IPPRINOMB AS 'PRIMER NOMBRE',
--INP.IPSEGNOMB AS 'SEGUNDO NOMBRE',
--INP.IPPRIAPEL AS 'PRIMER APELLIDO',
--INP.IPSEGAPEL AS 'SEGUNDO APELLIDO',

INP.IPDIRECCI AS Direccion,
INMU.MUNNOMBRE AS Municipio,
AGAC.DESACTMED AS Acitvidad
FROM            dbo.INPACIENT AS INP INNER JOIN
                         dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA INNER JOIN
                         dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI INNER JOIN
                         dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL INNER JOIN
                         dbo.INESPECIA AS INES ON AGA.CODESPECI= INES.CODESPECI INNER JOIN
                         dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI INNER JOIN
                         dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD INNER JOIN
                         dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
                         dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI AND AGA.IPCODPACI = AGCI.IPCODPACI
WHERE        (AGA.FECHORAIN BETWEEN @FECHA_INICIAL AND @FECHA_FINAL)
ORDER BY AGA.FECHORAIN ASC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de control de inasistencia a citas médicas para un rango de fechas dado. Consolida información del paciente (tipo de documento, identificación, nombre, sexo, fecha de nacimiento, edad, grupo etario, régimen, dirección y municipio), la cita agendada (fecha, estado —asignada, cumplida, incumplida, cancelada, preasignada—, tipo —primera vez, control, postoperatorio— y actividad médica), el profesional de salud asignado, la especialidad, y la entidad o aseguradora del paciente. Cruza las tablas de pacientes, entidades, agendamiento de citas, profesionales, especialidades, ubicación geográfica, actividades médicas y citas especializadas para generar un listado operativo y de auditoría que permite identificar pacientes que no asistieron o cumplieron sus citas en el período consultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Control_Inacistencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Control_Inacistencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de citas médicas en un rango de fechas, mostrando datos del paciente, profesional, especialidad, entidad, ubicación y estado/tipo de la cita para análisis de asistencia e inasistencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un rango válido de fechas (inicial y final).; Las tablas de catálogos (entidades, profesionales, especialidades, municipios, ubicaciones, actividades médicas) deben estar pobladas para que los INNER JOIN no eliminen registros.; El paciente debe existir en INPACIENT y tener entidad, ubicación y municipio asociados.; Las funciones escalares dbo.TipDocR256, dbo.SexoR256, dbo.Edad, dbo.Etarios, dbo.EtariosR248 y dbo.TipoPaciente deben existir.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo incluye citas cuya fecha/hora de inicio cae dentro del rango parametrizado.; Solo aparecen citas con paciente, entidad, profesional, especialidad, ubicación, municipio y actividad médica existentes (por uso de INNER JOIN).; La relación con AGCITESPE es opcional (LEFT JOIN) y no filtra resultados.; Cada fila clasifica edad y grupos etarios calculados a la fecha de la cita, no a la fecha actual.; El estado y el tipo de cita se traducen a etiquetas legibles fijas según códigos numéricos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Tipo de cita (Primera Vez, Control, PostOperatorio); Entidad/Aseguradora; Profesional de la salud; Especialidad médica; Actividad médica; Tipo de régimen del paciente; Grupo etario; Inasistencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AGA.FECHORAIN está entre @FECHA_INICIAL y @FECHA_FINAL, se devuelve el listado de citas con datos del paciente, médico, especialidad y estado, ordenado ascendentemente por fecha/hora de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT = 0 → Estado de cita se reporta como ''Asignada''.; si AGA.CODESTCIT = 1 → Estado de cita se reporta como ''Cumplida''.; si AGA.CODESTCIT = 2 → Estado de cita se reporta como ''Incumplida''.; si AGA.CODESTCIT = 3 → Estado de cita se reporta como ''PreAsignada''.; si AGA.CODESTCIT = 4 → Estado de cita se reporta como ''Cita Cancelada''.; si AGA.CODTIPCIT = 0 → Tipo de cita se reporta como ''Primera Vez''.; si AGA.CODTIPCIT = 1 → Tipo de cita se reporta como ''Control''.; si AGA.CODTIPCIT = 2 → Tipo de cita se reporta como ''PostOperatorio''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipDocR256; dbo.SexoR256; dbo.Edad; dbo.Etarios; dbo.EtariosR248; dbo.TipoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INENTIDAD; dbo.AGASICITA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.AGACTIMED; dbo.AGCITESPE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Control_Inacistencia';
-- GO
