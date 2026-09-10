

CREATE VIEW [dbo].[VKardexMedicineSupplier]
AS
	SELECT
		k.NUMCONSEC                   AS [Row],
		k.CODPRODUC                   AS Codigo,
		k.IPCODPACI                   AS CodigoPaciente,
		k.NUMINGRES                   AS Ingreso,
		k.FECREGKAR                   AS Fecha,
		k.DESMOVPRO                   AS Movimiento,
		k.CANPRODUCT                  AS Cantidad,
		k.TIPREGIST					  AS TipoKardex,
		k.TIPORIREG                   AS Origen,
		CASE 
			WHEN (TIPORIREG>0 AND TIPORIREG<6) OR (TIPORIREG = 20 AND k.TIPREGIST = 1) THEN '1' 
			WHEN (TIPORIREG>10 AND TIPORIREG < 20) OR (TIPORIREG = 20 AND k.TIPREGIST = 2) THEN '2' 
			WHEN TIPORIREG>5 AND TIPORIREG<9 THEN '3' 
			WHEN TIPORIREG>8 AND TIPORIREG<11 THEN '4' 
		END							  AS Tipo,
		RTRIM(fu.UFUDESCRI)           AS UFUDESCRI,
		k.CONSECFAR                   AS ConsLog,
		p.IPNOMCOMP                   AS PersonName
	FROM dbo.HCKARDPAC k WITH (NOLOCK)
	JOIN dbo.INUNIFUNC fu WITH (NOLOCK) ON fu.UFUCODIGO = k.UFUCODIGO
	JOIN dbo.INPACIENT p WITH (NOLOCK)  ON p.IPCODPACI = k.IPCODPACI
	WHERE k.TIPORIREG NOT IN (21, 22)
	  AND k.IdDetailPhysicalCUM IS NULL         -- << excluir los que se relacionan con HCFISIPRO

	UNION ALL

	-- Bloque 2: recién nacido (NUMINGRES del padre)
	SELECT
		k.NUMCONSEC                   AS [Row],
		k.CODPRODUC                   AS Codigo,
		k.IPCODPACI                   AS CodigoPaciente,
		INGMH.NUMINGRES               AS Ingreso,           -- mapeo RN → ingreso del padre
		k.FECREGKAR                   AS Fecha,
		k.DESMOVPRO                   AS Movimiento,
		k.CANPRODUCT                  AS Cantidad,
		k.TIPREGIST					  AS TipoKardex,
		k.TIPORIREG                   AS Origen,
		CASE 
			WHEN (TIPORIREG>0 AND TIPORIREG<6) OR (TIPORIREG = 20 AND k.TIPREGIST = 1) THEN '1' 
			WHEN (TIPORIREG>10 AND TIPORIREG < 20) OR (TIPORIREG = 20 AND k.TIPREGIST = 2) THEN '2' 
			WHEN TIPORIREG>5 AND TIPORIREG<9 THEN '3' 
			WHEN TIPORIREG>8 AND TIPORIREG<11 THEN '4' 
		END							  AS Tipo,
		RTRIM(fu.UFUDESCRI)           AS UFUDESCRI,
		k.CONSECFAR                   AS ConsLog,
		'Hijo ' + CAST(rn.NUMHIJREG AS varchar(20)) AS PersonName
	FROM dbo.HCKARDPAC k          WITH (NOLOCK)
	JOIN dbo.INUNIFUNC fu         WITH (NOLOCK) ON fu.UFUCODIGO        = k.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH WITH (NOLOCK) ON INGMH.NUMINGRESHIJO = k.NUMINGRES AND INGMH.TIPREGISTRO = 1
	JOIN dbo.HCRECINAC rn         WITH (NOLOCK) ON rn.NUMINGRESHIJO    = INGMH.NUMINGRESHIJO
	JOIN dbo.ADINGRESO dgo        WITH (NOLOCK) ON dgo.NUMINGRES       = INGMH.NUMINGRESHIJO
	WHERE dgo.IESTADOIN = 'C'
	  AND k.IdDetailPhysicalCUM IS NULL;       -- << excluir los que se relacionan con HCFISIPRO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los movimientos del kárdex de medicamentos y productos farmacéuticos dispensados a pacientes durante sus ingresos hospitalarios, permitiendo rastrear entregas, devoluciones y ajustes de inventario por paciente, unidad funcional y fecha. Combina dos bloques: el primero cubre pacientes regulares (excluyendo movimientos asociados a fisioterapia/procedimientos físicos y tipos de origen 21 y 22), y el segundo cubre recién nacidos mapeando sus movimientos al ingreso de la madre, identificando al neonato como ''Hijo N''. Integra el kárdex (HCKARDPAC) con el catálogo de unidades funcionales (INUNIFUNC), el maestro de pacientes (INPACIENT) y las tablas de vínculo madre-recién nacido (HCINGRESORECNAC, HCRECINAC). Se usa para reportería de dispensación de medicamentos, auditoría de suministros farmacéuticos y facturación de insumos por ingreso, incluyendo el caso especial de recién nacidos cuyo consumo se carga al ingreso de la madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VKardexMedicineSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VKardexMedicineSupplier';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los movimientos de kárdex farmacéutico por paciente mostrando datos del paciente o, en caso de recién nacido, asociándolos al ingreso de la madre con identificación del hijo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento de kárdex debe tener una unidad funcional válida en INUNIFUNC.; Para el bloque principal, el paciente debe existir en INPACIENT.; Para el bloque de recién nacido, debe existir vínculo madre-hijo en HCINGRESORECNAC con TIPREGISTRO = 1 y registro clínico en HCRECINAC.; El ingreso del recién nacido en ADINGRESO debe estar en estado ''C''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca expone movimientos de kárdex con TIPORIREG 21 o 22.; Nunca expone movimientos asociados a productos físicos CUM (IdDetailPhysicalCUM IS NOT NULL).; Los movimientos de recién nacidos solo se reportan si el ingreso del hijo está en estado ''C'' (cerrado).; El mapeo madre-hijo solo aplica cuando HCINGRESORECNAC.TIPREGISTRO = 1.; Cada movimiento se clasifica en una de cuatro categorías según TIPORIREG y TIPREGIST.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kárdex de medicamentos; Paciente; Unidad funcional; Recién nacido; Ingreso hospitalario; Vínculo madre-hijo; Movimiento farmacéutico; Origen del movimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas de kárdex excluyendo TIPORIREG IN (21,22) y registros con IdDetailPhysicalCUM no nulo (los relacionados con HCFISIPRO).; [RETURN_RESULT] N/A: Para movimientos de recién nacido, reemplaza el NUMINGRES del hijo por el NUMINGRES del registro padre (mapeo vía HCINGRESORECNAC) y la columna PersonName muestra ''Hijo '' + NUMHIJREG en lugar del nombre del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPORIREG entre 1 y 5, o TIPORIREG=20 con TIPREGIST=1 → Clasifica el movimiento como Tipo ''1''; si TIPORIREG entre 11 y 19, o TIPORIREG=20 con TIPREGIST=2 → Clasifica el movimiento como Tipo ''2''; si TIPORIREG entre 6 y 8 → Clasifica el movimiento como Tipo ''3''; si TIPORIREG entre 9 y 10 → Clasifica el movimiento como Tipo ''4''; si El movimiento corresponde a un ingreso de recién nacido (existe en HCINGRESORECNAC como hijo) y el ingreso está en estado ''C'' → Se incluye con el ingreso reasignado al padre y el PersonName como ''Hijo N'' else Se incluye en el bloque principal con datos del paciente desde INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCKARDPAC; dbo.INUNIFUNC; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplier';
GO
