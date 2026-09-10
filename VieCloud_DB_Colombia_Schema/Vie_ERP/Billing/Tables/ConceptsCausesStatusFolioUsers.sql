CREATE TABLE [Billing].[ConceptsCausesStatusFolioUsers] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptsCausesStatusFolioId] INT           NOT NULL,
    [UserId]                      INT           NOT NULL,
    [UserCode]                    VARCHAR (20)  NOT NULL,
    [FullNameUser]                VARCHAR (200) NULL,
    CONSTRAINT [PK_ConceptsCausesStatusFolioUsers_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptsCausesStatusFolioUsers_ConceptsCausesStatusFolio] FOREIGN KEY ([ConceptsCausesStatusFolioId]) REFERENCES [Billing].[ConceptsCausesStatusFolio] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario (profesional de salud, administrativo o gestor) que participa en la gestión del concepto, causa o estado del folio de facturación. Varchar(200), permite búsqueda por nombre del responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'FullNameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el nombre completo del usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'FullNameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'FullNameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (identificador alfanumérico, cédula o matrícula profesional) asignado en el sistema para identificar al profesional o gestor de facturación. Varchar(20), clave de búsqueda por usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (Int) del usuario en la tabla de usuarios del sistema. Referencia al usuario responsable de la gestión del concepto, causa o estado del folio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causación, concepto o estado del folio de facturación (FK a Billing.ConceptsCausesStatusFolio). Vincula al registro específico de facturación, RIPS, glosa o concepto a un usuario responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'ConceptsCausesStatusFolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla Conceptos Causas de Estado Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'ConceptsCausesStatusFolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'ConceptsCausesStatusFolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (Int, IDENTITY) de la relación entre usuario y concepto de causación/estado del folio. Clave primaria que registra la asignación del profesional o gestor a un concepto, causa o estado en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla Conceptos Causas de Estado Folio Usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los usuarios que han interactuado o gestionado los estados de conceptos y causas asociados a folios de facturación. Permite auditar qué usuario realizó cada cambio de estado en un folio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers';
