-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetTimeUnitList] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Common].[TimeUnit]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devuelve la lista completa de unidades de tiempo disponibles en el sistema (por ejemplo: minutos, horas, días, semanas). Consulta el catálogo de unidades de tiempo utilizado como referencia en agendamientos, órdenes médicas y configuraciones generales. Se usa para poblar listas desplegables o selectores en formularios donde se debe indicar una duración o intervalo de tiempo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetTimeUnitList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetTimeUnitList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo completo de unidades de tiempo disponibles para uso en el sistema.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No aplica filtros ni transformaciones; expone el catálogo tal cual está almacenado', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad de tiempo; Catálogo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.TimeUnit: Retorna todas las filas y columnas de Common.TimeUnit sin filtros', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.TimeUnit', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetTimeUnitList';
-- GO
