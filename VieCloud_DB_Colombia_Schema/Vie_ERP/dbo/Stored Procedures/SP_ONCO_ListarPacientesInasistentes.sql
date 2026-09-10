/*29/09/2020 - Rafael Patiño - SP para listar citas inasistentes de tratamientos activos */
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesInasistentes]
(
@CentroAtencion varchar(2000),
@TipoTratamiento int
)
AS
BEGIN
	SET NOCOUNT ON;

	

	IF @TipoTratamiento = 1 BEGIN 
		
			SELECT
			Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			Scheme.Code + ' - ' + Scheme.Description as NombreEsquema, 
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			'Ciclo: ' + convert(varchar(20),DiaCiclo.CICLO) + ' día: ' + convert(varchar(20),DiaCiclo.DIA)  as Dia,
			C.FECHORAIN as FechaCita,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C with (nolock)  inner join 
			(
						--select  
						--	(select  top 1 C.CODAUTONU from ehr.HCORDCICLOSD D with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCORDCICLOSD = D.ID where D.IDHCORDQUIMIO = ORD.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
						--	ORD.ID as IDordenQumio, ORD.IPCODPACI 
						--from ehr.HCORDQUIMIO ORD with (nolock)
						--where ORD.ESTADO IN (1,2) AND ORD.ORDENCONCITA = 1
				select  
					(select  top 1 C.CODAUTONU from dbo.AGASICITA C with (nolock) where C.IDHCORDCICLOSD = D.ID AND C.CODESTCIT IN ('0','2','3','4','5') AND D.IDHCORDQUIMIO = ORD.ID order by C.CODAUTONU desc) as IdUltimaCitaTratamiento,
					ORD.ID as IDordenQumio, ORD.IPCODPACI 
					from ehr.HCORDQUIMIO ORD with (nolock) inner join 
					ehr.HCORDCICLOSD D with (nolock) on ORD.ID = D.IDHCORDQUIMIO
					where ORD.ESTADO IN (1,2) AND ORD.ORDENCONCITA = 1 
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) inner join
			 EHR.HCORDQUIMIO Orden with (nolock) on X.IDordenQumio = orden.ID inner join 
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
			 EHR.Schemes Scheme with (nolock) on Scheme.Id = orden.SchemesId inner join
			 INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY inner join 
			 EHR.HCORDCICLOSD DiaCiclo with (nolock) on DiaCiclo.ID = C.IDHCORDCICLOSD 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN  >= DATEADD(month,-12, Common.GETDATE())

	END IF @TipoTratamiento = 2 BEGIN
	   
	   SELECT
	        Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			Esquema.ID as IDEsquemaRadio,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			C.FECHORAIN as FechaCita,
			rtrim(Cups.CODSERIPS) + ' - ' + rtrim(Cups.DESSERIPS) as CUPS,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C with (nolock)  inner join 
			(
						select  
							(select  top 1 C.CODAUTONU from HCRADESQUEMAS E with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCRADESQUEMAS = E.ID where C.IDHCRADESQUEMAS = ESQ.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
							ORD.ID as IDOrdenRadioterapia,
							ESQ.ID as IDEsquema
							from HCRADORDEN ORD with (nolock) INNER JOIN HCRADESQUEMAS ESQ with (nolock) on ORD.ID = ESQ.IDHCRADORDEN 
							--inner join INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = ORD.CODSERIPS and Cups.SERIPSDASH = 6
						   where ORD.ESTADO NOT IN (5,6,7)
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU inner join
			 HCRADORDEN Orden with (nolock) on X.IDOrdenRadioterapia = orden.ID inner join 
			 HCRADESQUEMAS Esquema with (nolock) on X.IDEsquema = Esquema.ID inner join 
			 INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = Orden.CODSERIPS AND Cups.SERIPSDASH = 6 inner join  
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
		     INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN < [Common].[GETDATE]() AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion))

	END IF @TipoTratamiento = 3 BEGIN
	   
	   SELECT 
	        Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			C.FECHORAIN as FechaCita,
			rtrim(Cups.CODSERIPS) + ' - ' + rtrim(Cups.DESSERIPS) as CUPS,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C inner join 
			(
						select  
							(select  top 1 C.CODAUTONU from HCRADORDEN  O with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCRADORDEN = O.ID where C.IDHCRADORDEN = ORD.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
							ORD.ID as IDOrdenBraquiterapia, ORD.IPCODPACI 
							from HCRADORDEN ORD with (nolock)
							--inner join INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = ORD.CODSERIPS and Cups.SERIPSDASH = 13
						   where ORD.ESTADO  NOT IN (5,6,7) 
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU inner join
			 HCRADORDEN Orden with (nolock) on X.IDOrdenBraquiterapia = orden.ID inner join 
			 INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = Orden.CODSERIPS AND Cups.SERIPSDASH = 13 inner join  
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
		     INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN < [Common].[GETDATE]() AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	END

     
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos que tienen citas inasistentes (no asistidas, canceladas o pendientes) dentro de tratamientos activos, filtrando por centro de atención y tipo de tratamiento. Según el tipo seleccionado, consulta órdenes de quimioterapia (HCORDQUIMIO/HCORDCICLOSD), radioterapia (HCRADORDEN/HCRADESQUEMAS) u otros esquemas oncológicos activos, y cruza con el agendamiento de citas (AGASICITA) para identificar la última cita registrada por ciclo o esquema que no fue cumplida. Para cada paciente inasistente retorna su identificación/cédula, nombre, edad, diagnóstico CIE-10, esquema de tratamiento, entidad aseguradora, centro de atención, número de ingreso activo, tipo de paciente e iconos de alerta de riesgos y escalas clínicas. Se usa en el módulo de oncología para seguimiento y gestión de inasistencias en tratamientos de quimioterapia, radioterapia y otros protocolos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con citas inasistentes (estados 0,2,3,4,5) en tratamientos oncológicos activos de quimioterapia, radioterapia o braquiterapia, filtrando por centros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.splitstring; El tipo de tratamiento debe ser 1 (quimioterapia), 2 (radioterapia) o 3 (braquiterapia); Deben existir órdenes oncológicas en estado activo según el tipo: HCORDQUIMIO con ESTADO IN (1,2) y ORDENCONCITA=1 para quimio; HCRADORDEN con ESTADO NOT IN (5,6,7) para radio/braqui', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera citas con estados de inasistencia/pendiente: ''0'',''2'',''3'',''4'',''5''; Solo retorna citas en centros de atención incluidos en la lista del parámetro; El ingreso del paciente se busca en estado activo ('' '',''P'',''B'') y se selecciona el más reciente por IFECHAING; Para quimioterapia se restringe a citas de los últimos 12 meses; para radioterapia/braquiterapia a citas anteriores a la fecha actual; TRATAESPECIA identifica el tipo de tratamiento del ingreso: 3=quimio, 4=radio, 5=braqui; SERIPSDASH=6 corresponde a CUPS de radioterapia y SERIPSDASH=13 a braquiterapia; Estados de orden de radioterapia/braquiterapia 5,6,7 se consideran no activos y se excluyen; Estados de orden de quimioterapia activos son 1 y 2, y deben tener ORDENCONCITA=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; cita inasistente; tratamiento oncológico; quimioterapia; radioterapia; braquiterapia; ciclo de tratamiento; esquema de tratamiento; diagnóstico; centro de atención; entidad/administradora de salud; ingreso hospitalario; CUPS; factores de riesgo; escalas de riesgo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @TipoTratamiento=1 retorna citas inasistentes de quimioterapia tomando la última cita por orden activa, filtrando CODESTCIT IN (''0'',''2'',''3'',''4'',''5'') y FECHORAIN >= últimos 12 meses; [RETURN_RESULT] resultset: Cuando @TipoTratamiento=2 retorna citas inasistentes de radioterapia (CUPS con SERIPSDASH=6), filtrando CODESTCIT IN (''0'',''2'',''3'',''4'',''5'') y FECHORAIN < fecha actual; [RETURN_RESULT] resultset: Cuando @TipoTratamiento=3 retorna citas inasistentes de braquiterapia (CUPS con SERIPSDASH=13), filtrando CODESTCIT IN (''0'',''2'',''3'',''4'',''5'') y FECHORAIN < fecha actual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoTratamiento = 1 → Consulta órdenes de quimioterapia (HCORDQUIMIO) en estado 1 o 2 con ORDENCONCITA=1 y obtiene la última cita por ciclo/día con ventana de últimos 12 meses; usa TRATAESPECIA=3 para ingreso; si @TipoTratamiento = 2 → Consulta órdenes de radioterapia (HCRADORDEN con esquemas HCRADESQUEMAS) cuyo estado no esté en (5,6,7), filtrando CUPS con SERIPSDASH=6 y TRATAESPECIA=4 para ingreso; si @TipoTratamiento = 3 → Consulta órdenes de braquiterapia (HCRADORDEN) cuyo estado no esté en (5,6,7), filtrando CUPS con SERIPSDASH=13 y TRATAESPECIA=5 para ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.Edad; Common.GETDATE; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADINGRESO; ehr.HCORDQUIMIO; ehr.HCORDCICLOSD; EHR.Schemes; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADCENATEN; Contract.HealthAdministrator; dbo.HCRADORDEN; dbo.HCRADESQUEMAS; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesInasistentes';
-- GO
