-- =============================================
-- Author:		Emanuel Olaya Penagos
-- Create date: 09/01/2020
-- Description: sp para listar actualizar el estado del los procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_MPPS_UpdateState] 
	@NUMINGRES char(10),
	@AUTO int,
	@ESTSERIPS char(1)
AS
BEGIN
	declare @total int
	set @total = (SELECT COUNT(NUMINGRES) FROM dbo.HCORDIMAG WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO)
	IF @total > 0
	BEGIN
		UPDATE dbo.HCORDIMAG SET ESTSERIPS = @ESTSERIPS WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
	END ELSE
	BEGIN
		UPDATE dbo.AMBORDIMA SET ESTSERIPS = @ESTSERIPS WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
	END

	RETURN @@ROWCOUNT
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualiza el estado de un procedimiento de imagen diagnóstica (como radiología, ecografía, tomografía o resonancia) identificado por el número de ingreso y un código interno (AUTO). Primero verifica si la orden existe en la historia clínica hospitalaria (HCORDIMAG) y, de ser así, actualiza el estado del servicio allí; si no la encuentra, actualiza el estado en la tabla de órdenes ambulatorias (AMBORDIMA). Se usa para sincronizar el estado de ejecución o resultado de un estudio de imagen, cubriendo tanto pacientes hospitalizados como ambulatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_UpdateState';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_UpdateState';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza el estado IPS de una orden de imagen diagnóstica, eligiendo entre la tabla de hospitalización o la de ambulatorio según dónde exista el registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden identificada por número de ingreso y consecutivo (AUTO) en HCORDIMAG o en AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se actualiza una de las dos tablas en cada ejecución, nunca ambas; La actualización solo afecta filas que coincidan con el mismo NUMINGRES y AUTO usados para la verificación; El único campo modificado es el estado IPS (ESTSERIPS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de imagen diagnóstica; estado IPS del servicio; atención hospitalaria; atención ambulatoria; número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCORDIMAG: Cuando existe al menos un registro en HCORDIMAG con el NUMINGRES y AUTO dados (COUNT > 0), se actualiza ESTSERIPS en HCORDIMAG; [UPDATE] dbo.AMBORDIMA: Cuando NO existen registros en HCORDIMAG con el NUMINGRES y AUTO dados, se actualiza ESTSERIPS en AMBORDIMA; [RETURN_RESULT] : Retorna @@ROWCOUNT, es decir, el número de filas afectadas por el último UPDATE ejecutado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe la orden en HCORDIMAG (COUNT > 0) → Actualiza el estado IPS en la tabla de órdenes de hospitalización HCORDIMAG else Actualiza el estado IPS en la tabla de órdenes ambulatorias AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_UpdateState';
-- GO
