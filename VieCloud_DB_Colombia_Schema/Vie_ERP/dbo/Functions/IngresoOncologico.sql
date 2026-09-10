

CREATE FUNCTION [dbo].[IngresoOncologico] 
(
	@Identificacion As varchar(25)
)
RETURNS varchar(10)
AS
Begin
		declare @Ingreso as varchar(10)

		--Consultamos el Ingreso con el que esta hospitalizado el paciente
		IF exists(  SELECT RTRIM(I.NUMINGRES)  FROM dbo.ADINGRESO I WITH(NOLOCK) 
								INNER JOIN dbo.CHREGESTA R WITH(NOLOCK) ON R.NUMINGRES = I.NUMINGRES  AND R.REGESTADO = 1
					WHERE I.IPCODPACI = @Identificacion 
				  ) 
		BEGIN
		--Asignamos el Ingreso con el que esta hospitalizado el paciente
					  SELECT @Ingreso = RTRIM(I.NUMINGRES) FROM dbo.ADINGRESO I WITH(NOLOCK) 
							 INNER JOIN dbo.CHREGESTA R WITH(NOLOCK) ON R.NUMINGRES = I.NUMINGRES  AND R.REGESTADO = 1
					  WHERE I.IPCODPACI = @Identificacion 
				 end  
		else BEGIN 
		--Asignamos el Ingreso que esta como oncologico en el ingreso que debe ser 1 
					  SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')
				
		end
				Return @Ingreso 
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que determina el número de ingreso oncológico activo de un paciente, dado su número de identificación (cédula). Primero busca si el paciente tiene un ingreso hospitalario actualmente en curso, verificando que exista un estado de estancia vigente (REGESTADO = 1) en el historial de estancias (CHREGESTA) cruzado con los ingresos (ADINGRESO). Si el paciente está hospitalizado, retorna ese número de ingreso; de lo contrario, busca el ingreso marcado como tratamiento especial oncológico (TRATAESPECIA = 3) con estado pendiente o en proceso. Se usa para identificar con qué episodio de ingreso está asociado un paciente oncológico, especialmente en flujos de atención, prescripción o seguimiento de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'IngresoOncologico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'IngresoOncologico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el número de ingreso asociado a un paciente oncológico, priorizando el ingreso en hospitalización activa y, en su defecto, el ingreso marcado como tratamiento especial oncológico pendiente o en proceso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en ADINGRESO identificado por su código de paciente.; Para la rama hospitalaria, debe existir un registro de estancia en CHREGESTA con REGESTADO = 1 vinculado al ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Prioriza siempre el ingreso con estancia activa sobre el ingreso oncológico marcado como tratamiento especial.; Solo considera ingresos oncológicos en estado vacío ('''') o ''P'' (pendiente/en proceso).; El tratamiento especial oncológico se identifica con TRATAESPECIA = 3.; Una estancia activa se identifica con REGESTADO = 1.; Retorna a lo sumo un único número de ingreso (TOP 1 en la rama alterna).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente oncológico; ingreso hospitalario; tratamiento especial oncológico; estancia activa; estado de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Si existe un ingreso del paciente con estancia activa (CHREGESTA.REGESTADO = 1), retorna ese NUMINGRES.; [RETURN_RESULT] N/A: Si no existe ingreso con estancia activa, retorna el primer NUMINGRES del paciente con TRATAESPECIA = 3 y IESTADOIN en ('''',''P'').; [RETURN_RESULT] N/A: Si no se cumple ninguna condición, retorna NULL (variable @Ingreso sin asignar).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe ingreso del paciente con registro en CHREGESTA con REGESTADO = 1 → Asigna como ingreso el NUMINGRES de la hospitalización activa else Busca el ingreso oncológico (TRATAESPECIA = 3) con estado IESTADOIN vacío o ''P''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoOncologico';
GO
