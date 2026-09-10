
CREATE Procedure [dbo].[SPHC_ListarPacientesAtendidosOtrosProcedimientos]
(
@CentroAtencion varchar(MAX),
@AreaOtrosProcedimientos Varchar(4),
@CodSala varchar(6),
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;

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
from AMBORDOTROSPRO Q 
		inner join INPACIENT P with(nolock)  on P.IPCODPACI = Q.IPCODPACI 
		inner join ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		inner join ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES and IESTADOIN in ('','P','B')
		inner join ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN INUNIFUNC F with(nolock) on F.UFUCODIGO = I.UFUCODIGO 
		left join  INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		left join  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS  
		left join  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		left join  INESPECIA C with(nolock) on C.CODESPECI =Q.CODESPECI 
		inner join HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS and  isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
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
WHERE (Q.ESTADO = '2' AND UltimoDestino.INDICAPAC <> '13')
AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))
AND AC.ID  = @AreaOtrosProcedimientos 
AND Convert(Date,Q.FECHAREG) = @FechaInicial

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen órdenes de otros procedimientos (CUPS/IPS) pendientes o en proceso para una fecha, centro de atención y área clínica específicos. Integra datos del ingreso o admisión del paciente (urgencias, hospitalización u otras modalidades), su información demográfica (cédula, nombre, fecha de nacimiento, edad), la entidad aseguradora o pagadora, el profesional de salud ordenante, la especialidad, la unidad funcional donde se encuentra, y el servicio o procedimiento solicitado. Compone además información de acompañantes, población especial, riesgo agregado, zona apartada y si el paciente vive solo, para alimentar listados de trabajo clínico y seguimiento de órdenes de procedimientos en el módulo de historia clínica ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con órdenes de otros procedimientos pendientes de atención en un área y centro(s) específicos para una fecha dada, con datos clínicos, administrativos y de cita asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centro(s) de atención debe poder dividirse mediante SplitString (lista delimitada).; Debe existir el área de otros procedimientos referenciada en HCAREASC.; Las órdenes consultadas deben pertenecer a ingresos con estado en ('''',''P'',''B'') (activos/pendientes).; Debe existir parametrización del área-servicio en HCAREASD vinculada al centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes en estado ''2'' (pendientes/programadas) cuyo paciente no haya tenido como último destino el indicador ''13''.; El filtro por centro de atención siempre se aplica tanto sobre la orden como sobre el área (HCAREASC.CODCENATE).; La coincidencia de área-servicio respeta la descripción relacionada (IDDESCRIPCIONRELACIONADA), tratando NULL como 0.; Solo se considera el último registro histórico de destino del paciente en el ingreso (TOP 1 ORDER BY FECHISPAC DESC).; La fecha de registro de la orden debe coincidir exactamente (a nivel de día) con la fecha inicial proporcionada.; Cuenta de población especial limitada a TIPOPOESPERIES = 1 (riesgo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Orden de otros procedimientos; Cita médica; Sala; Especialidad; Profesional de salud; Entidad/Aseguradora; Servicio CUPS; Autorización; Población especial / riesgo; Acompañantes; Riesgo de agresividad; Zona apartada; Tipo de población; Edad del paciente; Historia clínica (último destino)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AMBORDOTROSPRO: Devuelve solo órdenes con ESTADO=''2'' cuyo último registro de destino en HCHISPACA tenga INDICAPAC distinto de ''13'', filtradas por centro de atención, área y fecha de registro = FechaInicial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Q.ESTADO = ''2'' AND último INDICAPAC del paciente/ingreso <> ''13'' → Se incluye la orden en el resultado else Se excluye; si P.IPTIPODOC IN (6,7) → Marca ASMS=1 (paciente con tipo de documento especial, p.ej. menor sin identificación) else ASMS=0; si Cita asociada existe (cita.CODAUTONU = Q.IDCITA) → Usa FECHORAIN de la cita como FechaOrden else Usa Q.FECHAREG como FechaOrden; si Q.CODESPECI IS NULL → CodigoEspecialidad se devuelve vacío else Se devuelve el código de especialidad de la orden; si IESTADOIN del ingreso IN ('''',''P'',''B'') → El ingreso se considera vigente y se incluye else No se incluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDOTROSPRO; dbo.INPACIENT; dbo.ADCENATEN; dbo.ADINGRESO; dbo.ADACTIVID; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCAREASD; dbo.HCAREASC; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.AGASICITA; dbo.AGENSALAC; dbo.HCHISPACA; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosOtrosProcedimientos';
-- GO
