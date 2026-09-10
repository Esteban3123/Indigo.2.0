

CREATE PROCEDURE [dbo].[USP_AgendamientoVsCensoHospital]
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL AS DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE 	@FECHA_FINAL AS DATETIME=DATEADD(DAY,1,@FECHA_INICIAL);
WITH CENSO
AS 
(
SELECT	UF.UFUDESCRI AS Servicio, C.DESCCAMAS AS Cama, I.NUMINGRES AS NumeroIngreso, 
        I.IFECHAING AS FechaIngreso, DATEDIFF(d, I.IFECHAING, [Common].[GETDATE]()) AS DiasHospital, E.IPCODPACI AS Identificación, P.IPNOMCOMP AS NombrePaciente, 
        RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, [Common].[GETDATE](), 105)))) AS Edad, 
        CASE WHEN P.IPSEXOPAC = '1' THEN 'Masculino' WHEN P.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, D.CODDIAGNO AS CodDiag, V.NOMDIAGNO AS DiagnosticoPpal, 
        P.IPDIRECCI AS Direccion, P.IPTELEFON AS Telefono, P.IPTELMOVI AS Movil, CONVERT(varchar, E.FECINIEST, 100) AS IngresoCama, 
        DATEDIFF(d, E.FECINIEST, [Common].[GETDATE]()) AS DiasCama, A.NOMENTIDA AS Entidad, I.IAUTORIZA AS NumeroAutorizacion, I.IOBSERVAC AS Observaciones 
    FROM   dbo.CHCAMASHO AS C INNER JOIN 
        dbo.ADCENATEN AS CA ON CA.CODCENATE = C.CODCENATE INNER JOIN 
        dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = C.UFUCODIGO INNER JOIN 
        dbo.CHREGESTA AS E ON E.CODICAMAS = C.CODICAMAS AND E.REGESTADO = '1' INNER JOIN 
        dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN 
        dbo.ADINGRESO AS I ON I.NUMINGRES = E.NUMINGRES INNER JOIN 
        dbo.INPACIENT AS P ON P.IPCODPACI = E.IPCODPACI INNER JOIN
        dbo.INENTIDAD AS A ON A.CODENTIDA = I.CODENTIDA LEFT OUTER JOIN 
        dbo.INDIAGNOP AS D ON D.NUMINGRES = E.NUMINGRES AND D.IPCODPACI = E.IPCODPACI AND D.CODDIAPRI = 'True' LEFT OUTER JOIN 
        dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO 
    WHERE (C.CODCENATE = '001') AND (C.ESTADCAMA = '2')
),
UE
AS
(
SELECT ES.IPCODPACI, ES.NUMINGRES AS NumIngreso, ES.FECINIEST AS InicioEstancia, 
		CASE ES.FECFINEST WHEN '19000101' THEN '30000101' ELSE ES.FECFINEST END FinEstancia, DIAG.NombreDx AS DiagnosticoPpalEstancia,
		Qx.DescripcionQxPpal, Qx.FechaQx 
	FROM dbo.CHREGESTA AS ES LEFT OUTER JOIN
		.HCQXINFOR AS Q ON Q.NUMINGRES=ES.NUMINGRES OUTER APPLY
			(SELECT TOP(1) Q.NUMINGRES AS NumIngreso , Q.IPCODPACI AS IdPaciente, Q.CODSERIPS AS CodProced, 
				PR.DESSERIPS AS DescripcionQxPpal, Q.FECHORFIN AS FechaQx
				FROM .HCQXINFOR AS Q INNER JOIN . INCUPSIPS AS PR ON Q.CODSERIPS = PR.CODSERIPS
				WHERE Q.NUMINGRES=ES.NUMINGRES ORDER BY Q.FECHORINI DESC) AS Qx OUTER APPLY
			(SELECT TOP(1) CIE.NOMDIAGNO AS NombreDx
				FROM dbo.INDIAGNOH AS DX INNER JOIN dbo.INDIAGNOS AS CIE ON DX.CODDIAGNO=CIE.CODDIAGNO
				WHERE DX.CODDIAPRI=1 AND DX.NUMINGRES=ES.NUMINGRES
				ORDER BY DX.NUMEFOLIO DESC) AS DIAG
	WHERE ES.FECFINEST='19000101' OR ES.FECFINEST > DATEADD(DAY,-45,CURRENT_TIMESTAMP)
)
SELECT       
	dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumen, INP.IPCODPACI AS Identificacion, RTRIM(INP.IPNOMCOMP) + CONCAT(' (', INP.IPTELEFON + ' - ', INP.IPTELMOVI,')') AS  Nombre, 
	US.NOMUSUARI AS UsuarioAsigna, AGA.FECHORAIN AS FechaConsulta, DATENAME(MONTH, AGA.FECHORAIN) + '/' + CAST(YEAR(AGA.FECHORAIN) AS varchar) AS MesConsulta,
	DATENAME(YEAR, AGA.FECHORAIN) AS AñoConsulta,
	CASE AGA.CODESTCIT
		WHEN 0 THEN  'Asignada'  
		WHEN 1 THEN  'Cumplida' 
		WHEN 2 THEN  'Incumplida' 
		WHEN 3 THEN  'PreAsignada'
		WHEN 4 THEN  'Cita Cancelada'
	END AS EstadoCita,
	INE.NOMENTIDA AS EntidadPaciente,
	CASE AGA.CODTIPCIT
		WHEN 0 THEN 'Primera Vez' 
		WHEN 1 THEN 'Control' 
		WHEN 2 THEN 'PostOperatorio' 
	END AS Tipo,
	INPR.CODPROSAL AS CodMedico, INPR.NOMMEDICO AS NombreMedico, INES.DESESPECI AS Especialidad, 
	dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, CAST(INP.IPFECNACI as date)AS FNacimiento,
	dbo.Edad(CONVERT(varchar, INP.IPFECNACI, 105),CONVERT(varchar, AGA.FECHORAIN, 105))AS Edad,
	dbo.TipoPaciente(IPTIPOPAC)  AS TipoRegimen,
	INP.IPDIRECCI AS Direccion, INMU.MUNNOMBRE AS Municipio, AGAC.DESACTMED AS Actividad,
	CENSO.Servicio AS Hospitalización, CENSO.Cama, CENSO.DiasHospital, HCP.FECINIATE AS FechaParto, HCP.TERTRAPAR AS TipoParto,
	UE2.InicioEstancia AS InicioEstanciaUltimos45d, UE2.FinEstancia, UE2.DiagnosticoPpalEstancia, UE2.DescripcionQxPpal, UE2.FechaQx
