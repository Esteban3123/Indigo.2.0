
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_LaboratorioCodigoAzulInternos]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10),
@NumeroCodigoAzul nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',A.NUMEFOLIO AS 'CONSECUTIVO DEL CODIGO AZUL',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDLABI A WITH(NOLOCK)
INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroCodigoAzul AND MANEXTPRO = '0' AND IDETIPHIS='CODIGOAZU' UNION 
SELECT RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',A.NUMEFOLIO AS 'CONSECUTIVO DEL CODIGO AZUL',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDIMAI A WITH(NOLOCK)
INNER JOIN INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroCodigoAzul AND MANEXTPRO = '0' AND IDETIPHIS='CODIGOAZU'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los servicios internos de laboratorio e imágenes diagnósticas asociados a un evento de Código Azul para un paciente e ingreso específicos. Combina órdenes de laboratorio (HCORDLABI) y órdenes de imágenes diagnósticas (HCORDIMAI) que tengan el tipo de historia clínica ''CODIGOAZU'' y sean de manejo interno (no externo), obteniendo la descripción del servicio CUPS desde la tabla de servicios (INCUPSIPS). Se utiliza para reportar qué exámenes y estudios de imagen fueron solicitados durante la atención de un paciente en código azul (emergencia crítica), identificando el consecutivo del evento, el centro de atención y la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los servicios de laboratorio e imágenes diagnósticas solicitados internamente durante un evento de Código Azul para un paciente e ingreso específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso y folio de Código Azul indicados en HCORDLABI o HCORDIMAI; Las órdenes deben estar marcadas como no externas (MANEXTPRO=''0'') y de tipo historia ''CODIGOAZU''; Los códigos de servicio deben estar catalogados en INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes cuyo tipo de historia es ''CODIGOAZU''; Se excluyen procedimientos manejados externamente (MANEXTPRO distinto de ''0''); El UNION elimina duplicados entre órdenes de laboratorio e imágenes con la misma descripción y datos clave; Solo se retornan servicios que existan en el catálogo INCUPSIPS (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código Azul; Paciente; Ingreso/Admisión hospitalaria; Órdenes de laboratorio; Órdenes de imágenes diagnósticas; Catálogo CUPS/IPS; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABI: Cuando IPCODPACI=@CodigoPaciente, NUMINGRES=@NumeroIngreso, NUMEFOLIO=@NumeroCodigoAzul, MANEXTPRO=''0'' e IDETIPHIS=''CODIGOAZU'' → retorna las órdenes de laboratorio asociadas al Código Azul; [RETURN_RESULT] HCORDIMAI: Cuando IPCODPACI=@CodigoPaciente, NUMINGRES=@NumeroIngreso, NUMEFOLIO=@NumeroCodigoAzul, MANEXTPRO=''0'' e IDETIPHIS=''CODIGOAZU'' → retorna las órdenes de imágenes asociadas al Código Azul, unificadas con las de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABI; dbo.HCORDIMAI; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzulInternos';
-- GO
