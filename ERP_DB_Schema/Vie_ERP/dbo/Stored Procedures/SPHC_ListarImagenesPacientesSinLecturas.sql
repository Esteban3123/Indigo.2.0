
/***********************************************************************************************
Modified By: Carlos Pinzon
Date: 2019.03.15
Description: Realizo modificacion a consulta para traer información de lista imagenes de pacientes filtrado por grupo Imagenologia
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesSinLecturas]
(
@CentroAtencion Char(10),
@SubGrupo char(10)
)
AS
BEGIN
SET NOCOUNT ON;

	SELECT
		AUTO,
		A.FECORDMED AS FechaSolicitud,
		RTRIM(A.IPCODPACI) AS CodigoPaciente, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		B.IPFECNACI AS FechaNacimiento, 
		E.UFUACTPAC AS CodigoUnidad, 
		RTRIM(C.UFUDESCRI) AS DescripcionUnidad, 
		RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, 
		A.CODCENATE AS CodigoCentro, 
		A.ESTSERIPS AS Estado, 
		A.ESTALEIMG AS EstadoAlerta, 
		A.NUMINGRES AS Ingreso, 
		A.NUMEFOLIO AS Folio, 
		RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio,
		A.OBSSERIPS AS ObservacionServicio,
		RTRIM(A.CODSERIPS) AS CodigoServicio,
		RTRIM(I.NOMDIAGNO) AS NombreDiagnostico, 
		G.DESCCAMAS AS Cama,
		G.CODAISLAM AS TipoAislamiento,
		B.IPDIRECCI AS Direccion,
		B.IPTELEFON AS Telefono,
		B.IPSEXOPAC AS Sexo,
		B.IPRHSANGR AS Rh,
		B.IPGRUPSAN AS GrupoSanguineo,
		B.IPTELMOVI AS Movil, 
		B.CORELEPAC AS Correo, 
		H.NOMMEDICO AS Medico,
		A.CONCURRE AS Concurrencia,
		A.SERREAINT AS RealizaInterfaz,
		A.CANSERIPS AS Cantidad,
		RTRIM(CA.NOMCENATE) AS CentroAtencion,
		A.FECHASUGE AS FechaSugerida,
		CASE A.LATERALIDAD 
			WHEN 0 THEN 'No Aplica' 
			WHEN 1 THEN 'Izquierda' 
			WHEN 2 THEN 'Derecha' 
			WHEN 3 THEN 'Ambos' 
		END AS 'LATERALIDAD',
		CAST('' AS Varchar(100)) AS Edad,
		RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
		CASE A.PRISERIPS 
			WHEN '1' THEN 'Urgente' 
			WHEN '2' THEN 'Rutinario' 
			ELSE 'Otro' 
		END AS PRIORIDAD,
		dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', 
		dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM
		dbo.HCORDIMAG AS A with(nolock) 
		INNER JOIN	dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN	dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D.CODSERIPS 
		INNER JOIN	dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN	dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
		INNER JOIN	dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO 
		LEFT OUTER JOIN  dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS 
		INNER JOIN 	dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL = H.CODPROSAL  
		LEFT OUTER JOIN	dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO  
		INNER JOIN	dbo.INCUPSSUB AS CS with(nolock) ON CS.CODGRUSUB = D.CODGRUSUB 
		INNER JOIN	dbo.ADCENATEN AS CA with(nolock) ON CA.CODCENATE = A.CODCENATE 
		INNER JOIN	dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA = E.CODENTIDA 
		LEFT JOIN  contract.CUPSEntityContractDescriptions AS CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions AS CD ON CD.Id = CDD.ContractDescriptionId
	 WHERE
		A.ESTSERIPS IN(3) 
		AND A.CODCENATE IN(SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		AND CS.IDRISGRIMAGE = @subgrupo
		AND A.SERTRANSC = 0 
		AND A.SERVALMED = 0 
		AND A.ESTTRASER = '1' 
		and A.TIENEGRABACION = 0
	 ORDER BY A.FECORDMED DESC

 
END