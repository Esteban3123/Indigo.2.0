
-- =============================================
-- Author:		<Julian Andres Cardozo>
-- Create date: <05/08/2011>
-- Description:	<Listado de patologias>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarResultadosEstudiosPacienteIngreso]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		
		SELECT CAST('' AS BIT) AS IMPRIMIR,CAST(CONSECUTI AS INT) AS Consecutivo,CASE TIPODOCUM WHEN 2 THEN 'Laboratorios' WHEN 3 THEN 'Imagenes - DICOM' WHEN 4 THEN 'Patologias' WHEN 8 THEN 'Otros Estudios Realizados' WHEN 9 THEN 'Lecturas Imagenes Dx' END AS Tipo,NUMINGRES AS Ingreso,FECPROCES AS Fecha,RTRIM(B.DESSERIPS) AS Servicio,
		CODCENATE AS Centro,RTRIM(C.UFUDESCRI) AS UnidadFuncional,'' AS Estado,RTRIM(OBSERDOCU) AS Nombre,RTRIM(NOMARCADJ) AS Archivo,A.TIPODOCUM,A.IDMUESTRA,
		A.SupportDate as 'FechaSoporte', A.Observation, D.DESCATEGO AS DescripcionCategoria
		,'HCDOCUMAD' As 'OriginTable', 'CONSECUTI' As 'IdColumnName', A.CONSECUTI As 'IdOrigenTable', ARCHEXTEN As 'Extension', 'ARCHEXTEN' As 'ExtensionColumn', 'NOMARCADJ' As 'ArchiveColumn'
		FROM dbo.HCDOCUMAD A  
		LEFT  OUTER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		INNER JOIN dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO
		LEFT JOIN  dbo.HCCATDOCU D ON A.CodeHCCATDOCU=D.CODCATEGO
		WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND  NOMARCADJ IS NOT NULL  AND A.TIPODOCUM IN ('2','3','4','8','9') AND A.CODSERIPS IS NOT NULL
UNION
		SELECT CAST('' AS BIT) AS IMPRIMIR,CAST(CONSECUTI AS INT) AS Consecutivo,D.DESCATEGO AS Tipo,NUMINGRES AS Ingreso,FECPROCES AS Fecha,'NO APLICA' AS Servicio,
		CODCENATE AS Centro,RTRIM(C.UFUDESCRI) AS UnidadFuncional,'' AS Estado,RTRIM(OBSERDOCU) AS Nombre,RTRIM(NOMARCADJ) AS Archivo,A.TIPODOCUM,A.IDMUESTRA,
		A.SupportDate as 'FechaSoporte', A.Observation, D.DESCATEGO AS DescripcionCategoria
		,'HCDOCUMAD' As 'OriginTable', 'CONSECUTI' As 'IdColumnName', A.CONSECUTI As 'IdOrigenTable', ARCHEXTEN As 'Extension', 'ARCHEXTEN' As 'ExtensionColumn', 'NOMARCADJ' As 'ArchiveColumn'
		FROM dbo.HCDOCUMAD A 
		INNER JOIN  dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO 
		INNER JOIN  dbo.HCCATDOCU D ON A.TIPODOCUM=D.CODCATEGO
		WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND NOMARCADJ IS NOT NULL  AND D.TIPDOCUME='1'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los documentos de estudios clínicos adjuntos (laboratorios, imágenes DICOM, patologías, lecturas de imágenes diagnósticas y otros estudios) asociados a un paciente en un ingreso específico. Combina los documentos registrados en la historia clínica (HCDOCUMAD) con el nombre del servicio CUPS/IPS (INCUPSIPS), la descripción de la unidad funcional o sala de atención (INUNIFUNC) y la categoría documental (HCCATDOCU), filtrando únicamente los registros que tengan archivo adjunto y correspondan a tipos de documento de resultado de estudios. Recibe como parámetros la cédula o código del paciente y el número de ingreso, y devuelve el listado completo con fecha, tipo de estudio, servicio, unidad funcional, nombre del documento, archivo, muestra e identificador de soporte para visualización o impresión desde la historia clínica. Cada fila incluye además la tabla, el nombre de la columna llave y el identificador de origen del registro (OriginTable, IdColumnName, IdOrigenTable) para referenciarlo al auditar sustituciones o anulaciones de anexos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado consolidado de documentos/estudios clínicos adjuntos (laboratorios, imágenes DICOM, patologías, otros estudios y lecturas) asociados a un paciente y a un ingreso específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir en HCDOCUMAD con documentos cargados.; Los documentos deben tener un archivo adjunto (NOMARCADJ no nulo) para ser listados.; Para la primera consulta, el documento debe tener servicio IPS asignado (CODSERIPS no nulo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan documentos que tienen archivo adjunto (NOMARCADJ IS NOT NULL).; El campo IMPRIMIR siempre se inicializa en bit vacío (false) en la salida.; El campo Estado siempre se devuelve vacío.; El UNION elimina duplicados entre las dos consultas.; En la rama por categoría documental, el servicio siempre se reporta como ''NO APLICA''.; Cada fila expone OriginTable=''HCDOCUMAD'' e IdOrigenTable=A.CONSECUTI e IdColumnName=''CONSECUTI'' para trazabilidad de auditoría (sustitución/anulación de anexos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Documentos clínicos adjuntos; Laboratorios; Imágenes DICOM; Patologías; Lecturas de imágenes diagnósticas; Unidad funcional; Servicio IPS; Categoría documental', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDOCUMAD: Cuando IPCODPACI=@Paciente, NUMINGRES=@Ingreso, NOMARCADJ no nulo, TIPODOCUM ∈ (2,3,4,8,9) y CODSERIPS no nulo, devuelve el documento etiquetando el Tipo según TIPODOCUM (2=Laboratorios, 3=Imagenes - DICOM, 4=Patologias, 8=Otros Estudios Realizados, 9=Lecturas Imagenes Dx).; [RETURN_RESULT] dbo.HCDOCUMAD: Cuando IPCODPACI=@Paciente, NUMINGRES=@Ingreso, NOMARCADJ no nulo y la categoría asociada en HCCATDOCU tiene TIPDOCUME=''1'', devuelve el documento usando DESCATEGO como Tipo y ''NO APLICA'' como Servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPODOCUM = 2 → Etiqueta el documento como ''Laboratorios''.; si TIPODOCUM = 3 → Etiqueta el documento como ''Imagenes - DICOM''.; si TIPODOCUM = 4 → Etiqueta el documento como ''Patologias''.; si TIPODOCUM = 8 → Etiqueta el documento como ''Otros Estudios Realizados''.; si TIPODOCUM = 9 → Etiqueta el documento como ''Lecturas Imagenes Dx''.; si HCCATDOCU.TIPDOCUME = ''1'' → Incluye el documento en el resultado mostrando el nombre de la categoría como tipo y ''NO APLICA'' como servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDOCUMAD; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.HCCATDOCU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarResultadosEstudiosPacienteIngreso';
-- GO
