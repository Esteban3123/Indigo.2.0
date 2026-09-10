
CREATE PROCEDURE [dbo].[SP_INV_ListarGrupos]
(
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
IF @VersionERP=1
    
    SELECT RTRIM(IGRCODIGO) AS 'CODIGO DEL GRUPO',IGRNOMBRE AS 'NOMBRE DEL GRUPO', RTRIM(IGRCODIGO) + ' - ' + RTRIM(IGRNOMBRE) AS 'GRUPO'
    FROM DBO.INGRUPOS
       
    ELSE
        
    SELECT OID ,RTRIM(IGRCODIGO) AS 'CODIGO DEL GRUPO',IGRNOMBRE AS 'NOMBRE DEL GRUPO', RTRIM(IGRCODIGO) + ' - ' + RTRIM(IGRNOMBRE) AS 'GRUPO'
    FROM DBO.INNGRUPO

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los grupos de inventario disponibles en el sistema, consultando la tabla correspondiente según la versión del ERP configurada. Para la versión 1 devuelve el código y nombre del grupo desde la tabla clásica de grupos; para versiones posteriores incluye además el identificador único (OID) desde la tabla nueva de grupos. Se usa para poblar selectores o filtros de clasificación de ítems de inventario en formularios y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarGrupos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarGrupos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los grupos de inventario, eligiendo entre la tabla legacy o la nueva según la versión del ERP, para alimentar selectores/filtros de clasificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse la versión del ERP para determinar el origen de datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos y nombres se devuelven sin espacios a la derecha (RTRIM); Siempre se expone una columna compuesta ''código - nombre'' como etiqueta del grupo; Solo la versión nueva del ERP expone el identificador único OID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupos de inventario; Clasificación de ítems; Versión del ERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.INGRUPOS: Cuando la versión del ERP es 1, retorna código, nombre y etiqueta concatenada ''código - nombre'' desde la tabla legacy INGRUPOS; [RETURN_RESULT] DBO.INNGRUPO: Cuando la versión del ERP es distinta de 1, retorna OID, código, nombre y etiqueta concatenada desde la tabla nueva INNGRUPO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión ERP = 1 → Consulta INGRUPOS sin incluir OID else Consulta INNGRUPO incluyendo el identificador único OID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INGRUPOS; DBO.INNGRUPO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGrupos';
-- GO
