CREATE TABLE [HumanTalent].[PVOrganizationalUnitTree] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrganizationalUnitId] INT          NOT NULL,
    [Comentary]            VARCHAR (80) NULL,
    [ReasonId]             INT          NULL,
    [CreationUser]         VARCHAR (20) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [ModificationUser]     VARCHAR (20) NULL,
    [ModificationDate]     DATETIME     NULL,
    CONSTRAINT [PK__PVOrgani__3214EC079DCD7329] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVOrganizationalUnitTree_OrganizationalUnit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [HumanTalent].[OrganizationalUnit] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVOrganizationalUnitTree] NOCHECK CONSTRAINT [FK_PVOrganizationalUnitTree_OrganizationalUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la razón ó Motivo ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'ReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'Comentary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad organizacional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVOrganizationalUnitTree', @level2type = N'COLUMN', @level2name = N'Id';

