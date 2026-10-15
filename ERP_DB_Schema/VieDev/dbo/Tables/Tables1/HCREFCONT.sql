CREATE TABLE [dbo].[HCREFCONT] (
    [AUTO]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [MUNCODIGO]       CHAR (5)                                                                         NULL,
    [SERVIDOREM]      CHAR (50)                                                                        NOT NULL,
    [CODIGOIPS]       CHAR (100)                                                                       NULL,
    [NIVELREMIT]      CHAR (10)                                                                        NOT NULL,
    [FECSOLICIT]      DATETIME                                                                         NULL,
    [FECCONFIR]       DATETIME                                                                         NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    [CONFIRMAP]       CHAR (100)                                                                       NULL,
    [FECLLEGADA]      DATETIME                                                                         NULL,
    [QUIENSOLRE]      CHAR (100)                                                                       NULL,
    [MOTREMISI]       CHAR (30)                                                                        NULL,
    [OBSERVACIO]      VARCHAR (MAX)                                                                    NULL,
    [CODUSUMOD]       CHAR (20)                                                                        NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [FECREGSIS]       DATETIME                                                                         NULL,
    [CODCONCEC]       INT                                                                              NULL,
    [IDETIPHIS]       CHAR (9)                                                                         NULL,
    [NUMEFOLIO]       NCHAR (10)                                                                       NULL,
    [OBSEREMI]        VARCHAR (MAX)                                                                    NULL,
    [IDHCREFCONP]     INT                                                                              NULL,
    [EXTRAMURAL]      BIT                                                                              CONSTRAINT [DF_HCREFCONT_EXTRAMURAL] DEFAULT ((0)) NULL,
    [RequestType]     INT                                                                              NULL,
    [RequestPriority] INT                                                                              NULL,
    [CODSERIPS]       CHAR (10)                                                                        NULL,
    [QuantityService] INT                                                                              NULL,
    CONSTRAINT [PK_HCREFCONT] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCREFCONT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFCONT_HCREFCONP] FOREIGN KEY ([IDHCREFCONP]) REFERENCES [dbo].[HCREFCONP] ([AUTO]),
    CONSTRAINT [FK_HCREFCONT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [IX_HCREFCONT] UNIQUE NONCLUSTERED ([CODCONCEC] ASC)
);


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_HCREFCONP];


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONT__IPCODPACI__NUMEFOLIO]
    ON [dbo].[HCREFCONT]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Cantidad del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QuantityService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Codigo del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioritaria = 1 
No prioritaria = 2 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestPriority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia = 1
Contrarreferencia = 2 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la Solicitud de referencia por parte del medico fue marcada como extramural.   True    False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';

