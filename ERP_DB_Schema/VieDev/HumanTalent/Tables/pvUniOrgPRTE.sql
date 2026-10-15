CREATE TABLE [HumanTalent].[PVUniOrgPRTE] (
    [pvUnitOrgId]       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [unitOrgId]         INT          NOT NULL,
    [positionRId]       INT          NOT NULL,
    [responsibleTypeId] INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    CONSTRAINT [PK__pvUniOrg__63F50C24F0E7B1B5] PRIMARY KEY CLUSTERED ([pvUnitOrgId] ASC),
    CONSTRAINT [FK_OrganizationaUnit] FOREIGN KEY ([unitOrgId]) REFERENCES [HumanTalent].[OrganizationalUnit] ([Id]),
    CONSTRAINT [fk_responsibleType] FOREIGN KEY ([responsibleTypeId]) REFERENCES [StaffPick].[ResponsibleType] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVUniOrgPRTE] NOCHECK CONSTRAINT [FK_OrganizationaUnit];


GO
ALTER TABLE [HumanTalent].[PVUniOrgPRTE] NOCHECK CONSTRAINT [fk_responsibleType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de responsable', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'responsibleTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición del responsable', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'positionRId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad organizacional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'unitOrgId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad organizacional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVUniOrgPRTE', @level2type = N'COLUMN', @level2name = N'pvUnitOrgId';

