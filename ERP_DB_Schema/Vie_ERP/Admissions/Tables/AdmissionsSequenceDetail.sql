CREATE TABLE [Admissions].[AdmissionsSequenceDetail] (
    [Id]                   INT    IDENTITY (1, 1) NOT NULL,
    [AdmissionsSequenceId] INT    NOT NULL,
    [IdSequense]           INT    NOT NULL,
    [IdOperatingUnit]      INT    NULL,
    [Next]                 BIGINT CONSTRAINT [DF_AdmissionsSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_AdmissionsSequenceDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdmissionsSequenceDetail_AdmissionsSequence] FOREIGN KEY ([AdmissionsSequenceId]) REFERENCES [Admissions].[AdmissionsSequence] ([Id]),
    CONSTRAINT [FK_AdmissionsSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AdmissionsSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequenceDetail', @level2type = N'COLUMN', @level2name = N'AdmissionsSequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

