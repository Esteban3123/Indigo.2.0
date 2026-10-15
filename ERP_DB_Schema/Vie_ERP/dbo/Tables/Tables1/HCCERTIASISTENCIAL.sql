CREATE TABLE [dbo].[HCCERTIASISTENCIAL] (
    [ID]              INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODEPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identication_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                      NOT NULL,
    [TEXTOCERTI]      VARCHAR (MAX)                                                                  NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                       NOT NULL,
    [USUARIOREGISTO]  CHAR (20)                                                                      NOT NULL,
    [IDCERTIFICADO]   VARCHAR (6)                                                                    NULL,
    [CODESPECIALIDAD] CHAR (3)                                                                       NOT NULL,
    [CODCENATE]       CHAR (10)                                                                      NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                      NOT NULL,
    CONSTRAINT [PK_HCCERTIASISTENCIAL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_HCPLANDOC] FOREIGN KEY ([IDCERTIFICADO]) REFERENCES [dbo].[HCPLANDOC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INESPECIA] FOREIGN KEY ([CODESPECIALIDAD]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INPACIENT] FOREIGN KEY ([IPCODEPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCERTIASISTENCIAL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_HCPLANDOC];


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_INESPECIA];


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_INPACIENT];


GO
ALTER TABLE [dbo].[HCCERTIASISTENCIAL] NOCHECK CONSTRAINT [FK_HCCERTIASISTENCIAL_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad del Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'CODESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del certificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IDCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que hizo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha en la que se  registro el certificado el usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto del Certificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'TEXTOCERTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Ingreso del Paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'IPCODEPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCERTIASISTENCIAL', @level2type = N'COLUMN', @level2name = N'ID';

