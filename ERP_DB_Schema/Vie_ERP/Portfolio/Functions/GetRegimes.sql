
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

	-- Para clientes no cubiertos por la lista anterior, leer dinamicamente
	-- desde la estructura contable de los grupos de atencion
	INSERT INTO @Regimes
	SELECT DISTINCT
		ma.Number,
		CASE cg.EntityType
			WHEN 1  THEN 'Contributivo'
			WHEN 2  THEN 'EPS Subsidiado'
			WHEN 3  THEN 'Vinculados Municipios'
			WHEN 4  THEN 'Vinculados - Departamentos'
			WHEN 5  THEN 'Arl Riesgos Laborales'
			WHEN 6  THEN 'Medicina Prepagada'
			WHEN 7  THEN 'IPS Privada'
			WHEN 8  THEN 'IPS Publicas'
			WHEN 9  THEN 'Regimen Especial'
			WHEN 10 THEN 'Accidentes de Transito'
			WHEN 11 THEN 'FOSYGA'
			WHEN 12 THEN 'Otros'
			WHEN 13 THEN 'Aseguradoras'
			WHEN 99 THEN 'Particulares'
			ELSE NULL
		END
	FROM Contract.ContractAccountingStructure cas WITH (NOLOCK)
	INNER JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
	INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = cas.AccountWithoutRadicateId
	WHERE cas.AccountWithoutRadicateId IS NOT NULL
		AND NOT EXISTS (SELECT 1 FROM @Regimes r WHERE r.AccountNumber = ma.Number)

	RETURN
END