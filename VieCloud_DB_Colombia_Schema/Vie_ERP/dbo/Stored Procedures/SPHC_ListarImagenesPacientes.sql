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
		   B.PESO,RTRIM(CA.NOMCENATE) as CentroAtencion, A.FECHASUGE , CASE A.LATERALIDAD WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' else 'No Aplica' END AS 'LATERALIDAD', 
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
	WHERE ((@Estado <> 0 AND A.ESTSERIPS IN (@Estado,IIF(@Estado = 2,8,@Estado))) or (@Estado = 0 AND A.ESTSERIPS IN(1,2))) and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo AND A.SendToInterface = 2
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiología, ecografías, tomografías, resonancias y estudios similares) pendientes o en proceso para un centro de atención y subgrupo de imagen específicos, filtrando por estado de la orden. Integra datos del paciente (nombre, cédula, fecha de nacimiento, sexo, grupo sanguíneo, contacto, localidad y departamento), del ingreso hospitalario o de urgencias (tipo de ingreso, unidad funcional actual, cama asignada, tipo de aislamiento, entidad aseguradora), del servicio solicitado (código CUPS, descripción, prioridad urgente o rutinaria, lateralidad, cantidad, concurrencia), del profesional solicitante, del diagnóstico CIE-10, y de parámetros de tiempos máximos de examen y resultado definidos por imagen. También expone alertas de factores de riesgo y escalas clínicas del paciente, la modalidad de integración con interfaz externa y el color de alerta del tipo de aislamiento, siendo el insumo principal para la bandeja de trabajo de imágenes diagnósticas en el módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas de pacientes filtradas por estado, centro(s) de atención y subgrupo de imagen, enriqueciendo con datos demográficos, ingreso, profesional, entidad, ubicación, alertas y modalidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe ser una cadena que pueda dividirse vía dbo.splitstring (admite múltiples centros separados).; Debe existir el subgrupo en RISGRIMAGE referenciado por INCUPSSUB.IDRISGRIMAGE.; Las órdenes deben tener SendToInterface = 2 para ser consideradas.; El paciente, ingreso, servicio CUPS, profesional y entidad referenciados deben existir (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes con SendToInterface = 2.; El subgrupo de imagen filtrado coincide exactamente con IDRISGRIMAGE del grupo CUPS.; Cuando @Estado=2 los estados 2 y 8 se tratan como equivalentes en el listado.; La unidad solicitante se resuelve usando ISNULL(E.UFUACTPAC, A.UFUCODIGO) priorizando la unidad activa del ingreso.; Se calculan alertas de factores de riesgo y de escalas mediante la función RiskFactorAlert con tipos 1 y 2 respectivamente.; Solo se incluyen modalidades (HCINTESER) cuyo centro pertenece al filtro y cuya descripción relacionada coincide con la de la orden (tratando NULL como 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imagen diagnóstica; Paciente; Ingreso/Admisión; Centro de atención; Unidad funcional; Cama y tipo de aislamiento; Profesional de salud; Diagnóstico; Entidad/Aseguradora; CUPS y subgrupo de imagen; Lateralidad; Prioridad (Urgente/Rutinario); Modalidad de atención; Alertas de factores de riesgo y escalas; Tiempos máximos de realización y de entrega de resultados; Localidad/Departamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve órdenes de imagen cuando A.SendToInterface = 2, A.CODCENATE está en la lista de centros y CS.IDRISGRIMAGE = subgrupo recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado <> 0 → Filtra A.ESTSERIPS IN (@Estado, IIF(@Estado=2,8,@Estado)); es decir, si @Estado=2 incluye también estado 8. else Si @Estado = 0 filtra órdenes con ESTSERIPS IN (1,2).; si @Estado = 2 → Incluye además registros con ESTSERIPS = 8 (equivalencia entre estado 2 y 8).; si A.LATERALIDAD → Traduce 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral, otro=''No Aplica''.; si A.PRISERIPS = ''1'' → Marca prioridad como ''Urgente''. else Prioridad ''Rutinario''.; si B.IPSEXOPAC = 1 → Sexo descripción = ''M''. else Sexo descripción = ''F''.; si E.IINGREPOR = 1 → Urgencia = ''S''. else Urgencia = ''N''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.HCPARALEIMA; dbo.INENTIDAD; dbo.INCUPSSUB; dbo.ADCENATEN; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCINTESER; dbo.CHTIPOSAISLAMIENTOS; dbo.RISGRIMAGE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientes';
-- GO
