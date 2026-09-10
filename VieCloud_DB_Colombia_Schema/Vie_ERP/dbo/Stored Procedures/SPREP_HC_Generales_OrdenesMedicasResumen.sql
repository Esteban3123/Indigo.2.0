CREATE PROCEDURE [dbo].[SPREP_HC_Generales_OrdenesMedicasResumen]
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
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) + '. ' + isnull(CD.name,'') AS 'DESCRIPCION DEL SERVICIO',SUM(CANSERIPS) AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,A.FECHASUGE
FROM HCORDLABO A  with(noLock)
	 INNER JOIN INCUPSIPS B with(noLock) ON A.CODSERIPS=B.CODSERIPS 
     LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	 LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'
GROUP BY A.NUMEFOLIO, A.CODSERIPS,B.DESSERIPS,CD.[Name],A.OBSSERIPS, A.CODDIAGNO,A.FECHASUGE,A.CANSERIPS, A.CODCENATE, A.UFUCODIGO,A.IPCODPACI, A.FECORDMED

IF @TipodeOrden = '2'
--PATOLOGIAS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) + '. ' + isnull(CD.name,'') AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,
CASE A.laterality WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' ELSE 'No Aplica' END AS 'LATERALIDAD'
FROM HCORDPATO A  with(noLock)
	 INNER JOIN INCUPSIPS B with(noLock) ON A.CODSERIPS=B.CODSERIPS 
     LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	 LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '3'
--IMAGENES
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) + '. ' + isnull(CD.name,'') AS 'DESCRIPCION DEL SERVICIO',SUM(CANSERIPS) AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,A.FECHASUGE,CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' ELSE 'No Aplica' END AS 'LATERALIDAD'
FROM HCORDIMAG A with(noLock)
	 INNER JOIN INCUPSIPS B with(noLock)ON A.CODSERIPS=B.CODSERIPS 
     LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	 LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'
GROUP BY A.NUMEFOLIO,A.FECORDMED, A.CODSERIPS,B.DESSERIPS,CD.[Name],A.OBSSERIPS, A.CODDIAGNO,A.FECHASUGE,A.CANSERIPS, A.CODCENATE, A.UFUCODIGO,A.IPCODPACI, A.LATERALIDAD

IF @TipodeOrden = '4'
--PROCEDIMIENTOS QUIRURGICOS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI, CASE A.LATERALIDAD  WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 3 THEN 'Derecha' WHEN 4 THEN 'Bilateral' WHEN 5 THEN 'Multilateral' ELSE 'No Aplica' END AS 'LATERALIDAD'
FROM HCORDPROQ A  with(noLock)
	 INNER JOIN INCUPSIPS B with(noLock) ON A.CODSERIPS=B.CODSERIPS 
     LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	 LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '5'
--PROCEDIMIENTOS NO QUIRURGICOS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) + '. ' + isnull(CD.name,'') AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO', EXREASITI AS 'EXAMEN EN SITIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' ELSE 'No Aplica' END AS 'LATERALIDAD'
FROM HCORDPRON A  with(noLock)
	 INNER JOIN INCUPSIPS B with(noLock) ON A.CODSERIPS=B.CODSERIPS 
     LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
	 LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

