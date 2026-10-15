CREATE TABLE [dbo].[HCORDIMAI] (
    [IDETIPHIS]  CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                          NOT NULL,
    [CODCENATE]  CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]  DATETIME                                                                           NOT NULL,
    [CODSERIPS]  CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]  INT                                                                                NOT NULL,
    [OBSSERIPS]  CHAR (250)                                                                         NULL,
    [PRISERIPS]  CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS]  CHAR (1)                                                                           NULL,
    [MANEXTPRO]  BIT                                                                                NOT NULL,
    [CODDIAGNO]  CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [INTERPRET]  VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]  CHAR (20)                                                                          NULL,
    [NUMFOLINT]  NCHAR (10)                                                                         NULL,
    [SERREAINT]  BIT                                                                                NOT NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                       NOT NULL,
    [ESTALEIMG]  BIT                                                                                NOT NULL,
    [FECRECEXA]  DATETIME                                                                           NULL,
    [NOMARCIMG]  CHAR (250)                                                                         NULL,
    [CONCURRE]   ROWVERSION                                                                         NULL,
    [USURECEXA]  CHAR (20)                                                                          NULL,
    [REALINOTIF] BIT                                                                                NOT NULL,
    [NOMRESULT]  VARCHAR (100)                                                                      NULL,
    [SERTRANSC]  BIT                                                                                NULL,
    [SERVALMED]  BIT                                                                                NULL,
    [CODUSUTRA]  CHAR (20)                                                                          NULL,
    [CODPROVAL]  CHAR (20)                                                                          NULL,
    [FECTRASER]  DATETIME                                                                           NULL,
    [FECVALSER]  DATETIME                                                                           NULL,
    [ESTTRASER]  BIT                                                                                NULL,
    [USUOCUREG]  CHAR (20)                                                                          NULL,
    CONSTRAINT [PK_HCORDIMAI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCORDIMAI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDIMAI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDIMAI] NOCHECK CONSTRAINT [FK_HCORDIMAI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDIMAI] NOCHECK CONSTRAINT [FK_HCORDIMAI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Estudio Realizado  3: Imagen Procesada  4: Estudio Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

