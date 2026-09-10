
CREATE PROCEDURE [dbo].[SP_AD_ListarCitasMedicasNativo]
	@Paciente varchar(25),
	@CentroAtencion char(10)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;
		DECLARE 	@PacienteL varchar(25)= @Paciente,
					@CentroAtencionL char(10)= @CentroAtencion
 
    SELECT	CAST(0 AS BIT) AS Sel,
			A.CODAUTONU AS Codigo, 
			FECHORAIN AS FechaCita,
			(A.CODPROSAL) AS CodigoProfesional, 
			RTRIM(NOMMEDICO) AS Profesional , 
			C.CODIGONIT AS NitMedico, 
			CASE WHEN isnull(A.TIPSOLICITU,1) = 1 THEN RTRIM(B.CODIGOCON) WHEN isnull(A.TIPSOLICITU,1) = 2 or isnull(A.TIPSOLICITU,1) = 3 THEN RTRIM(S.CODCONCEC) END AS Consultorio,
			CASE A.CODTIPCIT WHEN 0 THEN 1 WHEN 1 THEN 2 WHEN 2 THEN 3  WHEN 3 THEN 4 END AS TipoCita,
			D.CODACTMED as CodigioActividadMedica,
			RTRIM(DESACTMED) AS ActividadMedica,
			CASE isnull(A.CODTIPCIT,'9') WHEN '0' THEN ISNULL(A.CODSERIPS,D.CODSERIPS) WHEN '1' THEN ISNULL(A.CODSERIPS, iif(D.CODSERIPSCONTROL is null or D.CODSERIPSCONTROL ='0',ISNULL(D.CODSERIPS, A.CODSERIPS),D.CODSERIPSCONTROL)) WHEN '9' THEN A.CODSERIPS ELSE D.CODSERIPS END as CodigoServicio,
			RTRIM(DESESPECI) AS Especialidad,
			RTRIM(F.DESSERIPS) AS Servicio,
			a.CODESPECI as CodigoEspecialidad,
			f.ARSCODIGO as AreaServicio,
			g.CODCENCOS as CentroCosto,
			f.TIPSERIPS AS Tipo, 
			ISNULL(A.GENGENERATESO, CAST(0 as bit)) AS GeneroOrdenServicio, 
			ISNULL(A.TIPSOLICITU,1) AS TipoSolicitud, --1 - Cita Medica, 2 - Apoyo Diagnostico; 3 - Tratamiento Especial
			CAST(ISNULL(IIF(ISNULL(A.TIPSOLICITU,1) = 2 AND ISNULL(F.SERIPSDASH, 0) IN (3, 12), IIF(AD.EXIGECONFCITA = 1, IIF(A.NUMINGRESCONFIRM IS NULL AND A.CONFIRMCITA = 1, 0, 1), 0), NULL), 0) AS BIT) RequiresConfirmAppointment,
			IIF(ISNULL(A.TIPSOLICITU,1) = 2 AND ISNULL(F.SERIPSDASH, 0) IN (3, 12), A.NUMINGRESCONFIRM, NULL) AS Ingreso,
			0 as OrigenCirugia,
			CAST(0 as bit) Principal,
			ISNULL(A.IDRIASCUPS, 0) RiasCupsId,
			ISNULL(rc.IDRIAS, 0) RiasId,
			CAST(D.PAQDIALISIS as bit) PaqDialisis,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cecd.ContractDescriptionId, 0) ContractDescriptionId,
			ISNULL(r.CODPRO + ' - ' + r.NOMBRE, '') RiasCodeName,
			ISNULL(cd.Code + ' - ' + cd.Name, '') DescriptionCodeName,
			CONCAT(ha.Code,' - ',ha.Name) HealthAdministrator,
			CAST(ISNULL(A.TIPSOLICITU, 1) AS TINYINT) ConsultationActivity,
			CASE WHEN isnull(A.TIPSOLICITU,1) = 1 THEN RTRIM(B.DESCRICON) WHEN isnull(A.TIPSOLICITU,1) = 2 or isnull(A.TIPSOLICITU,1) = 3 THEN RTRIM(S.DESCRIPSAL) END AS ConsultorioName
    FROM AGASICITA A with(nolock)
    JOIN AGACTIMED D with(nolock) ON A.CODACTMED=D.CODACTMED	
    LEFT JOIN AGCONSULT B with(nolock) ON A.CODCENATE=B.CODCENATE AND A.CODIGOCON=B.CODIGOCON 
    LEFT JOIN AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA
    LEFT JOIN INPROFSAL C with(nolock) ON A.CODPROSAL=C.CODPROSAL
    LEFT JOIN INESPECIA E with(nolock) ON A.CODESPECI=E.CODESPECI
    LEFT JOIN INCUPSIPS F with(nolock) ON CASE isnull(A.CODTIPCIT,'9') WHEN '0' THEN ISNULL(A.CODSERIPS,D.CODSERIPS) WHEN '1' THEN ISNULL(A.CODSERIPS, iif(D.CODSERIPSCONTROL is null or D.CODSERIPSCONTROL ='0',ISNULL(D.CODSERIPS, A.CODSERIPS),D.CODSERIPSCONTROL)) WHEN '9' THEN A.CODSERIPS ELSE D.CODSERIPS END =F.CODSERIPS
    LEFT JOIN INAREASER g with(nolock) on f.ARSCODIGO=g.ARSCODIGO
	LEFT JOIN RIASCUPS rc with(nolock) on rc.ID = a.IDRIASCUPS
	LEFT JOIN RIAS r with(nolock) on r.ID = rc.IDRIAS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Contract.HealthAdministrator ha ON a.GENCONENTITY = ha.Id
	LEFT JOIN
	(
		SELECT CODACTMED, CODSERIPS, MAX(EXIGECONFCITA + 0) EXIGECONFCITA
		FROM AGACTMEDD
		GROUP BY CODACTMED, CODSERIPS
	) AD ON AD.CODACTMED = D.CODACTMED AND AD.CODSERIPS = F.CODSERIPS
    WHERE cast(A.FECHORAIN as date) >= cast([Common].[GETDATE]() as date) AND A.IPCODPACI=@PacienteL AND A.CODCENATE = @CentroAtencionL
	AND (A.CODESTCIT ='0'  or A.CODESTCIT ='3')
 
