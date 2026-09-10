CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesRedireccionamiento] 
(
@Estado int,
@CentroAtencion Char(500),
@SubGrupo char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
		BEGIN
			
			
			SELECT distinct AUTO,CAST('' as bit) as Marcar,  A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
			RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, A.UFUCODIGO AS CodigoUnidad, RTRIM(C2.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, 
			A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, A.NUMEFOLIO AS Folio,RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, 
			A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) AS NombreDiagnostico, G.DESCCAMAS AS Cama, g.CODAISLAM AS TipoAislamiento, 
			b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo, b.IPTELMOVI as Movil ,rtrim(ltrim(b.CORELEPAC)) as Correo,
			rtrim(ltrim(H.CODPROSAL)) as CodigoProfesional,  rtrim(ltrim(H.NOMMEDICO))  AS Medico, J.IMAREAEXA AS TiempoMaximo, J.UNITIEREA  AS UnidadTiempo, 0 as Minutos,CAST('' as char) as Barra,
			J.IMAENTRES as TiempoResultado,J.UNITIERES as UnidadResultado,a.CONCURRE as Concurrencia,a.SERREAINT as RealizaInterfaz,a.CANSERIPS AS Cantidad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
			B.PESO,RTRIM(CA.NOMCENATE) as CentroAtencion, A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD', 
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) As Edad, CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad,rtrim(ltrim(CA.CODCENATE)) as CodigoCentroAtencion, B.IPTIPODOC as TipoIdentificacion,
			rtrim(ltrim(B.IPPRIAPEL)) as PrimerApellido,rtrim(ltrim(B.IPSEGAPEL)) as SegundoApellido, rtrim(ltrim(B.IPPRINOMB)) as PrimerNombre, rtrim(ltrim(B.IPSEGNOMB)) as SegunNombre, B.IPFECNACI as fechaNacimiento, 
			case B.IPSEXOPAC when 1 then 'M' else 'F' end SexoDescripcion, B.IPDIRECCI as Direccion, rtrim(ltrim(U.UBINOMBRE)) as NombreLocalidad, rtrim(ltrim(M.MUNCODIGO)) CodigoLocalidad, 
			rtrim(ltrim(DP.depcodigo)) as CodigoDpto, rtrim(ltrim(Dp.nomdepart)) as NombreDpto, rtrim(ltrim(B.IPTELEFON)) as Telefono,  rtrim(ltrim(B.IPTELMOVI)) as Celular, rtrim(ltrim(B.CORELEPAC)) as Correo,
			rtrim(ltrim(A.OBSSERIPS)) as Observacion, H.CODESPEC1 as Especialidad,replace(ltrim(replace(K.CODIGONIT,'0',' ')),' ','0') as NitEntidad, rtrim(ltrim(K.NOMENTIDA)) as Entidad, 
			E.TIPOINGRE as TipoIngreso, case IINGREPOR when 1 then 'S' else 'N' END as Urgencia, MO.TIPMODALI as Modalidad, 'Autorización' AS Autorizacion, z.Color
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
						LEFT JOIN  dbo.CHTIPOSAISLAMIENTOS Z with(nolock) on z.Id = g.CODAISLAM 
						left join dbo.HCINTESER MO with(nolock) on MO.CODSERIPS = A.CODSERIPS AND MO.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and isnull(MO.IDDESCRIPCIONRELACIONADA,0) = isnull(A.IDDESCRIPCIONRELACIONADA,0)
			 WHERE A.ESTSERIPS=@Estado and A.SendToInterface = 0 and A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and  CS.IDRISGRIMAGE=@subgrupo
		END	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, etc.) pendientes de redireccionamiento o envío a interfaz externas, para un centro de atención y subgrupo de imagen específicos. Combina datos del paciente (nombre, documento, fecha de nacimiento, sexo, grupo sanguíneo, dirección, contacto), del ingreso hospitalario (unidad funcional, cama, tipo de ingreso, urgencia, modalidad, entidad aseguradora), del profesional solicitante, del diagnóstico CIE-10 y de los parámetros de tiempo máximo de examen y resultado configurados por centro. Se utiliza en la pantalla de redireccionamiento de imágenes para que el personal de radiología o admisiones identifique qué estudios están en estado determinado, aún no enviados a la interfaz, y pueda gestionarlos o marcarlos para su despacho al servicio de imágenes correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista órdenes de imágenes diagnósticas pendientes de un paciente, filtradas por estado, centro(s) de atención y subgrupo de riesgo, para su redireccionamiento o gestión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención puede recibir múltiples valores separados que se descomponen mediante dbo.splitstring.; Debe existir relación válida del ingreso (IPCODPACI + NUMINGRES) entre HCORDIMAG y ADINGRESO.; El servicio (CODSERIPS) debe estar registrado en INCUPSIPS y pertenecer a un subgrupo (CODGRUSUB) en INCUPSSUB.; El profesional solicitante (CODPROSAL) debe existir en INPROFSAL.; La entidad pagadora del ingreso debe existir en INENTIDAD.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes que aún no han sido enviadas a interfaz (SendToInterface=0).; Se filtra estrictamente por el estado de servicio solicitado (ESTSERIPS=@Estado).; Las órdenes deben pertenecer a uno de los centros de atención indicados en la lista parseada.; Las órdenes se restringen al subgrupo de riesgo de imagen indicado (IDRISGRIMAGE=@SubGrupo).; Los parámetros de tiempo máximo y de resultado (HCPARALEIMA) se buscan únicamente para el centro de atención recibido.; La modalidad (HCINTESER) se considera solo si pertenece a los centros parseados y coincide la descripción contractual relacionada (IDDESCRIPCIONRELACIONADA).; Los resultados son distintos (DISTINCT) y la edad se calcula con la fecha actual del sistema mediante Common.GETDATE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imagen diagnóstica; Paciente; Ingreso/Admisión; Unidad funcional; Cama hospitalaria; Tipo de aislamiento; Diagnóstico (CIE); Profesional de la salud; Especialidad médica; Entidad pagadora/aseguradora; Servicio CUPS; Subgrupo de riesgo de imagen; Centro de atención; Lateralidad; Prioridad (Urgente/Rutinario); Modalidad de atención; Tiempo máximo de realización y de resultado; Concurrencia; Autorización; Redireccionamiento de imágenes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve órdenes de imágenes con ESTSERIPS=@Estado, SendToInterface=0, CODCENATE dentro de la lista @CentroAtencion y cuyo servicio pertenezca al subgrupo de riesgo de imagen IDRISGRIMAGE=@SubGrupo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.LATERALIDAD = 0/1/2/3 → Se traduce a ''No Aplica'' / ''Izquierda'' / ''Derecha'' / ''Ambos'' respectivamente para el campo LATERALIDAD.; si A.PRISERIPS = ''1'' → La prioridad se reporta como ''Urgente''. else La prioridad se reporta como ''Rutinario''.; si B.IPSEXOPAC = 1 → SexoDescripcion = ''M''. else SexoDescripcion = ''F''.; si E.IINGREPOR = 1 → Marca Urgencia = ''S''. else Marca Urgencia = ''N''.; si E.UFUACTPAC es NULL → Se utiliza A.UFUCODIGO como unidad funcional para resolver la descripción de la unidad solicitante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.HCPARALEIMA; dbo.INENTIDAD; dbo.INCUPSSUB; dbo.ADCENATEN; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.CHTIPOSAISLAMIENTOS; dbo.HCINTESER', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamiento';
-- GO
