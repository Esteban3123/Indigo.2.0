
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos]
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
                   
FROM HCINFLIQI WITH(NOLOCK)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLSUS=@NumeroFolio AND PREESTADO='4' AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las mezclas y líquidos intravenosos que han sido suspendidos internamente para un paciente hospitalizado, dado su código de paciente (cédula), número de ingreso y número de folio. Recupera de la tabla HCINFLIQI los registros de preparaciones magistrales o soluciones compuestas con estado de suspensión (PREESTADO=''4''), excluyendo registros de tipo historia azul, y retorna la descripción de la mezcla, la información de administración, el motivo de suspensión, el centro de atención y la unidad funcional. Se utiliza en la historia clínica para visualizar en el reporte de medicamentos suspendidos qué mezclas o líquidos fueron retirados del esquema terapéutico del paciente durante su estancia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consultar las mezclas y líquidos suspendidos internamente de un paciente, asociados a un ingreso y folio de suspensión, junto con su administración y motivo de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proporcionarse identificadores de paciente, ingreso y folio de suspensión válidos para que existan filas en HCINFLIQI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros en estado ''4'' (suspendidos).; Excluye registros cuyo tipo de historia sea ''CODIGOAZU''.; Resultados acotados al paciente, ingreso y folio de suspensión indicados.; Lectura sin bloqueo (WITH NOLOCK), permite lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Historia clínica; Mezclas y líquidos; Administración de mezclas y líquidos; Suspensión de medicamento; Motivo de suspensión; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFLIQI: Cuando PREESTADO=''4'' y IDETIPHIS<>''CODIGOAZU'' y coinciden paciente, ingreso y folio, se retorna la mezcla/líquido, su administración, motivo de suspensión, centro de atención, unidad funcional y código de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos';
-- GO
