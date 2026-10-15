CREATE TABLE [MixingStation].[DefectClassificationItem] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [DefectClassificationGroupId] INT           NOT NULL,
    [Description]                 VARCHAR (255) NOT NULL,
    [Weight]                      SMALLINT      NOT NULL,
    [Critical]                    BIT           CONSTRAINT [DF_DefectClassificationItem_Critical] DEFAULT ((0)) NOT NULL,
    [Less]                        BIT           CONSTRAINT [DF_DefectClassificationItem_Less] DEFAULT ((0)) NOT NULL,
    [State]                       BIT           NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [ProductionChemical]          BIT           NULL,
    [QualityChemical]             BIT           CONSTRAINT [DF__DefectCla__Quali__40A0D9E8] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DefectClassificationItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefectClassificationItem_DefectClassificationGroup] FOREIGN KEY ([DefectClassificationGroupId]) REFERENCES [MixingStation].[DefectClassificationGroup] ([Id]),
    CONSTRAINT [IX_DefectClassificationItem] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [MixingStation].[DefectClassificationItem] NOCHECK CONSTRAINT [FK_DefectClassificationItem_DefectClassificationGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectClassificationItem', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectClassificationItem', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectClassificationItem', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectClassificationItem', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Órden en que se mostrará el grupo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectClassificationItem', @level2type = N'COLUMN', @level2name = N'Weight';

