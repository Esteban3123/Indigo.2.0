
CREATE VIEW [AccountManagement].[ViewListRequests]
AS
SELECT 
    ai.NUMINGRES AdmissionNumber,
    ai.IFECHAING AdmissionDate,
    ai.IDADTIPOIDENTIFICA PatientIdentification,
    ai.UFUCODIGO  FunctionalUnitCode,
	ai.GENCAREGROUP CareGroup,
	ai.CODCENATE as CareGroupCode,
    ai.UFUINGHOS Bed,
    ai.CODDIAING Diagnosis,
    ai.INUMERORE Folio,
    NULL AS 'Asignado',
    ai.INUMERORE TypeIncome,
    ai.CODUSUCRE UserCreation,
    ai.CODUSUMOD UserModificacion
FROM 
    dbo.ADINGRESO ai