IF @TipodeOrden = '6'
--INTERCONSULTAS
SELECT A.FECORDMED AS 'FECHA ORDENES MEDICAS', A.CODSERIPS AS 'CODIGO DEL SERVICIO',RTRIM(DESSERIPS) + '. ' + isnull(CD.name,'') AS 'DESCRIPCION DEL SERVICIO',CANSERIPS AS 'CANTIDAD SERVICIO',OBSSERIPS AS 'OBSERVACION DEL SERVICIO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,C.DESESPECI AS 'ESPECIALIDAD INTERCONSULTA'
FROM HCORDINTE A with(noLock)
		INNER JOIN INCUPSIPS B with(noLock) ON A.CODSERIPS=B.CODSERIPS 
		INNER JOIN INESPECIA C with(noLock) ON A.CODESPECI=C.CODESPECI
		LEFT JOIN Contract.CUPSEntityContractDescriptions CDD with(noLock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN Contract.ContractDescriptions  CD with(noLock) on CD.Id = CDD.ContractDescriptionId
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND MANEXTPRO =@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el resumen de órdenes médicas registradas en la historia clínica de un paciente para un ingreso y folio específicos, clasificando los resultados según el tipo de orden solicitado: laboratorio clínico, patologías, imágenes diagnósticas, procedimientos quirúrgicos, procedimientos no quirúrgicos o interconsultas. Para cada tipo de orden consulta la tabla correspondiente (HCORDLABO, HCORDPATO, HCORDIMAG, HCORDPROQ, HCORDPRON, HCORDINTE) y enriquece la descripción del servicio CUPS con su nombre del catálogo maestro (INCUPSIPS) y, cuando aplica, con el concepto de contrato asociado (Contract.ContractDescriptions y Contract.CUPSEntityContractDescriptions). Se usa para visualizar en la historia clínica todas las órdenes médicas activas o históricas del paciente, filtrando por cédula del paciente, número de ingreso, número de folio y si el manejo es externo o interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el resumen de órdenes médicas de un paciente/ingreso/folio según el tipo de orden (laboratorio, patología, imágenes, procedimientos quirúrgicos/no quirúrgicos o interconsultas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso y folio referenciados en las tablas de órdenes correspondientes.; El tipo de orden debe ser uno de los valores soportados (''1'' a ''6''); en otro caso no se retorna ningún conjunto de resultados.; El indicador de manejo externo debe coincidir con el valor almacenado en la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se filtran las órdenes por paciente, número de ingreso, número de folio y bandera de manejo externo.; Siempre se excluyen los registros cuyo IDETIPHIS sea ''CODIGOAZU''.; La descripción del servicio se compone del nombre del CUPS más, opcionalmente, la descripción de contrato relacionada cuando exista.; El procedimiento solo lee datos; no modifica información (todas las consultas usan WITH (NOLOCK)).; Para procedimientos quirúrgicos el código de lateralidad 2 no se traduce y cae al valor por defecto ''No Aplica'', a diferencia de los demás tipos de orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de historia clínica; Órdenes médicas; Laboratorio clínico; Patología; Imágenes diagnósticas; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Interconsulta médica; Especialidad médica; Lateralidad anatómica; CUPS; Manejo externo de la orden; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABO: Cuando el tipo de orden = ''1'' se retornan órdenes de laboratorio agrupadas por servicio, excluyendo registros con IDETIPHIS=''CODIGOAZU'' y filtrando por paciente, ingreso, folio y manejo externo.; [RETURN_RESULT] HCORDPATO: Cuando el tipo de orden = ''2'' se retornan órdenes de patología con su lateralidad descrita, excluyendo IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDIMAG: Cuando el tipo de orden = ''3'' se retornan órdenes de imágenes diagnósticas agrupadas por servicio con lateralidad, excluyendo IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDPROQ: Cuando el tipo de orden = ''4'' se retornan órdenes de procedimientos quirúrgicos con lateralidad, excluyendo IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDPRON: Cuando el tipo de orden = ''5'' se retornan órdenes de procedimientos no quirúrgicos incluyendo el indicador de examen en sitio y lateralidad, excluyendo IDETIPHIS=''CODIGOAZU''.; [RETURN_RESULT] HCORDINTE: Cuando el tipo de orden = ''6'' se retornan las interconsultas con la especialidad solicitada, excluyendo IDETIPHIS=''CODIGOAZU''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipodeOrden = ''1'' → Consulta órdenes de laboratorio (HCORDLABO) sumando cantidades por servicio.; si @TipodeOrden = ''2'' → Consulta órdenes de patología (HCORDPATO) con descripción de lateralidad.; si @TipodeOrden = ''3'' → Consulta órdenes de imágenes (HCORDIMAG) sumando cantidades y describiendo lateralidad.; si @TipodeOrden = ''4'' → Consulta órdenes de procedimientos quirúrgicos (HCORDPROQ) con mapeo de lateralidad distinto (1=Izquierda, 3=Derecha, 4=Bilateral, 5=Multilateral).; si @TipodeOrden = ''5'' → Consulta órdenes de procedimientos no quirúrgicos (HCORDPRON) incluyendo el indicador de examen en sitio.; si @TipodeOrden = ''6'' → Consulta interconsultas (HCORDINTE) uniendo con catálogo de especialidades para mostrar la especialidad solicitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INESPECIA; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_OrdenesMedicasResumen';
-- GO
