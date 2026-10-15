CREATE TABLE [dbo].[HCFIRMFOL] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODPROCRE] CHAR (20)                                                                        NOT NULL,
    [CODPROFIR] CHAR (20)                                                                        NOT NULL,
    [FECFIRFOL] DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCFIRMFOL] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [NUMEFOLIO] ASC),
    CONSTRAINT [FK_HCFIRMFOL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFIRMFOL] NOCHECK CONSTRAINT [FK_HCFIRMFOL_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Firma Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'FECFIRFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio Consecutivo Unico por Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