FROM            dbo.INPACIENT AS INP INNER JOIN
	dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA INNER JOIN
	dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI INNER JOIN
	dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL INNER JOIN
	dbo.INESPECIA AS INES ON AGA.CODESPECI= INES.CODESPECI INNER JOIN
	dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI INNER JOIN
	dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD INNER JOIN
	dbo.SEGusuaru AS US ON AGA.CODUSUASI=US.CODUSUARI INNER JOIN 
	dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
	dbo.SEGusuaru AS US2 ON AGA.CANCELUSU = US2.CODUSUARI LEFT OUTER JOIN
	dbo.AGCAUCANC AS CC ON AGA.CODCAUCAN = CC.CODCAUCAN LEFT OUTER JOIN
	dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI AND AGA.IPCODPACI = AGCI.IPCODPACI LEFT OUTER JOIN
	CENSO ON CENSO.Identificación=INP.IPCODPACI LEFT OUTER JOIN
	dbo.HCATINPAR AS HCP ON (HCP.IPCODPACI = INP.IPCODPACI AND HCP.FECINIATE > DATEADD(DAY,-45,@FECHA_INICIAL)) OUTER APPLY
		(SELECT TOP(1) UE.InicioEstancia, CASE UE.FinEstancia WHEN '30000101' THEN NULL ELSE UE.FinEstancia END AS FinEstancia, 
			UE.DiagnosticoPpalEstancia, UE.DescripcionQxPpal, UE.FechaQx
			FROM UE WHERE UE.IPCODPACI=AGA.IPCODPACI ORDER BY UE.FinEstancia DESC ) AS UE2
