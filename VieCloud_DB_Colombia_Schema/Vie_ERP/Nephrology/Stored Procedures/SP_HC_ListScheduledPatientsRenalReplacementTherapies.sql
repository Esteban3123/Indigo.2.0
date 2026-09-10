
CREATE PROCEDURE [Nephrology].[SP_HC_ListScheduledPatientsRenalReplacementTherapies]
(
@CentroAtencion varchar(500),
@FechaConsulta as Date
)
AS
BEGIN
	SET NOCOUNT ON;

		SELECT 
		A.CODAUTONU,
		A.FECHORAIN, 
		A.FECHORAFI, 
		RTRIM(A.IPCODPACI) AS 'Identificacion paciente', 
		RTRIM(I.IPNOMCOMP) AS 'Nombre Paciente', 
		dbo.EDAD(I.IPFECNACI, Common.GETDATE()) As 'Edad', 
		RTRIM(C.NOMCENATE) AS 'Centro atencion',
		CONCAT(RTRIM(D.CODSERIPS), ' - ', RTRIM(D.DESSERIPS)) AS 'Terapia',
	    rtrim(DI.NOMDIAGNO) as 'Diagnostico',
		RTRIM(AG.DESCRIPSAL) AS SALA,
		RTRIM(AE.DESCREQUI) AS EQUIPO,
		B.CANSERIPS AS Sesion,
		CONCAT(RTRIM(CAST(1 + ( SELECT COUNT(*) FROM dbo.AGASICITA Z WITH(NOLOCK) WHERE Z.IdHCORDPRON = A.IdHCORDPRON AND (Z.FECHORAIN < A.FECHORAIN OR (Z.FECHORAIN = A.FECHORAIN AND Z.CODAUTONU < A.CODAUTONU)) ) AS VARCHAR(3))), ' de ', B.CANSERIPS) AS SESIONES,
		(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 and IESTADOIN IN ('','P','B')) AS 'INGRESO RENAL'
		FROM AGASICITA A
		INNER JOIN HCORDPRON B ON A.IdHCORDPRON = B.AUTO
		INNER JOIN INPACIENT I ON A.IPCODPACI = I.IPCODPACI
		INNER JOIN ADCENATEN C ON B.CODCENATE = C.CODCENATE
		INNER JOIN INCUPSIPS D ON B.CODSERIPS = D.CODSERIPS
		INNER JOIN INDIAGNOS DI ON DI.CODDIAGNO = B.CODDIAGNO
		INNER JOIN AGENSALAC AG ON A.IDSALA = AG.CODCONCEC
		INNER JOIN AGEQUIPTRA AE ON A.IDEQUIPOTRA = AE.ID
		WHERE CODESTCIT IN (0,1,3) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =3 AND FORMAT(A.FECHORAIN,'dd/MM/yyyy') = FORMAT(@FechaConsulta,'dd/MM/yyyy') AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consulta los pacientes con terapias de reemplazo renal programadas para una fecha y centro de atención específicos, filtrando citas activas (estados 0, 1, 3) de tipo solicitud 3 y tratamiento 3. Retorna datos del paciente, terapia CUPS, diagnóstico, sala, equipo, número de sesión actual sobre el total ordenado, y el número de ingreso renal activo asociado.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de terapias de reemplazo renal programadas para una fecha y uno o varios centros de atención, mostrando paciente, terapia, sala, equipo, número de sesión e ingreso renal asociado.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de centros de atención llega como cadena delimitada y debe poder ser dividida por dbo.splitstring.; Las citas deben tener sala (IDSALA) y equipo (IDEQUIPOTRA) válidos en AGENSALAC y AGEQUIPTRA respectivamente, así como diagnóstico y servicio IPS válidos.; La orden clínica (HCORDPRON) referenciada por la cita debe existir y contener cantidad de sesiones (CANSERIPS).', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran tratamientos cuyo TIPTRATAMIENTO=3 y TIPSOLICITU=3 (terapia de reemplazo renal).; Se excluyen citas canceladas o en estados distintos a 0,1,3.; El número de sesión se calcula como 1 + cantidad de citas previas de la misma orden (HCORDPRON) ordenadas por FECHORAIN y CODAUTONU, presentado como ''N de Total''.; El ingreso renal se identifica por TRATAESPECIA=2 y estados IESTADOIN '''', ''P'' o ''B''.; La comparación de fechas se hace sin componente de hora (FORMAT dd/MM/yyyy).', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita / agendamiento; Terapia de reemplazo renal; Sesión de terapia; Centro de atención; Sala; Equipo de tratamiento; Diagnóstico; Ingreso renal; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo citas con CODESTCIT IN (0,1,3), TIPSOLICITU=3, TIPTRATAMIENTO=3 y cuya FECHORAIN coincide (formato dd/MM/yyyy) con la fecha consultada y cuyo CODCENATE está en la lista recibida.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT IN (0,1,3) AND TIPSOLICITU=3 AND TIPTRATAMIENTO=3 → La cita se considera una terapia de reemplazo renal programada elegible y se incluye en el listado.; si Existe en ADINGRESO un registro con TRATAESPECIA=2 e IESTADOIN IN ('''',''P'',''B'') para el paciente → Se reporta el primer NUMINGRES como ''INGRESO RENAL'' del paciente. else El campo ''INGRESO RENAL'' queda en NULL.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.HCORDPRON; dbo.INPACIENT; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.ADINGRESO; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListScheduledPatientsRenalReplacementTherapies';
-- GO
