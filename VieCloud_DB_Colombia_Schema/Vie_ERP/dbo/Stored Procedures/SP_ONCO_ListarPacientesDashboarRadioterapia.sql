-- =============================================
-- Author:		Rafael Patiño
-- Create date: 12/02/2020
-- Description:	Listar pacientes en las pestañas del Dashboard oncologico
-- =============================================
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboarRadioterapia] 
 @CentroAtencion varchar(500),
 @TipoServicio int,
 @Estado int
AS
BEGIN
		select 
			A.ID,ESQUE.ID AS 'IDEsquema',
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			case when  FECHACONTORNEO is Null  then 'Sin Programar Contorneo' Else 'Contorneo programado' End as 'Contorneo',isnull(NOREFIEREFECHACONTOR, 0) as NOREFIEREFECHACONTOR,
			A.CODPROSAL,A.CODDIAGNO,A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, 
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(A.CODCENATEULTIMO) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, A.FECHACONTORNEO,
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,rtrim(ORD.NUMINGRES) as NUMINGRES, rtrim(ORD.UFUCODIGO) as UFUCODIGO, CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			'Autorización' AS Autorizacion,
			ORD.AUTO EntityId,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 and IESTADOIN IN ('','P','B')  ),
			-- AGASICITA (dbo) fue migrada a SchedulingManagement.Appointment (goBookings). Esta SP ya
			-- no cuenta las sesiones cumplidas aqui: deja el marcador '{SESIONES_CUMPLIDAS}' y el
			-- servicio (Indigo.ServicesOncologia.ServicioOncologia.AsignarSesionesRadioterapia) lo
			-- reemplaza por el conteo real via _schedulingPlatformClient, agrupando por
			-- RadiotherapySchemeId (=ESQUE.ID) con estado cumplida ('1').
			Concat('Sesión: ', '{SESIONES_CUMPLIDAS}', '/',  ESQUE.NUMSESION ) as 'Sesion'
			,[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 and IESTADOIN IN ('','P','B')  ),2) AS IconoEscalas
		from HCRADORDEN A 
		    left JOIN HCRADESQUEMAS ESQUE with(nolock) ON A.ID = ESQUE.IDHCRADORDEN 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado 
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = @TipoServicio AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes del dashboard de radioterapia oncológica, mostrando el estado de cada orden de radioterapia junto con su esquema de tratamiento (dosis, sesiones, técnica). Consolida información de múltiples fuentes: datos del paciente (cédula, nombre, edad), profesional tratante, diagnóstico CIE-10, especialidad, procedimiento CUPS con su descripción contractual, centro de atención y origen de la orden (ambulatoria u hospitalaria). Permite filtrar por centro de atención, tipo de servicio del dashboard y estado de la orden, facilitando el seguimiento clínico-operativo de pacientes en proceso de planeación, contorneo o simulación de radioterapia. También calcula el avance de sesiones cumplidas versus las programadas en el esquema, e incluye alertas de factores de riesgo y escalas clínicas del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar pacientes con órdenes de radioterapia para un tablero oncológico, filtrados por tipo de servicio, estado y centros de atención, mostrando esquema, planeación, contorneo, sesiones e indicadores de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El listado de centros de atención debe entregarse como cadena delimitada parseable por dbo.SplitString.; Debe existir correspondencia entre la orden y su pronóstico/autorización (HCORDPRON) para que aparezca en el dashboard.; Debe existir el paciente, profesional, diagnóstico, especialidad, CUPS y centro de atención referenciados por la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes cuyo servicio CUPS pertenezca al tipo de servicio del dashboard solicitado (SERIPSDASH).; Solo se listan órdenes cuyo último centro de atención esté dentro del listado recibido (multi-centro).; Solo se listan órdenes que se encuentren en el estado solicitado.; El ingreso asociado al paciente corresponde a un ingreso de tratamiento especial 4 (radioterapia) y en estado activo/pendiente ('''',''P'',''B'').; La columna Sesion contiene el marcador ''{SESIONES_CUMPLIDAS}'' en vez del conteo real — lo completa Indigo.ServicesOncologia.ServicioOncologia vía la API de Scheduling (AGASICITA fue migrada a SchedulingManagement.Appointment), agrupando por RadiotherapySchemeId con estado cumplida (''1'').; La edad se calcula contra la fecha actual del sistema vía Common.GETDATE.; Las alertas de riesgos y escalas se obtienen mediante la función RiskFactorAlert con tipos 1 y 2 respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Radioterapia; Esquema de tratamiento; Planeación; Contorneo; Sesiones de radioterapia; Diagnóstico; Especialidad; Profesional de salud; Centro de atención; Autorización; Ingreso hospitalario; Procedimiento (CUPS); Factores de riesgo; Escalas clínicas; Dashboard oncológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con los pacientes/órdenes de radioterapia filtrados por SERIPSDASH=@TipoServicio, ESTADO=@Estado y CODCENATEULTIMO dentro de los centros recibidos; la columna Sesion trae el marcador ''{SESIONES_CUMPLIDAS}'' sin reemplazar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FECHAPLANEA es NULL en la orden de radioterapia → Se etiqueta como ''Sin Programar Planeación'' else Se etiqueta como ''Planeación programada''; si FECHACONTORNEO es NULL en la orden de radioterapia → Se etiqueta como ''Sin Programar Contorneo'' else Se etiqueta como ''Contorneo programado''; si MANEXTPRO de la orden = 1 → Origen de la orden se marca como ''Ambulatoria'' else Origen de la orden se marca como ''Hospitalario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE; dbo.RiskFactorAlert; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCRADESQUEMAS; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarRadioterapia';
-- GO
