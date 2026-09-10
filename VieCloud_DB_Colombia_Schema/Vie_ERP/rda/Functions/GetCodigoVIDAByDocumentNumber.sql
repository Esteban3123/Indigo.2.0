CREATE FUNCTION [rda].[GetCodigoVIDAByDocumentNumber] (@DocumentNumber VARCHAR(25))
RETURNS VARCHAR(36)
AS
BEGIN
	DECLARE @codigoVIDA VARCHAR(36);

	IF NULLIF(LTRIM(RTRIM(@DocumentNumber)), '') IS NULL
		RETURN NULL;

	IF SCHEMA_ID(N'rda') IS NULL
		RETURN NULL;

	IF OBJECT_ID(N'rda.Submission', N'U') IS NULL
		RETURN NULL;

	SELECT TOP (1)
		@codigoVIDA = CAST(JSON_VALUE(CAST(s.Payload AS NVARCHAR(MAX)), '$.entry[0].resource.id') AS VARCHAR(36))
	FROM [rda].[Submission] s WITH (NOLOCK)
	WHERE LTRIM(RTRIM(s.DocumentNumber)) = LTRIM(RTRIM(@DocumentNumber))
		AND ISJSON(CAST(s.Payload AS NVARCHAR(MAX))) = 1
		AND NULLIF(JSON_VALUE(CAST(s.Payload AS NVARCHAR(MAX)), '$.entry[0].resource.id'), '') IS NOT NULL
	ORDER BY COALESCE(
		TRY_CONVERT(DATETIMEOFFSET(7), JSON_VALUE(CAST(s.Payload AS NVARCHAR(MAX)), '$.entry[0].response.lastModified')),
		TRY_CONVERT(DATETIMEOFFSET(7), JSON_VALUE(CAST(s.Payload AS NVARCHAR(MAX)), '$.entry[0].resource.meta.lastUpdated'))
	) DESC;

	RETURN @codigoVIDA;
END
GO
