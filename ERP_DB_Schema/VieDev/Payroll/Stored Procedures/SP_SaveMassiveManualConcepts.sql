-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-10-01
-- Description:	Inserta los conceptos manuales masivos
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveMassiveManualConcepts] 
	@XMLObj XML 
AS
BEGIN
	BEGIN TRAN [Tran1]

		BEGIN TRY

			DECLARE @ManualConceptsDetail TABLE(
				ManualConceptId	int,
				PayrollDateLiquidated	date,
				[Value]	numeric(18,2),
				[State]	tinyint,
				QuoteNumber INT,
				PaidEndContract BIT
			)
			DECLARE @seqTable TABLE(
				NumberConsecutive INT
			)

			DECLARE @consecutive INT = 
				(SELECT NumberConsecutive 
				FROM Common.Consecutive 
				WHERE [Description] = 'CONCEPTOSMANUALES')


			;WITH cteXML AS(
				SELECT
					t.x.value('Process[1]','TINYINT') AS Process,
					t.x.value('Nit[1]','VARCHAR(20)') AS Nit,
					t.x.value('InternalCode[1]','VARCHAR(10)') AS InternalCode,
					t.x.value('Code[1]','VARCHAR(4)') AS Code,
					t.x.value('QuoteValue[1]','NUMERIC(18,2)') AS QuoteValue,
					t.x.value('PaidEndContract[1]','BIT') AS PaidEndContract,
					t.x.value('QuoteNumber[1]','TINYINT') AS QuoteNumber,
					t.x.value('PaidFormat[1]','TINYINT') AS PaidFormat,
					t.x.value('Description[1]','VARCHAR(200)') AS [Description]
				FROM @XMLObj.nodes('/Data/Row') t(x)
			), cte AS(
				SELECT 
					@consecutive + ROW_NUMBER() OVER(ORDER BY(SELECT 0)) AS Consecutive
					,cntrc.GroupId AS GroupId
					,IIF(employee.Id IS NULL, E.ID,employee.Id ) as EmployeeId
					,cntrc.InitialContractNumber AS ContractNumber
					,cntrc.Id AS ContractId
					,fUnit.CostCenterId AS CostCenterId
					,fUnit.BranchOfficeId AS BranchOfficeId
					,fUnit.Id AS FunctionalUnitId
					,grp.NextDateLiquidation AS InitialDate
					,grp.NextDateLiquidation AS PayrollInitialDate
					,EOMONTH(grp.NextDateLiquidation,0) AS PayrollEndingDate
					,concept.Id AS ConceptId
					,cteXML.[Description]
					,cteXML.PaidEndContract
					,cteXML.PaidFormat
					,cteXML.QuoteNumber
					,cteXML.QuoteValue
					,1 AS [State]
					,cteXML.Process
				FROM cteXML
				LEFT JOIN Common.ThirdParty tParty ON tParty.Nit = cteXML.Nit
				LEFT JOIN Payroll.Employee employee ON employee.ThirdPartyId = tParty.Id
				LEFT JOIN Payroll.Employee E ON cteXML.InternalCode = E.InternalCode
				CROSS APPLY(
					SELECT TOP 1
						cntrc.Id,
						cntrc.InitialContractNumber,
						cntrc.GroupId,
						cntrc.FunctionalUnitId
					FROM Payroll.[Contract] cntrc 
					WHERE( cntrc.EmployeeId = employee.Id OR  cntrc.EmployeeId = E.ID)
						AND cntrc.Valid = 1 
						AND cntrc.[Status] = 1
				) AS cntrc
				JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
				JOIN Payroll.[Group] grp ON grp.Id = cntrc.GroupId
				JOIN Payroll.Concept concept ON concept.Code = cteXML.Code
			)

			INSERT INTO Payroll.ManualConcepts (
				Consecutive
				,GroupId
				,EmployeeId
				,ContractNumber
				,ContractId
				,CostCenterId
				,BranchOfficeId
				,FunctionalUnitId
				,InitialDate
				,PayrollInitialDate
				,PayrollEndingDate
				,ConceptId
				,[Description]
				,PaidEndContract
				,PaidFormat
				,QuoteNumber
				,QuoteValue
				,[State]
				,Process
			)
			OUTPUT
				INSERTED.Id,
				INSERTED.PayrollInitialDate,
				INSERTED.QuoteValue,
				INSERTED.QuoteNumber,
				1,
				INSERTED.PaidEndContract
			INTO @ManualConceptsDetail(
				ManualConceptId,
				PayrollDateLiquidated,
				[Value],
				QuoteNumber,
				[State],
				PaidEndContract
			)
			SELECT
				Consecutive
				,GroupId
				,EmployeeId
				,ContractNumber
				,ContractId
				,CostCenterId
				,BranchOfficeId
				,FunctionalUnitId
				,InitialDate
				,PayrollInitialDate
				,PayrollEndingDate
				,ConceptId
				,[Description]
				,PaidEndContract
				,PaidFormat
				,QuoteNumber
				,QuoteValue
				,[State]
				,Process
			FROM cte;

			;WITH cteMCD AS(
				SELECT
					1 AS ID,
					ManualConceptId,
					PayrollDateLiquidated,
					[Value],
					[State],
					QuoteNumber,
					PaidEndContract
				FROM @ManualConceptsDetail
				UNION ALL
				SELECT
					ID+1,
					ManualConceptId,
					PayrollDateLiquidated,
					[Value],
					[State],
					QuoteNumber,
					PaidEndContract
				FROM cteMCD
				WHERE ID < QuoteNumber
					AND PaidEndContract = 0
			)

			INSERT INTO Payroll.ManualConceptsDetail(
				ManualConceptId,
				PayrollDateLiquidated,
				[Value],
				[State]
			)
			OUTPUT INSERTED.ManualConceptId INTO @seqTable
			SELECT
				ManualConceptId,
				PayrollDateLiquidated,
				[Value],
				[State]

			FROM cteMCD


			UPDATE Common.Consecutive 
			SET NumberConsecutive = (SELECT MAX(NumberConsecutive) FROM @seqTable)
			WHERE [Description] = 'CONCEPTOSMANUALES'

			COMMIT TRAN [Tran1]
		
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN [Tran1]
		END CATCH

END