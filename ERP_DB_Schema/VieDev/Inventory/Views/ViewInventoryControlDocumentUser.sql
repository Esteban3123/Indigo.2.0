CREATE VIEW [Inventory].[ViewInventoryControlDocumentUser]
AS
	SELECT	ICD.DocumentType,
			ICD.DocumentNumber, 
			ICD.DocumentUser, 
			P.Fullname AS UserName 
	FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) 
	LEFT JOIN Security.[User] U ON ICD.DocumentUser = U.UserCode 
	LEFT JOIN Security.Person P ON U.IdPerson = P.Id