

/***********************************************************************************************
Modified By: Carlos Pinzon
Date: 2019.03.14
Description: Realizo modificacion a consulta para traer información de pacientes filtrado por grupo Imagenologia
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico]
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
			rtrim(ltrim(I.NOMENTIDA)) as Entidad, E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Urgente' as Prioridad -- Media
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
			 WHERE A.ESTSERIPS=@Estado and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo
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
			E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Urgente' as Prioridad -- Media
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías y otros estudios de imagenología) de pacientes ambulatorios, para mostrarlas en el dashboard médico del módulo de imágenes. Combina información de órdenes de imágenes (AMBORDIMA), datos del paciente (INPACIENT), el ingreso o episodio de atención (ADINGRESO), la unidad funcional o servicio donde está el paciente (INUNIFUNC), el centro de atención (ADCENATEN), el profesional solicitante (INPROFSAL), la entidad aseguradora o pagador (INENTIDAD) y el catálogo de servicios CUPS (INCUPSIPS/INCUPSSUB). Filtra por estado de la orden, centro de atención y subgrupo de imagenología, permitiendo al médico visualizar en tiempo real qué estudios de imágenes están pendientes, en proceso o finalizados para sus pacientes ambulatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas de pacientes ambulatorios para el dashboard médico de imagenología, filtradas por estado de servicio, centros de atención y subgrupo RIS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centro de atención debe ser una cadena parseable por dbo.splitstring con uno o varios códigos válidos de ADCENATEN; Debe existir la función dbo.splitstring; El subgrupo recibido debe corresponder a un IDRISGRIMAGE existente en INCUPSSUB para que se devuelvan filas; Las órdenes deben tener ingreso asociado en ADINGRESO (mismo IPCODPACI y NUMINGRES) y servicio CUPS en INCUPSIPS; El paciente debe estar registrado en INPACIENT con ubicación referenciable en INUBICACI/INMUNICIP/INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven órdenes cuyos centros de atención estén dentro de la lista recibida (multi-centro vía splitstring); El filtro por subgrupo de imagenología (INCUPSSUB.IDRISGRIMAGE) es siempre obligatorio; La modalidad y descripción contractual se cruzan exigiendo coincidencia de IDDESCRIPCIONRELACIONADA (con isnull a 0) entre la orden y HCINTESER; La prioridad siempre se devuelve como ''Urgente'' y la lateralidad como ''No Aplica'' (valores fijos); Solo se incluyen pacientes con ingreso (ADINGRESO) y servicio CUPS (INCUPSIPS) válidos por INNER JOIN; Cuando @Estado = 0 únicamente se consideran estados de servicio 1 y 2; El NIT de la entidad se normaliza eliminando ceros a la izquierda mediante manipulación con replace/ltrim', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente ambulatorio; orden de imágenes diagnósticas; centro de atención; subgrupo de imagenología (RIS); unidad funcional; ingreso/admisión; profesional de la salud; entidad/aseguradora; grupo poblacional; modalidad de servicio; urgencia; ubicación geográfica (municipio/departamento); contrato CUPS-Entidad; lateralidad; prioridad de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Estado <> 0: retorna órdenes ambulatorias de imágenes (AMBORDIMA) cuyo ESTSERIPS=@Estado, CODCENATE en la lista de centros y subgrupo de imagenología (INCUPSSUB.IDRISGRIMAGE) = @SubGrupo, con datos de paciente, ingreso, profesional, entidad, ubicación y modalidad; [RETURN_RESULT] resultset: Cuando @Estado = 0: retorna las mismas órdenes pero filtrando ESTSERIPS IN (1,2), manteniendo los filtros de centros y subgrupo de imagenología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado <> 0 → Filtra órdenes de imágenes ambulatorias por el estado específico recibido (A.ESTSERIPS = @Estado) else Cuando @Estado = 0, devuelve órdenes con A.ESTSERIPS IN (1,2) (estados solicitado/en curso); si E.CODTIPPAC en (1..5) → Mapea el grupo poblacional a etiquetas: 1=Maternas, 2=Menor de 5 Años, 3=Adulto Mayor, 4=Discapacitado, 5=Población General; cualquier otro valor se rotula como ''Población General''; si B.IPSEXOPAC = 1 → Devuelve sexo descriptivo ''M'' else Devuelve ''F''; si IINGREPOR = 1 → Marca la orden como Urgencia = ''S'' else Urgencia = ''N''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INENTIDAD; dbo.INCUPSSUB; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCINTESER; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesAmbulatorioDashboardMedico';
-- GO
