CREATE PROCEDURE [dbo].[SP_AGE_ListarPacienteOrdenesAutorizar]
(
	@CentroAtencion Char(10), 
	@Grupo varchar(500)
)
AS
BEGIN
	
	SET NOCOUNT ON;

    --QX Extramurales pendiente autorizar y sin radicacion de QX
	SELECT distinct convert(bit, 0) as 'Seleccionado',convert(bit, 0) as 'Principal',convert(bit, 1) as 'Visible',A.AUTO as ID, null as 'NumeroRadicacion' ,A.FECORDMED as 'FechaOrden',A.IPCODPACI, rtrim(ltrim(A.CODSERIPS)) as CODSERIPS,
		rtrim(A.IPCODPACI) as 'Numero de identificacion',
		rtrim (B.IPNOMCOMP) as 'Nombre paciente',
		rtrim (D.NOMMEDICO) as 'Profesional', rtrim(ltrim(A.NUMINGRES)) as 'Ingreso', rtrim(C.UFUDESCRI) as 'UnidadFuncional',  rtrim(F.DESESPECI) as 'Especialidad', CASE A.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta'  END AS PRIORIDAD, rtrim(A.CODSERIPS) + ' - ' + rtrim(S.DESSERIPS) + '. ' + isnull(CD.name,'') as 'Procedimiento', CASE A.SOLICITAMATOST WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS 'SolicitaMaterialesOsteosintesis', A.OTROSMATERIALES,rtrim(ltrim(A.CODCENATE)) as CODCENATE ,RTRIM(A.CODPROSAL) AS CODPROSAL,F.CODESPECI, ENT.code + ' - ' + ENT.name as 'Entidad','Hospitalario' as 'TipoRegistro',
		/*(select top 1 FECHISPAC  from HCHISPACA H with(nolock) inner join INESPECIA E on H.CODESPTRA = E.CODESPECI AND E.TIPESPECI = 4 where IPCODPACI = B.IPCODPACI  AND E.TIPESPECI = 4 order by FECHISPAC desc) */ '' as 'FechaConsultaPreanestesia',
		(select top 1 AutC.FECINFORM  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'FechaAutorizacion',
		(select top 1 AutC.CODCONCEC  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'NumeroAutorizacion' ,
		RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo', A.IDDESCRIPCIONRELACIONADA, rtrim(ltrim(A.NUMEFOLIO)) as  NUMEFOLIO, 1 as TipoProcedimiento, S.DESSERIPS , case TIPSERIPS when '5' then 'Quirúrgico' when '4' then 'No Quirúrgico' END as TipoServicio
		,'Autorización' as Autorizacion,
		BT.CODDIAGNO DiagnosisCode,
		rtrim(BT.CODDIAGNO) + ' - ' + rtrim (BT.NOMDIAGNO) DiagnosisCodeName, dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM HCORDPROQ A with(nolock) 
		INNER JOIN  INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI 
		INNER JOIN  INUNIFUNC C with(nolock) ON C.UFUCODIGO = A.UFUCODIGO 
		INNER JOIN  INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL 
		INNER JOIN  ADINGRESO E with(nolock) ON E.NUMINGRES = A.NUMINGRES 
		INNER JOIN  HCHISPACA his on his.NUMEFOLIO = A.NUMEFOLIO and his.IPCODPACI = a.IPCODPACI and his.NUMINGRES = A.NUMINGRES
		INNER JOIN  INESPECIA F with(nolock) ON F.CODESPECI = his.CODESPTRA ---E.CODESPTRA  // Se comenta hemtaoncologos para que tome la de la orden --> Juan Robayo
		INNER JOIN  INCUPSIPS S with(nolock) ON S.CODSERIPS = A.CODSERIPS 
		INNER JOIN [Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = E.GENCONENTITY 
		INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON S.CODSERIPS = GRUPO_D.CODSERIPS 
		INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		INNER JOIN Contract.CareGroup cg with(nolock) ON E.GENCAREGROUP = cg.Id
		INNER JOIN Contract.CUPSEntity ce with(nolock) ON A.CODSERIPS = ce.Code
		LEFT OUTER JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT OUTER JOIN contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
		LEFT OUTER JOIN dbo.INDIAGNOS BT with(nolock) on A.CODDIAGNO = BT.CODDIAGNO 
	WHERE 	/*A.SOLICITASALA = 1 AND*/ A.MANEXTPRO =1 AND A.ESTSERIPS = '1'  AND A.CODCENATE = @CentroAtencion
		AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) AND A.IDADRADICACIONQX IS NULL
		AND A.FECORDMED between  DATEADD(month,-3,Common.GETDATE()) AND Common.GETDATE()    		
   UNION ALL
   --NO QX Extramurales pendiente autorizar y sin radicacion de QX
 	SELECT distinct convert(bit, 0) as 'Seleccionado',convert(bit, 0) as 'Principal',convert(bit, 1) as 'Visible',A.AUTO as ID, null as 'NumeroRadicacion' ,A.FECORDMED as 'FechaOrden',A.IPCODPACI, rtrim(ltrim(A.CODSERIPS)) as CODSERIPS,
		rtrim(A.IPCODPACI) as 'Numero de identificacion',
		rtrim (B.IPNOMCOMP) as 'Nombre paciente',
		rtrim (D.NOMMEDICO) as 'Profesional', rtrim(ltrim(A.NUMINGRES)) as 'Ingreso', rtrim(C.UFUDESCRI) as 'UnidadFuncional',  rtrim(F.DESESPECI) as 'Especialidad', CASE A.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta'  END AS PRIORIDAD, rtrim(A.CODSERIPS) + ' - ' + rtrim(S.DESSERIPS) + '. ' + isnull(CD.name,'') as 'Procedimiento',  'No' AS 'SolicitaMaterialesOsteosintesis', '' as OTROSMATERIALES,rtrim(ltrim(A.CODCENATE)) as CODCENATE ,RTRIM(A.CODPROSAL) AS CODPROSAL,F.CODESPECI, ENT.code + ' - ' + ENT.name as 'Entidad','Hospitalario' as 'TipoRegistro',
		/*(select top 1 FECHISPAC  from HCHISPACA H with(nolock) inner join INESPECIA E on H.CODESPTRA = E.CODESPECI AND E.TIPESPECI = 4 where IPCODPACI = B.IPCODPACI  AND E.TIPESPECI = 4 order by FECHISPAC desc)*/ '' as 'FechaConsultaPreanestesia',
		(select top 1 AutC.FECINFORM  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'FechaAutorizacion',
		(select top 1 AutC.CODCONCEC  from ADAUTSERD AutD with(nolock) inner join ADAUTSERC AutC with(nolock) on AutD.CODCONCEC = AutC.CODCONCEC where AutD.IPCODPACI = A.IPCODPACI  AND AutD.NUMINGRES = A.NUMINGRES  AND AutD.NUMEFOLIO = A.NUMEFOLIO AND AutD.CODSERIPS = A.CODSERIPS) as 'NumeroAutorizacion' ,
		RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo', A.IDDESCRIPCIONRELACIONADA, rtrim(ltrim(A.NUMEFOLIO)) as  NUMEFOLIO, 2 as TipoProcedimiento, S.DESSERIPS , case TIPSERIPS when '5' then 'Quirúrgico' when '4' then 'No Quirúrgico' END as TipoServicio
		,'Autorización' as Autorizacion,
		BT.CODDIAGNO DiagnosisCode,
		rtrim(BT.CODDIAGNO) + ' - ' + rtrim (BT.NOMDIAGNO) DiagnosisCodeName, dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM HCORDPRON A with(nolock) 
		INNER JOIN INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI 
		INNER JOIN INUNIFUNC C with(nolock) ON C.UFUCODIGO = A.UFUCODIGO 
		INNER JOIN INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL 
		INNER JOIN  ADINGRESO E with(nolock) ON E.NUMINGRES = A.NUMINGRES 
		INNER JOIN  HCHISPACA his on his.NUMEFOLIO = A.NUMEFOLIO and his.IPCODPACI = a.IPCODPACI and his.NUMINGRES = A.NUMINGRES
		INNER JOIN INESPECIA F with(nolock) ON F.CODESPECI =  his.CODESPTRA ---E.CODESPTRA  // Se comenta hemtaoncologos para que tome la de la orden --> Juan Robayo
		INNER JOIN   INCUPSIPS S with(nolock) ON S.CODSERIPS = A.CODSERIPS INNER JOIN
		[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = E.GENCONENTITY 
		INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON S.CODSERIPS = GRUPO_D.CODSERIPS 
		INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
		INNER JOIN Contract.CareGroup cg with(nolock) ON E.GENCAREGROUP = cg.Id
		INNER JOIN Contract.CUPSEntity ce with(nolock) ON A.CODSERIPS = ce.Code
		LEFT OUTER JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT OUTER JOIN contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
		LEFT OUTER JOIN dbo.INDIAGNOS BT with(nolock) on A.CODDIAGNO = BT.CODDIAGNO 		
	WHERE A.MANEXTPRO =1 AND A.ESTSERIPS = '1'  AND A.CODCENATE = @CentroAtencion
		AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) AND A.IDADRADICACIONQX IS NULL
		AND A.FECORDMED between  DATEADD(month,-3,Common.GETDATE()) AND Common.GETDATE()  
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con órdenes de procedimientos (quirúrgicos y no quirúrgicos, hospitalarios y ambulatorios) pendientes de autorización ante la aseguradora o EPS, filtradas por centro de atención y grupo de servicios. Combina datos de órdenes médicas (HCORDPROQ), información del paciente (INPACIENT), ingreso/admisión (ADINGRESO), historia clínica (HCHISPACA), profesional tratante (INPROFSAL), unidad funcional (INUNIFUNC), especialidad (INESPECIA), código CUPS del servicio (INCUPSIPS) y entidad pagadora (HealthAdministrator) para presentar una vista consolidada de cada procedimiento pendiente. Retorna datos clave como identificación y nombre del paciente, número de ingreso, procedimiento (código CUPS y descripción), prioridad, especialidad, entidad responsable del pago, diagnóstico CIE-10, fecha de la orden, estado de autorización, alertas de factores de riesgo y escalas clínicas; se utiliza en el módulo de agendamiento quirúrgico y autorización de servicios extramurales para gestionar la aprobación de procedimientos ante aseguradoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista órdenes de procedimientos quirúrgicos y no quirúrgicos extramurales pendientes de autorización ante la aseguradora, sin radicación previa, generadas en los últimos 3 meses para un centro de atención y grupos específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir y coincidir con CODCENATE de las órdenes; El parámetro de grupos debe ser una cadena delimitada parseable por dbo.SplitString; Las órdenes deben estar marcadas como extramurales (MANEXTPRO=1) y en estado activo (ESTSERIPS=''1''); Las órdenes no deben tener radicación de QX previa (IDADRADICACIONQX IS NULL); Debe existir ingreso (ADINGRESO), paciente, profesional, unidad funcional, especialidad tratante, CUPS, entidad pagadora, grupo de investigación y CareGroup asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye órdenes extramurales pendientes (sin radicación QX) y activas; Limita la búsqueda a una ventana móvil de 3 meses hacia atrás desde la fecha actual del sistema (Common.GETDATE); El grupo asociado al CUPS debe pertenecer a los códigos de grupo enviados; El procedimiento debe pertenecer a un grupo de investigación configurado (HCGRUPINVD/HCGRUPINVC); Cada fila se enriquece con la última autorización registrada en ADAUTSERC/ADAUTSERD para el mismo paciente, ingreso, folio y servicio; Banderas de selección Seleccionado=0, Principal=0, Visible=1 por defecto; TipoRegistro siempre se reporta como ''Hospitalario'' y Autorizacion como ''Autorización''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden médica quirúrgica; Orden médica no quirúrgica; Procedimiento extramural; Autorización de servicios; Radicación quirúrgica; Aseguradora/Entidad pagadora; Centro de atención; Especialidad tratante; Unidad funcional; Profesional de la salud; Ingreso hospitalario; Prioridad clínica (Emergencia/Urgencia/Normal/Definir Conducta); CUPS; Diagnóstico CIE; Materiales de osteosíntesis; Grupo de investigación/servicios; Factores de riesgo y escalas; Consulta preanestésica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.MANEXTPRO=1 AND A.ESTSERIPS=''1'' AND A.CODCENATE=@CentroAtencion AND A.IDADRADICACIONQX IS NULL AND FECORDMED en últimos 3 meses, retorna las órdenes quirúrgicas (HCORDPROQ) marcadas con TipoProcedimiento=1 unidas con las no quirúrgicas (HCORDPRON) marcadas con TipoProcedimiento=2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.PRISERIPS = ''1'' | ''2'' | ''3'' | ''4'' → Etiqueta prioridad como Emergencia / Urgencia / Normal / Definir Conducta respectivamente; si TIPSERIPS = ''5'' vs ''4'' → Clasifica el servicio como ''Quirúrgico'' o ''No Quirúrgico''; si Origen es HCORDPROQ (quirúrgico) → Incluye SOLICITAMATOST (Si/No) y OTROSMATERIALES, con TipoProcedimiento=1 else Para HCORDPRON (no quirúrgico) fija SolicitaMaterialesOsteosintesis=''No'', OTROSMATERIALES vacío y TipoProcedimiento=2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.RiskFactorAlert; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPROQ; dbo.HCORDPRON; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.ADINGRESO; dbo.INESPECIA; dbo.INCUPSIPS; Contract.HealthAdministrator; dbo.HCGRUPINVD; dbo.HCGRUPINVC; Contract.CareGroup; Contract.CUPSEntity; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INDIAGNOS; dbo.ADAUTSERD; dbo.ADAUTSERC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteOrdenesAutorizar';
-- GO
