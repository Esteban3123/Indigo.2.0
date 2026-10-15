CREATE TABLE [Contract].[GroupersCareGroup] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [GroupersId]   INT          NOT NULL,
    [EjectEvent]   INT          CONSTRAINT [DF_GroupersCareGroup_EjectEvent] DEFAULT ((0)) NOT NULL,
    [RealCME]      NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_RealCME] DEFAULT ((0)) NOT NULL,
    [TotalEject]   NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_TotalEject] DEFAULT ((0)) NOT NULL,
    [DocumentDate] DATE         NOT NULL,
    CONSTRAINT [PK_GroupersCareGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroup_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_GroupersCareGroup_Groupers] FOREIGN KEY ([GroupersId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCareGroup] NOCHECK CONSTRAINT [FK_GroupersCareGroup_CareGroup];


GO
ALTER TABLE [Contract].[GroupersCareGroup] NOCHECK CONSTRAINT [FK_GroupersCareGroup_Groupers];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha del documento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el total de expulsión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'TotalEject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el número de CME', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'RealCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece evento de expulsión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'EjectEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Groupers', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'GroupersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Care Group', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'Id';

