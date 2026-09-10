
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Citas_en_Espera] 
	-- Add the parameters for the stored procedure here
	@Fecha_Inic Date,
	@Fecha_Fin Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT        
cast (AGC.FECREGSIS as date) AS Fecha_solicitud,
AGC.FECHACITA AS Fecha_Deseada,  
INPA.IPNOMCOMP AS Paciente, 
AGC.IPCODPACI AS Identificacion, 
INEN.NOMENTIDA AS Entidad, 
INPA.IPTELEFON AS Tel1, INPA.IPTELMOVI AS Tel2, INPA.IPDIRECCI AS Direccion, 
INES.DESESPECI AS Especialidad, 
AGAC.DESACTMED AS Acitividad,
INP.NOMMEDICO AS Médico, 
AGC.OBSERVACI AS Observaciones, 
AGC.CITAASIGN AS Se_asignó, '1' as cont,
CASE AGC.ESTADO 
WHEN 1 THEN 'En Espera'
WHEN 2 THEN 'Asignada'
WHEN 3 THEN 'Cancelada'
END AS 'ESTADO-CITA'
FROM            dbo.AGCITAESP AS AGC INNER JOIN
                         dbo.INPACIENT AS INPA ON AGC.IPCODPACI = INPA.IPCODPACI INNER JOIN
                         dbo.INESPECIA AS INES ON AGC.CODESPECI = INES.CODESPECI INNER JOIN
                         dbo.INENTIDAD AS INEN ON INPA.CODENTIDA = INEN.CODENTIDA INNER JOIN
                         dbo.AGACTIMED AS AGAC ON AGC.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
                         dbo.INPROFSAL AS INP ON AGC.CODPROSAL = INP.CODPROSAL
WHERE (AGC.FECREGSIS BETWEEN @Fecha_Inic AND @Fecha_Fin) ORDER BY AGC.FECREGSIS asc 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de citas en lista de espera registradas en un rango de fechas. Consolida información del paciente (nombre, cédula, dirección, teléfonos), su entidad aseguradora o EPS, la especialidad médica solicitada, la actividad o tipo de consulta, el profesional de salud asignado (si lo hay) y el estado actual de la solicitud (En Espera, Asignada o Cancelada). Se utiliza para hacer seguimiento y gestión operativa de las solicitudes de cita que aún no han sido confirmadas o que están pendientes de asignación de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Citas_en_Espera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Citas_en_Espera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las citas registradas en el módulo de citas en espera dentro de un rango de fechas de solicitud, con datos del paciente, entidad, especialidad, actividad, médico y estado actual de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas válido (fecha inicial y final) para filtrar por fecha de registro de la cita; Las tablas maestras de pacientes, especialidades, entidades y actividades médicas deben tener correspondencia con los registros de citas en espera', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas cuya fecha de registro en sistema (FECREGSIS) cae dentro del rango solicitado; Toda cita listada debe tener paciente, especialidad, entidad afiliadora y actividad médica relacionados (INNER JOIN); El profesional de salud es opcional: las citas sin médico asignado también se incluyen (LEFT JOIN sobre INPROFSAL); Los resultados se ordenan ascendentemente por fecha de registro de la cita; El estado de cita reportado se restringe a tres valores de negocio: En Espera, Asignada o Cancelada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica en espera; Paciente; Especialidad médica; Entidad/Aseguradora; Actividad médica; Profesional de la salud; Estado de cita (En Espera/Asignada/Cancelada); Asignación de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGCITAESP: Cuando AGC.FECREGSIS está entre @Fecha_Inic y @Fecha_Fin, se retorna un conjunto de resultados con los datos de la cita y su estado traducido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGC.ESTADO = 1 → Se etiqueta la cita como ''En Espera''; si AGC.ESTADO = 2 → Se etiqueta la cita como ''Asignada''; si AGC.ESTADO = 3 → Se etiqueta la cita como ''Cancelada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGCITAESP; dbo.INPACIENT; dbo.INESPECIA; dbo.INENTIDAD; dbo.AGACTIMED; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Citas_en_Espera';
-- GO
