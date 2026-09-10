

CREATE PROCEDURE [dbo].[USP_Agendamiento_CA]
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL AS DATETIME,
	@FECHA_FINAL AS DATETIME

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT       
	dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumen, INP.IPCODPACI AS Identificacion, INP.IPNOMCOMP AS  Nombre, 
	US.NOMUSUARI AS UsuarioAsigna, AGA.FECHORAIN AS FechaConsulta, DATENAME(MONTH, AGA.FECHORAIN) + '/' + CAST(YEAR(AGA.FECHORAIN) AS varchar) AS MesConsulta,
	DATENAME(YEAR, AGA.FECHORAIN) AS AñoConsulta,
	CASE AGA.CODESTCIT
		WHEN 0 THEN  'Asignada'  
		WHEN 1 THEN  'Cumplida' 
		WHEN 2 THEN  'Incumplida' 
		WHEN 3 THEN  'PreAsignada'
		WHEN 4 THEN  'Cita Cancelada'
	END AS EstadoCita,
	INE.NOMENTIDA AS Entidad,
	CASE AGA.CODTIPCIT
		WHEN 0 THEN 'Primera Vez' 
		WHEN 1 THEN 'Control' 
		WHEN 2 THEN 'PostOperatorio' 
	END AS Tipo,
	INPR.CODPROSAL AS CodMedico, INPR.NOMMEDICO AS NombreMedico, INES.DESESPECI AS Especialidad, 
	dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, CAST(INP.IPFECNACI as date)AS FNacimiento,
	dbo.Edad(CONVERT(varchar, INP.IPFECNACI, 105),CONVERT(varchar, AGA.FECHORAIN, 105))AS Edad,
	dbo.TipoPaciente(IPTIPOPAC)  AS TipoRegimen,
	INP.IPDIRECCI AS Direccion, INMU.MUNNOMBRE AS Municipio, AGAC.DESACTMED AS Actividad,
	US2.NOMUSUARI AS UsuarioCancela, CC.DESCAUCAN AS CausaCancelacion,
	AGA.OBSCAUCAN AS ObservacionCancelacion, [ContainerSP].dbo.Edad(CAST(INP.IPFECNACI as date),CAST(AGA.FECHORAIN as date)) AS Edad2,
	[ContainerSP].dbo.UnidadesEdad(CAST(INP.IPFECNACI as date),CAST(AGA.FECHORAIN as date)) AS UnidadesEdad2
FROM            dbo.INPACIENT AS INP INNER JOIN
	dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA INNER JOIN
	dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI INNER JOIN
	dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL INNER JOIN
	dbo.INESPECIA AS INES ON AGA.CODESPECI= INES.CODESPECI INNER JOIN
	dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI INNER JOIN
	dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD INNER JOIN
	dbo.SEGusuaru AS US ON AGA.CODUSUASI=US.CODUSUARI INNER JOIN 
	dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
	dbo.SEGusuaru AS US2 ON AGA.CANCELUSU = US2.CODUSUARI LEFT OUTER JOIN
	dbo.AGCAUCANC AS CC ON AGA.CODCAUCAN = CC.CODCAUCAN LEFT OUTER JOIN
	dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI AND AGA.IPCODPACI = AGCI.IPCODPACI
WHERE AGA.FECHORAIN >= @FECHA_INICIAL AND AGA.FECHORAIN < @FECHA_FINAL 
ORDER BY AGA.FECHORAIN;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de agendamiento de citas médicas para un rango de fechas. Consolida en una sola consulta los datos del paciente (cédula, nombre, tipo de documento, sexo, fecha de nacimiento, edad, dirección y municipio), la cita (fecha, mes, año, estado —asignada, cumplida, incumplida, pre-asignada o cancelada—, tipo de consulta —primera vez, control o postoperatorio—) y el profesional de la salud con su especialidad y actividad médica programada. Incorpora también la entidad/aseguradora del paciente, el usuario que asignó la cita, y en caso de cancelación, el usuario que canceló, la causa y la observación de cancelación. Se utiliza principalmente para reportería operativa y gerencial del módulo de agendamiento, permitiendo analizar la productividad de citas, las tasas de cumplimiento e incumplimiento, y los motivos de cancelación en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_Agendamiento_CA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_Agendamiento_CA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de citas médicas agendadas en un rango de fechas, con datos del paciente, profesional, especialidad, estado y, si aplica, información de cancelación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un rango de fechas (inicial y final) para filtrar las citas por su fecha/hora de atención.; Deben existir las funciones escalares dbo.TipDocR256, dbo.SexoR256, dbo.TipoPaciente, dbo.Edad y las funciones del servidor vinculado [ContainerSP].dbo.Edad y [ContainerSP].dbo.UnidadesEdad.; El paciente debe tener ubicación válida vinculada a un municipio y una entidad asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas es semiabierto: incluye @FECHA_INICIAL y excluye @FECHA_FINAL.; Solo se listan citas cuyo paciente tenga entidad, ubicación, municipio, profesional, especialidad, usuario asignador y actividad médica válidos (INNER JOIN obligatorio).; Los datos de cancelación (usuario que cancela y causa) son opcionales y solo aparecen si la cita tiene esa información (LEFT JOIN).; Los códigos de estado de cita se traducen siempre al mismo conjunto cerrado de descripciones (Asignada, Cumplida, Incumplida, PreAsignada, Cita Cancelada).; El reporte se entrega siempre ordenado cronológicamente por la fecha/hora de inicio de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Agendamiento; Estado de cita; Tipo de cita (Primera Vez, Control, PostOperatorio); Profesional de la salud / Médico; Especialidad médica; Entidad / Aseguradora; Régimen del paciente; Municipio / Ubicación; Actividad médica; Cancelación de cita y causa; Edad del paciente al momento de la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AGA.FECHORAIN está en el rango [@FECHA_INICIAL, @FECHA_FINAL), se retorna una fila por cita ordenada ascendentemente por FECHORAIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT = 0 → La cita se reporta como ''Asignada''.; si AGA.CODESTCIT = 1 → La cita se reporta como ''Cumplida''.; si AGA.CODESTCIT = 2 → La cita se reporta como ''Incumplida''.; si AGA.CODESTCIT = 3 → La cita se reporta como ''PreAsignada''.; si AGA.CODESTCIT = 4 → La cita se reporta como ''Cita Cancelada''.; si AGA.CODTIPCIT = 0 → Tipo de cita ''Primera Vez''.; si AGA.CODTIPCIT = 1 → Tipo de cita ''Control''.; si AGA.CODTIPCIT = 2 → Tipo de cita ''PostOperatorio''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipDocR256; dbo.SexoR256; dbo.TipoPaciente; dbo.Edad; ContainerSP.dbo.Edad; ContainerSP.dbo.UnidadesEdad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INENTIDAD; dbo.AGASICITA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.SEGusuaru; dbo.AGACTIMED; dbo.AGCAUCANC; dbo.AGCITESPE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_Agendamiento_CA';
-- GO
