CREATE TABLE [EHR].[OncologycalSchemeByDrugsPrescription] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCODCONCEC]           INT             NOT NULL,
    [DrugCode]              CHAR (20)       NOT NULL,
    [RouteOfAdministration] CHAR (2)        NOT NULL,
    [Dose]                  NUMERIC (18, 6) NOT NULL,
    [MeasurementUnit]       CHAR (3)        NOT NULL,
    [MaximumDose]           NUMERIC (18, 6) NULL,
    [Duration]              TINYINT         NOT NULL,
    [CycleFrequency]        VARCHAR (250)   NOT NULL,
    [Days]                  VARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDrugsPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDrugsPrescription_IHLISTPRO] FOREIGN KEY ([DrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos que componen un esquema oncológico de quimioterapia: para cada droga del protocolo define dosis, vía de administración, duración y frecuencia de ciclos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro del medicamento en el esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al esquema oncológico (protocolo de quimioterapia) al que pertenece este medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'IDCODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'IDCODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o droga oncológica prescrita en el esquema de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento (por ejemplo: intravenosa, oral, subcutánea).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento a administrar en cada aplicación del ciclo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis (por ejemplo: mg, mg/m², UI).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis máxima permitida del medicamento, límite de seguridad para el ciclo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'MaximumDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'MaximumDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del ciclo o del tratamiento con este medicamento, expresada en días.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia del ciclo oncológico: indica cada cuánto tiempo se repite la administración del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'CycleFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'CycleFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días específicos dentro del ciclo en que se administra el medicamento (por ejemplo: días 1, 8, 15).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugsPrescription', @level2type = N'COLUMN', @level2name = N'Days';
