CREATE TABLE [dbo].[HCHOJMEZINF] (
    [Id]             INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IDHCINFLIQC]    NUMERIC (18)                                                                     NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [FECHAPLICACION] DATE                                                                             NOT NULL,
    [HORA]           DATETIME                                                                         NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [VALORINFUSION]  NUMERIC (18, 2)                                                                  NULL,
    CONSTRAINT [PK_HCHOJMEZINF] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CODCENATE_ADCENATENs] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CODCONCEC_HCINFLIQCc] FOREIGN KEY ([IDHCINFLIQC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_CODPROSAL_INPROFSALl] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IPCODPACI_INPACIENTt] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_NUMINGRES_NUMINGRESs] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_UFUCODIGO_INUNIFUNCc] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_CODCENATE_ADCENATENs];


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_CODCONCEC_HCINFLIQCc];


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_CODPROSAL_INPROFSALl];


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_IPCODPACI_INPACIENTt];


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_NUMINGRES_NUMINGRESs];


GO
ALTER TABLE [dbo].[HCHOJMEZINF] NOCHECK CONSTRAINT [FK_UFUCODIGO_INUNIFUNCc];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Infusione por la hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'VALORINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha:    Mañana  00:00   01:00   02:00   03:00   04:00   05:00   06:00   07:00   08:00   09:00   10:00   11:00      Tarde:  12:00   13:00   14:00   15:00   16:00   17:00     Noche  18:00   19:00   20:00   21:00   22:00   23:00   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'HORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de aplicacion de esa mezcla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'FECHAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cedula paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de la orden de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'Id';

