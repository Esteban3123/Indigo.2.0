
-- =============================================
-- Author:		Juan Patiño 
-- Create date: 29/02/2016
-- Description:	SP que lista los pacientes Que Tiene Ordenes Medicas.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesOrdenesMedicas]
(
@CentroAtencion Char(10) ,
@UFuncional char(20)  
)
AS
BEGIN
	SET NOCOUNT ON;

	   CREATE TABLE #Pacientes(
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[CodigoUnidadFuncional] [varchar](MAX) NULL,
			[CodigoTipoServicio] [varchar](MAX) NULL,
			[NombreTipoServicio] [varchar](MAX) NULL, --Si Es Laboratorio-Patologias,Imagenes
			[Identificacion] [varchar](MAX) NULL,
			[Ingreso] [varchar](MAX) NULL,
			[FechaSolicitud] [datetime] NULL,
			[CodigoServicio] [varchar](MAX) NULL,
			[NombreServicio] [varchar](MAX) NULL,
			[EstadoServicio] [varchar](MAX) NULL,
			[NombreEstado] [varchar](MAX) NULL,
			[CodigoEstado] [integer],
			[NumeroFolio] [integer],
			[CodigoEstadoServicio] [integer],
			[Autorizacion] [varchar](MAX) NULL,
			[EntityId] int not null,
			[Cantidad] [integer],
			[Cantidadrea] int,
			[Terapia] int,
			[CodigoMotivoMuestraNoConforme] [varchar](MAX) NULL,
			[DescripcionMotivoMuestraNoConforme] [varchar](MAX) NULL,
			[ServicioPBS]  [varchar](20) null
		)
		
	    --variable de tipo tabla		
		declare @TablaIngresosFiltro as table(Ingreso varchar(36))
		
	    INSERT INTO @TablaIngresosFiltro 
			SELECT  C.NUMINGRES as Ingreso --Pacientes Con Ordenes Medicas.
			FROM dbo.CHCAMASHO A with(nolock) 
				LEFT OUTER JOIN dbo.CHREGESTA C with(nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
				LEFT OUTER JOIN dbo.ADINGRESO I with(nolock) ON I.NUMINGRES = C.NUMINGRES 
			WHERE 
				A.CODCENATE=@CentroAtencion 
				AND A.UFUCODIGO = ISNULL(NULLIF(@UFuncional, ''), A.UFUCODIGO)
				AND ESTADCAMA ='2'
				AND IESTADOIN IN ('', 'P')
		UNION ALL
			SELECT A.NUMINGRES AS Ingreso --Pacientes Con Pendientes.
			FROM dbo.ADINGRESO A with(nolock) 
			WHERE 
				IESTADOIN IN ('', 'P')  
				AND A.CODCENATE=@CentroAtencion 
				AND A.UFUINGMED = ISNULL(NULLIF(@UFuncional, ''), A.UFUINGMED)
				AND UFUINGHOS IS NULL 
				AND (UFUEGRMED IS NULL or DESTINOPAC = 1 ) 
				AND DESTINOPAC IS NOT NULL

  If (select count(*) from @TablaIngresosFiltro ) > 0 BEGIN
		
  Insert Into #Pacientes([CodigoUnidadFuncional],[CodigoTipoServicio],[NombreTipoServicio],[Identificacion],[Ingreso],[FechaSolicitud],[CodigoServicio],[NombreServicio],[EstadoServicio],[NombreEstado],[CodigoEstado],[NumeroFolio],[CodigoEstadoServicio],[Autorizacion],[Cantidad], [Cantidadrea], [EntityId], [Terapia], [CodigoMotivoMuestraNoConforme], [DescripcionMotivoMuestraNoConforme],[ServicioPBS])
		
---Por Esos pacientes Pregunto Si tiene algun tipo de Orden Medicas 
Select DISTINCT A.UFUCODIGO,TIPSERIPS as TipoServicio,'Imagenes Diagnosticas' as NombreServicio,a.IPCODPACI,A.NUMINGRES,FECORDMED As FechaSolicitud,RTRIM(B.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) As Nombre,ESTSERIPS, 
		 Case ESTSERIPS when 1 then 'Solicitado' When 2 then 'Estudio Realizado' When 3 then 'Imagen Procesada' When 5 then 'Remitido' When 9 then 'Realizando' End As Estado, B.TIPSERIPS,A.NUMEFOLIO,ESTSERIPS,
		 Case WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) Then 'No requiere autorización' 
			when SS.PROESTADO = 1 then 'Pendiente Solicitud'
			when SS.PROESTADO = 2 then 'Solicitado'
			when SS.PROESTADO = 3 then 'Autorizado'
			when SS.PROESTADO = 4 then 'Anulado'
			when SS.PROESTADO = 5 then 'No autorizado'
			else 'Pendiente por Autorizar'
		end AS Autorizacion, a.CANSERIPS as Cantidad, 
		0  AS 'Cantidadrea'
					,A.AUTO EntityId, 0 as Terapia,
					'' As CodigoMotivoMuestraNoConforme, '' AS DescripcionMotivoMuestraNoConforme, CASE B.SERIPSPOS WHEN 1 then 'Si' When 0 then 'NO' end  'ServicioPBS'
			From HCORDIMAG  A with(nolock)
				INNER JOIN INCUPSIPS  B with(nolock) ON A.CODSERIPS = B.CODSERIPS 
				LEFT OUTER JOIN ADCONFSER S with(nolock) ON S.CODSERIPS = A.CODSERIPS	
				LEFT OUTER JOIN ADAUTOSER ss with(nolock) on SS.IPCODPACI = a.IPCODPACI AND SS.NUMINGRES =A.NUMINGRES AND SS.CODSERIPS = A.CODSERIPS and SS.NUMEFOLIO = A.NUMEFOLIO
				WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS in (1,2,3,5,9) AND A.NUMINGRES in (select Ingreso from @TablaIngresosFiltro) 
		
