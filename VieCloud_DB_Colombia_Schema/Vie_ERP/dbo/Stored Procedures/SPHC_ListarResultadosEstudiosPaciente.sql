
-- =============================================
-- Author:		<Julian Andres Cardozo>
-- Create date: <05/08/2011>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarResultadosEstudiosPaciente]
(
@Paciente Varchar(25)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	


	
		SELECT CAST('' AS BIT) AS IMPRIMIR, CAST(CONSECUTI AS INT) AS Consecutivo, CASE TIPODOCUM WHEN 2 THEN 'Laboratorios' WHEN 3 THEN 'Imagenes - DICOM' WHEN 4 THEN 'Patologias' WHEN 8 THEN 'Otros Estudios Realizados' WHEN 9 THEN 'Lecturas Imagenes Dx'  WHEN 98 THEN 'Otros Procedimientos' END AS Tipo,NUMINGRES AS Ingreso,FECPROCES AS Fecha,RTRIM(B.DESSERIPS) AS Servicio,
		CODCENATE AS Centro,RTRIM(C.UFUDESCRI) AS UnidadFuncional,'' AS Estado,RTRIM(OBSERDOCU) AS Nombre,RTRIM(NOMARCADJ) AS Archivo,A.TIPODOCUM,A.IDMUESTRA,
		A.SupportDate as 'FechaSoporte', A.Observation, D.DESCATEGO AS DescripcionCategoria
		,'HCDOCUMAD' As 'OriginTable', 'CONSECUTI' As 'IdColumnName', A.CONSECUTI As 'IdOrigenTable', ARCHEXTEN As 'Extension', 'ARCHEXTEN' As 'ExtensionColumn', 'NOMARCADJ' As 'ArchiveColumn'
		FROM dbo.HCDOCUMAD A  
			LEFT OUTER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
			INNER JOIN dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO
			LEFT JOIN dbo.HCCATDOCU D ON A.CodeHCCATDOCU=D.CODCATEGO
		WHERE IPCODPACI=@paciente AND NOMARCADJ IS NOT NULL  AND A.TIPODOCUM IN ('2','3','4','8','9','98') AND A.CODSERIPS IS NOT NULL
UNION
		SELECT CAST('' AS BIT) AS IMPRIMIR, CAST(CONSECUTI AS INT) AS Consecutivo,D.DESCATEGO AS Tipo,NUMINGRES AS Ingreso,FECPROCES AS Fecha,'NO APLICA' AS Servicio,
		CODCENATE AS Centro,RTRIM(C.UFUDESCRI) AS UnidadFuncional,'' AS Estado,RTRIM(OBSERDOCU) AS Nombre,RTRIM(NOMARCADJ) AS Archivo,A.TIPODOCUM,A.IDMUESTRA,
		A.SupportDate as 'FechaSoporte', A.Observation, D.DESCATEGO AS DescripcionCategoria
		,'HCDOCUMAD' As 'OriginTable', 'CONSECUTI' As 'IdColumnName', A.CONSECUTI As 'IdOrigenTable', ARCHEXTEN As 'Extension', 'ARCHEXTEN' As 'ExtensionColumn', 'NOMARCADJ' As 'ArchiveColumn'
		FROM dbo.HCDOCUMAD A 
			INNER JOIN dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO 
			INNER JOIN dbo.HCCATDOCU D ON A.TIPODOCUM=D.CODCATEGO
		WHERE IPCODPACI=@Paciente AND NOMARCADJ IS NOT NULL AND D.TIPDOCUME='1' AND TIPODOCUM not IN ('2','3','4','8','9','98')

 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los documentos clínicos adjuntos (resultados de estudios) asociados a un paciente, identificado por su cédula o código. Recupera archivos de laboratorio, imágenes diagnósticas (DICOM), patologías, lecturas de imágenes y otros procedimientos registrados en la historia clínica, cruzando los documentos adjuntos (HCDOCUMAD) con el catálogo de servicios CUPS (INCUPSIPS), las unidades funcionales (INUNIFUNC) y las categorías de documentos clínicos (HCCATDOCU). Se usa para mostrar al profesional de salud o al sistema el historial completo de estudios realizados a un paciente, con su tipo, fecha de procesamiento, fecha soporte, archivo adjunto, nombre del estudio, unidad funcional y categoría documental. Cada fila incluye además la tabla y el identificador de origen del registro (OriginTable, IdOrigenTable) y el nombre de la columna llave de esa tabla (IdColumnName) para poder referenciarlo al auditar sustituciones o anulaciones de anexos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los documentos adjuntos con archivo de un paciente, distinguiendo los resultados de estudios clínicos estándar (laboratorios, imágenes, patologías, etc.) de otros documentos clasificados por categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse el identificador del paciente; Las unidades funcionales referenciadas deben existir en INUNIFUNC (INNER JOIN); Para el segundo bloque, el TIPODOCUM debe corresponder a una categoría existente en HCCATDOCU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan documentos que tengan archivo adjunto (NOMARCADJ no nulo); El primer bloque solo incluye documentos con servicio (CODSERIPS) asignado; El segundo bloque excluye los tipos clínicos estándar (2,3,4,8,9,98) y solo incluye categorías cuyo TIPDOCUME=''1''; La columna IMPRIMIR siempre se inicializa en 0 (bit vacío); Para documentos sin tipo estándar el Servicio se reporta como ''NO APLICA''; El resultado se filtra siempre por el paciente recibido; Cada fila expone OriginTable=''HCDOCUMAD'' e IdOrigenTable=A.CONSECUTI e IdColumnName=''CONSECUTI'' para trazabilidad de auditoría (sustitución/anulación de anexos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Documentos adjuntos; Resultados de estudios; Laboratorios; Imágenes DICOM; Patologías; Lecturas de imágenes diagnósticas; Unidad funcional; Servicio (CUPS/IPS); Categoría de documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDOCUMAD: Cuando IPCODPACI=@Paciente, NOMARCADJ IS NOT NULL, TIPODOCUM IN (''2'',''3'',''4'',''8'',''9'',''98'') y CODSERIPS IS NOT NULL → retorna fila con Tipo mapeado por TIPODOCUM y servicio desde INCUPSIPS; [RETURN_RESULT] dbo.HCDOCUMAD: Cuando IPCODPACI=@Paciente, NOMARCADJ IS NOT NULL, TIPODOCUM NOT IN (''2'',''3'',''4'',''8'',''9'',''98'') y la categoría asociada en HCCATDOCU tiene TIPDOCUME=''1'' → retorna fila con Tipo=DESCATEGO y Servicio=''NO APLICA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPODOCUM IN (''2'',''3'',''4'',''8'',''9'',''98'') con CODSERIPS no nulo → Clasifica el documento con etiqueta fija (Laboratorios, Imagenes - DICOM, Patologias, Otros Estudios Realizados, Lecturas Imagenes Dx, Otros Procedimientos) y resuelve servicio desde INCUPSIPS else Si TIPODOCUM no está en esa lista y la categoría es de tipo documento ''1'', usa la descripción de HCCATDOCU como Tipo y marca Servicio como ''NO APLICA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDOCUMAD; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.HCCATDOCU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPaciente';
-- GO
