CREATE TABLE [dbo].[PADCHEQUEO] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDLISTACHEQUEO]  INT                                                                              NOT NULL,
    [IDLISTACHEQUEOD] INT                                                                              NOT NULL,
    [CHEQUEADO]       BIT                                                                              NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500)                                                                    NULL,
    [CAMPOFECHA]      DATETIME                                                                         NULL,
    [URLADJUNTO]      VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_PADCHEQUEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADCHEQUEO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_ADINGRESO];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_HCLISTACC];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_HCLISTACD];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_PADCHEQUEO_IPCODPACI]
    ON [dbo].[PADCHEQUEO]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADCHEQUEO_NUMINGRES]
    ON [dbo].[PADCHEQUEO]([NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si fue seleccionada la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del detalle de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificaión del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'ID';

