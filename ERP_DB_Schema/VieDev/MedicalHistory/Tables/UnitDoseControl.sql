CREATE TABLE [MedicalHistory].[UnitDoseControl] (
    [Id]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareCenterCode]  CHAR (10)                                                                        NOT NULL,
    [PatientCode]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ProductCode]     VARCHAR (20)                                                                     NOT NULL,
    [UnitDoseTypeId]  INT                                                                              NOT NULL,
    [MixingStationId] INT                                                                              NULL,
    CONSTRAINT [PK_UnitDoseControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientCode_INPACIENT] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_UnitDoseControl_ADCENATEN] FOREIGN KEY ([CareCenterCode]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_UnitDoseControl_ATC] FOREIGN KEY ([ProductCode]) REFERENCES [Inventory].[ATC] ([Code]),
    CONSTRAINT [FK_UnitDoseControl_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MedicalHistory].[UnitDoseControl] NOCHECK CONSTRAINT [FK_PatientCode_INPACIENT];


GO
ALTER TABLE [MedicalHistory].[UnitDoseControl] NOCHECK CONSTRAINT [FK_UnitDoseControl_ADCENATEN];


GO
ALTER TABLE [MedicalHistory].[UnitDoseControl] NOCHECK CONSTRAINT [FK_UnitDoseControl_ATC];


GO
ALTER TABLE [MedicalHistory].[UnitDoseControl] NOCHECK CONSTRAINT [FK_UnitDoseControl_UnitDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'15-02-22 a la fecha no se va enviar nada, pero vamos a dejar el campo porque lo vamos a utilizar mas adelante, cualquiere cambio de esto por favor modificar comentario.      
* Id.de la estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'MixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'UnitDoseControl', @level2type = N'COLUMN', @level2name = N'Id';

