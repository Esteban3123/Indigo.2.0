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
	FROM HCORDPROQ A with(nolock) INNER JOIN 
		INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
		INUNIFUNC C with(nolock) ON C.UFUCODIGO = A.UFUCODIGO INNER JOIN  
		INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
		ADINGRESO E with(nolock) ON E.NUMINGRES = A.NUMINGRES INNER JOIN  
		INESPECIA F with(nolock) ON F.CODESPECI = E.CODESPTRA INNER JOIN  
		INCUPSIPS S with(nolock) ON S.CODSERIPS = A.CODSERIPS INNER JOIN
		[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = E.GENCONENTITY 
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
	FROM HCORDPRON A with(nolock) INNER JOIN 
		INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
		INUNIFUNC C with(nolock) ON C.UFUCODIGO = A.UFUCODIGO INNER JOIN  
		INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
		ADINGRESO E with(nolock) ON E.NUMINGRES = A.NUMINGRES INNER JOIN  
		INESPECIA F with(nolock) ON F.CODESPECI = E.CODESPTRA INNER JOIN  
		INCUPSIPS S with(nolock) ON S.CODSERIPS = A.CODSERIPS INNER JOIN
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