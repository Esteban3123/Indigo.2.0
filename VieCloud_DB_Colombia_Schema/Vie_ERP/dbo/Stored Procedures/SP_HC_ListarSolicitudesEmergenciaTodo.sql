

CREATE PROCEDURE [dbo].[SP_HC_ListarSolicitudesEmergenciaTodo]
(
	@Paciente AS VARCHAR(25),
	@Ingreso AS CHAR(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	SELECT 
		Convert(BIT,0) As 'Seleccion', CODCONSEC AS 'Consecutivo', CONCAT('Folio de emergencia: ', NUMCODAZU) AS 'Folio'
	FROM
		HCCODAZUC
	WHERE
		IPCODPACI = @Paciente AND NUMINGRES= @Ingreso AND JUSCODAZU IS NULL;

	SELECT -- MEDICAMENTOS E INSUMOS
		    A.CODCONSEC AS 'Consecutivo', 'Medicamentos e insumos' AS 'Tipo', A.FECHAORDE AS 'FechaSolicitud', B.CODPRODUC AS 'Codigo',
		    RTRIM(DESPRODUC) AS 'Descripcion', '' AS 'DescripcionRelacionada', Indications, CANPEDPRO AS 'Pedida', 0 AS 'Utilizada' 
	    FROM dbo.HCCODAZUC A 
		    INNER JOIN dbo.HCCODAZUD B ON A.CODCONSEC=b.CODCONSEC 
		    INNER JOIN dbo.IHLISTPRO C ON B.CODPRODUC=C.CODPRODUC
	    WHERE 
		    IPCODPACI= @Paciente AND NUMINGRES= @Ingreso AND JUSCODAZU IS NULL
	    UNION -- IMAGENES
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Imágenes diagnósticas' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	    FROM dbo.HCORDIMAG A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO = 0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- LABORATORIOS
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Laboratorios' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	     FROM dbo.HCORDLABO A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO=0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- PATOLOGIAS
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Patologias' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	     FROM dbo.HCORDPATO A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO=0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- HEMOCOMPONENTES
	    SELECT 
		    A.CODCONSEC AS 'Consecutivo', 'Hemocomponentes' AS 'Tipo', A.FECHAORDE AS 'FechaSolicitud', COM.CODCOMSAM AS 'Codigo',
		    RTRIM(COM.DESCOMSAM) AS 'Descripcion', '' AS 'DescripcionRelacionada', '' AS 'Indications', COUNT(*) AS 'Pedida', 0 AS 'Utilizada'
	    FROM dbo.HCORHEMBOL BOLSA 
		    INNER JOIN dbo.HCCOMSAN COM ON COM.ID = BOLSA.COMSAMID 
		    INNER JOIN dbo.HCORHEMCO HEM  ON HEM.ID = BOLSA.HCORHEMCOID 
		    INNER JOIN dbo.HCCODAZUC A ON A.NUMCODAZU = HEM.NUMEFOLIO AND HEM.IPCODPACI = A.IPCODPACI AND HEM.IDETIPHIS = 'CODIGOAZU' AND HEM.NUMINGRES = A.NUMINGRES 
	    WHERE 
		    A.IPCODPACI = @Paciente AND A.NUMINGRES =  @Ingreso AND JUSCODAZU IS NULL
	    GROUP BY A.CODCONSEC, A.NUMCODAZU, A.FECHAORDE, COM.CODCOMSAM, COM.DESCOMSAM
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las solicitudes clínicas pendientes asociadas a un código azul (alerta de emergencia crítica) de un paciente en un ingreso específico. Dado el código del paciente (cédula) y el número de ingreso, retorna dos conjuntos de resultados: primero los folios de emergencia activos (sin justificación de cierre), y luego el detalle unificado de todos los ítems solicitados bajo esos folios, clasificados por tipo: medicamentos e insumos, imágenes diagnósticas, laboratorios, patologías y hemocomponentes, cada uno con su código CUPS o producto, descripción, descripción contractual relacionada, indicaciones y cantidad pedida. Este procedimiento es usado por el módulo de emergencias para visualizar y gestionar en tiempo real los requerimientos clínicos críticos de un paciente en código azul.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, para un paciente e ingreso dados, los folios de emergencia (código azul) pendientes de justificar y todas las solicitudes clínicas asociadas (medicamentos/insumos, imágenes, laboratorios, patologías y hemocomponentes).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCCODAZUC para el paciente e ingreso indicados; Solo se consideran folios de código azul cuya justificación (JUSCODAZU) sea NULL; Para imágenes, laboratorios y patologías el origen debe ser IDETIPHIS=''CODIGOAZU'' y MANEXTPRO=0 (no manejo externo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen solicitudes cuyo folio de código azul aún no ha sido justificado (JUSCODAZU IS NULL); Las órdenes diagnósticas se enlazan al folio de código azul vía IDETIPHIS=''CODIGOAZU'' y NUMEFOLIO=NUMCODAZU; La columna ''Utilizada'' siempre se devuelve en 0 (no se calcula consumo real); Se excluyen órdenes con manejo externo (MANEXTPRO=1) en imágenes, laboratorios y patologías; Los hemocomponentes se cuentan por número de bolsas asociadas a la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Código azul / Emergencia; Folio de emergencia; Justificación de código azul; Medicamentos e insumos; Imágenes diagnósticas; Laboratorios; Patologías; Hemocomponentes / Transfusión; Órdenes médicas; CUPS; Manejo externo de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCODAZUC: Devuelve los folios de emergencia (CODCONSEC y NUMCODAZU) del paciente/ingreso cuando JUSCODAZU IS NULL, marcando ''Seleccion''=0; [RETURN_RESULT] HCCODAZUD: Devuelve los medicamentos e insumos asociados al folio de código azul no justificado, con cantidad pedida y utilizada=0; [RETURN_RESULT] HCORDIMAG: Devuelve órdenes de imágenes diagnósticas vinculadas al folio de código azul cuando MANEXTPRO=0, IDETIPHIS=''CODIGOAZU'' y JUSCODAZU IS NULL; [RETURN_RESULT] HCORDLABO: Devuelve órdenes de laboratorio vinculadas al folio de código azul cuando MANEXTPRO=0, IDETIPHIS=''CODIGOAZU'' y JUSCODAZU IS NULL; [RETURN_RESULT] HCORDPATO: Devuelve órdenes de patología vinculadas al folio de código azul cuando MANEXTPRO=0, IDETIPHIS=''CODIGOAZU'' y JUSCODAZU IS NULL; [RETURN_RESULT] HCORHEMBOL: Devuelve hemocomponentes solicitados agrupados por componente, contando bolsas como ''Pedida'', vinculados al folio de código azul con JUSCODAZU IS NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCODAZUC; dbo.HCCODAZUD; dbo.IHLISTPRO; dbo.HCORDIMAG; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORHEMBOL; dbo.HCCOMSAN; dbo.HCORHEMCO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSolicitudesEmergenciaTodo';
-- GO
