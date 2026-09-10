

CREATE VIEW [Inventory].[ViewPharmaceuticalDispensing]
AS

SELECT	
	CAST(NEWID() AS VARCHAR(40)) 'id',
	pd.Type,
	pd.IdPharmaceuticalDispensing,
	pd.Codigo,
	pd.UnidadOperativa,
	pd.Ingreso,
	pd.NombreCompletoPaciente,
	pd.FechaDispensacion,
	pd.AfectaInventario,
	pd.Estado,
	pd.UsuarioCreado,
	pd.FechaCreacion,
	pd.ProductId,
	pd.IdProducto,
	pd.Producto,
	pd.IdAlmacen,
	pd.Almacen,
	pd.Cantidad,
	pd.UnidadFuncional
FROM 
(
	SELECT
	1 Type,
	ipd.Id AS 'IdPharmaceuticalDispensing',
	ipd.Code AS 'Codigo',
	cou.UnitName AS 'UnidadOperativa',
	ipd.AdmissionNumber AS 'Ingreso',
	p.IPNOMCOMP AS 'NombreCompletoPaciente',
	ipd.DocumentDate AS 'FechaDispensacion', 
	case when ipd.AffectInventory = 1 then 'Si'when ipd.AffectInventory = 0 then 'No' end AS 'AfectaInventario',
	case when ipd.Status = 1 then 'registrado' when ipd.Status = 2 then 'confirmado' else 'anulado' end AS 'Estado',
	ipd.CreationUser AS 'UsuarioCreado',
	ipd.CreationDate AS 'FechaCreacion',
	iip.Id AS 'ProductId',
	iip.Code AS 'IdProducto',
	iip.Name AS 'Producto',
	iw.Id AS 'IdAlmacen',
	iw.Name AS 'Almacen',
	ipdd.Quantity AS 'Cantidad',
	pfu.Name AS 'UnidadFuncional'
	FROM Inventory.PharmaceuticalDispensing AS ipd
	INNER JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
	INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
	INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
	INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
	INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
	LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
	INNER JOIN dbo.INPACIENT p on p.IPCODPACI = ad.IPCODPACI

UNION ALL 

	SELECT DISTINCT
	2 Type,
	ipd.Id AS 'IdPharmaceuticalDispensing',
	ipd.Code AS 'Codigo',
	cou.UnitName AS 'UnidadOperativa',
	ipd.AdmissionNumber AS 'Ingreso',
	p.IPNOMCOMP AS 'NombreCompletoPaciente',
	ipd.DocumentDate AS 'FechaDispensacion', 
	case when ipd.AffectInventory = 1 then 'Si'when ipd.AffectInventory = 0 then 'No' end AS 'AfectaInventario',
	case when ipd.Status = 1 then 'registrado' when ipd.Status = 2 then 'confirmado' else 'anulado' end AS 'Estado',
	ipd.CreationUser AS 'UsuarioCreado',
	ipd.CreationDate AS 'FechaCreacion',
	0 AS 'ProductId',
	NULL AS 'IdProducto',
	NULL AS 'Producto',
	iw.Id AS 'IdAlmacen',
	iw.Name AS 'Almacen',
	0 AS 'Cantidad',
	pfu.Name AS 'UnidadFuncional'
	FROM Inventory.PharmaceuticalDispensing AS ipd
	INNER JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
	INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
	INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
	INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
	INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
	LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
	INNER JOIN dbo.INPACIENT p on p.IPCODPACI = ad.IPCODPACI
) pd
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las dispensaciones farmacéuticas realizadas a pacientes ingresados, combinando el encabezado del documento de despacho con el detalle de cada medicamento entregado. Integra información de la sede o unidad operativa donde se realizó el despacho, el número de ingreso del paciente, su nombre completo, la bodega de origen, la unidad funcional responsable y el estado del documento (registrado, confirmado o anulado). Presenta dos tipos de filas por dispensación: una con el detalle por producto (medicamento, cantidad, almacén) y otra con el resumen del encabezado sin producto específico, útil para reportería y consulta del ciclo de vida de los despachos de medicamentos. Sirve para rastrear qué medicamentos fueron dispensados, a qué paciente hospitalizado, en qué sede, desde qué bodega y en qué fecha.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para listar dispensaciones farmacéuticas con datos de paciente, unidad operativa, almacén, producto y unidad funcional, combinando dos perspectivas: detalle por ítem y resumen por documento.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada dispensación debe tener al menos un detalle (PharmaceuticalDispensingDetail) para aparecer en la vista (INNER JOIN).; El detalle debe referenciar un producto, almacén y unidad funcional existentes.; El AdmissionNumber de la dispensación debe corresponder a un ingreso (ADINGRESO) cuyo paciente exista en INPACIENT; si no, la fila se excluye por el INNER JOIN con INPACIENT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila recibe un identificador ''id'' único generado con NEWID() en cada ejecución (no persistente).; El estado se normaliza siempre a uno de tres valores: ''registrado'', ''confirmado'' o ''anulado''.; AfectaInventario solo puede ser ''Si'' o ''No'' (o NULL si AffectInventory no es 0 ni 1).; Solo se incluyen dispensaciones cuyo ingreso tenga paciente asociado en INPACIENT.; El conjunto Type=2 nunca expone información de producto ni cantidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Paciente; Ingreso/Admisión; Unidad operativa; Almacén/Bodega; Producto/Medicamento; Unidad funcional; Afectación de inventario; Estado de documento (registrado/confirmado/anulado)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la unión de dos conjuntos: Type=1 con detalle de producto y cantidad; Type=2 con producto y cantidad nulos/cero (resumen por almacén/unidad funcional).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ipd.AffectInventory = 1 → Se muestra ''Si'' en AfectaInventario else Si AffectInventory = 0 se muestra ''No''; si ipd.Status = 1 → Estado = ''registrado'' else Si Status = 2 → ''confirmado''; cualquier otro valor → ''anulado''; si Type = 1 (primer SELECT) → Se exponen ProductId, IdProducto, Producto y Cantidad reales del detalle else Type = 2: ProductId=0, IdProducto/Producto NULL y Cantidad=0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Common.OperatingUnit; Inventory.InventoryProduct; Inventory.Warehouse; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensing';
GO
