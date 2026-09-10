
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_CuentasXCobrar_megaplano]   
-- Add the parameters for the stored procedure here  
@CODE INT
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT 'HSP' AS Prefijo,

               /*F.InvoiceNumber as Nume_Fac,*/

               ContainerSP.dbo.fn_caracteres_especiales(F.InvoiceNumber) AS Numero_Fac, 
               'NI' AS Tipo_Doc_IPS, 
               CAST('891180134' AS INTEGER) AS Num_Cod_IPS, 
               U.IPSCode AS Cod_Hab_IPS,

        /*E.HealthEntityCode as Cod_Eps,*/
        /********CODIGO EPS CONTRI-SUB**************/

               CASE E.Code
                   WHEN 'EA0024'
                   THEN 'EPS003'
                   WHEN 'EA0025'
                   THEN 'EPSS03'
               END AS Cod_Eps,
               CASE
                   WHEN G.LiquidationType = 1
                   THEN '2'
                   WHEN G.LiquidationType = 2
                   THEN '1'
                   WHEN G.LiquidationType = 3
                   THEN '6'
                   WHEN G.LiquidationType = 4
                   THEN '1'
               END AS Cod_Cuenta, 
               '' AS Cod_Contrato, 
               F.InvoiceDate AS Fecha_Fact, 
               F.TotalInvoice AS ValorBruto,
               CASE
                   WHEN DF.RecoveryFeeType = 3
                   THEN F.TotalPatientWithDiscount
                   WHEN DF.RecoveryFeeType = 1
                   THEN F.TotalPatientWithDiscount
                   ELSE '0'
               END AS Copago, 
               '0' AS Valor_Copago_Compartido, 
               '0' AS Valor_Iva, 
               '0' AS Valor_Ico,
               CASE
                   WHEN DF.RecoveryFeeType = 2
                   THEN F.TotalPatientWithDiscount
                   ELSE '0'
               END AS Valor_Moderadora, 
               F.PatientDiscount AS Descuento, 
               '0' AS Con_Des, 
               F.ThirdPartySalesValue AS Valor_Neto, 
               '' AS Periodo, 
               '7' AS Cod_Regional,
               CASE
                   WHEN ing.ICAUSAING = '3'
                   THEN '1'
                   WHEN ing.ICAUSAING = '10'
                   THEN '2'
                   WHEN ing.ICAUSAING = '2'
                   THEN '3'
                   WHEN ing.ICAUSAING = '6'
                   THEN '4'
                   WHEN ing.ICAUSAING = '7'
                   THEN '5'
               END AS Clasificacion_Origen, 
               '' AS Tipo_Servicio, 
               '' AS Tipo_Paquete, 
               '' AS Fin_Consulta, 
               '' AS Dias_Trat, 
               dbo.TipoDocumento(p.IPTIPODOC) AS Tdoc_Paciente, 
               p.IPCODPACI AS Ndoc_Paciente, 
               p.IPPRINOMB AS Nombre, 
               p.IPSEGNOMB AS SegNombre, 
               p.IPPRIAPEL AS Apellido, 
               p.IPSEGAPEL AS SegApellido, 
               (CAST(DATEDIFF(dd, p.IPFECNACI, [Common].[GETDATE]()) / 365.25 AS INT)) AS Edad,
               CASE
                   WHEN p.IPSEXOPAC = '1'
                   THEN 'M'
                   WHEN p.IPSEXOPAC = '2'
                   THEN 'F'
               END AS Sexo,
               CASE
                   WHEN sal.ESTPACEGR = '3'
                   THEN '0'
                   WHEN sal.ESTPACEGR = '1'
                   THEN '1'
                   WHEN sal.ESTPACEGR = '2'
                   THEN '1'
                   WHEN sal.ESTPACEGR = '4'
                   THEN '1'
                   WHEN sal.ESTPACEGR IS NULL
                   THEN '1'
               END AS Estado_Paciente, 
               'N' AS Discapacidad,
               CASE
                   WHEN(CASE
                            WHEN pr.Code IS NULL
                            THEN cups.RIPSCode
                            WHEN cups.RIPSCode IS NULL
                            THEN pr.Code
                        END) LIKE 'D%'
                   THEN 'I'
                   WHEN ISNUMERIC(CASE
                                      WHEN pr.Code IS NULL
                                      THEN cups.RIPSCode
                                      WHEN cups.RIPSCode IS NULL
                                      THEN pr.Code
                                  END) <> 0
                   THEN 'P'
                   WHEN(CASE
                            WHEN pr.Code IS NULL
                            THEN cups.RIPSCode
                            WHEN cups.RIPSCode IS NULL
                            THEN pr.Code
                        END) LIKE 'S%'
                   THEN 'P'
                   ELSE 'M'
               END AS Tipo_Prestacion,
               CASE
                   WHEN pr.Code IS NULL
                   THEN cups.RIPSCode
                   WHEN cups.RIPSCode IS NULL
                   THEN(CASE
                            WHEN pr.Code LIKE 'M-%'
                            THEN pr.CodeAlternativeTwo
                            ELSE pr.Code
                        END)
               END AS 'Codigo_facturacion_principal',
               CASE
                   WHEN ContainerSP.dbo.Codigo_ProceQ(dq.Id) IS NULL
                   THEN '0'
                   ELSE ContainerSP.dbo.Codigo_ProceQ(dq.Id)
               END AS Cod_procedi_Detalle, 
               UPPER(CASE
                         WHEN ContainerSP.dbo.Detalle_ProceQ(dq.Id) IS NULL
                         THEN(CASE
                                  WHEN pr.Name IS NULL
                                  THEN ContainerSP.dbo.fn_caracteres_especiales2(CUPS.RIPSDescription)
                                  WHEN CUPS.RIPSDescription IS NULL
                                  THEN ContainerSP.dbo.fn_caracteres_especiales2(pr.Name)
                              END)
                         ELSE ContainerSP.dbo.Detalle_ProceQ(dq.Id)
                     END) AS Descripcion_procedi, 
               FORMAT(os.OrderDate, 'dd-MM-yyyy', 'en-US') AS FechaProcedi, 
               CAST(os.OrderDate AS TIME) AS HoraProcedi, 
               dos.InvoicedQuantity AS CantidadProcedi,
               CASE
                   WHEN dq.TotalSalesPrice IS NULL
                   THEN dos.TotalSalesPrice
                   ELSE dq.TotalSalesPrice
               END AS ValorUnitario, 
               '0' AS VALOR_COMPARTIDO_PACIENTE,
               CASE
                   WHEN DF.RecoveryFeeType = 2
                   THEN F.TotalPatientWithDiscount
                   ELSE '0'
               END AS VALOR_MODERADORA_PACIENTE,

