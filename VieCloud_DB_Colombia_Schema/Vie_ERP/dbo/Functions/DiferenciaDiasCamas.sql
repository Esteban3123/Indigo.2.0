
CREATE FUNCTION [dbo].[DiferenciaDiasCamas] (@Ingreso char(10), @Paciente varchar(25))
 RETURNS int
 AS
	 BEGIN
	 declare @Dias int, @EstadoActivo int
	 --conocer el estado
	 SELECT @EstadoActivo = count(*) from  CHREGESTA WHERE IPCODPACI =@Paciente AND NUMINGRES =@Ingreso AND REGESTADO ='1'
	 IF @EstadoActivo = 1 
	 BEGIN
	
		SELECT 
				@Dias=	DATEDIFF(DAY, MIN(FECINIEST ),GETDATE()) 
		  FROM 
				CHREGESTA
		 WHERE
				IPCODPACI =@Paciente AND NUMINGRES =@Ingreso
	END
	ELSE
	BEGIN
		SELECT 
				@Dias=	DATEDIFF(DAY, MIN(FECINIEST ),MAX(FECFINEST)) 
		  FROM 
				CHREGESTA
		 WHERE
				IPCODPACI =@Paciente AND NUMINGRES =@Ingreso
	END
	return @Dias
 END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad de días de estancia en camas que tuvo un paciente durante un ingreso hospitalario específico. Recibe como parámetros el número de ingreso y la cédula del paciente, y consulta el historial de estados de cama (CHREGESTA) para determinar el período de hospitalización. Si el paciente tiene un estado activo (aún internado), calcula los días transcurridos desde la fecha de inicio del primer estado hasta el día de hoy; si ya fue dado de alta, calcula la diferencia entre la fecha de inicio mínima y la fecha de fin máxima registradas. Se utiliza para reportes de ocupación, facturación por estancia y auditoría de días cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaDiasCamas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaDiasCamas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula los días de estancia hospitalaria de un paciente en un ingreso, distinguiendo si sigue activo (hasta hoy) o si ya fue dado de alta (hasta la fecha de fin registrada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de estancia en CHREGESTA para el paciente e ingreso indicados, de lo contrario los días retornados serán NULL.; El estado activo se identifica con REGESTADO=''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de inicio considerada siempre es la mínima FECINIEST registrada para el ingreso del paciente.; Solo se considera estancia activa cuando hay exactamente un registro con REGESTADO=''1''.; El cálculo siempre se restringe al par paciente/ingreso recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Estancia; Días cama; Estado de estancia (activo/alta)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si existe exactamente un registro con REGESTADO=''1'' para el paciente e ingreso, retorna DATEDIFF en días entre la mínima FECINIEST y la fecha actual (GETDATE()).; [RETURN_RESULT] : Si no existe un único registro activo (estado distinto de 1 o cantidad ≠ 1), retorna DATEDIFF en días entre la mínima FECINIEST y la máxima FECFINEST.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Conteo de registros con REGESTADO=''1'' para el paciente/ingreso es igual a 1 → Calcula los días desde la fecha mínima de inicio de estancia hasta la fecha actual (estancia en curso). else Calcula los días desde la fecha mínima de inicio de estancia hasta la fecha máxima de fin de estancia (estancia finalizada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaDiasCamas';
GO
