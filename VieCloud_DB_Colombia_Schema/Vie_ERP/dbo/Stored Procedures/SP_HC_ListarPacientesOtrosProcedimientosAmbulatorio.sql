CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio]
@CentroAtencion varchar(MAX),
@AreaOtrosProcedimientos Varchar(4),
@CodSala varchar(6),
@CodigoProfesionalAsistida as varchar(25),
@FechaFiltro as Date
with recompile AS
BEGIN

	SET NOCOUNT ON;

	if LEN(@CodigoProfesionalAsistida) = 0 SET @CodigoProfesionalAsistida = NULL

	--NO QX
select I.CODTIPPAC as TipoPoblacion , 
			ISNULL(cita.FECHORAIN, Q.FECHAREG) as FechaOrden,
			'' As CodCama,
			''  AS Cama,
			rtrim(F.UFUCODIGO) as CodigoUnidad,
			rtrim(F.UFUDESCRI)  as UnidadFuncionalActual,
			P.IPCODPACI as Identificacion,
			RTRIM(P.IPNOMCOMP) AS Paciente,
			RTRIM(P.IPCODPACI) + ' - '+ RTRIM(P.IPNOMCOMP) AS NombreCompletoPaciente,
			P.IPFECNACI AS FechaNacimiento ,[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(s.CODSERIPS ) as CodigoServicio,
			rtrim(s.CODSERIPS ) + ' - ' +rtrim(s.DESSERIPS) as Servicio,
			rtrim(CDD.Id) As IdDescRelacionada,
			isnull(rtrim(CD.name),'') as DescripcionRelacionada,		
			'' AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 '' as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 '' as Folio,
			 '' AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.ID EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion
			,rtrim(Q.OBSERVACION) as Observacion
			,Q.IDCITA 
			,Q.IDCITA 
			,Rtrim(DESCRIPSAL) as 'Sala', F.UFUTIPUNI
			,Rtrim(isnull(Cita.CODACTMED,'')) as CodigoActividad, q.CANTIDAD as CANSERIPS
		,iif(Q.CODESPECI is null,'',Q.CODESPECI) as CodigoEspecialidad
			--,RTRIM(Q.CODESPECI) as CodigoEspecialidad
from AMBORDOTROSPRO Q 
		inner join INPACIENT P with(nolock)  on P.IPCODPACI = Q.IPCODPACI 
		inner join ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		inner join ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES
		inner join ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN  INUNIFUNC F with(nolock) on F.UFUCODIGO = I.UFUCODIGO 
		left join INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		left join  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS  
		left join  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		left join  INESPECIA C with(nolock) on C.CODESPECI =Q.CODESPECI 
		inner join  HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS and  isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		inner join HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC AND AC.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))  LEFT JOIN 
		contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA LEFT JOIN 
		contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
		left join AGASICITA Cita with(nolock) on cita.CODAUTONU = Q.IDCITA 
		left JOIN AGENSALAC Sala with(nolock) on  sala.CODCONCEC = Cita.IDSALA
		OUTER APPLY (
						SELECT TOP 1 h.INDICAPAC
						FROM HCHISPACA h
						WHERE h.IPCODPACI = Q.IPCODPACI 
						AND h.NUMINGRES = Q.NUMINGRES
						ORDER BY h.FECHISPAC DESC
					) AS UltimoDestino
WHERE (Q.ESTADO = '1' OR UltimoDestino.INDICAPAC = 13)
AND S.SERIPSDASH = 12 
AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))
AND AC.ID  = @AreaOtrosProcedimientos 
AND (@CodigoProfesionalAsistida is null or Q.CODPROSAL = @CodigoProfesionalAsistida)
AND Convert(Date,Q.FECHAREG) = @FechaFiltro
--AND Sala.CODIGSALA = @CodSala  

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con órdenes de otros procedimientos ambulatorios (no quirúrgicos) pendientes de atención para un día específico, filtrando por centro de atención, área clínica y opcionalmente por profesional de salud. Combina datos de la orden ambulatoria (AMBORDOTROSPRO) con la información del paciente, su ingreso, unidad funcional, entidad aseguradora, servicio CUPS/IPS, profesional tratante y especialidad, para presentar en el tablero de trabajo (dashboard) clínico una lista unificada con identificación del paciente, nombre completo, cédula, servicio solicitado, profesional asignado, especialidad, centro de atención, datos de acompañantes y observaciones. Se usa principalmente en el módulo de gestión de agenda y atención ambulatoria para que el personal asistencial visualice los pacientes programados o pendientes de otros procedimientos en el día.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las órdenes ambulatorias de otros procedimientos pendientes/activas de un área y fecha dadas, en uno o varios centros de atención, con datos clínicos y administrativos del paciente para tableros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o más códigos separables por SplitString; Debe existir un área de otros procedimientos válida en HCAREASC; Debe proporcionarse una fecha de filtro para FECHAREG; El paciente debe tener ingreso (ADINGRESO), actividad (ADACTIVID) y unidad funcional (INUNIFUNC) válidos para aparecer', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes cuyo servicio CUPS tenga SERIPSDASH = 12 (dashboard de otros procedimientos); Solo se incluyen órdenes activas (ESTADO=''1'') o cuyo último destino histórico (HCHISPACA.INDICAPAC) sea 13; El centro de atención de la orden debe pertenecer a la lista recibida (multi-centro vía SplitString); El área de otros procedimientos (HCAREASC.ID) debe coincidir con el área solicitada; Solo se consideran órdenes registradas exactamente en la fecha de filtro (FECHAREG); El área HCAREASC debe pertenecer a alguno de los centros de atención recibidos; El emparejamiento HCAREASD se hace por servicio CUPS y descripción relacionada (tratando NULL como 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente ambulatorio; Otros procedimientos; Orden ambulatoria; Centro de atención; Unidad funcional; Sala de atención; Cita médica; Autorización; Especialidad; Profesional de salud; Entidad/Aseguradora; Población especial; Riesgo de agresividad; Acompañantes; Tipo de documento (ASMS); Edad del paciente; Servicio CUPS; Descripción de contrato (CUPS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de resultados con la información de pacientes/órdenes ambulatorias de otros procedimientos que cumplen ESTADO=''1'' o último INDICAPAC=13, SERIPSDASH=12, área y centro indicados, fecha de registro igual al filtro y opcionalmente profesional asignado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Longitud del código del profesional asistido es 0 → Se trata como NULL para no filtrar por profesional; si Q.ESTADO = ''1'' OR último registro HCHISPACA.INDICAPAC = 13 → Se incluye la orden ambulatoria en el listado; si @CodigoProfesionalAsistida es NULL → No se restringe por profesional; en caso contrario se filtra Q.CODPROSAL = profesional indicado; si P.IPTIPODOC IN (6,7) → Se marca el paciente como ASMS = 1 else ASMS = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDOTROSPRO; dbo.INPACIENT; dbo.ADCENATEN; dbo.ADINGRESO; dbo.ADACTIVID; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCAREASD; dbo.HCAREASC; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.AGASICITA; dbo.AGENSALAC; dbo.HCHISPACA; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientosAmbulatorio';
-- GO
