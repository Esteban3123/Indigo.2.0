CREATE VIEW [dbo].[RH_V_INN_Medicamentos_CrystalVsVIE]
as
SELECT M.Code, 
       M.Name AS Medicamento, 
       DCI.Code AS CodDCI, 
       DCI.Name AS DCI, 
       ATC.Code AS CodATC, 
       ATC.Name AS ATC, 
       M.Presentations AS Presentacion, 
       '' AS Forma, 
       v.Name AS Via, 
       G.Name AS GrupoFarmaco, 
       M.Concentration,
       CASE M.FormulationType
           WHEN 1
           THEN 'Peso'
           WHEN 2
           THEN 'Volumnen'
           WHEN 3
           THEN 'Peso/Volumen'
           WHEN 4
           THEN 'Unidad'
       END AS TipoFormulacion, 
       Volumen = CONVERT(CHAR(12), M.Volume) + ' ' + CONVERT(CHAR(12), U.[Name]), 
       Peso = CONVERT(CHAR(12), M.[Weight]) + ' ' + CONVERT(CHAR(12), U1.[Name]), 
       U2.[Name] AS Unidad, 
       'Vie' AS Indigo
FROM Inventory.ATC M
     JOIN Inventory.AdministrationRoute V ON M.AdministrationRouteId = V.Id
                                                   AND M.STATUS = 1
     JOIN Inventory.PharmacologicalGroup G ON M.PharmacologicalGroupId = G.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U ON M.VolumeMeasureUnit = U.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U1 ON M.WeightMeasureUnit = U1.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U2 ON M.AdministrationUnitId = U2.Id
     JOIN Inventory.DCI ON M.DCIId = DCI.Id
     JOIN Inventory.ATCEntity ATC ON M.ATCEntityId = ATC.Id
UNION
SELECT P.CODPRODUC AS Code, 
       P.DESPRODUC AS MEDICAMENTO, 
       DCI.CODDCIMED AS CodDCI, 
       DCI.DESDCIMED AS DCI, 
       '' AS CodATC, 
       '' AS ATC, 
       P.PRESENMED AS Presentacion, 
       F.DESFORMED AS Forma, 
       v.DESVIAADM AS Via, 
       G.DESGRUFAR AS Grupo, 
       P.CONCENMED AS Concentration,
       CASE P.TIPFORMED
           WHEN 1
           THEN 'Peso'
           WHEN 2
           THEN 'Volumnen'
           WHEN 3
           THEN 'Peso/Volumen'
           WHEN 4
           THEN 'Unidad'
       END AS TipoFormulacion, 
       CONVERT(CHAR(12), P.VOLTOTMED) + '' + CONVERT(CHAR(12), U.DESUNIMED) AS Volumen, 
       CONVERT(CHAR(12), P.PESTOTMED) + '' + CONVERT(CHAR(12), U1.DESUNIMED) AS Peso, 
       U2.DESUNIMED AS Unidad, 
       'Crystal' AS Indigo
FROM .IHLISTPRO P
     JOIN .HCVIAADMI V ON P.CODVIAADM = V.CODVIAADM
                                    AND P.TIPPRODUC <> 2
                                    AND P.PROESTADO = 1
     JOIN .IHGRUFARM G ON P.CODGRUFAR = G.CODGRUFAR
     JOIN .IHFORMEDI F ON P.CODFORMED = F.CODFORMED
     LEFT JOIN .INUNIMEDI U ON P.CODUNIVOL = U.CODUNIMED
     LEFT JOIN .INUNIMEDI U1 ON P.CODUNIPES = U1.CODUNIMED
     LEFT JOIN .INUNIMEDI U2 ON P.CODUNIADM = U2.CODUNIMED
     JOIN .IHDCIMEDI DCI ON P.CODDCIMED = DCI.CODDCIMED;