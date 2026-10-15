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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el medicamento fue adicionado manualmente por el químico. NULL o 0 = del ordenamiento médico, 1 = adición manual.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsManualAddition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la dosis ha sido dispensada', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IsDispensed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '15-02-22 a la fecha no se va enviar nada, pero vamos a dejar el campo porque lo vamos a utilizar mas adelante, cualquiere cambio de esto por favor modificar comentario.      ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo donde se almacena la fecha y hora de la postura de la dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDateDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis aplicada   1 = Si     0 = No ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'AppliedDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Cantidad por cobrar', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'QuantityReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 0 ''Sin Entregar''   1 ''Entregado''   2 ''Generado  3 Anulado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'DeliveryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Agrupación Código Dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código de unidad de medida', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'MeasurementUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código de producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código Susceptible Mixing Station', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo relación de la cabecera de farmacia (HCFARMEPC)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaDose', @level2type = N'COLUMN', @level2name = N'Id';

