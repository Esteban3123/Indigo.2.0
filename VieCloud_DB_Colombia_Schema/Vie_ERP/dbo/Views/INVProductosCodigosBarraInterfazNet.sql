

CREATE VIEW [dbo].[INVProductosCodigosBarraInterfazNet]
AS
Select 1 as Nombre
--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS Descripcion, B.CODBARRAS AS CodigoBarras, B.FECHCREAC AS FechaCreacion, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo
--FROM         DGEMPRES12.dbo.INNPRODUC AS A INNER JOIN
--                      dbo.IHCODBARR AS B ON A.IPRCODIGO = B.IPRCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista actualmente deshabilitada (retorna solo un valor de prueba) que originalmente mostraba el catálogo de productos del inventario junto con sus códigos de barras. Integraba información de productos (código interno, descripción corta y tipo: Ninguno, Suministro o Medicamento) con sus respectivos códigos de barras y fecha de creación. Sirve como interfaz de consulta para sistemas externos (.NET) que necesitan identificar productos del inventario —como medicamentos e insumos— mediante código de barras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosCodigosBarraInterfazNet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosCodigosBarraInterfazNet';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista placeholder que devuelve un valor constante; la lógica original de exponer productos con sus códigos de barras está comentada y no se ejecuta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosCodigosBarraInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista actualmente no expone datos reales: su cuerpo activo retorna una constante fija en lugar de la consulta original sobre productos y códigos de barras.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosCodigosBarraInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Siempre retorna una única fila con el valor 1 bajo la columna Nombre, sin consultar tablas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosCodigosBarraInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosCodigosBarraInterfazNet';
GO
