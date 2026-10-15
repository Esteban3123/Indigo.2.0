-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-12-14
-- Description:	Procedimiento que se encarga de guardar, actualizar el archivo plano de banco
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveBankFile] 
    @EntityXml AS XML,
	@ListDetailDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@OperatingUnitId INT,
			@CompanyId INT,
			@LiquidationDate DATE,
			@EntityBankAccountId INT,
			@ExpenseConceptId INT,
			@Value DECIMAL(18,0),
			@Status TINYINT,
			@Process INT,
			@PeriodIncentivePayment INT,
			@YearIncentivePayment INT,
			----------------------------
			@errors VARCHAR(MAX)

	--Tabla temporal de BankFileDetail
	DECLARE @BankFileDetail TABLE
	(
		[Id] [int],
		[BankFileId] [int],
		[LiquidationId] [int],
		[GroupId] [int],
		[PositionId] [int],
		[FunctionalUnitId] [int],
		[ContractId] [int],
		[EmployeeId] [int],
		[EmployeeBankId] [int],
		[EmployeeBankTypeAccount] [int],
		[EmployeeBankAccountNumber] [varchar](100),
		[BasicSalary] [decimal](18, 0),
		[TotalAccrued] [decimal](18, 0),
		[TotalDeducted] [decimal](18, 0),
		[TotalPaid] [decimal](18, 0)
	)

	--Tabla temporal para obtener el listado de ids eliminados
	DECLARE @ListBankFileDetailDelete TABLE(Id INT)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CompanyId = t.x.value('CompanyId[1]','int'),
			@LiquidationDate = t.x.value('LiquidationDate[1]','date'),
			@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
			@ExpenseConceptId = t.x.value('ExpenseConceptId[1]','int'),
			@Value = t.x.value('Value[1]','decimal(18,2)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@Process =t.x.value('Process[1]','int'),--se agregan estos 3 ultimos para prima
			@PeriodIncentivePayment =t.x.value('PeriodIncentivePayment[1]','int'),
			@YearIncentivePayment =t.x.value('YearIncentivePayment[1]','int')
		FROM @EntityXml.nodes('/BankFile') t(x)

		IF EXISTS (SELECT 1 FROM Payroll.BankFile WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(bb.Status = 2, 'Confirmado', 'Anulado') as Message, '' as Code, 0 as Id
			FROM Payroll.BankFile bb
			WHERE bb.Id = @Id
			RETURN
		END

		--Si no se esta anulando
		IF @Status <> 3
		BEGIN
			--Se obtiene BankFileDetail
			INSERT INTO @BankFileDetail
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('BankFileId[1]','int') as BankFileId,
					t.x.value('LiquidationId[1]','int') as LiquidationId,
					t.x.value('GroupId[1]','int') as GroupId,
					t.x.value('PositionId[1]','int') as PositionId,
					t.x.value('FunctionalUnitId[1]','int') as FunctionalUnitId,					
					t.x.value('ContractId[1]','int') as ContractId,
					t.x.value('EmployeeId[1]','int') as EmployeeId,
					t.x.value('EmployeeBankId[1]','int') as EmployeeBankId,
					t.x.value('EmployeeBankTypeAccount[1]','int') as EmployeeBankTypeAccount,
					t.x.value('EmployeeBankAccountNumber[1]','varchar(100)') as EmployeeBankAccountNumber,
					t.x.value('BasicSalary[1]','decimal(18,2)') as BasicSalary,
					t.x.value('TotalAccrued[1]','decimal(18,2)') as TotalAccrued,
					t.x.value('TotalDeducted[1]','decimal(18,2)') as TotalDeducted,
					t.x.value('TotalPaid[1]','decimal(18,2)') as TotalPaid
				FROM @EntityXml.nodes('/BankFile/BankFileDetail') t(x)

			--Se obtiene los detalles a eliminar
			INSERT INTO @ListBankFileDetailDelete
				SELECT 
					t.x.value('Id[1]','int') as Id
				FROM @ListDetailDeleteXml.nodes('/BankFileDetailDelete') t(x)

			--Eliminamos los detalles
			DELETE bfd
			FROM Payroll.BankFileDetail bfd
			JOIN @ListBankFileDetailDelete ld ON bfd.Id = ld.Id

			/*************************************VALIDACIONES************************************/

			IF EXISTS
			(
				SELECT 1
				FROM
				(
					SELECT SUM(bfd.TotalPaid) Value
					FROM
					(
						SELECT bfd.TotalPaid
						FROM @BankFileDetail bfd
						UNION ALL
						SELECT bfd.TotalPaid
						FROM Payroll.BankFileDetail bfd
						LEFT JOIN @BankFileDetail bfdt ON bfd.Id = bfdt.Id
						WHERE bfd.BankFileId = @Id AND bfdt.Id IS NULL
					) bfd
				) bbd
				WHERE bbd.Value <> @Value
			)
			BEGIN
				SELECT 999 as CodeMessage, 'El total pagado de los empleado no corresponden con el total de la cabecera' as Message, '' as Code, 0 as Id
				RETURN
			END

			IF EXISTS
(
			SELECT 1
			FROM
				(
					-- todos los pares (BankFileId, LiquidationId) que quedan
					SELECT bfd.BankFileId, bfd.LiquidationId
					FROM @BankFileDetail bfd
					UNION ALL
					SELECT bfd.BankFileId, bfd.LiquidationId
					FROM Payroll.BankFileDetail bfd
					LEFT JOIN @BankFileDetail bfdt ON bfd.Id = bfdt.Id
					WHERE bfd.BankFileId = @Id
					  AND bfdt.Id IS NULL
				) x
				JOIN Payroll.BankFile bf 
				  ON x.BankFileId <> bf.Id          -- otro archivo
				 AND x.LiquidationId = bf.Id        -- misma liquidación
				 AND bf.Process   = @Process        -- **¡mismo proceso!**
			)
			BEGIN
				SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + tp.Nit + ' - ' + tp.Name + '. Archivo: ' + bf.Code
					FROM
					(
						SELECT bfd.BankFileId, bfd.LiquidationId
						FROM @BankFileDetail bfd
						UNION ALL
						SELECT bfd.BankFileId, bfd.LiquidationId
						FROM Payroll.BankFileDetail bfd
						LEFT JOIN @BankFileDetail bfdt ON bfd.Id = bfdt.Id
						WHERE bfd.BankFileId = @Id AND bfdt.Id IS NULL
					) bfd
					JOIN Payroll.BankFileDetail b ON bfd.BankFileId <> b.BankFileId AND bfd.LiquidationId = b.LiquidationId
					JOIN Payroll.Employee e ON b.EmployeeId = e.Id
					JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
					JOIN Payroll.BankFile bf ON b.BankFileId = bf.Id
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 as CodeMessage, 'Ya se ha generado el archivo para los siguientes empleados: ' + CHAR(13) + CHAR(10) + @errors as Message, '' as Code, 0 as Id
				RETURN
			END

			/*************************************************************************************/

			--Si se esta insertando por primera vez se consulta la secuencia numerica
			IF @Id = 0
			BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @payrollSequenceId INT,
						@idForm varchar(5) = '336',
						@isManual BIT,
						@scope varchar(5) = ''
				
				SELECT 
					@payrollSequenceId = Id,
					@isManual = IsManual,
					@scope = Scope 
				FROM Payroll.PayrollSequence
				WHERE IdForm = @idForm

				IF @payrollSequenceId  IS NULL
				BEGIN
					SELECT 999 as CodeMessage, 'Secuencia de Archivo Plano no encontrada' as Message, '' as Code, 0 as Id
					RETURN
				END

				IF @isManual = 1
				BEGIN
					IF LEN(@Code) = 0
					BEGIN
						SELECT 999 as CodeMessage, 'La secuencia es manual y no se ha definido ningun código' as Message, '' as Code, 0 as Id
						RETURN
					END

					IF EXISTS (SELECT * FROM Payroll.BankFile WHERE Code = @Code)
					BEGIN
						SELECT 999 as CodeMessage, 'El codigo ya existe' as Message, '' as Code, 0 as Id
						RETURN
					END
				END
				ELSE
				BEGIN
					DECLARE @pattern varchar(300),
							@NextS int,
							@idSequenceDetail int

					--Se valida el scope
					IF @scope = 'O'
					BEGIN
						-- Consultamos la secuencia numerica del formulario
						SELECT @pattern = cs.Pattern, 
							@NextS = bsd.[Next] , 
							@idSequenceDetail = bsd.Id  
						FROM Payroll.PayrollSequenceDetail bsd 
						JOIN Payroll.PayrollSequence bs ON bs.Id = bsd.PayrollSequenceId
						JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
						WHERE bs.IdForm = @IdForm
					END
					ELSE
					BEGIN
						-- Consultamos la secuencia numerica del formulario
						SELECT @pattern = cs.Pattern, 
							@NextS = bsd.[Next], 
							@idSequenceDetail = bsd.Id  
						FROM Payroll.PayrollSequenceDetail bsd 
						JOIN Payroll.PayrollSequence bs ON bs.Id = bsd.PayrollSequenceId
						JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
						WHERE bs.IdForm = @IdForm AND bsd.IdOperatingUnit = @OperatingUnitId
					END

					IF (@idSequenceDetail IS NULL)
					BEGIN
						SELECT 999 as CodeMessage, 'Detalle de la Secuencia de Archivo Plano no encontrada' as Message, '' as Code, 0 as Id
						RETURN
					END

					SELECT @Code = dbo.GetSequence('', @pattern,@NextS)
					UPDATE Payroll.PayrollSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail
				END

				--Se inserta la cabecera
				INSERT INTO [Payroll].[BankFile]
				(
					[Code],[OperatingUnitId],[CompanyId],[LiquidationDate],[EntityBankAccountId],[ExpenseConceptId],[Value],[Status],[CreationUser],[CreationDate],[Process],[PeriodIncentivePayment],[YearIncentivePayment]
				)
				VALUES
				(
					@Code,@OperatingUnitId,@CompanyId,@LiquidationDate,@EntityBankAccountId,@ExpenseConceptId,@Value,@Status,@CodeUser,[Common].[GETDATE](),@Process,@PeriodIncentivePayment,@YearIncentivePayment
				)

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Payroll].[BankFile]
					SET [Code] = @Code,
						[OperatingUnitId] = @OperatingUnitId,
						[CompanyId] = @CompanyId,
						[LiquidationDate] = @LiquidationDate,
						[EntityBankAccountId] = @EntityBankAccountId,
						[ExpenseConceptId] = @ExpenseConceptId,
						[Value] = @Value,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[Process] = @Process,
						[PeriodIncentivePayment] = @PeriodIncentivePayment,
						[YearIncentivePayment] = @YearIncentivePayment
				WHERE Id = @Id
			END

			--Actualizamos los detalles
			UPDATE bfd
			SET bfd.LiquidationId = bfdt.LiquidationId,
				bfd.GroupId = bfdt.GroupId,
				bfd.PositionId = bfdt.PositionId,
				bfd.FunctionalUnitId = bfdt.FunctionalUnitId,
				bfd.ContractId = bfdt.ContractId,
				bfd.EmployeeId = bfdt.EmployeeId,
				bfd.EmployeeBankId = bfdt.EmployeeBankId,
				bfd.EmployeeBankTypeAccount = bfdt.EmployeeBankTypeAccount,
				bfd.EmployeeBankAccountNumber = bfdt.EmployeeBankAccountNumber,
				bfd.BasicSalary = bfdt.BasicSalary,
				bfd.TotalAccrued = bfdt.TotalAccrued,
				bfd.TotalDeducted = bfdt.TotalDeducted,
				bfd.TotalPaid = bfdt.TotalPaid
			FROM [Payroll].[BankFileDetail] bfd
			JOIN @BankFileDetail bfdt ON bfd.Id = bfdt.Id
			WHERE ISNULL(bfd.Id, 0) > 0

			--Insertamos los nuevos detalles
			INSERT INTO [Payroll].[BankFileDetail]
			(
				[BankFileId],
				[LiquidationId],
				[GroupId],
				[PositionId],
				[FunctionalUnitId],
				[ContractId],
				[EmployeeId],
				[EmployeeBankId],
				[EmployeeBankTypeAccount],
				[EmployeeBankAccountNumber],
				[BasicSalary],
				[TotalAccrued],
				[TotalDeducted],
				[TotalPaid]
				
			)
			SELECT
				@Id,
				bfd.LiquidationId,
				bfd.GroupId,
				bfd.PositionId,
				bfd.FunctionalUnitId,
				bfd.ContractId,
				bfd.EmployeeId,
				bfd.EmployeeBankId,
				bfd.EmployeeBankTypeAccount,
				bfd.EmployeeBankAccountNumber,
				bfd.BasicSalary,
				bfd.TotalAccrued,
				bfd.TotalDeducted,
				bfd.TotalPaid
			FROM @BankFileDetail bfd 
			WHERE ISNULL(bfd.Id, 0) = 0
		END
		ELSE
		BEGIN
			DECLARE @AnnulmentUser VARCHAR(20) = @CodeUser
			DECLARE @AnnulmentDate DATETIME = [Common].[GETDATE]()

			UPDATE [Payroll].[BankFile]
				SET [Status] = @Status,
					[AnnulmentUser] = @AnnulmentUser,
					[AnnulmentDate] = @AnnulmentDate
			WHERE Id = @Id

			DELETE FROM [Payroll].[BankFileDetail] WHERE BankFileId = @Id
		END

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END