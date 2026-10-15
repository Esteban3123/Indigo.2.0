CREATE TABLE [dbo].[HCFICHA217] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FIEBRE]              BIT           NULL,
    [ARTRALGIA]           BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [RASH]                BIT           NULL,
    [VOMITO]              BIT           NULL,
    [PIEL]                BIT           NULL,
    [HIGADO]              BIT           NULL,
    [BAZO]                BIT           NULL,
    [PULMON]              BIT           NULL,
    [CEREBRO]             BIT           NULL,
    [MIOCARDIO]           BIT           NULL,
    [MEDULA]              BIT           NULL,
    [RINON]               BIT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA217] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA217_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA217_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA217] NOCHECK CONSTRAINT [CK_HCFICHA217_JSON];


GO
ALTER TABLE [dbo].[HCFICHA217] NOCHECK CONSTRAINT [FK_HCFICHA217_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona RINON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona MEDULA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona MIOCARDIO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona CEREBRO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona PULMON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona BAZO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'BAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona HIGADO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras (Seleccione las muestras tomadas)  Si selecciona PIEL= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Síntomas (Seleccione los que se presenten)  Si selecciona   VOMITO= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Síntomas (Seleccione los que se presenten)  Si selecciona   RASH= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Síntomas (Seleccione los que se presenten)  Si selecciona   CEFALEA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Síntomas (Seleccione los que se presenten)  Si selecciona   ARTRALGIA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ARTRALGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Síntomas (Seleccione los que se presenten)  Si selecciona   Fiebre = True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'FIEBRE';

