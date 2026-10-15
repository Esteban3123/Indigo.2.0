CREATE TABLE [dbo].[HCQXREALI] (
    [CONSECUQX]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [QXPRINCIP]                BIT                                                                              NOT NULL,
    [CANTIDAQX]                INT                                                                              NULL,
    [CODVIAABO]                CHAR (2)                                                                         NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [GENSERVICEORDER2]         INT                                                                              NULL,
    CONSTRAINT [PK_HCQXCIRRE] PRIMARY KEY CLUSTERED ([CONSECUQX] ASC),
    CONSTRAINT [FK_HCQXREALI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCQXREALI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCQXREALI_HCQXVIABO] FOREIGN KEY ([CODVIAABO]) REFERENCES [dbo].[HCQXVIABO] ([CODVIAABO]),
    CONSTRAINT [FK_HCQXREALI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCQXREALI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCQXREALI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_HCQXVIABO];


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_INPACIENT];


GO
ALTER TABLE [dbo].[HCQXREALI] NOCHECK CONSTRAINT [FK_HCQXREALI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de generacion de orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXREALI', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';

