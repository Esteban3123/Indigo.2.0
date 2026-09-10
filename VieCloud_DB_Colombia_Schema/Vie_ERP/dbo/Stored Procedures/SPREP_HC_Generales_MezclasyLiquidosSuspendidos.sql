
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosSuspendidos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(MEZLIQPAC) AS 'MEZCLAS Y LIQUIDOS',RTRIM(ADMMEZLIQ) AS 'ADMINISTRACION MEZCLAS Y LIQUIDOS',RTRIM(MOTSUSMED) AS 'MOTIVO DE SUSPENSION',CODCENATE,UFUCODIGO,IPCODPACI              
FROM HCINFLIQA with(noLock)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLSUS=@NumeroFolio AND PREESTADO='4' AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para el reporte de mezclas y líquidos suspendidos en la historia clínica de un paciente hospitalizado. Consulta el registro de infusiones y líquidos (HCINFLIQA) filtrando por cédula del paciente, número de ingreso y número de folio, retornando el detalle de las mezclas administradas, la administración de esas mezclas y el motivo de suspensión, junto con el centro de atención y la unidad funcional. Solo incluye registros con estado ''4'' (suspendidos) y excluye el tipo de historia identificado como ''CODIGOAZU''. Se usa para visualizar en la historia clínica los medicamentos o líquidos intravenosos que fueron suspendidos durante un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consultar las mezclas y líquidos administrados, su administración y el motivo de suspensión registrados en la historia clínica de un paciente para un ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proporcionarse identificadores válidos de paciente, número de ingreso y número de folio para filtrar la información clínica.; Debe existir registro en HCINFLIQA que cumpla con estado ''4'' y tipo de historia distinto a ''CODIGOAZU''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros con estado preestablecido igual a ''4'' (suspendido/aplicable según el dominio).; Se excluyen explícitamente los registros cuyo tipo de historia sea ''CODIGOAZU''.; La consulta se realiza con NOLOCK, asumiendo lecturas sucias permitidas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Folio; Mezclas y líquidos; Administración de mezclas y líquidos; Motivo de suspensión de medicamentos; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFLIQA: Cuando paciente, ingreso y folio coinciden y el estado preestablecido es ''4'' y el tipo de historia es distinto de ''CODIGOAZU'', se retornan las mezclas/líquidos, su administración y motivo de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidos';
-- GO
