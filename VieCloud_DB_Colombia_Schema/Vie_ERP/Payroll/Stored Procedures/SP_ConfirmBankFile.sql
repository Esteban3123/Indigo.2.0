-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-12-14
-- Description:	Procedimiento que se encarga de confirmar el archivo plano de banco
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ConfirmBankFile] 
    @Id AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		/********************************************************** VALIDACIONES **********************************************************/

		IF EXISTS (SELECT 1 FROM Payroll.BankFile bb WHERE bb.Id = @Id AND bb.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(bb.Status = 2, 'Confirmado', 'Anulado') as Message
			FROM Payroll.BankFile bb
			WHERE bb.Id = @Id
			RETURN
		END

		/************************************************************************************************************************************/

		DECLARE @ConfirmationUser VARCHAR(20) = @CodeUser
		DECLARE @ConfirmationDate DATETIME = [Common].[GETDATE]()

		--Confirmamos el archivo plano
		UPDATE [Payroll].[BankFile]
			SET [Status] = 2,
				[ConfirmationUser] = @ConfirmationUser,
				[ConfirmationDate] = @ConfirmationDate
		WHERE Id = @Id

		SELECT 0 AS CodeMessage, 'Se confirmó correctamente' as Message
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma un archivo plano de pago bancario generado en el proceso de nómina. Valida que el archivo esté en estado pendiente (no confirmado ni anulado); si ya fue procesado, retorna un mensaje de advertencia indicando su estado actual. Al confirmar, actualiza el registro en la tabla de archivos bancarios de nómina marcándolo como confirmado (estado 2) y registrando el usuario y la fecha de confirmación. Se usa para cerrar el ciclo de aprobación de los pagos de nómina enviados al banco, garantizando trazabilidad de quién y cuándo autorizó el archivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmBankFile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmBankFile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma un archivo plano bancario de nómina cambiando su estado a confirmado y registrando el usuario y fecha de la confirmación, siempre que no haya sido procesado previamente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro identificado debe existir en Payroll.BankFile con Status = 1 (pendiente); en caso contrario retorna advertencia sin modificar nada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se confirma un archivo bancario cuando su estado actual es 1 (pendiente); nunca se reconfirma ni se modifica un archivo anulado.; La fecha de confirmación se obtiene siempre desde [Common].[GETDATE]() y no desde GETDATE() nativo, garantizando consistencia con la zona horaria del sistema.; Status = 2 representa ''Confirmado'' y siempre va acompañado de ConfirmationUser y ConfirmationDate poblados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'archivo plano bancario; nómina; confirmación de pagos; estados (pendiente/confirmado/anulado); trazabilidad de aprobación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Payroll.BankFile: Cuando el registro existe con Status = 1, se actualiza Status = 2, ConfirmationUser = usuario recibido y ConfirmationDate = [Common].[GETDATE]().; [RETURN_RESULT] (resultset): Si Status <> 1, devuelve CodeMessage=999 con texto ''El registro se encuentra en estado: Confirmado'' (Status=2) o ''Anulado'' (otro).; [RETURN_RESULT] (resultset): Tras UPDATE exitoso, devuelve CodeMessage=0 y mensaje ''Se confirmó correctamente''.; [RETURN_RESULT] (resultset): En caso de error capturado, devuelve CodeMessage=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe BankFile con Id dado y Status <> 1 → Retorna mensaje de advertencia (CodeMessage=999) indicando si está Confirmado (Status=2) o Anulado, y termina sin actualizar. else Procede a confirmar el archivo actualizando Status a 2 con usuario y fecha.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.BankFile', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBankFile';
-- GO
