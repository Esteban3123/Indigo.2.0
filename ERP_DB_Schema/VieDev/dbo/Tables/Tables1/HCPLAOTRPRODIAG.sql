CREATE TABLE [dbo].[HCPLAOTRPRODIAG] (
    [ID]             INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLAOTRPROC] INT      NOT NULL,
    [CODDIAGNO]      CHAR (4) NOT NULL,
    [PRINCIPAL]      BIT      NOT NULL,
    CONSTRAINT [PK_HCPLAOTRPRODIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCPLAOTRPRODIAG] NOCHECK CONSTRAINT [FK_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para marcar el Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico detalle de Diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPRODIAG', @level2type = N'COLUMN', @level2name = N'ID';

