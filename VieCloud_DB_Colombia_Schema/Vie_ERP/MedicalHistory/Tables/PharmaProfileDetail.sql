CREATE TABLE [MedicalHistory].[PharmaProfileDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaProfileHeaderId] INT             NOT NULL,
    [ProductCode]           CHAR (20)       NOT NULL,
    [Dose]                  VARCHAR (500)   NOT NULL,
    [Quantity]              INT             NOT NULL,
    [RouteofAdministration] VARCHAR (20)    NULL,
    [UnitMeasure]           VARCHAR (20)    NULL,
    [Indications]           VARCHAR (MAX)   NULL,
    [DurationFrequency]     INT             NULL,
    [FrequencyUnit]         INT             NULL,
    [DurationType]          VARCHAR (30)    NULL,
    [Weight]                NUMERIC (18, 2) NULL,
    [TotalVolume]           NUMERIC (18, 2) NULL,
    [AdministrationVolume]  NUMERIC (18, 2) NULL,
    [AdministrationTime]    INT             NULL,
    [InfusionSpeed]         NUMERIC (18, 2) NULL,
    [IDHCINFLIQC]           INT             NULL,
    CONSTRAINT [PK_PharmaProfileDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaProfileDetail_HCVIAADMI] FOREIGN KEY ([RouteofAdministration]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_PharmaProfileDetail_IHLISTPRO] FOREIGN KEY ([ProductCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_PharmaProfileDetail_PharmaProfileHeader] FOREIGN KEY ([PharmaProfileHeaderId]) REFERENCES [MedicalHistory].[PharmaProfileHeader] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla cabecera de mezclas/preparaciones de medicamentos (HCINFLIQC), referencia a preparación farmacéutica compuesta.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera de mezclas - HCINFLIQC ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad de infusión en unidades por tiempo (mL/h, gotas/min), parámetro crítico para administración IV de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de infusion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo total de administración en minutos, duración que tarda la aplicación del medicamento al paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de administración en mL, cantidad exacta de solución/medicamento a infundir por dosis.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total disponible del medicamento/mezcla en mL, cantidad preparada o dispensada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'TotalVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen total', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'TotalVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'TotalVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en kg, parámetro usado para cálculo de dosis según protocolo farmacoterapéutico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de duración del tratamiento (días, semanas, meses, crónico, agudo), clasificación temporal del esquema.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de duracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de frecuencia de administración (horas, días, semanas), intervalo temporal entre dosis.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la frecuencia', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración y frecuencia del medicamento (ej: cada 8 horas por 7 días), patrón de administración.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la frecuencia', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas y diagnósticos para los que se prescribe el medicamento, razones terapéuticas.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis (mg, g, mL, UI, comprimidos), unidad farmacéutica de cuantificación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'UnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'UnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'UnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento (IV, IM, VO, tópica, inhalada, rectal), enlace a tabla HCVIAADMI.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'RouteofAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Via de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'RouteofAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'RouteofAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto a administrar, número de dosis o presentaciones.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento (cantidad + unidad), especificación exacta por administración.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico, identificador único enlazado a tabla IHLISTPRO de inventario.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con cabecera del perfil farmacoterapéutico (PharmaProfileHeader), FK a historial medicamentoso del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'PharmaProfileHeaderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relación con la cabecera del perfil farmacoterapéutico (PharmaProfileHeader)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'PharmaProfileHeaderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'PharmaProfileHeaderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de cada detalle de medicamento en el perfil farmacoterapéutico, clave primaria IDENTITY.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo consecutivo d la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada medicamento o producto farmacéutico incluido en un perfil farmacológico del paciente. Registra dosis, cantidad, vía de administración, indicaciones y parámetros de infusión para cada ítem de la prescripción o fórmula médica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail';
