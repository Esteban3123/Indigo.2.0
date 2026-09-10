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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico de tipo de identificación en su nombre completo en texto. Recibe un entero (por ejemplo: 0 para Cédula de Ciudadanía, 1 para Cédula de Extranjería, 4 para Pasaporte, 7 para NIT, entre otros) y retorna la descripción legible del tipo de documento de identidad. Se usa para mostrar al usuario final el nombre completo del tipo de identificación en formularios, reportes y consultas relacionadas con pacientes, personas o entidades. Cubre todos los tipos de documento reconocidos en Colombia, incluyendo documentos especiales como Permiso por Protección Temporal (migrantes venezolanos) y Salvoconducto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetIdentificationNameTypeByCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetIdentificationNameTypeByCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de identificación a su descripción textual oficial, devolviendo ''SI'' cuando el código no corresponde al catálogo conocido.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe suministrar un código entero de tipo de identificación; valores fuera del catálogo conocido producirán el valor por defecto', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un VARCHAR no nulo: nombres oficiales para códigos 0-14 o ''SI'' en cualquier otro caso; El catálogo de tipos de identificación está fijado de forma literal en código (no consulta tablas); El código 9 produce el literal ''Cetrigicado Nacido Vivo'' (errata textual preservada)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de identificación; Cédula de Ciudadanía; Cédula de Extranjería; Tarjeta de Identidad; Registro Civil; Pasaporte; NIT; Carnet Diplomático; Salvoconducto; Permiso Especial de Permanencia; Permiso por Protección Temporal; Certificado de Nacido Vivo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código = 0 → Retorna ''Cédula de Ciudadanía''; si Código = 1 → Retorna ''Cédula de Extranjería''; si Código = 2 → Retorna ''Tarjeta de Identidad''; si Código = 3 → Retorna ''Registro Civil''; si Código = 4 → Retorna ''Pasaporte''; si Código = 5 → Retorna ''Adulto Sin Identificación''; si Código = 6 → Retorna ''Menor Sin Identificación''; si Código = 7 → Retorna ''Nit''; si Código = 8 → Retorna ''Número único de identificación personal''; si Código = 9 → Retorna ''Cetrigicado Nacido Vivo'' (texto con error tipográfico en el código); si Código = 10 → Retorna ''Carnet Diplomático''; si Código = 11 → Retorna ''Salvoconducto''; si Código = 12 → Retorna ''Permiso especial de Permanencia''; si Código = 13 → Retorna ''Permiso por Protección Temporal''; si Código = 14 → Retorna ''Documento extranjero''; si Código fuera del rango 0-14 o NULL → Retorna ''SI'' como valor por defecto', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationNameTypeByCode';
GO
