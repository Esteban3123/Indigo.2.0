CREATE TABLE [MedicalDiet].[DietPatientDetail] (
    [Id]                   INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DietControlNursingId] INT      NOT NULL,
    [DietTypeCode]         CHAR (3) NOT NULL,
    CONSTRAINT [PK_DietPatientDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DietControlNursingId] FOREIGN KEY ([DietControlNursingId]) REFERENCES [MedicalDiet].[DietControlNursing] ([Id]),
    CONSTRAINT [FK_DietTypeCode] FOREIGN KEY ([DietTypeCode]) REFERENCES [dbo].[CHTIPDIET] ([CODTIPDIE])
);


GO
ALTER TABLE [MedicalDiet].[DietPatientDetail] NOCHECK CONSTRAINT [FK_DietControlNursingId];


GO
ALTER TABLE [MedicalDiet].[DietPatientDetail] NOCHECK CONSTRAINT [FK_DietTypeCode];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Dieta Control Enfermería Id', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietControlNursingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'Id';

