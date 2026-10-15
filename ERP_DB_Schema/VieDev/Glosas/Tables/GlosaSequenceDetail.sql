CREATE TABLE [Glosas].[GlosaSequenceDetail] (
    [Id]               INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseGlosaC] INT    NOT NULL,
    [IdSequense]       INT    NOT NULL,
    [IdOperatingUnit]  INT    NULL,
    [Next]             BIGINT CONSTRAINT [DF_GlosaSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_GlosaSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaSequenceDetail_GlosaSequence] FOREIGN KEY ([IdSequenseGlosaC]) REFERENCES [Glosas].[GlosaSequence] ([Id]),
    CONSTRAINT [FK_GlosaSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_GlosaSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_GlosaSequence];


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseGlosaC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

