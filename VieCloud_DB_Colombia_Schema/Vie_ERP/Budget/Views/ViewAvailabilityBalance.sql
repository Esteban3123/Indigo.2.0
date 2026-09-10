

CREATE VIEW [Budget].[ViewAvailabilityBalance]
AS

SELECT a.Id
      ,Code
      ,BudgetaryValidityId
      ,DependencyId
      ,DocumentDate
      ,ExpirationDays
      ,ExpirationDate
      ,AvailabilityType
      ,Observations
      ,Status
	  ,(select sum(balance) from Budget.AvailabilityDetail as ad where a.id	= ad.AvailabilityId) as BalanceAvaliability
  FROM Budget.Availability as a
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los certificados de disponibilidad presupuestal (CDP) junto con su saldo disponible actual. Combina los datos de cabecera de cada CDP (código, vigencia presupuestal, dependencia, fecha del documento, días y fecha de vencimiento, tipo, observaciones y estado) con el saldo total calculado a partir de la sumatoria de los detalles registrados en AvailabilityDetail. Sirve para consultar de forma rápida cuánto presupuesto le queda disponible a cada certificado de disponibilidad, apoyando la gestión y control presupuestal por dependencia y vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewAvailabilityBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewAvailabilityBalance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los certificados de disponibilidad presupuestal junto con el saldo total agregado (suma de balances) calculado desde su detalle por rubro.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'BalanceAvaliability se obtiene exclusivamente de Budget.AvailabilityDetail filtrando por la relación AvailabilityId = Availability.Id.; Se incluyen todas las disponibilidades aunque no tengan detalle (en cuyo caso BalanceAvaliability será NULL al no existir filas que sumar).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad presupuestal; Saldo de disponibilidad; Vigencia presupuestal; Dependencia; Detalle por rubro', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.Availability: Devuelve cada disponibilidad con un campo BalanceAvaliability calculado como SUM(balance) del detalle correlacionado por AvailabilityId.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Availability; Budget.AvailabilityDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewAvailabilityBalance';
GO
