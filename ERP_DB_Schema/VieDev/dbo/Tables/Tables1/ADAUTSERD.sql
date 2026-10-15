CREATE TABLE [dbo].[ADAUTSERD] (
    [CODCONCEC]                NUMERIC (18)                                                                     NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                TINYINT                                                                          NOT NULL,
    [CANSERAUT]                TINYINT                                                                          NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [JUSCLISER]                VARCHAR (MAX)                                                                    NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [UFUDESCRI]                CHAR (30)                                                                        NOT NULL,
    [MEDIOSERV]                CHAR (1)                                                                         NOT NULL,
    [PROSERIPS]                BIT                                                                              NOT NULL,
    [ESTATUSER]                CHAR (1)                                                                         NOT NULL,
    [CODDOCALM]                NUMERIC (18)                                                                     NULL,
    [TIPOPCSEL]                CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [CHREGESTAID]              INT                                                                              NULL,
    [SOLEXTRAM]                BIT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK__ADAUTSER__3214EC2703E00E18] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADAUTSERD_ADAUTSERC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADAUTSERC] ([CODCONCEC]),
    CONSTRAINT [FK_ADAUTSERD_ADDOCADIC] FOREIGN KEY ([CODDOCALM]) REFERENCES [dbo].[ADDOCADIC] ([CODDOCALM]),
    CONSTRAINT [FK_ADAUTSERD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTSERD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADAUTSERD] NOCHECK CONSTRAINT [FK_ADAUTSERD_ADAUTSERC];


GO
ALTER TABLE [dbo].[ADAUTSERD] NOCHECK CONSTRAINT [FK_ADAUTSERD_ADDOCADIC];


GO
ALTER TABLE [dbo].[ADAUTSERD] NOCHECK CONSTRAINT [FK_ADAUTSERD_ADINGRESO];


GO
ALTER TABLE [dbo].[ADAUTSERD] NOCHECK CONSTRAINT [FK_ADAUTSERD_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERD_CODCONCEC_CODSERIPS]
    ON [dbo].[ADAUTSERD]([CODCONCEC] ASC, [CODSERIPS] ASC)
    INCLUDE([CANSERIPS]);


GO
CREATE NONCLUSTERED INDEX [ADAUTSERD_CODSERIPS]
    ON [dbo].[ADAUTSERD]([CODSERIPS] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERD_NUMINGRES]
    ON [dbo].[ADAUTSERD]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_ADAUTSERD_IPCODPACI_NUMINGRES]
    ON [dbo].[ADAUTSERD]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desde dashboar autorización solicitud es desde pestaña       pacentes hospitalarios = 0 = False        Solicitudes extramurales = 1= True', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'SOLEXTRAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Autorizacion del Servicio  1: Autorizado  2: No Autorizado  3: Pendiente de Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'ESTATUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Un Medicamento o Un Servicio  1: Servicio  2: Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'MEDIOSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

