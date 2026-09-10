
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_OrdenesMedicasResumenInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10),
@ManejoExterno bit,
@TipodeOrden char(2)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

IF @TipodeOrden = '1'
--LABORATORIO          
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDLABI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '2'
--PATOLOGIAS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDPATI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '3'
--IMAGENES
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDIMAI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '4'
--PROCEDIMIENTOS QUIRURGICOS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDPROI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '5'
--PROCEDIMIENTOS NO QUIRURGICOS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO', EXREASITI AS 'EXAMEN EN SITIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDPRNI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '6'
--INTERCONSULTAS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,C.DESESPECI AS 'ESPECIALIDAD INTERCONSULTA'
FROM HCORDINTI A WITH(NOLOCK) INNER JOIN INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
INNER JOIN INESPECIA C ON A.CODESPECI=C.CODESPECI
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen de órdenes médicas internas de la historia clínica de un paciente hospitalizado. Según el tipo de orden indicado, consulta los servicios solicitados en laboratorio clínico (HCORDLABI), patología (HCORDPATI), imágenes diagnósticas (HCORDIMAI), procedimientos quirúrgicos (HCORDPROI), procedimientos no quirúrgicos (HCORDPRNI) o interconsultas (HCORDINTI), cruzando siempre con el maestro de servicios CUPS/IPS (INCUPSIPS) para obtener la descripción del servicio. Filtra por cédula del paciente, número de ingreso, número de folio y si el manejo es interno o externo, excluyendo registros con identificador especial de historia. Se usa para imprimir o visualizar el detalle de órdenes médicas en la historia clínica interna del paciente, cubriendo exámenes de laboratorio, imágenes, procedimientos quirúrgicos y no quirúrgicos, e interconsultas por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el resumen de órdenes médicas internas de un paciente para un folio/ingreso, segmentado por tipo de orden (laboratorio, patología, imágenes, procedimientos quirúrgicos, no quirúrgicos e interconsultas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, folio e ingreso deben existir en las tablas de órdenes médicas correspondientes.; Los códigos de servicio deben estar registrados en el catálogo de CUPS (INCUPSIPS).; Para interconsultas, el código de especialidad debe existir en INESPECIA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se excluyen registros con IDETIPHIS=''CODIGOAZU'' (historias marcadas como código azul/confidenciales).; Los resultados se restringen al paciente, ingreso y folio indicados, y al indicador de manejo externo recibido.; Solo se retornan órdenes cuyo código de servicio existe en el catálogo INCUPSIPS (INNER JOIN).; El procedimiento es de solo lectura; no modifica datos.; Las consultas usan WITH(NOLOCK), permitiendo lecturas sucias sobre las tablas de órdenes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes médicas; Paciente; Folio de historia clínica; Ingreso hospitalario; Manejo externo; Laboratorio clínico; Patología; Imágenes diagnósticas; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Interconsulta; Especialidad médica; Catálogo CUPS; Examen en sitio; Código azul (exclusión IDETIPHIS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABI: Cuando el tipo de orden es ''1'' se retornan las órdenes de laboratorio del paciente/ingreso/folio cuyo manejo externo coincide y cuyo IDETIPHIS es distinto de ''CODIGOAZU''.; [RETURN_RESULT] HCORDPATI: Cuando el tipo de orden es ''2'' se retornan las órdenes de patología filtradas por paciente/ingreso/folio, manejo externo y excluyendo IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDIMAI: Cuando el tipo de orden es ''3'' se retornan las órdenes de imágenes diagnósticas con los mismos filtros y exclusión de IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDPROI: Cuando el tipo de orden es ''4'' se retornan las órdenes de procedimientos quirúrgicos con los mismos filtros y exclusión de IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDPRNI: Cuando el tipo de orden es ''5'' se retornan las órdenes de procedimientos no quirúrgicos incluyendo el indicador de examen en sitio (EXREASITI), con los mismos filtros y exclusión de IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDINTI: Cuando el tipo de orden es ''6'' se retornan las interconsultas junto con la descripción de la especialidad, con los mismos filtros y exclusión de IDETIPHIS=''CODIGOAZU''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipodeOrden = ''1'' → Consulta órdenes de laboratorio en HCORDLABI.; si @TipodeOrden = ''2'' → Consulta órdenes de patología en HCORDPATI.; si @TipodeOrden = ''3'' → Consulta órdenes de imágenes en HCORDIMAI.; si @TipodeOrden = ''4'' → Consulta procedimientos quirúrgicos en HCORDPROI.; si @TipodeOrden = ''5'' → Consulta procedimientos no quirúrgicos en HCORDPRNI incluyendo el campo EXREASITI.; si @TipodeOrden = ''6'' → Consulta interconsultas en HCORDINTI con join a INESPECIA para incluir la descripción de la especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABI; dbo.HCORDPATI; dbo.HCORDIMAI; dbo.HCORDPROI; dbo.HCORDPRNI; dbo.HCORDINTI; dbo.INCUPSIPS; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumenInternos';
-- GO
