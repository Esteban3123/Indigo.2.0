
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de un ingreso o admisión a partir de su número de ingreso. Consolida en una sola consulta los datos del episodio de atención (fecha, tipo, causa, estado, autorización, plan de beneficios), la información del paciente (cédula, fecha de nacimiento, sexo, estrato, tipo de afiliación, teléfono), el centro de atención donde ocurrió el ingreso, la unidad funcional o servicio asignado, y los niveles de copago y cuotas moderadoras aplicables según el nivel del paciente. Es el punto de entrada principal para obtener el resumen de una hospitalización, urgencia o consulta externa identificada por su número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_GetAdmissionByCode';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_GetAdmissionByCode';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la información consolidada de un ingreso/admisión (datos del ingreso, paciente, nivel, centro de atención y unidad funcional) a partir del código de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de ingreso debe existir en ADINGRESO (comparado con LTRIM/RTRIM); El paciente asociado debe existir en INPACIENT; El paciente debe tener un nivel válido en ADNIVELES; El ingreso debe tener centro de atención válido en ADCENATEN y unidad funcional válida en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos de centro de atención y unidad funcional se devuelven concatenados como ''CODIGO - NOMBRE'' con espacios recortados; Los porcentajes y topes del nivel (cuota moderadora, copago contributivo/subsidiado/vinculado, sisbén) se devuelven como 0 si son NULL; El código y nombre del nivel se devuelven como cadena vacía si son NULL; EntityCode, EntityName, PatientEntityCode y PatientEntityName siempre se devuelven como cadena vacía; Solo se retornan ingresos cuyo paciente tenga nivel, centro de atención y unidad funcional existentes (INNER JOIN); La búsqueda ignora espacios en blanco a izquierda y derecha del código de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Tipo de ingreso; Liquidación; Causa de ingreso; Tipo de riesgo de ingreso; Centro de atención; Unidad funcional; Plan de beneficios; Autorización; Responsable del paciente; Nivel de afiliación; Cuota moderadora; Copago contributivo; Copago subsidiado; Copago vinculado; SISBEN; Topes (evento y anual); Estrato; Tipo de afiliación; Tipo de documento; Care group; Entidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un único conjunto de resultados con datos del ingreso, paciente y catálogos relacionados cuando LTRIM(RTRIM(NUMINGRES)) coincide con el parámetro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; ADNIVELES; ADCENATEN; INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetAdmissionByCode';
-- GO
