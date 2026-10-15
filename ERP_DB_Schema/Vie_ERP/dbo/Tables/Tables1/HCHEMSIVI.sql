CREATE TABLE [dbo].[HCHEMSIVI] (
    [CONSECUTI]  NUMERIC (18)                                                             NOT NULL,
    [REGREGIST]  DATETIME                                                                 NOT NULL,
    [VALORPSPD]  CHAR (10)                                                                NOT NULL,
    [VALFRECARD] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)') NOT NULL,
    [PMCONDUTI]  CHAR (10)                                                                NOT NULL,
    [PMFLUJBOM]  CHAR (10)                                                                NOT NULL,
    [NUVALORPA]  CHAR (10)                                                                NOT NULL,
    [NUVALORPV]  CHAR (10)                                                                NOT NULL,
    [NVALORPTM]  CHAR (10)                                                                NOT NULL,
    [OBSERVACI]  CHAR (400)                                                               NULL,
    CONSTRAINT [PK_HCHEMSIVI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [REGREGIST] ASC),
    CONSTRAINT [FK_HCHEMSIVI_HCHEMODIA] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHEMODIA] ([CONSECUTI])
);


GO
ALTER TABLE [dbo].[HCHEMSIVI] NOCHECK CONSTRAINT [FK_HCHEMSIVI_HCHEMODIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PTM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NVALORPTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PV', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Flujo de Bomba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMFLUJBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conductividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMCONDUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALFRECARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PS/PD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALORPSPD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'REGREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