UNION ALL 
		
    	SELECT DISTINCT  C.UFUCODIGO,D.TIPSERIPS as TipoServicio,'Laboratorio' as NombreServicio,C.IPCODPACI,C.NUMINGRES,FECORDMED As FechaSolicitud,RTRIM(D.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) As Nombre,ESTSERIPS,
			 Case ESTSERIPS When 1 then 'Solicitado' When 2 then 'Muestra recolectada' When 3 then 'Pendiente interpretación' When 5 then 'Estudio remitido' When 9 then 'Muestra no conforme' End As Estado ,D.TIPSERIPS,C.NUMEFOLIO,ESTSERIPS, 
			 Case WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) Then 'No requiere autorización' 
			when SS.PROESTADO = 1 then 'Pendiente Solicitud'
			when SS.PROESTADO = 2 then 'Solicitado'
			when SS.PROESTADO = 3 then 'Autorizado'
			when SS.PROESTADO = 4 then 'Anulado'
			when SS.PROESTADO = 5 then 'No autorizado'
			else 'Pendiente por Autorizar'
		end AS Autorizacion, c.CANSERIPS as Cantidad,
		0 AS 'Cantidadrea'
					 ,C.AUTO EntityId, 0 as Terapia,
                     ISNULL(C.CODMOTIVOMUESTRANOCONFORME,'') As CodigoMotivoMuestraNoConforme,
                     ISNULL(RTRIM(MA.DESMOTANU),'') As DescripcionMotivoMuestraNoConforme, CASE D.SERIPSPOS WHEN 1 then 'Si' When 0 then 'NO' end  'ServicioPBS'
		FROM HCORDLABO C with(nolock)
				INNER JOIN INCUPSIPS  D with(nolock) ON C.CODSERIPS = D.CODSERIPS 
				LEFT OUTER JOIN ADCONFSER S with(nolock) ON S.CODSERIPS = C.CODSERIPS 
				LEFT OUTER JOIN ADAUTOSER ss with(nolock) on SS.IPCODPACI = C.IPCODPACI AND SS.NUMINGRES =C.NUMINGRES AND SS.CODSERIPS = C.CODSERIPS and SS.NUMEFOLIO = C.NUMEFOLIO
				LEFT JOIN HCMOANULB MA ON MA.CODMOTANU = C.CODMOTIVOMUESTRANOCONFORME
				WHERE C.CODCENATE=@CentroAtencion AND ESTSERIPS in (1,2,3,5,9) AND C.NUMINGRES in (select Ingreso from @TablaIngresosFiltro)
		
