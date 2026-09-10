

CREATE PROCEDURE [dbo].[SP_HCJustificacionesNoPos]
AS
BEGIN
	SET NOCOUNT ON;
SELECT   ROW_NUMBER() OVER (ORDER BY A.CODCONCEC) AS NUMEROFILA, A.CODCONCEC,  A.CODPRODUC
FROM         dbo.HCJUNOPOM AS A INNER JOIN
                      dbo.INPACIENT AS B ON A.IPCODPACI = B.IPCODPACI INNER JOIN
                      dbo.INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO INNER JOIN
                      dbo.HCFARMEPD AS D ON A.IDETIPHIS = D.IDETIPHIS AND A.NUMEFOLIO = D.NUMEFOLIO AND A.IPCODPACI = D.IPCODPACI AND 
                      A.NUMINGRES = D.NUMINGRES AND A.CODCENATE = D.CODCENATE AND A.UFUCODIGO = D.UFUCODIGO AND A.CODPRODUC = D.CODPRODUC INNER JOIN
                      dbo.IHLISTPRO AS E ON A.CODPRODUC = E.CODPRODUC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el listado de justificaciones de medicamentos o insumos No POS (no incluidos en el Plan de Beneficios en Salud) registradas en la historia clínica. Cruza las justificaciones de No POS (HCJUNOPOM) con la información del paciente, la unidad funcional de atención, el detalle de la dispensación farmacéutica (HCFARMEPD) y el catálogo maestro de productos (IHLISTPRO), para consolidar por cada registro el código de concepto No POS y el producto justificado. Se utiliza para auditoría clínica, control de prescripciones fuera del plan y soporte a procesos de facturación o glosas por tecnologías no cubiertas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HCJustificacionesNoPos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HCJustificacionesNoPos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las justificaciones de medicamentos No POS asociadas a pedidos de farmacia válidos, devolviendo el concepto y producto numerados secuencialmente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las justificaciones No POS deben estar enlazadas a un paciente, unidad funcional, pedido de farmacia y producto válidos en los catálogos correspondientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven justificaciones No POS cuyo medicamento existe simultáneamente como pedido en farmacia (HCFARMEPD) coincidiendo en tipo de historia, folio, paciente, ingreso, centro de atención, unidad funcional y producto.; El paciente, la unidad funcional y el producto deben existir en sus respectivos maestros (INPACIENT, INUNIFUNC, IHLISTPRO); de lo contrario la fila se excluye.; El resultado se enumera secuencialmente mediante un número de fila ordenado por el concepto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Justificación No POS; Medicamento ordenado; Paciente; Unidad funcional; Pedido de farmacia; Producto farmacéutico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCJUNOPOM: Devuelve un conjunto de resultados con numeración por ROW_NUMBER ordenada por CODCONCEC, incluyendo el concepto y el producto, solo cuando existe coincidencia con paciente, unidad funcional, pedido de farmacia (por tipo de historia, folio, paciente, ingreso, centro de atención, unidad funcional y producto) y catálogo de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCJUNOPOM; dbo.INPACIENT; dbo.INUNIFUNC; dbo.HCFARMEPD; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCJustificacionesNoPos';
-- GO
