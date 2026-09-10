-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 25/05/2014
-- Description:	Sp para desbloquear registro de las diferente tabla de bloqueos
-- =============================================
CREATE PROCEDURE [Common].[SP_UnlockBlockRecord]
	@CodUser as varchar(100)
AS
BEGIN
	BEGIN TRANSACTION 

	BEGIN TRY
		--TABLAS ERP
		DELETE FROM [Common].[BlockRecord] WHERE CodUser = @CodUser
		DELETE FROM [Billing] .[BlockRecordBilling]  where CodUser = @CodUser
		DELETE FROM [Budget].[BlockRecordBudget] where CodUser = @CodUser
		DELETE FROM [Contract].[BlockRecordContract] where CodUser = @CodUser
		DELETE FROM [Cost].[BlockRecordCost] where CodUser = @CodUser
		DELETE FROM [FixedAsset].[BlockRecordFixedAsset] where CodUser = @CodUser
		DELETE FROM [GeneralLedger].[BlockRecordGeneralLedger] where CodUser = @CodUser
		DELETE FROM [InteropCost].[BlockRecordInteropCost] where CodUser = @CodUser
		DELETE FROM [Inventory].[BlockRecordInventory] where CodUser = @CodUser
		DELETE FROM [Maintenance].[BlockRecordMaintenance] where CodUser = @CodUser
		DELETE FROM [MedicalFees].[BlockRecordMedicalFees] where CodUser = @CodUser
		DELETE FROM [MixingStation].[BlockRecordMixingStation] where CodUser = @CodUser
		DELETE FROM [Payments].[BlockRecordPayments] where CodUser = @CodUser
		DELETE FROM [Payroll].[BlockRecordPayroll] where CodUser = @CodUser
		DELETE FROM [Portfolio].[BlockRecordPortfolio] where CodUser = @CodUser
		DELETE FROM [Treasury].[BlockRecordTreasury] where CodUser = @CodUser
		DELETE FROM [Treasury].[CheckBlock]  where CodUser = @CodUser
		DELETE FROM [AccountManagement].[BlockRecordAccountManagement] WHERE CodUser = @CodUser
		DELETE FROM [Authorization].BlockRecordAuthorization WHERE CodUser = @CodUser

		--TABLAS EHR
		DELETE FROM dbo.INDOCUMEN WHERE CODUSUARI = @CodUser
		UPDATE dbo.HCORDIMAG SET USUOCUREG = NULL WHERE USUOCUREG = @CodUser
		UPDATE dbo.AMBORDIMA SET USUOCUREG = NULL WHERE USUOCUREG = @CodUser

		SELECT '1' AS Resultado, 'OK' Mensaje 
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION -- devuelvo la transaccion
		SELECT  '0'  as Resultado ,  'Se ha producido un error!' + ERROR_MESSAGE() Mensaje 
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desbloquea todos los registros que un usuario tiene bloqueados en el sistema, liberando simultáneamente los bloqueos en todos los módulos del ERP (facturación, presupuesto, contratos, costos, activos fijos, contabilidad general, inventario, mantenimiento, honorarios médicos, estación de mezclas, pagos, nómina, portafolio, tesorería y gestión de cuentas) y del EHR (documentos clínicos, órdenes de imágenes hospitalarias y ambulatorias). Se utiliza cuando un usuario cierra sesión de forma abrupta o necesita liberar manualmente sus bloqueos, evitando que otros usuarios queden impedidos de editar registros que quedaron bloqueados por sesiones inactivas o caídas del sistema. Recibe como parámetro el código del usuario (@CodUser) y elimina o limpia su referencia en cada tabla de bloqueos de todos los módulos de forma transaccional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_UnlockBlockRecord';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_UnlockBlockRecord';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Libera todos los bloqueos de edición que un usuario mantiene sobre registros en los distintos módulos ERP y EHR, eliminando o limpiando las marcas de bloqueo asociadas a ese usuario.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código del usuario cuyas marcas de bloqueo se desean liberar; Las tablas de bloqueo de cada módulo deben existir y ser accesibles desde la transacción', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las liberaciones de bloqueo se ejecutan dentro de una única transacción: o se liberan todos los bloqueos del usuario o ninguno; Solo se afectan registros pertenecientes al usuario indicado; no se tocan bloqueos de otros usuarios; Para órdenes de imagen (HCORDIMAG, AMBORDIMA) el desbloqueo se hace dejando NULL en el usuario ocupante, no eliminando la fila; El procedimiento siempre retorna un resultset con columnas Resultado y Mensaje', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Bloqueo de registros por usuario; Autorizaciones; Honorarios médicos; Tesorería / cheques; Órdenes de imágenes diagnósticas (hospitalización y ambulatorio); Documentos clínicos (EHR); Módulos ERP (facturación, presupuesto, contratos, costos, activos fijos, contabilidad, inventario, mantenimiento, nómina, cartera, pagos)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Common.BlockRecord: Elimina las filas donde CodUser = usuario indicado; [DELETE] Billing.BlockRecordBilling: Elimina las filas donde CodUser = usuario indicado; [DELETE] Budget.BlockRecordBudget: Elimina las filas donde CodUser = usuario indicado; [DELETE] Contract.BlockRecordContract: Elimina las filas donde CodUser = usuario indicado; [DELETE] Cost.BlockRecordCost: Elimina las filas donde CodUser = usuario indicado; [DELETE] FixedAsset.BlockRecordFixedAsset: Elimina las filas donde CodUser = usuario indicado; [DELETE] GeneralLedger.BlockRecordGeneralLedger: Elimina las filas donde CodUser = usuario indicado; [DELETE] InteropCost.BlockRecordInteropCost: Elimina las filas donde CodUser = usuario indicado; [DELETE] Inventory.BlockRecordInventory: Elimina las filas donde CodUser = usuario indicado; [DELETE] Maintenance.BlockRecordMaintenance: Elimina las filas donde CodUser = usuario indicado; [DELETE] MedicalFees.BlockRecordMedicalFees: Elimina las filas donde CodUser = usuario indicado; [DELETE] MixingStation.BlockRecordMixingStation: Elimina las filas donde CodUser = usuario indicado; [DELETE] Payments.BlockRecordPayments: Elimina las filas donde CodUser = usuario indicado; [DELETE] Payroll.BlockRecordPayroll: Elimina las filas donde CodUser = usuario indicado; [DELETE] Portfolio.BlockRecordPortfolio: Elimina las filas donde CodUser = usuario indicado; [DELETE] Treasury.BlockRecordTreasury: Elimina las filas donde CodUser = usuario indicado; [DELETE] Treasury.CheckBlock: Elimina las filas donde CodUser = usuario indicado, liberando los bloqueos de cheques; [DELETE] AccountManagement.BlockRecordAccountManagement: Elimina las filas donde CodUser = usuario indicado; [DELETE] Authorization.BlockRecordAuthorization: Elimina las filas donde CodUser = usuario indicado, liberando bloqueos de autorizaciones; [DELETE] dbo.INDOCUMEN: Elimina las filas donde CODUSUARI = usuario indicado (bloqueos EHR de documentos); [UPDATE] dbo.HCORDIMAG: Cuando USUOCUREG = usuario indicado, se setea USUOCUREG = NULL para liberar la órden de imagen ocupada por ese usuario; [UPDATE] dbo.AMBORDIMA: Cuando USUOCUREG = usuario indicado, se setea USUOCUREG = NULL para liberar la órden de imagen ambulatoria ocupada por ese usuario; [RETURN_RESULT] (resultset): Si todas las operaciones tienen éxito devuelve Resultado=''1'' y Mensaje=''OK''; en caso de error hace ROLLBACK y devuelve Resultado=''0'' con el mensaje del error', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre cualquier excepción dentro del TRY (al borrar/actualizar bloqueos) → ROLLBACK de la transacción y retorno de Resultado=''0'' con el mensaje de error else COMMIT y retorno de Resultado=''1'' con Mensaje=''OK''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_UnlockBlockRecord';
-- GO
