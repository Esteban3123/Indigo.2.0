CREATE TABLE [dbo].[ADRADICACIONQXD] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRADICACIONQX]  INT           NOT NULL,
    [IDLISTACHEQUEO]  INT           NOT NULL,
    [IDLISTACHEQUEOD] INT           NOT NULL,
    [CHEQUEADO]       BIT           NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500) NULL,
    [CAMPOFECHA]      DATETIME      NULL,
    [CAMPOIDCITA]     INT           NULL,
    [URLADJUNTO]      VARCHAR (100) NULL,
    [TIPOCAMPO]       INT           NULL,
    CONSTRAINT [PK_ADRADICACIONQXD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRADICACIONQXD_ADRADICACIONQXD] FOREIGN KEY ([IDRADICACIONQX]) REFERENCES [dbo].[ADRADICACIONQX] ([ID]),
    CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[ADRADICACIONQXD] NOCHECK CONSTRAINT [FK_ADRADICACIONQXD_ADRADICACIONQXD];


GO
ALTER TABLE [dbo].[ADRADICACIONQXD] NOCHECK CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACC];


GO
ALTER TABLE [dbo].[ADRADICACIONQXD] NOCHECK CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACD];


GO
CREATE NONCLUSTERED INDEX [ADRADICACIONQXD_IDRADICACIONQX_CAMPOFECHA_TIPOCAMPO]
    ON [dbo].[ADRADICACIONQXD]([IDRADICACIONQX] ASC, [CAMPOFECHA] ASC, [TIPOCAMPO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'clasificación del campo:  1 - Autorización  2 - Cita  3 - Cita Externa  4 - Otro Campos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo ID cita medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOIDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Listado chequeo = 1 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Item deatlle de la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de cabecera de radicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'ID';

