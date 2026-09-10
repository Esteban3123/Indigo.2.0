CREATE VIEW [dbo].[No_Pos_Fla]
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
     WHERE(a.CODCENATE = '002')
          AND (c.NOPOSPROD = 'true')
          AND (b.PROESTADO = '1')
          AND (c.TIPPRODUC = '1')
          AND (a.ORDESTADO NOT IN('3', '2'))
          AND (Z.IESTADOIN = '')
          AND (s.CODESPECI NOT IN('195', '190'));
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos NO POS (no incluidos en el plan de beneficios) pendientes de despacho por farmacia, para pacientes actualmente hospitalizados en el centro de atención 002. Combina las órdenes médicas de farmacia (HCFARMEPC y HCFARMEPD) con datos del ingreso, el paciente, el diagnóstico principal (CIE-10), la entidad aseguradora, la unidad funcional, el médico tratante con su especialidad, y el producto farmacéutico con su código CUM y valor de tarifa vigente. Sirve como insumo para la gestión de autorizaciones, glosas y cobro de medicamentos no cubiertos por el plan obligatorio, excluyendo órdenes canceladas o despachadas y especialidades de tipo 195 y 190.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'No_Pos_Fla';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'No_Pos_Fla';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las órdenes farmacéuticas de medicamentos NO POS (no incluidos en el plan de beneficios) dispensadas en un centro de atención específico, con datos del paciente, ingreso, diagnóstico principal, médico tratante, entidad pagadora y tarifa del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener registrado un diagnóstico principal (CODDIAPRI=''True'') en INDIAGNOP; El producto debe estar marcado como NO POS (NOPOSPROD=''true'') y ser de tipo medicamento (TIPPRODUC=''1''); La orden farmacéutica debe pertenecer al centro de atención ''002''; El ingreso debe estar activo (IESTADOIN vacío); El profesional de salud debe tener una especialidad principal asignada distinta de ''195'' y ''190''; Para obtener tarifa, debe existir un ProductRate vigente (EndDate > 31/12/2015) con STATUS=''1'' asociado al CareGroup del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan medicamentos clasificados como NO POS y de tipo producto ''1''; Solo se consideran órdenes del centro de atención ''002''; Solo se incluyen pedidos con PROESTADO=''1'' (pedido activo/vigente); Solo ingresos con estado vacío (activos) son considerados; Cada fila corresponde al diagnóstico principal del ingreso (CODDIAPRI=''True''); La tarifa del producto se cruza por el CodeAlternativeTwo del producto contra el CODPRODUC del pedido y por el ProductRate asociado al CareGroup del ingreso; La edad del paciente se calcula al momento de ejecución de la consulta (GETDATE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos NO POS; Paciente; Ingreso/Admisión; Diagnóstico principal (CIE); Orden médica de farmacia; Prescripción de medicamentos; Profesional de salud / especialidad médica; Entidad pagadora (EPS/aseguradora); Unidad funcional; Grupo de atención (CareGroup); Tarifa de producto / CUM; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un conjunto de filas con la información de pedidos de medicamentos NO POS filtrados por centro de atención, estado del pedido y tipo de producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.NOPOSPROD = ''true'' AND c.TIPPRODUC = ''1'' → Incluye el pedido en el resultado por tratarse de medicamento NO POS; si a.ORDESTADO NOT IN (''3'',''2'') → Excluye órdenes en estados ''2'' y ''3'' (presumiblemente anuladas/canceladas); si s.CODESPECI NOT IN (''195'',''190'') → Excluye prescripciones realizadas por especialidades ''195'' y ''190''; si dT.EndDate > ''31/12/2015 23:59:59'' AND T.STATUS=''1'' → Toma la tarifa vigente del producto; en caso contrario el valor (Vr_Producto) queda nulo por LEFT JOIN; si D.CODDIAPRI=''True'' → Solo se asocia el diagnóstico principal del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.ADINGRESO; dbo.HCFARMEPD; dbo.INUNIFUNC; dbo.IHLISTPRO; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.INDIAGNOS; Contract.CareGroup; Inventory.ProductRate; Inventory.ProductRateDetail; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Billing.BillingGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'No_Pos_Fla';
GO
