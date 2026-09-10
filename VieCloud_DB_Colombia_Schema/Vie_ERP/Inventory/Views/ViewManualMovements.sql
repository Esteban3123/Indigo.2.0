

CREATE VIEW [Inventory].[ViewManualMovements]
AS

select	pdd.Id,
		f.Name as FunctionalUnit,
		pa.IPNOMCOMP as Patient,
		pa.IPCODPACI as PatientCode,
		pd.DocumentDate,
		concat(pd.CreationUser, ' ', p.Fullname) as CreationUser,
		pdd.ProductId,
		pdd.Quantity,
		pd.AdmissionNumber,
		ip.Code CodeProduct,
		concat('Salida Dispensacion: ', pd.Code) MovementType
	from Inventory.PharmaceuticalDispensing pd WITH(NOLOCK)
	inner join Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) on pd.Id = pdd.PharmaceuticalDispensingId
	inner join Payroll.FunctionalUnit f WITH(NOLOCK) on f.Id = pdd.FunctionalUnitId
	inner join ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = pd.AdmissionNumber
	inner join INPACIENT pa WITH(NOLOCK) on pa.IPCODPACI = ad.IPCODPACI
	inner join Inventory.InventoryProduct ip WITH(NOLOCK) on pdd.ProductId= ip.Id
	left join [Security].[User] u on u.UserCode = pd.CreationUser
	left join [Security].Person p on p.Id = u.IdPerson
	where pd.EntityName = 'SavePharmaceuticalDispensing' OR pd.EntityName IS null
union all
select dd.Id,
		f.Name as FunctionalUnit,
		pa.IPNOMCOMP as Patient,
		pa.IPCODPACI as PatientCode,
		d.DocumentDate,
		concat(d.CreationUser, ' ', p.Fullname) as CreationUser,
		pdd.ProductId,
		dd.Quantity,
		d.AdmissionNumber,
		ip.Code CodeProduct,
		concat('Entrada Devolucion: ', d.Code) MovementType
from Inventory.PharmaceuticalDispensingDevolution d
inner join Inventory.PharmaceuticalDispensingDevolutionDetail dd on dd.PharmaceuticalDispensingDevolutionId = d.Id
inner join Inventory.PharmaceuticalDispensingDetailBatchSerial dbs on dbs.Id = dd.PharmaceuticalDispensingDetailBatchSerialId
inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = dbs.PharmaceuticalDispensingDetailId
inner join Payroll.FunctionalUnit f WITH(NOLOCK) on f.Id = pdd.FunctionalUnitId
inner join ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = d.AdmissionNumber
inner join INPACIENT pa WITH(NOLOCK) on pa.IPCODPACI = ad.IPCODPACI
inner join Inventory.InventoryProduct ip WITH(NOLOCK) on pdd.ProductId= ip.Id
left join [Security].[User] u on u.UserCode = d.CreationUser
	left join [Security].Person p on p.Id = u.IdPerson
where  d.EntityName IS NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos manuales de inventario farmacéutico: consolida en una sola consulta las salidas por dispensación de medicamentos a pacientes ingresados y las entradas por devolución de dispensaciones, permitiendo trazabilidad completa del movimiento de productos en inventario. Para cada registro expone la unidad funcional, el nombre y cédula del paciente, la fecha del documento, el usuario que generó el movimiento, el producto y cantidad, el número de ingreso hospitalario y el tipo de movimiento (salida por dispensación o entrada por devolución). Integra datos del catálogo de productos, el censo de admisiones, la información del paciente y la seguridad de usuarios, sirviendo como base para reportes de auditoría de inventario, conciliación de stock farmacéutico y control de despachos y retornos de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewManualMovements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewManualMovements';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los movimientos manuales de inventario originados por dispensaciones farmacéuticas (salidas) y por devoluciones de dispensación (entradas), enriquecidos con datos del paciente, unidad funcional, producto y usuario creador.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dispensaciones y devoluciones deben estar asociadas a un ingreso (AdmissionNumber) existente en ADINGRESO con paciente en INPACIENT.; Cada detalle de dispensación debe referenciar un producto vigente en Inventory.InventoryProduct y una unidad funcional en Payroll.FunctionalUnit.; Las devoluciones deben enlazar con un detalle de dispensación a través de PharmaceuticalDispensingDetailBatchSerial.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un movimiento manual: salida por dispensación o entrada por devolución, nunca ambos en la misma fila.; Solo se exponen movimientos asociados a un paciente con ingreso válido (INNER JOIN a ADINGRESO e INPACIENT).; El tipo de movimiento siempre se prefija con ''Salida Dispensacion:'' o ''Entrada Devolucion:'' seguido del código del documento.; El usuario creador se concatena con el nombre completo de la persona, aun cuando el usuario o persona no exista (LEFT JOIN).; Se excluyen dispensaciones cuyo EntityName no sea ''SavePharmaceuticalDispensing'' ni NULL, garantizando que solo se muestren movimientos manuales y no los provenientes de otros procesos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de dispensación; Movimiento manual de inventario; Paciente; Ingreso/Admisión hospitalaria; Unidad funcional; Producto de inventario; Salida de inventario; Entrada por devolución', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewManualMovements: Cuando pd.EntityName = ''SavePharmaceuticalDispensing'' o es NULL, se retorna la fila como movimiento tipo ''Salida Dispensacion: <Code>''.; [RETURN_RESULT] Inventory.ViewManualMovements: Cuando d.EntityName IS NULL en la devolución, se retorna la fila como movimiento tipo ''Entrada Devolucion: <Code>''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pd.EntityName = ''SavePharmaceuticalDispensing'' OR pd.EntityName IS NULL → Incluye la dispensación como movimiento de salida en la vista. else Excluye la dispensación (no aparece como salida).; si d.EntityName IS NULL → Incluye la devolución como movimiento de entrada en la vista. else Excluye la devolución (no aparece como entrada).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDevolution; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Payroll.FunctionalUnit; Inventory.InventoryProduct; Security.User; Security.Person; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewManualMovements';
GO
