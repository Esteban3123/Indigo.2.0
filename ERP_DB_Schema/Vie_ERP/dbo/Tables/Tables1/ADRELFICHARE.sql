CREATE TABLE [dbo].[ADRELFICHARE] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [IDFICHAR]      INT                                                                              NOT NULL,
    [ESTADO]        INT                                                                              NOT NULL,
    [USUCREACION]   CHAR (20)                                                                        NOT NULL,
    [FECHACREACION] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_ADRELFICHARE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRELFICHARE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADRELFICHARE_HCRENFICH] FOREIGN KEY ([IDFICHAR]) REFERENCES [dbo].[HCRENFICH] ([ID]),
    CONSTRAINT [FK_ADRELFICHARE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADRELFICHARE] NOCHECK CONSTRAINT [FK_ADRELFICHARE_ADINGRESO];


GO
ALTER TABLE [dbo].[ADRELFICHARE] NOCHECK CONSTRAINT [FK_ADRELFICHARE_HCRENFICH];


GO
ALTER TABLE [dbo].[ADRELFICHARE] NOCHECK CONSTRAINT [FK_ADRELFICHARE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación de la relación Ingreso con la ficha Renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Ficha   0 - No Esta Amarrada Con El Ingreso  1 - Amarrada Con El Ingreso  2 - Inativada La Ficha Renal    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Ficha Renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'IDFICHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Ingreso del Paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del Paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRELFICHARE', @level2type = N'COLUMN', @level2name = N'ID';

