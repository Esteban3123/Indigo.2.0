CREATE TABLE [dbo].[HCALEANULAB] (
    [CODCONSEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODMOTANU] CHAR (2)                                                                         NOT NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECANULE]  DATETIME                                                                         NOT NULL,
    [USUANUALE] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCALEANUL] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCALEANUL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCALEANUL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCALEANUL_HCALEMOTA] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCALEMOTA] ([CODMOTANU]),
    CONSTRAINT [FK_HCALEANUL_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCALEANUL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCALEANUL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_ADCENATEN];


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_HCALEMOTA];


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_INPACIENT];


GO
ALTER TABLE [dbo].[HCALEANULAB] NOCHECK CONSTRAINT [FK_HCALEANUL_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCALEANUL]
    ON [dbo].[HCALEANULAB]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC, [CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que Anula la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'USUANUALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulacion de la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'FECANULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Anulacion de la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEANULAB', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

