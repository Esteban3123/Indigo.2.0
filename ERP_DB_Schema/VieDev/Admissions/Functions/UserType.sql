
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