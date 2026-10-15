CREATE TABLE [Contract].[DiscountTypes] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Code]               VARCHAR (20)   NOT NULL,
    [DiscountType]       VARCHAR (50)   NOT NULL,
    [DiscountPercentage] NUMERIC (5, 2) NOT NULL,
    [CreationUser]       VARCHAR (50)   NOT NULL,
    [CreationDate]       DATETIME       NOT NULL,
    [ModificationUser]   VARCHAR (50)   NULL,
    [ModificationDate]   DATETIME       NULL,
    CONSTRAINT [PK_DiscountTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la fecha de la ultima modificación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el código del usuario que realizó la última modificación .', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación del tipo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación del tipo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del tipo de descuento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del tipo de descuento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'DiscountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del tipo de descuento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de tipo de descuento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DiscountTypes', @level2type = N'COLUMN', @level2name = N'Id';

