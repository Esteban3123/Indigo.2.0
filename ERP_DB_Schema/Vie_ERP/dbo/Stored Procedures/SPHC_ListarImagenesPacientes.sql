CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientes] 
(
@Estado int,
@CentroAtencion Char(10),
@SubGrupo char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT AUTO, ROW_NUMBER() OVER (ORDER BY A.FECORDMED DESC) AS NumeroFila, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
		   RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, A.UFUCODIGO AS CodigoUnidad, RTRIM(C2.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, 
		   A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, A.NUMEFOLIO AS Folio,RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, 
		   A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) AS NombreDiagnostico, G.DESCCAMAS AS Cama, g.CODAISLAM AS TipoAislamiento, 
		   b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,rtrim(ltrim(b.CORELEPAC)) as Correo,
		   rtrim(ltrim(H.CODPROSAL)) as CodigoProfesional,  rtrim(ltrim(H.NOMMEDICO))  AS Medico, J.IMAREAEXA AS TiempoMaximo, J.UNITIEREA  AS UnidadTiempo, 0 as Minutos,CAST('' as char) as Barra,
		   J.IMAENTRES as TiempoResultado,J.UNITIERES as UnidadResultado,CAST('' as bit) as Marcar,a.CONCURRE as Concurrencia,a.SERREAINT as RealizaInterfaz,a.CANSERIPS AS Cantidad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
		   B.PESO,RTRIM(CA.NOMCENATE) as CentroAtencion, A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD', 
		   CAST('' As Varchar(100)) As Edad, CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad,rtrim(ltrim(CA.CODCENATE)) as CodigoCentroAtencion, B.IPTIPODOC as TipoIdentificacion,
		   rtrim(ltrim(B.IPPRIAPEL)) as PrimerApellido,rtrim(ltrim(B.IPSEGAPEL)) as SegundoApellido, rtrim(ltrim(B.IPPRINOMB)) as PrimerNombre, rtrim(ltrim(B.IPSEGNOMB)) as SegunNombre, B.IPFECNACI as fechaNacimiento, 
		   case B.IPSEXOPAC when 1 then 'M' else 'F' end SexoDescripcion, B.IPDIRECCI as Direccion, rtrim(ltrim(U.UBINOMBRE)) as NombreLocalidad, rtrim(ltrim(M.MUNCODIGO)) CodigoLocalidad, 
		   rtrim(ltrim(DP.depcodigo)) as CodigoDpto, rtrim(ltrim(Dp.nomdepart)) as NombreDpto, rtrim(ltrim(B.IPTELEFON)) as Telefono,  rtrim(ltrim(B.IPTELMOVI)) as Celular, rtrim(ltrim(B.CORELEPAC)) as Correo,
		   rtrim(ltrim(A.OBSSERIPS)) as Observacion, H.CODESPEC1 as Especialidad,replace(ltrim(replace(K.CODIGONIT,'0',' ')),' ','0') as NitEntidad, rtrim(ltrim(K.NOMENTIDA)) as Entidad, 
		   E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Autorización' AS Autorizacion, z.Color,
		   dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
	FROM  dbo.HCORDIMAG  AS A with(nolock) 
	INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
	INNER JOIN dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS 
	INNER JOIN dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
	INNER JOIN dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
	INNER JOIN dbo.INUNIFUNC AS C2 with(nolock) ON isnull(E.UFUACTPAC,A.UFUCODIGO) = C2.UFUCODIGO 
	LEFT OUTER JOIN  dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS 
	INNER JOIN  dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  
	LEFT OUTER JOIN dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO 
	LEFT OUTER JOIN dbo.HCPARALEIMA  AS J with(nolock) ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE = @CentroAtencion 
	INNER JOIN  dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA 
	INNER JOIN  dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB 
	LEFT JOIN dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE 
	LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
	inner join dbo.INUBICACI  U with(nolock) on U.AUUBICACI   = B.AUUBICACI 
	inner join dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD 
	inner join dbo.INDEPARTA DP with(nolock) on DP.depcodigo = M.DEPCODIGO 
	left join dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0)
	LEFT JOIN  dbo.CHTIPOSAISLAMIENTOS Z with(nolock) on z.Id = g.CODAISLAM 
	INNER JOIN dbo.RISGRIMAGE RIS ON CS.IDRISGRIMAGE = RIS.ID
	WHERE ((@Estado <> 0 AND A.ESTSERIPS IN (@Estado,IIF(@Estado = 2,8,@Estado))) or (@Estado = 0 AND A.ESTSERIPS IN(1,2))) and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo AND (NOT EXISTS (SELECT 1 FROM HCPARPACS P WHERE P.CODCENATE = A.CODCENATE AND P.INTPACSAC = 1 AND P.INTERFAZHK = 3) OR A.SendToInterface = 2)
END