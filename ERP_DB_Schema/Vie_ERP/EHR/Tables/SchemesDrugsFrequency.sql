CREATE TABLE [EHR].[SchemesDrugsFrequency] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesID]      INT             NOT NULL,
    [SchemesDrugsID] INT             NOT NULL,
    [Day]            INT             NOT NULL,
    [Dose1]          NUMERIC (18, 2) NULL,
    [Dose2]          NUMERIC (18, 2) NULL,
    [Dose3]          NUMERIC (18, 2) NULL,
    [Dose4]          NUMERIC (18, 2) NULL,
    [Dose5]          NUMERIC (18, 2) NULL,
    [Hour1]          DATETIME        NULL,
    [Hour2]          DATETIME        NULL,
    [Hour3]          DATETIME        NULL,
    [Hour4]          DATETIME        NULL,
    [Hour5]          DATETIME        NULL,
    CONSTRAINT [PK_SchemesDrugsFrequency] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la hora 5', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la hora 4', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la hora 3', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la hora 2', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la hora 1', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la dosis 5 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la dosis 4  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la dosis 3 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la dosis 2 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la dosis 1 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del esquema X medicamento (EHR.SchemesDrugs)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesDrugsID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'ID';

