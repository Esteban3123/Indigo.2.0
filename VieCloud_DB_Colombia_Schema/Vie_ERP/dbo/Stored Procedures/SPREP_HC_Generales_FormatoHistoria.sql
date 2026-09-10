
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_FormatoHistoria]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@TipoHistoria Char(1)

)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODTIPHIS as 'CODIGO TIPO DE HISTORIA' 
FROM HCUNITHIS A with(nolock)
INNER JOIN HCHISTORI B with(nolock) ON A.CODTIPHIS=B.CODTIPHIS 
 WHERE A.CODCENATE=@CentroAtencion AND A.UFUCODIGO=@UnidadFuncional AND B.FORTIPHIS=@TipoHistoria
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los tipos de historia clínica disponibles para una combinación específica de centro de atención, unidad funcional y formato de historia (ambulatorio, hospitalario, urgencias, etc.). Cruza la configuración de la unidad funcional (HCUNITHIS) con el catálogo de tipos de historia clínica (HCHISTORI) para devolver únicamente los tipos habilitados según el formato solicitado. Se utiliza para determinar qué plantillas o formatos de historia clínica puede abrir o registrar un profesional en una unidad de atención concreta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los códigos de tipo de historia clínica configurados para un centro de atención y unidad funcional específicos, filtrados por el formato (tipo) de historia solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre HCUNITHIS y HCHISTORI por CODTIPHIS; Los códigos de centro de atención y unidad funcional deben corresponder a registros válidos en HCUNITHIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan tipos de historia que estén asociados simultáneamente a la unidad funcional/centro de atención y que tengan el formato de historia indicado; Las consultas se realizan con NOLOCK, asumiendo lecturas sucias aceptables', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de historia clínica; Centro de atención; Unidad funcional; Formato de historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCUNITHIS: Cuando HCUNITHIS.CODCENATE coincide con el centro de atención, HCUNITHIS.UFUCODIGO coincide con la unidad funcional y HCHISTORI.FORTIPHIS coincide con el formato indicado, se retorna el CODTIPHIS asociado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCUNITHIS; dbo.HCHISTORI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_FormatoHistoria';
-- GO
