CREATE TABLE [Inventory].[ATCClinicalData] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [DataType]            INT            NOT NULL,
    [Description]         NVARCHAR (MAX) NOT NULL,
    [ControlLaboratoryId] INT            NULL,
    [TimeRequest]         INT            NULL,
    [Frequency]           INT            NULL,
    [DiagnosisId]         INT            NULL,
    [ATCId]               INT            NOT NULL,
    CONSTRAINT [PK_ATCClinicalData_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCClinicalData_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ATCClinicalData_ControlLaboratory] FOREIGN KEY ([ControlLaboratoryId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ATCClinicalData_Diagnosis] FOREIGN KEY ([DiagnosisId]) REFERENCES [Inventory].[Diagnostic] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del diagnóstico clínico asociado (FK a Inventory.Diagnostic); tipo de patología, enfermedad o condición médica de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DiagnosisId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de diagnóstico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DiagnosisId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DiagnosisId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración/control: 1=Hora(s), 2=Día(s), 3=Semana(s), 4=Mes; intervalo de repetición en monitoreo laboratorial o farmacológico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Frequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia:
1.Hora(s)
2.Dia(s)
3.Semana(s)
4.Mes
', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Frequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Frequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido desde solicitud para laboratorio de control (en unidades de frecuencia); plazo de ejecución de examen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'TimeRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de la solicitud para laboratorio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'TimeRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'TimeRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador CUPS del laboratorio de control (FK a Contract.CUPSEntity); código de servicio de diagnóstico laboratorial asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'ControlLaboratoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS de tipo laboratorio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'ControlLaboratoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'ControlLaboratoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del dato clínico, información farmacológica o parámetro de monitoreo del medicamento/tratamiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del dato clínico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato clínico vinculado al medicamento ATC: 1=Advertencias, 2=Posología, 3=Indicaciones, 4=Contraindicaciones, 5=Precauciones, 6=Reacciones Adversas, 7=Indicaciones no farmacológicas, 8=Información al paciente, 9=Laboratorio(s) de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DataType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de dato clínico:

1.Advertencias 
2.Posología 
3.Indicaciones 
4.Contraindicaciones 
5.Precauciones 
6.Reacciones Adversas 
7.Indicaciones no farmacológicas 
8.Información al paciente 
9.Laboratorio(s) de control ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DataType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'DataType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de datos clínicos asociados a medicamento ATC (PK, IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de datos clínicos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de datos clínicos asociados a medicamentos según la clasificación ATC (Anatómica, Terapéutica y Química). Guarda información como tipo de dato clínico, descripción, laboratorio de control, tiempo de solicitud, frecuencia de administración y diagnóstico relacionado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del medicamento o grupo farmacológico según la clasificación ATC (Anatómica, Terapéutica y Química). Relaciona el dato clínico con el medicamento correspondiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCClinicalData', @level2type = N'COLUMN', @level2name = N'ATCId';
