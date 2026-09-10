
CREATE Function [Admissions].[UserType]
(
	@PatientType tinyint,
	@AffiliateType tinyint
)
Returns varchar(5)
As
Begin 
	RETURN  CASE
		WHEN @PatientType = 1 AND @AffiliateType = 1 THEN '01' --Tipo paciente "Contributivo" + Tipo afiliado "Cotizante" = 01: Contributivo cotizante
		WHEN @PatientType = 1 AND @AffiliateType = 2 THEN '02' --Tipo paciente "Contributivo" + Tipo afiliado "Beneficiario" = 02: Contributivo beneficiario
		WHEN @PatientType = 1 AND @AffiliateType = 3 THEN '03' --Tipo paciente "Contributivo" + Tipo afiliado "Adicional" = 03: Contributivo adicional
		WHEN @PatientType = 2 THEN '04' --Tipo paciente "Subsidiado" = 04: Subsidiado
		WHEN @PatientType = 3 THEN '05' --Tipo paciente "No afiliado" = 05: No afiliado
		WHEN @PatientType = 9 AND @AffiliateType = 1 THEN '06' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Cotizante" = 06: Especial o Excepción cotizante
		WHEN @PatientType = 9 AND @AffiliateType = 2 THEN '07' --Tipo paciente "Especial o Excepción" + Tipo afiliado "Beneficiario" = 07: Especial o Excepción beneficiario
		WHEN @PatientType = 10 THEN '08' --Tipo paciente "Personas privadas de la libertad a cargo del Fondo Nacional de Salud" = 08: Personas privadas de la libertad a cargo del Fondo Nacional de Salud
		WHEN @PatientType = 11 THEN '09' --Tipo paciente "Tomador/ Amparado ARL" = 09: Tomador / Amparado ARL
		WHEN @PatientType = 12 THEN '10' --Tipo paciente "Tomador/ Amparado SOAT" = 10: Tomador / Amparado SOAT
		WHEN @PatientType = 13 THEN '11' --Tipo paciente "Tomador/ Amparado Planes voluntarios de salud" = 11: Tomador / Amparado Planes voluntarios de salud
		WHEN @PatientType = 4 THEN '12' --Tipo paciente "Particular" = 12: Particular
	 END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina el tipo de usuario RIPS (código de 2 dígitos) a partir del tipo de paciente y el tipo de afiliado en el sistema de admisiones. Combina ambos parámetros para clasificar al paciente según las categorías definidas por la normativa colombiana de salud: contributivo cotizante, contributivo beneficiario, subsidiado, particular, ARL, SOAT, planes voluntarios, privados de la libertad, entre otros. Se usa principalmente para la generación de reportes RIPS y facturación, garantizando que cada ingreso quede marcado con el código correcto de tipo de usuario según el régimen y condición de afiliación del paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'FUNCTION', @level1name = N'UserType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'FUNCTION', @level1name = N'UserType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Mapea la combinación de tipo de paciente y tipo de afiliado a un código de dos dígitos según la clasificación normativa colombiana de usuarios del sistema de salud.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@PatientType debe pertenecer al conjunto {1,2,3,4,9,10,11,12,13}; valores fuera de ese dominio retornan NULL; Cuando @PatientType=1 (Contributivo) o @PatientType=9 (Especial/Excepción) se requiere @AffiliateType en {1,2,3} (sólo {1,2} para tipo 9) para obtener un código válido', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código retornado siempre es una cadena de 2 caracteres entre ''01'' y ''12'' o NULL; @AffiliateType sólo es relevante cuando @PatientType ∈ {1,9}; para los demás tipos se ignora; No existe rama ELSE: combinaciones inválidas devuelven NULL en lugar de un valor por defecto; @PatientType=9 con @AffiliateType=3 (Adicional) no está soportado y produce NULL', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de paciente; Tipo de afiliado; Régimen contributivo; Cotizante; Beneficiario; Afiliado adicional; Régimen subsidiado; No afiliado; Régimen especial o de excepción; Personas privadas de la libertad; Fondo Nacional de Salud; ARL (Riesgos Laborales); SOAT; Planes voluntarios de salud; Particular', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve varchar(5) con el código ''01''..''12'' según combinación de @PatientType y @AffiliateType; cualquier combinación no contemplada retorna NULL', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PatientType=1 AND @AffiliateType=1 → Retorna ''01'' (Contributivo cotizante); si @PatientType=1 AND @AffiliateType=2 → Retorna ''02'' (Contributivo beneficiario); si @PatientType=1 AND @AffiliateType=3 → Retorna ''03'' (Contributivo adicional); si @PatientType=2 → Retorna ''04'' (Subsidiado), independiente de @AffiliateType; si @PatientType=3 → Retorna ''05'' (No afiliado), independiente de @AffiliateType; si @PatientType=9 AND @AffiliateType=1 → Retorna ''06'' (Especial o Excepción cotizante); si @PatientType=9 AND @AffiliateType=2 → Retorna ''07'' (Especial o Excepción beneficiario); si @PatientType=10 → Retorna ''08'' (Personas privadas de la libertad a cargo del Fondo Nacional de Salud); si @PatientType=11 → Retorna ''09'' (Tomador/Amparado ARL); si @PatientType=12 → Retorna ''10'' (Tomador/Amparado SOAT); si @PatientType=13 → Retorna ''11'' (Tomador/Amparado Planes voluntarios de salud); si @PatientType=4 → Retorna ''12'' (Particular); si Ninguna rama del CASE se cumple → Retorna NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'UserType';
GO
