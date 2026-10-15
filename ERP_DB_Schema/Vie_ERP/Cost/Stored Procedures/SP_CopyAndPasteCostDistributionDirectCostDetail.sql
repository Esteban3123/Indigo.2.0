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