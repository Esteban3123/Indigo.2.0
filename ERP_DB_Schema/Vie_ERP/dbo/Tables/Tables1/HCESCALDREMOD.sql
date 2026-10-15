CREATE TABLE [dbo].[HCESCALDREMOD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [ACTIVIDAD]   TINYINT                                                                          NOT NULL,
    [RESPIRACION] TINYINT                                                                          NOT NULL,
    [CIRCULACION] TINYINT                                                                          NOT NULL,
    [NIVCONCIEN]  TINYINT                                                                          NOT NULL,
    [SATUOXIGE]   TINYINT                                                                          NOT NULL,
    [APOQUIRUR]   TINYINT                                                                          NOT NULL,
    [DOLOR]       TINYINT                                                                          NOT NULL,
    [DEAMBULAC]   TINYINT                                                                          NOT NULL,
    [ALIMENTAC]   TINYINT                                                                          NOT NULL,
    [MICCION]     TINYINT                                                                          NOT NULL,
    [RESULTADO]   INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCESCALDREMOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALDREMOD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCALDREMOD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCALDREMOD] NOCHECK CONSTRAINT [FK_HCESCALDREMOD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCALDREMOD] NOCHECK CONSTRAINT [FK_HCESCALDREMOD_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Micción 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'MICCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alimentación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ALIMENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deambulación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DEAMBULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dolor 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DOLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aposito Qirurgico 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'APOQUIRUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturacion Oxigeno 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'SATUOXIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel Conciencia 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NIVCONCIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Circulación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CIRCULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESPIRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ID';

