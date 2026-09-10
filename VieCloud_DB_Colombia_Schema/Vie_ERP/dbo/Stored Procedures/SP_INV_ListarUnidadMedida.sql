
CREATE PROCEDURE [dbo].[SP_INV_ListarUnidadMedida]
(
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @VersionERP=1

	SELECT A.IUNCODIGO AS 'CODIGO UNIDAD MEDIDA',A.IUNNOMBRE AS 'UNIDAD COMPRA',A.IUNFRACCI AS 'FRACCIONES',B.IUNNOMBRE AS 'UNIDAD CONSUMO', RTRIM(A.IUNCODIGO) + ' - '+ RTRIM(A.IUNNOMBRE) + ' - ' + RTRIM(B.IUNNOMBRE) AS 'UNIDAD MEDIDA'
	FROM DBO.INUNIDAD A
	INNER JOIN DBO.INUNIDAD B ON A.IUNCODIGO = B.IUNCODUND

ELSE
	
	SELECT A.OID,A.IUNCODIGO AS 'CODIGO UNIDAD MEDIDA',A.IUNUNICOM AS 'UNIDAD COMPRA',A.IUNFRACCI AS 'FRACCIONES',B.IUNUNICOM AS 'UNIDAD CONSUMO', RTRIM(A.IUNCODIGO) + ' - '+ RTRIM(A.IUNUNICOM) AS 'UNIDAD MEDIDA'
	FROM DBO.INNUNIDAD A
	INNER JOIN DBO.INNUNIDAD B ON A.OID = B.INNUNIDAD

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las unidades de medida utilizadas en el módulo de inventario, mostrando el código de unidad, la unidad de compra, la cantidad de fracciones y la unidad de consumo. Soporta dos versiones del ERP mediante el parámetro @VersionERP: la versión 1 consulta la tabla INUNIDAD (estructura antigua) y las versiones posteriores consultan INNUNIDAD (estructura nueva). Se usa para poblar selectores o catálogos de unidades de medida en procesos de compra, dispensación y gestión de insumos o medicamentos en inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarUnidadMedida';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarUnidadMedida';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades de medida de inventario combinando unidad de compra y unidad de consumo, eligiendo la tabla origen según la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse la versión del ERP para decidir la tabla a consultar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La descripción ''UNIDAD MEDIDA'' siempre se construye concatenando código y nombres de unidades con separador '' - ''; Solo se devuelven unidades que tengan correspondencia entre unidad de compra y unidad de consumo (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad de medida; Unidad de compra; Unidad de consumo; Fracciones; Versión de ERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.INUNIDAD: Cuando la versión del ERP es 1, se retorna el listado desde INUNIDAD auto-relacionada por IUNCODIGO = IUNCODUND mostrando unidad de compra y unidad de consumo; [RETURN_RESULT] DBO.INNUNIDAD: Cuando la versión del ERP no es 1, se retorna el listado desde INNUNIDAD auto-relacionada por OID = INNUNIDAD mostrando unidad de compra y unidad de consumo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión del ERP = 1 → Consulta INUNIDAD usando IUNCODIGO/IUNCODUND y campos IUNNOMBRE else Consulta INNUNIDAD usando OID/INNUNIDAD y campos IUNUNICOM, incluyendo OID en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INUNIDAD; DBO.INNUNIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarUnidadMedida';
-- GO
