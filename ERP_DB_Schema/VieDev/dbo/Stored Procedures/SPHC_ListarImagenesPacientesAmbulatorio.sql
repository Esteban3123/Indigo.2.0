

/***********************************************************************************************
Modified By: Carlos Pinzon
Date: 2019.03.14
Description: Realizo modificacion a consulta para traer información de pacientes filtrado por grupo Imagenologia
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesAmbulatorio]
(
@Estado int,
@CentroAtencion Char(100),
@SubGrupo char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @Estado <> 0
		BEGIN
			SELECT DISTINCT AUTO, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
			B.IPFECNACI AS FechaNacimiento, E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,
			A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, b.IPDIRECCI AS Direccion,
			b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,rtrim(ltrim(b.CORELEPAC)) as Correo, rtrim(ltrim(H.CODPROSAL)) as CodigoProfesional,
			rtrim(ltrim(H.NOMMEDICO))  AS Medico, 0 as Minutos,CAST('' as char) as Barra,CAST('' as bit) as Marcar, a.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio,
			A.FECRECEXA,RTRIM(I.NOMENTIDA) AS DescripcionEntidad, A.OBSERVSER AS ObservacionServicioAmbulatorio, B.PESO, A.NUMCONCIT,RTRIM(CA.NOMCENATE) as CentroAtencion, 'No Aplica' AS 'LATERALIDAD',
			E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION,
			CAST('' As Varchar(100)) As Edad, rtrim(ltrim(CA.CODCENATE)) as CodigoCentroAtencion, B.IPTIPODOC as TipoIdentificacion, rtrim(ltrim(B.IPPRIAPEL)) as PrimerApellido,rtrim(ltrim(B.IPSEGAPEL)) as SegundoApellido, 
			rtrim(ltrim(B.IPPRINOMB)) as PrimerNombre, rtrim(ltrim(B.IPSEGNOMB)) as SegunNombre, B.IPFECNACI as fechaNacimiento, case B.IPSEXOPAC when 1 then 'M' else 'F' end SexoDescripcion, B.IPDIRECCI as Direccion,
			rtrim(ltrim(U.UBINOMBRE)) as NombreLocalidad, rtrim(ltrim(M.MUNCODIGO)) CodigoLocalidad, rtrim(ltrim(DP.depcodigo)) as CodigoDpto, rtrim(ltrim(Dp.nomdepart)) as NombreDpto, rtrim(ltrim(B.IPTELEFON)) as Telefono,  
			rtrim(ltrim(B.IPTELMOVI)) as Celular, rtrim(ltrim(B.CORELEPAC)) as Correo, rtrim(ltrim(A.OBSERVSER)) as Observacion, H.CODESPEC1 as Especialidad, replace(ltrim(replace(I.CODIGONIT,'0',' ')),' ','0')  as NitEntidad, 
			rtrim(ltrim(I.NOMENTIDA)) as Entidad, E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Urgente' as Prioridad, -- Media
			dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		    dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
			FROM  dbo.AMBORDIMA  AS A with(nolock) INNER JOIN
			 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
			 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
			 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
			 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
			 dbo.INUNIFUNC AS C2 with(nolock) ON E.UFUACTPAC = C2.UFUCODIGO LEFT OUTER JOIN 
			 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL INNER JOIN 
			 dbo.INENTIDAD AS I with(nolock) ON I.CODENTIDA=E.CODENTIDA INNER JOIN 
			 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB LEFT JOIN
			 dbo.INUBICACI  U with(nolock) on U.AUUBICACI   = B.AUUBICACI INNER JOIN 
			 dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD inner JOIN 
			 dbo.INDEPARTA DP with(nolock) on DP.depcodigo = M.DEPCODIGO left JOIN 
			 dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0) left join
			 contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
			 contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
			 WHERE A.ESTSERIPS IN (@Estado,IIF(@Estado = 2,8,@Estado)) and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))  and  CS.IDRISGRIMAGE=@subgrupo
		END
		 --select * from HCORDIMAG
	ELSE
		BEGIN
			SELECT DISTINCT AUTO, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
			RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
			E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
			RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio ,
			b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
			b.IPTELMOVI as Movil ,rtrim(ltrim(b.CORELEPAC)) as Correo, rtrim(ltrim(H.CODPROSAL)) as CodigoProfesional,rtrim(ltrim(H.NOMMEDICO))  AS Medico, 0 as Minutos,CAST('' as char) as Barra,CAST('' as bit) as Marcar,
			a.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio,A.FECRECEXA,RTRIM(I.NOMENTIDA) AS DescripcionEntidad,
			A.OBSERVSER AS ObservacionServicioAmbulatorio, B.PESO, A.NUMCONCIT,RTRIM(CA.NOMCENATE) as CentroAtencion, 'No Aplica' AS 'LATERALIDAD',
			E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION,
			CAST('' As Varchar(100)) As Edad,
			rtrim(ltrim(CA.CODCENATE)) as CodigoCentroAtencion,
			B.IPTIPODOC as TipoIdentificacion,
			rtrim(ltrim(B.IPPRIAPEL)) as PrimerApellido,rtrim(ltrim(B.IPSEGAPEL)) as SegundoApellido, rtrim(ltrim(B.IPPRINOMB)) as PrimerNombre, rtrim(ltrim(B.IPSEGNOMB)) as SegunNombre,
			B.IPFECNACI as fechaNacimiento, case B.IPSEXOPAC when 1 then 'M' else 'F' end SexoDescripcion, B.IPDIRECCI as Direccion,
			rtrim(ltrim(U.UBINOMBRE)) as NombreLocalidad, rtrim(ltrim(M.MUNCODIGO)) CodigoLocalidad, rtrim(ltrim(DP.depcodigo)) as CodigoDpto, rtrim(ltrim(Dp.nomdepart)) as NombreDpto, rtrim(ltrim(B.IPTELEFON)) as Telefono,  rtrim(ltrim(B.IPTELMOVI)) as Celular, rtrim(ltrim(B.CORELEPAC)) as Correo,
			rtrim(ltrim(A.OBSERVSER)) as Observacion, H.CODESPEC1 as Especialidad, replace(ltrim(replace(I.CODIGONIT,'0',' ')),' ','0')  as NitEntidad, rtrim(ltrim(I.NOMENTIDA)) as Entidad, 
			E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Urgente' as Prioridad, -- Media
			dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo',
		    dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
			FROM  dbo.AMBORDIMA  AS A with(nolock) INNER JOIN
			 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
			 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
			 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
			 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
			 dbo.INUNIFUNC AS C2 with(nolock) ON E.UFUACTPAC = C2.UFUCODIGO LEFT OUTER JOIN 
			 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL INNER JOIN 
			 dbo.INENTIDAD AS I with(nolock) ON I.CODENTIDA=E.CODENTIDA INNER JOIN 
			 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB LEFT JOIN
			 dbo.INUBICACI  U with(nolock) on U.AUUBICACI   = B.AUUBICACI INNER JOIN 
			 dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD inner JOIN 
			 dbo.INDEPARTA DP with(nolock) on DP.depcodigo = M.DEPCODIGO left JOIN 
			 dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0) left join
			 contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
			 contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
			 WHERE A.ESTSERIPS IN (1,2) and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo
		END
END