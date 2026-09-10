

CREATE PROCEDURE [dbo].[SP_HC_ListarOrdenes]
-- Add the parameters for the stored procedure here
@IPCODPACI VARCHAR(25),
@NUMINGRES CHAR(10),
@NUMEFOLIO NCHAR(10)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT
'MEDICAMENTOS DE QUIMIOTERAPIA' AS TIPO
  ,A.ID AS 'AUTO'
  ,RTRIM(B.CODPRODUC) AS CODIGO
  ,RTRIM(c.DESPRODUC) AS DESCRIPCION
  , Z.GENCONEXT AS 'MANEXTPRO'
  ,B.CODPRODUC AS 'Codigo Producto'
  ,OC.Environment AS SchemaType
FROM EHR.HCORDQUIMIO A WITH(NOLOCK)
INNER JOIN EHR.HCORMEDICAMESQUEMA B WITH(NOLOCK) ON A.ID = B.IDHCORDQUIMIO and b.NUMEFOLIO= @NUMEFOLIO
INNER JOIN dbo.HCHISPACA z WITH(NOLOCK) ON z.NUMEFOLIO = B.NUMEFOLIO and z.ipcodpaci = a.IPCODPACI  and z.NUMEFOLIO=@NUMEFOLIO
INNER JOIN dbo.IHLISTPRO c WITH(NOLOCK) ON C.CODPRODUC = B.CODPRODUC
INNER JOIN EHR.HCORDCICLOS OC WITH(NOLOCK) on OC.IDHCORDQUIMIO = A.ID AND OC.NUMEFOLIO = @NUMEFOLIO
WHERE A.IPCODPACI=@IPCODPACI AND Z.NUMINGRES=@NUMINGRES   ---AND z.GENCONEXT = 1
 
UNION ALL

SELECT
'MEDICAMENTO CONTROL' AS TIPO
  ,E.ID AS 'AUTO'
  ,RTRIM(B.CODPRODUC) AS CODIGO
  ,RTRIM(B.DESPRODUC) AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCPRESCRD A WITH(NOLOCK)
INNER JOIN DBO.IHLISTPRO B WITH(NOLOCK)
ON A.CODPRODUC = B.CODPRODUC
INNER JOIN DBO.HCPRODUCTOSCONTROL E WITH(NOLOCK)
ON E.CODPRODUC = A.CODPRODUC
AND E.NUMEFOLIO = A.NUMEFOLIO
AND E.NUMINGRES = A.NUMINGRES
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO AND IDESQUEMAONC IS NULL
UNION ALL
SELECT
'MEDICAMENTO' AS TIPO
  ,A.CODCONCEC AS 'AUTO'
  ,RTRIM(B.CODPRODUC) AS CODIGO
  ,RTRIM(B.DESPRODUC) AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCPRESCRD A WITH(NOLOCK)
INNER JOIN DBO.IHLISTPRO B WITH(NOLOCK)
ON A.CODPRODUC = B.CODPRODUC
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO AND IDESQUEMAONC IS NULL
UNION ALL
SELECT
'LABORATORIO' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDLABO A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
UNION ALL
SELECT
'PATOLOGIA' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDPATO A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
UNION ALL
SELECT
'IMAGEN DX' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDIMAG A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
UNION ALL
SELECT
'PROCEDIMIENTO QX' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDPROQ A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
UNION ALL
SELECT
'PROCEDIMIENTO NO QX' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDPRON A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
UNION ALL
SELECT
'INTERCONSULTA' AS TIPO
  ,A.AUTO AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,A.MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
FROM DBO.HCORDINTE A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
INNER JOIN DBO.INESPECIA C WITH(NOLOCK)
ON A.CODESPECI = C.CODESPECI
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO      
UNION ALL
SELECT
'CONSULTA DE CONTROL' as TIPO
  ,A.[AUTO] AS 'AUTO'
  ,RTRIM(A.CODSERIPS) AS CODIGO
  ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, '') AS DESCRIPCION
  ,1 as MANEXTPRO
  ,'' AS 'Codigo Producto'
  ,'' AS SchemaType
