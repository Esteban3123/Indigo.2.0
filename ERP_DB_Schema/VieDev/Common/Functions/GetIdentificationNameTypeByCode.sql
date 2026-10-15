-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2024-03-30
-- Description:	De acuerdo con el tipo de identificación recibido devolver el texto completo del tipo de identificación
-- =============================================
CREATE FUNCTION [Common].[GetIdentificationNameTypeByCode]
(	
	@IdentificationType INT
)
RETURNS VARCHAR(MAX) 
BEGIN

	DECLARE @IdentificationTypeCode VARCHAR(MAX)

	BEGIN
		SELECT @IdentificationTypeCode = CASE @IdentificationType
			WHEN 0 THEN 'Cédula de Ciudadanía' 
			WHEN 1 THEN 'Cédula de Extranjería' 
			WHEN 2 THEN 'Tarjeta de Identidad' 
			WHEN 3 THEN 'Registro Civil' 
			WHEN 4 THEN 'Pasaporte' 		
			WHEN 5 THEN 'Adulto Sin Identificación' 
			WHEN 6 THEN 'Menor Sin Identificación' 
			WHEN 7 THEN 'Nit' 
			WHEN 8 THEN 'Número único de identificación personal' 
			WHEN 9 THEN 'Cetrigicado Nacido Vivo' 
			WHEN 10 THEN 'Carnet Diplomático' 
			WHEN 11 THEN 'Salvoconducto'
			WHEN 12 THEN 'Permiso especial de Permanencia'
			WHEN 13 THEN 'Permiso por Protección Temporal'
			WHEN 14 THEN 'Documento extranjero'
			ELSE 'SI' 
		END
	END
	
	 RETURN @IdentificationTypeCode
END