CREATE TABLE [dbo].[ODOPARDIA] (
    [CONSECDIA] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGNO] CHAR (4)        NOT NULL,
    [INDICECPO] CHAR (1)        NOT NULL,
    [INDICECEO] CHAR (1)        NOT NULL,
    [IMAGENDIA] VARBINARY (MAX) NULL,
    [APLICADIA] CHAR (1)        NOT NULL,
    [COLORSDIA] NCHAR (40)      NULL,
    [OBSERVDIA] NVARCHAR (200)  NULL,
    [ESTADODIA] BIT             NOT NULL,
    CONSTRAINT [PK_ODOPARDIA] PRIMARY KEY CLUSTERED ([CONSECDIA] ASC),
    CONSTRAINT [FK_ODOPARDIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[ODOPARDIA] NOCHECK CONSTRAINT [FK_ODOPARDIA_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del diagnostico    True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'ESTADODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion del diagnostico parametrizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'OBSERVDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color de la Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'COLORSDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para determinar si aplica a  1: Diente  2: Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'APLICADIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagen relacionada al diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'IMAGENDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice CEO    N: No Aplica  C: Cariado  E: Extraido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice CPO    N: No Aplica  C: Cariado  P: Perdido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnosticio CIE 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';