UNION ALL
		
		Select DISTINCT E.UFUCODIGO,TIPSERIPS as TipoServicio,'Interconsultas' as NombreServicio,E.IPCODPACI,E.NUMINGRES,FECORDMED As FechaSolicitud,RTRIM(F.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) As Nombre,ESTSERIPS, 
		    Case ESTSERIPS when 1 then 'Sin Realizar' When 2 then 'Sin Realizar'  End As Estado ,TIPSERIPS,E.NUMEFOLIO,ESTSERIPS,
			Case WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) Then 'No requiere autorización' 
			when SS.PROESTADO = 1 then 'Pendiente Solicitud'
			when SS.PROESTADO = 2 then 'Solicitado'
			when SS.PROESTADO = 3 then 'Autorizado'
			when SS.PROESTADO = 4 then 'Anulado'
			when SS.PROESTADO = 5 then 'No autorizado'
			else 'Pendiente por Autorizar'
		end AS Autorizacion, e.CANSERIPS as Cantidad,
		0 AS 'Cantidadrea'
					 ,E.AUTO EntityId, 0 as Terapia,
					 '' As CodigoMotivoMuestraNoConforme, '' AS DescripcionMotivoMuestraNoConforme, CASE F.SERIPSPOS WHEN 1 then 'Si' When 0 then 'NO' end  'ServicioPBS'
		From HCORDINTE E with(nolock)
				INNER JOIN INCUPSIPS  F with(nolock) ON E.CODSERIPS = F.CODSERIPS 
				LEFT OUTER JOIN ADCONFSER S with(nolock) ON S.CODSERIPS = E.CODSERIPS  
				LEFT OUTER JOIN ADAUTOSER ss with(nolock) on SS.IPCODPACI = E.IPCODPACI AND SS.NUMINGRES = E.NUMINGRES AND SS.CODSERIPS = E.CODSERIPS and SS.NUMEFOLIO = E.NUMEFOLIO
				where E.CODCENATE=@CentroAtencion  AND ESTSERIPS in (1,2) AND E.NUMINGRES in (select Ingreso from @TablaIngresosFiltro)  
		
UNION ALL
		
	Select  G.UFUCODIGO,TIPSERIPS as TipoServicio,'Patologias' as NombreServicio,G.IPCODPACI,G.NUMINGRES,FECORDMED As FechaSolicitud,RTRIM(H.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) As Nombre,ESTSERIPS, Case ESTSERIPS when 1 then 'Sin Realizar' When 2 then 'Sin Realizar' When 3 then 'Pendientes Iterpretación' when 5 Then 'Estudio Remitido'  End As Estado ,TIPSERIPS,G.NUMEFOLIO,ESTSERIPS,
	Case WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) Then 'No requiere autorización' 
			when SS.PROESTADO = 1 then 'Pendiente Solicitud'
			when SS.PROESTADO = 2 then 'Solicitado'
			when SS.PROESTADO = 3 then 'Autorizado'
			when SS.PROESTADO = 4 then 'Anulado'
			when SS.PROESTADO = 5 then 'No autorizado'
			else 'Pendiente por Autorizar'
		end AS Autorizacion, g.CANSERIPS as Cantidad,
		0 AS 'Cantidadrea'
					 ,G.AUTO EntityId, 0 as Terapia,
					 '' As CodigoMotivoMuestraNoConforme, '' AS DescripcionMotivoMuestraNoConforme, CASE H.SERIPSPOS WHEN 1 then 'Si' When 0 then 'NO' end  'ServicioPBS'
		From HCORDPATO G with(nolock)
				INNER JOIN INCUPSIPS H with(nolock) ON H.CODSERIPS = G.CODSERIPS 
				LEFT OUTER JOIN ADCONFSER S with(nolock) ON S.CODSERIPS = G.CODSERIPS  
				LEFT OUTER JOIN ADAUTOSER ss with(nolock) on SS.IPCODPACI = G.IPCODPACI AND SS.NUMINGRES =G.NUMINGRES AND SS.CODSERIPS = G.CODSERIPS and SS.NUMEFOLIO = G.NUMEFOLIO
				where G.CODCENATE=@CentroAtencion  AND ESTSERIPS in (1,2,3,5) AND G.NUMINGRES in (select Ingreso from @TablaIngresosFiltro) 
		
