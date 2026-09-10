

Create VIEW [dbo].[VIEW_PACIENTES_MEDICAMENTOS_NOPOS]

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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los medicamentos prescritos a pacientes, clasificándolos como POS o NO POS, a partir de las órdenes médicas registradas en la historia clínica. Combina el encabezado de la prescripción (HCPRESCRC), el detalle del medicamento solicitado (HCPRESCRD), la vía de administración (HCPRESCRA), el catálogo de productos farmacéuticos (IHLISTPRO) y los datos del paciente (INPACIENT), enriqueciendo cada registro con la unidad funcional donde se generó la orden (INUNIFUNC). Sirve para reportería de medicamentos no incluidos en el Plan de Beneficios en Salud (NO POS), mostrando por cada prescripción el documento y nombre del paciente, el número de ingreso, el producto solicitado con su cantidad, la fecha de la orden, la vía de administración y el servicio o área que la emitió. Es útil para auditorías de recobro, gestión de medicamentos especiales, control de gasto NO POS y seguimiento de solicitudes de medicamentos por paciente y unidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los medicamentos prescritos a pacientes en un rango fijo de fechas, clasificándolos como POS o NO POS junto con datos de la orden, paciente y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de prescripciones (HCPRESCRC, HCPRESCRD, HCPRESCRA) asociadas al mismo paciente; El producto debe existir en el catálogo IHLISTPRO con TIPPRODUC = 1 (medicamento); El paciente debe estar registrado en INPACIENT y la unidad funcional en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos cuyo tipo corresponde a medicamentos (TIPPRODUC = 1); El rango de fechas de la orden está fijo entre 01-07-2019 y 02-07-2019; Se eliminan duplicados mediante DISTINCT; Solo aparecen pacientes con prescripciones (encabezado, detalle y administración) que coincidan por documento de paciente; La clasificación POS/NO POS depende exclusivamente del flag NOPOSPROD del catálogo de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Medicamento POS; Medicamento NO POS; Prescripción médica; Unidad funcional; Orden médica; Administración de medicamento; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESCRC: Devuelve filas distintas de prescripciones de medicamentos (TIPPRODUC=1) cuyas órdenes (FECHAORDE) estén entre 01-07-2019 y 02-07-2019, cruzando encabezado, detalle y administración de la receta con datos de paciente, producto y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.NOPOSPROD = 0 → Se etiqueta como ''Medicamento POS'' else Si NOPOSPROD = 1 se etiqueta como ''Medicamento NO POS''; otros valores quedan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRC; dbo.INPACIENT; dbo.INUNIFUNC; dbo.HCPRESCRD; dbo.IHLISTPRO; dbo.HCPRESCRA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_NOPOS';
GO
