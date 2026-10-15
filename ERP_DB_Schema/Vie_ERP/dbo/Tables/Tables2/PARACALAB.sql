CREATE TABLE [dbo].[PARACALAB] (
    [ID]         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]  CHAR (10)      NOT NULL,
    [CODSERIPS]  CHAR (20)      NOT NULL,
    [CANSERIPS]  INT            NOT NULL,
    [OBSSERIPS]  VARCHAR (2000) NULL,
    [PRISERIPS]  CHAR (1)       NOT NULL,
    [CARGARAUTO] INT            NULL,
    CONSTRAINT [PK_PARACALAB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PARACALAB_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PARACALAB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[PARACALAB] NOCHECK CONSTRAINT [FK_PARACALAB_ADCENATEN];


GO
ALTER TABLE [dbo].[PARACALAB] NOCHECK CONSTRAINT [FK_PARACALAB_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargar automaticamente en la hitoria del recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CARGARAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada para el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'ID';

