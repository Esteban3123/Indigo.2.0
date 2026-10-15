CREATE TABLE [dbo].[HCQXEQUIP] (
    [CONSEEQQX] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [QXPRINCIP] BIT                                                                              NOT NULL,
    [TIPOEQUIP] CHAR (1)                                                                         NOT NULL,
    [PERFILPRO] CHAR (100)                                                                       NULL,
    CONSTRAINT [PK_HCQXEQUIP] PRIMARY KEY CLUSTERED ([CONSEEQQX] ASC),
    CONSTRAINT [FK_HCQXEQUIP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCQXEQUIP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCQXEQUIP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCQXEQUIP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCQXEQUIP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_INPACIENT];


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCQXEQUIP] NOCHECK CONSTRAINT [FK_HCQXEQUIP_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesion:  1. Cirujano  2. Ayudante  3. Anestesiologo  4. Instrumentadora  5. Circulante  6.Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXEQUIP', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';

