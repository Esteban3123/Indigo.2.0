-- =============================================
-- Author:    Juan Pablo Daza Medina
-- Create date: 28/12/2023
-- Update date: 06/05/2026
-- Modify by : Brandon Yulian Villanueva Serrano
-- Description:  Store Procedure que generara la vista para las primas del archivo plano
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
      LiquidationId INT,
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

    --Validacion de la informacion

    INSERT INTO @ReturnTable 
      SELECT
        I.Id Id,
        L.LiquidationId,
        P.Name Position,
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
      FROM Payroll.IncentivePayment I 
      INNER JOIN Payroll.[Contract] C ON I.ContractId = C.Id
      INNER JOIN Payroll.Employee E ON C.EmployeeId = E.Id 
      OUTER APPLY (
        SELECT TOP 1 L2.Id as LiquidationId
        FROM Payroll.Liquidation L2 WITH (NOLOCK)
        WHERE L2.RegisterStatus = 'C'
          AND L2.EmployeeId = E.Id
        ORDER BY 
          CASE WHEN L2.ContractId = C.Id THEN 0 ELSE 1 END,
          L2.Id DESC
      ) L
      INNER JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
      INNER JOIN Payroll.Position P ON P.Id = C.PositionId
      INNER JOIN Payroll.Bank B ON B.Id = C.BankId
      INNER JOIN Payroll.[Group] G on G.Id = C.GroupId
      INNER JOIN Payroll.FunctionalUnit F ON F.id = C.FunctionalUnitId
      WHERE I.Period = @Period 
        AND I.PeriodEndDate = @DateLiquidated 
        AND I.RegisterStatus = 2 
        AND I.PaymentType = 2 
        AND E.Id NOT IN (
          SELECT BFD.EmployeeId 
          FROM Payroll.BankFileDetail BFD 
          INNER JOIN Payroll.BankFile BF ON BF.Id = BFD.BankFileId 
          WHERE BF.Process = 1 AND BF.LiquidationDate = @DateLiquidated
        )
      SELECT * FROM @ReturnTable 
       RETURN

    END TRY
    BEGIN CATCH
      INSERT INTO @ReturnTable (MessageField)
        SELECT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
    END CATCH
    SELECT * FROM @ReturnTable 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado de pagos de incentivos y bonificaciones de nómina que aún no han sido confirmados ni incluidos en un archivo bancario, para un período y fecha de liquidación específicos. Consulta los pagos de incentivos pendientes (estado 2, tipo de pago 2) cruzándolos con el último registro de liquidación cerrada (''C'') de cada contrato, excluyendo los empleados cuyos incentivos ya fueron procesados en un archivo bancario previo para esa misma fecha. Integra información del empleado (NIT, nombre), cargo, banco, cuenta bancaria, grupo de nómina y unidad funcional para construir el archivo plano de transferencia bancaria de primas e incentivos. Se utiliza en el proceso de dispersión bancaria de pagos extraordinarios de nómina (primas, bonificaciones) antes de su confirmación formal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado previsualizado (sin confirmar) de los pagos de incentivos/primas de nómina elegibles para incluirse en el archivo plano bancario de un período y fecha de liquidación dados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Payroll.IncentivePayment con Period=@Period, PeriodEndDate=@DateLiquidated, RegisterStatus=2 y PaymentType=2.; Cada IncentivePayment debe tener un Contract relacionado con Employee, ThirdParty, Position, Bank, Group y FunctionalUnit válidos (joins INNER).; Debe existir al menos una Liquidation con RegisterStatus=''C'' (confirmada/cerrada) asociada al contrato; se toma la de mayor Id.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran incentivos con RegisterStatus = 2 y PaymentType = 2 (tipo pago vía banco/incentivo).; Solo se vincula la liquidación más reciente confirmada (RegisterStatus=''C'') del contrato.; Un empleado nunca aparece dos veces en el archivo plano para la misma fecha de liquidación si ya fue incluido en un BankFile con Process=1.; El procedimiento es de solo lectura: no modifica tablas físicas, únicamente retorna un resultset desde una tabla variable.; Cada fila exitosa se marca con BankFileStatus=0 y Process=1, indicando que aún no está confirmada en archivo bancario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago de incentivos/primas; Liquidación de nómina; Contrato laboral; Archivo plano bancario; Cuenta bancaria del empleado; Período de nómina; Unidad funcional; Grupo de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas con datos del incentivo, contrato, empleado, banco, grupo y unidad funcional, con BankFileStatus=0, Process=1 y MessageField=''Proceso Realizado Correctamente'' cuando la consulta es exitosa.; [RETURN_RESULT] RESULTSET: Si ocurre un error en el TRY, devuelve una fila con MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE() y los demás campos en NULL.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.Period = @Period AND I.PeriodEndDate = @DateLiquidated AND I.RegisterStatus = 2 AND I.PaymentType = 2 → Se incluye el pago de incentivo en el resultado. else Se excluye del resultado.; si E.Id NOT IN (SELECT BFD.EmployeeId FROM BankFileDetail BFD JOIN BankFile BF WHERE BF.Process = 1 AND BF.LiquidationDate = @DateLiquidated) → El empleado se incluye porque aún no fue procesado en un archivo bancario confirmado para esa fecha. else Se excluye al empleado para evitar duplicar el pago en el archivo bancario.; si L.Id = MAX(L2.Id) WHERE L2.ContractId = C.Id AND L2.RegisterStatus = ''C'' → Solo se asocia la última liquidación confirmada del contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Contract; Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.Position; Payroll.Bank; Payroll.Group; Payroll.FunctionalUnit; Payroll.BankFileDetail; Payroll.BankFile', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_BankFileIncentivePaymentWithoutConfirm';
-- GO
