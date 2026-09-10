

CREATE PROCEDURE [dbo].[SP_AGE_ListarPacienteGestionQX_SinProgramar]
(
	@CentroAtencion Char(10), 
	@Grupo varchar(max)
)
AS
BEGIN
	
	SET NOCOUNT ON;

--QX Hospitalarios
	SELECT convert(bit, 0) as 'Seleccionado',convert(bit, 0) as 'Principal',convert(bit, 1) as 'Visible',A.AUTO as ID, null as 'NumeroRadicacion' ,A.FECORDMED as 'FechaRadicado',A.IPCODPACI, A.CODSERIPS,
		rtrim(A.IPCODPACI) as 'Numero de identificacion',
		rtrim (B.IPNOMCOMP) as 'Nombre paciente',
		rtrim(A.CODPROSAL) + ' - ' + rtrim (D.NOMMEDICO) as 'Profesional', A.NUMINGRES as 'Ingreso', rtrim(C.UFUDESCRI) as 'UnidadFuncional',  rtrim(F.DESESPECI) as 'Especialidad', CASE A.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta'  END AS PRIORIDAD, rtrim(A.CODSERIPS) + ' - ' + rtrim(S.DESSERIPS)  as 'Procedimiento', CASE A.SOLICITAMATOST WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS 'SolicitaMaterialesOsteosintesis', A.OTROSMATERIALES,A.CODCENATE ,RTRIM(A.CODPROSAL) AS CODPROSAL,F.CODESPECI, ENT.code + ' - ' + ENT.name as 'Entidad','Hospitalario' as 'TipoRegistro',
		(select top 1 FECHISPAC  from HCHISPACA H with(nolock) inner join INESPECIA E on H.CODESPTRA = E.CODESPECI where IPCODPACI = B.IPCODPACI  AND E.TIPESPECI = 4 order by FECHISPAC desc) as 'FechaConsultaPreanestesia',
		(select top 1 AutC.FECINFORM  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'FechaAutorizacion',
		(select top 1 AutC.CODCONCEC  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'NumeroAutorizacion' ,
		RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo',  A.IDDESCRIPCIONRELACIONADA
		,'Autorización' AS Autorizacion
		,'HCORDPROQ' AS EntityName, B.GENCAREGROUP,
		DX.CODDIAGNO DiagnosisCode,rtrim(DX.CODDIAGNO) + ' - ' + rtrim(DX.NOMDIAGNO) DiagnosisCodeName,isnull(CD.name,'')  AS 'Descripción relacionada' ,
		rtrim(B.IPPRINOMB) as 'IPPRINOMB',
		rtrim(B.IPSEGNOMB) as 'IPSEGNOMB',
		rtrim(B.IPPRIAPEL) as 'IPPRIAPEL',
		rtrim(B.IPSEGAPEL) as 'IPSEGAPEL',dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM HCORDPROQ A with(nolock) INNER JOIN 
		INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
		INUNIFUNC C with(nolock) ON C.UFUCODIGO = A.UFUCODIGO INNER JOIN  
		INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
		ADINGRESO E with(nolock) ON E.NUMINGRES = A.NUMINGRES INNER JOIN  
		HCHISPACA H with(nolock) ON H.NUMINGRES = A.NUMINGRES AND H.NUMEFOLIO = A.NUMEFOLIO AND H.IPCODPACI = A.IPCODPACI  INNER JOIN 
		INESPECIA F with(nolock) ON F.CODESPECI = H.CODESPTRA INNER JOIN  
		INCUPSIPS S with(nolock) ON S.CODSERIPS = A.CODSERIPS INNER JOIN
		[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = E.GENCONENTITY 
		INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON S.CODSERIPS = GRUPO_D.CODSERIPS  AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
		INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) on CD.Id = CDD.ContractDescriptionId
	    LEFT OUTER JOIN INDIAGNOS DX WITH(NOLOCK) On A.CODDIAGNO = DX.CODDIAGNO 
	WHERE 	A.SOLICITASALA = 1 	AND A.ESTSERIPS = '1' 	AND A.CODCENATE = @CentroAtencion AND A.MANEXTPRO = 0
		AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo))

