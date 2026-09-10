
CREATE PROCEDURE [dbo].[SP_AD_ListarCitasMedicas]
(
@Paciente varchar(25),
@CentroAtencion char(10),
@VersionERP int
)
AS
BEGIN
	SET NOCOUNT ON;
	  IF @VersionERP=1
SELECT  CAST(0 AS BIT) AS Sel,A.CODAUTONU AS Codigo, FECHORAIN AS FechaCita, RTRIM(A.CODPROSAL) AS CodigoProfesional, 
RTRIM(NOMMEDICO) AS Profesional , RTRIM(DESCRICON) AS Consultorio,  
CASE CODTIPCIT WHEN 0 THEN 1 WHEN 1 THEN 2 WHEN 2 THEN 3  WHEN 3 THEN 4 END AS TipoCita,
--CASE CODTIPCIT WHEN 0 THEN 'Primera Vez' WHEN 1 THEN 'Control' WHEN 2 THEN 'Remisión' END AS TipoCita,
RTRIM(DESACTMED) AS ActividadMedica,D.CODSERIPS AS CodigoServicio,RTRIM(F.DESSERIPS) AS Servicio,a.CODESPECI as CodigoEspecialidad,
f.ARSCODIGO as AreaServicio,g.CODCENCOS as CentroCosto,f.TIPSERIPS AS Tipo, RTRIM(DESESPECI) AS Especialidad
FROM  dbo.AGASICITA A 
INNER JOIN  dbo.AGCONSULT B ON A.CODCENATE=B.CODCENATE AND A.CODIGOCON=B.CODIGOCON 
INNER JOIN  dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
INNER JOIN  dbo.AGACTIMED D ON A.CODACTMED=D.CODACTMED
INNER JOIN  DBO.INCUPSIPS F ON D.CODSERIPS=F.CODSERIPS
INNER JOIN  DBO.INAREASER g on f.ARSCODIGO=g.ARSCODIGO
INNER JOIN dbo.INESPECIA E ON A.CODESPECI=E.CODESPECI
WHERE A.IPCODPACI=@Paciente AND A.CODCENATE =@CentroAtencion AND  (A.CODESTCIT ='0'  or A.CODESTCIT ='3')  

ELSE

SELECT  CAST(0 AS BIT) AS Sel,A.CODAUTONU AS Codigo, FECHORAIN AS FechaCita, RTRIM(A.CODPROSAL) AS CodigoProfesional, 
RTRIM(NOMMEDICO) AS Profesional , RTRIM(DESCRICON) AS Consultorio,  
CASE CODTIPCIT WHEN 0 THEN 1 WHEN 1 THEN 2 WHEN 2 THEN 3  WHEN 3 THEN 4 END AS TipoCita,
--CASE CODTIPCIT WHEN 0 THEN 'Primera Vez' WHEN 1 THEN 'Control' WHEN 2 THEN 'Remisión' END AS TipoCita,
RTRIM(DESACTMED) AS ActividadMedica,D.CODSERIPS AS CodigoServicio,RTRIM(F.DESSERIPS) AS Servicio,a.CODESPECI as CodigoEspecialidad,
f.ARSCODIGO as AreaServicio,g.CODCENCOS as CentroCosto,f.TIPSERIPS AS Tipo, RTRIM(DESESPECI) AS Especialidad
FROM  dbo.AGASICITA A 
INNER JOIN  dbo.AGCONSULT B ON A.CODCENATE=B.CODCENATE AND A.CODIGOCON=B.CODIGOCON 
INNER JOIN  dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
INNER JOIN  dbo.AGACTIMED D ON A.CODACTMED=D.CODACTMED
INNER JOIN  DBO.INCUPSIPS F ON D.CODSERIPS=F.CODSERIPS
INNER JOIN  DBO.INAREASER g on f.ARSCODIGO=g.ARSCODIGO
INNER JOIN dbo.INESPECIA E ON A.CODESPECI=E.CODESPECI
WHERE A.IPCODPACI=@Paciente AND A.CODCENATE =@CentroAtencion AND  (A.CODESTCIT ='0'  or A.CODESTCIT ='3')   

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las citas médicas pendientes o confirmadas de un paciente en un centro de atención específico. Consulta el registro de citas (AGASICITA) y enriquece cada cita con el nombre del profesional de salud, el consultorio, el tipo de actividad médica, el código y descripción del servicio CUPS/IPS, el área de servicio, el centro de costo y la especialidad médica. Retorna información clave como fecha de la cita, profesional asignado, tipo de cita (primera vez, control, remisión, etc.) y la actividad médica programada, filtrando únicamente citas en estado pendiente o agendada. Se usa para mostrar al paciente o al operador administrativo las próximas consultas agendadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCitasMedicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCitasMedicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas médicas activas o reprogramadas de un paciente en un centro de atención, con datos del profesional, consultorio, actividad, servicio y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y centro de atención deben existir en AGASICITA; Las citas deben tener estado ''0'' o ''3'' (activas/reprogramadas) para ser listadas; Las relaciones de consultorio, profesional, actividad médica, servicio CUPS, área de servicio y especialidad deben estar correctamente referenciadas (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven citas con estado ''0'' o ''3'' (excluye canceladas u otros estados); El campo Sel siempre se devuelve en 0 (BIT) como indicador de selección por defecto; El TipoCita se normaliza desplazando el código original (+1): 0→1, 1→2, 2→3, 3→4; Las dos ramas de @VersionERP retornan exactamente la misma estructura y datos (no hay diferenciación efectiva); Solo lista citas que tengan integridad referencial completa con consultorio, profesional, actividad médica, CUPS, área de servicio y especialidad (por uso de INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Centro de atención; Profesional de la salud; Consultorio; Tipo de cita (Primera Vez, Control, Remisión); Actividad médica; Servicio CUPS/IPS; Área de servicio; Centro de costo; Especialidad médica; Estado de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Cuando CODESTCIT=''0'' o CODESTCIT=''3'' y coincide paciente y centro de atención, retorna el listado de citas con datos enriquecidos de profesional, consultorio, actividad, servicio y especialidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Ejecuta el SELECT de listado de citas (versión ERP 1) else Ejecuta el mismo SELECT de listado de citas (rama por defecto, idéntica a la versión 1); si CODTIPCIT IN (0,1,2,3) → Mapea el tipo de cita a 1, 2, 3 o 4 respectivamente en el campo TipoCita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGCONSULT; dbo.INPROFSAL; dbo.AGACTIMED; dbo.INCUPSIPS; dbo.INAREASER; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicas';
-- GO
