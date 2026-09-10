
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve los datos del paciente
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientByIdentification]
	@Identification varchar(25)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT pat.*,
	CASE pat.IPSEXOPAC WHEN 2 THEN SEP.CONSECUTI ELSE NULL END AS SonNumber,
	EMP.DESEMPRES AS CompanyDesc,
	UBI.UBINOMBRE AS LocationDesc,
	ADA.desactivi AS ActivityDesc,
	GRU.DESGRUPET AS EtGroupDesc,
	CRE.CREDDESCRI AS BeliefDesc,
	DIS.DISCDESCRI AS DisabilityDesc,
	IDI.IDIDESCRI AS LanguageDesc,
	NIV.NIVDESCRI AS LevelDescription,
	NID.NIVEDESCRI AS EducationLevelDesc,
	ESP.GRUPCODIGO AS SpecialGroupDesc,
	ENT.NOMENTIDA AS EntityDescription
	FROM ..INPACIENT pat WITH(NOLOCK)
	LEFT JOIN ..INCONSEPA SEP WITH(NOLOCK) ON SEP.IDDOCUMEN = pat.IPCODPACI AND SEP.IDPOBLACI = '5'
	LEFT JOIN ..ADEMPRESA EMP WITH(NOLOCK) ON pat.CODEMPRES = EMP.CODEMPRES
	LEFT JOIN ..INUBICACI UBI WITH(NOLOCK) ON UBI.AUUBICACI = pat.AUUBICACI
	LEFT JOIN ..ADACTIVID ADA WITH(NOLOCK) ON ADA.codactivi = pat.CODACTIVI
	LEFT JOIN ..ADGRUETNI GRU WITH(NOLOCK) ON GRU.CODGRUPOE = pat.CODGRUPOE
	LEFT JOIN ..ADCREDO CRE WITH(NOLOCK) ON CRE.CREDCODIGO = pat.CREDCODIGO
	LEFT JOIN ..ADDISCAPACI DIS WITH(NOLOCK) ON DIS.DISCCODIGO = pat.DISCCODIGO
	LEFT JOIN ..ADIDIOMA IDI WITH(NOLOCK) ON IDI.IDICODIGO = pat.IDICODIGO
	LEFT JOIN ..ADNIVELES NIV WITH(NOLOCK) ON NIV.NIVCODIGO = pat.NIVCODIGO
	LEFT JOIN ..ADNIVELED NID WITH(NOLOCK) ON NID.NIVECODIGO = pat.NIVECODIGO
	LEFT JOIN ..ADGRUPESP ESP WITH(NOLOCK) ON ESP.GRUPCODIGO = pat.GRUPCODIGO
	LEFT JOIN ..INENTIDAD ENT WITH(NOLOCK) ON ENT.CODENTIDA = pat.CODENTIDA
	WHERE pat.IPCODPACI = @Identification

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Busca y retorna el perfil completo de un paciente a partir de su cédula o documento de identificación. Compone los datos maestros del paciente (tabla INPACIENT) con información descriptiva de múltiples catálogos: empresa o aseguradora, ubicación, actividad de admisión, grupo étnico, creencia o credo, discapacidad, idioma, nivel socioeconómico, nivel educativo, grupo especial y entidad. Adicionalmente, para pacientes de sexo femenino gestantes (población 5), recupera el número de consecutivo del hijo desde INCONSEPA. Se usa en admisión, historia clínica y cualquier proceso que requiera visualizar la ficha completa del paciente por documento, cédula o identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_GetPatientByIdentification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_GetPatientByIdentification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la ficha completa de un paciente por su identificación, enriquecida con descripciones de catálogos relacionados (empresa, ubicación, actividad, etnia, credo, discapacidad, idioma, nivel, educación, grupo especial y entidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La identificación del paciente debe corresponder a un registro existente en INPACIENT (de lo contrario retorna conjunto vacío).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el consecutivo poblacional con IDPOBLACI = ''5'' al cruzar INCONSEPA.; El SonNumber únicamente se entrega cuando el paciente es de sexo 2.; Las relaciones con catálogos son LEFT JOIN: la ausencia de un catálogo no excluye al paciente del resultado.; Consulta de solo lectura (NOLOCK en todas las tablas): puede leer datos no confirmados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Empresa; Ubicación; Actividad; Grupo étnico; Credo/Religión; Discapacidad; Idioma; Nivel educativo; Grupo especial; Entidad; Consecutivo poblacional/Número de hijo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INPACIENT: Devuelve los datos del paciente cuyo IPCODPACI coincide con la identificación recibida, junto con descripciones de catálogos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pat.IPSEXOPAC = 2 (sexo del paciente) → Expone SEP.CONSECUTI como SonNumber (número de hijo/parto) else Devuelve NULL en SonNumber', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INCONSEPA; dbo.ADEMPRESA; dbo.INUBICACI; dbo.ADACTIVID; dbo.ADGRUETNI; dbo.ADCREDO; dbo.ADDISCAPACI; dbo.ADIDIOMA; dbo.ADNIVELES; dbo.ADNIVELED; dbo.ADGRUPESP; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetPatientByIdentification';
-- GO
