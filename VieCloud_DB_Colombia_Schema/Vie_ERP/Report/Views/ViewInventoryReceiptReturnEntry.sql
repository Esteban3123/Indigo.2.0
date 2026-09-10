
/*******************************************************************************************************************
Nombre: [Report].[ViewInventoryReceiptReturnEntry]
Tipo:Vistas
Observacion:Vista sobre la devolucion vs comprobante de entrada de inventarios.
Profesional: Nilsson Miguel Galindo Lopez
Fecha:25-01-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico:
Fecha: 
Ovservaciones: 
--------------------------------------
Version 3
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewInventoryReceiptReturnEntry] AS

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
EVD.Code AS [CODIGO DE DEVOLUCION], 
EVD.DocumentDate AS [FECHA DEVOLUCION], 
PR.Name AS PROVEEDOR, 
EV.InvoiceNumber AS [# FACTURA], 
EVD.Description AS DESCRIPCION, 
EVD.FreightIVAPercentage AS [% IVA], 
EVD.FreightIVAValue AS [VALOR IVA], 
EVD.Value AS [SUB TOTAL], 
EVD.WithholdingICA AS ICA, 
EVD.RetentionSource AS [BASE RETENCION], 
EVD.TotalValue AS TOTAL, 
CASE EVD.status WHEN 1 THEN 'Registrado' 
				WHEN 2 THEN 'Confirmado' 
				WHEN 3 THEN 'Anulado' END AS ESTADO,
'1' AS CATIDAD,
CAST (EVD.DocumentDate as date) 'FECHA BUSQUEDA',
YEAR(EVD.DocumentDate) AS 'AÑO FECHA BUSQUEDA',
MONTH(EVD.DocumentDate) AS 'MES AÑO FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(EVD.DocumentDate), '00') ,' - ', 
	   CASE MONTH(EVD.DocumentDate) 
	        WHEN 1 THEN 'ENERO'
			WHEN 2 THEN 'FEBRERO'
			WHEN 3 THEN 'MARZO'
			WHEN 4 THEN 'ABRIL'
			WHEN 5 THEN 'MAYO'
			WHEN 6 THEN 'JUNIO'
			WHEN 7 THEN 'JULIO'
			WHEN 8 THEN 'AGOSTO'
			WHEN 9 THEN 'SEPTIEMBRE'
			WHEN 10 THEN 'OCTUBRE'
			WHEN 11 THEN 'NOVIEMBRE'
			WHEN 12 THEN 'DICIEMBRE'
		END) AS 'MES NOMBRE FECHA BUSQUEDA',
DAY(EVD.DocumentDate) AS 'DIA FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM     
Inventory.EntranceVoucherDevolution AS EVD INNER JOIN
Inventory.EntranceVoucher AS EV ON EV.Id = EVD.EntranceVoucherId INNER JOIN
Common.Supplier AS PR ON PR.Id = EV.SupplierId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida las devoluciones a proveedor vinculadas a comprobantes de entrada de inventario. Cruza cada devolución con su comprobante de entrada original y los datos del proveedor, exponiendo importes fiscales (IVA, ICA, retención en la fuente), estado del documento (Registrado, Confirmado, Anulado) y columnas de fecha desagregadas por año, mes y día para facilitar filtros y agrupaciones en herramientas de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para reportería las devoluciones de mercancía a proveedor cruzadas con su comprobante de entrada y proveedor, enriquecidas con desglose temporal (año/mes/día) y descripción legible del estado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación válida entre EntranceVoucherDevolution.EntranceVoucherId y EntranceVoucher.Id.; El comprobante de entrada debe tener un SupplierId que exista en Common.Supplier.; La zona horaria ''Pakistan Standard Time'' debe estar instalada en el servidor SQL para que ULT_ACTUAL se calcule correctamente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan devoluciones que tengan un comprobante de entrada asociado (INNER JOIN con EntranceVoucher) y un proveedor existente (INNER JOIN con Supplier).; El identificador de compañía (ID_COMPANY) se deriva siempre del nombre de la base de datos actual vía DB_NAME(), truncado a 9 caracteres.; La cantidad reportada por fila es siempre constante = ''1'' (cada fila representa una devolución).; La marca de última actualización (ULT_ACTUAL) se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución a proveedor; Comprobante de entrada de inventario; Proveedor; Factura; IVA; Retención ICA; Retención en la fuente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventoryReceiptReturnEntry: Devuelve una fila por cada devolución (EntranceVoucherDevolution) emparejada con su comprobante de entrada y proveedor; descompone la fecha de devolución en año, mes numérico, mes en texto en español y día, y traduce el código de estado (1=Registrado, 2=Confirmado, 3=Anulado).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EVD.status = 1 → Estado se reporta como ''Registrado''; si EVD.status = 2 → Estado se reporta como ''Confirmado''; si EVD.status = 3 → Estado se reporta como ''Anulado'' else Estado queda NULL si status no está en {1,2,3}', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucherDevolution; Inventory.EntranceVoucher; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventoryReceiptReturnEntry';
GO
