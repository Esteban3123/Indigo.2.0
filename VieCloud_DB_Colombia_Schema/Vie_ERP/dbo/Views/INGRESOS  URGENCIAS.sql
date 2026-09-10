
CREATE VIEW [dbo].[INGRESOS  URGENCIAS]
AS
SELECT        TOP (100) PERCENT dbo.ADINGRESO.NUMINGRES, dbo.ADINGRESO.IPCODPACI, dbo.ADINGRESO.IINGREPOR, dbo.ADINGRESO.IFECHAING, 
                         dbo.INPACIENT.CODENTIDA, dbo.ADINGRESO.ICAUSAING
FROM            dbo.ADINGRESO INNER JOIN
                         dbo.INPACIENT ON dbo.ADINGRESO.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE        (dbo.ADINGRESO.IFECHAING >= CONVERT(DATETIME, '2015-08-01 00:00:00', 102)) AND (dbo.ADINGRESO.IFECHAING <= CONVERT(DATETIME, 
                         '2015-08-31 23:59:00', 102)) AND (dbo.ADINGRESO.IINGREPOR = 1) AND (dbo.INPACIENT.CODENTIDA = '00130') AND (dbo.ADINGRESO.ICAUSAING = 6)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos de urgencias del mes de agosto de 2015 para la entidad con código 00130. Combina los datos de admisiones (ADINGRESO) con la información maestra del paciente (INPACIENT) para mostrar únicamente los ingresos cuya vía de ingreso corresponde a urgencias (IINGREPOR = 1) y cuya causa de ingreso es de tipo 6. Expone el número de ingreso, la cédula del paciente, la fecha de ingreso, el código de la entidad aseguradora y la causa del ingreso. Es una vista de reportería puntual, aparentemente creada para un análisis o auditoría de urgencias en ese período específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INGRESOS  URGENCIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INGRESOS  URGENCIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los ingresos de urgencias ocurridos en agosto de 2015 para los pacientes afiliados a la entidad ''00130'' y con una causa de ingreso específica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relación válida entre ADINGRESO.IPCODPACI e INPACIENT.IPCODPACI.; Los códigos de dominio IINGREPOR=1, ICAUSAING=6 y CODENTIDA=''00130'' deben corresponder a los catálogos vigentes para que la vista retorne datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos cuyo tipo de ingreso (IINGREPOR) es 1, interpretado como urgencias.; Solo se exponen ingresos con causa de ingreso (ICAUSAING) igual a 6.; Solo se incluyen pacientes pertenecientes a la entidad ''00130''.; El rango temporal está fijo en agosto de 2015 (2015-08-01 00:00:00 a 2015-08-31 23:59:00), no es parametrizable.; Cada ingreso se asocia a un único paciente vía IPCODPACI (INNER JOIN, excluye ingresos sin paciente maestro).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ingreso; urgencias; paciente; entidad/aseguradora; causa de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve ingresos cuando IFECHAING está entre 2015-08-01 y 2015-08-31 23:59, IINGREPOR=1 (urgencias), CODENTIDA=''00130'' e ICAUSAING=6.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'INGRESOS  URGENCIAS';
GO
