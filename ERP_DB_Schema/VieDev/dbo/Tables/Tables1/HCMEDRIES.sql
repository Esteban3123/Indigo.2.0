CREATE TABLE [dbo].[HCMEDRIES] (
    [CODCONSEC]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                   CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]                   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                   CHAR (20)                                                                        NOT NULL,
    [MOTSUSMED]                   VARCHAR (2000)                                                                   NULL,
    [NIVRIEPAC]                   CHAR (1)                                                                         NULL,
    [OBSNIVRIE]                   VARCHAR (1000)                                                                   NULL,
    [FECREGIST]                   DATETIME                                                                         NOT NULL,
    [ALERGICO]                    BIT                                                                              NULL,
    [TIPOREGISTRO]                INT                                                                              NULL,
    [ReasonDiscontinuationOfDrug] INT                                                                              NULL,
    CONSTRAINT [PK_HCMEDRIES] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCMEDRIES_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCMEDRIES_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCMEDRIES_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCMEDRIES_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCMEDRIES_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCMEDRIES_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_ADCENATEN];


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_ADINGRESO];


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_INPACIENT];


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_INPROFSAL];


GO
ALTER TABLE [dbo].[HCMEDRIES] NOCHECK CONSTRAINT [FK_HCMEDRIES_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCMEDRIES__IPCODPACI]
    ON [dbo].[HCMEDRIES]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Guardo Medicamento alérgico    
2 - Guardo Medicamento farmacológico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMEDRIES', @level2type = N'COLUMN', @level2name = N'TIPOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si es alergia al medicamento SI / No     0 - INACTIVO    1 - ACTIVO    Si es 0 o Inactivo significa no es alergico ó que eliminaron el farmacologico.   Si es 1 o Activo es decir que si es alergico ó que aun esta como farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMEDRIES', @level2type = N'COLUMN', @level2name = N'ALERGICO';

