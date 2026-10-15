
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