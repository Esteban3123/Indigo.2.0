-- =============================================
-- Author:		Emanuel Olaya Penagos
-- Create date: 09/01/2020
-- Description: sp para listar actualizar el estado del los procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_MPPS_UpdateStatus] 
	@AutoRisOrdenes varchar(16),
	@RisEstado int
AS
BEGIN

	declare @NUMINGRES varchar(10)
	declare @AUTO int
	declare @total int

	declare @len int
	declare @index int

	set @index = CHARINDEX('-', @AutoRisOrdenes)
	set @len = LEN(@AutoRisOrdenes)

	set @NUMINGRES = SUBSTRING(@AutoRisOrdenes, 0, CHARINDEX('-', @AutoRisOrdenes))
	set @AUTO = SUBSTRING(@AutoRisOrdenes, @index+1, @len-@index)

	set @total = (SELECT COUNT(NUMINGRES) FROM dbo.HCORDIMAG WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO)
	IF @total > 0
	BEGIN
		IF @RisEstado = 3 BEGIN
			UPDATE dbo.HCORDIMAG SET ESTTRASER = '1', TIENEGRABACION = '0', ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END ELSE BEGIN
			UPDATE dbo.HCORDIMAG SET ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END
	END ELSE
	BEGIN
		IF @RisEstado = 3 BEGIN
			UPDATE dbo.HCORDIMAG SET ESTTRASER = '1', TIENEGRABACION = '0', ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END ELSE BEGIN
			UPDATE dbo.HCORDIMAG SET ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END
	END

	RETURN @@ROWCOUNT
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualiza el estado de una orden de imagen diagnóstica (radiología, ecografía, tomografía, resonancia, etc.) en la historia clínica del paciente. Recibe como parámetros el identificador compuesto de la orden RIS (número de ingreso + autonumérico de la orden) y el nuevo estado a asignar. Cuando el estado corresponde a ''completado/grabado'' (valor 3), además de actualizar el estado del servicio CUPS/IPS, marca la orden como transmitida y sin grabación pendiente; para cualquier otro estado solo actualiza el campo de estado del servicio. Es utilizado por la integración con el sistema RIS de imágenes para sincronizar el avance o resultado de los estudios solicitados desde la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_UpdateStatus';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_UpdateStatus';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza el estado de una orden de imagen diagnóstica a partir del estado reportado por el sistema RIS, marcando además su trazabilidad cuando el estudio llega a estado 3.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador compuesto recibido debe contener un guion ''-'' que separe el número de ingreso del consecutivo de autorización (AUTO).; Debe existir (o no) un registro en HCORDIMAG identificado por NUMINGRES y AUTO derivados del parámetro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador compuesto se descompone siempre por la primera ocurrencia de ''-'': prefijo = NUMINGRES, sufijo = AUTO.; El estado 3 implica simultáneamente reiniciar la bandera de traslado (ESTTRASER=''1'') y marcar la orden como sin grabación (TIENEGRABACION=''0'').; ESTSERIPS siempre se actualiza con el estado recibido, sin importar la rama tomada.; La actualización siempre se realiza sobre la combinación exacta NUMINGRES + AUTO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imagen diagnóstica; Integración RIS; Estado de servicio (ESTSERIPS); Traslado de estudio (ESTTRASER); Grabación de estudio (TIENEGRABACION); Número de ingreso del paciente; Autorización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCORDIMAG: Cuando el estado RIS recibido es 3, se fija ESTTRASER=''1'', TIENEGRABACION=''0'' y ESTSERIPS=3 para la orden identificada por NUMINGRES y AUTO.; [UPDATE] dbo.HCORDIMAG: Cuando el estado RIS recibido es distinto de 3, solo se actualiza ESTSERIPS con el estado recibido para la orden identificada por NUMINGRES y AUTO.; [RETURN_RESULT] dbo.HCORDIMAG: Retorna @@ROWCOUNT, es decir, el número de filas afectadas por el último UPDATE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RisEstado = 3 → Actualiza ESTTRASER=''1'', TIENEGRABACION=''0'' y ESTSERIPS=3 (marca traslado pendiente y sin grabación). else Actualiza solamente ESTSERIPS con el valor recibido.; si Existen registros en HCORDIMAG con NUMINGRES y AUTO derivados (@total > 0) o no existen → Se ejecuta la misma lógica de actualización en ambas ramas (la validación de existencia no altera el comportamiento).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateStatus';
-- GO
