CREATE TABLE [Marketplace].[MeteredDimensions] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [PlanId]      VARCHAR (75)  NOT NULL,
    [DisplayName] VARCHAR (100) NOT NULL,
    [DimensionId] VARCHAR (75)  NOT NULL,
    [LimitUnits]  VARCHAR (10)  NOT NULL,
    [OfferId]     VARCHAR (70)  NOT NULL,
    CONSTRAINT [PK_DIMENSION_ID] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene las dimensiones relacionada a las suscripciones existentes, cada suscripcion puede tener una cantidad N de suscripciones', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id unico de la dimension en la base de datos', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del plan', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions', @level2type = N'COLUMN', @level2name = N'PlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del plan', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dimensión actual en la que está el plan', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions', @level2type = N'COLUMN', @level2name = N'DimensionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad máxima de la dimensión', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredDimensions', @level2type = N'COLUMN', @level2name = N'LimitUnits';

