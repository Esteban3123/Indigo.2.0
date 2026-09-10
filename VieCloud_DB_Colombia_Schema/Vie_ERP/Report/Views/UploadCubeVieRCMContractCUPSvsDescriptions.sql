
create view [Report].[UploadCubeVieRCMContractCUPSvsDescriptions] AS
--CREATE PROCEDURE [Contract].[SP_CUPS_DESCRIPCIONES]
--as

	select DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CUPS.Code AS 'CODIGO CUPS',--[CodigoCUP],
		CUPS.Description AS 'DESCRIPCION CUPS',--[DescripcionCUP],
		CD.Code AS 'CODIGO DESCRIPCION RELACIONADA',--[CodigoDescripcionRelacionada],
		CD.Name AS 'DESCRIPCION RELACIONADA',--[DescripcionRelacionada]
	    CAST(GETDATE() AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	from Contract .CUPSEntityContractDescriptions AS D 
	INNER JOIN Contract.CUPSEntity as CUPS ON CUPS.ID =D.CUPSEntityId 
	INNER JOIN Contract.ContractDescriptions AS CD ON CD.Id =D.ContractDescriptionId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para cubos de datos que aplane la relación entre códigos CUPS y las descripciones contractuales asociadas a ellos. Consolida, por empresa (identificada con el nombre de la base de datos), cada procedimiento CUPS con sus descripciones de contrato vinculadas, incluyendo código y nombre de ambas entidades. Está orientada a alimentar procesos de carga a cubos OLAP del módulo RCM (Revenue Cycle Management), incorporando marca de fecha de consulta y fecha de última actualización en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación entre códigos CUPS y las descripciones de contrato vinculadas, para alimentar un cubo de reportes con identificación de la base de datos origen y marca temporal de extracción.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de vínculo en Contract.CUPSEntityContractDescriptions con referencias válidas a Contract.CUPSEntity y Contract.ContractDescriptions (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen CUPS que tengan al menos una descripción de contrato asociada (INNER JOIN sobre la tabla puente CUPSEntityContractDescriptions).; ID_COMPANY se trunca a VARCHAR(9) a partir del nombre de la base de datos.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Se eliminan duplicados mediante SELECT DISTINCT.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Descripciones de contrato; Compañía/empresa (ID_COMPANY)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMContractCUPSvsDescriptions: Devuelve filas DISTINCT con código y descripción CUPS junto a su descripción de contrato relacionada, agregando el nombre de la BD actual (DB_NAME), la fecha de búsqueda (GETDATE como DATE) y la última actualización ajustada a zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntityContractDescriptions; Contract.CUPSEntity; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractCUPSvsDescriptions';
GO
