
-- =============================================
-- Author:		Juan Pablo Daza Medina
-- Create date: 28/12/2023
-- Description:	Store Procedure que generara la vista para las primas del archivo plano
-- =============================================
CREATE PROCEDURE [Payroll].[SP_BankFileIncentivePaymentWithoutConfirm]
	@Period INT,
	@DateLiquidated DATETIME
AS 
BEGIN
	SET NOCOUNT ON;


		-- TABLA DE RETORNO 
		DECLARE @ReturnTable TABLE
		(
			Id INT,
			Position VARCHAR(255),
			PositionId INT,
			Nit VARCHAR(255),
			EmployeeName VARCHAR(255),
			Bank VARCHAR(255),
			BankAccount VARCHAR(255),
			PaidValue NUMERIC(18, 0),
			Status TINYINT,
			Valid TINYINT,
			ContractId INT,
			EmployeeBankTypeAccount VARCHAR(255),
			RegisterStatus TINYINT,
			[Period] TINYINT,
			PeriodEndDate DATETIME,
			GroupName VARCHAR(255),
			GroupId INT,
			FunctionalUnitName VARCHAR(255),
			FunctionalUnitId INT,
			TotalAccrued NUMERIC(18, 0),
			TotalDeducted NUMERIC(18, 0),
			BasicSalary NUMERIC(18, 0),
			EmployeeId INT,
			BankId INT,
			BankFileStatus INT,
			Process INT,
			MessageField NVARCHAR(MAX)
		)

		BEGIN TRY

		--Validación de la información

		
		INSERT INTO @ReturnTable 
				SELECT 
				I.Id Id,
				P.Name Position ,
				P.id PositionId,
				TP.NIT Nit,
				TP.NAME EmployeeName,
				B.Name Bank,
				C.BankAccountNumber BankAccount,
				I.PaidValue PaidValue,
				C.Status Status,
				C.Valid Valid,
				C.Id ContractId,
				C.BankAccountType EmployeeBankTypeAccount,
				I.RegisterStatus RegisterStatus, 
				I.Period [Period], 
				I.PeriodEndDate PeriodEndDate,	
				G.[Name] GroupName,
				G.id GroupId,
				F.[Name] FunctionalUnitName,
				F.id FunctionalUnitId,
				I.TotalAccrued,
				I.TotalDeducted,
				C.BasicSalary,
				E.id EmployeeId,
				B.id BankId,
				0 BankFileStatus,
				1 Process,
				'Proceso Realizado Correctamente' as MessageField
			from Payroll.IncentivePayment I 
			INNER JOIN  Payroll.[Contract] C ON I.ContractId = C.Id
			INNER JOIN Payroll.Employee E ON C.EmployeeId= E.Id 
			INNER JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
			INNER JOIN Payroll.Position P ON P.Id = C.PositionId
			INNER JOIN Payroll.Bank B ON B.Id = C.BankId
			INNER JOIN Payroll.[Group] G on G.Id = C.GroupId
			INNER JOIN Payroll.FunctionalUnit F ON F.id = C.FunctionalUnitId
			WHERE I.Period = @Period AND I.PeriodEndDate = @DateLiquidated AND I.RegisterStatus= 2 AND I.PaymentType = 2 
			AND E.Id NOT IN( SELECT BFD.EmployeeId FROM Payroll.BankFileDetail BFD 
							INNER JOIN Payroll.BankFile BF ON BF.Id = BFD.BankFileId WHERE BF.Process = 1 AND BF.LiquidationDate = @DateLiquidated)
			GROUP BY TP.NIT,  
			 I.Id,
			 P.Name,
			 P.id,
			 TP.NAME,
			 B.Name,
			 C.BankAccountNumber,
			 I.PaidValue,
			 C.Status,
			 C.Valid,
			 C.Id,
			 C.BankAccountType,
			 I.RegisterStatus, 
			 I.Period, 
			 I.PeriodEndDate,	
			 G.[Name],
			 G.id,
			 F.[Name],
			 F.id,
			 I.TotalAccrued,
			 I.TotalDeducted,
			 C.BasicSalary,
			 E.id,
			 B.id


			SELECT * FROM @ReturnTable 
			 RETURN


		END TRY
		BEGIN CATCH
			INSERT INTO @ReturnTable (MessageField)
				SELECT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
		END CATCH
		SELECT * FROM @ReturnTable 
		
END