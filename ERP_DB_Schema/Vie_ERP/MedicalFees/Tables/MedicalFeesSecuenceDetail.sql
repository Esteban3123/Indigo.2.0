CREATE TABLE [MedicalFees].[MedicalFeesSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseMedicalFeesId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseMedicalFeesD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseMedicalFeesD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseMedicalFeesD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseMedicalFeesD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseMedicalFeesD_SequenseMedicalFeesC] FOREIGN KEY ([SequenseMedicalFeesId]) REFERENCES [MedicalFees].[MedicalFeesSecuence] ([Id])
);


GO
ALTER TABLE [MedicalFees].[MedicalFeesSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseMedicalFeesD_OperatingUnit];


GO
ALTER TABLE [MedicalFees].[MedicalFeesSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseMedicalFeesD_Sequense];


GO
ALTER TABLE [MedicalFees].[MedicalFeesSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseMedicalFeesD_SequenseMedicalFeesC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseMedicalFeesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

