CREATE TABLE [dbo].[CHTRAMEDC] (
    [CODCONCEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGTRA]  DATETIME                                                                         NOT NULL,
    [FECREGACE]  DATETIME                                                                         NULL,
    [CODPROENT]  CHAR (20)                                                                        NOT NULL,
    [CODPROACE]  CHAR (20)                                                                        NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODORI]  CHAR (10)                                                                        NOT NULL,
    [UFUCODDES]  CHAR (10)                                                                        NOT NULL,
    [CODICAORI]  INT                                                                              NOT NULL,
    [CODICADES]  INT                                                                              NOT NULL,
    [ORDESTADO]  CHAR (1)                                                                         NOT NULL,
    [JUSANUTRAS] CHAR (250)                                                                       NULL,
    CONSTRAINT [PK_HCFARMDEVC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_CHTRAMEDC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHTRAMEDC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO] FOREIGN KEY ([CODICAORI]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO1] FOREIGN KEY ([CODICADES]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTRAMEDC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHTRAMEDC_INPROFSAL] FOREIGN KEY ([CODPROACE]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHTRAMEDC_INPROFSAL1] FOREIGN KEY ([CODPROENT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC] FOREIGN KEY ([UFUCODORI]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC1] FOREIGN KEY ([UFUCODDES]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_ADCENATEN];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_ADINGRESO];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO1];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_INPACIENT];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_INPROFSAL];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_INPROFSAL1];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC];


GO
ALTER TABLE [dbo].[CHTRAMEDC] NOCHECK CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC1];


GO
CREATE NONCLUSTERED INDEX [IX_CHTRAMEDC_IPCODPACI_NUMINGRES_CODICAORI]
    ON [dbo].[CHTRAMEDC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODICAORI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Orden:  1: Enviado  2: Aceptado  3: Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';

