-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 31/01/2014
-- Description:	Sp para validar que el contenedor exista
-- =============================================
CREATE PROCEDURE [Glosas].[SP_ValidateContainer]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	Respuesta  varchar(100)
	)

	INSERT INTO @tablaTmp
	SELECT count(*) FROM sys.databases where UPPER(name) = UPPER(@Empresa)
	
	select * from @tablaTmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que verifica si existe una base de datos (contenedor/empresa) registrada en el servidor SQL Server, consultando el catálogo del sistema sys.databases con el nombre de empresa recibido como parámetro. Retorna un conteo numérico: si devuelve 1 o más, la empresa/base de datos existe; si devuelve 0, no existe. Se usa en el módulo de Glosas para validar que el contenedor de datos de la empresa esté disponible antes de ejecutar operaciones de glosas o facturación entre empresas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateContainer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateContainer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si existe una base de datos (contenedor) con el nombre de la empresa indicada, devolviendo el conteo de coincidencias.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario ejecutor debe tener permisos de lectura sobre sys.databases; Se debe proporcionar el nombre de la empresa/contenedor a validar', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La comparación de nombres es case-insensitive al aplicar UPPER en ambos lados; Siempre retorna exactamente una fila con un valor numérico (0 o más)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosas; Facturación entre empresas; Contenedor de datos por empresa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] sys.databases: Devuelve el conteo de bases de datos cuyo nombre (en mayúsculas) coincide con el nombre de empresa recibido; 0 indica que el contenedor no existe.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.databases', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateContainer';
-- GO
