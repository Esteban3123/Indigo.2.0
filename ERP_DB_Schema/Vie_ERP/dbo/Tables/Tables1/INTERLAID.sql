CREATE TABLE [dbo].[INTERLAID] (
    [AUTO]          INT                                                                             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     INT                                                                             NOT NULL,
    [NUMMUESTRA]    INT                                                                             NOT NULL,
    [ANALITO]       VARCHAR (70)                                                                    NOT NULL,
    [VALOR]         VARCHAR (16) MASKED WITH (FUNCTION = 'partial(0, "Value_Ofuscado", 0)')         NOT NULL,
    [UNIDAD]        VARCHAR (30)                                                                    NOT NULL,
    [OBSERVACION]   VARCHAR (7000) MASKED WITH (FUNCTION = 'partial(0, "Observation_Ofuscado", 0)') NULL,
    [DESCRIPCION]   VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')   NULL,
    [VALORMINIMO]   VARCHAR (15)                                                                    NULL,
    [VALORMAXIMO]   VARCHAR (15)                                                                    NULL,
    [AUTOLABOR]     INT                                                                             NULL,
    [CLASIFICACION] VARCHAR (60)                                                                    NULL,
    CONSTRAINT [PK_INTERLAID] PRIMARY KEY CLUSTERED ([AUTO] ASC, [CODCONCEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de ordenes medicas de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Maximo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Minimo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Resultado Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OIbservacion Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Unidad Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Analito del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'ANALITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de la Cabecera del Resultado de la Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AutoNumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTO';

