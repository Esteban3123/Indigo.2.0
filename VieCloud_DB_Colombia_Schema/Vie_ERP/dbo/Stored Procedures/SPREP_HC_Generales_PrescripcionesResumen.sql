
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_PrescripcionesResumen]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10),
@ManejoExterno bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDLABO A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno
UNION SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDPATO A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno
UNION SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDIMAG A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno
UNION SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDPROQ A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno
UNION SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDPRON A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno
UNION SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO'
FROM HCORDINTE A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el resumen consolidado de todas las prescripciones y órdenes médicas registradas en la historia clínica de un paciente para un ingreso y folio específicos. Integra en un único resultado las órdenes de laboratorio, patología, imágenes diagnósticas, procedimientos quirúrgicos, procedimientos no quirúrgicos e interconsultas (tablas HCORDLABO, HCORDPATO, HCORDIMAG, HCORDPROQ, HCORDPRON y HCORDINTE), enriqueciendo cada orden con la descripción del servicio CUPS/IPS desde INCUPSIPS. Permite filtrar por cédula del paciente, número de ingreso, número de folio y si el manejo es externo o interno, devolviendo fecha de la orden, código y descripción del servicio, cantidad y observaciones. Se usa para visualizar en pantalla o imprimir el resumen de prescripciones de una atención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado las prescripciones/órdenes médicas (laboratorios, patología, imágenes, procedimientos quirúrgicos y no quirúrgicos, e interconsultas) de un paciente para un ingreso y folio específicos, filtradas por su manejo interno o externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir y coincidir en las tablas de órdenes médicas; Los códigos de servicio (CODSERIPS) deben estar registrados en el catálogo INCUPSIPS para ser retornados; Se debe especificar si las órdenes son de manejo externo o interno mediante el indicador MANEXTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes cuyo código de servicio exista en el catálogo INCUPSIPS (INNER JOIN); Todas las consultas usan WITH(NOLOCK), permitiendo lecturas sucias; El filtro de manejo externo/interno se aplica uniformemente a todas las fuentes de órdenes; Filas duplicadas entre las distintas fuentes se eliminan por el UNION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Folio de historia clínica; Órdenes médicas; Prescripciones; Órdenes de laboratorio; Órdenes de patología; Órdenes de imágenes diagnósticas; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Interconsultas; Manejo externo de procedimientos; Catálogo CUPS de servicios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna la unión (UNION, elimina duplicados) de órdenes médicas de las 6 tablas de prescripciones que coincidan con paciente, ingreso, folio y tipo de manejo (externo/interno)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCORDINTE; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_PrescripcionesResumen';
-- GO
