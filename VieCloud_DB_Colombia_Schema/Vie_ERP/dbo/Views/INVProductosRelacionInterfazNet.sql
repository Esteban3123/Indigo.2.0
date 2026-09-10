

CREATE VIEW [dbo].[INVProductosRelacionInterfazNet]
AS
 Select 1 as Nombre

--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS Descripcion, C.CODPRODUC AS CodigoHC, C.DESPRODUC AS DescripcionHC, 
--                      CASE WHEN IPRCLAPRO = '0' THEN 'Producto' WHEN IPRCLAPRO = '1' THEN 'Servicio' END AS Clase, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo
--FROM         DGEMPRES12.dbo.INNPRODUC AS A INNER JOIN
--                      dbo.IHRINDDGH AS B ON A.IPRCODIGO = B.IPRCODIGO INNER JOIN
--                      dbo.IHLISTPRO AS C ON B.CODPRODUC = C.CODPRODUC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona productos del inventario con su equivalente en el sistema de historia clínica (HC), permitiendo cruzar el código y descripción del producto en el módulo de inventario con el código y descripción correspondiente en HC. Incluye la clasificación del ítem (Producto o Servicio) y su tipo (Ninguno, Suministro o Medicamento). Actualmente la consulta principal está comentada y retorna solo un valor fijo de prueba, por lo que no está operativa; fue diseñada como interfaz de integración entre el módulo de inventario y el módulo clínico para sincronizar catálogos de productos, medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosRelacionInterfazNet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INVProductosRelacionInterfazNet';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista placeholder que actualmente solo devuelve una constante; su lógica real de relación entre productos de inventario y catálogo clínico está comentada y no operativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosRelacionInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista no expone datos de productos ni de su relación con el módulo clínico mientras el SELECT real permanezca comentado.; El resultado es determinístico: una fila con valor constante 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosRelacionInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Integración inventario-clínico (según comentario descriptivo); Catálogo de productos, medicamentos e insumos (según comentario descriptivo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosRelacionInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Siempre retorna una única fila con el valor 1 bajo la columna Nombre, sin consultar tablas reales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosRelacionInterfazNet';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INVProductosRelacionInterfazNet';
GO
