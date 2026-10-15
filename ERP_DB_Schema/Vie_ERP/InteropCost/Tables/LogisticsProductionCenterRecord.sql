CREATE TABLE [InteropCost].[LogisticsProductionCenterRecord] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [ProductionCenterId] INT           NOT NULL,
    [RecordDate]         DATETIME      NOT NULL,
    [Description]        VARCHAR (500) NULL,
    [Status]             TINYINT       NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [ConfirmUser]        VARCHAR (20)  NULL,
    [ConfirmDate]        DATETIME      NULL,
    CONSTRAINT [PK_LogisticsProductionCenterRecord__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogisticsProductionCenterRecord_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[LogisticsProductionCenterRecord] NOCHECK CONSTRAINT [FK_LogisticsProductionCenterRecord_ProductionCenter];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_LogisticsProductionCenterRecord__Code]
    ON [InteropCost].[LogisticsProductionCenterRecord]([Code] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_LogisticsProductionCenterRecord__ProductionCenterId__RecordDate]
    ON [InteropCost].[LogisticsProductionCenterRecord]([ProductionCenterId] ASC, [RecordDate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se confirmó el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien cofimó el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se modificó el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien modificó el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se creó el registro en la base de datos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien creó el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en que se encuentra el registro:  ==========================  1 - Registrado  2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizó el movimiento que se esta registrando', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'RecordDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de producción al que se le realiza el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'Id';

