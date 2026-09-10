CREATE TABLE [Marketplace].[ActiveSubscription] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [OfferID]            VARCHAR (75) NOT NULL,
    [AMPSubscriptionId]  VARCHAR (75) NOT NULL,
    [SubscriptionStatus] VARCHAR (40) NOT NULL,
    [IsActive]           BIT          NOT NULL,
    [CreateDate]         DATETIME     NOT NULL,
    [PurchaserEmail]     VARCHAR (70) NOT NULL,
    [PurchaserTenantId]  VARCHAR (75) NOT NULL,
    [PlanId]             VARCHAR (75) NOT NULL,
    [Container]          VARCHAR (10) NOT NULL,
    [IsMetered]          BIT          DEFAULT ('0') NOT NULL,
    [LastDimension]      INT          NULL,
    [LastNotifyDate]     DATETIME     NULL,
    [IsRenewable]        BIT          DEFAULT ((0)) NULL,
    [InitialDate]        DATETIME     NULL,
    [FinalDate]          DATETIME     NULL,
    CONSTRAINT [PK_SUBSCRIPTION_ID] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DIMENSION_ID] FOREIGN KEY ([LastDimension]) REFERENCES [Marketplace].[MeteredDimensions] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene la suscripcion de cada cliente/contenedor, cada uno solo puede tener una suscripcion a la vez por tipo', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la oferta a la que pertenece la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'OfferID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id unico que representa la suscripcion ', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'AMPSubscriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'estado en el que se encuentra la suscripcion.

Subscribed    
UnSubscribed
Suspend', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'SubscriptionStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determina si esta o no activa la suscripcion 0 = no 1 = si', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha en la que se creo la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'CreateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email del comprador', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'PurchaserEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tenant del comprador', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'PurchaserTenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'plan al que petertenece la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'PlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'base de datos del cliente comprador de la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determina si soporta o no dimensiones || 0 = no 1 = si', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'IsMetered';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultima dimension en la que se encontraba el uso de las unidades de la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'LastDimension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ultima notificacion effectiva a microsoft de cambios de dimension', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'LastNotifyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si es recurrente la facturacion mensual', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'IsRenewable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de inicio de la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la suscripcion', @level0type = N'SCHEMA', @level0name = N'Marketplace', @level1type = N'TABLE', @level1name = N'ActiveSubscription', @level2type = N'COLUMN', @level2name = N'FinalDate';

