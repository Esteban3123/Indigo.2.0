CREATE TABLE [dbo].[INTERCABE] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_INTERCABE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_IPCODPACI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_NUMINGRES_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES])
);


GO
ALTER TABLE [dbo].[INTERCABE] NOCHECK CONSTRAINT [FK_IPCODPACI_INPACIENT];


GO
ALTER TABLE [dbo].[INTERCABE] NOCHECK CONSTRAINT [FK_NUMINGRES_ADINGRESO];


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCABE_7_665821484__K2_3]
    ON [dbo].[INTERCABE]([IPCODPACI] ASC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERCABE_7_665821484__K3_K2]
    ON [dbo].[INTERCABE]([NUMINGRES] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCABE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCABE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCABE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno  - Numero de la Orden Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCABE', @level2type = N'COLUMN', @level2name = N'AUTO';

