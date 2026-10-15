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