
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	Listar DashBoard Areas Otros Procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesOtrosProcedimientos]
@CentroAtencion varchar(MAX),
@AreaOtrosProcedimientos Varchar(4)
with recompile AS 
BEGIN

	SET NOCOUNT ON;
	--NO QX
select  I.CODTIPPAC as TipoPoblacion , 
			FECORDMED as FechaOrden,
			CA.CODICAMAS As CodCama,
			rtrim(CA.DESCCAMAS)  AS Cama,
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
			dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 RTRIM(D.CODDIAGNO) + ' - ' + RTRIM(D.NOMDIAGNO) as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 Q.NUMEFOLIO as Folio,
			 CASE WHEN Q.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.AUTO EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion
			,rtrim(Q.OBSSERIPS) as Observacion
			,'NOQX' As Origen, Q.CANSERIPS
from HCORDPRON Q with(nolock) 
		inner join  INPACIENT P with(nolock) on P.IPCODPACI = Q.IPCODPACI 
		INNER JOIN ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		INNER JOIN ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES  AND I.IESTADOIN IN ('','P') 
		LEFT JOIN   CHCAMASHO CA with(nolock) on CA.CODICAMAS = I.CODCAMACT 
		INNER JOIN ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN  INUNIFUNC F with(nolock) on F.UFUCODIGO = I.UFUACTPAC 
		LEFT JOIN INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		LEFT JOIN  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS 
		LEFT JOIN  INDIAGNOS D with(nolock) ON D.CODDIAGNO = Q.CODDIAGNO 
		LEFT JOIN  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		LEFT JOIN  INESPECIA C with(nolock) on C.CODESPECI =I.CODESPTRA  
		INNER JOIN  HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS and  isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		INNER JOIN HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC  
		LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
WHERE Q.ESTSERIPS IN ('1') AND Q.SOLICITASALA = 0 AND S.SERIPSDASH = 12 AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) AND AC.ID = @AreaOtrosProcedimientos 
--Q.ESTSERIPS IN ('1','2') -> Listar los 2 no tiene sentido, cuando llega a estado 2:Confirmado, es cuando el CUPS no se hace en el centro de atención por ende desde la historia clinica los pasamos a este estado 2 directamente
-- cuando el CUPS si lo hacen en el centro de atención este cups desde la orden lo ponemos en estado 1:solicitado y cuando le hacen la HC lo pasamos a estado 2:Confirmado, asi es el deber ser
--Normalmente tienen mala parametrización y por eso hacen listar el estado 2 pero no es el deber ser, el deber ser es que hagan la parametrización correcta, comentario realizado 09-01-2024 juan patiño con davithson garavito
UNION  --QX
select  I.CODTIPPAC as TipoPoblacion ,
			FECORDMED as FechaOrden,
			CA.CODICAMAS As CodCama,
			rtrim(CA.DESCCAMAS)  AS Cama,
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
			dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 RTRIM(D.CODDIAGNO) + ' - ' + RTRIM(D.NOMDIAGNO) as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 Q.NUMEFOLIO as Folio,
			 CASE WHEN Q.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.AUTO EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion 
			,rtrim(Q.OBSSERIPS) as Observacion
			,'QX' As Origen, Q.CANSERIPS
from HCORDPROQ  Q with(nolock) 
		inner join INPACIENT P with(nolock) on P.IPCODPACI = Q.IPCODPACI 
		INNER JOIN ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		INNER JOIN ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES  AND I.IESTADOIN IN ('','P') 
		LEFT JOIN   CHCAMASHO CA with(nolock) on CA.CODICAMAS = I.CODCAMACT 
		INNER JOIN ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN  INUNIFUNC F with(nolock)  on F.UFUCODIGO = I.UFUACTPAC 
		LEFT JOIN INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		LEFT JOIN  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS 
		LEFT JOIN  INDIAGNOS D with(nolock) ON D.CODDIAGNO = Q.CODDIAGNO 
		LEFT JOIN  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		LEFT JOIN INESPECIA C with(nolock) on C.CODESPECI =I.CODESPTRA  
		INNER JOIN  HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS  and isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		INNER JOIN HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC  
		LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN  contract.ContractDescriptions  CD with(nolock)  on CD.Id = CDD.ContractDescriptionId
