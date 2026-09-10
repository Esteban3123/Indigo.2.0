-- =============================================
-- Author:		Sergio Fernandez
-- Create date: 08/05/2014
-- Description:	sp para insertar las cuentas contables
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_InsertPUC]
	@IdAccountLevel int, 
    @IdAccountClass int,
	@Number varchar(50),
	@Name varchar(100),
	@IdParent int,
	@HandlesThirdParty bit,
	@CloseThirdParty bit,
	@IdThirdParty int,
	@ReconcileAccount bit,
	@Availability tinyint,	
	@HandlesCostCenter bit,
	@RetencionType tinyint,
	@AllowsMovement bit,
	@Status bit	
AS
BEGIN
SET NOCOUNT ON;
	INSERT INTO GeneralLedger.MainAccounts 
	 (IdAccountLevel, IdAccountClass, Number,Name,IdParent,HandlesThirdParty,CloseThirdParty,IdThirdParty,ReconcileAccount,[Availability]
	 ,HandlesCostCenter,RetencionType,[Status],AllowsMovement, CreationUser,CreationDate)
	 VALUES (@IdAccountLevel,@IdAccountClass,@Number,@Name,@IdParent,@HandlesThirdParty,@CloseThirdParty,@IdThirdParty,@ReconcileAccount,@Availability
	 ,@HandlesCostCenter,@RetencionType,@Status,@AllowsMovement, '', [Common].[GETDATE]());

	 select SCOPE_IDENTITY() as id;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva cuenta contable en el Plan Único de Cuentas (PUC) dentro del módulo de contabilidad general. Inserta la cuenta en la tabla maestra de cuentas contables (MainAccounts) con todos sus atributos: nivel jerárquico, clase, número y nombre de cuenta, cuenta padre, manejo de terceros, conciliación, disponibilidad, centro de costos, tipo de retención, estado y si permite movimientos. Retorna el identificador generado para la nueva cuenta, lo que permite al sistema de facturación y contabilidad vincular inmediatamente la cuenta recién creada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_InsertPUC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_InsertPUC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra una nueva cuenta contable en el plan único de cuentas (PUC) y devuelve el identificador generado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir referencias válidas para nivel de cuenta, clase de cuenta, cuenta padre y tercero asociado cuando aplique', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de creación siempre se obtiene desde [Common].[GETDATE]() y no desde el cliente; El usuario de creación se persiste siempre como cadena vacía; Cada inserción retorna el identidad generado mediante SCOPE_IDENTITY()', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan Único de Cuentas (PUC); Cuenta contable; Clase de cuenta; Nivel de cuenta; Tercero; Centro de costo; Tipo de retención; Conciliación de cuenta; Disponibilidad', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.MainAccounts: Inserta una cuenta contable con CreationUser vacío ('''') y CreationDate fijada por [Common].[GETDATE](); [RETURN_RESULT] GeneralLedger.MainAccounts: Devuelve SCOPE_IDENTITY() como ''id'' tras la inserción', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPUC';
-- GO
