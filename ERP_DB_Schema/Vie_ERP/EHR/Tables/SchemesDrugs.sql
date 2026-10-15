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
ALTER TABLE [EHR].[SchemesDrugs] NOCHECK CONSTRAINT [FK_SchemesDrugs_IHLISTPRO];


GO
ALTER TABLE [EHR].[SchemesDrugs] NOCHECK CONSTRAINT [FK_SchemesDrugs_IHLISTPRO1];


GO
ALTER TABLE [EHR].[SchemesDrugs] NOCHECK CONSTRAINT [FK_SchemesDrugs_INUNIMEDI];


GO
ALTER TABLE [EHR].[SchemesDrugs] NOCHECK CONSTRAINT [FK_SchemesDrugs_Schemes];


GO
ALTER TABLE [EHR].[SchemesDrugs] NOCHECK CONSTRAINT [FK_SchemesDrugs_SchemesDrugs];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Estandar   2 - Frecuencia   ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypePrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columna es un Identificador de los dias.  Ejemplo:  Rango dias [1,2,3,4,5-10]  SIN Rango dias [1,2,3,4,5,6,7,8,9,10]', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DescriptionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo por minima unidad de medida:', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se administra en casa?    1 - Si   2 - No ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'HomeAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'QuantityDiluent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen final del medicamento y el diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'FinalVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto diluyente ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Instrucciones de Administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'InstructionsAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posicion de aplicacion del medicamento', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Indice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la excepcion es exlusivo para el cambio del lugar de administracion (casa o clinica)  1 - SI  0 - NO', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'ExclusivePlaceAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja alguna excepcion de aplicacion  1- si   0-no', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Exception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -Superficie corporal total  2 -Indice de masa corporal  3 - Peso  4 - Sin factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'TypeFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'texto de Dias en que se aplica el medicamento, separado por coma (,) EJ: 1,8,15,22', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dosis maxima', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DoseMaximum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vía de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugs', @level2type = N'COLUMN', @level2name = N'Id';

