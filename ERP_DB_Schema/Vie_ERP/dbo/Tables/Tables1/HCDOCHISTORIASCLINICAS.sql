CREATE TABLE [dbo].[HCDOCHISTORIASCLINICAS] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [USUARIOSUBE]      CHAR (20)                                                                        NOT NULL,
    [NOMBREARCHIVO]    VARCHAR (500)                                                                    NOT NULL,
    [EXTENSIONARCHIVO] VARCHAR (5)                                                                      NOT NULL,
    [TIPODOCUMENTO]    CHAR (3)                                                                         NOT NULL,
    [OBSERVACION]      VARCHAR (MAX)                                                                    NOT NULL,
    CONSTRAINT [PK_HCDOCHISTORIASCLINICAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCDOCHISTORIASCLINICAS] NOCHECK CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCDOCHISTORIASCLINICAS] NOCHECK CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lista tipo de documetos desde la tabla HCCATDOCU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del archivo normalmente son   .PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'EXTENSIONARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'NOMBREARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que esta connectado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'USUARIOSUBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'ID';