from HCDESCOEX A WITH(NOLOCK)
INNER JOIN DBO.INCUPSIPS B WITH(NOLOCK)
ON A.CODSERIPS = B.CODSERIPS
INNER JOIN DBO.INESPECIA C WITH(NOLOCK)
ON A.CODESPECI = C.CODESPECI
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD WITH(NOLOCK)
ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK)
ON CD.ID = CDD.CONTRACTDESCRIPTIONID
WHERE A.IPCODPACI=@IPCODPACI AND A.NUMINGRES=@NUMINGRES AND A.NUMEFOLIO=@NUMEFOLIO
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las órdenes médicas registradas en un folio de historia clínica para un paciente y un ingreso específicos, devolviendo en un único resultado unificado (UNION ALL) los diferentes tipos de orden: medicamentos de quimioterapia (con su esquema y ciclo oncológico), medicamentos de control, medicamentos regulares, exámenes de laboratorio, estudios de patología, imágenes diagnósticas, procedimientos quirúrgicos, procedimientos no quirúrgicos, interconsultas y consultas de control. Recibe como parámetros la cédula del paciente (@IPCODPACI), el número de ingreso (@NUMINGRES) y el número de folio de la historia clínica (@NUMEFOLIO). Para cada orden retorna el tipo de orden, el identificador interno, el código del servicio o producto (CUPS o código de medicamento), la descripción del ítem, si requiere manejo externo y —en el caso de quimioterapia— el tipo de esquema oncológico aplicado; es el procedimiento central para visualizar el resumen de órdenes activas de una nota clínica en el módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado todas las órdenes clínicas (medicamentos, quimioterapia, laboratorio, patología, imágenes, procedimientos, interconsultas y consulta de control) asociadas a un paciente, ingreso y folio de nota clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir y estar relacionados entre sí en las tablas de órdenes clínicas.; Los productos referenciados en las órdenes deben existir en el catálogo IHLISTPRO; los servicios deben existir en INCUPSIPS.; Para órdenes de quimioterapia debe existir registro en HCHISPACA que vincule paciente, ingreso y folio, y al menos un ciclo en HCORDCICLOS para el folio.; Para medicamentos de control debe existir el producto en HCPRODUCTOSCONTROL para la combinación folio/ingreso/producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las prescripciones con esquema oncológico (IDESQUEMAONC NOT NULL) nunca se listan en las ramas de medicamento estándar ni de medicamento de control; se reservan para la rama de quimioterapia.; Para la rama de quimioterapia siempre se exige consistencia paciente-folio-ingreso vía HCHISPACA (z.ipcodpaci=a.IPCODPACI, z.NUMEFOLIO=B.NUMEFOLIO, z.NUMINGRES=@NUMINGRES).; Las descripciones de servicios CUPS se enriquecen con el nombre de la descripción contractual cuando existe vínculo en CUPSENTITYCONTRACTDESCRIPTIONS/CONTRACTDESCRIPTIONS; si no existe se concatena cadena vacía.; Para ''CONSULTA DE CONTROL'' siempre se marca MANEXTPRO=1 (manejo externo), independientemente del dato real.; Toda lectura se realiza con WITH(NOLOCK), por lo que puede haber lecturas sucias.; El resultado siempre filtra estrictamente por la tripleta paciente/ingreso/folio recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Folio de nota clínica / Historia clínica; Órdenes médicas; Quimioterapia / Esquema oncológico; Ciclos de quimioterapia; Medicamentos de control; Prescripción de medicamentos; Órdenes de laboratorio; Órdenes de patología; Órdenes de imágenes diagnósticas; Procedimientos quirúrgicos y no quirúrgicos; Interconsulta por especialidad; Consulta de control externa; Catálogo CUPS/IPS; Manejo externo del producto (MANEXTPRO); Descripciones contractuales de servicios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un único conjunto unificado (UNION ALL) con columnas TIPO, AUTO, CODIGO, DESCRIPCION, MANEXTPRO, Codigo Producto y SchemaType, etiquetando cada origen con un literal de TIPO (''MEDICAMENTOS DE QUIMIOTERAPIA'', ''MEDICAMENTO CONTROL'', ''MEDICAMENTO'', ''LABORATORIO'', ''PATOLOGIA'', ''IMAGEN DX'', ''PROCEDIMIENTO QX'', ''PROCEDIMIENTO NO QX'', ''INTERCONSULTA'', ''CONSULTA DE CONTROL'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCPRESCRD.IDESQUEMAONC IS NULL y existe registro en HCPRODUCTOSCONTROL para el folio/ingreso/producto → La prescripción se reporta como ''MEDICAMENTO CONTROL'' usando el ID de HCPRODUCTOSCONTROL como AUTO; si HCPRESCRD.IDESQUEMAONC IS NULL (sin filtrar por control) → La prescripción se reporta también como ''MEDICAMENTO'' usando CODCONCEC como AUTO else Las prescripciones con IDESQUEMAONC NOT NULL (asociadas a esquema oncológico) se excluyen de las ramas ''MEDICAMENTO'' y ''MEDICAMENTO CONTROL''; si Existe orden en HCORDQUIMIO con su detalle en HCORMEDICAMESQUEMA y ciclo en HCORDCICLOS para el folio → Se listan los medicamentos como ''MEDICAMENTOS DE QUIMIOTERAPIA'' tomando MANEXTPRO desde HCHISPACA.GENCONEXT y SchemaType desde HCORDCICLOS.Environment', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.HCORMEDICAMESQUEMA; dbo.HCHISPACA; dbo.IHLISTPRO; EHR.HCORDCICLOS; dbo.HCPRESCRD; dbo.HCPRODUCTOSCONTROL; dbo.HCORDLABO; dbo.INCUPSIPS; CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS; CONTRACT.CONTRACTDESCRIPTIONS; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCORDINTE; dbo.INESPECIA; dbo.HCDESCOEX', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenes';
-- GO
