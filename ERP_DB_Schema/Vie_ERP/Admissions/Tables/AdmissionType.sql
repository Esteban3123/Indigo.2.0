CREATE TABLE [Admissions].[AdmissionType] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Code]               VARCHAR (25)  NOT NULL,
    [Name]               VARCHAR (150) NOT NULL,
    [IsSpecialTreatment] BIT           CONSTRAINT [DF_AdmissionType_IsSpecialTreatment] DEFAULT ((0)) NOT NULL,
    [TreatmentType]      TINYINT       NOT NULL,
    [Color]              VARCHAR (50)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [CreationUser]       CHAR (20)     NOT NULL,
    [ModificationDate]   DATETIME      NULL,
    [ModificationUser]   CHAR (20)     NULL,
    CONSTRAINT [PK_AdmissionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha creación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Normal   2-Renal   3-Oncologico - Quimioterapia   4-Oncologico - Radioterapia   5-Oncologico - Braquiterapia       Si no es tratamiento especial "IsSpecialTeaetment" aca debe llenarse en 1 -Normal', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'TreatmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True (1) - tratamiento Especial   False (0) - no es tratamiento especial ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'IsSpecialTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción tipo identificacion', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionType', @level2type = N'COLUMN', @level2name = N'Id';

