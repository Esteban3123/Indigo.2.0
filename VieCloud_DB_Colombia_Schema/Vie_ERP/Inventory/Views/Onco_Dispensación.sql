
CREATE VIEW [Inventory].[Onco_Dispensación]
AS
     SELECT CAST(K.DocumentDate AS DATE) AS Fecha, 
            K.EntityCode AS Documento, 
            al.Code Almac, 
            al.Name Nombre_Almacen,
            CASE
                WHEN MovementType = '1'
                THEN 'Dispensación'
                ELSE 'Devolución'
            END TipoMov,
            CASE
                WHEN pd.ProductTypeId = '1'
                THEN 'Medicamento'
                WHEN pd.ProductTypeId = '2'
                THEN 'Insumo'
                ELSE 'Otro'
            END Tipo_Actividad, 
            pd.Code Código, 
            PD.Name NombreProducto, 
            pd.CodeCUM CUM, 
            PUC.Number, 
            PUC.Name NomCuenta, 
            K.EntityCode, 
            K.EntityName,
            CASE
                WHEN K.EntityName = 'PharmaceuticalDispensing'
                THEN 'Ingeso Nro ' + dp.AdmissionNumber
                WHEN K.EntityName = 'PharmaceuticalDispensingDevolution'
                THEN 'Ingeso Nro ' + DD.AdmissionNumber
            END AS Descripción,
            CASE
                WHEN MovementType = '1'
                THEN k.Quantity
            END CantEnt,
            CASE
                WHEN MovementType = '2'
                THEN k.Quantity
            END CantSal,
            CASE
                WHEN k.Value > 0
                THEN k.Value
                WHEN k.Value <= 0
                THEN K.AverageCost
            END CtProm, 
            k.AffectInventory AS Ct
     FROM inventory.Kardex AS K
          INNER JOIN Inventory.InventoryProduct PD WITH(NOLOCK) ON Pd.Id = k.ProductId
          LEFT JOIN Inventory.Warehouse AL WITH(NOLOCK) ON al.Id = k.WarehouseId  
          --left join VIE08.Inventory.EntranceVoucher CE WITH (nolock) ON CE.Id = k.EntityId AND K.EntityName = 'EntranceVoucher'  
          --left join VIE08.Common.Supplier P WITH (nolock) ON P.Id = CE.SupplierId  
          LEFT JOIN Inventory.PharmaceuticalDispensing DP WITH(NOLOCK) ON DP.Id = k.EntityId
                                                                          AND K.EntityName = 'PharmaceuticalDispensing'
          LEFT JOIN Inventory.PharmaceuticalDispensingDevolution DD WITH(NOLOCK) ON DD.Id = k.EntityId
                                                                                    AND K.EntityName = 'PharmaceuticalDispensingDevolution'

          --left join VIE08.Inventory.TransferOrder TR WITH (nolock) ON TR.Id = k.EntityId AND K.EntityName = 'TransferOrder' AND TR.TargetWarehouseId = AL.Id  
          --LEFT JOIN VIE08.Inventory.InventoryAdjustment AJ WITH (nolock) ON AJ.Id = k.EntityId AND K.EntityName = 'InventoryAdjustment'  
          --LEFT JOIN VIE08.Inventory.TransferOrderDevolution DTR WITH (nolock) ON DTR.Id = k.EntityId AND DTR.TransferOrderId = TR.Id AND K.EntityName = 'TransferOrderDevolution'  
          LEFT JOIN Inventory.ProductGroup GP WITH(NOLOCK) ON GP.ID = PD.ProductGroupId
          LEFT JOIN Payments.AccountPayableConcepts CCXP WITH(NOLOCK) ON GP.InventoryAccountPayableConceptId = CCXP.Id
          LEFT JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON PUC.ID = CCXP.IdAccount
          LEFT JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON ing.NUMINGRES = dp.AdmissionNumber
                                                         OR ing.NUMINGRES = dd.AdmissionNumber
          LEFT JOIN dbo.INPACIENT AS pac WITH(NOLOCK) ON ing.IPCODPACI = pac.ID     
     ---LEFT JOIN Common.OperatingUnit  as up WITH (nolock) ON up.Id = ing.u      

     WHERE K.EntityName IN('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution')
          AND K.id = '23787';--- CreationDate  BETWEEN '2020-08-31 22:56:00.880' and  '2020-09-02 22:56:00.880' ---and k.EntityCode='0000001261'---pd.Code='20096371-25' and   
---k.EntityCode='0000001261'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de movimientos de dispensación oncológica que consolida las salidas y devoluciones de medicamentos e insumos registradas en el kardex de inventario, filtrando únicamente los documentos de tipo dispensación farmacéutica y devolución de dispensación. Integra información del producto (nombre, código CUM, tipo: medicamento o insumo), el almacén o bodega de origen, la cuenta contable PUC asociada al grupo de producto, y el número de ingreso hospitalario del paciente al que se le despachó o devolvió el medicamento. Para cada movimiento muestra la fecha, tipo de movimiento (dispensación o devolución), cantidades de entrada y salida, costo promedio, y la descripción con el número de ingreso del paciente. Esta vista está orientada a reportería y auditoría de consumos farmacéuticos en el servicio de oncología, permitiendo trazabilidad contable e inventarial de los medicamentos oncológicos dispensados o devueltos por paciente ingresado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Dispensación';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Dispensación';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los movimientos de Kardex de dispensación y devolución farmacéutica oncológica, enriqueciéndolos con datos del producto, almacén, cuenta contable y paciente/ingreso asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los movimientos del Kardex deben tener EntityName en (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution''); El producto referenciado por ProductId debe existir en InventoryProduct; Para enlazar la cuenta contable, el grupo del producto debe tener configurado InventoryAccountPayableConceptId con su IdAccount en MainAccounts; El número de ingreso (AdmissionNumber) debe existir en dbo.ADINGRESO para vincular paciente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos de dispensación farmacéutica y sus devoluciones; Cada movimiento se clasifica unívocamente como Dispensación o Devolución según MovementType; El costo unitario reportado nunca es cero o negativo: si k.Value no es positivo se sustituye por el costo promedio; La descripción del documento siempre referencia el número de ingreso del paciente cuando existe el vínculo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de dispensación; Kardex de inventario; Medicamento; Insumo; CUM (Código Único de Medicamento); Almacén/Bodega; Costo promedio; Cuenta contable (PUC); Concepto de cuentas por pagar; Ingreso de paciente (admisión); Paciente; Oncología', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna registros donde K.EntityName IN (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'') AND K.id=''23787'' (filtro fijo del view)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MovementType = ''1'' → Se etiqueta como ''Dispensación'' y la cantidad se asigna a CantEnt else Se etiqueta como ''Devolución'' y la cantidad se asigna a CantSal (cuando MovementType=''2''); si pd.ProductTypeId = ''1'' → Tipo_Actividad = ''Medicamento'' else Si ProductTypeId=''2'' → ''Insumo''; en otro caso → ''Otro''; si K.EntityName = ''PharmaceuticalDispensing'' → Descripción usa el AdmissionNumber de PharmaceuticalDispensing (''Ingeso Nro '' + dp.AdmissionNumber) else Si EntityName=''PharmaceuticalDispensingDevolution'' usa el AdmissionNumber de la devolución; si k.Value > 0 → CtProm = k.Value (costo del movimiento) else Si k.Value <= 0 entonces CtProm = K.AverageCost (costo promedio)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolution; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Dispensación';
GO
