
/***********************************************************************************************
Modified By: Andres Ramirez
Date: 2020.01.22
Description: Consulta para traer información de pacientes para asignarSala y MWL - Web 
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_ListarImagenesPacientes] 
(
@Estado int,
@CentroAtencion Char(10),
@SubGrupo char(10) = NULL,
@Sala int = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @Sala IS NULL

    -- Insert statements for procedure here
SELECT 
	ROW_NUMBER() OVER (ORDER BY RIS.FECHCREA DESC) AS NumeroFila, RIS.AUTO AS AutoOrden, RIS.ESTADO AS EstadoOrden, RIS.CONCURREN AS Concurrencia, RIS.IDHCORDIMAG, RIS.IDAMBORDIMA, RIS.NUMCONCIT,RIS.FECHCREA,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECORDMED ELSE AM.FECORDMED END AS FechaSolicitud,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.IPCODPACI) ELSE RTRIM(AM.IPCODPACI) END AS CodigoPaciente,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(PHC.IPNOMCOMP) ELSE RTRIM(PAM.IPNOMCOMP) END AS NombrePaciente,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPFECNACI ELSE PAM.IPFECNACI END AS FechaNacimiento,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN [dbo].[Edad](PHC.IPFECNACI,[Common].[GETDATE]()) ELSE [dbo].[Edad](PAM.IPFECNACI,[Common].[GETDATE]()) END AS Edad,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.PESO ELSE PAM.PESO END AS Peso,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPSEXO ELSE PAM.IPSEXO END AS SexoL,--
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPSEXOPAC ELSE PAM.IPSEXOPAC END AS SexoN,--
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPRHSANGR ELSE PAM.IPRHSANGR END AS RH,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPGRUPSAN ELSE PAM.IPGRUPSAN END AS GrupoSanguineo,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPDIRECCI ELSE PAM.IPDIRECCI END AS Direccion,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTELEFON ELSE PAM.IPTELEFON END AS Telefono,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTELMOVI ELSE PAM.IPTELMOVI END AS Movil,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.CORELEPAC ELSE PAM.CORELEPAC END AS Correo,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTIPODOC ELSE PAM.IPTIPODOC END AS TipoDocumento,
	--Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPCODPACI ELSE PAM.IPCODPACI END AS NumeroDocumento,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.UFUACTPAC ELSE IAM.UFUACTPAC END AS CodigoUnidad,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.UFUCODIGO ELSE IAM.UFUCODIGO END AS CodigoUnidad2,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(UHC.UFUDESCRI) ELSE RTRIM(UAM.UFUDESCRI) END AS DescripcionUnidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(UHC2.UFUDESCRI) ELSE RTRIM(UAM2.UFUDESCRI) END AS UnidadSolicitante,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.ESTSERIPS ELSE AM.ESTSERIPS END AS EstadoServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.CODCENATE ELSE AM.CODCENATE END AS CodigoCentro,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.ESTALEIMG ELSE AM.ESTALEIMG END AS EstadoAlerta,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.NUMINGRES ELSE AM.NUMINGRES END AS Ingreso,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.CODSERIPS) + ' - ' + RTRIM(CHC.DESSERIPS) ELSE RTRIM(AM.CODSERIPS) + ' - ' + RTRIM(CAM.DESSERIPS) END AS DescripcionServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.CODSERIPS) ELSE RTRIM(AM.CODSERIPS) END AS CodigoServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.CANSERIPS ELSE AM.CANSERIPS END AS Cantidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN PRHC.NOMMEDICO ELSE PRAM.NOMMEDICO END AS Medico,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.CODCENATE ELSE AM.CODCENATE END AS CodigoCentroAtencion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(CAHC.NOMCENATE) ELSE RTRIM(CAAM.NOMCENATE) END AS CentroAtencion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(EHC.NOMENTIDA) ELSE RTRIM(EAM.NOMENTIDA) END AS DescripcionEntidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.AUTO ELSE AM.AUTO END AS Auto,
	RIS.IDSALA AS IdSala, RIS.FECHORAIN AS FechaInicial, RIS.FECHORAFI AS FechaFinal, 
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.SALA ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.IDSALA ELSE NULL END END AS IdSala, 
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECHORAIN ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.FECHORAIN ELSE NULL END END AS FechaIncial,
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECHORAFI ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.FECHORAFI ELSE NULL END END AS FechaFinal,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.OBSSERIPS ELSE AM.OBSERVACI END AS ObservacionServicio,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN NULL ELSE AM.OBSERVSER END AS ObservacionServicioAmbulatorio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.CODTIPPAC ELSE IAM.CODTIPPAC END AS GrupoPoblacion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN 
		Case IHC.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General' END 
		ELSE Case IAM.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General' END END AS TipoPoblacion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.NUMEFOLIO ELSE NULL END AS Folio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.DESCCAMAS ELSE NULL END AS Cama,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.CODICAMAS ELSE NULL END AS CodigoCama,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.CODAISLAM ELSE 6 END AS TipoAislamiento,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC2.Nombre ELSE 'No Aplica' END AS NombreAislamiento,--
	Case When Ris.IDHCORDIMAG IS NOT NULL THEN DHC.NOMDIAGNO ELSE NULL END AS NombreDiagnostico,--
	0 as Minutos, 
	CAST('' as char) as Barra, 
	--CAST('' as bit) as Marcar,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.IMAREAEXA ELSE TAM.IMAREAEXA END AS TiempoMaximo,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.UNITIEREA ELSE TAM.UNITIEREA END AS UnidadTiempo,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.IMAENTRES ELSE TAM.IMAENTRES END AS TiempoResultado,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.UNITIERES ELSE TAM.UNITIERES END AS UnidadResultado,
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.PRISERIPS when '1' then 'Urgente' when '2' then 'Rutinario' else 'Otro' END ELSE 'Otro' END AS Prioridad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.PRISERIPS when '1' then 'Urgente' when '2' then 'Rutinario' else 'Rutinario' END ELSE 'Rutinario' END AS Prioridad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.LATERALIDAD when 0 then 'No Aplica' when 1 then 'Izquierda' when 2 then 'Derecha' when 3 then 'Ambos' END ELSE 'No Aplica' END AS Lateralidad

FROM RISORDENES AS RIS WITH(nolock)
LEFT OUTER JOIN dbo.HCORDIMAG AS HC WITH(nolock) on HC.AUTO = RIS.IDHCORDIMAG
LEFT OUTER JOIN dbo.AMBORDIMA AS AM WITH(nolock) on AM.AUTO = RIS.IDAMBORDIMA
LEFT OUTER JOIN dbo.AGASICITA AS AG WITH(nolock) on AG.CODAUTONU = RIS.NUMCONCIT

left JOIN dbo.INPACIENT AS PHC with(nolock) on PHC.IPCODPACI = HC.IPCODPACI
left JOIN dbo.INPACIENT AS PAM with(nolock) on PAM.IPCODPACI = AM.IPCODPACI
left JOIN dbo.INCUPSIPS AS CHC with(nolock) ON HC.CODSERIPS = CHC.CODSERIPS
left JOIN dbo.INCUPSIPS AS CAM with(nolock) ON AM.CODSERIPS = CAM.CODSERIPS 
left JOIN dbo.INCUPSSUB AS CSHC with(nolock) ON CSHC.CODGRUSUB = CHC.CODGRUSUB
left JOIN dbo.INCUPSSUB AS CSAM with(nolock) ON CSAM.CODGRUSUB = CAM.CODGRUSUB
left JOIN  dbo.ADINGRESO AS IHC with(nolock) ON HC.IPCODPACI = IHC.IPCODPACI AND HC.NUMINGRES = IHC.NUMINGRES 
left JOIN  dbo.ADINGRESO AS IAM with(nolock) ON AM.IPCODPACI = IAM.IPCODPACI AND AM.NUMINGRES = IAM.NUMINGRES 
left JOIN dbo.ADCENATEN AS CAHC with(nolock) On CAHC.CODCENATE = HC.CODCENATE
left JOIN dbo.ADCENATEN AS CAAM with(nolock) On CAAM.CODCENATE = AM.CODCENATE
left JOIN dbo.INUNIFUNC AS UHC with(nolock) ON IHC.UFUACTPAC = UHC.UFUCODIGO 
left JOIN dbo.INUNIFUNC AS UAM with(nolock) ON IAM.UFUACTPAC = UAM.UFUCODIGO 
left JOIN dbo.INUNIFUNC AS UHC2 with(nolock) ON UHC2.UFUCODIGO = HC.UFUCODIGO
left JOIN dbo.INUNIFUNC AS UAM2 with(nolock) ON UAM2.UFUCODIGO = AM.UFUCODIGO
left JOIN dbo.CHCAMASHO AS TAHC with(nolock) on IHC.CODCAMACT = TAHC.CODICAMAS
left JOIN dbo.CHTIPOSAISLAMIENTOS AS TAHC2 with(nolock) ON TAHC.CODAISLAM = TAHC2.Id
left JOIN dbo.INPROFSAL AS PRHC with(nolock) ON HC.CODPROSAL = PRHC.CODPROSAL  
left JOIN dbo.INPROFSAL AS PRAM with(nolock) ON HC.CODPROSAL = PRAM.CODPROSAL  
left JOIN dbo.INENTIDAD AS EHC with(nolock) ON EHC.CODENTIDA = IHC.CODENTIDA 
left JOIN dbo.INENTIDAD AS EAM with(nolock) ON EAM.CODENTIDA = IAM.CODENTIDA 
left JOIN dbo.HCPARALEIMA AS THC with(nolock) ON HC.CODSERIPS = THC.CODSERIPS and THC.CODCENATE = @CentroAtencion
left JOIN dbo.HCPARALEIMA AS TAM with(nolock) ON AM.CODSERIPS = TAM.CODSERIPS and TAM.CODCENATE = @CentroAtencion--
left JOIN dbo.INDIAGNOS AS DHC with(nolock) ON HC.CODDIAGNO = DHC.CODDIAGNO 

WHERE RIS.ESTADO = @Estado
AND (HC.CODCENATE = @CentroAtencion or AM.CODCENATE = @CentroAtencion)
AND (CSHC.IDRISGRIMAGE = @SubGrupo or CSAM.IDRISGRIMAGE = @SubGrupo)
and RIS.IDSALA IS NULL

 ELSE 

SELECT 
	ROW_NUMBER() OVER (ORDER BY RIS.FECHCREA DESC) AS NumeroFila, RIS.AUTO AS AutoOrden, RIS.ESTADO AS EstadoOrden, RIS.CONCURREN AS Concurrencia, RIS.IDHCORDIMAG, RIS.IDAMBORDIMA, RIS.NUMCONCIT,RIS.FECHCREA,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECORDMED ELSE AM.FECORDMED END AS FechaSolicitud,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.IPCODPACI) ELSE RTRIM(AM.IPCODPACI) END AS CodigoPaciente,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(PHC.IPNOMCOMP) ELSE RTRIM(PAM.IPNOMCOMP) END AS NombrePaciente,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPFECNACI ELSE PAM.IPFECNACI END AS FechaNacimiento,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN [dbo].[Edad](PHC.IPFECNACI,[Common].[GETDATE]()) ELSE [dbo].[Edad](PAM.IPFECNACI,[Common].[GETDATE]()) END AS Edad,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.PESO ELSE PAM.PESO END AS Peso,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPSEXO ELSE PAM.IPSEXO END AS SexoL,--
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPSEXOPAC ELSE PAM.IPSEXOPAC END AS SexoN,--
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPRHSANGR ELSE PAM.IPRHSANGR END AS RH,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPGRUPSAN ELSE PAM.IPGRUPSAN END AS GrupoSanguineo,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPDIRECCI ELSE PAM.IPDIRECCI END AS Direccion,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTELEFON ELSE PAM.IPTELEFON END AS Telefono,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTELMOVI ELSE PAM.IPTELMOVI END AS Movil,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.CORELEPAC ELSE PAM.CORELEPAC END AS Correo,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPTIPODOC ELSE PAM.IPTIPODOC END AS TipoDocumento,
	--Case when RIS.IDHCORDIMAG IS NOT NULL THEN PHC.IPCODPACI ELSE PAM.IPCODPACI END AS NumeroDocumento,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.UFUACTPAC ELSE IAM.UFUACTPAC END AS CodigoUnidad,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.UFUCODIGO ELSE IAM.UFUCODIGO END AS CodigoUnidad2,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(UHC.UFUDESCRI) ELSE RTRIM(UAM.UFUDESCRI) END AS DescripcionUnidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(UHC2.UFUDESCRI) ELSE RTRIM(UAM2.UFUDESCRI) END AS UnidadSolicitante,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.ESTSERIPS ELSE AM.ESTSERIPS END AS EstadoServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.CODCENATE ELSE AM.CODCENATE END AS CodigoCentro,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.ESTALEIMG ELSE AM.ESTALEIMG END AS EstadoAlerta,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.NUMINGRES ELSE AM.NUMINGRES END AS Ingreso,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.CODSERIPS) + ' - ' + RTRIM(CHC.DESSERIPS) ELSE RTRIM(AM.CODSERIPS) + ' - ' + RTRIM(CAM.DESSERIPS) END AS DescripcionServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(HC.CODSERIPS) ELSE RTRIM(AM.CODSERIPS) END AS CodigoServicio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.CANSERIPS ELSE AM.CANSERIPS END AS Cantidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN PRHC.NOMMEDICO ELSE PRAM.NOMMEDICO END AS Medico,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.CODCENATE ELSE AM.CODCENATE END AS CodigoCentroAtencion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(CAHC.NOMCENATE) ELSE RTRIM(CAAM.NOMCENATE) END AS CentroAtencion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN RTRIM(EHC.NOMENTIDA) ELSE RTRIM(EAM.NOMENTIDA) END AS DescripcionEntidad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.AUTO ELSE AM.AUTO END AS Auto,
	RIS.IDSALA AS Sala, RIS.FECHORAIN AS FechaInicial, RIS.FECHORAFI AS FechaFinal, 
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.SALA ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.IDSALA ELSE NULL END END AS Sala, 
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECHORAIN ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.FECHORAIN ELSE NULL END END AS FechaIncial,
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.FECHORAFI ELSE Case When RIS.NUMCONCIT IS NOT NULL THEN AG.FECHORAFI ELSE NULL END END AS FechaFinal,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN HC.OBSSERIPS ELSE AM.OBSERVACI END AS ObservacionServicio,
	Case when RIS.IDHCORDIMAG IS NOT NULL THEN NULL ELSE AM.OBSERVSER END AS ObservacionServicioAmbulatorio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN IHC.CODTIPPAC ELSE IAM.CODTIPPAC END AS GrupoPoblacion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN 
		Case IHC.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General' END 
		ELSE Case IAM.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General' END END AS TipoPoblacion,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN HC.NUMEFOLIO ELSE NULL END AS Folio,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.DESCCAMAS ELSE NULL END AS Cama,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.CODICAMAS ELSE NULL END AS CodigoCama,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC.CODAISLAM ELSE 6 END AS TipoAislamiento,--
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN TAHC2.Nombre ELSE 'No Aplica' END AS NombreAislamiento,--
	Case When Ris.IDHCORDIMAG IS NOT NULL THEN DHC.NOMDIAGNO ELSE NULL END AS NombreDiagnostico,--
	0 as Minutos, 
	CAST('' as char) as Barra, 
	--CAST('' as bit) as Marcar,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.IMAREAEXA ELSE TAM.IMAREAEXA END AS TiempoMaximo,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.UNITIEREA ELSE TAM.UNITIEREA END AS UnidadTiempo,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.IMAENTRES ELSE TAM.IMAENTRES END AS TiempoResultado,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN THC.UNITIERES ELSE TAM.UNITIERES END AS UnidadResultado,
	--Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.PRISERIPS when '1' then 'Urgente' when '2' then 'Rutinario' else 'Otro' END ELSE 'Otro' END AS Prioridad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.PRISERIPS when '1' then 'Urgente' when '2' then 'Rutinario' else 'Rutinario' END ELSE 'Rutinario' END AS Prioridad,
	Case When RIS.IDHCORDIMAG IS NOT NULL THEN Case HC.LATERALIDAD when 0 then 'No Aplica' when 1 then 'Izquierda' when 2 then 'Derecha' when 3 then 'Ambos' END ELSE 'No Aplica' END AS Lateralidad

FROM RISORDENES AS RIS WITH(nolock)
LEFT OUTER JOIN dbo.HCORDIMAG AS HC WITH(nolock) on HC.AUTO = RIS.IDHCORDIMAG
LEFT OUTER JOIN dbo.AMBORDIMA AS AM WITH(nolock) on AM.AUTO = RIS.IDAMBORDIMA
LEFT OUTER JOIN dbo.AGASICITA AS AG WITH(nolock) on AG.CODAUTONU = RIS.NUMCONCIT

left JOIN dbo.INPACIENT AS PHC with(nolock) on PHC.IPCODPACI = HC.IPCODPACI
left JOIN dbo.INPACIENT AS PAM with(nolock) on PAM.IPCODPACI = AM.IPCODPACI
left JOIN dbo.INCUPSIPS AS CHC with(nolock) ON HC.CODSERIPS = CHC.CODSERIPS
left JOIN dbo.INCUPSIPS AS CAM with(nolock) ON AM.CODSERIPS = CAM.CODSERIPS 
left JOIN dbo.INCUPSSUB AS CSHC with(nolock) ON CSHC.CODGRUSUB = CHC.CODGRUSUB
left JOIN dbo.INCUPSSUB AS CSAM with(nolock) ON CSAM.CODGRUSUB = CAM.CODGRUSUB
left JOIN  dbo.ADINGRESO AS IHC with(nolock) ON HC.IPCODPACI = IHC.IPCODPACI AND HC.NUMINGRES = IHC.NUMINGRES 
left JOIN  dbo.ADINGRESO AS IAM with(nolock) ON AM.IPCODPACI = IAM.IPCODPACI AND AM.NUMINGRES = IAM.NUMINGRES 
left JOIN dbo.ADCENATEN AS CAHC with(nolock) On CAHC.CODCENATE = HC.CODCENATE
left JOIN dbo.ADCENATEN AS CAAM with(nolock) On CAAM.CODCENATE = AM.CODCENATE
left JOIN dbo.INUNIFUNC AS UHC with(nolock) ON IHC.UFUACTPAC = UHC.UFUCODIGO 
left JOIN dbo.INUNIFUNC AS UAM with(nolock) ON IAM.UFUACTPAC = UAM.UFUCODIGO 
left JOIN dbo.INUNIFUNC AS UHC2 with(nolock) ON UHC2.UFUCODIGO = HC.UFUCODIGO
left JOIN dbo.INUNIFUNC AS UAM2 with(nolock) ON UAM2.UFUCODIGO = AM.UFUCODIGO
left JOIN dbo.CHCAMASHO AS TAHC with(nolock) on IHC.CODCAMACT = TAHC.CODICAMAS
left JOIN dbo.CHTIPOSAISLAMIENTOS AS TAHC2 with(nolock) ON TAHC.CODAISLAM = TAHC2.Id
left JOIN dbo.INPROFSAL AS PRHC with(nolock) ON HC.CODPROSAL = PRHC.CODPROSAL  
left JOIN dbo.INPROFSAL AS PRAM with(nolock) ON HC.CODPROSAL = PRAM.CODPROSAL  
left JOIN dbo.INENTIDAD AS EHC with(nolock) ON EHC.CODENTIDA = IHC.CODENTIDA 
left JOIN dbo.INENTIDAD AS EAM with(nolock) ON EAM.CODENTIDA = IAM.CODENTIDA 
left JOIN dbo.HCPARALEIMA AS THC with(nolock) ON HC.CODSERIPS = THC.CODSERIPS and THC.CODCENATE = @CentroAtencion
left JOIN dbo.HCPARALEIMA AS TAM with(nolock) ON AM.CODSERIPS = TAM.CODSERIPS and TAM.CODCENATE = @CentroAtencion--
left JOIN dbo.INDIAGNOS AS DHC with(nolock) ON HC.CODDIAGNO = DHC.CODDIAGNO 

WHERE RIS.ESTADO = @Estado
AND (HC.CODCENATE = @CentroAtencion or AM.CODCENATE = @CentroAtencion)
AND (CSHC.IDRISGRIMAGE = @SubGrupo or CSAM.IDRISGRIMAGE = @SubGrupo)
AND RIS.IDSALA = @Sala

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiología, ecografía, tomografía, resonancia, etc.) pendientes o en proceso para un centro de atención, permitiendo filtrar por estado, subgrupo de servicio CUPS y sala de toma. Consolida en un único resultado las órdenes provenientes tanto de pacientes hospitalizados (HCORDIMAG) como ambulatorios (AMBORDIMA), enriqueciendo cada orden con datos demográficos del paciente (nombre, documento, fecha de nacimiento, edad, sexo, grupo sanguíneo, contacto), información del ingreso o cita (ADINGRESO, AGASICITA), descripción del servicio CUPS (INCUPSIPS, INCUPSSUB), unidad funcional, centro de atención, médico solicitante, cama y tipo de aislamiento. Se utiliza en el módulo RIS (Radiología e Imágenes) para la gestión de worklist de modalidades (MWL), asignación de salas y seguimiento del ciclo de vida de cada estudio de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista órdenes de imágenes diagnósticas (hospitalarias y ambulatorias) registradas en RIS para asignación de sala y MWL, unificando datos de paciente, ingreso, servicio, tiempos y aislamiento según el centro de atención y subgrupo de imagen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir y coincidir con CODCENATE de la orden hospitalaria o ambulatoria.; El subgrupo de imagen (IDRISGRIMAGE) debe coincidir con el del servicio CUPS asociado a la orden.; La orden RIS debe tener el estado solicitado.; Cada orden RIS referencia exclusivamente a una orden hospitalaria (IDHCORDIMAG) o ambulatoria (IDAMBORDIMA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se calcula con dbo.Edad sobre la fecha de nacimiento usando Common.GETDATE() (fecha del servidor de aplicación).; Las órdenes ambulatorias siempre tienen prioridad ''Rutinario'' y lateralidad ''No Aplica''.; Cuando la orden no es hospitalaria, los campos de cama, código de cama, folio y diagnóstico salen nulos y el aislamiento se devuelve como 6 / ''No Aplica''.; Sólo se devuelven órdenes cuyo centro de atención coincide con el parámetro y cuyo subgrupo de imagen coincide con el parámetro.; Los parámetros de tiempo (HCPARALEIMA) se obtienen exclusivamente para el centro de atención solicitado.; La numeración de fila se ordena por fecha de creación de la orden RIS descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imagen diagnóstica; Paciente; Ingreso/Admisión; Centro de atención; Unidad funcional; Sala de imágenes; Cita (agendamiento); Cama hospitalaria; Tipo de aislamiento; Diagnóstico; Profesional de salud / médico; Entidad responsable de pago; Servicio CUPS/IPS; Subgrupo CUPS; Grupo poblacional (Maternas, Menor de 5, Adulto Mayor, Discapacitado, Población General); Prioridad (Urgente/Rutinario); Lateralidad; MWL (Modality Worklist); Tiempos máximos de atención y entrega de resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RISORDENES: Devuelve órdenes de imagen filtradas por estado, centro y subgrupo; si no se pasa sala, sólo retorna las que aún no tienen sala asignada (RIS.IDSALA IS NULL); si se pasa sala, retorna sólo las asignadas a esa sala (RIS.IDSALA = @Sala).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Sala IS NULL → Consulta órdenes pendientes de asignar sala (RIS.IDSALA IS NULL). else Consulta órdenes ya asignadas a la sala indicada (RIS.IDSALA = @Sala).; si RIS.IDHCORDIMAG IS NOT NULL → Toma datos de la orden hospitalaria (HCORDIMAG) y sus relaciones (cama, aislamiento, diagnóstico, folio). else Toma datos de la orden ambulatoria (AMBORDIMA); cama, folio, aislamiento y diagnóstico se devuelven nulos o por defecto (TipoAislamiento=6, NombreAislamiento=''No Aplica'').; si CODTIPPAC del ingreso → Mapea el tipo de población: 1=Maternas, 2=Menor de 5 Años, 3=Adulto Mayor, 4=Discapacitado, 5=Población General; cualquier otro valor o nulo => ''Población General''.; si HC.PRISERIPS de la orden hospitalaria → Prioridad: ''1''=Urgente, ''2''=Rutinario, otro=''Rutinario''. else Para órdenes ambulatorias, la prioridad siempre se devuelve como ''Rutinario''.; si HC.LATERALIDAD → Mapea: 0=''No Aplica'', 1=''Izquierda'', 2=''Derecha'', 3=''Ambos''; en órdenes ambulatorias se devuelve ''No Aplica''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RISORDENES; dbo.HCORDIMAG; dbo.AMBORDIMA; dbo.AGASICITA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INCUPSSUB; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.CHTIPOSAISLAMIENTOS; dbo.INPROFSAL; dbo.INENTIDAD; dbo.HCPARALEIMA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ListarImagenesPacientes';
-- GO
