-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-05-03
-- Description:	De acuerdo con el tipo de identificación recibido devolver el respectivo en Letras
-- =============================================
CREATE FUNCTION [Common].[GetIdentificationTypeCode]
(	
	@IdentificationType INT,
	@Environment TINYINT
)
RETURNS VARCHAR(2) 
BEGIN

	DECLARE @IdentificationTypeCode VARCHAR(2)

	IF @Environment = 1
	BEGIN
		SELECT @IdentificationTypeCode = CASE @IdentificationType
			WHEN 1 THEN 'CC' 
			WHEN 2 THEN 'CE' 
			WHEN 3 THEN 'TI' 
			WHEN 4 THEN 'RC' 
			WHEN 5 THEN 'PA' 		
			WHEN 6 THEN 'AS' 
			WHEN 7 THEN 'MS' 
			WHEN 8 THEN 'NU' 
			WHEN 9 THEN 'CN' 
			WHEN 10 THEN 'CD' 
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE'
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			ELSE 'SI' 
		END
	END
	ELSE IF @Environment = 2
	BEGIN
		SELECT @IdentificationTypeCode = CASE @IdentificationType
			WHEN 0 THEN 'CC' 
			WHEN 1 THEN 'CE' 
			WHEN 2 THEN 'TI' 
			WHEN 3 THEN 'RC' 
			WHEN 4 THEN 'PA' 		
			WHEN 5 THEN 'AS' 
			WHEN 6 THEN 'MS' 
			WHEN 7 THEN 'NI' 
			WHEN 8 THEN 'NU' 
			WHEN 9 THEN 'CN' 
			WHEN 10 THEN 'CD' 
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE'
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			ELSE 'SI' 
		END
	END

	 RETURN @IdentificationTypeCode
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un código numérico interno de tipo de identificación en su abreviatura oficial de dos letras (por ejemplo: CC = Cédula de Ciudadanía, CE = Cédula de Extranjería, TI = Tarjeta de Identidad, RC = Registro Civil, PA = Pasaporte, entre otros). Recibe el número de tipo de identificación y un indicador de entorno o sistema origen, ya que distintos entornos pueden usar numeraciones diferentes para el mismo tipo de documento. Se usa para normalizar y traducir los tipos de documento del paciente al formato estándar requerido en reportes RIPS, facturación y otros procesos del sistema de salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetIdentificationTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetIdentificationTypeCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un tipo de identificación numérico a su código abreviado de dos letras (CC, CE, TI, etc.) según el entorno indicado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar un entorno (1 o 2) que define el mapeo numérico a aplicar; El tipo de identificación numérico debe corresponder al catálogo del entorno; valores fuera de rango se resuelven a ''SI''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es un código de máximo 2 caracteres; Cualquier tipo de identificación no contemplado en el CASE devuelve ''SI'' (Sin Identificación); El entorno 2 incluye el código ''NI'' (posición 7) que no existe en el entorno 1; El entorno 1 inicia el catálogo en 1 mientras el entorno 2 inicia en 0, desplazando todos los códigos en una posición', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de identificación; Cédula de ciudadanía (CC); Cédula de extranjería (CE); Tarjeta de identidad (TI); Registro civil (RC); Pasaporte (PA); Adulto sin identificación (AS); Menor sin identificación (MS); NIT (NI); Permiso especial de permanencia (PE); Permiso de protección temporal (PT); Documento extranjero (DE); Sin identificación (SI)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve VARCHAR(2) con el código abreviado del tipo de identificación según el entorno; si el código no coincide con ninguno mapeado retorna ''SI''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Entorno = 1 → Aplica mapeo: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE; otros=''SI'' else Evalúa el siguiente bloque de entorno; si Entorno = 2 → Aplica mapeo: 0=CC, 1=CE, 2=TI, 3=RC, 4=PA, 5=AS, 6=MS, 7=NI, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE; otros=''SI'' else Si entorno no es 1 ni 2, retorna NULL al no asignarse valor', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetIdentificationTypeCode';
GO
