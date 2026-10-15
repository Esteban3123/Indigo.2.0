


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