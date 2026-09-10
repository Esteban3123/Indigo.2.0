
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-23
-- Description:	Obtiene el regimen a partir de la cuenta contable
-- =============================================
CREATE FUNCTION [Portfolio].[GetRegimes]
(
)
RETURNS @Regimes TABLE 
(
	AccountNumber VARCHAR(20),
	RegimenName VARCHAR(MAX)
)
AS
BEGIN

	INSERT INTO @Regimes VALUES
		/****** ONCOLOGOS ********/
		('13010501', 'Contributivo'),
		('13010601', 'EPS Subsidiado'),
		('13016501', 'Vinculados Municipios'),
		('13015001', 'Arl Riesgos Laborales'),
		('13011501', 'Medicina Prepagada'),
		('13011001', 'IPS Privada'),
		('13014001', 'Regimen Especial'),
		('13014501', 'Regimen Especial'),		
		('13659501', 'Otras Cuentas por Cobrar'),
		('13016001', 'Aseguradoras'),
		('13012501','Particulares - Personas Naturales'),
		('13013001','Particulares - Personas Juridicas'),
		('13023001','Particulares - Personas Juridicas'),

		/****** HOSPITAL MONCALEANO********/
		('13190101', 'Contributivo'),
		('13190201', 'Contributivo'),
		('1385090101', 'Contributivo'),
		('13190301', 'EPS Subsidiado'),
		('13190401', 'EPS Subsidiado'),
		('1385090301', 'EPS Subsidiado'),
		('13190501', 'Medicina Prepagada'),
		('13190601', 'Medicina Prepagada'),
		('1385090501', 'Medicina Prepagada'),
		('13191401', 'Regimen Especial'),
		('13191501', 'Regimen Especial'),
		('1385091001', 'Regimen Especial'),
		('1385090401', 'IPS Privada'),
		('13190801', 'IPS Privada'),
		('13190901', 'IPS Privada'),
		('13191001', 'IPS Publicas'),
		('13191101', 'IPS Publicas'),
		('1385090901', 'IPS Publicas'),
		('13191601', 'Particulares'),
		('1385090701','Particulares'),
	   	('13191701', 'Accidentes de Transito'),
		('13191801', 'Accidentes de Transito'),
		('1385091401', 'Accidentes de Transito'),
		('13191901', 'Vinculados Municipios'),
	    ('13192101', 'Atencion con Cargo Sub a la oferta'),
		('13192201', 'Vinculados - Departamentos'),
		('1385091101', 'Atencion con Cargo Sub a la oferta'),
		('1385091301', 'Cuotas de Recuperacion'),
		('13192901', 'Cuotas de Recuperacion'),
	    ('13192301', 'Arl Riesgos Laborales'),
		('13192401', 'Arl Riesgos Laborales'),
		('1385091201', 'Arl Riesgos Laborales'),
		('13192701', 'FOSYGA'),
		('13192801', 'FOSYGA'),
		('1385091001', 'FOSYGA'),
		('13171601', 'Otras Cuentas por Cobrar'),
		('13849001', 'Otras Cuentas por Cobrar'),
		('13849004', 'Otras Cuentas por Cobrar'),
		('1385091801', 'Otras Cuentas por Cobrar'),
		('1319900101', 'Otras Cuentas por Cobrar'),
		('1385900101', 'No Operativo'),
		('13849012', 'No Operativo'),
		('13191201','Aseguradoras'),
		('13241601','Otros'),
		('13199002','Otros'),
		('13192102','ET Vinculados Municipios'),
		('13191604','Otros'),
			
		/****** ORIGINAL *****/
		('14090103', 'Contributivo'),
		('14090304', 'EPS Subsidiado'),
		('14090401', 'Servicio IPS Privada'),
		('14090501', 'Medicina Prepagada'),
		('14090601', 'Compañias Aseguradoras'),
		('14090701', 'Particulares'),
		('14090901', 'Servicio IPS Publicas'),
		('14091004', 'Regimen Especial'),
		('14091102', 'Vinculados - Departamentos'),
		('14091103', 'Vinculados Municipios'),
		('14091201', 'Arl Riesgos Profesionales'),
		('14091403', 'Accidentes de Transito'),
		('14090201', 'Otras Cuentas por Cobrar')
			   		 	  	  	   	
	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que retorna el catálogo de regímenes de salud asociados a cada cuenta contable de cartera. Mapea números de cuenta del plan contable (cuentas por cobrar) al nombre del régimen correspondiente: Contributivo, Subsidiado, Medicina Prepagada, Régimen Especial, Particulares, ARL, Accidentes de Tránsito, FOSYGA, Vinculados, entre otros. Cubre múltiples unidades de negocio u hospitales (Oncólogos, Hospital Moncaleano y configuración original), permitiendo clasificar la cartera según el tipo de pagador o régimen. Se utiliza en reportes financieros y de cartera para agrupar cuentas por cobrar por régimen, facilitando el análisis de deuda, facturación y conciliación contable por tipo de asegurador o pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetRegimes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetRegimes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función tabular que mapea números de cuenta contable a su régimen/concepto de cartera correspondiente, consolidando catálogos de tres orígenes institucionales.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetRegimes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo está agrupado por origen institucional: ONCÓLOGOS (cuentas 130xxx), HOSPITAL MONCALEANO (cuentas 1319xxxx, 1385xxxx, 1317xxxx, 1384xxxx, 1324xxxx) y ORIGINAL (cuentas 1409xxxx).; Una misma denominación de régimen puede asociarse a múltiples números de cuenta (p.ej. ''Contributivo'' aplica a 13010501, 13190101, 13190201, 1385090101, 14090103).; La cuenta ''1385091001'' aparece duplicada con dos regímenes distintos (''Regimen Especial'' y ''FOSYGA''), produciendo dos filas para el mismo AccountNumber.; Los nombres de régimen se manejan con variantes textuales no normalizadas (p.ej. ''Arl Riesgos Laborales'' vs ''Arl Riesgos Profesionales''; ''IPS Privada'' vs ''Servicio IPS Privada'').', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetRegimes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Régimen de salud; Cuenta contable; Cartera; Contributivo; EPS Subsidiado; Medicina Prepagada; ARL Riesgos Laborales; FOSYGA; Accidentes de Tránsito; IPS Pública; IPS Privada; Régimen Especial; Vinculados; Cuotas de Recuperación; Aseguradoras; Particulares', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetRegimes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Regimes: Inserta un catálogo estático de pares (AccountNumber, RegimenName) que clasifica cuentas contables en regímenes como Contributivo, EPS Subsidiado, Medicina Prepagada, ARL, FOSYGA, Particulares, IPS Públicas/Privadas, Régimen Especial, Vinculados, Accidentes de Tránsito, entre otros.; [RETURN_RESULT] @Regimes: Retorna la tabla @Regimes con la totalidad de mapeos cuenta→régimen, sin filtrado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetRegimes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetRegimes';
GO
