CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientesAmbulatorio]
(
	@Estado varchar(100),
	@CentroAtencion Char(500)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
    -- Insert statements for procedure here
 
	SELECT DISTINCT
		A.[AUTO], 
		A.FECORDMED AS 'FechaSolicitud', 
		A.FECRECMUE AS 'FechaRecoleccion',
		RTRIM(A.IPCODPACI) AS 'CodigoPaciente', 
		RTRIM(B.IPNOMCOMP) AS 'NombrePaciente', 
		B.IPFECNACI AS 'FechaNacimiento', 
		E.UFUACTPAC AS 'CodigoUnidad', 
		RTRIM(C.UFUDESCRI) AS 'DescripcionUnidad', 
		A.CODCENATE AS 'CodigoCentro',
		CASE A.ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Muestra recolectada' WHEN 3 THEN 'Pendiente interpretación' WHEN 4 THEN 'Examen interpretado' WHEN 5 THEN 'Estudio remitido' 
		WHEN 6 THEN 'Anulado' WHEN 7 THEN 'Extramural' WHEN 8 THEN 'Muestra recolectada parcialmente' WHEN 9 THEN 'Muestra no conforme' END AS 'Estado', 
		A.ESTALELAB AS 'EstadoAlerta', 
		A.NUMINGRES AS 'Ingreso', 
		(SELECT MIN(CODCONCEC) FROM dbo.INTERDETA AS M WITH(NOLOCK) WHERE M.autolabor = A.AUTO AND M.ORDTIP = 'AMB' ) AS 'IDMuestra',
		RTRIM(A.CODSERIPS) AS 'CodigoServicio',
		RTRIM(D .DESSERIPS) + '. ' + ISNULL(CD.name,'') AS 'DescripcionServicio', 
		ISNULL(CD.name,'') AS 'DescripcionRelacionada',
		dbo.edad(b.IPFECNACI, common.GETDATE()) AS 'Edad',
		B.CORELEPAC, 
		b.IPDIRECCI AS 'Direccion', 
		b.IPTELEFON AS 'Telefono', 
		b.IPSEXOPAC  AS 'Sexo', 
		b.IPRHSANGR AS 'Rh', 
		b.IPGRUPSAN AS 'GrupoSanguineo', 
		b.IPTELMOVI AS 'Movil', 
		b.CORELEPAC AS 'Correo', 
		H.NOMMEDICO  AS 'Medico', 
		J.LABRECMUE AS 'TiempoMaximo', 
		J.UNITIEREC AS 'UnidadTiempo', 
		0 AS 'Minutos', 
		CAST('' AS CHAR) AS 'Barra', 
		LABENTRES AS 'TiempoResultado', 
		UNITIEENT AS 'UnidadResultado', 
		CAST('' AS BIT) AS 'Marcar', 
		a.CONCURRE AS 'Concurrencia', 
		e.UFUCODIGO, 
		e.UFUCODIGO, 
		A.CODPROSAL, 
		IPTIPODOC, 
		B.IPPRINOMB, 
		B.IPSEGNOMB, 
		B.IPPRIAPEL, 
		B.IPSEGAPEL, 
		B.IPSEXOPAC, 
		B.IPFECNACI, 
		B.CODENTIDA, 
		B.IPTIPOPAC, 
		B.IPTIPOAFI, 
		B.IPTELEFON, 
		B.IPTELMOVI, 
		B.IPDIRECCI, 
		A.CANSERIPS, 
		D.CODGOCUPS, 
		A.OBSERVACI AS 'ObservacionServicio', 
		RTRIM(MUN.DEPMUNCOD) AS 'MunicipioCodigo',
		RTRIM(MUN.MUNNOMBRE) AS 'MunicipioNombre', 
		RTRIM(ENT.CODENTIDA) AS 'entidadpagadoraCodigo', 
		RTRIM(ENT.NOMENTIDA) AS 'entidadpagadoraNombre', 
		E.CODTIPPAC, 
		'' AS 'CodigoDiagnosticoSegundo', 
		'' AS 'NombreDiagnosticoSegundo', 
		'' AS 'CodigoDiagnosticoTercero', 
		'' AS 'NombreDiagnosticoTercero', 
		'' AS 'Prioridad', 
		RP.GESTACION, 
		A.NUMCONCIT, 
		RTRIM(CA.NOMCENATE) AS 'CentroAtencion', 
		RTRIM(G.DESCCAMAS) AS 'Cama', 
		CASE WHEN ce.FinancedResourceUPC = '1' THEN 'Si' ELSE 'No' END AS 'FinanciadoUPC',
		A.CODMOTIVOMUESTRANOCONFORME AS 'CodigoMotivoMuestraNoConforme',
		RTRIM(MA.DESMOTANU) AS 'DescripcionMotivoMuestraNoConforme',
		RTRIM(CenRem.DESCENLAB) AS 'CentroRemision'
		,[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,A.NUMINGRES,2) AS IconoEscalas
	FROM  
		dbo.AMBORDLAB AS A  WITH(NOLOCK) 
		INNER JOIN dbo.INPACIENT AS B  WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INCUPSIPS AS D  WITH(NOLOCK) ON A.CODSERIPS = D .CODSERIPS 
		INNER JOIN CONTRACT.CUPSEntity ce ON A.CODSERIPS = ce.Code 
		INNER JOIN dbo.ADINGRESO AS E  WITH(NOLOCK) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN dbo.INENTIDAD AS ENT  WITH(NOLOCK) ON ENT.CODENTIDA = E.CODENTIDA 
		INNER JOIN dbo.INUNIFUNC AS C  WITH(NOLOCK) ON E.UFUACTPAC = C.UFUCODIGO 
		LEFT OUTER JOIN dbo.INPROFSAL AS H  WITH(NOLOCK) ON A.CODPROSAL =H.CODPROSAL  
		LEFT OUTER JOIN dbo.HCPARALELAB AS J  WITH(NOLOCK) ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE = @CentroAtencion 
		INNER JOIN INUBICACI UBI  WITH(NOLOCK) ON B.AUUBICACI = UBI.AUUBICACI 
		INNER JOIN INMUNICIP MUN  WITH(NOLOCK) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD 
		LEFT JOIN dbo.HCRIESGOSP AS RP  WITH(NOLOCK) ON RP.NUMINGRCES = A.NUMINGRES 
		LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.ContractDescriptions  CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
		INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE 
		LEFT JOIN dbo.CHCAMASHO AS G WITH(NOLOCK)  ON E.CODCAMACT = G.CODICAMAS
		LEFT JOIN HCMOANULB MA WITH(NOLOCK) ON MA.CODMOTANU = A.CODMOTIVOMUESTRANOCONFORME
		LEFT JOIN HCLABREMI Rem WITH(NOLOCK) ON  rem.IPCODPACI = A.IPCODPACI AND rem.NUMINGRES = A.NUMINGRES AND rem.CODSERIPS = A.CODSERIPS   AND Rem.NUMEFOLIO IS NULL
	    LEFT JOIN HCCENLABU CenRem WITH(NOLOCK) ON CenRem.CODCENLAB = Rem.CODCENLAB 
	WHERE 
		A.ESTSERIPS IN (SELECT Value FROM dbo.splitstring(@Estado)) 
		AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		AND A.FECORDMED BETWEEN  DATEADD(MONTH,-4,common.GETDATE()) AND common.GETDATE()  	
	ORDER BY A.FECORDMED DESC
 
END