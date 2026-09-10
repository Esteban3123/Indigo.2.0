CREATE TABLE [Payroll].[AuthorizationConceptGroup] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationConceptId] INT NOT NULL,
    [GroupId]                INT NOT NULL,
    [State]                  BIT NOT NULL,
    CONSTRAINT [PK_AuthorizationConceptGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationConceptGroup_AuthorizationConcept] FOREIGN KEY ([AuthorizationConceptId]) REFERENCES [Payroll].[AuthorizationConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationConceptGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación: 1=Activo, 0=Inactivo. Indica si la autorización del concepto está vigente en el grupo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Grupo (FK). Referencia al grupo de nómina o departamento asociado a la autorización de conceptos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Concepto de Autorización (FK). Referencia al concepto autorizado de nómina vinculado al grupo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Autorización de Conceptos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación entre Concepto de Autorización y Grupo. Clave primaria de la asociación de autorización por grupo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autorizacion de Conceptos x Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre conceptos de autorización de nómina y grupos de empleados o grupos de liquidación. Permite controlar qué conceptos salariales o de nómina están habilitados para cada grupo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup';
