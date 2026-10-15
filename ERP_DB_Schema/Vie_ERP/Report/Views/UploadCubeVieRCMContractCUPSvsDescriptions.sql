
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