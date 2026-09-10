
CREATE function [dbo].[DiagnosticoPrincipal] (@NumIngreso as char(10), @CodigoPaciente as varchar(25))
RETURNS nvarchar (60)
AS
BEGIN
declare @DiagnosticoPrincipal as varchar(100)
	SELECT 
		 @DiagnosticoPrincipal =	RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO  )
		FROM  
			INDIAGNOP  A
			LEFT OUTER JOIN 
			INDIAGNOS B
			ON A.CODDIAGNO = B.CODDIAGNO 
		WHERE 
			NUMINGRES = @NumIngreso AND IPCODPACI =@CodigoPaciente AND CODDIAPRI = 1 
		
			ORDER BY A.FECDIAGNO DESC

 return @DiagnosticoPrincipal
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna el diagnóstico principal de un paciente para un ingreso o atención clínica específica. Recibe el número de ingreso y la cédula del paciente, y busca en los diagnósticos registrados por atención (INDIAGNOP) aquel marcado como diagnóstico principal (CODDIAPRI = 1), tomando el más reciente según la fecha de diagnóstico. Complementa el resultado con el nombre completo del diagnóstico consultando el catálogo maestro CIE-10 (INDIAGNOS), devolviendo una cadena con el formato ''código - nombre diagnóstico''. Se utiliza para mostrar o reportar el diagnóstico principal del paciente en historias clínicas, resúmenes de atención, RIPS y otros documentos clínicos o administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiagnosticoPrincipal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiagnosticoPrincipal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la descripción concatenada (código y nombre) del diagnóstico principal más reciente de un paciente para un ingreso determinado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de diagnósticos del paciente con el número de ingreso y código de paciente indicados, marcado como diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el diagnóstico marcado como principal (CODDIAPRI = 1).; Cuando hay múltiples diagnósticos principales, prevalece el de fecha de diagnóstico más reciente.; El resultado siempre se entrega con formato ''código - nombre'' del diagnóstico, eliminando espacios sobrantes.; Si el catálogo de diagnósticos no tiene correspondencia, igualmente se intenta retornar el dato (LEFT JOIN), pudiendo devolver NULL si no hay match.; Si no existe diagnóstico principal para el ingreso/paciente, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; diagnóstico; diagnóstico principal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOP: Cuando existe un registro con CODDIAPRI=1 para el ingreso y paciente, retorna ''CODDIAGNO - NOMDIAGNO'' del diagnóstico de FECDIAGNO más reciente; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPrincipal';
GO
