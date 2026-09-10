

/*******************************************************************************************************************
Nombre: [Report].[TablaAntecedentes]
Tipo:Vista
Observacion:vista, que cumple la función de unir tablas de antecedentes por mes y año 
Profesional: Nilsson Miguel Galindo Lopez
Fecha:28-02-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:
Fecha:
Ovservaciones:
--------------------------------------
Version 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/
CREATE VIEW [Report].[TablaAntecedentes] AS

WITH CTE_ANTECEDENTES AS 
 (
	SELECT * FROM ANTVALORES202104
	UNION ALL
	SELECT * FROM ANTVALORES202105
	UNION ALL
	SELECT * FROM ANTVALORES202106
	UNION ALL
	SELECT * FROM ANTVALORES202107
	UNION ALL
	SELECT * FROM ANTVALORES202108
	UNION ALL
	SELECT * FROM ANTVALORES202109
	UNION ALL
	SELECT * FROM ANTVALORES202110
	UNION ALL
	SELECT * FROM ANTVALORES202111
	UNION ALL
	SELECT * FROM ANTVALORES202112
	UNION ALL
	SELECT * FROM ANTVALORES202201
	UNION ALL
	SELECT * FROM ANTVALORES202202
	UNION ALL
	SELECT * FROM ANTVALORES202203
	UNION ALL
	SELECT * FROM ANTVALORES202204
	UNION ALL
	SELECT * FROM ANTVALORES202205
	UNION ALL
	SELECT * FROM ANTVALORES202206
	UNION ALL
	SELECT * FROM ANTVALORES202207
	UNION ALL
	SELECT * FROM ANTVALORES202208
	UNION ALL
	SELECT * FROM ANTVALORES202209
	UNION ALL
	SELECT * FROM ANTVALORES202210
	UNION ALL
	SELECT * FROM ANTVALORES202211
	UNION ALL
	SELECT * FROM ANTVALORES202212
	UNION ALL
	SELECT * FROM ANTVALORES202301
	UNION ALL
	SELECT * FROM ANTVALORES202302
	UNION ALL
	SELECT * FROM ANTVALORES202303
	UNION ALL
	SELECT * FROM ANTVALORES202304
	UNION ALL
	SELECT * FROM ANTVALORES202305
	UNION ALL
	SELECT * FROM ANTVALORES202306
	UNION ALL
	SELECT * FROM ANTVALORES202307
	UNION ALL
	SELECT * FROM ANTVALORES202308
	UNION ALL
	SELECT * FROM ANTVALORES202309
	UNION ALL
	SELECT * FROM ANTVALORES202310
	UNION ALL
	SELECT * FROM ANTVALORES202311
	UNION ALL
	SELECT * FROM ANTVALORES202312
	UNION ALL
	SELECT * FROM ANTVALORES202401
	UNION ALL
	SELECT * FROM ANTVALORES202402
	--UNION ALL
	--SELECT * FROM ANTVALORES202403
	--UNION ALL
	--SELECT * FROM ANTVALORES202404
	--UNION ALL
	--SELECT * FROM ANTVALORES202405
 )

SELECT 
 ANT.*, VAL.VARIABLE,HIS.FECHISPAC,HIS.IPCODPACI
FROM 
 CTE_ANTECEDENTES ANT 
 INNER JOIN dbo.ANTVARIABLES VAL ON ANT.IDANTVARIABLE=VAL.ID 
 INNER JOIN dbo.HCHISPACA HIS ON ANT.IDHCHISPACA=HIS.ID
--GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida registros de valores de antecedentes clínicos almacenados en tablas particionadas mensualmente (desde abril de 2021 hasta febrero de 2024) mediante UNION ALL. Enriquece cada registro con el nombre de la variable clínica correspondiente (antecedentes familiares, personales o patológicos) y los datos de la historia clínica asociada, incluyendo fecha de atención y código de paciente. Está orientada al consumo en reporting longitudinal de antecedentes clínicos del paciente a lo largo del tiempo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los valores de antecedentes clínicos almacenados en tablas mensuales particionadas (ANTVALORESYYYYMM) y los enriquece con la variable clínica y los datos de la historia clínica/paciente asociada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir las tablas mensuales ANTVALORESYYYYMM desde 202104 hasta 202402 con esquema compatible para UNION ALL.; Cada registro de antecedente debe tener un IDANTVARIABLE existente en dbo.ANTVARIABLES y un IDHCHISPACA existente en dbo.HCHISPACA (INNER JOIN obliga a coincidencia).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen antecedentes cuyo IDANTVARIABLE existe en ANTVARIABLES y cuyo IDHCHISPACA existe en HCHISPACA (los huérfanos son excluidos por los INNER JOIN).; El rango temporal cubierto está acotado a las particiones mensuales activas: abril/2021 a febrero/2024; los meses 202403–202405 están comentados y por tanto excluidos.; Al usar UNION ALL no se eliminan duplicados entre particiones mensuales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes clínicos del paciente; Variables de antecedentes (familiares/personales/patológicos); Historia clínica del paciente; Particionamiento mensual de antecedentes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.TablaAntecedentes: Devuelve la unión (UNION ALL) de las tablas mensuales ANTVALORES desde 202104 hasta 202402, cruzada con ANTVARIABLES (por IDANTVARIABLE=ID) y HCHISPACA (por IDHCHISPACA=ID), exponiendo VARIABLE, FECHISPAC e IPCODPACI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ANTVALORES202104; dbo.ANTVALORES202105; dbo.ANTVALORES202106; dbo.ANTVALORES202107; dbo.ANTVALORES202108; dbo.ANTVALORES202109; dbo.ANTVALORES202110; dbo.ANTVALORES202111; dbo.ANTVALORES202112; dbo.ANTVALORES202201; dbo.ANTVALORES202202; dbo.ANTVALORES202203; dbo.ANTVALORES202204; dbo.ANTVALORES202205; dbo.ANTVALORES202206; dbo.ANTVALORES202207; dbo.ANTVALORES202208; dbo.ANTVALORES202209; dbo.ANTVALORES202210; dbo.ANTVALORES202211; dbo.ANTVALORES202212; dbo.ANTVALORES202301; dbo.ANTVALORES202302; dbo.ANTVALORES202303; dbo.ANTVALORES202304; dbo.ANTVALORES202305; dbo.ANTVALORES202306; dbo.ANTVALORES202307; dbo.ANTVALORES202308; dbo.ANTVALORES202309 (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaAntecedentes';
GO
