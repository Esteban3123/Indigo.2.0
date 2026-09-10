CREATE TABLE [dbo].[HCRADTUMORES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT           NOT NULL,
    [NOMBRETUMOR]     VARCHAR (150) NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [USUCREA]         CHAR (20)     NOT NULL,
    CONSTRAINT [PK_HCRADTUMORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADTUMORES_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación del registro de tumor en radioterapia. VARCHAR(20), auditoría de quién registró el dato oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'USUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro tumoral. DATETIME, marca temporal de ingreso del tumor al esquema de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tumor oncológico. VARCHAR(150), denominación clínica del neoplasma (ej: carcinoma, sarcoma, melanoma).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'NOMBRETUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tumor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'NOMBRETUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'NOMBRETUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con esquema de radioterapia (FK→HCRADESQUEMAS). INT, vincula tumor a plan/protocolo de radioterapia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de relacion con el esquema de radioterapia (HCRADESQUEMA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la lesión tumoral. INT IDENTITY, clave primaria para rastrear cada tumor en historia clínica oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de tumores asociados a esquemas de tratamiento oncológico. Guarda el nombre de cada tumor vinculado a un esquema de quimioterapia u oncología, junto con el usuario y la fecha en que fue creado el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES';
