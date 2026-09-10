
CREATE PROCEDURE [Nephrology].[SP_HC_listPatientsMissedRenalReplacementTherapy]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;

		SELECT 
			A.CODAUTONU,
			A.FECHORAIN,
			RTRIM(A.IPCODPACI) AS 'Identificacion paciente', 
			RTRIM(I.IPNOMCOMP) AS 'Nombre Paciente', 
			dbo.EDAD(I.IPFECNACI, Common.GETDATE()) As 'Edad', 
			CONCAT(RTRIM(C.CODCENATE), ' - ', RTRIM(C.NOMCENATE)) AS 'Centro atencion',
			AD.CODTIPPAC AS 'TipoPoblacional',
			CONCAT(RTRIM(D.CODSERIPS), ' - ', RTRIM(D.DESSERIPS)) AS 'Terapia',
			CONCAT(RTRIM(DI.CODDIAGNO), ' - ', rtrim(DI.NOMDIAGNO)) as 'Diagnostico',
			rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',
			RTRIM(AG.DESCRIPSAL) AS SALA,
			RTRIM(AE.DESCREQUI) AS EQUIPO,
			B.CANSERIPS AS SESIONES,
			RTRIM(CAST(1 + (SELECT COUNT(*) FROM dbo.AGASICITA Z WITH(NOLOCK) WHERE Z.IdHCORDPRON = A.IdHCORDPRON AND (Z.FECHORAIN < A.FECHORAIN OR (Z.FECHORAIN = A.FECHORAIN AND Z.CODAUTONU < A.CODAUTONU)) ) AS VARCHAR(3))) AS Sesion,
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 and IESTADOIN IN ('','P','B')) AS 'INGRESO RENAL'
		FROM AGASICITA A
		INNER JOIN HCORDPRON B ON A.IdHCORDPRON = B.AUTO
		INNER JOIN ADINGRESO AD ON B.NUMINGRES = AD.NUMINGRES
		INNER JOIN INPACIENT I ON A.IPCODPACI = I.IPCODPACI
		INNER JOIN ADCENATEN C ON B.CODCENATE = C.CODCENATE
		INNER JOIN INCUPSIPS D ON B.CODSERIPS = D.CODSERIPS
		INNER JOIN INDIAGNOS DI ON DI.CODDIAGNO = B.CODDIAGNO
		INNER JOIN AGENSALAC AG ON A.IDSALA = AG.CODCONCEC
		INNER JOIN AGEQUIPTRA AE ON A.IDEQUIPOTRA = AE.ID
		INNER JOIN INENTIDAD ENT with(nolock) on ENT.CODENTIDA = AD.CODENTIDA 
		WHERE CODESTCIT IN (2,5) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =3 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND A.FECHORAIN >= DATEADD(MONTH, -1, Common.GETDATE()) AND A.FECHORAIN <= Common.GETDATE()
	
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Retorna el listado de pacientes con citas en estado 2 o 5 (inasistidas/canceladas) correspondientes a terapia de reemplazo renal (TIPSOLICITU=3, TIPTRATAMIENTO=3) en los centros de atención indicados, dentro del último mes. Por cada registro muestra datos del paciente, entidad, diagnóstico, sala, equipo, número de sesión calculado secuencialmente y el ingreso renal activo asociado. Sirve para seguimiento de pacientes que no asistieron a sus sesiones de diálisis u otra terapia renal.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar pacientes con citas de terapia de reemplazo renal inasistidas o no atendidas en el último mes para los centros de atención indicados, con datos demográficos, clínicos, de programación e ingreso renal asociado.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena parseable por dbo.splitstring (lista delimitada de códigos de centro).; Deben existir relaciones íntegras entre la cita y sus catálogos (orden, ingreso, paciente, centro, servicio CUPS, diagnóstico, sala, equipo y entidad), pues todos los joins son INNER.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas cuyo estado (CODESTCIT) sea 2 o 5 (citas no atendidas / inasistencias).; Solo se consideran solicitudes con TIPSOLICITU = 3 y tratamientos con TIPTRATAMIENTO = 3 (terapia de reemplazo renal).; El rango temporal está acotado al último mes hasta la fecha actual (DATEADD(MONTH,-1,GETDATE()) hasta GETDATE()).; El centro de atención de la cita debe estar dentro de la lista parametrizada recibida.; La numeración de la sesión se calcula como 1 + cantidad de citas previas de la misma orden (IdHCORDPRON) ordenadas por fecha y CODAUTONU.; El ''INGRESO RENAL'' solo se obtiene de ingresos con TRATAESPECIA = 2 y estado IESTADOIN en ('''',''P'',''B'').', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapia de reemplazo renal; Paciente; Cita médica; Centro de atención; Diagnóstico; Entidad (asegurador); Sala; Equipo de tratamiento; Sesión de terapia; Ingreso renal; Tipo poblacional', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Cuando CODESTCIT IN (2,5) AND TIPSOLICITU=3 AND TIPTRATAMIENTO=3 y la cita está dentro del último mes y su centro pertenece a la lista, se retorna el registro del paciente con la sesión calculada y su ingreso renal vigente.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; Common.GETDATE; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.HCORDPRON; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listPatientsMissedRenalReplacementTherapy';
-- GO
