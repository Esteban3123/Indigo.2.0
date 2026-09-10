

CREATE VIEW [GeneralLedger].[ViewHomologationAccount]
as
(

select  h.Id,
m.Id as OfficalMainAccountId,
CONCAT(m.Number,' - ',m.Name) as NumberNameOfficialMainAccount,
h.MainAccountId, 
CONCAT(mh.Number,' - ',mh.Name) as NumberNameMainAccount,
b.Id as LegalBookId,
mh.LegalBookId as HomologationLegalBookId,
m.HandlesCostCenter as HandlesCostCenterOfficialMainAccount,
m.HandlesThirdParty as HandlesThirdPartyOfficialMainAccount,
h.CreationUser,
h.CreationDate,
h.ModificationUser,
h.ModificationDate
from GeneralLedger.LegalBook b with (nolock) 
inner join GeneralLedger.MainAccounts m with (nolock)  on m.LegalBookId = b.Id
left join GeneralLedger.HomologationAccount h  with (nolock) on h.OfficialMainAccountId = m.Id
left join GeneralLedger.MainAccounts mh  with (nolock) on mh.id = h.MainAccountId
where m.AllowsMovement = 1 and m.Status = 1

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra la homologación de cuentas contables: relaciona cada cuenta oficial del libro legal con su cuenta equivalente (homologada) en otro plan de cuentas. Para cada cuenta oficial activa y que permite movimientos, expone su número y nombre concatenados, y si existe una homologación, también muestra el número y nombre de la cuenta homologada junto con el libro legal y centro de costos al que pertenece. Sirve para reportería contable y procesos de migración o equivalencia entre planes de cuentas, permitiendo identificar qué cuenta local corresponde a cada cuenta oficial del libro legal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewHomologationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewHomologationAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el mapeo entre cuentas contables oficiales y sus cuentas homólogas del plan de cuentas, mostrando datos descriptivos de ambas y del libro contable, restringido a cuentas oficiales activas que admiten movimiento.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas oficiales deben estar activas (Status = 1) y permitir movimiento (AllowsMovement = 1) para ser incluidas en el resultado.; Cada cuenta principal debe estar asociada a un libro legal (LegalBook).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas oficiales con AllowsMovement = 1 y Status = 1.; El nombre concatenado de cuenta se construye como ''Número - Nombre'' tanto para la cuenta oficial como para la homologada.; La cuenta homologada (MainAccountId) puede pertenecer a un LegalBook distinto al de la cuenta oficial, exponiéndose ambos identificadores por separado.; Cuentas oficiales sin homologación aparecen con campos de homologación en NULL.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan único de cuentas (PUC); Cuenta contable oficial; Homologación de cuentas; Libro contable / Libro legal; Centro de costo; Manejo de terceros; Cuentas que permiten movimiento', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.HomologationAccount: Devuelve homologaciones de cuentas oficiales con sus equivalencias; si no existe homologación para una cuenta oficial, igualmente se retorna la cuenta oficial con campos de homologación nulos (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewHomologationAccount';
GO