UNION ALL
		
	Select  I.UFUCODIGO,TIPSERIPS as TipoServicio,case J.TIPSERTER when 1 --SI ES TERAPIA
	then 'Terapias - Procedimientos no Qx' else 'Procedimientos no Qx' end as NombreServicio,I.IPCODPACI,I.NUMINGRES,FECORDMED As FechaSolicitud,RTRIM(J.CODSERIPS) AS Codigo,RTRIM(DESSERIPS) As Nombre,ESTSERIPS, 
	case J.TIPSERTER when 1 --SI ES TERAPIA
		then case ESTSERIPS when 1 then iif((Select count(*) from HCPROCTER where IPCODPACI = I.IPCODPACI AND NUMINGRES IN (Select Ingreso from @TablaIngresosFiltro)AND CODSERIPS = I.CODSERIPS ) > 0  ,'Pendiente','Ordenado') When 2 then 'Cumplido' end 
	else case ESTSERIPS when 1 then 'Solicitado' When 2 then 'Pendiente Interpretación'  end  End As Estado,
	TIPSERIPS,I.NUMEFOLIO,ESTSERIPS,
		Case WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) Then 'No requiere autorización' 
			when SS.PROESTADO = 1 then 'Pendiente Solicitud'
			when SS.PROESTADO = 2 then 'Solicitado'
			when SS.PROESTADO = 3 then 'Autorizado'
			when SS.PROESTADO = 4 then 'Anulado'
			when SS.PROESTADO = 5 then 'No autorizado'
			else 'Pendiente por Autorizar'
		end AS Autorizacion, i.CANSERIPS as Cantidad,
		case J.TIPSERTER when 1 then (Select count(*) from HCPROCTER where IPCODPACI = I.IPCODPACI AND NUMINGRES IN (Select Ingreso from @TablaIngresosFiltro) AND CODSERIPS = I.CODSERIPS) else 0 end AS 'Cantidadrea'
					 ,I.AUTO EntityId, ISNULL(J.TIPSERTER,0) as Terapia,
					 '' As CodigoMotivoMuestraNoConforme, '' AS DescripcionMotivoMuestraNoConforme, CASE J.SERIPSPOS WHEN 1 then 'Si' When 0 then 'NO' end  'ServicioPBS'
		From HCORDPRON I with(nolock)
				INNER JOIN INCUPSIPS J with(nolock) ON J.CODSERIPS = I.CODSERIPS 
				LEFT OUTER JOIN ADCONFSER S with(nolock) ON S.CODSERIPS = I.CODSERIPS
				LEFT OUTER JOIN ADAUTOSER ss with(nolock) on SS.IPCODPACI = I.IPCODPACI AND SS.NUMINGRES = I.NUMINGRES AND SS.CODSERIPS = I.CODSERIPS and SS.NUMEFOLIO = I.NUMEFOLIO  
				where I.CODCENATE=@CentroAtencion  AND ESTSERIPS in (1,2)  AND I.NUMINGRES in (select Ingreso from @TablaIngresosFiltro) and i.MANEXTPRO = 0 
		
