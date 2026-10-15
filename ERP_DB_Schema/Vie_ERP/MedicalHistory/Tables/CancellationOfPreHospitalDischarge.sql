CREATE TABLE [MedicalHistory].[CancellationOfPreHospitalDischarge] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [IdHCHISPACA] INT           NOT NULL,
    [CODMOTANU]   CHAR (4)      NOT NULL,
    [Observation] VARCHAR (200) NULL,
    CONSTRAINT [PK_CancellationOfPreHospitalDischarge] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationOfPreHospitalDischarge_HCHISPACA] FOREIGN KEY ([IdHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_CancellationOfPreHospitalDischarge_HCMOANULB] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la observación de la anulación de la pre-alta hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código del motivo de anulación de la tabla HCMOANULB  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el Id de la historia clínica', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'IdHCHISPACA';

