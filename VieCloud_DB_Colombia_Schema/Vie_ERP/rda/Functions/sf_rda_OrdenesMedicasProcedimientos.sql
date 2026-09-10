
CREATE FUNCTION [rda].[sf_rda_OrdenesMedicasProcedimientos]
(
	@NumeroIngreso    char(10),
    @CodigoPaciente   varchar(25),
    @FolioInicio      nchar(10),
    @FolioFin         nchar(10),  -- NULL = folio único (igual a @FolioInicio)
    @ManExtPro        bit         -- NULL = ignorar filtro, 0 o 1 = filtrar explícitamente
)
RETURNS varchar(MAX)
AS
BEGIN

    DECLARE @json varchar(MAX)

	DECLARE @temp TABLE (
        Tipo_tecnologia_salud		  varchar(100),
		id							  int	,
        Codigo_servicio				  varchar(50),
        Descripcion_servicio		  varchar(MAX),
		Finalidad_defecto_servicio	  varchar(20),
		Fecha_realizacion_servicio	  datetime,
		Tipo_identificacion_prof	  varchar(5),
		Identificacion_profesional	  varchar(20),
		Fecha_resultado_servicio	  datetime
    )

	INSERT INTO @temp

	SELECT '01; Procedimiento en salud',
       A.AUTO, RTRIM(A.CODSERIPS), RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, ''),
	   '01 - Diagnóstico', FECRECMUE, [dbo].[TipoDocumento](P.IDADTIPOIDENTIFICA), RTRIM(P.CODPROSAL), FECHARESULT
    FROM DBO.HCORDLABO A
		INNER JOIN dbo.INPROFSAL P ON A.USURECMUE = P.CODUSUARI
        INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
        LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
        LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD ON CD.ID = CDD.CONTRACTDESCRIPTIONID
    WHERE (A.MANEXTPRO = @ManExtPro) AND A.IPCODPACI  = @CodigoPaciente AND A.NUMINGRES  = @NumeroIngreso
    AND A.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	UNION ALL

	SELECT '01; Procedimiento en salud',
	   A.AUTO, RTRIM(A.CODSERIPS), RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, ''),
	   '01 - Diagnóstico', FECRECEPMUES, [dbo].[TipoDocumento](P.IDADTIPOIDENTIFICA), RTRIM(P.CODPROSAL), FECHARESULT
	FROM DBO.HCORDPATO A
		INNER JOIN dbo.INPROFSAL P ON A.USURECEXA = P.CODUSUARI
		INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
		LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD ON CDD.ID = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD ON CD.ID = CDD.CONTRACTDESCRIPTIONID
    WHERE (A.MANEXTPRO = @ManExtPro) AND A.IPCODPACI  = @CodigoPaciente AND A.NUMINGRES  = @NumeroIngreso
    AND A.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	UNION ALL

	SELECT '01; Procedimiento en salud',
	   A.AUTO, RTRIM(A.CODSERIPS) ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, ''),
	   '01 - Diagnóstico', FECRECEXA, [dbo].[TipoDocumento](P.IDADTIPOIDENTIFICA), RTRIM(P.CODPROSAL), FECHARESCLA
	FROM DBO.HCORDIMAG A
		INNER JOIN dbo.INPROFSAL P ON A.USURECEXA = P.CODUSUARI
		INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
		LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
		LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD ON CD.ID = CDD.CONTRACTDESCRIPTIONID
    WHERE (A.MANEXTPRO = @ManExtPro) AND A.IPCODPACI  = @CodigoPaciente AND A.NUMINGRES  = @NumeroIngreso
    AND A.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	UNION ALL

	SELECT '01; Procedimiento en salud' ,
	   A.AUTO, RTRIM(A.CODSERIPS), RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, ''),
	   '01 - Diagnóstico', FECHAPRO, [dbo].[TipoDocumento](P.IDADTIPOIDENTIFICA), RTRIM(P.CODPROSAL), FECHAPRO
	FROM DBO.HCORDPROQ A
		INNER JOIN dbo.INPROFSAL P ON A.CODUSUPRO = P.CODUSUARI
		INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
		LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD  ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
		LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD ON CD.ID = CDD.CONTRACTDESCRIPTIONID
    WHERE (A.MANEXTPRO = @ManExtPro) AND A.IPCODPACI  = @CodigoPaciente AND A.NUMINGRES  = @NumeroIngreso
    AND A.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	UNION ALL

	SELECT '01; Procedimiento en salud' ,
	   A.AUTO, RTRIM(A.CODSERIPS) ,RTRIM(DESSERIPS) + '. ' + ISNULL(CD.NAME, ''),
	   '01 - Diagnóstico', FECHREALI, [dbo].[TipoDocumento](P.IDADTIPOIDENTIFICA), RTRIM(P.CODPROSAL), FECHREALI
	FROM DBO.HCORDPRON A
		INNER JOIN dbo.INPROFSAL P ON A.MEDREALI = P.CODPROSAL
		INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
		LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CDD ON CDD.ID = A.IDDESCRIPCIONRELACIONADA
		LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD ON CD.ID = CDD.CONTRACTDESCRIPTIONID
    WHERE (A.MANEXTPRO = @ManExtPro) AND A.IPCODPACI  = @CodigoPaciente AND A.NUMINGRES  = @NumeroIngreso
    AND A.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	UNION ALL
	
	SELECT DISTINCT '01; Procedimiento en salud' ,
		hb.id, RTRIM(A.CODSERIPS), RTRIM(DESSERIPS), '02 - Terapéutico',
		IIF(hd.TIPOCARGUE = 1, FECRESERVA, FECTRANSFU), IIF(hd.TIPOCARGUE = 1, [dbo].[TipoDocumento](PR.IDADTIPOIDENTIFICA), [dbo].[TipoDocumento](PT.IDADTIPOIDENTIFICA)), 
		IIF(hd.TIPOCARGUE = 1, RTRIM(hb.PROFSOLRES), RTRIM(hb.PROFSOLTRA)), FECAPLICENF
	FROM DBO.HCORHEMCUPS A 
		INNER JOIN HCCOMSAND hd ON A.IDHCCOMSAN = hd.COMSAMID 
		INNER JOIN HCORHEMBOL hb ON A.HCORHEMBOLID = hb.Id
		LEFT JOIN dbo.INPROFSAL PR ON hb.PROFSOLRES = PR.CODPROSAL
		LEFT JOIN dbo.INPROFSAL PT ON hb.PROFSOLTRA = PT.CODPROSAL
		INNER JOIN HCORHEMCO h ON A.HCORHEMCOID = h.Id
		INNER JOIN dbo.INCUPSIPS B ON B.CODSERIPS = A.CODSERIPS 
    WHERE (h.MANEXTPRO = @ManExtPro) AND h.IPCODPACI  = @CodigoPaciente AND h.NUMINGRES  = @NumeroIngreso
    AND h.NUMEFOLIO BETWEEN @FolioInicio AND ISNULL(@FolioFin, @FolioInicio)

	SELECT @json = json_result
    FROM (
        SELECT Tipo_tecnologia_salud, Codigo_servicio, Descripcion_servicio, Finalidad_defecto_servicio, 
		Fecha_realizacion_servicio, Tipo_identificacion_prof, Identificacion_profesional, Fecha_resultado_servicio
        FROM @temp
        FOR JSON PATH
    ) AS t(json_result)

    RETURN @json
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que consolida y serializa en formato JSON los procedimientos en salud ordenados para un paciente en un ingreso hospitalario específico, filtrando por rango de folios y modalidad de atención (manual/externa). Consulta seis tipos de órdenes clínicas —laboratorio, patología, imágenes diagnósticas, procedimientos quirúrgicos, procedimientos no quirúrgicos y hemoterapia— unificándolas bajo el tipo de tecnología CUPS colombiano. Para cada registro retorna el código y descripción del servicio, la finalidad (diagnóstica o terapéutica), las fechas de realización y resultado, y la identificación del profesional responsable.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un JSON los procedimientos en salud (laboratorio, patología, imágenes, quirúrgicos, no quirúrgicos y hemocomponentes) ordenados a un paciente durante un ingreso y rango de folios, para reportes RIPS/RDA.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener registros en alguna de las tablas de órdenes (HCORDLABO, HCORDPATO, HCORDIMAG, HCORDPROQ, HCORDPRON, HCORHEMCUPS) con el ingreso y folio indicados.; Los códigos de servicio referenciados deben existir en INCUPSIPS y los profesionales en INPROFSAL.; @FolioFin puede ser NULL; en ese caso se considera folio único igual a @FolioInicio.; @ManExtPro debe coincidir exactamente con el valor en las tablas de órdenes (no se contempla NULL pese al comentario).', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros se etiquetan como Tipo_tecnologia_salud = ''01; Procedimiento en salud''.; Las cinco primeras fuentes (laboratorio, patología, imágenes, quirúrgico, no quirúrgico) marcan Finalidad ''01 - Diagnóstico''; los hemocomponentes marcan ''02 - Terapéutico''.; La descripción del servicio se compone como DESSERIPS + ''. '' + nombre del contrato (si existe), tomando el nombre desde CONTRACTDESCRIPTIONS via CUPSENTITYCONTRACTDESCRIPTIONS.; El filtro de paciente, ingreso y rango de folios se aplica idénticamente a todas las fuentes.; La fecha de resultado y la fecha de realización en HCORDPROQ y HCORDPRON coinciden (FECHAPRO/FECHREALI usadas en ambos campos).; El tipo de identificación del profesional siempre se traduce mediante la función dbo.TipoDocumento.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento en salud; Orden de laboratorio; Orden de patología; Orden de imágenes diagnósticas; Procedimiento quirúrgico; Procedimiento no quirúrgico; Hemocomponentes / transfusión; Folio de atención; Ingreso del paciente; Profesional de la salud; Finalidad diagnóstica/terapéutica; CUPS (códigos de servicio); Manejo externo del procedimiento (MANEXTPRO); RIPS / RDA', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un varchar(MAX) con un arreglo JSON (FOR JSON PATH) que combina los seis orígenes de procedimientos del paciente/ingreso/folio.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hd.TIPOCARGUE = 1 (en HCCOMSAND, sub-consulta de hemocomponentes) → Usa FECRESERVA como fecha de realización, el profesional solicitante de reserva (PROFSOLRES) y su tipo de identificación. else Usa FECTRANSFU como fecha de realización y el profesional solicitante de transfusión (PROFSOLTRA) con su tipo de identificación.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCORHEMCUPS; dbo.HCCOMSAND; dbo.HCORHEMBOL; dbo.HCORHEMCO; dbo.INPROFSAL; dbo.INCUPSIPS; CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS; CONTRACT.CONTRACTDESCRIPTIONS', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasProcedimientos';
GO
