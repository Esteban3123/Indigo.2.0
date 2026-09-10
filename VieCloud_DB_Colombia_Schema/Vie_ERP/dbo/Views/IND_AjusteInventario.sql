
CREATE VIEW [dbo].[IND_AjusteInventario]
AS
SELECT        TOP (100) PERCENT D.IPCODPACI AS DOCUMENTO, P.IPNOMCOMP AS NOMBRE, D.NUMINGRES AS INGRESO, U.UFUDESCRI AS UNIDAD_FUNCIONAL, D.CODPRODUC AS COD_PRODUC, 
                         H.DESPRODUC AS PRODUCTO, US.NOMUSUARI AS USU_AJUSTE, D.CANTTOTAL AS CANT_TOTAL, D.CANTAJUST AS CANT_AJUSTE, F.DESCODAJU AS CONCEPTO_AJUSTE, 
                         CASE WHEN C.TIPOAJUSTE = '1' THEN 'ENTRADA' WHEN C.TIPOAJUSTE = '2' THEN 'SALIDA' END AS TIPO_AJUSTE, C.OBSERVACI AS OBSERVACION, C.FECHAREGIS AS FECHA_AJUSTE
FROM            dbo.HCAJUSINC AS C INNER JOIN
                         dbo.HCAJUSIND AS D ON D.CODCONCEC = C.CODCONCEC INNER JOIN
                         dbo.INPACIENT AS P ON P.IPCODPACI = C.IPCODPACI INNER JOIN
                         dbo.IHLISTPRO AS H ON H.CODPRODUC = D.CODPRODUC INNER JOIN
                         dbo.INUNIFUNC AS U ON U.UFUCODIGO = D.UFUCODIGO INNER JOIN
                         dbo.SEGusuaru AS US ON US.CODUSUARI = D.CODUSUARI INNER JOIN
                         dbo.HCCONAJUS AS F ON F.CODCONAJU = C.CODCONAJU
WHERE        (C.CODCENATE = '001') AND (C.FECHAREGIS >= '01/03/2016 00:00')
ORDER BY FECHA_AJUSTE DESC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ajustes de inventario de medicamentos e insumos realizados durante los ingresos de pacientes en el centro de atención. Combina el encabezado del ajuste (HCAJUSINC) con el detalle por producto (HCAJUSIND), enriqueciendo la información con el nombre del paciente, descripción del producto farmacéutico o dispositivo médico, unidad funcional donde ocurrió el ajuste, usuario responsable y el concepto o motivo del ajuste. Permite identificar si cada movimiento fue una entrada o salida de inventario, incluyendo cantidades originales y ajustadas, observaciones y fecha del ajuste. Sirve para auditoría, control de inventario clínico y seguimiento de correcciones de medicamentos e insumos aplicadas sobre historias clínicas de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AjusteInventario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AjusteInventario';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los ajustes de inventario (entradas/salidas) realizados sobre insumos consumidos por pacientes en un centro de atención específico, mostrando datos del paciente, producto, usuario que ajusta, concepto y tipo de movimiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCAJUSINC con CODCENATE=''001'' y FECHAREGIS >= 01/03/2016; Integridad referencial entre encabezado de ajuste (HCAJUSINC) y detalle (HCAJUSIND) por CODCONCEC; Existencia de paciente, producto, unidad funcional, usuario y concepto de ajuste relacionados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ajustes del centro de atención ''001''; Solo se exponen ajustes registrados desde el 01/03/2016 en adelante; El tipo de ajuste solo puede interpretarse como ENTRADA o SALIDA; cualquier otro código no se etiqueta; Cada ajuste mostrado debe tener paciente, producto, unidad funcional, usuario y concepto válidos (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ajuste de inventario; Paciente; Ingreso; Unidad funcional; Producto/Insumo; Concepto de ajuste; Centro de atención; Entrada/Salida de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando C.CODCENATE=''001'' AND C.FECHAREGIS >= ''01/03/2016 00:00'' → se retorna fila con datos del ajuste de inventario ordenada por FECHA_AJUSTE DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.TIPOAJUSTE = ''1'' → Clasifica el movimiento como ''ENTRADA'' else Si TIPOAJUSTE=''2'' clasifica como ''SALIDA''; otros valores quedan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCAJUSINC; dbo.HCAJUSIND; dbo.INPACIENT; dbo.IHLISTPRO; dbo.INUNIFUNC; dbo.SEGusuaru; dbo.HCCONAJUS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AjusteInventario';
GO
