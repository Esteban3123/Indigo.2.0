

CREATE VIEW [dbo].[INVProductosInterfazNet]
AS
Select 1 as Nombre
--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS DescripcionCorta, RTRIM(A.IPRDESLAR) AS DescripcionLarga, 
--                      CASE WHEN IPRCLAPRO = '0' THEN 'Producto' WHEN IPRCLAPRO = '1' THEN 'Servicio' END AS Clase, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo, 
--                      RTRIM(B.IUNUNICOM) AS UCompra, RTRIM(B1.IUNUNICOM) AS UConsumo, RTRIM(A.IPRCONCEN) AS Concentracion
--FROM         DGEMPRES12.dbo.INNPRODUC AS A LEFT OUTER JOIN
--                      DGEMPRES12.dbo.INNUNIDAD AS B ON A.INNUNIDADD = B.OID LEFT OUTER JOIN
--                      DGEMPRES12.dbo.INNUNIDAD AS B1 ON B.IUNCODIGO = B1.IUNCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de interfaz de productos e insumos del módulo de inventario, actualmente desactivada (el cuerpo real está comentado). Cuando estaba operativa, exponía el catálogo de productos y servicios con su código, descripción corta, descripción larga, clase (producto o servicio), tipo (ninguno, suministro o medicamento), unidad de compra, unidad de consumo y concentración. Servía como capa de integración entre el sistema de inventario (INNPRODUC) y aplicaciones externas o módulos que consumen el maestro de artículos, medicamentos e insumos hospitalarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosInterfazNet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosInterfazNet';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista placeholder que retorna una constante; la lógica real de exposición de productos de inventario está comentada y deshabilitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista no expone datos de productos; cualquier consumidor recibirá únicamente el literal 1.; El código original (comentado) pretendía mapear códigos de clase (''0''=Producto,''1''=Servicio) y de tipo (''0''=Ninguno,''1''=Suministro,''2''=Medicamento), pero esa lógica no está activa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Servicio; Suministro; Medicamento; Unidad de compra; Unidad de consumo; Concentración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Siempre retorna una única columna ''Nombre'' con valor constante 1, sin consultar tablas reales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosInterfazNet';
GO