UNION ALL 
		
		Select P.UFUCODIGO, 99 as TipoServicio, 'Remisión' as NombreServicio, IPCODPACI , NUMINGRES , FECSOLICIT as FechaSolicitud, '' as Codigo, 'Remisión' as Nombre, '' as ESTSERIPS, case P.ESTADO when 1 then 'Solicitado'  when 2 then 'Pendiente o gestionando' When 3 then 'Aceptado con pendiente de salida' END as Estado, 0 as TIPSERIPS,
		0 as NUMEFOLIO, case P.ESTADO when 1 then 0  when 2 then 0 When 3 then 4 END as Estado,'No aplica' AS Autorizacion
					 ,0 as 'Cantidad', 0 as 'Cantidadrea', P.AUTO EntityId, 0 AS Terapia,'' As CodigoMotivoMuestraNoConforme, '' AS DescripcionMotivoMuestraNoConforme,  '' as  'ServicioPBS'
		From HCREFCONP as P with(nolock) 
		where CODCENATE = @CentroAtencion  
		AND P.UFUCODIGO=ISNULL(NULLIF(@UFuncional, ''), p.UFUCODIGO) 
		AND ESTADO in (1,2,3) 
		AND NUMINGRES in (select Ingreso from @TablaIngresosFiltro)
	
END
	
		--Consulto el Resto de Información.
		SELECT N.CODTIPPAC as TipoPaciente,L.CODICAMAS AS 'Codigo Cama', RTRIM(DESCCAMAS) AS Cama,S.FechaSolicitud , S.Identificacion  AS Identificacion,RTRIM(K.IPNOMCOMP) AS 'Nombre Completo', IPFECNACI AS 'Fecha Nacimiento',S.NombreTipoServicio,CAST('' AS CHAR(50)) AS Edad,
		CASE WHEN K.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
		, K.ZONAPARTADA,F.RIESGOAGRE,(SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = K.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,N.VIVESOLO,
		(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = N.NUMINGRES) AS ACOMPANANTES,CONVERT(BIT,0) AS Riesgo,S.Ingreso AS Ingreso,RTRIM(O.NOMENTIDA) as EntidadPaciente, RTRIM(Q.NOMDIAGNO) as Diagnostico,RTRIM(G.DESESPECI) AS DescripcionEspecialidad,
		Rtrim(s.CodigoServicio) as 'Codigo Servicio',rtrim(NombreServicio) as 'Nombre Servicio',Rtrim(s.NombreEstado) as 'Estado Servicio' ,s.CodigoTipoServicio,s.NumeroFolio,s.CodigoEstadoServicio ,S.Autorizacion, S.EntityId, S.Cantidad, S.Cantidadrea, S.Terapia, S.CodigoMotivoMuestraNoConforme, S.DescripcionMotivoMuestraNoConforme, S.ServicioPBS
		FROM #Pacientes S with(nolock)
				INNER JOIN  ADINGRESO N with(nolock) ON N.NUMINGRES COLLATE DATABASE_DEFAULT = S.Ingreso  COLLATE DATABASE_DEFAULT
				INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  COLLATE DATABASE_DEFAULT = S.CodigoUnidadFuncional  COLLATE DATABASE_DEFAULT
				LEFT OUTER JOIN CHCAMASHO L with(nolock) ON L.CODICAMAS = N.CODCAMACT  
				LEFT OUTER JOIN CHREGESTA M with(nolock) ON M.CODICAMAS = L.CODICAMAS  AND M.REGESTADO = 1 
				INNER JOIN INPACIENT K with(nolock) ON K.IPCODPACI COLLATE DATABASE_DEFAULT = S.Identificacion COLLATE DATABASE_DEFAULT
				LEFT OUTER JOIN INENTIDAD O with(nolock) ON O.CODENTIDA = K.CODENTIDA 
				LEFT OUTER JOIN INDIAGNOS Q with(nolock) ON Q.CODDIAGNO = N.CODDIAING
				LEFT OUTER JOIN INESPECIA G with(nolock) ON N.CODESPTRA = G.CODESPECI 
				LEFT OUTER JOIN dbo.ADACTIVID F with(nolock) ON F.CODACTIVI = K.codactivi

		ORDER BY S.Identificacion 
END