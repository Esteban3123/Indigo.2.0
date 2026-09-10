-- =============================================
-- Author:      Duván Mejia Cortes
-- Create Date: 2022-03-01
-- Description: Lista los Diagnosticos & Procedimientos QX - NoQX - Cirugias 
-- =============================================
CREATE PROCEDURE [dbo].[SP_ANE_ListarDiagnosticosPaciente]
(
	@PatientCode varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON

		SELECT TOP(1) AG.FECHORAIN currentDay,IPS.CODSERIPS ServiceCode,
				IPS.DESSERIPS ServiceDescription,
				DG.CODDIAGNO DiagnosisCode,
				DG.NOMDIAGNO DiagnosisName	 
		  FROM 
		       AGEPROGQX AG with(nolock) INNER JOIN 			  
		       INCUPSIPS IPS with(nolock) ON IPS.CODSERIPS = AG.CODSERIPS INNER JOIN	
		       INDIAGNOS DG  with(nolock) ON DG.CODDIAGNO = AG.DiagnosisCode
		 WHERE AG.IPCODPACI = @PatientCode AND AG.CODESTPQX <> '6' AND Cast(AG.FECHORAIN As Date) = Cast(Common.Getdate() As DATE)		   
         ORDER BY Cast(AG.FECHORAIN  as DATE) DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista el diagnóstico y procedimiento quirúrgico vigente de un paciente para el día actual, utilizado en el módulo de anestesia. Recibe la cédula o código del paciente y consulta la programación quirúrgica del día (AGEPROGQX), excluyendo cirugías canceladas (estado 6), para obtener el servicio o procedimiento CUPS/IPS asociado (INCUPSIPS) y el diagnóstico CIE-10 correspondiente (INDIAGNOS). Retorna el registro más reciente del día con el código y nombre del servicio (procedimiento quirúrgico o no quirúrgico) y el código y nombre del diagnóstico, permitiendo al anestesiólogo o personal clínico identificar rápidamente qué intervención y diagnóstico tiene agendado el paciente en el quirófano para la jornada en curso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el diagnóstico y servicio quirúrgico programado del día actual para un paciente, excluyendo programaciones canceladas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener una programación quirúrgica registrada para la fecha actual del sistema; El estado de la programación quirúrgica debe ser distinto de ''6''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la programación del día actual (fecha del servidor vía Common.Getdate()); Se excluyen programaciones con estado ''6'' (presumiblemente canceladas/anuladas); Siempre devuelve a lo sumo un registro (TOP 1); Únicamente se incluyen programaciones que tengan diagnóstico válido en INDIAGNOS y servicio válido en INCUPSIPS (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Diagnóstico; Programación quirúrgica; Servicio IPS; Procedimiento quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando existe una programación quirúrgica del paciente cuya FECHORAIN = fecha actual y CODESTPQX <> ''6'', se retorna el primer registro (TOP 1) ordenado por fecha descendente con datos de servicio IPS y diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.Getdate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.INCUPSIPS; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ANE_ListarDiagnosticosPaciente';
-- GO
