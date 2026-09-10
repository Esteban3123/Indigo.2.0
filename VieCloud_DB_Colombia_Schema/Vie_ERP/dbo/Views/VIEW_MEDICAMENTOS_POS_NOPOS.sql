

CREATE VIEW [dbo].[VIEW_MEDICAMENTOS_POS_NOPOS]
AS
SELECT DISTINCT PCRD.CODPRODUC AS CÓDIGO_PRODUCTO, PRO.DESPRODUC AS NOMBRE_PRODUCTO, 
CASE WHEN PRO.NOPOSPROD = 0 THEN 'Medicamento POS' WHEN PRO.NOPOSPROD = 1 THEN 'Medicamento NO POS' END AS Tipo_Medicamento,
PCRD.CANPEDPRO AS CANTIDAD_PEDIDA_PRODUTO, PCRA.DESADMINI AS DESCRIPCIÓN_ADMINISTRACIÓN, PCRC.FECHAORDE AS FECHA_ORDEN, UF.UFUDESCRI AS DESCRIPCIÓN_UNIDAD_FUNCIONAL, PCRD.NUMINGRES,
PCRC.IPCODPACI AS DOCUMENTO_PACIENTE, INP.IPNOMCOMP AS NOMBRE_PACIENTE
FROM dbo.HCPRESCRC PCRC
INNER JOIN dbo.INPACIENT INP ON PCRC.IPCODPACI = INP.IPCODPACI
INNER JOIN dbo.INUNIFUNC UF ON UF.UFUCODIGO = PCRC.UFUCODIGO
INNER JOIN dbo.HCPRESCRD PCRD ON PCRD.IPCODPACI = INP.IPCODPACI
INNER JOIN dbo.IHLISTPRO PRO ON PRO.CODPRODUC = PCRD.CODPRODUC
INNER JOIN dbo.HCPRESCRA PCRA ON PCRA.IPCODPACI = INP.IPCODPACI
Where /*PRO.NOPOSPROD = 1 AND*/ PRO.TIPPRODUC = 1 AND PCRC.FECHAORDE Between '01-07-2019' AND '02-07-2019' ---dd-MM-yyyy
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las prescripciones de medicamentos POS y NO POS ordenadas a pacientes, combinando el encabezado de la receta médica (HCPRESCRC), el detalle del medicamento prescrito (HCPRESCRD), la información de administración (HCPRESCRA), el catálogo de productos farmacéuticos (IHLISTPRO) y el maestro de unidades funcionales (INUNIFUNC). Para cada línea de prescripción muestra el código y nombre del medicamento, si es POS o NO POS, la cantidad pedida, la descripción de administración, la fecha de la orden médica, la unidad funcional donde se generó, el número de ingreso, y el documento y nombre completo del paciente. Sirve para reportería y auditoría de medicamentos cubiertos y no cubiertos por el plan de beneficios (PBS), análisis de recetas, consumo farmacéutico por paciente e ingreso, y seguimiento de prescripciones en un rango de fechas determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos prescritos clasificados como POS o NO POS con datos del paciente, unidad funcional y orden, en un rango de fechas definido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe estar catalogado como medicamento (TIPPRODUC = 1).; Las prescripciones deben tener fecha de orden entre 01-07-2019 y 02-07-2019.; Debe existir relación entre paciente, unidad funcional, encabezado/detalle/administración de prescripción y catálogo de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos cuyo tipo es medicamento (TIPPRODUC=1).; Solo se reportan prescripciones dentro del rango de fechas fijo del 01 al 02 de julio de 2019.; La clasificación POS/NO POS depende exclusivamente del campo NOPOSPROD del catálogo de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento POS; Medicamento NO POS; Prescripción médica; Paciente; Unidad funcional; Orden médica; Administración de medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas distintas de medicamentos prescritos clasificados según NOPOSPROD: 0=''Medicamento POS'', 1=''Medicamento NO POS''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.NOPOSPROD = 0 → Etiqueta el producto como ''Medicamento POS''. else Si NOPOSPROD = 1 etiqueta como ''Medicamento NO POS''; otros valores quedan en NULL.; si PRO.TIPPRODUC = 1 AND PCRC.FECHAORDE BETWEEN ''01-07-2019'' AND ''02-07-2019'' → Incluye la prescripción en el resultado. else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRC; dbo.INPACIENT; dbo.INUNIFUNC; dbo.HCPRESCRD; dbo.IHLISTPRO; dbo.HCPRESCRA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_MEDICAMENTOS_POS_NOPOS';
GO
