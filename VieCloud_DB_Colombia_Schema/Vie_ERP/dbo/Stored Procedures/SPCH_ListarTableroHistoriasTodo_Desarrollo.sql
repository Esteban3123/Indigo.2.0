
CREATE PROCEDURE [dbo].[SPCH_ListarTableroHistoriasTodo_Desarrollo]
(
@Paciente Varchar(25),
@Empresa Char(3)
,@Origen Char(1) 
)
AS
BEGIN
	SET NOCOUNT ON;

	IF @Origen='1'
SELECT FECHISPAC AS 'FechaHistoria',RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live,RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento,NUMINGRES AS Ingreso,RTRIM(NUMEFOLIO) AS Folio, RTRIM(DESESPECI) AS Especialidad, UFUTIPUNI AS TipoUnidad, @Empresa AS Empresa,ESTAFOLIO AS Estado
FROM dbo.HCHISPACA A INNER JOIN 
dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO INNER JOIN 
dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL INNER JOIN 
dbo.ADcenaten D ON A.CODCENATE=D.codcenate INNER JOIN 
dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO INNER JOIN 
dbo.INESPECIA ES ON C.CODESPEC1=ES.CODESPECI 
WHERE A.IPCODPACI=@Paciente AND TIPHISPAC='I' 

else

	
SELECT FECHISPAC AS 'FechaHistoria',RTRIM(B.NOMDIAGNO) AS 'DiagnosticoPrincipal',RTRIM(C.NOMMEDICO) AS Medico,C.CODPROSAL,'' AS Live,RTRIM(E.UFUDESCRI) + ' - ' + RTRIM(D.nomcenate) AS Ubicacion, NUMEFOLIO+NUMINGRES AS NUMEFOLIO, CONSFOLIO+NUMINGRES AS CONSFOLIO, RTRIM(DATSUBJET) AS Subjetivo, RTRIM(DATOBJETI) AS Objetivo, RTRIM(DATPRONOS) AS Pronostico, RTRIM(DATTRATAM) AS Tratamiento,NUMINGRES AS Ingreso,RTRIM(NUMEFOLIO) AS Folio, RTRIM(DESESPECI) AS Especialidad, UFUTIPUNI AS TipoUnidad, @Empresa AS Empresa,ESTAFOLIO AS Estado
FROM dbo.HCHISPACA A INNER JOIN 
dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO INNER JOIN 
dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL INNER JOIN 
dbo.ADcenaten D ON A.CODCENATE=D.codcenate INNER JOIN 
dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO INNER JOIN 
dbo.INESPECIA ES ON C.CODESPEC1=ES.CODESPECI 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el tablero completo de historias clínicas de un paciente para una empresa determinada, combinando datos de folios clínicos (HCHISPACA) con el diagnóstico principal (CIE-10), el médico tratante y su especialidad, la unidad funcional y el centro de atención donde se generó cada nota. Según el parámetro de origen (@Origen=''1''), filtra únicamente las historias de tipo ingreso del paciente indicado; de lo contrario, devuelve todos los folios sin filtro por paciente ni tipo. Se utiliza para alimentar el tablero o visor de historia clínica, mostrando fecha, diagnóstico, médico, ubicación, datos SOAP (subjetivo, objetivo, pronóstico, tratamiento), número de folio, ingreso, especialidad, tipo de unidad y estado del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas de un paciente para alimentar el tablero/visor de historia clínica, mostrando datos SOAP, diagnóstico, médico, ubicación, especialidad y estado del folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir como IPCODPACI en HCHISPACA cuando se filtra por hospitalización; Las historias deben tener diagnóstico, profesional, centro de atención, unidad funcional y especialidad referenciados en sus catálogos (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen historias que tengan diagnóstico, profesional, centro de atención, unidad funcional y especialidad válidos (por uso de INNER JOIN); Los campos textuales se entregan recortados con RTRIM; El número de folio expuesto se compone concatenando NUMEFOLIO+NUMINGRES y CONSFOLIO+NUMINGRES; La empresa retornada corresponde al parámetro recibido, no a un dato calculado de la historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Diagnóstico principal; Profesional médico; Especialidad médica; Unidad funcional; Centro de atención; SOAP (subjetivo, objetivo, pronóstico, tratamiento); Folio; Ingreso/Hospitalización; Estado de folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando @Origen=''1'' devuelve historias del paciente filtradas por TIPHISPAC=''I'' (hospitalización/internamiento); [RETURN_RESULT] dbo.HCHISPACA: Cuando @Origen<>''1'' devuelve TODAS las historias clínicas sin filtro por paciente ni por tipo de historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Origen = ''1'' → Listar historias del paciente (IPCODPACI=@Paciente) cuyo TIPHISPAC=''I'' else Listar todas las historias clínicas sin filtro por paciente ni por tipo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasTodo_Desarrollo';
-- GO
