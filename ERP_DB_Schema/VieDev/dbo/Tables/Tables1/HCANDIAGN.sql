CREATE TABLE [dbo].[HCANDIAGN] (
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [CODCENATE] CHAR (10)                                                                        NULL,
    [UFUCODIGO] CHAR (10)                                                                        NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI] BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [DIAINGEGR] CHAR (1)                                                                         NULL,
    [TIPDIAGNO] CHAR (1)                                                                         NULL,
    [CLADIAGNO] CHAR (2)                                                                         NULL,
    [OBSDIAGNO] CHAR (250)                                                                       NULL,
    [FECDIAGNO] DATETIME                                                                         NULL,
    [FOLDIAGNO] INT                                                                              NULL,
    [DIAESTADO] INT                                                                              NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NULL,
    [TIPDIAGNA] CHAR (1)                                                                         NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCANDIAGN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANDIAGN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANDIAGN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCANDIAGN] NOCHECK CONSTRAINT [FK_HCANDIAGN_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANDIAGN] NOCHECK CONSTRAINT [FK_HCANDIAGN_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de diagnostico:  PR: Pre-operatorio  PO: Post-operatorio  PP: Pre y POst-operatorio  HI: Hipatologico  NA: No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANDIAGN', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de diagnostico  I : Impresion diagnostica  C: Confirmado nuevo  R: confirmado repetido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANDIAGN', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico de:  I : Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANDIAGN', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';

