CREATE TABLE [Contract].[ControlByTypeFunctionalUnit] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]            INT           NULL,
    [UnitType]               TINYINT       NULL,
    [TypeFunctionalUnit]     VARCHAR (150) NULL,
    [MandatoryAuthorization] BIT           NULL,
    CONSTRAINT [PK_ControlByTypeFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ControlByTypeFunctionalUnit_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[ControlByTypeFunctionalUnit] NOCHECK CONSTRAINT [FK_ControlByTypeFunctionalUnit_CareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autorización obligatoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'MandatoryAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'TypeFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de unidad 1 - Urgencias 2 - Hospitalizacion 3 - Quirofanos 4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

