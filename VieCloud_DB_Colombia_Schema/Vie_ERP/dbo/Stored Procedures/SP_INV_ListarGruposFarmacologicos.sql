
CREATE PROCEDURE [dbo].[SP_INV_ListarGruposFarmacologicos]
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

		SELECT FARCODIGO AS 'CODIGO DEL GRUPO FARMACOLOGICO',FARNOMBRE AS 'NOMBRE DEL GRUPO FARMACOLOGICO', RTRIM(FARCODIGO) + ' - '+ RTRIM(FARNOMBRE) AS 'GRUPO FARMACOLOGICO'
		FROM DBO.INFARCOL
		
ELSE
	
		SELECT OID,FARCODIGO AS 'CODIGO DEL GRUPO FARMACOLOGICO',FARNOMBRE AS 'NOMBRE DEL GRUPO FARMACOLOGICO', RTRIM(FARCODIGO) + ' - '+ RTRIM(FARNOMBRE) AS 'GRUPO FARMACOLOGICO'
		FROM DBO.INNFARCOL

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los grupos farmacológicos disponibles en el sistema de inventario de medicamentos. Según la versión del ERP configurada, consulta la tabla correspondiente (INFARCOL para versión 1 o INNFARCOL para versiones posteriores) y devuelve el código, el nombre y una descripción combinada de cada grupo farmacológico. Se utiliza para poblar listas desplegables o catálogos de clasificación de medicamentos por grupo terapéutico o farmacológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo de grupos farmacológicos seleccionando la tabla origen según la versión del ERP en uso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse la versión del ERP para decidir el origen de datos (tabla legacy vs. nueva).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo concatenado ''GRUPO FARMACOLOGICO'' siempre se construye como código + '' - '' + nombre, recortando espacios con RTRIM.; La versión 1 del ERP nunca expone el OID; las versiones posteriores siempre lo incluyen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo farmacológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Si @VersionERP=1 retorna código, nombre y etiqueta concatenada desde INFARCOL.; [RETURN_RESULT] Resultset: Si @VersionERP<>1 retorna OID, código, nombre y etiqueta concatenada desde INNFARCOL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Lista grupos farmacológicos desde INFARCOL (versión legacy, sin OID). else Lista grupos farmacológicos desde INNFARCOL incluyendo OID (nueva versión).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INFARCOL; DBO.INNFARCOL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarGruposFarmacologicos';
-- GO
