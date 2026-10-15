CREATE TABLE [MixingStation].[Campaign] (
    [Id]                INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT        NOT NULL,
    [CampaignQuantity]  INT        NOT NULL,
    [TimeStamp]         ROWVERSION NOT NULL,
    CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Campaign_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);


GO
ALTER TABLE [MixingStation].[Campaign] NOCHECK CONSTRAINT [FK_Campaign_CMConfiguration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de campañas asociadas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la central de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'Id';

