
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboardQuimioterapia]
(
@CentroAtencion varchar(500),
@PestanaConsultar INT,
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;

IF @PestanaConsultar = 1  --Pacientes con Ordenes Nuevas

			Select  Case A.MANEJOEXTERNO when 1 then 'Ambulatorio' when 0 then 'Hospitalario' end as 'Tipo Orden' ,A.ID,C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',[dbo].[EDAD] (B.IPFECNACI,[Common].[GETDATE]()) As 'Edad',
				 Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',
				 Rtrim(A.CODDIAGNO) +'-'+ Rtrim(S.NOMDIAGNO) As 'Diagnostico',Rtrim(P.UFUDESCRI) As 'Nombre Unidad Funcional',A.UFUCODIGO as 'Codigo Unidad Funcional',
				 A.NUMEFOLIO AS 'Folio', Rtrim(o.Description) As 'Nombre Esquema', A.CICLOS As 'Ciclos Ordenados', Rtrim(M.NOMMEDICO) as 'Medico Ordeno',
				 Rtrim(N.DESESPECI) As 'Especialidad Medico',
				 Rtrim(A.CODDIAGNO) as 'Codigo Diagnostico', 
				 CI.IDHCORDPRON,
				 CASE CI.Environment WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' WHEN 3 THEN 'Mixta (Ambulatorio y Hospitalario)' ELSE 'N/A' END Ambiente,
				'Autorizacion' as Autorizacion, O.TypeScheme
				,Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')) --	,Ingreso = dbo.IngresoOncologico(A.IPCODPACI) 
				,A.NUMINGRES AS IngresoOrdenMedica, --Este ingreso es el que esta marcado como quimioterapia
				A.ESTADO AS 'Estado Quimioterapia',
				case A.ESTADO when 1 then 'Orden Solicitada' when 2 then 'Esquema iniciado' end as 'Estado Quimioterapia Nombre',
				[dbo].[ValidateSchemaStatus](1,a.ID,'',0,'') AS 'EstadoEsquemaMod',
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 1) AS 'AlertaFactoresRiesgo', 
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 2) AS 'AlertaEscalas',
				(SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC) AS 'UltimoIngreso'
			FROM  [EHR].HCORDQUIMIO  A 
				INNER JOIN ehr.Schemes O with(nolock) ON O.Id  = A.SchemesId 
				INNER JOIN INPACIENT B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
				INNER JOIN ADINGRESO C with(nolock) on A.NUMINGRES = C.NUMINGRES
				INNER JOIN INENTIDAD ENT with(nolock) on ENT.CODENTIDA = C.CODENTIDA 
				INNER JOIN ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE 
				INNER JOIN INDIAGNOS S with(nolock) on S.CODDIAGNO = A.CODDIAGNO 
				INNER JOIN INUNIFUNC P with(nolock) on P.UFUCODIGO = A.UFUCODIGO 
				INNER JOIN INPROFSAL M with(nolock) on M.CODPROSAL = A.CODPROSAL
				INNER JOIN INESPECIA N with(nolock) on N.CODESPECI = A.CODESPECI 
				LEFT OUTER JOIN EHR.HCORDCICLOS CI WITH(NOLOCK) ON CI.IDHCORDQUIMIO = A.ID AND CI.CICLO = A.CICLOACTUAL --Agregado por Hector
		where A.ESTADO IN (1) and ORDENCONCITA = 0 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   --- 1.Solicitado sin cita
	

	ELSE IF @PestanaConsultar = 2     --Pacientes Con Esquemas 
		  Select 
				(select top 1 FECHAREGISTRO from EHR.HCORDCICLOS where CICLO = A.CICLOACTUAL and IDHCORDQUIMIO = a.ID) AS 'Fecha Registro',
				Case A.ORDENCONCITA when 1 then 'Con Esquema Iniciado' else 'Con Esquema, Sin Cita' end as 'Estado Esquema' ,A.ID,C.CODTIPPAC,B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',[dbo].[EDAD] (B.IPFECNACI,[Common].[GETDATE]()) As 'Edad',
				Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',
				Rtrim(A.CODDIAGNO) +'-'+ Rtrim(S.NOMDIAGNO) As 'Diagnostico',Rtrim(P.UFUDESCRI) As 'Nombre Unidad Funcional',A.UFUCODIGO as 'Codigo Unidad Funcional',
				A.NUMEFOLIO AS 'Folio', Rtrim(o.Description) As 'Nombre Esquema',
				A.CICLOS As 'Ciclos Ordenados', Rtrim(M.NOMMEDICO) as 'Medico Ordeno',Rtrim(N.DESESPECI) As 'Especialidad Medico',
				Rtrim(A.CODDIAGNO) as 'Codigo Diagnostico',NULL AS IDHCORDPRON, NULL Environment,
				'Autorizacion' as Autorizacion, O.TypeScheme
				,Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B'))--Ingreso = dbo.IngresoOncologico(A.IPCODPACI)
				,A.NUMINGRES AS IngresoOrdenMedica
				,A.CICLOS As 'Ciclos Ordenados', A.CICLOACTUAL As 'Ciclos Actual',A.ULTIMOCICLOAUTORIZADO as 'Ultimo Ciclo Autorizado',
				--Intervalo = concat('Ciclo ', CICLOACTUAL, ' de ' , CICLOS, ' Día: ', case when  EXISTS ( select Rtrim(Dia) from EHR.HCORDCICLOSD where CICLO = A.CICLOACTUAL and IDHCORDQUIMIO = a.ID and ESTADODIA = 2  ) then ( select top 1 Rtrim(Dia) from EHR.HCORDCICLOSD where CICLO = A.CICLOACTUAL and IDHCORDQUIMIO = a.ID and ESTADODIA = 2 order by DIA desc  )  ELSE 'Sin Administración'  end  ),
				Intervalo = concat('Ciclo ', CICLOACTUAL, ' de ' , CICLOS, ' Día: ', COALESCE(( select top 1 Rtrim(Dia) from EHR.HCORDCICLOSD where CICLO = A.CICLOACTUAL and IDHCORDQUIMIO = a.ID and ESTADODIA = 2 order by DIA desc),'Sin Administración')),
				A.ESTADO AS 'Estado Quimioterapia',
				case A.ESTADO when 1 then 'Orden Solicitada' when 2 then 'Esquema iniciado' end as 'Estado Quimioterapia Nombre',
				[dbo].[ValidateSchemaStatus](1,a.ID,'',0,'') AS 'EstadoEsquemaMod',
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 1) AS 'AlertaFactoresRiesgo', 
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 2) AS 'AlertaEscalas',
				(SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC) AS 'UltimoIngreso'
			FROM  [EHR].HCORDQUIMIO  A 
				INNER JOIN ehr.Schemes O with(nolock) ON O.Id  = A.SchemesId 
				INNER JOIN INPACIENT B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
				INNER JOIN ADINGRESO C with(nolock) on A.NUMINGRES = C.NUMINGRES
				INNER JOIN INENTIDAD ENT with(nolock) on ENT.CODENTIDA = C.CODENTIDA 
				INNER JOIN INDIAGNOS S with(nolock) on S.CODDIAGNO = A.CODDIAGNO 
				INNER JOIN INUNIFUNC P with(nolock) on P.UFUCODIGO = A.UFUCODIGO
				INNER JOIN INPROFSAL M with(nolock) on M.CODPROSAL = A.CODPROSAL
				INNER JOIN INESPECIA N with(nolock) on N.CODESPECI = A.CODESPECI  
				INNER JOIN ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE 
			where A.ESTADO IN (1,2) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND A.FECHAREGISTRO >= DATEADD(month,-30, Common.GETDATE()) ORDER BY A.FECHAREGISTRO DESC   --2.Solicitado con cita 
			
	ELSE IF @PestanaConsultar = 3 ---Pacientes Agendados

			SELECT A.CODAUTONU AS AUTO,A.TIPTRATAMIENTO,
			(CASE ConfirmationStatus WHEN 2 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS ConfirmationStatus, 
			FECHORAIN, FECHORAFI,A.IDEQUIPOTRA AS IDEQUIPO,E.CODEQUIPO as CODIGOEQUIPO,E.DESCREQUI as EQUIPO,S.CODCONCEC AS IDSALA, S.CODIGSALA as CODIGOSALA, Rtrim(S.DESCRIPSAL)  AS SALA , S.UFUCODIGO AS FunctionalUnit, A.CODACTMED, F.DESACTMED, A.CODESPECI, null AS ID, RTRIM(A.IPCODPACI) AS 'Identificacion',RTRIM(D.IPNOMCOMP) AS 'NombrePaciente',RTRIM(D.IPDIRECCI) AS IPDIRECCI ,RTRIM(D.IPTELEFON) AS TELEFONO ,RTRIM(D.IPTELMOVI) AS CELULAR,CODESTCIT AS ESTADO, CITAEXTRA, CAST(CONVERT(DATE, FECHORAIN, 108) AS varchar) AS FECHAINICIAL,
			RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO' ,rtrim(OS.Description) as 'Nombre Esquema', CODESTCIT , A.IDHCRADESQUEMAS, A.IDHCORDCICLOSD,CICLO.CICLO,CICLO.DIA, 'Ciclo: ' + Rtrim(CICLO.CICLO) + ' de ' + Rtrim(O.CICLOS) +  +' Día: '+ Rtrim(CICLO.DIA) as 'Intervalo',
			rtrim(P.NOMMEDICO) as Profesional,  rtrim(DI.NOMDIAGNO) as 'Diagnostico',C.CODCENATE AS AttentionCenter,C.NOMCENATE as 'Nombre Centro Atencion',[dbo].[EDAD] (D.IPFECNACI,[Common].[GETDATE]()) As 'Edad',
			O.FECHAREGISTRO AS 'Fecha orden',O.CICLOS AS 'Ciclos Ordenados',
			Rtrim(O.CODDIAGNO) as 'Codigo Diagnostico',NULL AS IDHCORDPRON, NULL AS Environment, 
			'' AS Autorizacion, OS.TypeScheme
			,Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')) --,Ingreso = dbo.IngresoOncologico(A.IPCODPACI) --Este ingreso es el que esta marcado como quimioterapia
			,A.NUMINGRES AS IngresoOrdenMedica
			,O.ESTADO AS 'Estado Quimioterapia',
			case o.ESTADO when 1 then 'Orden Solicitada' when 2 then 'Esquema iniciado' end as 'Estado Quimioterapia Nombre',
			dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 1) AS 'AlertaFactoresRiesgo', 
			dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 2) AS 'AlertaEscalas',
			(SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC) AS 'UltimoIngreso'
			FROM dbo.AGASICITA A with(nolock) INNER JOIN
				dbo.ADCENATEN C with(nolock) ON A.CODCENATE=C.CODCENATE INNER JOIN
				dbo.INPACIENT D with(nolock) ON A.IPCODPACI=D.IPCODPACI INNER JOIN
				dbo.AGACTIMED F with(nolock) ON A.CODACTMED=F.CODACTMED INNER JOIN
				dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA   INNER JOIN
				dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA  INNER JOIN
				dbo.SEGusuaru G with(nolock) ON A.CODUSUASI=G.CODUSUARI INNER JOIN
				ehr.HCORDCICLOSD CICLO with(nolock) on A.IDHCORDCICLOSD = CICLO.ID  INNER JOIN
				ehr.HCORDQUIMIO O with(nolock) on O.ID = CICLO.IDHCORDQUIMIO INNER JOIN
				ehr.Schemes OS with(nolock) on O.SchemesId = OS.Id INNER JOIN
				dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = O.CODDIAGNO  INNER JOIN
				dbo.INPROFSAL P with(nolock) on P.CODPROSAL = O.CODPROSAL 
				INNER JOIN dbo.HCFARMEPC HCF with(nolock) on A.CODAUTONU = HCF.IDCITA
			WHERE A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND format(A.FECHORAIN,'dd/MM/yyyy') = format(@FechaInicial,'dd/MM/yyyy') AND CODESTCIT IN(0) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =1 

END