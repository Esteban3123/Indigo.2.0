CREATE PROCEDURE [dbo].[SPCH_ListarEstadoActualCamas]
@centroAtencion varchar(20),
@unidadFuncional varchar(1000)
as
begin
SELECT 
	tmp.[CODCENATE],
	tmp.[CENTROATENCION],
	tmp.[UFUCODIGO],
	rtrim(tmp.[UNIDADFUN]) UNIDADFUN,
	tmp.[CODICAMAS],
	rtrim(tmp.[DescripcionCama]) DescripcionCama,
	tmp.[ESTADOCAMA],
	tz.FECHAINICIAL,
	tmp.IPCODPACI,
	rtrim(p.IPNOMCOMP) PACIENTE,
	p.CODENTIDA [CodigoEntidad],
	rtrim(i.NOMENTIDA) [ENTIDAD],
	CH.Id [CodigoAislamiento],
	rtrim(CH.Nombre) [AISLAMIENTO],
	dp.coddiagno [CodigoDiagnostico],
	rtrim(DI.NOMDIAGNO) [DIAGNOSTICO],
	TMP.[CodigoBloqueo],
	Q.DESTIPBLO TIPOBLOQUEO,
	TMP.[CodigoMedico],
	RTRIM(S.NOMMEDICO) [NombreMedico],
	TMP.[CodigoEspecialidad],
	RTRIM(SP.DESESPECI) DescripcionEspecialidad
FROM (	
			select 
				a.codcenate [CODCENATE],
				rtrim(b.nomcenate) as [CENTROATENCION],
				a.ufucodigo [UFUCODIGO],
				rtrim(c.ufudescri) as [UNIDADFUN],
				a.codicamas [CODICAMAS], 
				a.desccamas [DescripcionCama],
				a.estadcama [ESTADOCAMA],				
				(select Top 1 ipcodpaci from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as IPCODPACI,
				(select Top 1 NUMINGRES from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as INGRESO,
				(select Top 1 CODPROSAL from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as [CodigoMedico],
				(select Top 1 CODESPECI from CHREGESTA  with (nolock) where CODICAMAS = A.CODICAMAS and REGESTADO = 1) as [CodigoEspecialidad],
				(select Top 1 chregeblo.CODTIPBLO from chregeblo  with (nolock) where CODICAMAS = A.CODICAMAS and chregeblo.FECFINBLO is null) as [CodigoBloqueo]
			from
				chcamasho as a with (nolock)
				inner join adcenaten as b with (nolock) on a.codcenate = b.codcenate
				inner join inunifunc as c with (nolock) on a.ufucodigo = c.ufucodigo	
			WHERE a.CODCENATE = @centroAtencion and a.ufucodigo IN (SELECT Value FROM dbo.splitstring(@unidadFuncional))
		) as TMP 
	left join INPACIENT P with (nolock) on P.IPCODPACI =  TMP.IPCODPACI
	left join INENTIDAD i with (nolock) on p.CODENTIDA = i.CODENTIDA
	LEFT OUTER JOIN CHREGAISL AS K with (nolock)  ON  TMP.[CODICAMAS] = K.CODICAMAS AND K.IPCODPACI = P.IPCODPACI and K.FECFINAIS is null
	LEFT JOIN CHTIPOSAISLAMIENTOS CH with (nolock) ON K.CODAISLAM = CH.Id
	left join indiagnop dp with (nolock) on tmp.IPCODPACI = dp.IPCODPACI and dp.CODDIAPRI = 1 and dp.NUMINGRES = tmp.INGRESO
	LEFT JOIN INDIAGNOS DI WITH (NOLOCK) ON DP.CODDIAGNO = DI.CODDIAGNO
	LEFT JOIN CHTIPBLOQ Q WITH (NOLOCK) ON TMP.[CodigoBloqueo] = Q.CODTIPBLO
	LEFT JOIN INPROFSAL S WITH (NOLOCK) ON TMP.[CodigoMedico] = S.CODPROSAL
	LEFT JOIN INESPECIA SP WITH (NOLOCK) ON TMP.[CodigoEspecialidad] = SP.CODESPECI
	left outer join CHCAMATRAZA tz with (nolock) on TMP.CODICAMAS = tz.codicamas and tz.FECHAFINAL is null 
where 	
	tmp.[UFUCODIGO] IN (SELECT Value FROM dbo.splitstring(@unidadFuncional))
order by
	tmp.[CODICAMAS]
end