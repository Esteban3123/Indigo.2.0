CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboarBraquiterapia] 
 @CentroAtencion varchar(500),
 @Estado int,
 @OrdenConCita bit
AS
BEGIN

	 if @OrdenConCita = 0 begin
		select 
			A.ID,
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			A.CODPROSAL,A.CODDIAGNO,/*A.CODCENATE*/A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(ltrim(CE.CODCENATE)) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, 
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ) as NUMINGRES,
			CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			'Autorización' AS Autorizacion,
			ORD.AUTO EntityId,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P', 'B')  ),
			[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')),2) AS IconoEscalas
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado AND A.CITABRAQUIASIGNADA IS NULL
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = 13 AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))

	end else if @OrdenConCita =1 begin

			select 
			A.ID,
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			A.CODPROSAL,A.CODDIAGNO,/*A.CODCENATE*/A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(ltrim(CE.CODCENATE)) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, 
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ) as NUMINGRES,
            CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			-- AGASICITA (dbo) fue migrada a SchedulingManagement.Appointment (goBookings). Esta SP ya
			-- no la consulta directamente: FechaPrimerCita se devuelve NULL y el servicio
			-- (Indigo.ServicesOncologia.ServicioOncologia.AsignarFechaPrimerCitaBraquiterapia) la
			-- completa via _schedulingPlatformClient, agrupando por BrachytherapyOrderId (=A.ID) y
			-- tomando la fecha minima (equivalente al ORDER BY FECHORAIN ASC / TOP 1 original).
			CAST(NULL AS DATETIME) as FechaPrimerCita,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ),
			[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')),2) AS IconoEscalas
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado AND A.CITABRAQUIASIGNADA = 1
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO 
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = 13 AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))

		end

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes del módulo de oncología que tienen órdenes de braquiterapia activas, para poblar el tablero (dashboard) de seguimiento de braquiterapia. Recibe como parámetros el centro de atención, el estado de la orden y un indicador que distingue si se quieren órdenes sin cita de braquiterapia asignada o con cita ya asignada. Para cada paciente devuelve su identificación, nombre, edad, procedimiento CUPS, diagnóstico CIE-10, especialidad, profesional tratante, sede de atención, número de ingreso activo en oncología, estado de planeación y simulación, descripción del contrato relacionada, origen de la orden (ambulatoria u hospitalaria), íconos de riesgo y escalas clínicas; y cuando la orden ya tiene cita asignada, incluye además la fecha de la primera cita de braquiterapia agendada. Combina información de órdenes de radiología (HCRADORDEN), órdenes de procedimientos (HCORDPRON), catálogos de servicios CUPS, datos del paciente, centros de atención, profesionales, diagnósticos, especialidades y descripciones de contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes para el dashboard de braquiterapia, separando órdenes sin cita asignada de aquellas con cita ya programada, filtrando por centros de atención y estado de la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.SplitString; El servicio CUPS asociado debe tener SERIPSDASH = 13 (identificador del dashboard de braquiterapia); Debe existir relación entre la orden de radiología y la orden de procedimiento (HCRADORDEN.IDHCORDPRON = HCORDPRON.AUTO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios marcados como dashboard de braquiterapia (SERIPSDASH = 13); Solo se consideran ingresos con tratamiento especial = 5 y estado en ('''',''P'',''B'') para asociar el NUMINGRES; Se filtran órdenes cuyo último centro de atención está dentro de la lista de centros recibidos; El estado de la orden (HCRADORDEN.ESTADO) debe coincidir con el parámetro @Estado; La edad del paciente se calcula con la fecha actual común mediante dbo.Edad y Common.GETDATE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Braquiterapia; Oncología; Orden de radiología; Planeación de tratamiento; Simulación; Cita médica; Paciente; Diagnóstico; Especialidad; Centro de atención; Profesional de salud; Procedimiento CUPS; Ingreso hospitalario; Atención ambulatoria vs hospitalaria; Factores de riesgo; Autorización; Contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando OrdenConCita=0, devuelve órdenes con CITABRAQUIASIGNADA IS NULL (sin cita asignada); [RETURN_RESULT] resultset: Cuando OrdenConCita=1, devuelve órdenes con CITABRAQUIASIGNADA = 1; FechaPrimerCita se devuelve siempre NULL desde esta SP (columna reservada) — la completa Indigo.ServicesOncologia.ServicioOncologia vía la API de Scheduling (AGASICITA fue migrada a SchedulingManagement.Appointment)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @OrdenConCita = 0 → Consulta órdenes sin cita de braquiterapia asignada (CITABRAQUIASIGNADA IS NULL) else Si @OrdenConCita = 1, consulta órdenes con cita asignada (CITABRAQUIASIGNADA = 1), columna FechaPrimerCita reservada en NULL para completar fuera de la SP; si FECHAPLANEA IS NULL → Marca el estado de planeación como ''Sin Programar Planeación'' else Marca el estado como ''Planeación programada''; si ORD.MANEXTPRO = 1 → Clasifica el origen de la orden como ''Ambulatoria'' else Clasifica el origen de la orden como ''Hospitalario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.Edad; dbo.RiskFactorAlert; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboarBraquiterapia';
-- GO
