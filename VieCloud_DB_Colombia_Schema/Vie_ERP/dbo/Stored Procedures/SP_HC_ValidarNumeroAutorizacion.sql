-- Stored Procedure

-- =============================================
-- Author:		<Sumit Sarkar>
-- Create date: <23 de Septiembre 2019>
-- Description:	<Description, Averiguar que si el paciente ya tiene el mismo numero de autorizacion con misma entidad>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ValidarNumeroAutorizacion]
(
@PacientId as Varchar(25), @NumeroAutorizacion as Varchar(30), @Entidad as Char(9)
)

AS
BEGIN
	SET NOCOUNT ON;
	select A.NUMAUTORI, C.IPCODPACI,CASE A.CODTIPCIT WHEN 0 THEN 'Primera vez' WHEN 1 THEN 'Control' WHEN 2 THEN 'Pos-operatorio' END AS TIPOCITA,
	D.DESACTMED AS ACTIVIDAD, A.FECHORAIN AS FECHAINICIO, E.NOMENTIDA, 'AGASICITA' AS TABLA  , Rtrim(c.IPNOMCOMP)  as 'Nombre Paciente'
	FROM AGASICITA A  
	INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI 
	INNER JOIN AGACTIMED D ON D.CODACTMED = A.CODACTMED INNER JOIN 
	INENTIDAD E ON E.CODENTIDA = C.CODENTIDA 
	INNER JOIN Contract.HealthAdministrator  HEL ON C.GENCONENTITY = HEL.Id 
	WHERE A.NUMAUTORI = @NumeroAutorizacion AND HEL.code = @Entidad
UNION
	select A.NUMAUTORI, C.IPCODPACI AS TIPOCITA, 'Cirugia' AS TIPOCITA, D.DESSERIPS AS ACTIVIDAD, A.FECHORAIN AS FECHAINICIO, E.NOMENTIDA AS NOMENTIDA, 
	'AGEPROGQX' AS TABLA  , Rtrim(c.IPNOMCOMP)  as 'Nombre Paciente'
	FROM AGEPROGQX A  
	INNER JOIN INPACIENT C ON (A.IPCODPACI = C.IPCODPACI) 
	INNER JOIN  INCUPSIPS D ON D.CODSERIPS = A.CODSERIPS 
	INNER JOIN  INENTIDAD E ON C.CODENTIDA = E.CODENTIDA
	INNER JOIN Contract.HealthAdministrator  HEL ON C.GENCONENTITY = HEL.Id 
	WHERE A.NUMAUTORI =@NumeroAutorizacion AND HEL.code = @Entidad
   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si un número de autorización ya está registrado para una entidad (EPS, aseguradora) específica, verificando duplicados tanto en citas médicas agendadas como en cirugías programadas. Recibe como parámetros el código del paciente, el número de autorización y el código de la entidad pagadora, y retorna los registros coincidentes con el tipo de atención (primera vez, control, pos-operatorio o cirugía), la actividad o procedimiento asociado, la fecha de inicio, el nombre de la entidad y el nombre del paciente. Cruza las tablas de agendamiento de citas (AGASICITA), programación quirúrgica (AGEPROGQX), información del paciente (INPACIENT), actividades médicas (AGACTIMED), servicios CUPS (INCUPSIPS), entidades (INENTIDAD) y administradoras de salud (HealthAdministrator). Se usa para prevenir el uso duplicado de un número de autorización de una aseguradora o EPS en los módulos de agendamiento y cirugías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si ya existe un número de autorización registrado para una entidad/administradora de salud determinada, tanto en citas asistenciales como en programaciones quirúrgicas, devolviendo el detalle del paciente y la actividad asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre el paciente (INPACIENT) y una administradora de salud en Contract.HealthAdministrator a través de GENCONENTITY.; El número de autorización y el código de entidad deben proporcionarse para poder filtrar coincidencias.; Las tablas maestras de actividad médica (AGACTIMED), servicios CUPS (INCUPSIPS) y entidades (INENTIDAD) deben tener los códigos referenciados por las citas/programaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan autorizaciones cuyo número coincide exactamente con el solicitado y cuya entidad (código de administradora de salud) corresponde a la indicada.; La validación cruza dos fuentes de autorización: citas asistenciales (AGASICITA) y programaciones quirúrgicas (AGEPROGQX), unificando resultados con UNION.; La entidad se valida contra el contrato vigente del paciente (GENCONENTITY) en Contract.HealthAdministrator, no contra la entidad histórica de INPACIENT.; El nombre del paciente se devuelve sin espacios finales (RTRIM).; Los registros duplicados exactos entre ambas fuentes se eliminan por efecto del UNION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización médica; Paciente; Entidad / Administradora de salud (EPS); Cita médica; Tipo de cita (Primera vez, Control, Pos-operatorio); Programación quirúrgica; Actividad médica; Servicio CUPS / IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AGASICITA.NUMAUTORI coincide con el número solicitado y el contrato del paciente apunta a una HealthAdministrator cuyo code coincide con la entidad → devuelve fila con tabla origen ''AGASICITA'' y el tipo de cita traducido (Primera vez/Control/Pos-operatorio).; [RETURN_RESULT] resultset: Cuando AGEPROGQX.NUMAUTORI coincide con el número solicitado y el contrato del paciente apunta a una HealthAdministrator cuyo code coincide con la entidad → devuelve fila con tabla origen ''AGEPROGQX'' y tipo de cita ''Cirugia''.; [RETURN_RESULT] resultset: Cuando no existe ninguna autorización coincidente en AGASICITA ni en AGEPROGQX para la entidad indicada → devuelve resultset vacío (indicando que la autorización no está duplicada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de cita en AGASICITA: CODTIPCIT = 0 → Se etiqueta como ''Primera vez''; si Tipo de cita en AGASICITA: CODTIPCIT = 1 → Se etiqueta como ''Control''; si Tipo de cita en AGASICITA: CODTIPCIT = 2 → Se etiqueta como ''Pos-operatorio''; si Origen de la autorización en AGEPROGQX (programación quirúrgica) → Se etiqueta el tipo de cita como ''Cirugia'' de forma fija', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.AGACTIMED; dbo.INENTIDAD; Contract.HealthAdministrator; dbo.AGEPROGQX; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidarNumeroAutorizacion';
-- GO
