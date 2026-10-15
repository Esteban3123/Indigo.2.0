-- =============================================
-- Author:		<Yezid Garcia Medina, Desarrollador Junior>
-- Create date: <06 Septiembre de 2019>
-- Description:	<Listar Reporte Recepción Referencias>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarReporteRecepcionReferencia]
(
	@Centro varchar(20),
	@FechaInicio Datetime ,
	@FechaFin Datetime,
	@TipoVista bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF @TipoVista = 1
		Begin

			SELECT 
				B.FECREGIS AS 'Fecha solicitud'  ,
				B.ID AS 'ID solicitud',
				CASE
					WHEN IPTIPODOC = 1 THEN 'C.C.'
					WHEN IPTIPODOC = 2 THEN 'C.E.'
					WHEN IPTIPODOC = 3 THEN 'T.I.'
					WHEN IPTIPODOC = 4 THEN 'Registro Civil'
					WHEN IPTIPODOC = 5 THEN 'Pasaporte'
					WHEN IPTIPODOC = 6 THEN 'Adulto Sin Identificación'
					WHEN IPTIPODOC = 7 THEN 'Menor Sin Identificación'
					WHEN IPTIPODOC = 8 THEN 'Número único de identificación personal'
				END AS 'Tipo identificacion' ,
				RTRIM(A.IPCODPACI) AS 'Numero de identificacion' ,
				RTRIM(IPNOMCOMP) AS 'Nombres y Apellidos' ,
				(cast(datediff(dd, IPFECNACI ,[Common].[GETDATE]()) / 365.25 as int))  AS 'Edad' ,
				RTRIM(C.CODENTIDA) + ' - ' + RTRIM(C.NOMENTIDA) AS 'EPS' ,
				RTRIM(D.UBINOMBRE) AS 'Ciudad de origen' ,		
				RTRIM(G.CODIGOIPS) + ' - ' + RTRIM(G.DSCRIPIPS)AS 'IPS Emisora' ,
				RTRIM(E.CODESPECI) + ' - ' + RTRIM(E.DESESPECI) AS 'Especialidad que remite' ,
				RTRIM(B.OBSERVACION) AS 'Observacion' ,
				CASE
					WHEN I.ESTADO = 1 THEN 'Aceptado'
					WHEN I.ESTADO = 2 THEN 'No Aceptado'
					WHEN I.ESTADO = 3 THEN 'Sin Definir Conducta'
					WHEN I.ESTADO = 4 THEN 'Cancelado'			
				END AS 'Gestion' ,
				CASE
				WHEN I.ESTADO = 2 THEN RTRIM(J.CODIGO) + ' - ' + RTRIM(J.DESCRIPCION) 
				ELSE 'N/A'
				END AS 'Motivo' ,
				RTRIM(I.OBSERVACION) AS 'Observacion Registro' ,
				RTRIM(K.NOMMEDICO) AS 'Profesional' ,
				RTRIM(H.UFUCODIGO) + ' - ' + RTRIM(H.UFUDESCRI) AS 'Unidad Funcional' ,
				RTRIM(F.CODESPECI) + ' - ' + RTRIM(F.DESESPECI) AS 'Especialidad a la que remite' ,
				I.FECREGIS AS 'Fecha recepcion IPS y/o contrareferencia' ,
				DATEDIFF(day, B.FECREGIS, I.FECREGIS) AS 'Dias entre solicitud y gestion' ,		
				CASE 
					WHEN B.ESTADO = 3 AND L.FECALTPAC IS NOT NULL  THEN 'Cerrado'
					ELSE 'Abierto' 
				END 'Estado' 

			FROM 
				RCPACIENREF AS A with (nolock)
				INNER JOIN HCREFRECEP AS B with (nolock) ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN INENTIDAD  AS C with (nolock) ON A.CODENTIDA = C.CODENTIDA
				INNER JOIN INUBICACI  AS D with (nolock) ON A.AUUBICACI = D.AUUBICACI
				INNER JOIN INESPECIA  AS E with (nolock) ON B.INESPECIAIDREMITE = E.CODESPECI
				INNER JOIN INESPECIA  AS F with (nolock) ON B.INESPECIAIDAREMITIR = F.CODESPECI
				INNER JOIN ADCONTIPS AS G with (nolock) ON B.ADCONTIPSID = G.CODIGOIPS
				INNER JOIN INUNIFUNC AS H with (nolock) ON B.INUNIFUNCID = H.UFUCODIGO
				INNER JOIN ADCENATEN AS M with (nolock) ON B.ADCENATENID = M.CODCENATE
				LEFT JOIN HCREFRECDE AS I with (nolock) ON B.ID = I.HCREFRECEPID 
				LEFT JOIN HCMOTREFREC AS J with (nolock) ON I.HCMOTREFRECID = J.ID 
				LEFT JOIN INPROFSAL AS K with (nolock) ON I.MEDRECIB =  K.CODPROSAL
				LEFT JOIN HCREGEGRE AS L with (nolock) ON B.NUMINGRES =  L.NUMINGRES
				
			WHERE 
				B.ADCENATENID IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				B.FECREGIS BETWEEN  @FechaInicio  AND @FechaFin

		End
	ELSE
		Begin 

			SELECT 
				B.FECREGIS AS 'Fecha solicitud'  ,
				B.ID AS 'ID solicitud',
				CASE
					WHEN IPTIPODOC = 1 THEN 'C.C.'
					WHEN IPTIPODOC = 2 THEN 'C.E.'
					WHEN IPTIPODOC = 3 THEN 'T.I.'
					WHEN IPTIPODOC = 4 THEN 'Registro Civil'
					WHEN IPTIPODOC = 5 THEN 'Pasaporte'
					WHEN IPTIPODOC = 6 THEN 'Adulto Sin Identificación'
					WHEN IPTIPODOC = 7 THEN 'Menor Sin Identificación'
					WHEN IPTIPODOC = 8 THEN 'Número único de identificación personal'
				END AS 'Tipo identificacion' ,
				RTRIM(A.IPCODPACI) AS 'Numero de identificacion' ,
				RTRIM(IPNOMCOMP) AS 'Nombres y Apellidos' ,
				(cast(datediff(dd, IPFECNACI ,[Common].[GETDATE]()) / 365.25 as int))  AS 'Edad' ,
				RTRIM(C.CODENTIDA) + ' - ' + RTRIM(C.NOMENTIDA) AS 'EPS' ,
				RTRIM(D.UBINOMBRE) AS 'Ciudad de origen' ,		
				RTRIM(G.CODIGOIPS) + ' - ' + RTRIM(G.DSCRIPIPS)AS 'IPS Emisora' ,
				RTRIM(E.CODESPECI) + ' - ' + RTRIM(E.DESESPECI) AS 'Especialidad que remite' ,
				RTRIM(B.OBSERVACION) AS 'Observacion' ,
				CASE
					WHEN I.ESTADO = 1 THEN 'Aceptado'
					WHEN I.ESTADO = 2 THEN 'No Aceptado'
					WHEN I.ESTADO = 3 THEN 'Sin Definir Conducta'
					WHEN I.ESTADO = 4 THEN 'Cancelado'			
				END AS 'Gestion' ,
				CASE
				WHEN I.ESTADO = 2 THEN RTRIM(J.CODIGO) + ' - ' + RTRIM(J.DESCRIPCION) 
				ELSE 'N/A'
				END AS 'Motivo' ,
				RTRIM(I.OBSERVACION) AS 'Observacion Registro' ,
				RTRIM(K.NOMMEDICO) AS 'Profesional' ,
				RTRIM(H.UFUCODIGO) + ' - ' + RTRIM(H.UFUDESCRI) AS 'Unidad Funcional' ,
				RTRIM(F.CODESPECI) + ' - ' + RTRIM(F.DESESPECI) AS 'Especialidad a la que remite' ,
				I.FECREGIS AS 'Fecha recepcion IPS y/o contrareferencia' ,
				DATEDIFF(day, B.FECREGIS, I.FECREGIS) AS 'Dias entre solicitud y gestion' ,		
				CASE 
					WHEN B.ESTADO = 3 AND L.FECALTPAC IS NOT NULL  THEN 'Cerrado'
					ELSE 'Abierto' 
				END 'Estado' 

			FROM 
				RCPACIENREF AS A with (nolock)
				INNER JOIN HCREFRECEP AS B with (nolock) ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN INENTIDAD  AS C with (nolock) ON A.CODENTIDA = C.CODENTIDA
				INNER JOIN INUBICACI  AS D with (nolock) ON A.AUUBICACI = D.AUUBICACI
				INNER JOIN INESPECIA  AS E with (nolock) ON B.INESPECIAIDREMITE = E.CODESPECI
				INNER JOIN INESPECIA  AS F with (nolock) ON B.INESPECIAIDAREMITIR = F.CODESPECI
				INNER JOIN ADCONTIPS AS G with (nolock) ON B.ADCONTIPSID = G.CODIGOIPS
				INNER JOIN INUNIFUNC AS H with (nolock) ON B.INUNIFUNCID = H.UFUCODIGO
				INNER JOIN ADCENATEN AS M with (nolock) ON B.ADCENATENID = M.CODCENATE
				LEFT JOIN (	SELECT I1.HCREFRECEPID, MAX(I1.ID) ULTIMOID FROM HCREFRECEP AS B1 INNER JOIN HCREFRECDE AS I1 ON I1.HCREFRECEPID = B1.ID  
				GROUP BY I1.HCREFRECEPID  ) T ON B.ID = T.HCREFRECEPID
				LEFT JOIN HCREFRECDE AS I with (nolock) ON I.ID = T.ULTIMOID
				LEFT JOIN HCMOTREFREC AS J with (nolock) ON I.HCMOTREFRECID = J.ID 
				LEFT JOIN INPROFSAL AS K with (nolock) ON I.MEDRECIB =  K.CODPROSAL
				LEFT JOIN HCREGEGRE AS L with (nolock) ON B.NUMINGRES =  L.NUMINGRES
				
			WHERE 
				B.ADCENATENID IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				B.FECREGIS BETWEEN  @FechaInicio  AND @FechaFin

		End 	

END