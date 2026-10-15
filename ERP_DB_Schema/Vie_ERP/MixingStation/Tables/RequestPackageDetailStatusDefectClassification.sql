CREATE TABLE [MixingStation].[RequestPackageDetailStatusDefectClassification] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusId] INT             NOT NULL,
    [Observation]                  VARCHAR (500)   NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ValidateWeigthNPT]            BIT             NULL,
    [ActualWeight]                 DECIMAL (18, 2) NULL,
    [IndicationSize]               INT             NULL,
    CONSTRAINT [PK_RequestPackageDetailStatusDefectClassification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassification_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatusDefectClassification] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatusDefectClassification_RequestPackageDetailStatus];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la adecuacion NPT tiene defectos en el peso real: 0 - Tiene defectos,  1 - Valida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ValidateWeigthNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationUser';

