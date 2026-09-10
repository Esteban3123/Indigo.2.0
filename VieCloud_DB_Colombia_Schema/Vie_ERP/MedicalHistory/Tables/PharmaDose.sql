CREATE TABLE [MedicalHistory].[PharmaDose] (
    [Id]                                       INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCFARMEPC]                              INT              NOT NULL,
    [CodeSusceptibleMixingStation]             UNIQUEIDENTIFIER NOT NULL,
    [ProductCode]                              VARCHAR (20)     NOT NULL,
    [MeasurementUnitCode]                      VARCHAR (20)     NULL,
    [UnitDoseTypeId]                           INT              NOT NULL,
    [Dose]                                     DECIMAL (18, 2)  NOT NULL,
    [GroupingCodeDose]                         UNIQUEIDENTIFIER NOT NULL,
    [DeliveryStatus]                           TINYINT          NOT NULL,
    [QuantityReceivable]                       INT              NOT NULL,
    [AppliedDose]                              INT              NOT NULL,
    [AppliedDateDose]                          DATETIME         NULL,
    [MixingStationId]                          INT              NULL,
    [IsDispensed]                              BIT              CONSTRAINT [DF_PharmaDose_IsDispensed] DEFAULT ((0)) NOT NULL,
    [IsTransformedProductStandardDispensation] BIT              NULL,
    [IsManualAddition]                         BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PharmaDose] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE NONCLUSTERED INDEX [IX_PharmaDose_GroupingCodeDose] ON [MedicalHistory].[PharmaDose] ([GroupingCodeDose] ASC)
INCLUDE ([Id], [ProductCode], [MeasurementUnitCode], [Dose], [CodeSusceptibleMixingStation]);
GO
CREATE NONCLUSTERED INDEX [IX_PharmaDose_CodeSusceptibleMixingStation] ON [MedicalHistory].[PharmaDose] ([CodeSusceptibleMixingStation] ASC)
INCLUDE ([Id], [GroupingCodeDose], [ProductCode], [MeasurementUnitCode], [Dose]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default 0) que indica si el medicamento fue adicionado manualmente por el químico farmacéutico: 0=del ordenamiento médico prescrito, 1=adición manual del profesional de farmacia. Auditoria de dispensación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsManualAddition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento fue adicionado manualmente por el químico. NULL o 0 = del ordenamiento médico, 1 = adición manual.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsManualAddition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsManualAddition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default 0) que indica si la dosis ha sido dispensada al paciente o unidad funcional: 0=no dispensada, 1=dispensada. Control de entrega de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsDispensed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la dosis ha sido dispensada', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsDispensed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsDispensed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la estación de mezcla/preparación farmacéutica. Campo reservado para uso futuro; actualmente sin envío de datos (desde 15-02-22). Preparación de medicamentos personalizados.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MixingStationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'15-02-22 a la fecha no se va enviar nada, pero vamos a dejar el campo porque lo vamos a utilizar mas adelante, cualquiere cambio de esto por favor modificar comentario.      ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MixingStationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se aplicó/administró la dosis al paciente. Registro temporal de administración farmacológica. Puede ser NULL si aún no se administra.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDateDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo donde se almacena la fecha y hora de la postura de la dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDateDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDateDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (INT) de aplicación de la dosis: 1=dosis aplicada/administrada al paciente, 0=no aplicada. Auditoria de cumplimiento terapéutico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis aplicada   1 = Si     0 = No ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) de unidades de dosis facturables o por cobrar al asegurador/paciente. Cálculo de ingresos y glosa farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'QuantityReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Cantidad por cobrar', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'QuantityReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'QuantityReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de entrega (TINYINT) de la dosis: 0=sin entregar, 1=entregado, 2=generado, 3=anulado. Control de ciclo de dispensación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'DeliveryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0 ''''Sin Entregar''''   1 ''''Entregado''''   2 ''''Generado  3 Anulado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'DeliveryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'DeliveryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) de agrupación o lote de dosis relacionadas. Trazabilidad y agrupamiento lógico de medicamentos dispensados.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Agrupación Código Dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (DECIMAL 18,2) de la cantidad de medicamento prescrito (ej: 500 mg, 10 ml). Unidad fundamental de dosificación farmacológica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tipo de unidad de dosis unitaria (ej: tableta, ampolla, frasco). Clasificación de presentación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) de la unidad de medida de la dosis (ej: mg, ml, g, UI). Estandarización de unidades farmacéuticas; puede ser NULL.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MeasurementUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código de unidad de medida', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MeasurementUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MeasurementUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) único del medicamento/producto farmacéutico en inventario. Identificación de droga, genérico o marca comercial.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código de producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) del código susceptible de la estación de mezcla/preparación. Referencia a infraestructura de farmacotecnia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código Susceptible Mixing Station', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de relación con la cabecera de farmacia (tabla HCFARMEPC). Foreign key de historial farmacéutico de paciente/ingreso. Vinculación de dosis a prescripción.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo relación de la cabecera de farmacia (HCFARMEPC)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY) de cada registro de dosis en la tabla. Primary key, autoincrementable. Clave única de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de dosis farmacéuticas asociadas a la historia clínica del paciente. Contiene la información de cada dosis prescrita o aplicada, incluyendo el producto, la unidad de medida, la cantidad, el estado de entrega y si fue dispensada o agregada manualmente en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la dosis corresponde a un producto transformado (preparado o reconstituydo) que fue dispensado bajo el esquema de dispensación estándar. Valor verdadero cuando el medicamento pasó por un proceso de transformación antes de su entrega.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsTransformedProductStandardDispensation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsTransformedProductStandardDispensation';

GO
CREATE NONCLUSTERED INDEX [IX_PharmaDose_IDHCFARMEPC]
    ON [MedicalHistory].[PharmaDose]([IDHCFARMEPC] ASC)
    INCLUDE([DeliveryStatus]);


GO
CREATE NONCLUSTERED INDEX [IX_PharmaDose_MixingStation_Product_Dispensed]
    ON [MedicalHistory].[PharmaDose]([CodeSusceptibleMixingStation] ASC, [ProductCode] ASC, [IsDispensed] ASC)
    INCLUDE([GroupingCodeDose], [DeliveryStatus], [IDHCFARMEPC]);


GO
