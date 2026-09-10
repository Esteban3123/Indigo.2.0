CREATE  VIEW [Billing].[ViewAdmissionsToAccountControlAmbulatory]
AS

	

select DISTINCT ing.NUMINGRES AdmissionNumber, 
ing.IFECHAING AdmissionDate, 
pac.IPCODPACI PatientIdentification, 
pac.IPNOMCOMP PatientName, 
RTRIM(LTRIM(pac.IPCODPACI)) + ' - ' + RTRIM(LTRIM(pac.IPNOMCOMP)) PatientDescription,
fu.UFUCODIGO FunctionalUnitCode, 
fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(fu.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FuntionalUnitDescription,
cc.CODCENATE CareCenterCode,
cc.NOMCENATE CareCenterName,
RTRIM(LTRIM(cc.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterDescription
from .ADINGRESO ing with(nolock)
inner join .INPACIENT pac with(nolock) on pac.IPCODPACI = ing.IPCODPACI
inner join .INUNIFUNC fu with(nolock) on fu.UFUCODIGO = ing.UFUCODIGO
inner join .ADCENATEN cc with(nolock) on cc.CODCENATE = ing.CODCENATE
left join Billing.RevenueControl rc with(nolock) ON rc.AdmissionNumber = LTRIM(RTRIM(ing.NUMINGRES))
left join Billing.RevenueControlDetail rcd with(nolock) on rcd.RevenueControlId = rc.Id
left join (select sodd.RevenueControlDetailId, sod.* 
			from Billing.ServiceOrderDetailDistribution sodd with(nolock)
			inner join Billing.ServiceOrderDetail sod with(nolock) on sodd.ServiceOrderDetailId = sod.Id
			) sod1 on sod1.RevenueControlDetailId = rcd.Id
where (ing.IINGREPOR = 2 or ing.IINGREPOR = 3) and ing.IESTADOIN in (' ', 'P') and ing.CUPSPENDFAC = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos ambulatorios (consulta externa y urgencias sin hospitalización) que tienen servicios pendientes de facturar, integrando datos del paciente, la unidad funcional, el centro de atención y el control de ingresos de facturación. Combina las admisiones activas o en proceso (estado abierto o en trámite) que ingresaron por las modalidades ambulatorias y tienen CUPS pendientes de facturación, cruzando con los folios de control de cartera, el detalle de órdenes de servicio y su distribución financiera entre asegurador y paciente. Se usa como fuente de datos para el módulo de control de cuentas ambulatorio, permitiendo identificar qué ingresos de paciente (cédula, nombre) en qué sede y servicio aún tienen conceptos por liquidar o facturar a la EPS, al tercero pagador o al propio paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToAccountControlAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las admisiones ambulatorias/urgencias activas o pendientes con CUPS pendientes de facturar, enriquecidas con datos del paciente, unidad funcional y centro de atención, para control de cuentas de cobro.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe tener tipo de ingreso 2 o 3 (IINGREPOR in (2,3)); El estado del ingreso debe ser '' '' (activo) o ''P'' (pendiente) — IESTADOIN in ('' '',''P''); La admisión debe tener CUPS pendientes por facturar (CUPSPENDFAC = 1); Deben existir registros en INPACIENT, INUNIFUNC y ADCENATEN coincidentes con la admisión (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone admisiones cuyo tipo de ingreso es ambulatorio/urgencias (IINGREPOR 2 o 3), excluyendo hospitalización u otros tipos; Excluye admisiones cerradas o anuladas: solo estados '' '' o ''P''; Solo considera admisiones marcadas con CUPS pendientes por facturar (CUPSPENDFAC=1); El cruce con RevenueControl se hace por NUMINGRES con LTRIM/RTRIM, tolerando espacios en la clave de admisión; Aunque hace LEFT JOIN con RevenueControl/Detail y la distribución de órdenes de servicio, no expone columnas de esos joins (no afectan el resultado salvo por posible duplicación mitigada por DISTINCT)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión; Paciente; Unidad funcional; Centro de atención; Ingreso ambulatorio; Ingreso por urgencias; Control de ingresos (RevenueControl); Folio de facturación; Orden de servicio; CUPS pendientes por facturar', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewAdmissionsToAccountControlAmbulatory: Devuelve filas DISTINCT con admisión + paciente + unidad funcional + centro de atención solo cuando IINGREPOR ∈ (2,3), IESTADOIN ∈ ('' '',''P'') y CUPSPENDFAC = 1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADINGRESO; INPACIENT; INUNIFUNC; ADCENATEN; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToAccountControlAmbulatory';
GO