WHERE Q.ESTSERIPS = '1' AND Q.MANEXTPRO = 0 AND ISNULL(Q.SOLICITASALA,0) = 0 AND S.SERIPSDASH = 12 AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) 
AND AC.ID = @AreaOtrosProcedimientos 
ORDER BY FECORDMED desc
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con órdenes médicas de procedimientos no quirúrgicos y quirúrgicos pendientes (estado solicitado), agrupados por área de ''Otros Procedimientos'' del dashboard de historia clínica. Recibe como parámetros el centro de atención y el área específica, y consolida en un solo resultado la información del paciente (cédula, nombre, fecha de nacimiento, edad), su ingreso activo (número de ingreso, cama, unidad funcional), la orden médica (servicio CUPS, diagnóstico CIE-10, fecha de la orden, prioridad, folio, observaciones, cantidad), el profesional que ordenó, la entidad aseguradora o pagadora, y datos de riesgo social como aislamiento, acompañantes, zona apartada y población especial. Combina órdenes de procedimientos generales (HCORDPRON) con órdenes quirúrgicas (HCORDPROQ) mediante UNION, filtrando únicamente ingresos activos o pendientes, y sirve como fuente del tablero de control (dashboard) para el seguimiento operativo de procedimientos pendientes por ejecutar en hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, para el dashboard de ''Otros Procedimientos'', los pacientes con órdenes médicas pendientes (no quirúrgicas y quirúrgicas) en un área y centros de atención dados, unificando ambos orígenes con datos clínicos y administrativos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.SplitString; Debe existir parametrización en HCAREASD/HCAREASC que asocie el CUPS al área solicitada; Los servicios listados deben tener INCUPSIPS.SERIPSDASH = 12; El ingreso del paciente debe estar en estado '''' o ''P''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes en estado solicitado (ESTSERIPS=''1''); se excluye explícitamente el estado 2 (Confirmado) según comentario del autor; Se excluyen órdenes que ya solicitaron sala (SOLICITASALA<>0); Para órdenes quirúrgicas, se excluyen las marcadas como manejo extra-procedimiento (MANEXTPRO=1); Solo se listan servicios CUPS cuyo SERIPSDASH = 12 (parametrización de dashboard ''Otros Procedimientos''); Se restringe a centros de atención recibidos por parámetro y a un área de procedimientos específica (HCAREASC.ID); Solo ingresos con estado en ('''',''P'') son considerados; El campo Riesgo siempre se devuelve como BIT 0 (no se calcula); El campo Autorización se devuelve siempre con literal ''Autorización''; Cada fila se etiqueta con Origen ''NOQX'' o ''QX'' según la tabla origen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Cama hospitalaria; Unidad funcional; Aislamiento; Diagnóstico; Profesional de la salud; Especialidad; Entidad / aseguradora; Servicio CUPS; Orden médica no quirúrgica; Orden médica quirúrgica; Centro de atención; Población especial; Riesgo de agresividad; Zona apartada; Acompañantes del paciente; Autorización; Folio; Prioridad (urgente/rutinario); Áreas de procedimientos (Dashboard)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPRON: Cuando ESTSERIPS=''1'', SOLICITASALA=0, S.SERIPSDASH=12, centro en lista y área coincide → retorna fila con Origen=''NOQX''; [RETURN_RESULT] dbo.HCORDPROQ: Cuando ESTSERIPS=''1'', MANEXTPRO=0, ISNULL(SOLICITASALA,0)=0, S.SERIPSDASH=12, centro en lista y área coincide → retorna fila con Origen=''QX''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen NOQX: órdenes provienen de HCORDPRON con ESTSERIPS=''1'' y SOLICITASALA=0 → Se listan como procedimientos no quirúrgicos pendientes; si Origen QX: órdenes provienen de HCORDPROQ con ESTSERIPS=''1'', MANEXTPRO=0 y SOLICITASALA=0 → Se listan como procedimientos quirúrgicos pendientes; si PRISERIPS = ''1'' → Prioridad = ''Urgente'' else Prioridad = ''Rutinario''; si IPTIPODOC IN (6,7) → Marca ASMS = 1 (paciente de tipo de documento especial) else ASMS = 0; si Estado del ingreso IESTADOIN IN ('''',''P'') → Solo se consideran ingresos activos o pendientes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.Edad; dbo.TipoAislamiento; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.HCORDPROQ; dbo.INPACIENT; dbo.ADCENATEN; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADACTIVID; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCAREASD; dbo.HCAREASC; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesOtrosProcedimientos';
-- GO