UNION ALL
 
    SELECT	CAST(0 AS BIT) AS Sel,
			A.CODAUTONU as Codigo,
			FECHORAIN AS FechaCita,
			(F.CODPROSAL) AS CodigoProfesional, 
			RTRIM(F.NOMMEDICO) AS Profesional, 
			C.CODIGONIT AS NitMedico, 
			B.CODIGSALA AS Consultorio,
			null AS TipoCita,
			NULL as CodigioActividadMedica,
			NULL AS ActividadMedica,
			E.codserips as CodigoServicio,
			RTRIM(G.DESESPECI) AS Especialidad,
			RTRIM(E.DESSERIPS) AS Servicio,
			G.CODESPECI as CodigoEspecialidad,
			NULL as AreaServicio,
			NULL as CentroCosto,
			E.TIPSERIPS AS Tipo, 
			CAST(0 as bit) AS GeneroOrdenServicio, 
			null AS TipoSolicitud, 
			CAST(0 as bit) RequiresConfirmAppointment,
			A.NUMINGRES as Ingreso,
			1 as OrigenCirugia,
			CAST(A.PRINCIPAL as bit) Principal,
			0 RiasCupsId,
			0 RiasId,
			CAST(0 as bit) PaqDialisis,
			0 CUPSEntityContractDescriptionId,
			0 ContractDescriptionId,
			'' RiasCodeName,
			'' DescriptionCodeName,
			'' HealthAdministrator,
			cast(0 as TINYINT) ConsultationActivity,
			B.DESCRIPSAL AS ConsultorioName
    FROM AGEPROGQX A with(nolock)
    JOIN AGENSALAC B with(nolock) ON A.AGENSALAC = B.CODCONCEC 
    JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI
    JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
    JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
    JOIN INESPECIA G with(nolock) ON A.CODESPECI = G.CODESPECI 
    WHERE cast(A.FECHORAIN as date) >= cast([Common].[GETDATE]() as date) AND ORIGENQX = 1  AND A.CODESTPQX = 0 AND ORIGENQX = 1 AND ESTADOFARM = 2 AND A.IPCODPACI=@PacienteL AND A.CODCENATE =@CentroAtencionL 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las citas médicas vigentes (desde hoy en adelante) de un paciente en un centro de atención determinado, combinando dos fuentes: las citas del agendamiento convencional (consultas, apoyos diagnósticos y tratamientos especiales con estado pendiente o reprogramada) y los procedimientos quirúrgicos programados pendientes de farmacia. Para cada cita devuelve datos completos como fecha, profesional de salud, consultorio o sala, actividad médica, código de servicio CUPS, especialidad, área de servicio, centro de costo, información de RIAS, contrato, administradora de salud y si requiere confirmación de ingreso. Se usa principalmente en la historia clínica y el portal del paciente para mostrar la agenda de citas y cirugías próximas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas médicas vigentes (consultas, apoyos diagnósticos, tratamientos especiales) y las cirugías programadas pendientes de un paciente en un centro de atención, a partir de la fecha actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el centro de atención deben existir y coincidir con registros en AGASICITA o AGEPROGQX; La fecha/hora de inicio de la cita debe ser igual o posterior a la fecha actual obtenida de [Common].[GETDATE](); Para citas médicas: el estado de la cita (CODESTCIT) debe ser ''0'' o ''3''; Para cirugías: ORIGENQX=1, CODESTPQX=0 y ESTADOFARM=2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven citas/cirugías con fecha de inicio igual o futura respecto a hoy; Las citas médicas devueltas siempre tienen estado ''0'' o ''3'' (activas/pendientes); Las cirugías programadas devueltas siempre tienen ORIGENQX=1, CODESTPQX=0 y ESTADOFARM=2; El campo Sel siempre se devuelve en 0 (BIT); OrigenCirugia=0 para citas médicas y =1 para cirugías programadas; TipoSolicitud por defecto es 1 (Cita Médica) cuando no está definido; RequiresConfirmAppointment solo aplica a apoyos diagnósticos con SERIPSDASH 3 o 12; EXIGECONFCITA se consolida por (CODACTMED, CODSERIPS) tomando el máximo valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Apoyo diagnóstico; Tratamiento especial; Cirugía programada; Consultorio; Sala; Profesional de la salud; Especialidad; Servicio CUPS; Centro de atención; Centro de costo; Área de servicio; RIAS (Rutas Integrales de Atención en Salud); Contrato / Descripción de contrato; Administradora de salud (EPS); Confirmación de cita; Paquete de diálisis; Orden de servicio; Actividad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Cuando FECHORAIN >= hoy, IPCODPACI = paciente, CODCENATE = centro y CODESTCIT IN (''0'',''3''), se retorna la cita médica con sus datos asociados; [RETURN_RESULT] AGEPROGQX: Cuando FECHORAIN >= hoy, ORIGENQX=1, CODESTPQX=0, ESTADOFARM=2 y coinciden paciente y centro, se retorna la cirugía programada marcada con OrigenCirugia=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPSOLICITU = 1 (Cita Médica) → Consultorio se toma de AGCONSULT (CODIGOCON/DESCRICON) else Cuando TIPSOLICITU = 2 (Apoyo Diagnóstico) o 3 (Tratamiento Especial), el consultorio se toma de AGENSALAC (CODCONCEC/DESCRIPSAL); si CODTIPCIT = ''0'' → CodigoServicio = ISNULL(CODSERIPS de cita, CODSERIPS de actividad médica); si CODTIPCIT = ''1'' → CodigoServicio prioriza CODSERIPS de cita; si no existe, usa CODSERIPSCONTROL de actividad médica (cuando es válido) o CODSERIPS de actividad/cita; si CODTIPCIT = ''9'' o cita sin tipo → CodigoServicio = CODSERIPS de la cita; si TIPSOLICITU = 2 y SERIPSDASH IN (3,12) y EXIGECONFCITA = 1 → RequiresConfirmAppointment = 1 cuando NUMINGRESCONFIRM IS NULL y CONFIRMCITA = 1; en otro caso 0 else RequiresConfirmAppointment = 0; si CODTIPCIT IN (0,1,2,3) → TipoCita se mapea a 1,2,3,4 respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGACTIMED; dbo.AGCONSULT; dbo.AGENSALAC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INCUPSIPS; dbo.INAREASER; dbo.RIASCUPS; dbo.RIAS; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.HealthAdministrator; dbo.AGACTMEDD; dbo.AGEPROGQX; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCitasMedicasNativo';
-- GO
