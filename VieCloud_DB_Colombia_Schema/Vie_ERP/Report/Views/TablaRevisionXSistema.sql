

CREATE VIEW [Report].[TablaRevisionXSistema] as

select HC.numingres,HC.ipcodpaci,RS.idhchispaca,RS.idrsvariable,RS.valor,HC.FECHISPAC,VAL.VARIABLE
from 
(
	select * from RSVALORES202104
	UNION ALL	
	select * from RSVALORES202105
	UNION ALL											
	select * from RSVALORES202106
	UNION ALL											
	select * from RSVALORES202107
	UNION ALL											
	select * from RSVALORES202108
	UNION ALL											
	select * from RSVALORES202109
	UNION ALL											
	select * from RSVALORES202110
	UNION ALL											
	select * from RSVALORES202111
	UNION ALL											
	select * from RSVALORES202112
	UNION ALL											
	select * from RSVALORES202201
	UNION ALL											
	select * from RSVALORES202202
	UNION ALL											
	select * from RSVALORES202203
	UNION ALL											
	select * from RSVALORES202204
	UNION ALL											
	select * from RSVALORES202205
	UNION ALL											
	select * from RSVALORES202206
	UNION ALL											
	select * from RSVALORES202207
	UNION ALL											
	select * from RSVALORES202208
	UNION ALL											
	select * from RSVALORES202209
	UNION ALL											
	select * from RSVALORES202210
	UNION ALL											
	select * from RSVALORES202211
	UNION ALL											
	select * from RSVALORES202212
	UNION ALL											
	select * from RSVALORES202301
	UNION ALL									
	select * from RSVALORES202302
	UNION ALL
	select * from RSVALORES202303
	UNION ALL
	select * from RSVALORES202304
	UNION ALL									
	select * from RSVALORES202305
	UNION ALL									
	select * from RSVALORES202307
	UNION ALL									
	select * from RSVALORES202308
	UNION ALL									
	select * from RSVALORES202309
	UNION ALL									
	select * from RSVALORES202310
	UNION ALL									
	select * from RSVALORES202311
	UNION ALL									
	select * from RSVALORES202312
	UNION ALL									
	select * from RSVALORES202401
	--UNION ALL									
	--select * from RSVALORES202402
	--UNION ALL									
	--select * from RSVALORES202403
	--UNION ALL									
	--select * from RSVALORES202404
	--UNION ALL									
	--select * from RSVALORES202405
)  as RS
INNER JOIN HCHISPACA AS HC WITH(NOLOCK) ON HC.ID=RS.IDHCHISPACA INNER JOIN
dbo.RSVARIABLES VAL ON RS.IDRSVARIABLE=VAL.ID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de tipo reporting que consolida registros de tablas mensuales particionadas (`RSVALORES` desde abril 2021 hasta enero 2024) junto con datos de la historia clínica de hospitalización y las variables clínicas definidas. Permite consultar, por ingreso y paciente, los valores registrados en revisiones por sistema, asociando cada medición a su variable clínica correspondiente con fecha de atención. Sirve como fuente aplanada para reportes de seguimiento clínico longitudinal.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los valores de revisión por sistemas almacenados en tablas mensuales (abr-2021 a ene-2024) y los enriquece con datos del ingreso/historia clínica del paciente y la definición de la variable clínica asociada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todas las tablas particionadas mensualmente RSVALORESYYYYMM listadas deben existir y compartir el mismo esquema de columnas (UNION ALL con SELECT *).; Las claves IDHCHISPACA y IDRSVARIABLE deben corresponder a registros existentes en HCHISPACA y RSVARIABLES respectivamente, dado el uso de INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen valores de revisión cuyas tablas mensuales (RSVALORESYYYYMM) existen y están enlazadas en el UNION ALL; el rango cubierto va desde abril 2021 hasta enero 2024, excluyendo explícitamente junio 2023 (RSVALORES202306) y los meses posteriores a enero 2024.; Cada fila combina obligatoriamente un valor de revisión (RS) con su historia clínica (HCHISPACA) y su variable maestra (RSVARIABLES) mediante INNER JOIN; valores huérfanos sin HC o sin variable definida quedan excluidos.; La lectura de HCHISPACA se realiza con NOLOCK, por lo que pueden leerse filas no confirmadas (lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica de paciente; Ingreso hospitalario; Variables clínicas/de evaluación; Revisión por sistemas', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.TablaRevisionXSistema: Devuelve por cada valor de revisión: número de ingreso y código de paciente (HCHISPACA), id de HC, id y nombre de la variable (RSVARIABLES), valor registrado y fecha de la HC, uniendo todas las particiones mensuales de RSVALORES desde 202104 hasta 202401 (excepto 202306).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RSVALORES202104; dbo.RSVALORES202105; dbo.RSVALORES202106; dbo.RSVALORES202107; dbo.RSVALORES202108; dbo.RSVALORES202109; dbo.RSVALORES202110; dbo.RSVALORES202111; dbo.RSVALORES202112; dbo.RSVALORES202201; dbo.RSVALORES202202; dbo.RSVALORES202203; dbo.RSVALORES202204; dbo.RSVALORES202205; dbo.RSVALORES202206; dbo.RSVALORES202207; dbo.RSVALORES202208; dbo.RSVALORES202209; dbo.RSVALORES202210; dbo.RSVALORES202211; dbo.RSVALORES202212; dbo.RSVALORES202301; dbo.RSVALORES202302; dbo.RSVALORES202303; dbo.RSVALORES202304; dbo.RSVALORES202305; dbo.RSVALORES202307; dbo.RSVALORES202308; dbo.RSVALORES202309; dbo.RSVALORES202310 (+5 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'TablaRevisionXSistema';
GO
