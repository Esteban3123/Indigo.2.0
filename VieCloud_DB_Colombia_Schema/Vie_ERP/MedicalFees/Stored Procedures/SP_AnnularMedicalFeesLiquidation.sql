

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_AnnularMedicalFeesLiquidation] 
	@MedicalFeesLiquidationId as int,
	@CodeUser as varchar(20)
AS
BEGIN

	Begin try
	
		--Actualizo el estado de MedicalFeesCausation
		update [MedicalFees].MedicalFeesCausation
		set Status = case when InvoiceReversal = 1 then 4 when ObjectionAccepted = 1 then 4 else 1 end
		where Id in (select MedicalFeesCausationId from MedicalFees.MedicalFeesLiquidationDetail where MedicalFeesLiquidacionId = @MedicalFeesLiquidationId and LiquidationType = 1)
		
		--Actualizo el estado de MedicalFeesLiquidation a anulado
		update MedicalFees.MedicalFeesLiquidation
		set Status = 3, AnnulmentUser = @CodeUser, AnnulmentDate = [Common].[GETDATE]()
		where Id = @MedicalFeesLiquidationId

		select 0 as CodeMessage, 'Se anuló correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anula una liquidación de honorarios médicos registrada en el sistema. Al ejecutarse, revierte el estado de las causaciones de honorarios asociadas al detalle de la liquidación (devolviendo cada causación a su estado anterior según si tiene reversión de factura u objeción aceptada), y marca la liquidación principal como anulada (estado 3), registrando el usuario y la fecha de anulación. Recibe como parámetros el identificador de la liquidación a anular y el código del usuario que realiza la operación. Se usa para dejar sin efecto liquidaciones de honorarios médicos que fueron generadas incorrectamente o que deben corregirse.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Anula una liquidación de honorarios médicos, marcándola como anulada y revirtiendo el estado de las causaciones asociadas según si tuvieron reversión de factura u objeción aceptada.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la liquidación de honorarios médicos identificada; Deben existir detalles de liquidación con LiquidationType = 1 asociados para que se reviertan causaciones', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reprocesan causaciones cuyo detalle de liquidación tenga LiquidationType = 1; Una liquidación anulada queda en Status = 3 con usuario y fecha de anulación registrados; Cuando hay reversión de factura u objeción aceptada, la causación queda en estado 4; en caso contrario vuelve al estado 1 (disponible); Los errores no interrumpen al cliente: se devuelven como CodeMessage = 999', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de honorarios médicos; Causación de honorarios médicos; Anulación de liquidación; Reversión de factura; Objeción aceptada', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] MedicalFees.MedicalFeesCausation: Para causaciones referenciadas por detalles de la liquidación con LiquidationType = 1: si InvoiceReversal = 1 u ObjectionAccepted = 1 → Status = 4; en otro caso → Status = 1; [UPDATE] MedicalFees.MedicalFeesLiquidation: Marca la liquidación indicada como anulada (Status = 3), registrando usuario y fecha de anulación; [RETURN_RESULT] (resultset): Si todo va bien retorna CodeMessage = 0 y mensaje de éxito; ante excepción retorna CodeMessage = 999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MedicalFeesCausation.InvoiceReversal = 1 o ObjectionAccepted = 1 → Status de la causación se establece en 4 else Status de la causación se establece en 1', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_AnnularMedicalFeesLiquidation';
-- GO
