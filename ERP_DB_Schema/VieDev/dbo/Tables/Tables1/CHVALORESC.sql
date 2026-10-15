CREATE TABLE [dbo].[CHVALORESC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC]   INT                                                                              NOT NULL,
    [NOMBRELISTACC] VARCHAR (200)                                                                    NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO] DATETIME                                                                         NOT NULL,
    [CODUSUCRE]     CHAR (25)                                                                        NOT NULL,
    CONSTRAINT [PK_CHVALORESC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVALORESC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHVALORESC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHVALORESC_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_CHVALORESC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHVALORESC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHVALORESC] NOCHECK CONSTRAINT [FK_CHVALORESC_ADCENATEN];


GO
ALTER TABLE [dbo].[CHVALORESC] NOCHECK CONSTRAINT [FK_CHVALORESC_ADINGRESO];


GO
ALTER TABLE [dbo].[CHVALORESC] NOCHECK CONSTRAINT [FK_CHVALORESC_HCLISTACC];


GO
ALTER TABLE [dbo].[CHVALORESC] NOCHECK CONSTRAINT [FK_CHVALORESC_INPACIENT];


GO
ALTER TABLE [dbo].[CHVALORESC] NOCHECK CONSTRAINT [FK_CHVALORESC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario quien diligencia la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que diligencia la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'NOMBRELISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESC', @level2type = N'COLUMN', @level2name = N'ID';

