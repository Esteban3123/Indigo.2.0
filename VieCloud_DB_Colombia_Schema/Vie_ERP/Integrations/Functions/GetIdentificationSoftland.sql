
-- =============================================
-- Author:		Rafael patiño
-- Create date: 2021-09-29
-- Description:	funcion para integracion con softland del manejo de tipo y lentgh de caracteres
-- =============================================
CREATE FUNCTION [Integrations].[GetIdentificationSoftland]
(	
	@IdentificationType INT,
	@Numero varchar(25)
)
RETURNS VARCHAR(25) 
BEGIN

	DECLARE @Identificacion VARCHAR(25)

   set @numero = Ltrim(Rtrim(@Numero))

	SELECT @Identificacion = CASE @IdentificationType
			WHEN 16 THEN iif(len(@Numero) > 9,SUBSTRING (@Numero,1,9),RIGHT('000000000000' + @numero,9))   --CF - Cédula Física : debe tener 9 caracteres
			WHEN 17 THEN iif(len(@Numero) > 10,SUBSTRING (@Numero,1,10),RIGHT('000000000000' + @numero,10)) --CJ - Cédula Jurídica : debe tener 10 caracteres
			WHEN 18 THEN iif(len(@Numero) > 12,SUBSTRING (@Numero,1,12),RIGHT('000000000000' + @numero,12)) --DM - Dimex : debe tener 12 caracteres
			WHEN 19 THEN iif(len(@Numero) > 10,SUBSTRING (@Numero,1,10),RIGHT('000000000000' + @numero,10)) --NI - Nite : debe tener 10 caracteres
			WHEN 20 THEN iif(len(@Numero) > 10,SUBSTRING (@Numero,1,10),RIGHT('000000000000' + @numero,10)) --OT - Otro : debe tener 10 caracteres	
			WHEN 7 THEN iif(len(@Numero) > 10,SUBSTRING (@Numero,1,10),RIGHT('000000000000' + @numero,10)) --OT - Otro : debe tener 10 caracteres
			WHEN 6 THEN iif(len(@Numero) > 10,SUBSTRING (@Numero,1,10),RIGHT('000000000000' + @numero,10)) --OT - Otro : debe tener 10 caracteres
	END

	 RETURN @Identificacion
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de integración con el sistema contable Softland que formatea el número de identificación de una persona (paciente, proveedor o entidad) según su tipo de documento, ajustando la longitud con ceros a la izquierda o truncando si supera el máximo permitido. Soporta los tipos: Cédula Física (9 dígitos), Cédula Jurídica (10 dígitos), Dimex (12 dígitos), Nite (10 dígitos) y Otros (10 dígitos). Se usa para garantizar que el número de cédula o identificación enviado a Softland cumpla exactamente el formato requerido por ese sistema externo, evitando rechazos en la integración contable o de facturación.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'FUNCTION', @level1name = N'GetIdentificationSoftland';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'FUNCTION', @level1name = N'GetIdentificationSoftland';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Normaliza un número de identificación al formato (longitud fija con ceros a la izquierda o truncado) requerido por Softland según el tipo de identificación tributaria de Costa Rica.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un tipo de identificación reconocido (6, 7, 16, 17, 18, 19 o 20); cualquier otro valor produce NULL; El número debe caber en VARCHAR(25)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número se entrega siempre sin espacios en blanco (LTRIM/RTRIM aplicados antes del formateo); Cédula Física se normaliza a longitud fija 9; Cédula Jurídica, NITE y Otro (tipos 19, 20, 7, 6, 17) se normalizan a longitud fija 10; DIMEX se normaliza a longitud fija 12; El relleno se hace con ceros a la izquierda usando un literal de 12 ceros; Si el número excede la longitud requerida se trunca tomando los primeros caracteres por la izquierda; Tipos de identificación no contemplados resultan en NULL', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cédula Física; Cédula Jurídica; DIMEX; NITE; Identificación tributaria; Integración Softland', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (valor escalar de retorno): Retorna el número formateado según el tipo: 9 chars (tipo 16), 10 chars (tipos 6,7,17,19,20), 12 chars (tipo 18); NULL si el tipo no está mapeado', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IdentificationType = 16 (Cédula Física) → Trunca a 9 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 9; si IdentificationType = 17 (Cédula Jurídica) → Trunca a 10 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 10; si IdentificationType = 18 (DIMEX) → Trunca a 12 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 12; si IdentificationType = 19 (NITE) → Trunca a 10 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 10; si IdentificationType = 20 (Otro) → Trunca a 10 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 10; si IdentificationType = 7 → Trunca a 10 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 10; si IdentificationType = 6 → Trunca a 10 caracteres si excede; de lo contrario rellena con ceros a la izquierda hasta 10 else Si el tipo no coincide con ningún caso, retorna NULL', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'FUNCTION', @level1name=N'GetIdentificationSoftland';
GO
