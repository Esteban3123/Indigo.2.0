CREATE VIEW [Contract].[ViewListDefinitionRateDetail]
AS
select	drd.Id, drd.DefinitionRateId, drd.RuleType,
		case drd.RuleType 
			when 1 then CONCAT('0', drd.RuleType, ' - ', 'Servicio IPS')
			when 2 then CONCAT('0', drd.RuleType, ' - ', 'CUPS')
			when 3 then CONCAT('0', drd.RuleType, ' - ', 'SubGrupo CUPS')
			when 4 then CONCAT('0', drd.RuleType, ' - ', 'Grupo CUPS')
			when 5 then CONCAT('0', drd.RuleType, ' - ', 'General')
		end RuleTypeName,
		case drd.RuleType 
			when 1 then IIF(ce.Id is null, CONCAT(ips.Code, ' - ', ips.Name), CONCAT(ips.Code, ' - ', ce.Code, ' - ', ips.Name))
			when 2 then CONCAT(ce.Code, ' - ', ce.Description)
			when 3 then CONCAT(csg.Code, ' - ', csg.Name)
			when 4 then CONCAT(cg.Code, ' - ', cg.Name)
			when 5 then 'General'
		end RuleDescription,
		ips.Id IPSServiceId, ce.Id CUPSEntityId, csg.Id CUPSSubgroupId, cg.Id CUPSGroupId,
		drd.ConditionType, drd.LogicalOperator, drd.ConditionType2,
		Contract.fnGetConditionTypeName(drd.ConditionType) ConditionTypeName, Contract.fnGetConditionTypeName(drd.ConditionType2) ConditionTypeName2,
		case drd.LogicalOperator
			when 1 then Contract.fnGetConditionTypeName(drd.ConditionType)
			else Contract.fnGetConditionTypeName(drd.ConditionType) + IIF(drd.LogicalOperator = 2, ' Y ', ' O ') + Contract.fnGetConditionTypeName(drd.ConditionType2)
		end ConditionName,
		drd.Weight, drd.AllowValueChange, drd.LiquidationType,
		rm.Id RateManualId, CONCAT(rm.Code, ' - ', rm.Name) RateManualDescription,
		rmv.Id RateManualValidityId, CONCAT(rmv.Code, ' - ', rmv.Name) RateManualValidityDescription,
		drd.ManualType, drd.SalesValue, drd.SalesValueWithSurcharge, drd.RateVariation
from Contract.DefinitionRateDetail drd
left join Contract.IPSService ips on ips.Id = drd.IPSServiceId
left join Contract.CUPSEntity ce on ce.Id = drd.CUPSEntityId
left join Contract.CupsSubgroup csg on csg.Id = drd.CUPSSubgroupId
left join Contract.CupsGroup cg on cg.Id = drd.CUPSGroupId
left join Contract.RateManual rm on rm.Id = drd.RateManualId
left join Contract.RateManualValidity rmv on rmv.Id = drd.RateManualValidityId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista detallada de las reglas tarifarias definidas en los contratos, combinando información de múltiples niveles de aplicación: servicio IPS propio, código CUPS individual, subgrupo CUPS, grupo CUPS o regla general. Para cada regla muestra una descripción legible del tipo (RuleTypeName), la descripción del elemento tarifado (RuleDescription), las condiciones de aplicación con sus nombres decodificados, el tipo de liquidación, el manual tarifario con su vigencia, el valor de venta, el valor con recargo y la variación de tarifa. Se usa en la configuración y consulta de contratos con EPS, aseguradoras o pagadores para visualizar cómo se componen las tarifas negociadas por servicio, procedimiento o grupo CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDefinitionRateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDefinitionRateDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de reglas tarifarias de contratos enriquecido con descripciones legibles del tipo de regla, condición lógica y referencias a servicios IPS, CUPS y manuales tarifarios.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código numérico del RuleType siempre se prefija con ''0'' en la etiqueta visible (formato ''0X - Nombre'').; La descripción de la regla depende exclusivamente del RuleType, ignorando los demás IDs no aplicables.; Para reglas tipo Servicio IPS, la presencia de CUPSEntity altera el formato de la descripción agregando su código.; El operador lógico 2 se interpreta como conjunción (Y) y cualquier otro valor distinto de 1 como disyunción (O).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa de contrato; Servicio IPS; CUPS; Subgrupo CUPS; Grupo CUPS; Manual tarifario; Vigencia de manual tarifario; Tipo de condición; Operador lógico; Tipo de liquidación; Variación tarifaria; Valor de venta; Recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.DefinitionRateDetail: Devuelve cada detalle de tarifa con joins LEFT a IPSService, CUPSEntity, CupsSubgroup, CupsGroup, RateManual y RateManualValidity para mostrar descripciones aunque la referencia sea nula.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RuleType = 1 → Etiqueta la regla como ''Servicio IPS'' y describe con código/nombre de IPSService; si existe CUPSEntity asociada, incluye también su código en la descripción.; si RuleType = 2 → Etiqueta como ''CUPS'' y describe con código y descripción de CUPSEntity.; si RuleType = 3 → Etiqueta como ''SubGrupo CUPS'' y describe con código y nombre de CupsSubgroup.; si RuleType = 4 → Etiqueta como ''Grupo CUPS'' y describe con código y nombre de CupsGroup.; si RuleType = 5 → Etiqueta y describe la regla como ''General''.; si LogicalOperator = 1 → El nombre de la condición usa solo ConditionType (condición simple). else Concatena ConditionType y ConditionType2 unidos por '' Y '' si LogicalOperator = 2 o por '' O '' en otro caso.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.fnGetConditionTypeName', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.DefinitionRateDetail; Contract.IPSService; Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Contract.RateManual; Contract.RateManualValidity', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetail';
GO