UNION ALL --QX y NO QX Ambulatorios (radicacion de cirugia)

	SELECT convert(bit, 0) as 'Seleccionado',convert(bit, 0) as 'Principal',convert(bit, 1) as 'Visible', A.ID as ID,A.NUMRADICACION as 'NumeroRadicacion' ,A.FECHAREGISTRO  as 'FechaRadicado', A.IPCODPACI, S.CODSERIPS,
		rtrim(A.IPCODPACI) as 'Numero de identificacion',
		rtrim (B.IPNOMCOMP) as 'Nombre paciente',
		rtrim(A.CODPROSAL) + ' - ' + rtrim (D.NOMMEDICO) as 'Profesional', null as 'Ingreso', 'AMBULATORIO' as 'UnidadFuncional', rtrim(F.DESESPECI) as 'Especialidad', CASE A.PRIORIDAD WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal'   END AS 'PRIORIDAD', rtrim(S.CODSERIPS) + ' - ' + rtrim(S.DESSERIPS) as 'Procedimiento', null as  'SolicitaMaterialesOsteosintesis', null as OTROSMATERIALES,A.CODCENATE ,RTRIM(A.CODPROSAL) AS CODPROSAL,F.CODESPECI, ENT.code + ' - ' + ENT.name as 'Entidad','Ambulatorio' as 'TipoRegistro',
		(select top 1 CAMPOFECHA  from ADRADICACIONQXD D with(nolock) where  D.IDRADICACIONQX = 4 AND TIPOCAMPO IN (2,3) AND CAMPOFECHA IS NOT NULL ) as 'FechaConsultaPreanestesia',
		(select top 1 CAMPOFECHA  from ADRADICACIONQXD D with(nolock) where  D.IDRADICACIONQX = 4 AND TIPOCAMPO = 1) as 'FechaAutorizacion',
		null as 'NumeroAutorizacion' ,
		RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo', A.IDDESCRIPCIONRELACIONADA
		,CASE WHEN (select top 1 CAMPOFECHA  from ADRADICACIONQXD D with(nolock) where  D.IDRADICACIONQX = A.ID AND D.TIPOCAMPO = 1) IS NULL THEN 'No requiere autorización'
		ELSE 'Autorizado'
		END AS Autorizacion
		,'ADRADICACIONQX' AS EntityName, B.GENCAREGROUP,
		DX.CODDIAGNO DiagnosisCode,rtrim(DX.CODDIAGNO) + ' - ' + rtrim(DX.NOMDIAGNO) DiagnosisCodeName,isnull(CD.name,'')  AS 'Descripción relacionada',
		rtrim(B.IPPRINOMB) as 'IPPRINOMB',
		rtrim(B.IPSEGNOMB) as 'IPSEGNOMB',
		rtrim(B.IPPRIAPEL) as 'IPPRIAPEL',
		rtrim(B.IPSEGAPEL) as 'IPSEGAPEL', dbo.[RiskFactorAlert](A.IPCODPACI, (SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC), 1) AS 'AlertaFactoresRiesgo',
		 dbo.[RiskFactorAlert](A.IPCODPACI, (SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC), 2) AS 'AlertaEscalas'
	FROM ADRADICACIONQX A with(nolock) INNER JOIN 
		INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
		INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
		INESPECIA F with(nolock) ON F.CODESPECI = A.CODESPECI INNER JOIN  
		INCUPSIPS S with(nolock) ON S.CODSERIPS = A.QXPRINCIPAL INNER JOIN
		[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = A.GENCONENTITY 
		INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON S.CODSERIPS = GRUPO_D.CODSERIPS 
		INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID  AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) on CD.Id = CDD.ContractDescriptionId
		LEFT OUTER JOIN Contract.CareGroup cg ON A.GENCAREGROUP = cg.Id
		LEFT OUTER JOIN Contract.CUPSEntity ce ON A.QXPRINCIPAL = ce.Code
		LEFT OUTER JOIN INDIAGNOS DX WITH(NOLOCK) On A.CODDIAGNO = DX.CODDIAGNO 
	WHERE A.ESTADO = 2 AND A.YAPROGRAMOQX =0 AND A.CODCENATE = @CentroAtencion 
		AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo))

