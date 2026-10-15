-- =============================================
-- Author:		Andrea Pahola Coqueco
-- Create date: 2023-08-17
-- Description:	Procedimiento que se encarga de realizar la distribución del costo por cuadro de turnos
-- =============================================

CREATE PROCEDURE [Payroll].[SP_DistributionBySchedule]
(
	@CostDistributionsXML AS XML OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON

	/*===================== DECLARACIÓN DE VARIABLES ======================*/

	DECLARE @EmployeeId AS INT,
			@EmployeeRow AS INT

	DECLARE @CostDistributionsInput TABLE
	(
		[Id] [int] NOT NULL,	
		[LiquidationId] [int] NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		-----------------------------------------------
		[EmployeeId] INT,
		[NumberHours] INT DEFAULT(0),
		[Detail] VARCHAR(500),
		[BaseValue] DECIMAL(18,2) DEFAULT(0),
		[IdRetention] INT NULL
	)

	DECLARE @CostDistributionsOutput TABLE
	(
		[Id] [int] IDENTITY(1,1) NOT NULL,	
		[LiquidationId] [int] NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		-----------------------------------------------
		[EmployeeId] INT,
		[NumberHours] INT DEFAULT(0),
		[Detail] VARCHAR(500),
		[BaseValue] DECIMAL(18,2) DEFAULT(0),
		[IdRetention] INT NULL
	)

	/*===================== ASIGNACIÓN DE DATOS ======================*/

	INSERT INTO @CostDistributionsInput
		SELECT	t.x.value('Id[1]','int') AS Id,
				t.x.value('LiquidationId[1]','int') AS LiquidationId,
				t.x.value('LiquidationDetailId[1]','int') AS LiquidationDetailId,
				t.x.value('JournalVoucherTypeId[1]','int') AS JournalVoucherTypeId,
				t.x.value('MainAccountId[1]','int') AS MainAccountId,
				t.x.value('MainAccountNumber[1]','varchar(50)') AS MainAccountNumber,
				t.x.value('ThirdPartyId[1]','int') AS ThirdPartyId,
				t.x.value('CostCenterId[1]','int') AS CostCenterId,
				t.x.value('DebitValue[1]','decimal(18,0)') AS DebitValue,
				t.x.value('CreditValue[1]','decimal(18,0)') AS CreditValue,
				t.x.value('EmployeeId[1]','int') AS EmployeeId,
				t.x.value('NumberHours[1]','int') AS NumberHours,
				t.x.value('Detail[1]','varchar(500)') AS Detail,
				t.x.value('BaseValue[1]','decimal(18,0)') AS BaseValue,
				t.x.value('IdRetention[1]','int') AS IdRetention
		FROM @CostDistributionsXML.nodes('/CostDistributions') t(x)


	/*===================== PROCESAMIENTO POR EMPLEADO ======================*/

	SET @EmployeeRow = 1
	SET @EmployeeId = 0

	WHILE @EmployeeRow > 0
	BEGIN
		SELECT TOP 1
			@EmployeeId = cdi.EmployeeId
		FROM @CostDistributionsInput cdi
		WHERE cdi.EmployeeId > @EmployeeId
		ORDER BY cdi.EmployeeId

		SET @EmployeeRow = @@ROWCOUNT
		IF @EmployeeRow = 0 
		BEGIN
			BREAK
		END

		-------------------------------------------------------------------------

		SELECT @CostDistributionsXML = CONVERT(xml, 
			(
				SELECT * 
				FROM @CostDistributionsInput CostDistributions
				WHERE CostDistributions.EmployeeId = @EmployeeId
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Payroll].[SP_DistributionByScheduleByEmployee] @CostDistributionsXML OUTPUT

		INSERT INTO @CostDistributionsOutput
		(
			LiquidationId, LiquidationDetailId, 
			JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
			DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
		)
			SELECT	t.x.value('LiquidationId[1]','int') AS LiquidationId,
					t.x.value('LiquidationDetailId[1]','int') AS LiquidationDetailId,
					t.x.value('JournalVoucherTypeId[1]','int') AS JournalVoucherTypeId,
					t.x.value('MainAccountId[1]','int') AS MainAccountId,
					t.x.value('MainAccountNumber[1]','varchar(50)') AS MainAccountNumber,
					t.x.value('ThirdPartyId[1]','int') AS ThirdPartyId,
					t.x.value('CostCenterId[1]','int') AS CostCenterId,
					t.x.value('DebitValue[1]','decimal(18,0)') AS DebitValue,
					t.x.value('CreditValue[1]','decimal(18,0)') AS CreditValue,
					t.x.value('EmployeeId[1]','int') AS EmployeeId,
					t.x.value('NumberHours[1]','int') AS NumberHours,
					t.x.value('Detail[1]','varchar(500)') AS Detail,
					t.x.value('BaseValue[1]','decimal(18,0)') AS BaseValue,
					t.x.value('IdRetention[1]','int') AS IdRetention
			FROM @CostDistributionsXML.nodes('/CostDistributions') t(x)	
	END
		
	/*===================== RESULTADO ======================*/

	SELECT @CostDistributionsXML = CONVERT(xml, 
		(
			SELECT * FROM @CostDistributionsOutput CostDistributions
			For xml AUTO,TYPE, ELEMENTS
		)
	)
END