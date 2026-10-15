CREATE TABLE [MixingStation].[CampaignDetail] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignId]            INT           NOT NULL,
    [ProductionLineId]      INT           NOT NULL,
    [UnitDoseTypeId]        INT           NOT NULL,
    [Observations]          VARCHAR (MAX) NULL,
    [CampaignNumber]        INT           NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [ProductionBasketId]    INT           NULL,
    [BasketUser]            VARCHAR (20)  NULL,
    [BasketDate]            DATETIME      NULL,
    [CampaignStatus]        TINYINT       CONSTRAINT [DF__CampaignD__Campa__6610BF0B] DEFAULT ((1)) NOT NULL,
    [ProcessingDate]        DATETIME      NULL,
    [LabelConfirmationDate] DATETIME      NULL,
    [WorkingAreaId]         INT           NULL,
    [FinishDate]            DATETIME      NULL,
    [PreparationTime]       TIME (7)      NULL,
    CONSTRAINT [PK_CampaignDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetail_Campaign] FOREIGN KEY ([CampaignId]) REFERENCES [MixingStation].[Campaign] ([Id]),
    CONSTRAINT [FK_CampaignDetail_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id]),
    CONSTRAINT [FK_CampaignDetail_WorkingArea] FOREIGN KEY ([WorkingAreaId]) REFERENCES [MixingStation].[WorkingArea] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetail] NOCHECK CONSTRAINT [FK_CampaignDetail_Campaign];


GO
ALTER TABLE [MixingStation].[CampaignDetail] NOCHECK CONSTRAINT [FK_CampaignDetail_UnitDoseType];


GO
ALTER TABLE [MixingStation].[CampaignDetail] NOCHECK CONSTRAINT [FK_CampaignDetail_WorkingArea];


GO
CREATE NONCLUSTERED INDEX [IDX_CampaignDetail_ProductionLineId_CampaignNumber]
    ON [MixingStation].[CampaignDetail]([ProductionLineId] ASC, [CampaignNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del área de trabajo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'WorkingAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Estado Externos de la campaña  1- Abierta : Si no se ha cerrado la Campaña.  2- Cerrada: Si ya se ha cerrado la Campaña.  3- Bloqueada: Opción eventual.  4- Anulada: Opcion Eventual.  5- Procesada:  Cambio Automático al ejecutar la acción Procesar  6- Terminada: La campaña ha acabado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'CampaignStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que asocia la canasta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'BasketDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que asocia la canasta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'BasketUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de canastas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'ProductionBasketId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1 - Calculado  2 - Picking  3 - Validacion de lotes  4 - Confirmado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el no. de la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'CampaignNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'CampaignId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetail', @level2type = N'COLUMN', @level2name = N'Id';

