CREATE PROCEDURE [dbo].[SP_eHC_ListarReporteInterconsulta]
(
	@Centro varchar(20),
	@FechaInicio Datetime ,
	@FechaFin Datetime ,
	@TipoSolicitud Char(1)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here	
		SELECT 
			A.FECORDMED AS 'Fecha Solicitud' ,
			RTRIM(B.CODCENATE) AS 'Codigo Centro de Atencion' ,
			RTRIM(B.NOMCENATE) AS 'Centro de Atencion' ,
			CASE A.ESTSERIPS
				WHEN 1 THEN '1.Solicitado'
				WHEN 2 THEN '2.Solicitud Enviada'
				WHEN 3 THEN '3.Interconsulta Realizada'
				WHEN 4 THEN '4.Extramural'
				WHEN 5 THEN '5.Anulado'
				when 6 THEN '6.Pendiente de verificación por parte del especialista'
			END AS 'Estado',
			RTRIM(C.UFUCODIGO) AS 'Codigo Unidad Funcional' ,
			RTRIM(C.UFUDESCRI) AS 'Unidad Funcional' ,
			dbo.TipoDocumentoNombreCompleto(d.IPTIPODOC) as 'Tipo de Identificacion',
			RTRIM(D.IPCODPACI) AS 'Numero de Identificacion' ,
			RTRIM(D.IPNOMCOMP) AS 'Nombres y Apellidos' ,
			RTRIM(D.IPPRINOMB) AS 'Primer Nombre' , 
			RTRIM(D.IPSEGNOMB) AS 'Segundo Nombre' , 
			RTRIM(D.IPPRIAPEL)AS 'Primer Apellido' , 
			RTRIM(D.IPSEGAPEL) AS 'Segundo Apellido' ,
			D.IPFECNACI AS 'Fecha Nacimiento' ,
			cast ( datediff ( dd, D.IPFECNACI ,GETDATE( ) ) / 365.25 as int ) AS 'Edad en Años' ,
			dbo.EDAD( D.IPFECNACI, [Common].[GETDATE]() ) AS 'Edad Completa' ,
			RTRIM(D.IPDIRECCI) AS 'Direccion' ,
			RTRIM(D.IPTELMOVI) AS 'Celular' ,
			RTRIM(D.IPTELEFON) AS 'Telefono Fijo' , 
			CASE D.IPSEXOPAC
				WHEN 1 THEN 'Masculino'
				WHEN 2 THEN 'Femenino'
			END AS 'Sexo' ,
			RTRIM(J.Code) AS 'Codigo Grupo de Atencion' ,
			RTRIM(J.Name) AS 'Grupo de Atencion' ,
			RTRIM(K.Code) AS 'Codigo Entidad' ,
			RTRIM(K.Name) AS 'Entidad' ,
			RTRIM(F.CODSERIPS) AS 'Codigo de Servicio' ,
			RTRIM(F.DESSERIPS) AS 'Servicio' ,			
			CASE 
				WHEN M.IDMODELOHC IS NULL THEN 'N/A'
				WHEN M.IDMODELOHC IS NOT NULL THEN N.CODIGO
			END AS 'Codigo Modelo HC' ,
			CASE 
				WHEN M.IDMODELOHC IS NULL THEN 
					(CASE M.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' THEN 'Nota Evolución'
						WHEN 'E' THEN 'Evolución'
						WHEN 'O' THEN 'Otros modelos de apoyo'
						WHEN 'PT' THEN 'Partograma'
						WHEN 'NF' THEN 'Nota Farmaceutica'
						WHEN 'V' THEN 'Valoración de Seguimiento'
						WHEN 'F' THEN 'Consulta Preanestesia'	
						WHEN 'T' THEN 'Historia clinica de Control'
						WHEN 'S' THEN 'Servicio de apoyo'
						WHEN 'P' THEN 'Atención partos'
						WHEN 'B' THEN 'Recien Nacido'
						WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
						ELSE 'Otro'
					END)
				WHEN M.IDMODELOHC IS NOT NULL THEN N.DESCRIPCION
			END AS 'Modelo HC' ,
			RTRIM(G.CODPROSAL) AS 'Codigo Profesional' ,
			RTRIM(G.NOMMEDICO) AS 'Profesional' ,
			RTRIM(H.CODDIAGNO) AS 'Codigo Dx' , 
			RTRIM(H.NOMDIAGNO) AS 'Diagnostico' , 
			RTRIM(I.CODESPECI) AS 'Codigo Especialidad' ,
			RTRIM(I.DESESPECI) AS 'Especialidad' ,
			CASE 
				WHEN A.CODPROINT IS NULL THEN 'N/A'
				WHEN A.CODPROINT IS NOT NULL THEN RTRIM(L.CODPROSAL)
			END AS 'Codigo Medico' ,			
			CASE 
				WHEN A.CODPROINT IS NULL THEN 'N/A'
				WHEN A.CODPROINT IS NOT NULL THEN RTRIM(L.NOMMEDICO)
			END AS 'Medico' , 
			CASE A.PRISERIPS
				WHEN '1' THEN 'Urgente'
				WHEN '2' THEN 'Rutina'
			END AS 'Prioridad' ,
			CASE A.MANEXTPRO
				WHEN 0 THEN 'Intrahospitalario'
				WHEN 1 THEN 'Extramural'
			END AS 'Tipo de Solicitud' ,
			CASE
				WHEN A.OBSSERIPS IS NULL THEN ''
				WHEN A.OBSSERIPS IS NOT NULL THEN RTRIM(A.OBSSERIPS)
			END AS 'Recomendaciones'	
		FROM
			HCORDINTE AS A with (nolock) 
			INNER JOIN ADCENATEN AS B with (nolock) ON A.CODCENATE = B.CODCENATE
			INNER JOIN INUNIFUNC AS C with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
			INNER JOIN INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI
			INNER JOIN ADINGRESO AS E with (nolock) ON A.NUMINGRES = E.NUMINGRES
			INNER JOIN INCUPSIPS AS F with (nolock) ON A.CODSERIPS = F.CODSERIPS
			INNER JOIN INPROFSAL AS G with (nolock) ON A.CODPROSAL = G.CODPROSAL
			INNER JOIN INDIAGNOS AS H with (nolock) ON A.CODDIAGNO = H.CODDIAGNO
			INNER JOIN INESPECIA AS I with (nolock) ON A.CODESPECI = I.CODESPECI
			INNER JOIN Contract.CareGroup AS J with (nolock) ON E.GENCAREGROUP = J.Id
			INNER JOIN Contract.HealthAdministrator AS K with (nolock) ON E.GENCONENTITY = K.Id
			LEFT JOIN INPROFSAL AS L with (nolock) ON A.CODPROINT = L.CODPROSAL
			INNER JOIN HCHISPACA AS M with (nolock) ON A.IPCODPACI = M.IPCODPACI AND A.NUMINGRES = M.NUMINGRES AND A.NUMEFOLIO = M.NUMEFOLIO AND A.IDETIPHIS = M.IDETIPHIS
			LEFT JOIN PRMODELOHC AS N with (nolock) ON M.IDMODELOHC = N.ID
		 WHERE	
			A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro))
			AND A.FECORDMED BETWEEN  @FechaInicio  AND @FechaFin  AND
				CASE @TipoSolicitud
					WHEN '0' THEN A.MANEXTPRO
					WHEN '1' THEN A.MANEXTPRO
					ELSE CAST (1 AS BIT)
				END
				=
				CASE @TipoSolicitud
					WHEN '0' THEN 0
					WHEN '1' THEN 1
					ELSE CAST (1 AS BIT)
				END
		
-- fin SP
END