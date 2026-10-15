CREATE TABLE [AccountManagement].[AccountManagementSequenceDetail] (
    [Id]                           INT    IDENTITY (1, 1) NOT NULL,
    [IdSequenseAccountManagementC] INT    NOT NULL,
    [IdSequense]                   INT    NOT NULL,
    [IdOperatingUnit]              INT    NULL,
    [Next]                         BIGINT CONSTRAINT [DF_SequenseAccountManagementD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseAccountManagementD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseAccountManagementD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseAccountManagementD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseAccountManagementD_SequenseAccountManagementC] FOREIGN KEY ([IdSequenseAccountManagementC]) REFERENCES [AccountManagement].[AccountManagementSequence] ([Id])
);


GO
ALTER TABLE [AccountManagement].[AccountManagementSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountManagementD_OperatingUnit];


GO
ALTER TABLE [AccountManagement].[AccountManagementSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountManagementD_Sequense];


GO
ALTER TABLE [AccountManagement].[AccountManagementSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountManagementD_SequenseAccountManagementC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAccountManagementC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

