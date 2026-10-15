CREATE TABLE [Authorization].[AuthorizationSequenceDetail] (
    [Id]                       INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseAuthorizationC] INT    NOT NULL,
    [IdSequense]               INT    NOT NULL,
    [IdOperatingUnit]          INT    NULL,
    [Next]                     BIGINT CONSTRAINT [DF_AuthorizationSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_AuthorizationSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationSequenceDetail_AuthorizationSequence] FOREIGN KEY ([IdSequenseAuthorizationC]) REFERENCES [Authorization].[AuthorizationSequence] ([Id]),
    CONSTRAINT [FK_AuthorizationSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AuthorizationSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_AuthorizationSequence];


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Authorization].[AuthorizationSequenceDetail] NOCHECK CONSTRAINT [FK_AuthorizationSequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAuthorizationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

