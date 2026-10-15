

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