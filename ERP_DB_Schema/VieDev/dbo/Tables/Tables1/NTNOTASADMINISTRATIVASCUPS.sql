CREATE TABLE [dbo].[NTNOTASADMINISTRATIVASCUPS] (
    [ID]                        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTNOTASADMINISTRATIVASD] INT                                                                              NOT NULL,
    [IPCODPACI]                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                 CHAR (10)                                                                        NOT NULL,
    [CODSERIPS]                 CHAR (20)                                                                        NOT NULL,
    [GENSERVICEORDER]           INT                                                                              NULL,
    CONSTRAINT [PK_NTNOTASADMINISTRATIVASCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_INCUPSIPS1] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASCUPS] NOCHECK CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_ADINGRESO];


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASCUPS] NOCHECK CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_INCUPSIPS1];


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASCUPS] NOCHECK CONSTRAINT [FK_NTNOTASADMINISTRATIVASCUPS_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASCUPS', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Servicio IPS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASCUPS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASCUPS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASCUPS', @level2type = N'COLUMN', @level2name = N'ID';

