CREATE TABLE [MedicalHistory].[DetailPhysicalCUM] (
    [Id]                INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCFISIPRO]       INT              NOT NULL,
    [ProductId]         INT              NOT NULL,
    [BatchCode]         VARCHAR (50)     NOT NULL,
    [DateExpiration]    DATETIME         NOT NULL,
    [Hour]              DATETIME         NULL,
    [DispensedQuantity] INT              NOT NULL,
    [UsedQuantity]      INT              NOT NULL,
    [GroupingCodeDose]  UNIQUEIDENTIFIER NULL,
    [ReturnedQuantity]  INT              DEFAULT ((0)) NOT NULL,
CONSTRAINT [PK_DetailPhysicalCUM] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_DetailPhysicalCUM_DispensingLookup]
    ON [MedicalHistory].[DetailPhysicalCUM]([IDHCFISIPRO] ASC, [ProductId] ASC, [GroupingCodeDose] ASC, [BatchCode] ASC)
    INCLUDE([Id], [DispensedQuantity]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta/no utilizada (INT, DEFAULT=0) del medicamento a farmacia; diferencia entre dispensado y consumido para gestión de inventario y costos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Cantidad devuelta', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (UNIQUEIDENTIFIER NULL) que agrupa múltiples dosis de un mismo medicamento; facilita auditoría y control de lotes relacionados.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la agrupacion de dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad consumida/administrada (INT) del medicamento aplicado al paciente por enfermería; incrementa conforme se administran dosis en atención clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'UsedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad utilizada, esto cuando se aplica el medicamento desde enfermeria, aca se va  aumentando la cantidad utilizada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'UsedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'UsedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad dispensada (INT) del medicamento desde el sistema ERP; número de unidades entregadas por farmacia a la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DispensedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad dispensada por vie erp', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DispensedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DispensedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto (DATETIME NULL) de dispensación o administración del medicamento por enfermería; marca temporal de cuando se aplica el fármaco al paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Hour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Hora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Hour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Hour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento (DATETIME) del lote; dato crítico para validar medicamentos vigentes antes de dispensación y uso clínico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DateExpiration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DateExpiration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DateExpiration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de lote (VARCHAR 50) del medicamento; trazabilidad farmacéutica obligatoria para identificación, recall y auditoría de farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del Lote', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del producto/medicamento; referencia al catálogo de fármacos, insumos o dispositivos dispensados en enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK INT) al identificador del examen físico/historia clínica del producto; vincula el registro al acto clínico donde se registra la medicación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'IDHCFISIPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id examen fisico de los producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'IDHCFISIPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'IDHCFISIPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY), consecutivo correlativo de la tabla DetailPhysicalCUM para auditoría de medicamentos dispensados.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del control físico de medicamentos (CUM) dispensados en la historia clínica: registra por cada ítem de medicamento el lote, fecha de vencimiento, cantidades dispensadas, utilizadas y devueltas, agrupadas por dosis dentro de un proceso de dispensación clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM';
