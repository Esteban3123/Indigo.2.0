

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

	DECLARE @Centros TABLE (CodigoCentro VARCHAR(20) NOT NULL);

    INSERT INTO @Centros
    SELECT Value FROM dbo.splitstring(@CentroAtencion);

    -- Insert statements for procedure here
	IF @Estado <> 0
		BEGIN
			SELECT DISTINCT AUTO, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
			B.IPFECNACI AS FechaNacimiento, E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,
			A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, b.IPDIRECCI AS Direccion,
			b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,rtrim(ltrim(b.CORELEPAC)) as Correo, rtrim(ltrim(H.CODPROSAL)) as CodigoProfesional,
			rtrim(ltrim(H.NOMMEDICO))  AS Medico, 0 as Minutos,CAST('' as char) as Barra,CAST('' as bit) as Marcar, a.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio,
			A.FECRECEXA,RTRIM(I.NOMENTIDA) AS DescripcionEntidad, A.OBSERVSER AS ObservacionServicioAmbulatorio, B.PESO, A.NUMCONCIT,RTRIM(CA.NOMCENATE) as CentroAtencion, CASE A.LATERALIDAD WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' else 'No Aplica' END AS 'LATERALIDAD',
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
			 dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT CodigoCentro FROM @Centros) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0) left join
			 contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
			 contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
			 WHERE A.ESTSERIPS IN (@Estado,IIF(@Estado = 2,8,@Estado)) and A.CODCENATE in (SELECT CodigoCentro FROM @Centros)  and  CS.IDRISGRIMAGE=@subgrupo
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
			A.OBSERVSER AS ObservacionServicioAmbulatorio, B.PESO, A.NUMCONCIT,RTRIM(CA.NOMCENATE) as CentroAtencion, CASE A.LATERALIDAD WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' else 'No Aplica' END AS 'LATERALIDAD',
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
			 WHERE A.ESTSERIPS IN (1,2) and A.CODCENATE in (SELECT CodigoCentro FROM @Centros) and  CS.IDRISGRIMAGE=@subgrupo
		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiología, ecografía, tomografía y similares) de pacientes ambulatorios, combinando datos de la orden con información del paciente, el ingreso, el profesional solicitante, la entidad aseguradora y el centro de atención. Permite filtrar por estado de la orden (pendiente, en proceso, entregada, etc.), por centro de atención y por subgrupo de imagenología (grupo CUPS/IPS), siendo utilizado principalmente por el módulo de imágenes diagnósticas para mostrar la lista de trabajo (worklist) de los servicios de imagen en modalidad ambulatoria. Integra tablas de órdenes ambulatorias (AMBORDIMA), pacientes (INPACIENT), ingresos (ADINGRESO), servicios CUPS (INCUPSIPS), subgrupos de servicios (INCUPSSUB), unidades funcionales (INUNIFUNC), centros de atención (ADCENATEN), profesionales de salud (INPROFSAL) y entidades aseguradoras (INENTIDAD), además de exponer datos como lateralidad del estudio, grupo poblacional, alertas de factores de riesgo y descripción contractual del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas ambulatorias de pacientes filtradas por estado, uno o varios centros de atención y subgrupo de imagenología, devolviendo datos clínicos, demográficos, de entidad y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.splitstring que entregue al menos un código de centro válido.; El subgrupo recibido debe corresponder a un valor de INCUPSSUB.IDRISGRIMAGE (subgrupo de imagenología).; Las órdenes deben tener ingreso (NUMINGRES) y paciente (IPCODPACI) coherentes con ADINGRESO e INPACIENT para satisfacer los INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes ambulatorias cuyo centro de atención esté incluido en la lista de centros recibida.; El filtro por subgrupo de imagenología (INCUPSSUB.IDRISGRIMAGE) es obligatorio en ambas ramas, garantizando que solo se listen servicios de imagenología.; La prioridad reportada se fija siempre como ''Urgente'' (literal), independientemente del dato de la orden.; El NIT de la entidad se normaliza eliminando ceros con espacios y reemplazándolos por ceros (formato canónico).; Cada fila incluye dos llamados a la función escalar RiskFactorAlert para tipos 1 (factores de riesgo) y 2 (escalas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente ambulatorio; Orden de imágenes diagnósticas; Centro de atención; Subgrupo de imagenología (CUPS); Ingreso/Admisión; Unidad funcional solicitante; Entidad responsable de pago; Profesional de la salud / Especialidad; Lateralidad del estudio; Grupo poblacional (maternas, menor de 5 años, adulto mayor, discapacitado, población general); Modalidad de atención; Urgencia del ingreso; Alertas de factores de riesgo y escalas; Contrato/Descripción CUPS por entidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Estado <> 0: devuelve órdenes de AMBORDIMA cuyo ESTSERIPS está en (@Estado, 8 si @Estado=2 si no @Estado), centro en la lista parseada y subgrupo CUPS = @SubGrupo.; [RETURN_RESULT] resultset: Cuando @Estado = 0: devuelve órdenes de AMBORDIMA con ESTSERIPS IN (1,2), centro en la lista parseada y subgrupo CUPS = @SubGrupo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado <> 0 → Filtra por ESTSERIPS IN (@Estado, IIF(@Estado=2,8,@Estado)); si @Estado=2 amplía a estado 8 además del 2. else Si @Estado = 0, filtra por ESTSERIPS IN (1,2) (estados activos por defecto).; si @Estado = 2 → Incluye además registros con ESTSERIPS = 8 (equivalencia funcional entre estados 2 y 8). else Solo se aplica el estado recibido.; si A.LATERALIDAD WHEN 1/2/3/4 → Traduce a ''Izquierda''/''Derecha''/''Bilateral''/''Multilateral'' respectivamente. else Cualquier otro valor se rotula como ''No Aplica''.; si E.CODTIPPAC entre 1 y 5 → Asigna grupo poblacional: Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado o Población General. else Cualquier otro valor se clasifica como ''Población General''.; si IINGREPOR = 1 → Marca el campo Urgencia como ''S''. else Marca Urgencia como ''N''.; si B.IPSEXOPAC = 1 → Devuelve ''M'' como descripción de sexo. else Devuelve ''F''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INENTIDAD; dbo.INCUPSSUB; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCINTESER; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorio';
-- GO
