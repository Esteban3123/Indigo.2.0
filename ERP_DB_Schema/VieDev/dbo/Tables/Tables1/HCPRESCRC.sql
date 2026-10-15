CREATE TABLE [dbo].[HCPRESCRC] (
    [CODCONCEC]     NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]     NCHAR (10)                                                                       NOT NULL,
    [FECHAORDE]     DATETIME                                                                         NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [INDFARINT]     VARCHAR (MAX)                                                                    NULL,
    [INDFAREXT]     VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]     NUMERIC (18)                                                                     NOT NULL,
    [IDESQUEMAONC]  INT                                                                              NULL,
    [IDHCORDQUIMIO] INT                                                                              NULL,
    CONSTRAINT [PK_HCPRESCRC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCPRESCRC_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESCRC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESCRC_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESCRC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPRESCRC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPRESCRC_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_ADcenaten];


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_INPacient];


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPRESCRC] NOCHECK CONSTRAINT [FK_HCPRESCRC_Schemes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRC', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo:  Id del Consecutivo 00000012', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