UNION ALL 
	-- Procedimientos No QX Hospitalarios
	
	SELECT 
		convert(bit, 0) as 'Seleccionado',
		convert(bit, 0) as 'Principal',
		convert(bit, 1) as 'Visible',
		A.AUTO as ID,
		null as 'NumeroRadicacion' ,
		A.FECORDMED  as 'FechaRadicado',
		A.IPCODPACI, 
		G.CODSERIPS,
		rtrim(A.IPCODPACI) as 'Numero de identificacion',
		rtrim (B.IPNOMCOMP) as 'Nombre paciente',
		rtrim(A.CODPROSAL) + ' - ' + rtrim (F.NOMMEDICO) as 'Profesional',
		A.NUMINGRES as 'Ingreso',
		rtrim(E.UFUDESCRI)  as 'UnidadFuncional',
		rtrim(I.DESESPECI) as 'Especialidad', 
		CASE A.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutina' END AS 'PRIORIDAD',
		rtrim(G.CODSERIPS) + ' - ' + rtrim(G.DESSERIPS) as 'Procedimiento',
		null as  'SolicitaMaterialesOsteosintesis', 
		null as OTROSMATERIALES,
		A.CODCENATE ,
		RTRIM(A.CODPROSAL) AS CODPROSAL,
		I.CODESPECI,
		ENT.code + ' - ' + ENT.name as 'Entidad',
		CASE A.MANEXTPRO WHEN 0 THEN 'Hospitalario' WHEN 1 THEN 'Ambulatorio' END as 'TipoRegistro',
		(select top 1 FECHISPAC  from HCHISPACA H with(nolock) inner join INESPECIA F on H.CODESPTRA = F.CODESPECI where IPCODPACI = B.IPCODPACI  AND F.TIPESPECI = 4 order by FECHISPAC desc) as 'FechaConsultaPreanestesia',
		(select top 1 AutC.FECINFORM  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'FechaAutorizacion',
		(select top 1 AutC.CODCONCEC  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'NumeroAutorizacion' ,
		RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo', A.IDDESCRIPCIONRELACIONADA
		,'Autorización' AS Autorizacion
		,'HCORDPRON' AS EntityName, B.GENCAREGROUP,
	    DX.CODDIAGNO DiagnosisCode,rtrim(DX.CODDIAGNO) + ' - ' + rtrim(DX.NOMDIAGNO) DiagnosisCodeName,isnull(CD.name,'') AS 'Descripción relacionada',
		rtrim(B.IPPRINOMB) as 'IPPRINOMB',
		rtrim(B.IPSEGNOMB) as 'IPSEGNOMB',
		rtrim(B.IPPRIAPEL) as 'IPPRIAPEL',
		rtrim(B.IPSEGAPEL) as 'IPSEGAPEL', dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM 
		HCORDPRON AS A with(nolock)
		INNER JOIN INPACIENT AS B with(nolock) on A.IPCODPACI = B.IPCODPACI
		INNER JOIN ADINGRESO AS C with(nolock) on A.NUMINGRES = C.NUMINGRES 
		INNER JOIN ADCENATEN AS D with(nolock) ON A.CODCENATE = D.codcenate 
		INNER JOIN INUNIFUNC AS E with(nolock) ON A.UFUCODIGO = E.UFUCODIGO 
		INNER JOIN INPROFSAL AS F with(nolock) ON A.CODPROSAL = F.CODPROSAL
		INNER JOIN INCUPSIPS AS G with(nolock) ON A.CODSERIPS = G.CODSERIPS
		INNER JOIN HCHISPACA H with(nolock) ON H.NUMINGRES = A.NUMINGRES AND H.NUMEFOLIO = A.NUMEFOLIO AND H.IPCODPACI = A.IPCODPACI  
		INNER JOIN  INESPECIA AS I with(nolock) ON H.CODESPTRA = I.CODESPECI
		INNER JOIN [Contract].[HealthAdministrator] AS ENT with(nolock) on ENT.ID = C.GENCONENTITY 
		INNER JOIN HCGRUPINVD AS GRUPO_D with(nolock) ON G.CODSERIPS = GRUPO_D.CODSERIPS AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
		INNER JOIN HCGRUPINVC AS GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) on CD.Id = CDD.ContractDescriptionId
		LEFT OUTER JOIN INDIAGNOS DX WITH(NOLOCK) On A.CODDIAGNO = DX.CODDIAGNO 
	WHERE
	    A.CODCENATE = @CentroAtencion AND A.SOLICITASALA = 1 AND MANEXTPRO = 0 AND A.ESTSERIPS = '1' AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de programación quirúrgica (sin fecha de cirugía asignada) para un centro de atención y uno o varios grupos de procedimientos quirúrgicos. Combina dos universos mediante UNION ALL: pacientes hospitalizados con órdenes quirúrgicas activas registradas en la historia clínica (HCORDPROQ), y pacientes ambulatorios radicados en el módulo de cirugía ambulatoria (ADRADICACIONQX). Para cada paciente devuelve datos de identificación (cédula, nombre completo), el profesional que ordenó el procedimiento, el ingreso o episodio de atención, la unidad funcional, la especialidad médica, la prioridad (emergencia, urgencia, normal), el procedimiento CUPS, la entidad pagadora (EPS/aseguradora), la fecha de orden o radicación, el estado de autorización, la fecha de consulta preanestésica, el diagnóstico CIE-10, materiales de osteosíntesis solicitados, el grupo quirúrgico al que pertenece el procedimiento, alertas de factores de riesgo y escalas clínicas. Sirve como fuente de datos para el módulo de gestión y programación de sala de cirugías, permitiendo al equipo quirúrgico identificar qué pacientes están en lista de espera y priorizarlos antes de asignarles fecha de intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con procedimientos quirúrgicos y no quirúrgicos (hospitalarios y ambulatorios) pendientes de programación en sala, filtrados por centro de atención y grupos de invasividad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @Grupo debe ser una cadena parseable por dbo.SplitString que contenga códigos válidos de HCGRUPINVC; Debe existir el centro de atención indicado en @CentroAtencion; Para QX hospitalarios: la orden debe tener SOLICITASALA=1, ESTSERIPS=''1'' y MANEXTPRO=0; Para radicaciones ambulatorias: ESTADO=2 y YAPROGRAMOQX=0; Para procedimientos no QX hospitalarios: SOLICITASALA=1, ESTSERIPS=''1'' y MANEXTPRO=0; Cada procedimiento (CODSERIPS) debe estar asociado a un grupo de invasividad en HCGRUPINVD/HCGRUPINVC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen procedimientos que solicitan sala y aún no han sido programados (SOLICITASALA=1 en hospitalarios; YAPROGRAMOQX=0 en ambulatorios); Sólo se consideran servicios con estado activo ESTSERIPS=''1'' en órdenes hospitalarias; El filtrado por grupo de invasividad se realiza siempre contra HCGRUPINVC.CODIGO usando los valores parseados de @Grupo; El cruce con descripciones contractuales (CUPSEntityContractDescriptions) preserva las filas aunque no exista descripción relacionada (LEFT JOIN); La fecha de consulta preanestesia se obtiene de la última HCHISPACA del paciente cuya especialidad sea TIPESPECI=4; Para radicaciones ambulatorias, las alertas de factores de riesgo y escalas se calculan sobre el último ingreso del paciente con estado en ('''',''P'',''B''); Las tres ramas exponen un esquema de columnas idéntico (compatibilidad UNION ALL) identificando el origen mediante EntityName: HCORDPROQ, ADRADICACIONQX, HCORDPRON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Procedimiento quirúrgico (QX); Procedimiento no quirúrgico; Hospitalario vs Ambulatorio; Radicación de cirugía; Programación de sala de cirugía; Autorización de servicios; Consulta preanestésica; Prioridad clínica (Emergencia/Urgencia/Normal/Definir conducta); Materiales de osteosíntesis; Grupos de invasividad; Especialidad médica; Unidad funcional; Centro de atención; Entidad/administradora de salud; CUPS; Diagnóstico; Ingreso/admisión; Folio de historia clínica; Alertas de factores de riesgo y escalas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado consolidado (UNION ALL): Retorna tres conjuntos unidos: órdenes QX hospitalarias (HCORDPROQ), radicaciones QX/no-QX ambulatorias (ADRADICACIONQX) y órdenes de procedimientos no QX hospitalarios (HCORDPRON), todas pendientes de programación en sala', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRISERIPS en órdenes QX hospitalarias → Traduce 1=Emergencia, 2=Urgencia, 3=Normal, 4=Definir Conducta; si PRIORIDAD en radicaciones ambulatorias → Traduce 1=Emergencia, 2=Urgencia, 3=Normal; si PRISERIPS en órdenes no QX hospitalarias → Traduce 1=Urgente, 2=Rutina; si SOLICITAMATOST = 1 / 0 (sólo QX hospitalarios) → Marca ''Si''/''No'' la solicitud de materiales de osteosíntesis; si MANEXTPRO en HCORDPRON → 0=''Hospitalario'', 1=''Ambulatorio'' como TipoRegistro; si Para radicaciones ambulatorias: existe registro en ADRADICACIONQXD con TIPOCAMPO=1 → Marca ''Autorizado'' else Marca ''No requiere autorización''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPROQ; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INESPECIA; dbo.INCUPSIPS; Contract.HealthAdministrator; dbo.HCGRUPINVD; dbo.HCGRUPINVC; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INDIAGNOS; dbo.ADAUTSERD; dbo.ADAUTSERC; dbo.ADRADICACIONQX; dbo.ADRADICACIONQXD; Contract.CareGroup; Contract.CUPSEntity; dbo.HCORDPRON; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_SinProgramar';
-- GO