WHERE AGA.FECHORAIN >= @FECHA_INICIAL AND AGA.FECHORAIN < @FECHA_FINAL AND AGA.CODESTCIT <> 4
ORDER BY AGA.FECHORAIN;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte diario de agendamiento de citas médicas cruzado con el censo hospitalario activo, para una fecha dada. Para cada cita del día (consulta externa, controles, postoperatorios) muestra datos del paciente —cédula, nombre, tipo de documento, edad, sexo, régimen, dirección y municipio—, datos de la cita —médico, especialidad, actividad médica, estado de la cita (asignada, cumplida, incumplida, cancelada, preasignada) y usuario que la asignó—, y datos de hospitalización vigente del mismo paciente —servicio, cama, días hospitalizados, diagnóstico principal, entidad aseguradora, número de ingreso y número de autorización—, además de la última estancia de los 45 días anteriores con su diagnóstico, procedimiento quirúrgico principal y fecha de cirugía, y si aplica, la fecha y tipo de parto reciente. Integra el maestro de camas (CHCAMASHO), los estados de estancia (CHREGESTA), los ingresos (ADINGRESO), la agenda de citas (AGASICITA), el catálogo de pacientes (INPACIENT), entidades aseguradoras (INENTIDAD), profesionales de la salud (INPROFSAL), especialidades (INESPECIA) y diagnósticos CIE-10 (INDIAGNOP/INDIAGNOS), sirviendo como herramienta de gestión hospitalaria para coordinar la atención ambulatoria con el censo de pacientes hospitalizados en tiempo real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AgendamientoVsCensoHospital';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas agendadas para una fecha dada cruzándolas con el censo hospitalario actual y la última estancia/cirugía del paciente en los últimos 45 días, para apoyar la gestión clínico-administrativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha inicial debe ser un día válido; el rango se calcula como [FECHA_INICIAL, FECHA_INICIAL+1).; El centro de atención ''001'' debe existir y tener camas registradas para que el censo retorne datos.; Las funciones escalares dbo.Edad, dbo.TipDocR256, dbo.SexoR256, dbo.TipoPaciente y [Common].[GETDATE] deben existir.; Se requiere acceso a un esquema/base externo no calificado (referenciado como ''.HCQXINFOR'' y ''.INCUPSIPS'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre cubre exactamente un día (24h) a partir de @FECHA_INICIAL.; Las citas canceladas (estado 4) nunca aparecen en el resultado.; El censo se restringe siempre al centro de atención ''001'' y a camas en estado ''2''.; Para cada paciente solo se devuelve una estancia reciente (la de FinEstancia más reciente) y una cirugía (la última por FECHORINI).; La fecha fin ''19000101'' se trata como marcador de estancia abierta y se presenta como NULL.; Los diagnósticos mostrados son siempre el principal del ingreso/atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Censo hospitalario; Cama y servicio de hospitalización; Ingreso/admisión hospitalaria; Estancia del paciente; Diagnóstico principal (CIE-10); Procedimiento quirúrgico (CUPS); Atención de parto; Entidad responsable de pago; Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Tipo de cita (Primera Vez/Control/PostOperatorio); Tipo de régimen del paciente; Autorización de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve citas con FECHORAIN dentro del día solicitado y CODESTCIT distinto de 4 (excluye ''Cita Cancelada''), enriquecidas con datos del paciente, censo y última estancia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.CODCENATE = ''001'' AND C.ESTADCAMA = ''2'' (en CTE CENSO) → Solo se consideran camas del centro ''001'' en estado ''2'' (ocupadas) para construir el censo.; si CHREGESTA.REGESTADO = ''1'' → Solo se toman registros de estancia con estado activo para el censo.; si INDIAGNOP.CODDIAPRI = ''True'' / INDIAGNOH.CODDIAPRI = 1 → Se selecciona el diagnóstico marcado como principal para el ingreso.; si ES.FECFINEST = ''19000101'' OR ES.FECFINEST > hoy-45 días → Se incluyen estancias abiertas (sin fecha fin) o finalizadas en los últimos 45 días.; si FECFINEST = ''19000101'' → Se interpreta como estancia abierta y se sustituye por ''30000101'' para ordenar; al exponer al usuario se muestra como NULL.; si AGA.CODESTCIT IN (0,1,2,3,4) → Se traduce a etiqueta: 0 Asignada, 1 Cumplida, 2 Incumplida, 3 PreAsignada, 4 Cita Cancelada.; si AGA.CODTIPCIT IN (0,1,2) → Se traduce a etiqueta: 0 Primera Vez, 1 Control, 2 PostOperatorio.; si IPSEXOPAC = ''1'' / ''2'' (en CENSO) → Se mapea a ''Masculino'' / ''Femenino''.; si AGA.CODESTCIT <> 4 → Se excluyen las citas canceladas del resultado final.; si HCP.FECINIATE > FECHA_INICIAL-45 días → Solo se asocia información de parto (HCATINPAR) si ocurrió en los últimos 45 días previos a la fecha consultada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.INDIAGNOH; dbo.AGASICITA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.SEGusuaru; dbo.AGACTIMED; dbo.AGCAUCANC; dbo.AGCITESPE; dbo.HCATINPAR; HCQXINFOR; INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoVsCensoHospital';
-- GO
