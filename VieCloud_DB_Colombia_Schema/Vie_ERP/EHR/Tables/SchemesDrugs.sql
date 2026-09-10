CREATE TABLE [EHR].[SchemesDrugs] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]                    INT             NOT NULL,
    [DrugCode]                     CHAR (20)       NOT NULL,
    [RouteOfAdministration]        VARCHAR (20)    NOT NULL,
    [Dose]                         NUMERIC (18, 2) NOT NULL,
    [MeasurementUnit]              VARCHAR (20)    NOT NULL,
    [DoseMaximum]                  NUMERIC (18, 2) NULL,
    [Days]                         VARCHAR (2000)  NOT NULL,
    [TypeFactor]                   INT             NOT NULL,
    [Exception]                    BIT             NOT NULL,
    [ExclusivePlaceAdministration] BIT             NOT NULL,
    [Indice]                       INT             NOT NULL,
    [InstructionsAdministration]   VARCHAR (MAX)   NULL,
    [DiluentDrugCode]              CHAR (20)       NULL,
    [FinalVolume]                  NUMERIC (18, 2) NULL,
    [QuantityDiluent]              INT             NULL,
    [HomeAdministration]           INT             NULL,
    [CostMinimumUnitMeasure]       DECIMAL (18, 2) CONSTRAINT [DF_SchemesDrugs_CostMinimumUnitMeasure] DEFAULT ((1)) NULL,
    [DescriptionDays]              VARCHAR (2000)  NULL,
    [TypePrescription]             INT             CONSTRAINT [DF_SchemesDrugs_TypePrescription] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SchemeByDrugs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesDrugs_IHLISTPRO] FOREIGN KEY ([RouteOfAdministration]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_SchemesDrugs_IHLISTPRO1] FOREIGN KEY ([DiluentDrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_SchemesDrugs_INUNIMEDI] FOREIGN KEY ([MeasurementUnit]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_SchemesDrugs_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_SchemesDrugs_SchemesDrugs] FOREIGN KEY ([DrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción: 1=Estándar, 2=Frecuencia (patrón repetido)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Estandar   2 - Frecuencia   ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypePrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de días de aplicación con rango condensado (ej: 1,2,3,4,5-10) o expandido (ej: 1,2,3,4,5,6,7,8,9,10)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DescriptionDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna es un Identificador de los dias.  Ejemplo:  Rango dias [1,2,3,4,5-10]  SIN Rango dias [1,2,3,4,5,6,7,8,9,10]', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DescriptionDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DescriptionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo asociado a la mínima unidad de medida del medicamento (default: 1)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo por minima unidad de medida:', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración en domicilio permitida: 1=Sí, 2=No', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'HomeAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se administra en casa?    1 - Si   2 - No ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'HomeAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'HomeAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de diluyente a añadir para la reconstitución', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'QuantityDiluent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'QuantityDiluent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'QuantityDiluent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen final resultante tras mezclar medicamento con diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'FinalVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen final del medicamento y el diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'FinalVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'FinalVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto diluyente o solución para reconstitución del medicamento (FK a IHLISTPRO.CODPRODUC)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto diluyente ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones detalladas de administración, advertencias o notas especiales para el profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'InstructionsAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones de Administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'InstructionsAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'InstructionsAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición o secuencia de aplicación del medicamento dentro del esquema terapéutico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Indice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posicion de aplicacion del medicamento', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Indice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Indice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepción exclusiva para cambio de lugar de administración (domicilio vs clínica): 1=Sí, 0=No', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'ExclusivePlaceAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si la excepcion es exlusivo para el cambio del lugar de administracion (casa o clinica)  1 - SI  0 - NO', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'ExclusivePlaceAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'ExclusivePlaceAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de excepciones en la aplicación: 1=Sí maneja excepción, 0=No aplica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Exception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja alguna excepcion de aplicacion  1- si   0-no', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Exception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Exception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de cálculo de dosis: 1=Superficie corporal, 2=IMC, 3=Peso, 4=Sin factor ajustable', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypeFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -Superficie corporal total  2 -Indice de masa corporal  3 - Peso  4 - Sin factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypeFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypeFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de aplicación del medicamento en formato texto separado por coma (ej: 1,8,15,22)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'texto de Dias en que se aplica el medicamento, separado por coma (,) EJ: 1,8,15,22', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis máxima permitida del medicamento, umbral superior de seguridad', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DoseMaximum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la dosis maxima', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DoseMaximum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DoseMaximum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis: mg, ml, UI, gramos, comprimidos, etc. (FK a INUNIMEDI.CODUNIMED)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis o cantidad numérica del medicamento a administrar por cada toma', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento: oral, intravenosa, intramuscular, tópica, etc. (FK a HCVIAADMI.CODVIAADM)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico, medicamento o fármaco (FK a IHLISTPRO.CODPRODUC)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema de medicación o tratamiento farmacológico relacionado (FK a EHR.Schemes)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la fila en tabla SchemesDrugs (PK)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos que componen cada esquema farmacológico (protocolo de tratamiento), con la dosis, vía de administración, diluyente, días de aplicación e instrucciones para cada fármaco incluido en el esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs';
