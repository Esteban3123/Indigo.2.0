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

