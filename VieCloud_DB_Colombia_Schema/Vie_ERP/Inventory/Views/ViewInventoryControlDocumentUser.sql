CREATE VIEW [Inventory].[ViewInventoryControlDocumentUser]
AS
	SELECT	ICD.DocumentType,
			ICD.DocumentNumber, 
			ICD.DocumentUser, 
			P.Fullname AS UserName 
	FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) 
	LEFT JOIN Security.[User] U ON ICD.DocumentUser = U.UserCode 
	LEFT JOIN Security.Person P ON U.IdPerson = P.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra los documentos de control de inventario (entradas, salidas, ajustes) junto con el nombre completo del usuario responsable de cada movimiento. Combina el registro de documentos de inventario con la información de seguridad para traducir el código de usuario al nombre legible de la persona. Útil para reportes y auditorías donde se necesite identificar quién generó o autorizó cada documento de inventario, mostrando el tipo de documento, número de documento, código de usuario y nombre del responsable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewInventoryControlDocumentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewInventoryControlDocumentUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los documentos de control de inventario junto con el nombre completo de la persona asociada al usuario que los registró.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La relación usuario→persona se establece mediante User.IdPerson = Person.Id.; El documento referencia al usuario por UserCode.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se mantienen todos los documentos de control de inventario aunque no exista usuario o persona asociada (LEFT JOIN).; El nombre mostrado del usuario corresponde al Fullname de la persona vinculada al usuario que registró el documento.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento de control de inventario; Usuario; Persona', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryControlDocument: Devuelve una fila por documento de control de inventario con tipo, número, código de usuario y nombre completo; si no hay usuario o persona vinculada, UserName queda en NULL por el LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryControlDocument; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewInventoryControlDocumentUser';
GO
