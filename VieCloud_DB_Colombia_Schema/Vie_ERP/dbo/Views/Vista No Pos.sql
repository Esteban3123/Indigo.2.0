CREATE VIEW [dbo].[Vista No Pos]
AS
     SELECT a.IPCODPACI AS Cedula, 
            e.IPNOMCOMP AS Paciente, 
            RTRIM(LTRIM(dbo.Edad(CONVERT(VARCHAR, e.IPFECNACI, 105), CONVERT(VARCHAR, GETDATE(), 105)))) AS Edad, 
            D.CODDIAGNO AS Cod_Diag, 
            V.NOMDIAGNO AS Diagnostico, 
            a.NUMINGRES AS Ingreso, 
            Z.IFECHAING AS FechaIngreso, 
            w.Code AS Cod_GA, 
            w.Name AS Grupo_Atencion, 
            F.NOMENTIDA AS Entidad, 
            g.UFUDESCRI AS Unidad, 
            p.NOMMEDICO AS Medico, 
            s.DESESPECI AS EspecialidadMedico, 
            a.FECHAORDE AS FechaOrden, 
            b.CODPRODUC AS CodProducto, 
            O.CodeAlternative AS CodigoCUM, 
            c.DESPRODUC AS Producto, 
            b.CANPEDPRO AS Cantidad, 
            O.Vr_Producto
     FROM dbo.HCFARMEPC AS a
          INNER JOIN dbo.ADINGRESO AS Z ON Z.NUMINGRES = a.NUMINGRES
                                           AND Z.IPCODPACI = a.IPCODPACI
          INNER JOIN dbo.HCFARMEPD AS b ON b.CODCONCEC = a.CODCONCEC
          INNER JOIN dbo.INUNIFUNC AS g ON g.UFUCODIGO = a.UFUCODIGO
          INNER JOIN dbo.IHLISTPRO AS c ON c.CODPRODUC = b.CODPRODUC
          LEFT OUTER JOIN dbo.INPACIENT AS e ON e.IPCODPACI = a.IPCODPACI
          INNER JOIN dbo.INPROFSAL AS p ON p.CODPROSAL = a.CODPROSAL
          INNER JOIN dbo.INESPECIA AS s ON s.CODESPECI = p.CODESPEC1
          INNER JOIN dbo.INENTIDAD AS F ON F.CODENTIDA = Z.CODENTIDA
          INNER JOIN dbo.INDIAGNOP AS D ON D.NUMINGRES = Z.NUMINGRES
                                           AND D.IPCODPACI = e.IPCODPACI
                                           AND D.CODDIAPRI = 'True'
          LEFT OUTER JOIN dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO
          INNER JOIN Contract.CareGroup AS w ON w.Id = Z.GENCAREGROUP
          LEFT OUTER JOIN
     (
         SELECT p.CodeAlternative, 
                p.CodeAlternativeTwo AS Alterno, 
                dT.SalesValue AS Vr_Producto, 
                T.Code AS ManualT, 
                T.Id AS ID
         FROM Inventory.ProductRate AS T
              INNER JOIN Inventory.ProductRateDetail AS dT ON T.Id = dT.ProductRateId
              INNER JOIN Inventory.InventoryProduct AS p ON p.Id = dT.ProductId
              INNER JOIN Inventory.ProductGroup AS G ON G.Id = p.ProductGroupId
              INNER JOIN Inventory.ProductSubGroup AS SG ON SG.Id = p.ProductSubGroupId
              LEFT OUTER JOIN Billing.BillingGroup AS gf ON gf.Id = p.BillingGroupId
         WHERE(dT.EndDate > '31/12/2015 23:59:59')
              AND (T.STATUS = '1')
     ) AS O ON O.Alterno = b.CODPRODUC
               AND O.ID = w.ProductRateId
     WHERE(a.CODCENATE = '001')
          AND (c.NOPOSPROD = 'true')
          AND (b.PROESTADO = '1')
          AND (c.TIPPRODUC = '1')
          AND (a.FECHAORDE >= '01/12/2015')
          AND (a.ORDESTADO NOT IN('3', '2'))
          AND (s.CODESPECI NOT IN('195', '190'))
     AND (Z.IESTADOIN NOT IN('F'));
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes de medicamentos NO POS (no incluidos en el Plan Obligatorio de Salud) despachadas o pendientes de despacho en el centro de atención principal. Integra información de ingresos o admisiones de pacientes (urgencias, hospitalización, consulta externa), el catálogo de productos farmacéuticos, el maestro de pacientes, profesionales de la salud con su especialidad, la entidad aseguradora o pagadora, la unidad funcional donde se generó la orden, el diagnóstico principal CIE-10 del ingreso y el grupo de atención contractual. Incluye además el precio o valor comercial del producto obtenido desde las tarifas del inventario (código CUM y valor de venta vigente). Sirve como herramienta de reporte y control de prescripciones de medicamentos No POS para gestión de glosas, facturación a terceros, auditoría clínica y seguimiento de costos de medicamentos no cubiertos por el plan de beneficios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vista No Pos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vista No Pos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de medicamentos/productos marcados como NO POS despachados por farmacia desde diciembre de 2015, con datos del paciente, ingreso, médico, entidad pagadora, diagnóstico principal y tarifa vigente del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener un diagnóstico principal (CODDIAPRI=''True'') registrado en INDIAGNOP; El producto debe estar catalogado como NO POS (NOPOSPROD=''true'') y ser de tipo ''1'' en IHLISTPRO; La orden farmacéutica debe pertenecer al centro de atención ''001''; El ingreso debe tener un grupo de atención (GENCAREGROUP) asociado en Contract.CareGroup; Para obtener tarifa, debe existir un ProductRate activo (STATUS=''1'') con detalle vigente (EndDate > 31/12/2015) ligado al CareGroup del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos clasificados como NO POS; Solo se incluyen pedidos en estado activo (PROESTADO=''1'') y órdenes no anuladas; Solo se consideran productos de tipo ''1'' (medicamentos); Solo se reporta actividad del centro de atención ''001'' desde el 01/12/2015; Cada fila corresponde al diagnóstico principal del ingreso; Se excluyen especialidades ''190'' y ''195'' y estados de ingreso ''F''; La tarifa del producto se determina por el grupo de atención (CareGroup) del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Diagnóstico principal (CIE); Orden médica de farmacia; Medicamento NO POS; Código CUM; Especialidad médica; Profesional de salud; Entidad pagadora (EPS/aseguradora); Unidad funcional; Grupo de atención (CareGroup); Tarifa de producto; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de pedidos farmacéuticos NO POS filtradas por CODCENATE=''001'', NOPOSPROD=''true'', PROESTADO=''1'', TIPPRODUC=''1'', FECHAORDE>=''01/12/2015'', ORDESTADO no en (''2'',''3''), especialidad no en (''190'',''195'') e IESTADOIN<>''F''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si a.ORDESTADO IN (''2'',''3'') → Se excluye la orden (estados anulados/cancelados no se reportan) else Se incluye en el resultado si cumple los demás filtros; si s.CODESPECI IN (''195'',''190'') → Se excluye la orden por especialidad médica no aplicable al reporte else Se incluye; si Z.IESTADOIN = ''F'' → Se excluye el ingreso (estado ''F'' marca ingresos no válidos para este reporte) else Se incluye; si D.CODDIAPRI = ''True'' → Se toma ese diagnóstico como diagnóstico principal del ingreso else No se considera; si Existe ProductRate con STATUS=''1'' y EndDate>''31/12/2015'' ligado al CareGroup y producto → Se asigna Vr_Producto y CodigoCUM desde la tarifa else Se devuelven NULL en valor del producto y CUM (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.ADINGRESO; dbo.HCFARMEPD; dbo.INUNIFUNC; dbo.IHLISTPRO; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.INDIAGNOS; Contract.CareGroup; Inventory.ProductRate; Inventory.ProductRateDetail; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Billing.BillingGroup; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Vista No Pos';
GO
