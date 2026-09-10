CREATE  Procedure [dbo].[SP_ListarDocumentosPaciente_EMR]
(
    @Paciente Varchar(25),
    @AttentionCenterCode varchar(20)
)
AS

    DECLARE @Path varchar(100) = 'D://'

    SELECT TOP 1 @Path = RTRIM(CARDOCTRA) FROM ADPA3047C WHERE CODCENATE = '11011'

 --Select  RTRIM(A.NOMARCADJ) as NombreDocumento,
 --		RTRIM(A.ARCHEXTEN) as Extension,
 --		Case TIPODOCUM
 --			when 2 then 'Laboratorio'
 --			when 3 then 'Imagenologia'
 --			when 4 then 'Patologias'
 --			when 5 then 'Decreto_3047'
 --			when 6 then 'Plantillas_Documentos'
 --			when 7 then 'Consentimiento_Informado'
 --			when 8 then 'Resultados_Examenes_Sitio'
 --			when 9 then 'Lectura_Imagenes'
 --			when 10 then 'Solicitudes'
 --			when 11 then 'FacturasDeVenta'
 --			else 'Otro'
 --		end as TipoDocumento,
 --		RTRIM(A.NUMINGRES) as Ingreso,
 --		A.FECPROCES as 'Fecha',
 --		dbo.ObtenerFechaFormateada(FECPROCES) as 'FechaFormateada',
 --       url = 'https://ontheline.trincoll.edu/images/bookdown/sample-local-pdf.pdf'
 --		-- url = 'http://ruta/' + RTRIM(NOMARCADJ)
 -- from HCDOCUMAD as A
 -- where A.IPCODPACI = @Paciente

 -- Documentos Paciente
    SELECT 
        RTRIM(DESDOCALM) as NombreDocumento,
        CASE TIPARCPAC WHEN '1' THEN 'documentos_paciente' WHEN '2' THEN 'documentos_ingreso' END AS TipoDocumento, 
        NUMINGRES AS Ingreso, 
        FECREGDOC AS Fecha,
        'https://ontheline.trincoll.edu/images/bookdown/sample-local-pdf.pdf' Archivo, --CONCAT(@Path, RTRIM(NOMANTARC)) as Archivo, 
        --RTRIM(NOMANTARC) as NombreArchivo, 
        CODCENATE AS Centro, 
        CAST(0 AS INT) AS Consecutivo, 
        RTRIM(DESCATEGO) AS DescripcionCategoria
        --, Observation, 
        --SupportDate as 'FechaSoporte' 
    FROM dbo.INPACIDOC A 
    LEFT OUTER JOIN dbo.HCCATDOCU B ON A.TIPODOCUM = B.CODCATEGO 
    WHERE IPCODPACI = @Paciente AND TIPARCPAC IN ('1', '2')
 
    UNION ALL

    -- documentos Adicionales
    SELECT 
        RTRIM(NOMARCADJ) AS NombreDocumento, 
        B.DESCATEGO AS TipoDocumento, 
        A.NUMINGRES AS Ingreso, 
        FECPROCES AS Fecha, 
        'https://ontheline.trincoll.edu/images/bookdown/sample-local-pdf.pdf' Archivo, --CONCAT(@Path, RTRIM(NOMARCADJ)) AS Archivo, 
        A.CODCENATE AS Centro, 
        CAST(CONSECUTI AS INT) AS Consecutivo, 
        RTRIM(DESCATEGO) AS DescripcionCategoria 
    FROM dbo.HCDOCUMAD A 
    INNER JOIN dbo.HCCATDOCU B ON A.TIPODOCUM = B.CODCATEGO 
    WHERE IPCODPACI = @Paciente AND TIPDOCUME = '0'

    UNION ALL

    -- Certificados Asistenciales
    SELECT 
        A.IDCERTIFICADO As NombreDocumento, 
        'certificados_asistenciales' As TipoDocumento, 
        A.NUMINGRES As Ingreso, 
        A.FECHAREGISTRO As Fecha, 
        'https://ontheline.trincoll.edu/images/bookdown/sample-local-pdf.pdf' Archivo, --CONCAT(@Path, CONCAT(RTRIM(E.DESCRIPLA), '-Reporte')) As Archivo,
        C.NOMCENATE As Centro, 
        A.ID As Consecutivo, 
        'CERTIFICADO ASISTENCIAL' As DescripcionCategoria 
        --E.DESCRIPLA As NombreArchivo, 
        --CONCAT(LTRIM(E.DESCRIPLA), '-Reporte') As Nombre, 
    FROM dbo.HCCERTIASISTENCIAL A 
    INNER JOIN dbo.INUNIFUNC B ON A.UFUCODIGO = B.UFUCODIGO 
    INNER JOIN dbo.ADCENATEN C ON A.CODCENATE = C.CODCENATE 
    INNER JOIN dbo.INESPECIA D ON A.CODESPECIALIDAD = D.CODESPECI 
    LEFT JOIN dbo.HCPLANDOC E ON A.IDCERTIFICADO = E.CODCONSEC 
    INNER JOIN dbo.SEGusuaru F ON A.USUARIOREGISTO = F.CODUSUARI 
    INNER JOIN dbo.INPACIENT G ON A.IPCODEPACI = G.IPCODPACI 
    WHERE IPCODEPACI = @Paciente

  ----Consentimientos Informados
    --SELECT 
    --    CASE TIPODOCUM WHEN '7' THEN 'Consentimientos Informados' WHEN '99' THEN 'Recepción referencias' END AS Tipo, 
    --    NUMINGRES AS Ingreso, 
    --    FECPROCES AS Fecha, 
    --    RTRIM(OBSERDOCU) AS Nombre, 
    --    RTRIM(NOMARCADJ) AS Archivo, 
    --    DC.CODCENATE AS Centro, 
    --    CAST(CONSECUTI AS INT) AS Consecutivo, 
    --    CAT.DESCATEGO AS DescripcionCategoria, 
    --    RTRIM(NOMARCADJ) AS NombreArchivo, 
    --    ES.URL, 
    --    ES.APIToken, 
    --    DC.SupportDate AS FechaSoporte 
    --FROM dbo.HCDOCUMAD AS DC 
    --LEFT JOIN Admissions.ElectronicSignature AS ES ON DC.CODCENATE = ES.CODCENATE 
    --INNER JOIN dbo.INPROFSAL B ON DC.CODPROSAL = B.CODPROSAL 
    --LEFT JOIN dbo.HCCATDOCU CAT ON DC.CodeHCCATDOCU = CAT.CODCATEGO 
    --WHERE IPCODPACI = @Paciente AND (
    --    TIPODOCUM = '7' 
    --    OR TIPODOCUM = '99'
    --)
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consolida y retorna todos los documentos asociados a un paciente, combinando tres fuentes: documentos propios del paciente/ingreso (INPACIDOC), documentos adjuntos de historia clínica (HCDOCUMAD) y certificados asistenciales (HCCERTIASISTENCIAL). Cada registro incluye nombre, categoría, ingreso, fecha y centro de atención. Obtiene la ruta base de archivos desde ADPA3047C según un centro fijo, aunque actualmente las URLs de archivo están sustituidas por un valor de prueba estático.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los documentos asociados a un paciente: documentos generales del paciente/ingreso, documentos adicionales de historia clínica y certificados asistenciales emitidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y estar identificado por su código (IPCODPACI/IPCODEPACI).; Se requiere que exista un registro en ADPA3047C con CODCENATE=''11011'' para obtener la ruta base de documentos (CARDOCTRA), aunque si no existe se usa el valor por defecto ''D://''.; Las categorías de documento deben estar parametrizadas en HCCATDOCU para resolver la descripción de categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ruta base de documentos se obtiene siempre del centro de atención fijo ''11011'' en ADPA3047C.; El consecutivo de los documentos del paciente (INPACIDOC) se entrega siempre como 0; el de documentos adicionales y certificados refleja su consecutivo real.; Los certificados asistenciales requieren existencia de unidad funcional, centro de atención, especialidad y usuario de registro válidos (joins INNER); si falta la plantilla en HCPLANDOC el certificado igual se lista (LEFT JOIN).; El parámetro de centro de atención recibido no se usa para filtrar los resultados.; Solo se devuelven documentos asociados al paciente solicitado; no hay efectos sobre datos persistentes (procedimiento de solo lectura).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; documentos del paciente; documentos adicionales de historia clínica; categorías de documentos clínicos; certificados asistenciales; centro de atención; especialidad médica; unidad funcional; plantillas de documentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve documentos del paciente desde INPACIDOC cuando IPCODPACI = paciente y TIPARCPAC IN (''1'',''2''), clasificándolos como ''documentos_paciente'' (TIPARCPAC=''1'') o ''documentos_ingreso'' (TIPARCPAC=''2'').; [RETURN_RESULT] resultset: Une documentos adicionales de HCDOCUMAD cuando IPCODPACI = paciente y TIPDOCUME = ''0'', tomando la descripción de categoría desde HCCATDOCU vía TIPODOCUM=CODCATEGO.; [RETURN_RESULT] resultset: Une los certificados asistenciales desde HCCERTIASISTENCIAL cuando IPCODEPACI = paciente, etiquetándolos como TipoDocumento=''certificados_asistenciales'' y DescripcionCategoria=''CERTIFICADO ASISTENCIAL''.; [RETURN_RESULT] resultset: Para todos los registros la columna Archivo se devuelve con una URL fija de ejemplo (''https://ontheline.trincoll.edu/.../sample-local-pdf.pdf'') en lugar de la ruta real del archivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPARCPAC = ''1'' → El documento se cataloga como ''documentos_paciente''. else Si TIPARCPAC = ''2'' se cataloga como ''documentos_ingreso''; otros valores se excluyen del filtro.; si TIPDOCUME = ''0'' en HCDOCUMAD → El documento adicional se incluye en el listado. else Se omite del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADPA3047C; dbo.INPACIDOC; dbo.HCCATDOCU; dbo.HCDOCUMAD; dbo.HCCERTIASISTENCIAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INESPECIA; dbo.HCPLANDOC; dbo.SEGusuaru; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDocumentosPaciente_EMR';
-- GO
