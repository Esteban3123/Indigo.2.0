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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra masivamente conceptos manuales de nómina (devengados o deducciones) para múltiples empleados a partir de un archivo XML con los datos de cada concepto. Para cada fila del XML, identifica al empleado por NIT o código interno, localiza su contrato vigente y obtiene el grupo de liquidación, la unidad funcional y el centro de costo correspondientes, luego inserta los registros en la tabla de conceptos manuales (ManualConcepts) y genera el detalle de cuotas en ManualConceptsDetail según el número de cuotas pactadas. Al finalizar, actualiza el consecutivo global de ''CONCEPTOSMANUALES'' en Common.Consecutive para mantener la numeración secuencial correcta de los conceptos registrados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveManualConcepts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveManualConcepts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Inserta masivamente conceptos manuales de nómina (devengos/deducciones) y genera sus cuotas detalladas a partir de un XML, resolviendo empleado, contrato y unidad funcional vigentes, y avanzando el consecutivo correspondiente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Common.Consecutive con Description=''CONCEPTOSMANUALES'' del cual se toma el contador inicial.; Cada fila del XML debe poder resolver el empleado por Nit (Common.ThirdParty → Payroll.Employee) o por InternalCode.; Debe existir un Payroll.Contract vigente (Valid=1 y Status=1) asociado al empleado para obtener Group, FunctionalUnit y ContractNumber.; El Code del XML debe corresponder a un Payroll.Concept existente.; El XML debe respetar la estructura /Data/Row con los nodos esperados (Process, Nit, InternalCode, Code, QuoteValue, PaidEndContract, QuoteNumber, PaidFormat, Description).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las operaciones (INSERT en ManualConcepts, INSERT en ManualConceptsDetail y UPDATE de Consecutive) ocurren dentro de una única transacción Tran1 — son atómicas.; El consecutivo inicial se reserva tomando el valor actual de Common.Consecutive y sumándole ROW_NUMBER() por fila, garantizando numeración correlativa por lote.; Solo se considera el contrato vigente del empleado (Valid=1 y Status=1) — TOP 1 por CROSS APPLY.; PayrollEndingDate siempre es el último día del mes de NextDateLiquidation del grupo (EOMONTH).; Los conceptos se insertan con State=1 (activo) por defecto.; Si PaidEndContract=1 nunca se generan múltiples cuotas independientemente de QuoteNumber.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'conceptos manuales de nómina; consecutivo de documentos; contrato laboral vigente; unidad funcional; grupo de nómina; fecha de liquidación de nómina; cuotas/quotes de un concepto; pago al fin de contrato (PaidEndContract); centro de costo y sucursal; empleado identificado por NIT o código interno', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.ManualConcepts: Por cada fila del XML se inserta un concepto manual con Consecutive = consecutivo base + ROW_NUMBER(), tomando InitialDate y PayrollInitialDate de Group.NextDateLiquidation y PayrollEndingDate como EOMONTH(NextDateLiquidation), con State=1.; [INSERT] Payroll.ManualConceptsDetail: Por cada ManualConcept insertado se generan N filas de detalle: si PaidEndContract=0 se crean tantas como QuoteNumber (CTE recursivo ID+1 hasta QuoteNumber); si PaidEndContract=1 se crea una sola cuota.; [UPDATE] Common.Consecutive: Al final, NumberConsecutive de la fila Description=''CONCEPTOSMANUALES'' se actualiza al MAX(NumberConsecutive) capturado en @seqTable de los detalles insertados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si employee.Id IS NULL (no se encontró empleado por Nit) → Se usa el EmployeeId resuelto por InternalCode (E.ID) como empleado destino. else Se usa el EmployeeId resuelto vía ThirdParty.Nit.; si PaidEndContract = 0 en el concepto manual insertado → Se expanden tantas filas de ManualConceptsDetail como QuoteNumber mediante el CTE recursivo. else Solo se inserta una única fila de detalle (no se expanden cuotas) cuando PaidEndContract=1.; si Error en cualquier paso del bloque TRY → Se ejecuta ROLLBACK TRAN [Tran1] revirtiendo todos los cambios; no se relanza la excepción. else COMMIT TRAN [Tran1] confirma inserciones y actualización del consecutivo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Consecutive; Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.FunctionalUnit; Payroll.Group; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveManualConcepts';
-- GO
