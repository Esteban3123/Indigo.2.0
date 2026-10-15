CREATE VIEW [GeneralLedger].[ViewEntityNameDescriptions]
AS
	SELECT EntityName, Description
	FROM Common.GetEntityNameDescriptions()