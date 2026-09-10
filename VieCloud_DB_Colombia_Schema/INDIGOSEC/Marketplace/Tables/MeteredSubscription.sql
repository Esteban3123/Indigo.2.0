CREATE TABLE [Marketplace].[MeteredSubscription] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [SubscriptionId] VARCHAR (75) NOT NULL,
    [Container]      VARCHAR (75) NOT NULL,
    [DimensionId]    INT          NOT NULL,
    [Units]          VARCHAR (10) NOT NULL,
    [EffectiveDate]  DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([DimensionId]) REFERENCES [Marketplace].[MeteredDimensions] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene los registros de notificacion de las diferentes suscripciones, cuando se realiza una notificacion queda un registro unico por suscripcion que almacena la ultima actualizacion correcta realizada y datos necesarios que evalua la libreria de Marketplace', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de suscripción con dimensiones', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription', @level2type = N'COLUMN', @level2name = N'SubscriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor relacionado a la suscripción', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la dimensión', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription', @level2type = N'COLUMN', @level2name = N'DimensionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades correspondientes a la dimensión', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription', @level2type = N'COLUMN', @level2name = N'Units';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha en la que se notificó correctamente la dimensión', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'MeteredSubscription', @level2type = N'COLUMN', @level2name = N'EffectiveDate';

