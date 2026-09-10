-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-12-13
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail]
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @GeneralExpenseId INT,
			@DistributionType TINYINT,
			@GenerateAccountPayable BIT

	SELECT 
		@GeneralExpenseId = t.x.value('GeneralExpenseId[1]','int')
	FROM @XmlObject.nodes('/Data') t(x)

	SELECT @DistributionType = ge.DistributionType,
			@GenerateAccountPayable = ge.GenerateAccountPayable
	FROM Cost.CostGeneralExpense ge WITH (NOLOCK)
	WHERE ge.Id = @GeneralExpenseId

	IF @DistributionType = 1 BEGIN -- Distribución estandar
		EXEC [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution] @XmlObject
	END
	ELSE IF @DistributionType = 2 BEGIN --Distribución Mano de obra
		IF @GenerateAccountPayable = 0 BEGIN -- No Genera CxP
			EXEC [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable] @XmlObject
		END ELSE IF @GenerateAccountPayable IS NULL OR @GenerateAccountPayable = 1 -- Genera CxP
			EXEC [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution] @XmlObject
	END 
	ELSE IF @DistributionType = 3 BEGIN --Distribución gastos generales
		EXEC [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution] @XmlObject
	END 
	ELSE IF @DistributionType = 4 BEGIN --Distribución Productos
		EXEC [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution] @XmlObject
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la operación de copiar y pegar los detalles de distribución de costos directos para un gasto general específico. Recibe un XML con el identificador del gasto general, consulta el tipo de distribución y si genera cuenta por pagar, y según esos valores enruta la ejecución al subprocedimiento correspondiente: distribución estándar, mano de obra (con o sin cuenta por pagar), gastos generales o productos. Es el punto de entrada central para replicar configuraciones de distribución de elementos de costo entre períodos o centros de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despacha la operación de copiar/pegar el detalle de distribución de costos directos hacia el procedimiento especializado según el tipo de distribución del gasto general y si genera cuentas por pagar.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Cost.CostGeneralExpense con el Id recibido en el XML; El XML debe contener el nodo /Data/GeneralExpenseId', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El XML de entrada se reenvía sin modificación al procedimiento especializado seleccionado; Solo se ejecuta uno de los procedimientos especializados por invocación; Para distribución de mano de obra, un GenerateAccountPayable nulo se trata igual que verdadero (genera CxP); Si DistributionType no está en {1,2,3,4} no se ejecuta ningún procedimiento', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Gasto general; Mano de obra; Gastos generales; Distribución de productos; Cuentas por pagar (CxP); Copy & Paste de detalles de distribución', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution: Cuando DistributionType = 1 (Distribución estándar) se delega la ejecución al procedimiento de distribución estándar; [RETURN_RESULT] Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable: Cuando DistributionType = 2 (Mano de obra) y GenerateAccountPayable = 0 se delega al procedimiento que no genera cuentas por pagar; [RETURN_RESULT] Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution: Cuando DistributionType = 2 (Mano de obra) y GenerateAccountPayable IS NULL o = 1 se delega al procedimiento de distribución de mano de obra que genera CxP; [RETURN_RESULT] Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution: Cuando DistributionType = 3 (Gastos generales) se delega al procedimiento de distribución de gastos generales; [RETURN_RESULT] Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution: Cuando DistributionType = 4 (Productos) se delega al procedimiento de distribución de productos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DistributionType = 1 → Ejecuta el procedimiento de distribución estándar; si DistributionType = 2 AND GenerateAccountPayable = 0 → Ejecuta el procedimiento que no genera cuentas por pagar else Si DistributionType=2 y GenerateAccountPayable IS NULL o =1, ejecuta el procedimiento de distribución de mano de obra; si DistributionType = 3 → Ejecuta el procedimiento de distribución de gastos generales; si DistributionType = 4 → Ejecuta el procedimiento de distribución de productos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_StandardDistribution; Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable; Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution; Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution; Cost.SP_CopyAndPasteCostDistributionDirectCostDetail_ProductDistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostGeneralExpense', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail';
-- GO
