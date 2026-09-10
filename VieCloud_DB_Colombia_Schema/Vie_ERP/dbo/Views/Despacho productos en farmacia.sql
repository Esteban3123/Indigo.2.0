

CREATE VIEW [dbo].[Despacho productos en farmacia]
AS
SELECT        dbo.HCKARDPAC.IPCODPACI AS DOCUMENTO, dbo.HCKARDPAC.NUMINGRES AS INGRESO, dbo.INPACIENT.IPNOMCOMP AS NOMBRE, 
                         dbo.INUNIFUNC.UFUDESCRI AS [UNIDAD FUNCIONAL], dbo.HCKARDPAC.FECREGKAR AS [FECHA DEL REGISTRO], 
                         dbo.HCKARDPAC.CODPRODUC AS [CODIGO PRODUCTO], dbo.IHLISTPRO.DESPRODUC AS [NOMBRE PRODUCTO], 
                         dbo.HCKARDPAC.CANPRODUCT AS [CANTIDAD SOLICITADA], dbo.HCKARDPAC.DESMOVPRO AS EVENTO
FROM            dbo.HCKARDPAC INNER JOIN
                         dbo.INUNIFUNC ON dbo.HCKARDPAC.UFUCODIGO = dbo.INUNIFUNC.UFUCODIGO INNER JOIN
                         dbo.INPACIENT ON dbo.HCKARDPAC.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                         dbo.IHLISTPRO ON dbo.HCKARDPAC.CODPRODUC = dbo.IHLISTPRO.CODPRODUC
WHERE        (dbo.HCKARDPAC.FECREGKAR BETWEEN '23/05/2013 00:00:00' AND '23/05/2013 23:59:59') AND (dbo.HCKARDPAC.TIPORIREG = '1' OR
                         dbo.HCKARDPAC.TIPORIREG = '2' OR
                         dbo.HCKARDPAC.TIPORIREG = '3' OR
                         dbo.HCKARDPAC.TIPORIREG = '9')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los despachos y movimientos de productos farmacéuticos realizados en farmacia para un día específico (23/05/2013), cruzando el kárdex de medicamentos por paciente (HCKARDPAC) con el catálogo de productos (IHLISTPRO), el maestro de pacientes (INPACIENT) y las unidades funcionales (INUNIFUNC). Muestra por cada movimiento de dispensación: la cédula e identificación del paciente, el número de ingreso u hospitalización, el nombre completo del paciente, el área o sala de atención (unidad funcional), la fecha del registro, el código y nombre del medicamento o insumo despachado, la cantidad solicitada y el tipo de evento o movimiento (entrega, devolución, ajuste). Filtra únicamente los tipos de registro 1, 2, 3 y 9, correspondientes a movimientos válidos de dispensación. Sirve para reportería de consumo farmacéutico, auditoría de entregas de medicamentos e insumos, y trazabilidad del despacho de productos a pacientes hospitalizados o en atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Despacho productos en farmacia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Despacho productos en farmacia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los movimientos de despacho de productos farmacéuticos a pacientes ocurridos en una fecha específica, mostrando datos del paciente, unidad funcional, producto y cantidad solicitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe correspondencia referencial entre el kárdex y los catálogos de unidad funcional, paciente y productos (los INNER JOIN exigen coincidencia en UFUCODIGO, IPCODPACI y CODPRODUC).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos cuyo tipo de origen pertenezca al conjunto {1,2,3,9}.; El rango de fechas está fijo (hard-coded) al 23/05/2013, por lo que la vista no es paramétrica respecto al período.; Se excluyen movimientos sin paciente, unidad funcional o producto válido en los catálogos relacionados (por uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Despacho de productos farmacéuticos; Kárdex del paciente; Unidad funcional; Paciente; Producto/medicamento; Ingreso hospitalario; Tipo de origen de movimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo movimientos del kárdex con FECREGKAR entre ''23/05/2013 00:00:00'' y ''23/05/2013 23:59:59'' y TIPORIREG en (''1'',''2'',''3'',''9'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCKARDPAC.TIPORIREG IN (''1'',''2'',''3'',''9'') → Se incluye el movimiento del kárdex en el resultado else Se excluye del resultado; si HCKARDPAC.FECREGKAR BETWEEN ''23/05/2013 00:00:00'' AND ''23/05/2013 23:59:59'' → Se considera el registro como despacho del día reportado else Se descarta del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCKARDPAC; dbo.INUNIFUNC; dbo.INPACIENT; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Despacho productos en farmacia';
GO
