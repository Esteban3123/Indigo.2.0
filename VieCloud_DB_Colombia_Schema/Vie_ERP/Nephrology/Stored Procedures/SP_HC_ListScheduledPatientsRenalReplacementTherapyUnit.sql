
CREATE PROCEDURE [Nephrology].[SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit]
(
@CentroAtencion varchar(500),
@UnidadFuncional Char(10),
@FechaConsulta as Date
)
AS
BEGIN
	SET NOCOUNT ON;

		SELECT 
			A.CODAUTONU,
			A.FECHORAIN, 
			A.FECHORAFI,
			(select top 1 CODTIPPAC FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 AND IESTADOIN IN ('','P')) AS 'TipoPoblacion',
			RTRIM(A.IPCODPACI) AS 'IPCODPACI', 
			RTRIM(I.IPNOMCOMP) AS 'IPNOMCOMP', 
			RTRIM(IU.UFUDESCRI) AS 'UnidadActual' ,
			RTRIM(AE.DESCREQUI) AS EQUIPO,
			CONCAT(RTRIM(INP.CODPROSAL), ' - ', RTRIM(INP.NOMMEDICO)) AS Profesional,
			rtrim(DI.NOMDIAGNO) as 'Diagnostico',
			RTRIM(IE.DESESPECI) AS 'Especialidad',
			CONCAT(RTRIM(D.CODSERIPS), ' - ', RTRIM(D.DESSERIPS)) AS 'TipoTerapia',
			CONCAT(RTRIM(CAST(1 + ( SELECT COUNT(*) FROM dbo.AGASICITA Z WITH(NOLOCK) WHERE Z.IdHCORDPRON = A.IdHCORDPRON AND (Z.FECHORAIN < A.FECHORAIN OR (Z.FECHORAIN = A.FECHORAIN AND Z.CODAUTONU < A.CODAUTONU)) ) AS VARCHAR(3))), ' de ', B.CANSERIPS) AS SESIONES,
			dbo.EDAD(I.IPFECNACI, Common.GETDATE()) As 'Edad', 
			RTRIM(C.NOMCENATE) AS 'Centro atencion',		
			RTRIM(AG.DESCRIPSAL) AS DESCRIPSAL,	
			B.CANSERIPS AS Sesion,			
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 and IESTADOIN IN ('','P')) AS 'NUMINGRES',			
			'Terapia de reemplazo renal' as 'Tipo de Cita',
			AD.ESCADOWNT ,  AD.ESCARASS, AD.ESCNORPAC, AD.ESCVASPAC, AD.ESCAPAPAC, dbo.PuntajeEscalaDownTon(AD.NUMINGRES, AD.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(AD.NUMINGRES, AD.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(AD.NUMINGRES, AD.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(AD.NUMINGRES, AD.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(AD.NUMINGRES, AD.IPCODPACI) as PUNTAJEAPACHE
		FROM AGASICITA A
		INNER JOIN HCORDPRON B ON A.IdHCORDPRON = B.AUTO		
		INNER JOIN INPACIENT I ON A.IPCODPACI = I.IPCODPACI
		INNER JOIN ADCENATEN C ON B.CODCENATE = C.CODCENATE
		INNER JOIN INCUPSIPS D ON B.CODSERIPS = D.CODSERIPS
		INNER JOIN INDIAGNOS DI ON DI.CODDIAGNO = B.CODDIAGNO
		INNER JOIN AGENSALAC AG ON A.IDSALA = AG.CODCONCEC		
		INNER JOIN AGEQUIPTRA AE ON A.IDEQUIPOTRA = AE.ID
		INNER JOIN INPROFSAL INP ON AG.CODPROSAL = INP.CODPROSAL
		INNER JOIN INESPECIA IE ON INP.CODESPEC1 = IE.CODESPECI
		LEFT JOIN INUNIFUNC IU ON IU.UFUCODIGO = AG.UFUCODIGO
		LEFT JOIN ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES
		WHERE CODESTCIT IN (0,1,3) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =3 AND FORMAT(A.FECHORAIN,'dd/MM/yyyy') = FORMAT(@FechaConsulta,'dd/MM/yyyy') AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lista los pacientes programados en la unidad de terapia de reemplazo renal para una fecha, centro de atención y unidad funcional dados. Recupera datos clínicos y administrativos de la cita: tipo de terapia (CUPS), sesión actual vs. total ordenadas, diagnóstico, especialidad, equipo y sala. Incluye escalas de valoración clínica (Downton, RASS, Norton, VAS, Apache) calculadas mediante funciones escalares, y el número de ingreso activo del paciente filtrado por modalidad hospitalaria (TRATAESPECIA=2).', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes agendados en una fecha específica para sesiones de terapia de reemplazo renal en una unidad funcional, incluyendo datos clínicos, conteo de sesión actual y puntajes de escalas de valoración.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@FechaConsulta debe contener la fecha a consultar; @CentroAtencion debe ser una cadena con uno o más códigos de centro de atención separables por dbo.splitstring; Las citas deben existir en AGASICITA con su orden de prescripción asociada en HCORDPRON', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número de sesión actual se calcula como 1 + cantidad de citas anteriores de la misma orden de prescripción (IdHCORDPRON), ordenadas por FECHORAIN y CODAUTONU; El total de sesiones programadas proviene de HCORDPRON.CANSERIPS; Solo se consideran citas con estado 0, 1 o 3 (excluye estados como cancelada/no atendida); El tipo de cita se etiqueta siempre como ''Terapia de reemplazo renal''; Solo se vincula un ingreso por paciente cuyo tratamiento especial sea 2 y esté en estado activo o pendiente', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapia de reemplazo renal; Cita médica; Sesión de terapia; Orden de prescripción; Paciente; Ingreso/admisión; Tipo de población; Diagnóstico; Especialidad médica; Profesional de la salud; Centro de atención; Unidad funcional; Equipo de tratamiento; Escala Downton (riesgo de caídas); Escala RASS (sedación); Escala Norton (úlceras por presión); Escala VAS (dolor); Escala APACHE; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Devuelve citas cuyo CODESTCIT IN (0,1,3) AND TIPSOLICITU=3 AND TIPTRATAMIENTO=3 AND FECHORAIN coincide con @FechaConsulta y CODCENATE pertenece a la lista de @CentroAtencion', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT IN (0,1,3) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO = 3 → La cita se considera elegible como sesión de terapia de reemplazo renal y se incluye en el resultado else Se excluye del listado; si FORMAT(A.FECHORAIN,''dd/MM/yyyy'') = FORMAT(@FechaConsulta,''dd/MM/yyyy'') → Se incluyen únicamente las citas programadas exactamente en la fecha consultada else Se omiten; si ADINGRESO.TRATAESPECIA = 2 AND IESTADOIN IN ('''',''P'') → Se toma el tipo de población y número de ingreso del ingreso especial activo/pendiente del paciente else TipoPoblacion y NUMINGRES quedan sin valor', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.EDAD; Common.GETDATE; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.HCORDPRON; dbo.INPACIENT; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUNIFUNC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapyUnit';
-- GO
