
CREATE  PROCEDURE [dbo].[SPREP_HC_Generales_InterpretacionParaclinicosInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
     
SELECT   IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDIMAI A WITH(NOLOCK)
INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio
UNION SELECT  IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDLABI A WITH(NOLOCK)
INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO =@NumeroFolio
UNION SELECT IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDPATI A WITH(NOLOCK)
INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO  =@NumeroFolio
                  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna las interpretaciones de paraclínicos internos (exámenes de laboratorio, imágenes diagnósticas y patología) registradas en la historia clínica de un paciente para un ingreso y folio específicos. Combina en un único resultado las órdenes médicas de imágenes (HCORDIMAI), laboratorio clínico (HCORDLABI) y patología (HCORDPATI), enriqueciendo cada registro con el nombre del servicio (CUPS/IPS) obtenido de la tabla maestra INCUPSIPS. Se usa para generar el reporte de interpretaciones de paraclínicos internos en la historia clínica, permitiendo visualizar los resultados e interpretaciones de todos los exámenes solicitados a un paciente durante su atención o ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las interpretaciones de paraclínicos internos (imágenes, laboratorio y patología) de un paciente para un folio e ingreso específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente con el código indicado; Debe existir el ingreso indicado para el paciente; Debe existir el folio de orden asociado en alguna de las tablas de órdenes (imágenes, laboratorio o patología); Los servicios referenciados en las órdenes deben existir en el catálogo de CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan registros cuyo servicio (CODSERIPS) exista en el catálogo INCUPSIPS (INNER JOIN); El filtro siempre exige coincidencia simultánea de paciente, ingreso y folio en las tres fuentes; Se construye una llave compuesta concatenando folio y código de servicio separados por ''-''; Se eliminan duplicados entre las tres fuentes por efecto del UNION; Las consultas se ejecutan con NOLOCK, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de orden; Interpretación de paraclínicos; Órdenes de imágenes diagnósticas; Órdenes de laboratorio; Órdenes de patología; Catálogo CUPS; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión (UNION elimina duplicados) de interpretaciones de órdenes de imágenes, laboratorio y patología filtradas por paciente, ingreso y folio, enriquecidas con la descripción del servicio del catálogo CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAI; dbo.HCORDLABI; dbo.HCORDPATI; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicosInternos';
-- GO
