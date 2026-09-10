CREATE VIEW [dbo].[VIE_AD_Treasury_Maestro_Bancos]
AS
SELECT        b.Id, b.Code AS Código, b.Name AS Nombre, T.Nit, T.Name AS Tercero, b.AchCode AS CODACH, b.CenitCode AS [Código CENIT], b.CenitVerification AS [Dígito verificación CENIT], 
                         CASE WHEN b.BankFileCode = '001' THEN 'Bancolombia' WHEN b.BankFileCode = '002' THEN 'Av Villas' WHEN b.BankFileCode = '003' THEN 'Banco Popular' WHEN b.BankFileCode = '004' THEN 'Banco Occidente'
                          WHEN b.BankFileCode = '005' THEN 'Banco BBVA' WHEN b.BankFileCode = '006' THEN 'Banco Davivienda' END AS [Codigo de archivo de banco], b.State AS Estado
FROM            Payroll.Bank AS b WITH (nolock) INNER JOIN
                         Common.ThirdParty AS T WITH (NOLOCK) ON T.Id = b.ThirdPartyId
WHERE        (b.State = '1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de bancos activos utilizados en los procesos de tesorería y nómina. Combina la información de cada entidad bancaria (código interno, nombre, códigos de integración para transferencias electrónicas ACH y CENIT, dígito de verificación CENIT y código de archivo bancario para bancos como Bancolombia, Davivienda, BBVA, entre otros) con los datos del tercero asociado a cada banco (NIT y razón social), filtrando únicamente los bancos en estado activo. Sirve como referencia para la configuración de pagos electrónicos a empleados y proveedores, y para la generación de archivos de dispersión bancaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_Treasury_Maestro_Bancos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el maestro de bancos activos enriquecido con datos del tercero asociado y la descripción legible del código de archivo bancario para uso de tesorería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe relación válida entre el banco y un tercero mediante ThirdPartyId.; El banco debe tener estado activo (''1'') para ser expuesto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen bancos con State = ''1'' (activos).; Cada banco expuesto debe tener un tercero asociado existente (INNER JOIN con ThirdParty).; El código de archivo bancario solo reconoce seis entidades financieras colombianas (001-006); cualquier otro valor se devuelve como NULL.; Lectura sin bloqueo (NOLOCK) sobre las tablas base.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Banco; Tercero; NIT; Código ACH; Código CENIT; Dígito de verificación CENIT; Archivo de banco; Tesorería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando b.State = ''1'' y existe tercero ligado, retorna el banco con identificación (NIT/Tercero), códigos ACH/CENIT y descripción del archivo bancario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BankFileCode = ''001'' → Etiqueta como ''Bancolombia''; si BankFileCode = ''002'' → Etiqueta como ''Av Villas''; si BankFileCode = ''003'' → Etiqueta como ''Banco Popular''; si BankFileCode = ''004'' → Etiqueta como ''Banco Occidente''; si BankFileCode = ''005'' → Etiqueta como ''Banco BBVA''; si BankFileCode = ''006'' → Etiqueta como ''Banco Davivienda'' else NULL (códigos no mapeados quedan sin descripción)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Bank; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Treasury_Maestro_Bancos';
GO
