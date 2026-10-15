-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2017-07-26
-- Description:	Genera los detalles de comprobante de egreso para un grupo de atencion
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateJournalVoucherDetailsReverseRecognition] @CareGroupId AS     INT, 
                                                                               @OperativeUnitId AS INT
AS
    BEGIN
        SET NOCOUNT ON;

        --declare @reverse as bit = 0

        DECLARE @TableDetail AS TABLE
        (MainAccountId INT, 
         ThirdPartyId  INT, 
         CostCenterId  INT, 
         CreditValue   NUMERIC(18, 0), 
         DebitValue    NUMERIC(18, 0), 
         Detail        VARCHAR(300)
        );
        DECLARE @DiscountSalesMainAccountId INT;
        DECLARE @handlesThirdPartyMainAccountSetting BIT;
        DECLARE @handlesHandlesCostCenterMainAccountSetting BIT;
        DECLARE @AccountingForSurgical TINYINT;
        DECLARE @InvoiceJournalVoucherTypeId INT;
        DECLARE @RecoveryFeeDiscountMainAccountId INT;
        DECLARE @MainAccountRecoveryFee INT;
        DECLARE @RecoveryFeeCostCenterDiscountId INT;
        BEGIN TRY
            DECLARE @careGroupType INT, @contractAccountingStructureId INT, @careGroupCostCenter INT, @careGroupCostCenterId INT;
            SELECT @careGroupType = CareGroupType, 
                   @contractAccountingStructureId = ContractAccountingStructureId, 
                   @careGroupCostCenterId = CostCenterId
            FROM [Contract].CareGroup
            WHERE Id = @CareGroupId;
            DECLARE @tmpRevenueControl TABLE
            (Id              INT
             PRIMARY KEY, 
             AdmissionNumber CHAR(10), 
             PatientCode     CHAR(15)
            );
            DECLARE @tmpRevenueControlDetail TABLE
            (Id               INT
             PRIMARY KEY, 
             RevenueControlId INT, 
             CareGroupId      INT, 
             ThirdPartyId     INT, 
             TotalFolio       NUMERIC(18, 0), 
             ValueVoucher     NUMERIC(18, 0), 
             PatientDiscount  NUMERIC(18, 0)
            );

            --se trabaja con tablas temporales para evitar los interbloqueos
            --cargamos en una tabla temporal los folios filtrando por los parámetros necesarios
            INSERT INTO @tmpRevenueControlDetail
                   SELECT rcd.Id, 
                          rcd.RevenueControlId, 
                          rcd.CareGroupId, 
                          rcd.ThirdPartyId, 
                          rcd.TotalFolio, 
                          rcd.ValueVoucher, 
                          rcd.PatientDiscount
                   FROM Billing.RevenueControlDetail rcd
                   --inner join Billing.RevenueControl rc on rcd.RevenueControlId = rc.Id --temporal mientras se hace pruebas
                   --inner join dbo.ADINGRESO ing on rc.AdmissionNumber = ing.NUMINGRES --temporal mientras se hace pruebas
                   WHERE rcd.CareGroupId = @CareGroupId
                         AND rcd.[Status] = 5;

            --cargamos la cabecera del ingreso en tabla temporal
            INSERT INTO @tmpRevenueControl
                   SELECT rc.Id, 
                          rc.AdmissionNumber, 
                          rc.PatientCode
                   FROM Billing.RevenueControl rc
                   WHERE Id IN
                   (
                       SELECT DISTINCT 
                              RevenueControlId
                       FROM @tmpRevenueControlDetail
                   );

            --==DEBITO POR EL VALOR DEL SERVICIO/PRODUCTO SIN INCLUIR EL DESCUENTO (Valor cobrado a Entidad)
            INSERT INTO @TableDetail
                   SELECT dt.MainAccountId, 
                          dt.ThirdPartyId, 
                          dt.CostCenterId, 
                          SUM(dt.CreditValue), 
                          SUM(dt.DebitValue), 
                          dt.Descripcion
                   FROM
                   (
                       SELECT Detail.MainAccountId, 
                              Detail.ThirdPartyId, 
                              Detail.CostCenterId, 
                              0 AS CreditValue, 
                              ROUND((SUM(EntityValue) - (SUM(EntityValue) / (TotalFolio + PatientDiscount)) * IIF(@careGroupType = 3, PatientDiscount, 0)), 0) AS DebitValue, 
                              Detail.Descripcion
                       FROM
                       (
                           SELECT mar.Id AS MainAccountId,
                                  CASE mar.HandlesThirdParty
                                      WHEN 1
                                      THEN rcd.ThirdPartyId
                                      ELSE NULL
                                  END AS ThirdPartyId,
                                  CASE mar.HandlesCostCenter
                                      WHEN 1
                                      THEN sod.CostCenterId
                                      ELSE NULL
                                  END AS CostCenterId, 
                                  sodd.ThirdPartySalesPrice AS EntityValue, 
                                  '1 Generación de Reconocimiento de Ingreso módulo de facturación' AS Descripcion, 
                                  rcd.TotalFolio, 
                                  rcd.ValueVoucher, 
                                  rcd.PatientDiscount
                           FROM @tmpRevenueControlDetail rcd
                                JOIN [Contract].ContractAccountingStructure cas ON cas.Id = @contractAccountingStructureId
                                JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                JOIN GeneralLedger.MainAccounts mar ON mar.Id = cas.ServicesPendingBillingMainAccountId
                           WHERE sod.IsDelete = 0
                                 AND rcd.TotalFolio <> 0
                       ) AS Detail
                       GROUP BY Detail.MainAccountId, 
                                Detail.ThirdPartyId, 
                                Detail.CostCenterId, 
                                Detail.Descripcion, 
                                Detail.PatientDiscount, 
                                Detail.TotalFolio
                   ) AS dt
                   GROUP BY dt.MainAccountId, 
                            dt.ThirdPartyId, 
                            dt.CostCenterId, 
                            dt.Descripcion;
            SELECT @DiscountSalesMainAccountId = DiscountSalesMainAccountId, 
                   @handlesThirdPartyMainAccountSetting = ma.HandlesThirdParty, 
                   @handlesHandlesCostCenterMainAccountSetting = ma.HandlesCostCenter
            FROM Inventory.SettingInventory si
                 JOIN GeneralLedger.MainAccounts ma ON si.DiscountSalesMainAccountId = ma.Id
            WHERE si.OperatingUnitId = @OperativeUnitId;

            --==DESCUENTOS DEBITO - (Descuentos a la entidad)
            INSERT INTO @TableDetail
                   SELECT CASE sod.RecordType
                              WHEN 1
                              THEN bc.DiscountAccountId
                              ELSE @DiscountSalesMainAccountId
                          END AS MainAccountId,
                          CASE sod.RecordType
                              WHEN 1
                              THEN CASE mad.HandlesThirdParty
                                       WHEN 1
                                       THEN rcd.ThirdPartyId
                                       ELSE NULL
                                   END
                              ELSE CASE @handlesThirdPartyMainAccountSetting
                                       WHEN 1
                                       THEN rcd.ThirdPartyId
                                       ELSE NULL
                                   END
                          END AS ThirdParty,
                          CASE sod.RecordType
                              WHEN 1
                              THEN CASE mad.HandlesCostCenter
                                       WHEN 1
                                       THEN sod.CostCenterId
                                       ELSE NULL
                                   END
                              ELSE CASE @handlesHandlesCostCenterMainAccountSetting
                                       WHEN 1
                                       THEN sod.CostCenterId
                                       ELSE NULL
                                   END
                          END AS CostCenterId, 
                          0 AS CreditValue, 
                          SUM(sodd.GrandTotalDiscount) AS DebitValue, 
                          '2 Generación de Reconocimiento de Ingreso módulo de facturación'
                   FROM @tmpRevenueControlDetail rcd
                        JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                        JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                        JOIN Contract.CUPSEntity ce ON sod.CUPSEntityId = ce.Id
                        JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
                        LEFT JOIN GeneralLedger.MainAccounts mad ON mad.Id = bc.DiscountAccountId
                   WHERE sod.ThirdPartyDiscount > 0
                         AND sod.IsDelete = 0
                   GROUP BY sod.RecordType, 
                            bc.DiscountAccountId, 
                            mad.HandlesThirdParty, 
                            rcd.ThirdPartyId, 
                            mad.HandlesCostCenter, 
                            sod.CostCenterId;
            SELECT @AccountingForSurgical = AccountingForSurgical, 
                   @InvoiceJournalVoucherTypeId = InvoiceJournalVoucherTypeId, 
                   @RecoveryFeeDiscountMainAccountId = RecoveryFeeDiscountMainAccountId, 
                   @RecoveryFeeCostCenterDiscountId = RecoveryFeeDiscountCostCenterId
            FROM Billing.SettingsBilling
            WHERE IdOperatingUnit = @OperativeUnitId;
            SELECT @MainAccountRecoveryFee = cas.AccountRecoveryFeeId
            FROM [Contract].ContractAccountingStructure cas
            WHERE cas.Id = @contractAccountingStructureId;
            DECLARE @tmpThirdPartyPatient TABLE
            (ThirdPartyPatientId INT, 
             AdmissionNumber     CHAR(10)
            );--, SubTotalPatientSalesPrice numeric(18,0), TotalPatientDiscount numeric(18,0))
            DECLARE @tmpAccountvalues TABLE
            (ThirdPartyId           INT, 
             TotalPatientSalesPrice NUMERIC(18, 0), 
             TotalPatientDiscount   NUMERIC(18, 0)
            );

            --insertamos los id de los terceros de los pacientes en una tabla temporal
            INSERT INTO @tmpThirdPartyPatient
                   SELECT DISTINCT 
                          tp.Id, 
                          rc.AdmissionNumber
                   FROM @tmpRevenueControl rc
                        INNER JOIN Common.ThirdParty tp ON LTRIM(RTRIM(rc.PatientCode)) = tp.Nit;

            --update @tmpRevenueControlDetail set PatientDiscount = 1 --update para pruebas
            --==Si se ha aplicado cuota de recuperación va a la cuenta contable configurada en el grupo de atención del folio
            INSERT INTO @tmpAccountvalues
                   SELECT ttp.ThirdPartyPatientId, 
                          ISNULL(SUM(sodd.SubTotalPatientSalesPrice), 0) AS SubTotal, 
                   (
                       SELECT SUM(PatientDiscount)
                       FROM @tmpRevenueControlDetail
                       WHERE ThirdPartyId = rcd.ThirdPartyId
                   )
                   FROM Billing.ServiceOrderDetailDistribution sodd
                        INNER JOIN @tmpRevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                        INNER JOIN @tmpRevenueControl rc ON rcd.RevenueControlId = rc.Id
                        INNER JOIN @tmpThirdPartyPatient ttp ON rc.AdmissionNumber = ttp.AdmissionNumber
                   GROUP BY rcd.ThirdPartyId, 
                            ttp.ThirdPartyPatientId;

            --print cast(@TotalPatientSalesPrice as varchar)
            --======================================================================================================
            IF @MainAccountRecoveryFee IS NOT NULL
                BEGIN
                    --Si hay valor a paciente con descuento
                    --insert into @TableDetail 
                    --select @MainAccountRecoveryFee, tpp.ThirdPartyPatientId, @careGroupCostCenterId, 0 as CreditValue
                    --, sum(av.TotalPatientSalesPrice - av.TotalPatientDiscount) as DebitValue,
                    --'3 Generación de Reconocimiento de Ingreso módulo de facturación' as Detail
                    --from @tmpRevenueControlDetail rcd
                    --inner join @tmpRevenueControl rc on rcd.RevenueControlId = rc.Id
                    --inner join @tmpThirdPartyPatient tpp on rc.AdmissionNumber = tpp.AdmissionNumber
                    --inner join @tmpAccountvalues av on rcd.ThirdPartyId = av.ThirdPartyId			
                    --inner join GeneralLedger.MainAccounts mad on mad.Id = @MainAccountRecoveryFee
                    --where (av.TotalPatientSalesPrice - av.TotalPatientDiscount) > 0
                    --group by tpp.ThirdPartyPatientId, av.ThirdPartyId
                    --insert into @TableDetail
                    --select * from (
                    --	select @MainAccountRecoveryFee as MainAccountRecoveryFee
                    --	, tpp.ThirdPartyPatientId as ThirdPartyPatientId
                    --	, @careGroupCostCenterId as careGroupCostCenterId
                    --	, 0 as CreditValue
                    --	, (
                    --		select sum(TotalPatientSalesPrice - TotalPatientDiscount) from @tmpAccountvalues where ThirdPartyId = rcd.ThirdPartyId
                    --	) as DebitValue,
                    --	'3 Generación de Reconocimiento de Ingreso módulo de facturación' as Detail
                    --	from @tmpRevenueControlDetail rcd
                    --	inner join GeneralLedger.MainAccounts mad on mad.Id = @MainAccountRecoveryFee
                    --	inner join @tmpRevenueControl rc on rcd.RevenueControlId = rc.Id
                    --	inner join @tmpThirdPartyPatient tpp on rc.AdmissionNumber = tpp.AdmissionNumber
                    --	group by rcd.ThirdPartyId, tpp.ThirdPartyPatientId
                    --) as tp where tp.DebitValue > 0

                    INSERT INTO @TableDetail
                           SELECT DISTINCT 
                                  @MainAccountRecoveryFee, 
                                  tpp.ThirdPartyPatientId, 
                                  @careGroupCostCenterId, 
                                  0 AS CreditValue, 
                                  (av.TotalPatientSalesPrice - av.TotalPatientDiscount) AS DebitValue, 
                                  '3 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                           FROM @tmpRevenueControlDetail rcd
                                JOIN GeneralLedger.MainAccounts mad ON mad.Id = @MainAccountRecoveryFee
                                JOIN @tmpRevenueControl rc ON rcd.RevenueControlId = rc.Id
                                JOIN @tmpThirdPartyPatient tpp ON rc.AdmissionNumber = tpp.AdmissionNumber
                                JOIN @tmpAccountvalues av ON tpp.ThirdPartyPatientId = av.ThirdPartyId
                           WHERE(av.TotalPatientSalesPrice - av.TotalPatientDiscount) > 0;
                    --group by tpp.ThirdPartyPatientId
                    --select * from @tmpThirdPartyPatient
                    --select * from @tmpRevenueControlDetail
                    --select * from @tmpAccountvalues
                    --insert into @TableDetail 
                    --select @MainAccountRecoveryFee as MainAccountId,
                    --0,
                    --0,
                    --0 as CreditValue, 
                    --(
                    --	select sum(TotalPatientSalesPrice - TotalPatientDiscount) from @tmpAccountvalues where ThirdPartyId = rcd.ThirdPartyId
                    --) as DebitValue,
                    --'3 Generación de Reconocimiento de Ingreso módulo de facturación' as Detail
                    --from @tmpRevenueControlDetail rcd
                    --inner join GeneralLedger.MainAccounts mad on mad.Id = @MainAccountRecoveryFee
                    --group by rcd.ThirdPartyId
            END;

            --======================================================================================================
            --==Si se ha aplicado descuento a cuota de recuperación va a la cuenta contable configurada en los parámetros de facturación
            --Descuento al paciente
            INSERT INTO @TableDetail
                   SELECT @RecoveryFeeDiscountMainAccountId, 
                          tpp.ThirdPartyPatientId, 
                          @RecoveryFeeCostCenterDiscountId, 
                          0 AS CreditValue, 
                          SUM(av.TotalPatientDiscount) AS DebitValue, 
                          '4 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                   FROM @tmpRevenueControlDetail rcd
                        JOIN GeneralLedger.MainAccounts mad ON mad.Id = @RecoveryFeeDiscountMainAccountId
                        JOIN @tmpRevenueControl rc ON rcd.RevenueControlId = rc.Id
                        JOIN @tmpThirdPartyPatient tpp ON tpp.AdmissionNumber = rc.AdmissionNumber
                        JOIN @tmpAccountvalues av ON rcd.ThirdPartyId = av.ThirdPartyId
                   WHERE av.TotalPatientDiscount > 0
                   GROUP BY tpp.ThirdPartyPatientId;

            --========================================================================================================
            --==Cuando esta configurado como detallado la contabilizacion de los procedimientos qx
            IF @AccountingForSurgical = 2
                BEGIN
                    INSERT INTO @TableDetail
                           SELECT MainAccountId, 
                                  ThirdParty, 
                                  CostCenterId, 
                                  SUM(CreditValue), 
                                  DebitValue, 
                                  Detail
                           FROM
                           (
                               SELECT ma.Id AS MainAccountId, 
                                      rcd.ThirdPartyId AS ThirdParty, 
                                      sod.CostCenterId AS CostCenterId, 
                                      SUM(sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount) AS CreditValue, 
                                      0 AS DebitValue, 
                                      '5 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                               FROM @tmpRevenueControlDetail rcd
                                    JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                    JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                    JOIN Payroll.FunctionalUnit f ON f.Id = sod.PerformsFunctionalUnitId
                                    JOIN [Contract].CUPSEntity ce ON sod.CUPSEntityId = ce.Id
                                    JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
                                    JOIN GeneralLedger.MainAccounts ma ON ma.Id = [Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](sod.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, sod.IncomeMainAccountId, f.UnitType)
                               WHERE sod.Presentation <> 2
                                     AND sod.RecordType = 1
                                     AND sod.IsDelete = 0
                               GROUP BY sodd.DistributionType, 
                                        ma.Id, 
                                        rcd.ThirdPartyId, 
                                        sod.CostCenterId
                               UNION ALL
                               SELECT ma.Id AS MainAccountId, 
                                      rcd.ThirdPartyId AS ThirdParty, 
                                      sod.CostCenterId AS CostCenterId,
                                      CASE
                                          WHEN sodd.DistributionType = 2
                                          THEN SUM(sods.TotalSalesPrice * sod.InvoicedQuantity * (sodd.ThirdPartySalesPrice / sod.GrandTotalSalesPrice))
                                          ELSE SUM(sods.TotalSalesPrice * sod.InvoicedQuantity)
                                      END AS CreditValue,
                                      CASE
                                          WHEN sodd.DistributionType = 2
                                          THEN 0
                                          ELSE 0
                                      END AS DebitValue, 
                                      '6 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                               FROM @tmpRevenueControlDetail rcd
                                    JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                    JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                    JOIN Payroll.FunctionalUnit f ON f.Id = sod.PerformsFunctionalUnitId
                                    JOIN Billing.ServiceOrderDetailSurgical sods ON sods.ServiceOrderDetailId = sod.Id
                                    JOIN Billing.BillingConcept bc ON sods.BillingConceptId = bc.Id
                                    JOIN GeneralLedger.MainAccounts ma ON ma.Id = [Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](sod.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, sods.IncomeMainAccountId, f.UnitType)
                               WHERE sod.RecordType = 1
                                     AND sod.Presentation = 2
                                     AND sod.GrandTotalSalesPrice > 0
                                     AND sod.IsDelete = 0
                                     AND sods.OnlyMedicalFees = 0
                               GROUP BY sodd.DistributionType, 
                                        ma.Id, 
                                        ma.HandlesThirdParty, 
                                        ma.HandlesCostCenter, 
                                        sod.CostCenterId, 
                                        rcd.ThirdPartyId
                           ) AS Table1
                           GROUP BY Table1.ThirdParty, 
                                    Table1.CostCenterId, 
                                    Table1.DebitValue, 
                                    Table1.Detail, 
                                    Table1.MainAccountId
                           UNION ALL
                           SELECT ma.Id AS MainAccountId, 
                                  rcd.ThirdPartyId AS ThirdParty, 
                                  sod.CostCenterId AS CostCenterId, 
                                  SUM(sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount) AS CreditValue, 
                                  0 AS DebitValue, 
                                  '7 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                           FROM @tmpRevenueControlDetail rcd
                                JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                JOIN Inventory.InventoryProduct ip ON sod.ProductId = ip.Id
                                JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
                                JOIN GeneralLedger.MainAccounts ma ON ma.Id = sod.IncomeMainAccountId
                           WHERE sod.RecordType = 2
                                 AND sod.IsDelete = 0
                           GROUP BY sodd.DistributionType, 
                                    ma.Id, 
                                    ma.HandlesThirdParty, 
                                    rcd.ThirdPartyId, 
                                    ma.HandlesCostCenter, 
                                    sod.CostCenterId;
            END;
                ELSE
                BEGIN
                    --==Cuando esta configurado como por procedimiento
                    INSERT INTO @TableDetail
                           SELECT ma.Id, 
                                  rcd.ThirdPartyId AS ThirdParty, 
                                  sod.CostCenterId AS CostCenterId, 
                                  SUM(sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount) AS CreditValue, 
                                  0 AS DebitValue, 
                                  '8 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                           FROM @tmpRevenueControlDetail rcd
                                JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                JOIN Payroll.FunctionalUnit f ON f.Id = sod.PerformsFunctionalUnitId
                                JOIN Contract.CUPSEntity ce ON sod.CUPSEntityId = ce.Id
                                JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
                                JOIN GeneralLedger.MainAccounts ma ON ma.Id = [Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](sod.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, sod.IncomeMainAccountId, f.UnitType)
                           WHERE sod.IsDelete = 0
                           GROUP BY sodd.DistributionType, 
                                    ma.Id, 
                                    ma.HandlesThirdParty, 
                                    rcd.ThirdPartyId, 
                                    ma.HandlesCostCenter, 
                                    sod.CostCenterId
                           UNION ALL
                           SELECT ma.Id AS MainAccountId, 
                                  rcd.ThirdPartyId AS ThirdParty, 
                                  sod.CostCenterId AS CostCenterId, 
                                  SUM(sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount) AS CreditValue, 
                                  0 AS DebitValue, 
                                  '9 Generación de Reconocimiento de Ingreso módulo de facturación' AS Detail
                           FROM @tmpRevenueControlDetail rcd
                                JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
                                JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                                JOIN Inventory.InventoryProduct ip ON sod.ProductId = ip.Id
                                JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
                                JOIN GeneralLedger.MainAccounts ma ON ma.Id = sod.IncomeMainAccountId
                           WHERE sod.RecordType = 2
                                 AND sod.IsDelete = 0
                           GROUP BY sodd.DistributionType, 
                                    ma.Id, 
                                    ma.HandlesThirdParty, 
                                    rcd.ThirdPartyId, 
                                    ma.HandlesCostCenter, 
                                    sod.CostCenterId;
            END;
            DELETE FROM @TableDetail
            WHERE CreditValue = 0
                  AND DebitValue = 0;
            SELECT *
            FROM @TableDetail;
        END TRY
        BEGIN CATCH
            SELECT CONVERT(BIT, 0) AS StatusResult, 
                   'Se ha producido un error!' + ERROR_MESSAGE() AS MessageResult;
            SELECT *
            FROM @TableDetail;
            --select CONVERT(bit, 0) as StatusResult, 'Se ha producido un error!'+ ERROR_MESSAGE() as MessageResult ,NULL as CareGroupType, NULL as LiquidationType
        END CATCH;

        --select cast(null as int) as MainAccountId, cast(null as int) as ThirdPartyId, cast(null as int) as CostCenterId, cast(null as numeric(18,0)) as CreditValue, cast(null as numeric(18,0)) as DebitValue, '' as Detail
    END;