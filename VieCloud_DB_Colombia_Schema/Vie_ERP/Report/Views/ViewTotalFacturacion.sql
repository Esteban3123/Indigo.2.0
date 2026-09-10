
CREATE view [Report].[ViewTotalFacturacion] AS
 SELECT 
  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
  ((YEAR(CAST(JV.VoucherDate AS DATE))  * 100) + MONTH(CAST(JV.VoucherDate AS DATE))) [ID_TIEMPO_VENTAS],
  jv.EntityName, 
  IdJournalVoucher,
  SUM(JVD.CreditValue) Valor_Credito,
  CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 FROM 
  GeneralLedger.JournalVouchers JV 
  INNER JOIN GeneralLedger .JournalVoucherDetails AS JVD  ON JVD.IdAccounting =JV.Id
  INNER JOIN Billing.Invoice AS F  ON F.Id =JV.EntityId AND F.DocumentType <> 5
 WHERE 
  JV.LegalBookId = 1 AND jv.EntityName  IN ('Invoice', 'InvoiceEntityCapitated') 
 GROUP BY
  ((YEAR(CAST(JV.VoucherDate AS DATE))  * 100) + MONTH(CAST(JV.VoucherDate AS DATE))),
  jv.EntityName, 
  IdJournalVoucher
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consolidar la facturación total por período y comprobante contable. Cruza comprobantes de diario del libro legal (LegalBookId = 1) con sus detalles de crédito y las facturas de venta (excluyendo tipo de documento 5), agrupando por año-mes (YYYYMM), entidad contable e identificador de comprobante. Excluye facturas capturadas desde zona horaria estándar de Pakistán como marca de última actualización, limitándose a entidades de tipo `Invoice` e `InvoiceEntityCapitated`.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida mensualmente el total acreditado en comprobantes de diario asociados a facturas de venta, agrupado por período (año-mes), tipo de entidad y comprobante, para reportes de facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen comprobantes en GeneralLedger.JournalVouchers con LegalBookId = 1 (libro legal oficial); Los comprobantes deben estar asociados a entidades de tipo ''Invoice'' o ''InvoiceEntityCapitated''; La factura referenciada en Billing.Invoice debe existir y tener DocumentType distinto de 5', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran comprobantes del libro legal 1 (LegalBookId = 1); Se excluyen facturas cuyo DocumentType = 5 (tipo de documento excluido del reporte de facturación); Solo se incluyen entidades contables de tipo factura: ''Invoice'' o ''InvoiceEntityCapitated''; El período de tiempo (ID_TIEMPO_VENTAS) se construye como AAAAMM a partir de VoucherDate; ID_COMPANY corresponde al nombre de la base de datos truncado a 9 caracteres; ULT_ACTUAL refleja la fecha/hora actual convertida a zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación; Comprobante de diario; Libro legal contable; Factura de venta; Factura capitada (InvoiceEntityCapitated); Valor crédito contable; Período de ventas (año-mes)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewTotalFacturacion: Devuelve la suma de CreditValue por (año*100+mes de VoucherDate, EntityName, IdJournalVoucher) solo para LegalBookId=1, EntityName IN (''Invoice'',''InvoiceEntityCapitated'') y facturas con DocumentType<>5', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalFacturacion';
GO