/**************************/

               CASE
                   WHEN DF.RecoveryFeeType = 3
                   THEN(CASE
                            WHEN dq.TotalSalesPrice IS NULL
                            THEN(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor(F.InvoiceNumber) = DF.GrandTotalSalesPrice
                                     THEN F.TotalPatientWithDiscount
                                     ELSE '0'
                                 END)
                            ELSE(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor2(F.InvoiceNumber) = dq.TotalSalesPrice
                                     THEN F.TotalPatientWithDiscount
                                     ELSE '0'
                                 END)
                        END)

/*************************/

                   WHEN DF.RecoveryFeeType = 1
                   THEN(CASE
                            WHEN dq.TotalSalesPrice IS NULL
                            THEN(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor(F.InvoiceNumber) = DF.GrandTotalSalesPrice
                                     THEN F.TotalPatientWithDiscount
                                     ELSE '0'
                                 END)
                            ELSE(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor2(F.InvoiceNumber) = dq.TotalSalesPrice
                                     THEN F.TotalPatientWithDiscount
                                     ELSE '0'
                                 END)
                        END)
                   ELSE '0'
               END AS VALOR_COPAGO_PACIENTE,

/***********/

               CASE
                   WHEN DF.RecoveryFeeType = 3
                   THEN(CASE
                            WHEN dq.TotalSalesPrice IS NULL
                            THEN(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor(F.InvoiceNumber) = DF.GrandTotalSalesPrice
                                     THEN(DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE DF.GrandTotalSalesPrice
                                 END)
                            ELSE(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor2(F.InvoiceNumber) = dq.TotalSalesPrice
                                     THEN(dq.TotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE dq.TotalSalesPrice
                                 END)
                        END)

/***********BONO PACIENTE********************/

                   WHEN DF.RecoveryFeeType = 1
                   THEN(CASE
                            WHEN dq.TotalSalesPrice IS NULL
                            THEN(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor(F.InvoiceNumber) = DF.GrandTotalSalesPrice
                                     THEN(DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE DF.GrandTotalSalesPrice
                                 END)
                            ELSE(CASE
                                     WHEN ContainerSP.dbo.Valor_Mayor2(F.InvoiceNumber) = dq.TotalSalesPrice
                                     THEN(dq.TotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE dq.TotalSalesPrice
                                 END)
                        END)
                   ELSE(CASE
                            WHEN dq.TotalSalesPrice IS NULL
                            THEN(CASE
                                     WHEN DF.RecoveryFeeType = 2
                                     THEN(DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE DF.GrandTotalSalesPrice
                                 END)
                            ELSE(CASE
                                     WHEN DF.RecoveryFeeType = 2
                                     THEN(dq.TotalSalesPrice - F.TotalPatientWithDiscount)
                                     ELSE dq.TotalSalesPrice
                                 END)
                        END)
               END AS ValorTotalServicio,

/*************/

               dos.AuthorizationNumber AS CodAutorizacion, 
               ing.CODDIAEGR AS DiagnosticoPrincipal, 
               '' AS TIPO_DIAG, 
               '' AS DIAGNOSTICO_SECUNDARIO_1, 
               '' AS DIAGNOSTICO_SECUNDARIO_2, 
               FORMAT(ing.IFECHAING, 'dd-MM-yyyy', 'en-US') AS FECHA_ENTRADA, 
               CAST(ing.IFECHAING AS TIME) AS HORA_ENTRADA, 
               FORMAT(CASE
                          WHEN sal.FECALTPAC IS NULL
                          THEN F.OutputDate
                          ELSE sal.FECALTPAC
                      END, 'dd-MM-yyyy', 'en-US') AS FECHA_SALIDA, 
               CAST(CASE
                        WHEN sal.FECALTPAC IS NULL
                        THEN F.OutputDate
                        ELSE sal.FECALTPAC
                    END AS TIME) AS HORA_SALIDA
        FROM Billing.Invoice AS F WITH(NOLOCK)
             INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = F.RevenueControlDetailId
             INNER JOIN Billing.BillingAuthorization au ON au.Id = rcd.BillingAuthorizationId
             INNER JOIN Common.ThirdParty AS T ON F.ThirdPartyId = T.Id
             INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = F.Id
             INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
             INNER JOIN Billing.ServiceOrder AS os WITH(NOLOCK) ON os.Id = dos.ServiceOrderId
             LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = dos.Id
                                                                                      AND dq.OnlyMedicalFees = '0'
             LEFT OUTER JOIN [Contract].CUPSEntity AS cups ON dos.CUPSEntityId = cups.Id
             INNER JOIN Common.OperatingUnit AS U WITH(NOLOCK) ON F.operatingUnitid = U.id
             INNER JOIN Contract.HealthAdministrator AS E WITH(NOLOCK) ON F.HealthAdministratorId = E.Id
             INNER JOIN Contract.CareGroup AS G WITH(NOLOCK) ON F.caregroupid = G.id
             INNER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON ing.NUMINGRES = F.AdmissionNumber
             INNER JOIN dbo.INPACIENT AS p WITH(NOLOCK) ON p.IPCODPACI = F.PatientCode
             LEFT OUTER JOIN dbo.HCREGEGRE AS sal WITH(NOLOCK) ON sal.NUMINGRES = F.AdmissionNumber
             LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.Id = dos.ProductId
             LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
             LEFT OUTER JOIN Portfolio.RadicateInvoiceD AS Dr ON F.InvoiceNumber = Dr.InvoiceNumber
             LEFT OUTER JOIN Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId = Cr.Id
        WHERE(F.[Status] = '1')
             AND (Dr.RadicatedNumber = @CODE)
        ORDER BY dos.ServiceDate ASC;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el archivo plano de cuentas por cobrar en formato MEGAPLANO para la transmisión a administradoras de salud (EPS/ARS). Consolida información de facturas emitidas, detalle de servicios facturados, órdenes de servicio, datos del paciente, copagos, cuotas moderadoras, descuentos y valores netos a cobrar al tercero pagador, cruzando datos de las tablas de facturación (Invoice, InvoiceDetail, ServiceOrder, ServiceOrderDetail), catálogo CUPS, unidades operativas y terceros responsables del pago. Clasifica cada registro según tipo de prestación (consulta, procedimiento, imagen), causa de ingreso, régimen del paciente y código de la EPS, produciendo las columnas exigidas por el estándar de cuentas por cobrar para radicación y cobro ante las entidades contratantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CuentasXCobrar_megaplano';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el archivo plano (megaplano) de cuentas por cobrar para una radicación específica, consolidando datos de la factura, paciente, ingreso, servicios y autorizaciones según formato exigido por la EPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener Status = ''1'' (activa/vigente).; Debe existir un registro en Portfolio.RadicateInvoiceD cuyo RadicatedNumber coincida con el parámetro de entrada.; La factura debe estar enlazada a un control de ingresos (RevenueControlDetail) y a una autorización de facturación (BillingAuthorization).; El ingreso (ADINGRESO) y el paciente (INPACIENT) referenciados por la factura deben existir.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen facturas activas (Status=''1'').; Sólo se exporta lo asociado a una radicación específica (RadicatedNumber = @CODE).; El prefijo de la institución siempre es ''HSP'', el código IPS siempre 891180134 y la regional siempre ''7''.; El tipo de documento de la IPS siempre se reporta como ''NI'' (NIT).; Discapacidad siempre se reporta como ''N''.; Valor_Iva, Valor_Ico, Valor_Copago_Compartido y VALOR_COMPARTIDO_PACIENTE siempre son ''0'' (no se calculan).; El cobro al paciente (TotalPatientWithDiscount) sólo se asigna a UNA línea de detalle: aquella cuyo TotalSalesPrice coincide con el mayor valor de la factura (Valor_Mayor/Valor_Mayor2), evitando duplicar el cobro.; Sólo se consideran detalles quirúrgicos con OnlyMedicalFees=''0''.; Únicamente se reporta diagnóstico principal; los secundarios y tipo de diagnóstico siempre van vacíos.; La edad se calcula en años completos desde la fecha de nacimiento al momento de ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un conjunto de filas con el formato megaplano (Prefijo ''HSP'', código IPS fijo 891180134, regional ''7'') filtrado por Status=''1'' y RadicatedNumber=@CODE, ordenado por fecha de servicio ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HealthAdministrator.Code = ''EA0024'' → Cod_Eps = ''EPS003'' (régimen contributivo) else Si Code=''EA0025'' → ''EPSS03'' (régimen subsidiado); si CareGroup.LiquidationType in (1,2,3,4) → Mapea Cod_Cuenta a ''2'',''1'',''6'',''1'' respectivamente; si InvoiceDetail.RecoveryFeeType = 3 o 1 → El valor TotalPatientWithDiscount se reporta como Copago else Copago = ''0''; si InvoiceDetail.RecoveryFeeType = 2 → El valor TotalPatientWithDiscount se reporta como cuota Moderadora (Valor_Moderadora y VALOR_MODERADORA_PACIENTE) else Esos campos = ''0''; si ADINGRESO.ICAUSAING in (''3'',''10'',''2'',''6'',''7'') → Clasificacion_Origen mapea a ''1'',''2'',''3'',''4'',''5'' respectivamente (causa de ingreso); si INPACIENT.IPSEXOPAC = ''1'' o ''2'' → Sexo = ''M'' o ''F''; si HCREGEGRE.ESTPACEGR = ''3'' → Estado_Paciente = ''0'' (fallecido/egreso especial) else Otros valores o NULL → ''1'' (vivo); si Código de procedimiento (pr.Code o cups.RIPSCode) empieza con ''D'' → Tipo_Prestacion = ''I'' (insumo/dispositivo) else Si es numérico o empieza con ''S'' → ''P'' (procedimiento); en otro caso → ''M'' (medicamento); si pr.Code LIKE ''M-%'' → Usa pr.CodeAlternativeTwo como código de facturación principal else Usa pr.Code o cups.RIPSCode según cuál no sea NULL; si RecoveryFeeType in (1,3) y Valor_Mayor/Valor_Mayor2 coincide con el total mayor de la factura/quirúrgico → Asigna TotalPatientWithDiscount como VALOR_COPAGO_PACIENTE en esa línea y descuenta del ValorTotalServicio else VALOR_COPAGO_PACIENTE = ''0'' y ValorTotalServicio conserva el total sin descuento; si HCREGEGRE.FECALTPAC IS NULL → Usa Invoice.OutputDate como fecha/hora de salida else Usa FECALTPAC como fecha/hora de salida; si ServiceOrderDetailSurgical.TotalSalesPrice IS NULL → ValorUnitario = ServiceOrderDetail.TotalSalesPrice else ValorUnitario = dq.TotalSalesPrice (precio quirúrgico)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'ContainerSP.dbo.fn_caracteres_especiales; ContainerSP.dbo.fn_caracteres_especiales2; ContainerSP.dbo.Codigo_ProceQ; ContainerSP.dbo.Detalle_ProceQ; ContainerSP.dbo.Valor_Mayor; ContainerSP.dbo.Valor_Mayor2; dbo.TipoDocumento; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CuentasXCobrar_megaplano';
-- GO
