-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-06-19
-- Description:	Homologa el tipo de documento de crystal de acuerdo a los tipos de documento de VIE
-- =============================================
CREATE FUNCTION [dbo].[GetHomologationIdentificationType]
(	
	@IdentificationTypeHIS INT
)
RETURNS INT
BEGIN

	DECLARE @IdentificationType INT

	SELECT @IdentificationType = CASE @IdentificationTypeHIS
		WHEN 1 THEN 0
		WHEN 2 THEN 1
		WHEN 3 THEN 2
		WHEN 4 THEN 3
		WHEN 5 THEN 4
		WHEN 6 THEN 5
		WHEN 7 THEN 6
		WHEN 8 THEN 8
		WHEN 9 THEN 9
		WHEN 10 THEN 10
		WHEN 11 THEN 11
		WHEN 12 THEN 12
		WHEN 13 THEN 13
		WHEN 14 THEN 14
		WHEN 15 THEN 15
		WHEN 16 THEN 16
		WHEN 17 THEN 17
		WHEN 18 THEN 18
		WHEN 19 THEN 19
		WHEN 20 THEN 20
		ELSE 0
	END

	 RETURN @IdentificationType
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte (homologa) el código de tipo de documento de identificación usado en el sistema Crystal Reports al código equivalente en el sistema VIE. Recibe un número entero que representa el tipo de documento en Crystal (por ejemplo: cédula de ciudadanía, tarjeta de identidad, pasaporte, entre otros) y devuelve el código correspondiente en VIE. Es utilizada para garantizar compatibilidad entre ambos sistemas al momento de generar reportes o integrar información de pacientes. Cubre hasta 20 tipos distintos de documento de identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetHomologationIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetHomologationIdentificationType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Homologa el código de tipo de documento de identificación del HIS (Crystal) al código equivalente usado en el sistema VIE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un código entero de tipo de documento del HIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor 7 del HIS no tiene equivalente directo en VIE (se omite el mapeo a 7); Cualquier código no homologable se normaliza a 0; La función es determinística y pura: no consulta tablas ni produce efectos secundarios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento de identificación; Homologación HIS-VIE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Cuando el tipo HIS está entre 1 y 7, se mapea a (HIS-1); cuando está entre 8 y 20, se devuelve el mismo valor; cualquier otro valor (incluyendo NULL o fuera de rango) retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo HIS entre 1 y 7 → Devuelve el valor decrementado en 1 (1→0, 2→1, …, 7→6); si Tipo HIS entre 8 y 20 → Devuelve el mismo valor sin cambio; si Tipo HIS no contemplado en el CASE → Devuelve 0 por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetHomologationIdentificationType';
GO
