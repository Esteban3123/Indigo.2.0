
CREATE PROCEDURE [dbo].[SP_INV_ListarCodigosFacturacion]
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
	SELECT GCFCODIGO AS CODIGO,GCFNOMBRE AS NOMBRE, RTRIM(GCFCODIGO) + ' - ' + RTRIM(GCFNOMBRE) AS FACTURACION
	FROM DBO.GECONFAC
ELSE
	SELECT OID,GCFCODIGO AS CODIGO,GCFNOMBRE AS NOMBRE, RTRIM(GCFCODIGO) + ' - ' + RTRIM(GCFNOMBRE) AS FACTURACION
	FROM DBO.GENCONFAC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los códigos y nombres de configuración de facturación disponibles en el sistema. Según la versión del ERP indicada, consulta la tabla de configuración de facturación correspondiente (versión 1 o superior) y devuelve el código, el nombre y una descripción combinada en formato ''código - nombre''. Se usa para poblar selectores o catálogos de conceptos de facturación en formularios de liquidación, facturación de servicios de salud y parametrización contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosFacturacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los códigos y nombres de facturación disponibles, eligiendo la tabla origen según la versión del ERP solicitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar la versión del ERP para determinar la fuente de datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo FACTURACION siempre se construye como ''CODIGO - NOMBRE'' con espacios derechos eliminados; Sólo la versión distinta de 1 expone el identificador OID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Códigos de facturación; Versión de ERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.GECONFAC: Cuando la versión de ERP es 1, devuelve código, nombre y concatenación ''código - nombre'' desde GECONFAC; [RETURN_RESULT] DBO.GENCONFAC: Cuando la versión de ERP es distinta de 1, devuelve OID, código, nombre y concatenación ''código - nombre'' desde GENCONFAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión ERP = 1 → Consulta la tabla GECONFAC sin incluir OID else Consulta la tabla GENCONFAC incluyendo el OID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.GECONFAC; DBO.GENCONFAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosFacturacion';
-- GO
