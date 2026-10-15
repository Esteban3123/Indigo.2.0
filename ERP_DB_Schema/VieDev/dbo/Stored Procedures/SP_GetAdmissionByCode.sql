
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve los datos del ingreso
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAdmissionByCode]
	@numingres varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT TAdmissions.NUMINGRES AS AdmissionCode, 
	TAdmissions.IFECHAING AS AdmissionDate,
	TAdmissions.TIPOINGRE AS AdmissionType,
	TAdmissions.ILIQUIDAC AS LiquidationType,
	TAdmissions.GENCAREGROUP AS AdmissionCaregroupId,
	TAdmissions.ICAUSAING AS AdmissionReason,
	TAdmissions.ITIPORIES AS AdmissionRiskType,
	CONCAT(LTRIM(RTRIM(TCentAtenc.CODCENATE)), ' - ', LTRIM(RTRIM(TCentAtenc.NOMCENATE))) AS AdmissionCentAtencCodeName,
	CONCAT(LTRIM(RTRIM(TUniFunc.UFUCODIGO)), ' - ', (LTRIM(RTRIM(TUniFunc.UFUDESCRI)))) AS AdmissionUniFuncCodeName,
	TAdmissions.IINGREPOR AS PlaceEntry,
	TAdmissions.CODPANATE AS BenefitPlan,
	TAdmissions.IAUTORIZA AS AuthorizationNumber,
	TAdmissions.IPTELEFON AS ResponsiblePhone,
	TAdmissions.IPRNOMBRE AS ResponsibleName,
	TPatient.IPCODPACI AS PatientCode,
	TPatient.IPFECNACI AS PatientBirth,
	TPatient.IPSEXOPAC AS PatientGenus,
	TPatient.IPESTRATO AS PatientEstrato,
	TPatient.IPTIPOPAC AS PatientType,
	TPatient.IPTIPOAFI AS PatientAfiliation,
	TPatient.IPTIPODOC AS PatientDocumentType,
	ISNULL(TNiveles.NIVCODIGO,'') AS NivelCode,
	ISNULL(TNiveles.NIVDESCRI,'') AS NivelName,
	ISNULL(TNiveles.NIVPORCMO,0) AS NivelModeratorSharePercentage,
	ISNULL(TNiveles.NIVPORCOP,0) AS NivelCoPayContribPercentage,
	ISNULL(TNiveles.NIVPORSUB,0) AS NivelCoPaySubsiPercentage,
	ISNULL(TNiveles.NIVPORVIN,0) AS NivelCoPayVincuPercentage,
	ISNULL(TNiveles.NIVSISBEN,0) AS NivelSisben,
	ISNULL(TNiveles.TOPEVECMO,0) AS NivelModeratorShareTop,
	ISNULL(TNiveles.TOPEVECOP,0) AS NivelCoPayContribTop,
	ISNULL(TNiveles.TOPEVESUB,0) AS NivelCoPaySubsibTop,
	ISNULL(TNiveles.TOPEVEVIN,0) AS NivelCoPayVincuTop,
	ISNULL(TNiveles.TOPANUCMO,0) AS NivelModeratorShareTopYear,
	ISNULL(TNiveles.TOPANUCOP,0) AS NivelCoPayContribTopYear,
	ISNULL(TNiveles.TOPANUSUB,0) AS NivelCoPaySubsibTopYear,
	ISNULL(TNiveles.TOPANUVIN,0) AS NivelCoPayVincuTopYear,
	TAdmissions.IESTADOIN AS [Status],
	'' AS EntityCode,
	'' AS EntityName,
	TAdmissions.GENCONENTITY AS EntityId,
	'' AS PatientEntityCode,
	'' AS PatientEntityName,
	TPatient.GENCONENTITY AS PatientEntityId,
	TPatient.IPTELMOVI AS PatientPhone,
	TPatient.GENCAREGROUP AS PatientCareGroupId
	FROM dbo.ADINGRESO TAdmissions WITH(NOLOCK)
	INNER JOIN dbo.INPACIENT TPatient WITH(NOLOCK) ON TAdmissions.IPCODPACI = TPatient.IPCODPACI
	INNER JOIN ..ADNIVELES TNiveles WITH(NOLOCK) ON TPatient.NIVCODIGO = TNiveles.NIVCODIGO
	INNER JOIN ..ADCENATEN TCentAtenc WITH(NOLOCK) ON TAdmissions.CODCENATE = TCentAtenc.CODCENATE
	INNER JOIN ..INUNIFUNC TUniFunc WITH(NOLOCK) ON TAdmissions.UFUCODIGO = TUniFunc.UFUCODIGO
	WHERE LTRIM(RTRIM(TAdmissions.NUMINGRES)) = @numingres
	
END