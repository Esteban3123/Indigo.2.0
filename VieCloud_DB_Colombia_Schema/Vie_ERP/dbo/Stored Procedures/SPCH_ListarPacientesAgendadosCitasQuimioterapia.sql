
CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasQuimioterapia]
(
@centroAtencion char(300),
@UnidadFuncional Char(10),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;

	--Consultamos la citas de quimioterapia
	SELECT A.CODAUTONU AS AUTO,A.TIPTRATAMIENTO,  FECHORAIN, FECHORAFI,A.IDEQUIPOTRA AS IDEQUIPO,E.CODEQUIPO as CODIGOEQUIPO,E.DESCREQUI as EQUIPO,S.CODCONCEC AS IDSALA, S.CODIGSALA as CODIGOSALA, S.CODIGSALA +' - ' + S.DESCRIPSAL  AS SALA ,A.CODACTMED, F.DESACTMED, A.CODESPECI, null AS ID, RTRIM(A.IPCODPACI) AS IPCODPACI,RTRIM(D.IPNOMCOMP) AS IPNOMCOMP,RTRIM(D.IPDIRECCI) AS IPDIRECCI ,RTRIM(D.IPTELEFON) AS TELEFONO ,RTRIM(D.IPTELMOVI) AS CELULAR,CODESTCIT AS ESTADO, CITAEXTRA, 
	RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO',  CASE CODESTCIT WHEN 0 THEN 'Cita asignada a:' WHEN 1 THEN 'Cita cumplida por:' WHEN 2 THEN 'Cita incumplida por:' WHEN 3 THEN 'Cita preasignada a:' END AS 'ESTADO CITA',
	rtrim(OS.Code) + ' - ' + rtrim(OS.Description) as 'Esquema', CODESTCIT , A.IDHCRADESQUEMAS, A.IDHCORDCICLOSD,CONCAT('Ciclo: ' + Rtrim(CICLO.CICLO) ,' - ', 'Día: ' + Rtrim(CICLO.DIA)) AS 'CICLO',
	rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO) as Profesional, 
	rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) as Diagnostico,
	rtrim(ESP.CODESPECI) + ' - ' + rtrim(ESP.DESESPECI) as Especialidad,
	---O.NUMINGRES as NUMINGRES,
	isnull((select top 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')),O.NUMINGRES ) as NUMINGRES,
	(select top 1 CODTIPPAC FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')) as TipoPoblacion,
	rtrim(U.UFUDESCRI) as UnidadActual,CONVERT(BIT,0) AS Riesgo,S.DESCRIPSAL  
	,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, 'Quimioterapia' as 'Tipo de Cita', CONVERT(BIT,0) AS Hemocomponente, 0 as BOLSAS, 0 AS ESTADO, 0 as Factura
	FROM dbo.AGASICITA A with(nolock) INNER JOIN 
	dbo.ADCENATEN C with(nolock) ON A.CODCENATE=C.CODCENATE INNER JOIN 
	dbo.INPACIENT D with(nolock) ON A.IPCODPACI=D.IPCODPACI INNER JOIN 
	dbo.AGACTIMED F with(nolock) ON A.CODACTMED=F.CODACTMED INNER JOIN 
	dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA   INNER JOIN 
	dbo.INUNIFUNC  U with(nolock) ON U.UFUCODIGO  = S.UFUCODIGO  INNER JOIN
	dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA  INNER JOIN 
	dbo.SEGusuaru G with(nolock) ON A.CODUSUASI=G.CODUSUARI INNER JOIN 
	ehr.HCORDCICLOSD CICLO with(nolock) on A.IDHCORDCICLOSD = CICLO.ID  INNER JOIN 
	ehr.HCORDQUIMIO O with(nolock) on O.ID = CICLO.IDHCORDQUIMIO INNER JOIN
	ehr.Schemes OS with(nolock) on O.SchemesId = OS.Id INNER JOIN  
	dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = O.CODDIAGNO  INNER JOIN
	dbo.INPROFSAL P with(nolock) on P.CODPROSAL = O.CODPROSAL INNER JOIN 
	dbo.INESPECIA  ESP with(nolock) on ESP.CODESPECI  = O.CODESPECI  
	WHERE A.CODCENATE=@centroAtencion AND CAST(a.FECHORAIN as DATE) = CAST(@Fecha as DATE) AND CODESTCIT IN(0,1) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =1 

	UNION ALL

	--Consultamos la citas de hemocomponentes
	SELECT DISTINCT A.CODAUTONU AS AUTO,A.TIPTRATAMIENTO,  FECHORAIN, FECHORAFI,A.IDEQUIPOTRA AS IDEQUIPO,E.CODEQUIPO as CODIGOEQUIPO,E.DESCREQUI as EQUIPO,S.CODCONCEC AS IDSALA, S.CODIGSALA as CODIGOSALA, S.CODIGSALA +' - ' + S.DESCRIPSAL  AS SALA ,A.CODACTMED, F.DESACTMED, A.CODESPECI, null AS ID, RTRIM(A.IPCODPACI) AS IPCODPACI,RTRIM(D.IPNOMCOMP) AS IPNOMCOMP,RTRIM(D.IPDIRECCI) AS IPDIRECCI ,RTRIM(D.IPTELEFON) AS TELEFONO ,RTRIM(D.IPTELMOVI) AS CELULAR,CODESTCIT AS ESTADO, CITAEXTRA, 
	RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO',  CASE CODESTCIT WHEN 0 THEN 'Cita asignada a:' WHEN 1 THEN 'Cita cumplida por:' WHEN 2 THEN 'Cita incumplida por:' WHEN 3 THEN 'Cita preasignada a:' END AS 'ESTADO CITA',
	'' as 'Esquema', CODESTCIT , A.IDHCRADESQUEMAS, A.IDHCORDCICLOSD,'' AS 'CICLO',
	rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO)  as Profesional, 
	isnull(rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO), 'NO ESPECIFICADO') as Diagnostico,
	rtrim(ESP.CODESPECI) + ' - ' + rtrim(ESP.DESESPECI) as Especialidad,
	FAC.NUMINGRES as NUMINGRES,
	(select top 1 CODTIPPAC FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')) as TipoPoblacion,
	rtrim(U.UFUDESCRI) as UnidadActual,CONVERT(BIT,0) AS Riesgo,S.DESCRIPSAL  
	,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, 'Tranfusión' as 'Tipo de Cita', CONVERT(BIT,0) AS Hemocomponente, Componentes.BOLSAS, Componentes.ESTADO, CASE WHEN Componentes.BOLSAS > 0 AND Componentes.ESTADO > 0 AND FAC.LIQUIDAR = 1 THEN 1 ELSE 0 END as 'Factura'
	FROM dbo.AGASICITA A with(nolock) LEFT JOIN 
	dbo.ADCONCOEX FAC WITH(NOLOCK) ON CAST(FAC.NUMCONCIT as INT) = A.CODAUTONU  AND FAC.UFUCODIGO = @UnidadFuncional INNER JOIN
	dbo.ADCENATEN C with(nolock) ON A.CODCENATE=C.CODCENATE INNER JOIN 
	dbo.INPACIENT D with(nolock) ON A.IPCODPACI=D.IPCODPACI INNER JOIN 
	dbo.AGACTIMED F with(nolock) ON A.CODACTMED=F.CODACTMED INNER JOIN 
	dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA   INNER JOIN 
	dbo.INUNIFUNC  U with(nolock) ON U.UFUCODIGO  = S.UFUCODIGO  INNER JOIN
	dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA  INNER JOIN 
	dbo.SEGusuaru G with(nolock) ON A.CODUSUASI=G.CODUSUARI LEFT JOIN 
	dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = A.CODDIAGNO LEFT JOIN
	dbo.INPROFSAL P with(nolock) on FAC.CODPROSAL = P.CODPROSAL LEFT JOIN 
	dbo.INESPECIA  ESP with(nolock) on ESP.CODESPECI  = FAC.CODESPECI  
	OUTER APPLY (
	SELECT COUNT(*)as BOLSAS,COUNT(*)as ESTADO FROM HCORHEMBOL M with(nolock) INNER JOIN HCORHEMCO N with(nolock) ON  M.HCORHEMCOID = N.ID AND N.IPCODPACI = A.IPCODPACI WHERE M.CONFRECBOL = 1 AND M.ESTADO = 6 AND N.SOLEXTRAMU = 1 AND N.idAGASICITA = A.CODAUTONU
	)as Componentes
	WHERE A.CODCENATE=@centroAtencion AND CAST(a.FECHORAIN as DATE) = CAST(@Fecha as DATE) AND CODESTCIT = 0 AND TIPSOLICITU = 3 AND TIPTRATAMIENTO = 5 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con citas agendadas para sesiones de quimioterapia y transfusión de hemocomponentes en una fecha, centro de atención y unidad funcional específicos. Combina en un solo resultado dos tipos de cita oncológica: las sesiones de quimioterapia (con su ciclo, día de tratamiento y esquema terapéutico prescrito en la orden de quimioterapia) y las transfusiones de hemocomponentes (con el número de bolsas confirmadas y estado de facturación). Para cada cita devuelve datos del paciente como cédula, nombre, dirección, teléfono y celular; datos de la cita como sala, equipo, actividad médica, modalidad (presencial o teleconsulta), estado y usuario que asignó; y datos clínicos como diagnóstico CIE-10, especialidad, profesional tratante, número de ingreso y tipo de población. Se usa en la agenda diaria del servicio de oncología y hemoterapia para que el personal asistencial vea y gestione los pacientes programados para tratamientos oncológicos en el día.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, para un centro de atención y fecha dados, los pacientes agendados ese día en citas de quimioterapia y de transfusión de hemocomponentes, con su información clínica, de agenda y estado de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADCENATEN.; Debe recibirse una fecha válida para filtrar FECHORAIN.; Para citas de transfusión, la unidad funcional recibida debe corresponder a la usada en ADCONCOEX para enlazar la cuenta de cobro.; Las citas deben tener sala, equipo de traslado, actividad médica, paciente y usuario asignador existentes en sus catálogos (joins INNER).; Las citas de quimioterapia deben tener orden de quimioterapia, ciclo, esquema, diagnóstico, profesional y especialidad registrados en ehr.HCORDQUIMIO y catálogos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas del centro de atención y fecha (FECHORAIN) recibidos como parámetro.; Solo se consideran solicitudes con TIPSOLICITU = 3 (oncología/hemoterapia).; Las citas de quimioterapia listadas solo pueden estar asignadas (0) o cumplidas (1); las de transfusión solo asignadas (0).; El bloque de quimioterapia exige cita ligada a un ciclo (HCORDCICLOSD) y a una orden de quimioterapia con esquema (HCORDQUIMIO/Schemes).; El conteo de bolsas considera únicamente hemocomponentes con CONFRECBOL = 1, ESTADO = 6, SOLEXTRAMU = 1 y vinculados a la cita por idAGASICITA = CODAUTONU.; El ingreso preferente del paciente es el que tenga TRATAESPECIA = 3 e IESTADOIN vacío o ''P''.; Riesgo y Hemocomponente se devuelven siempre como BIT 0 (placeholders).; En quimioterapia BOLSAS, ESTADO y Factura siempre son 0.; La cuenta de cobro (ADCONCOEX) se filtra por la unidad funcional recibida y se enlaza por NUMCONCIT = CODAUTONU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita de quimioterapia; Cita de transfusión / hemocomponentes; Esquema de quimioterapia; Ciclo y día de tratamiento; Sala y equipo de tratamiento; Modalidad presencial/teleconsulta; Estado de cita; Ingreso del paciente; Tipo de población; Diagnóstico CIE; Especialidad y profesional tratante; Bolsas de hemocomponentes confirmadas; Liquidación / facturación de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPTRATAMIENTO = 1 (quimioterapia) y CODESTCIT IN (0,1) → Devuelve la cita con datos del esquema, ciclo y día de quimioterapia (ehr.HCORDQUIMIO/HCORDCICLOSD/Schemes), marcando ''Tipo de Cita'' = ''Quimioterapia''.; si TIPTRATAMIENTO = 5 (transfusión/hemocomponentes) y CODESTCIT = 0 → Devuelve la cita con datos de profesional, especialidad y diagnóstico desde la cuenta de cobro ADCONCOEX y el conteo de bolsas, marcando ''Tipo de Cita'' = ''Tranfusión''.; si Componentes.BOLSAS > 0 AND Componentes.ESTADO > 0 AND FAC.LIQUIDAR = 1 → Marca la cita de transfusión como facturable (Factura = 1). else Factura = 0.; si MODALIDAD = 0 / 1 / otro → Etiqueta la cita como ''Presencial'', ''Teleconsulta'' o cadena vacía respectivamente.; si CODESTCIT = 0/1/2/3 → Traduce el estado a ''Cita asignada/cumplida/incumplida/preasignada a:''.; si Existe ADINGRESO del paciente con TRATAESPECIA = 3 e IESTADOIN IN ('''',''P'') → Usa ese NUMINGRES como ingreso vigente del paciente; si no existe, en quimioterapia usa O.NUMINGRES de la orden, y en transfusión usa FAC.NUMINGRES.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.INUNIFUNC; dbo.AGEQUIPTRA; dbo.SEGusuaru; ehr.HCORDCICLOSD; ehr.HCORDQUIMIO; ehr.Schemes; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.ADCONCOEX; dbo.HCORHEMBOL; dbo.HCORHEMCO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasQuimioterapia';
-- GO
