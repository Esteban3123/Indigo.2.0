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