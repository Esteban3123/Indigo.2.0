CREATE TABLE [MedicalHistory].[ChemicalPharmaceuticalNotes] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]    VARCHAR (25)   NOT NULL,
    [NUMINGRES]    CHAR (10)      NOT NULL,
    [CODPROSAL]    VARCHAR (20)   NOT NULL,
    [CODCENATE]    CHAR (10)      NOT NULL,
    [UFUCODIGO]    CHAR (10)      NOT NULL,
    [CODNIVIMP]    CHAR (2)       NULL,
    [Title]        VARCHAR (150)  NOT NULL,
    [Analysis]     VARCHAR (3000) NOT NULL,
    [ActionPlan]   VARCHAR (3000) NOT NULL,
    [DocumentDate] DATETIME       NOT NULL,
    [CreationUser] VARCHAR (20)   NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_ChemicalPharmaceuticalNotes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalHistory.ChemicalPharmaceuticalNotes_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP])
);


GO
ALTER TABLE [MedicalHistory].[ChemicalPharmaceuticalNotes] NOCHECK CONSTRAINT [FK_MedicalHistory.ChemicalPharmaceuticalNotes_HCNIVIMPN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien lo creo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del documento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda plan de acción', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'ActionPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el analisis Químico Farmacéutico Notas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Analysis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripcion titulo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Importancia de Perfil Farmacoterapeutico -  Quimico Famaceutico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotes', @level2type = N'COLUMN', @level2name = N'Id';

