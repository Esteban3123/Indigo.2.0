
CREATE PROCEDURE [dbo].[SPCH_ListarEstadoCamasFechaHoraEspecifica]( 
	@Centro varchar(20),
	@Unidad varchar(1000), --Vienen varias unidades funcionales!!
	@FechaEspecifica Datetime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @tablaUltimaTraza as table(ID INTEGER) 
	insert into @tablaUltimaTraza
		select 
		(SELECT TOP 1 TRA.ID  FROM CHCAMATRAZA TRA INNER JOIN CHCAMASHO CAMA ON CAMA.CODICAMAS = TRA.CODICAMAS AND CAMA.ESTADCAMA = TRA.ESTADOCAMA WHERE TRA.CODICAMAS = A.CODICAMAS and TRA.FECHACREACION <=@FechaEspecifica ORDER BY TRA.FECHACREACION DESC ) as codicamas
		from CHCAMASHO A 
		WHERE	
		A.CODCENATE = @Centro AND A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
			   
	SELECT 
		A.CODCENATE,
		RTRIM(B.NOMCENATE) AS CENTROATENCION,
		A.UFUCODIGO,
		RTRIM(C.UFUDESCRI) AS UNIDADFUN,
		A.CODICAMAS,
		RTRIM(E.DESCCAMAS) AS DescripcionCama,
		A.ESTADOCAMA AS ESTADOCAMA,
		A.FECHAINICIAL, 
		CASE 
		WHEN FECHAFINAL > @FechaEspecifica THEN NULL
			ELSE FECHAFINAL
			END FECHAFINAL,
		A.IPCODPACI,
		RTRIM(D.IPNOMCOMP) AS PACIENTE,
		D.CODENTIDA AS CodigoEntidad,
		RTRIM(J.NOMENTIDA) AS ENTIDAD,
		(SELECT TOP 1  RTRIM(L.Nombre) FROM CHTIPOSAISLAMIENTOS AS L LEFT OUTER JOIN CHREGAISL AS K with (nolock)  on K.CODAISLAM = L.Id  and K.CODICAMAS = A.CODICAMAS AND K.IPCODPACI =A.IPCODPACI AND  K.NUMINGRES = A.NUMINGRES WHERE k.FECINIAIS <=@FechaEspecifica ORDER BY K.FECINIAIS DESC ) AS AISLAMIENTO,
		CASE
		WHEN A.FECHAFINAL IS NULL THEN Q.CODDIAGNO 
			ELSE O.CODDIAGNO
			END AS CodigoDiagnostico,
		CASE
			WHEN A.FECHAFINAL IS NULL THEN RTRIM(Q.NOMDIAGNO)
			ELSE RTRIM(O.NOMDIAGNO)
		END AS DIAGNOSTICO,
		F.CODTIPBLO AS CodigoBloqueo,
		RTRIM(M.DESTIPBLO) AS TIPOBLOQUEO,
		H.CODPROSAL AS CodigoMedico, 
		RTRIM(H.NOMMEDICO) AS NombreMedico, 
		I.CODESPECI AS CodigoEspecialidad,
		RTRIM(I.DESESPECI) AS DescripcionEspecialidad	,'DE TRZA' AS ORIGEN
	
	FROM
		@tablaUltimaTraza TU inner join CHCAMATRAZA AS A with (nolock) on TU.ID = A.ID  
		INNER JOIN 	ADCENATEN AS B  with (nolock) ON A.CODCENATE = B.CODCENATE  
		INNER JOIN	INUNIFUNC AS C  with (nolock) ON A.UFUCODIGO = C.UFUCODIGO  
		INNER JOIN	CHCAMASHO AS E with (nolock) ON A.CODICAMAS = E.CODICAMAS  
		LEFT OUTER JOIN	INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI  
		LEFT OUTER JOIN	CHREGEBLO AS F with (nolock) ON A.IDCHREGEBLO = F.ID
		LEFT OUTER JOIN CHREGESTA AS G with (nolock) ON E.CODICAMAS = G.CODICAMAS
			AND (A.FECHAINICIAL = G.FECFINEST OR( ABS ( DATEDIFF( minute , G.FECINIEST,  A.FECHAINICIAL ) ) <= 5))
			AND A.IPCODPACI IS NOT NULL
			AND A.IPCODPACI = G.IPCODPACI
		LEFT OUTER JOIN INPROFSAL AS H with (nolock) ON G.CODPROSAL = H.CODPROSAL
		LEFT OUTER JOIN INESPECIA AS I with (nolock) ON G.CODESPECI = I.CODESPECI
		LEFT OUTER JOIN INENTIDAD AS J with (nolock) ON D.CODENTIDA = J.CODENTIDA
		LEFT OUTER JOIN CHTIPBLOQ AS M with (nolock) ON F.CODTIPBLO = M.CODTIPBLO
		LEFT OUTER JOIN ADINGRESO AS N with (nolock) ON G.NUMINGRES = N.NUMINGRES 
		LEFT OUTER JOIN INDIAGNOS AS O with (nolock) ON A.CODDIAGNO = O.CODDIAGNO
		LEFT OUTER JOIN INDIAGNOP AS P with (nolock) ON D.IPCODPACI = P.IPCODPACI 
			AND N.NUMINGRES = P.NUMINGRES 
			AND P.CODDIAPRI = 1
		LEFT OUTER JOIN INDIAGNOS AS Q with (nolock) ON P.CODDIAGNO = Q.CODDIAGNO
			   		
	WHERE	
		A.CODCENATE = @Centro
		AND A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
		and A.FECHAINICIAL <= @FechaEspecifica 

	UNION

	SELECT 
		tmp.[CODCENATE],
		tmp.[CENTROATENCION],
		tmp.[UFUCODIGO],
		rtrim(tmp.[UNIDADFUN]) UNIDADFUN,
		tmp.[CODICAMAS],
		rtrim(tmp.[DescripcionCama]) DescripcionCama,
		tmp.[ESTADOCAMA],
		NULL AS FECHAINICIAL,
		NULL AS FECHAFINAL,
		tmp.IPCODPACI,
		rtrim(p.IPNOMCOMP) PACIENTE,
		p.CODENTIDA [CodigoEntidad],
		rtrim(i.NOMENTIDA) [ENTIDAD],
		(SELECT TOP 1 RTRIM(L.Nombre) FROM CHTIPOSAISLAMIENTOS AS L LEFT OUTER JOIN CHREGAISL AS K with (nolock)  on K.CODAISLAM = L.Id  and K.CODICAMAS = TMP.CODICAMAS AND K.IPCODPACI =TMP.IPCODPACI AND  K.NUMINGRES = TMP.INGRESO WHERE k.FECINIAIS <=@FechaEspecifica ORDER BY K.FECINIAIS DESC  ) AS AISLAMIENTO,
		dp.coddiagno [CodigoDiagnostico],
		rtrim(DI.NOMDIAGNO) [DIAGNOSTICO],
		TMP.[CodigoBloqueo],
		Q.DESTIPBLO TIPOBLOQUEO,
		TMP.[CodigoMedico],
		RTRIM(S.NOMMEDICO) [NombreMedico],
		TMP.[CodigoEspecialidad],
		RTRIM(SP.DESESPECI) DescripcionEspecialidad, 'DE CAMA' AS ORIGEN
	FROM (	
				select 
					a.codcenate [CODCENATE],
					rtrim(b.nomcenate) as [CENTROATENCION],
					a.ufucodigo [UFUCODIGO],
					rtrim(c.ufudescri) as [UNIDADFUN],
					a.codicamas [CODICAMAS], 
					a.desccamas [DescripcionCama],
					a.estadcama [ESTADOCAMA],				
					(select ipcodpaci from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as IPCODPACI,
					(select NUMINGRES from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as INGRESO,
					(select CODPROSAL from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as [CodigoMedico],
					(select chregeblo.CODTIPBLO from chregeblo where CODICAMAS = A.CODICAMAS and chregeblo.FECFINBLO is null) as [CodigoBloqueo],
					(select CODESPECI from CHREGESTA where CODICAMAS = A.CODICAMAS and REGESTADO = 1) as [CodigoEspecialidad]
				from
					chcamasho as a with (nolock)
					inner join adcenaten as b with (nolock) on a.codcenate = b.codcenate
					inner join inunifunc as c with (nolock) on a.ufucodigo = c.ufucodigo	
				WHERE a.CODCENATE = @centro AND A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
			) as TMP 
		left join INPACIENT P with (nolock) on P.IPCODPACI =  TMP.IPCODPACI
		left join INENTIDAD i with (nolock) on p.CODENTIDA = i.CODENTIDA
		left join indiagnop dp with (nolock) on tmp.IPCODPACI = dp.IPCODPACI and dp.CODDIAPRI = 1 and dp.NUMINGRES = tmp.INGRESO
		LEFT JOIN INDIAGNOS DI WITH (NOLOCK) ON DP.CODDIAGNO = DI.CODDIAGNO
		LEFT JOIN CHTIPBLOQ Q WITH (NOLOCK) ON TMP.[CodigoBloqueo] = Q.CODTIPBLO
		LEFT JOIN INPROFSAL S WITH (NOLOCK) ON TMP.[CodigoMedico] = S.CODPROSAL
		LEFT JOIN INESPECIA SP WITH (NOLOCK) ON TMP.[CodigoEspecialidad] = SP.CODESPECI
		WHERE TMP.CODICAMAS NOT IN (SELECT CODICAMAS FROM CHCAMATRAZA A WHERE	
											A.CODCENATE = @Centro
											AND A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
											and A.FECHAINICIAL <= @FechaEspecifica )
END