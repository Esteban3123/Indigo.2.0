CREATE TABLE [Cost].[StandardCostStructure] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Code]     VARCHAR (20) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Validity] DATE         NULL,
    CONSTRAINT [PK_StandardCostStructure] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de válidez', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructure', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la estructura estandar del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructure', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la estructura estandar de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la estructura estandar de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructure', @level2type = N'COLUMN', @level2name = N'Id';

