CREATE TABLE [dbo].[HCRADCAMPOS] (
    [ID]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADPLANTRATA] INT             NOT NULL,
    [NUMCAMPO]         VARCHAR (5)     NOT NULL,
    [NOMCAMPO]         VARCHAR (100)   NOT NULL,
    [Y1]               NUMERIC (18, 2) NULL,
    [Y2]               NUMERIC (18, 2) NULL,
    [X1]               NUMERIC (18, 2) NULL,
    [X2]               NUMERIC (18, 2) NULL,
    [ENERGIA]          VARCHAR (5)     NULL,
    [GANTRY]           NUMERIC (18, 2) NULL,
    [COLIMADOR]        NUMERIC (18, 2) NULL,
    [CAMILLA]          NUMERIC (18, 2) NULL,
    [ACCESORIOS]       VARCHAR (5)     NULL,
    [MLC]              NUMERIC (18, 2) NULL,
    [BOLUS]            NUMERIC (18, 2) NULL,
    [DFP]              NUMERIC (18, 2) NULL,
    [PROFUN]           NUMERIC (18, 2) NULL,
    [UM]               NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_HCRADCAMPOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'x2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'x1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'X1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'y2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'y1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'Y1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NOMCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'NUMCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabl de Plan de Tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADPLANTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCAMPOS', @level2type = N'COLUMN', @level2name = N'ID';

