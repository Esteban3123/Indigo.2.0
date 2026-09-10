CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesDashboardMedico] 
(
@Estado int,
@CentroAtencion Char(10),
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
			E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Autorización' AS Autorizacion, z.Color
			FROM  dbo.HCORDIMAG  AS A with(nolock) INNER JOIN
			 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
			 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
			 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
			 dbo.INUNIFUNC AS C2 with(nolock) ON isnull(E.UFUACTPAC,A.UFUCODIGO) = C2.UFUCODIGO LEFT OUTER JOIN 
			 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
			 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
			 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO LEFT OUTER JOIN
			 dbo.HCPARALEIMA  AS J with(nolock) ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE = @CentroAtencion INNER JOIN 
			 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA INNER JOIN 
			 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB LEFT JOIN
			 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE LEFT JOIN 
			 contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
			 contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId inner join
			 dbo.INUBICACI  U with(nolock) on U.AUUBICACI   = B.AUUBICACI inner join
			 dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD inner join
			 dbo.INDEPARTA DP with(nolock) on DP.depcodigo = M.DEPCODIGO left join
			 dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0)
			 LEFT JOIN  dbo.CHTIPOSAISLAMIENTOS Z with(nolock) on z.Id = g.CODAISLAM 
			 WHERE A.ESTSERIPS=@Estado and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo
		END
		 --select * from HCORDIMAG
	ELSE
		BEGIN
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
			E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Autorización' AS Autorizacion, z.Color
			FROM  dbo.HCORDIMAG  AS A with(nolock) INNER JOIN
			 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
			 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
			 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
			 dbo.INUNIFUNC AS C2 with(nolock) ON isnull(E.UFUACTPAC,A.UFUCODIGO) = C2.UFUCODIGO LEFT OUTER JOIN 
			 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
			 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
			 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO LEFT OUTER JOIN
			 dbo.HCPARALEIMA  AS J with(nolock) ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE = @CentroAtencion INNER JOIN 
			 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA INNER JOIN 
			 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB LEFT JOIN
			 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE LEFT JOIN 
			 contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
			 contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId inner join
			 dbo.INUBICACI  U with(nolock) on U.AUUBICACI   = B.AUUBICACI inner join
			 dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD inner join
			 dbo.INDEPARTA DP with(nolock) on DP.depcodigo = M.DEPCODIGO left join
			 dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0)
			 LEFT JOIN  dbo.CHTIPOSAISLAMIENTOS Z with(nolock) on z.Id = g.CODAISLAM 
			 WHERE A.ESTSERIPS IN (1,2) and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo
		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias y estudios similares) pendientes o en proceso para el dashboard del médico, filtrando por centro de atención, subgrupo de imagen y estado de la orden. Combina datos del paciente (nombre, cédula, fecha de nacimiento, sexo, grupo sanguíneo, contacto), el ingreso o episodio de atención (tipo de ingreso, urgencia, modalidad, entidad aseguradora), la unidad funcional y cama actual, el profesional solicitante, el diagnóstico CIE-10, el servicio CUPS y sus parámetros de tiempo máximo de examen y entrega de resultado. Existe para alimentar en tiempo real el panel de control de imágenes diagnósticas del médico, permitiéndole ver el estado de cada orden, su prioridad (urgente o rutinaria), lateralidad, concurrencia e información completa del paciente hospitalizado o en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas de pacientes para el dashboard del médico, filtradas por estado, uno o varios centros de atención y subgrupo de riesgo de imagen, enriquecidas con datos demográficos, de ingreso, ubicación, entidad, profesional y aislamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centro de atención puede contener múltiples valores separados, procesados por dbo.splitstring; El subgrupo recibido debe corresponder a INCUPSSUB.IDRISGRIMAGE para filtrar el grupo de imágenes; Debe existir el ingreso del paciente (ADINGRESO) asociado a la orden, y la entidad asociada al ingreso; Debe existir el profesional que solicita la orden en INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes cuyo servicio CUPS pertenece al subgrupo de imagen indicado (CS.IDRISGRIMAGE = @SubGrupo); Las órdenes se restringen siempre a los centros de atención contenidos en el parámetro multi-valor; Cuando no se especifica estado (=0), se asume listar solo órdenes en estados 1 y 2; La unidad solicitante se resuelve con isnull(E.UFUACTPAC, A.UFUCODIGO), priorizando la unidad activa del ingreso; Los parámetros de tiempo máximo y de entrega de imagen se toman de HCPARALEIMA solo para el centro de atención recibido; La modalidad se asocia al servicio para los centros indicados y respetando la descripción contractual relacionada; Se usa NOLOCK en todas las tablas, asumiendo lecturas sucias aceptables para el dashboard', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes diagnósticas; Paciente; Ingreso/Admisión; Centro de atención; Unidad funcional; Cama hospitalaria; Tipo de aislamiento; Profesional de la salud / Médico solicitante; Diagnóstico (CIE); Servicio CUPS; Subgrupo de riesgo de imagen; Entidad/Aseguradora; Lateralidad del estudio; Prioridad (Urgente/Rutinario); Modalidad de atención; Autorización; Tiempo máximo de examen y de entrega de resultado; Alerta de imagen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Cuando @Estado <> 0: retorna órdenes con A.ESTSERIPS = @Estado, A.CODCENATE dentro de los centros de @CentroAtencion y CS.IDRISGRIMAGE = @SubGrupo; [RETURN_RESULT] dbo.HCORDIMAG: Cuando @Estado = 0: retorna órdenes con A.ESTSERIPS IN (1,2), A.CODCENATE dentro de los centros de @CentroAtencion y CS.IDRISGRIMAGE = @SubGrupo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado <> 0 → Filtra órdenes por A.ESTSERIPS = @Estado else Filtra órdenes con A.ESTSERIPS IN (1,2) (pendientes/en proceso); si A.LATERALIDAD = 0/1/2/3 → Traduce a ''No Aplica'' / ''Izquierda'' / ''Derecha'' / ''Ambos'' respectivamente; si A.PRISERIPS = ''1'' → Marca prioridad como ''Urgente'' else Marca prioridad como ''Rutinario''; si B.IPSEXOPAC = 1 → Sexo se describe como ''M'' else Sexo se describe como ''F''; si E.IINGREPOR = 1 → Urgencia = ''S'' else Urgencia = ''N''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.HCPARALEIMA; dbo.INENTIDAD; dbo.INCUPSSUB; dbo.ADCENATEN; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCINTESER; dbo.CHTIPOSAISLAMIENTOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesDashboardMedico';
-- GO
