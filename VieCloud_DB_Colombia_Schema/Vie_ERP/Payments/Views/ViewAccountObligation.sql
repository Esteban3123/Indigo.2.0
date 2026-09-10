CREATE VIEW [Payments].[ViewAccountObligation]
AS
	SELECT DISTINCT
		CAST(od.EntityId AS VARCHAR(20)) Id, od.EntityId AccountPayableId, o.Id ObligationId, o.Code ObligationCode
	FROM Budget.Obligation o
	JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
	WHERE od.EntityName = 'AccountPayable'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona las cuentas por pagar con sus obligaciones presupuestarias correspondientes. Cruza las obligaciones registradas en el presupuesto con el detalle de cada obligación, filtrando únicamente los ítems de tipo ''Cuenta por Pagar'' (AccountPayable). Para cada cuenta por pagar devuelve su identificador, el identificador de la obligación presupuestal asociada y el código de dicha obligación. Sirve como puente entre el módulo de pagos y el módulo presupuestal, permitiendo consultar qué compromiso formal de gasto respalda cada cuenta por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountObligation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountObligation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación entre cuentas por pagar y las obligaciones presupuestarias que las respaldan, filtrando los detalles cuyo tipo de entidad es ''AccountPayable''.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en el detalle de obligación cuya entidad referenciada corresponde a ''AccountPayable''.; Cada detalle de obligación referencia una obligación válida mediante su identificador.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de obligación cuyo tipo de entidad asociada es ''AccountPayable''.; El identificador de la cuenta por pagar se expone como cadena de hasta 20 caracteres.; Se eliminan duplicados en la relación entre cuenta por pagar y obligación (DISTINCT).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Obligación presupuestaria; Cuenta por pagar; Detalle de obligación', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.Obligation: Cuando ObligationDetail.EntityName = ''AccountPayable'', se retorna el vínculo entre la cuenta por pagar (EntityId) y su obligación (Id, Code).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Obligation; Budget.ObligationDetail', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountObligation';
GO
