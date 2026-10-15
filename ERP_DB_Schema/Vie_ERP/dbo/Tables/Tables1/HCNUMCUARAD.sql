CREATE TABLE [dbo].[HCNUMCUARAD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCUADRO]   VARCHAR (15)                                                                     NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECREGISTRO] DATETIME                                                                         NOT NULL,
    [USUCREACION] CHAR (20)                                                                        NOT NULL,
    [PACIEFOTO]   VARBINARY (MAX)                                                                  NULL,
    [FECHACUADRO] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_HCNUMCUARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUMCUARAD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCNUMCUARAD] NOCHECK CONSTRAINT [FK_HCNUMCUARAD_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCNUMCUARAD]
    ON [dbo].[HCNUMCUARAD]([IPCODPACI] ASC, [NUMCUADRO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Cuadro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECHACUADRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuacrio Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Cuadro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'NUMCUADRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'ID';

