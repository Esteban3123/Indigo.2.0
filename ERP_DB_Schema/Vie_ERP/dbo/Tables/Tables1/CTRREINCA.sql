CREATE TABLE [dbo].[CTRREINCA] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [FECCONTRO] DATETIME                                                                         NOT NULL,
    [CODUSUCRE] CHAR (20)                                                                        NOT NULL,
    [SENTENCIA] VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_CTRREINCA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CTRREINCA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CTRREINCA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CTRREINCA] NOCHECK CONSTRAINT [FK_CTRREINCA_ADINGRESO];


GO
ALTER TABLE [dbo].[CTRREINCA] NOCHECK CONSTRAINT [FK_CTRREINCA_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sentencia SQL generada para eliminar la orden de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'SENTENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de realizacion del reingreso de camas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'FECCONTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso del paciente al que van a reingresar a la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente al cual lo van a reingresar a la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRREINCA', @level2type = N'COLUMN', @level2name = N'AUTO';

