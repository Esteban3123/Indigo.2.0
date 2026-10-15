CREATE TABLE [Common].[TaxExemptions] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Description]      VARCHAR (300) NOT NULL,
    [ApplicationType]  BIT           NOT NULL,
    [InternalCode]     VARCHAR (20)  NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Status]           BIT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TaxExemptions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo interno de la exoneracion tributaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'InternalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tipo de aplicacion de la exoneracion tributaria 1 - Documento 0 - Institucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'ApplicationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion de la enoxeracion tributaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la exoneracion tributaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la exoneracion tributaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TaxExemptions', @level2type = N'COLUMN', @level2name = N'Id';

